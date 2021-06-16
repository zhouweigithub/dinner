using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;
using ZqUtils.Core.Helpers;

namespace BLL
{
    public class MiniPayService
    {
        //文档地址 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_5_3.shtml

        private readonly ILogger<MiniPayService> _logger;

        public MiniPayService(ILogger<MiniPayService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 预支付
        /// </summary>
        /// <param name="para">支付参数</param>
        /// <returns></returns>
        public RespData<string> PreMiniPay(MiniPayStartPara para)
        {
            RespData<String> result = new RespData<string>();
            try
            {
                //检测支付参数是否正确
                string errMsg = IsValidPrePayPara(para);

                if (!string.IsNullOrWhiteSpace(errMsg))
                {
                    result.code = -2;
                    result.msg = "支付参数异常：" + errMsg;
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

                result.data = jobject["prepay_id"].ToString();
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
        /// 关闭微信订单
        /// </summary>
        /// <param name="mchid">直连商户号</param>
        /// <param name="out_trade_no">商户订单号</param>
        /// <returns></returns>
        public RespData ClosePay(string mchid, string out_trade_no)
        {
            RespData result = new RespData();
            try
            {
                if (string.IsNullOrWhiteSpace(mchid) || string.IsNullOrWhiteSpace(out_trade_no))
                {
                    result.code = -3;
                    result.msg = "参数错误";
                    return result;
                }

                string url = string.Format("https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{0}/close", out_trade_no);

                string postData = string.Format("{{\"mchid\": \"{0}\"}}", mchid);

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
        /// <param name="mchid">直连商户号</param>
        /// <param name="transaction_id">微信支付订单号</param>
        /// <returns></returns>
        public RespData<QueryWxPayResp> QueryPay(string mchid, string transaction_id)
        {
            RespData<QueryWxPayResp> result = new RespData<QueryWxPayResp>();
            try
            {
                if (string.IsNullOrWhiteSpace(mchid) || string.IsNullOrWhiteSpace(transaction_id))
                {
                    result.code = -2;
                    result.msg = "参数错误";
                    return result;
                }

                string url = string.Format("https://api.mch.weixin.qq.com/v3/pay/transactions/id/{0}?mchid={1}", transaction_id, mchid);

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
        /// <param name="para">请求参数</param>
        /// <returns></returns>
        private HttpRequest CreatePostRequest(string url, string para)
        {
            return new HttpRequest()
            {
                Url = url,
                HttpMethod = HttpMethod.Post,
                ContentType = "application/json",
                PostString = para,
                KeepAlive = true
            };
        }

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

        /// <summary>
        /// 创建Get请求对象
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        private HttpRequest CreateGetRequest(string url)
        {
            return new HttpRequest()
            {
                Url = url,
                HttpMethod = HttpMethod.Get,
                KeepAlive = true
            };
        }
    }
}
