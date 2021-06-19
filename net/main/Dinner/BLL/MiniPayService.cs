using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;
using ZqUtils.Core.Helpers;
using Model.Database;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BLL
{
    /// <summary>
    /// 微信小程序支付功能
    /// </summary>
    public class MiniPayService : BaseService, IMiniPayService
    {
        //文档地址 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_5_3.shtml

        private readonly ILogger<MiniPayService> _logger;
        private readonly IOptions<WxOpenidConfigModel> _wxconfig;
        private readonly IMiniPaySignService _signServer;

        public MiniPayService(DbService context, IMiniPaySignService signServer, ILogger<MiniPayService> logger, IOptions<WxOpenidConfigModel> wxconfig) : base(context, logger)
        {
            _logger = logger;
            _wxconfig = wxconfig;
            _signServer = signServer;
        }

        /// <summary>
        /// 预支付，生成的传话标识有效期：2小时
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <param name="host">回调地址域名信息</param>
        /// <returns></returns>
        public RespData<string> PreMiniPay(string orderid, string host)
        {
            RespData<String> result = new RespData<string>();
            try
            {
                //获取订单详情
                var orderInfo = context.Set<TOrder>().AsNoTracking().Include(a => a.TOrderProduct).Include(b => b.User).FirstOrDefault(c => c.Id == orderid);

                if (orderInfo == null || orderInfo.TOrderProduct.Count == 0 || orderInfo.User == null)
                {
                    result.code = -1;
                    result.msg = "预支付失败：订单信息异常";
                    return result;
                }

                MiniPayStartPara para = new MiniPayStartPara()
                {
                    appid = _wxconfig.Value.AppId,
                    mchid = _wxconfig.Value.Mchid,
                    out_trade_no = orderid,
                    description = orderInfo.TOrderProduct.First().ProductName,
                    openid = orderInfo.User.Code,
                    total = (int)(orderInfo.PayMoney * 100),
                    notify_url = string.Format("{0}/PayNotify/ReceiveWxPayNotyfy", host)
                };

                //检测支付参数是否正确
                string errMsg = IsValidPrePayPara(para);

                if (!string.IsNullOrWhiteSpace(errMsg))
                {
                    result.code = -2;
                    result.msg = "预支付失败：支付参数异常：" + errMsg;
                    return result;
                }

                string url = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";

                string postData = GetPrePayParaJson(para);

                using HttpHelper request = new HttpHelper(CreatePostRequest(url, postData));

                var resp = request.GetResult();

                if (resp.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string msg = string.Format("微信预支付失败！StatusCode：{0}, out_trade_no：{1}", (int)resp.StatusCode, para.out_trade_no);
                    result.code = -3;
                    result.msg = msg;
                    _logger.LogError(msg);
                    return result;
                }

                var jobject = ZqUtils.Core.Extensions.StringExtensions.ToJObject(resp.ResultString);

                //预支付交易会话标识。用于后续接口调用中使用，该值有效期为2小时
                //示例值：wx201410272009395522657a690389285100
                result.data = jobject["prepay_id"].ToString();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "预支付失败：服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 关闭微信订单
        /// 以下情况需要调用关单接口：
        /// 1、商户订单支付失败需要生成新单号重新发起支付，要对原订单号调用关单，避免重复支付；
        /// 2、系统下单后，用户支付超时，系统退出不再受理，避免用户继续，请调用关单接口。
        /// </summary>
        /// <param name="out_trade_no">商户订单号</param>
        /// <returns></returns>
        public RespData ClosePay(string out_trade_no)
        {
            RespData result = new RespData();
            try
            {
                if (string.IsNullOrWhiteSpace(_wxconfig.Value.Mchid) || string.IsNullOrWhiteSpace(out_trade_no))
                {
                    result.code = -3;
                    result.msg = "参数错误";
                    return result;
                }

                string url = string.Format("https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{0}/close", out_trade_no);

                string postData = string.Format("{{\"mchid\": \"{0}\"}}", _wxconfig.Value.Mchid);

                using HttpHelper request = new HttpHelper(CreatePostRequest(url, postData));

                var respCode = request.GetResult().StatusCode;

                if (respCode != System.Net.HttpStatusCode.OK)
                {
                    result.code = -2;
                    result.msg = "关闭订单失败";
                    _logger.LogError(string.Format("微信关闭订单出错！订单号：{0} 返回状态码：{1}", out_trade_no, (int)respCode));
                }
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 查询支付情况
        /// </summary>
        /// <param name="transaction_id">微信支付订单号</param>
        /// <returns></returns>
        public RespData<QueryWxPayResp> QueryPay(string transaction_id)
        {
            RespData<QueryWxPayResp> result = new RespData<QueryWxPayResp>();
            try
            {
                if (string.IsNullOrWhiteSpace(_wxconfig.Value.Mchid) || string.IsNullOrWhiteSpace(transaction_id))
                {
                    result.code = -2;
                    result.msg = "参数错误";
                    return result;
                }

                string url = string.Format("https://api.mch.weixin.qq.com/v3/pay/transactions/id/{0}?mchid={1}", transaction_id, _wxconfig.Value.Mchid);

                using HttpHelper request = new HttpHelper(CreateGetRequest(url));

                var resp = request.GetResult();

                if (resp.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string msg = string.Format("微信查询订单失败！StatusCode：{0}, transaction_id：{1}", (int)resp.StatusCode, transaction_id);
                    result.code = -3;
                    result.msg = msg;
                    _logger.LogError(msg);
                    return result;
                }

                result.data = GetQueryPayResult(resp.ResultString);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }



        /// <summary>
        /// 检查参数是否正确
        /// </summary>
        /// <param name="para">支付参数</param>
        /// <returns></returns>
        private string IsValidPrePayPara(MiniPayStartPara para)
        {
            string msg = string.Empty;
            if (string.IsNullOrWhiteSpace(para.appid))
            {
                msg = "支付参数为空：appid";
                _logger.LogError(msg);
            }
            else if (string.IsNullOrWhiteSpace(para.description))
            {
                msg = "支付参数为空：description";
                _logger.LogError(msg);
            }
            else if (string.IsNullOrWhiteSpace(para.mchid))
            {
                msg = "支付参数为空：mchid";
                _logger.LogError(msg);
            }
            else if (string.IsNullOrWhiteSpace(para.notify_url))
            {
                msg = "支付参数为空：notify_url";
                _logger.LogError(msg);
            }
            else if (string.IsNullOrWhiteSpace(para.openid))
            {
                msg = "支付参数为空：openid";
                _logger.LogError(msg);
            }
            else if (string.IsNullOrWhiteSpace(para.out_trade_no))
            {
                msg = "支付参数为空：out_trade_no";
                _logger.LogError(msg);
            }
            else if (para.total <= 0)
            {
                msg = "支付参数异常：total=" + para.total;
                _logger.LogError(msg);
            }

            if (!string.IsNullOrWhiteSpace(msg))
                _logger.LogError(msg);

            return msg;
        }

        private string GetPrePayParaJson(MiniPayStartPara para)
        {
            object tmp = new
            {
                para.appid,
                para.mchid,
                para.notify_url,
                para.description,
                para.out_trade_no,
                amount = new
                {
                    total = para.total,
                    currency = "CNY"
                },
                payer = new
                {
                    openid = para.openid
                }
            };

            return ZqUtils.Core.Extensions.ObjectExtensions.ToJson(tmp);
        }

        /// <summary>
        /// 创建Post请求对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <returns></returns>
        private HttpRequest CreatePostRequest(string url, string postData)
        {
            var request = new HttpRequest()
            {
                Url = url,
                HttpMethod = HttpMethod.Post,
                ContentType = "application/json",
                PostString = postData,
                KeepAlive = true,
            };

            MiniPaySignPara signPara = new MiniPaySignPara
            {
                Method = "Post",
                Body = postData,
                Path = new Uri(url).PathAndQuery
            };

            //添加头部签名
            request.Header.Add("Authorization", _signServer.Sign(signPara));

            return request;
        }

        /// <summary>
        /// 创建Get请求对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        private HttpRequest CreateGetRequest(string url)
        {
            var request = new HttpRequest()
            {
                Url = url,
                HttpMethod = HttpMethod.Get,
                KeepAlive = true
            };


            MiniPaySignPara signPara = new MiniPaySignPara
            {
                Method = "Get",
                Body = string.Empty,
                Path = new Uri(url).PathAndQuery
            };

            //添加头部签名
            request.Header.Add("Authorization", _signServer.Sign(signPara));

            return request;
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="respString">json串</param>
        /// <returns></returns>
        private QueryWxPayResp GetQueryPayResult(string respString)
        {
            QueryWxPayResp result = new QueryWxPayResp();

            var jobject = ZqUtils.Core.Extensions.StringExtensions.ToJObject(respString);

            result.openid = jobject["payer"]["openid"].ToString();
            result.payer_total = jobject["amount"]["payer_total"].ToString();
            result.out_trade_no = jobject["out_trade_no"].ToString();
            result.success_time = jobject["success_time"].ToString();
            result.trade_state = jobject["trade_state"].ToString();
            result.trade_state_desc = jobject["trade_state_desc"].ToString();
            result.transaction_id = jobject["transaction_id"].ToString();

            return result;
        }

    }
}
