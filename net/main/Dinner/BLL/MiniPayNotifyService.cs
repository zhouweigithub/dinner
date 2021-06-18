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
using ZwUtil;
using Model.Database;

namespace BLL
{
    /// <summary>
    /// 微信支付接收通支付结果通知
    /// </summary>
    public class MiniPayNotifyService : BaseService, IMiniPayNotifyService
    {
        //文档地址 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_5_3.shtml

        private readonly ILogger<MiniPayNotifyService> _logger;
        private readonly IOptions<WxOpenidConfigModel> _wxconfig;

        public MiniPayNotifyService(DbService context, ILogger<MiniPayNotifyService> logger, IOptions<WxOpenidConfigModel> wxconfig) : base(context, logger)
        {
            _wxconfig = wxconfig;
            _logger = logger;
        }

        /// <summary>
        /// 接收并处理微信支付通知结果
        /// </summary>
        /// <param name="notifyContent">通知内容</param>
        public RespData ReceiveWxPayNotyfy(WxPayNotify notifyInfo)
        {
            RespData result = new RespData();
            try
            {
                if (notifyInfo == null)
                {
                    string errmsg = "未获取到微信支付回调数据！";
                    _logger.LogError(errmsg);

                    result.code = -1;
                    result.msg = errmsg;
                    return result;
                };

                //解密后的JSON串
                string sourceJson = AesGcmHelper.AesGcmDecrypt(notifyInfo.resource.associated_data, notifyInfo.resource.nonce, notifyInfo.resource.ciphertext, _wxconfig.Value.AesKey);

                //解密后的数据
                WxPayNotifyData data = JsonHelper.DeserializeObject<WxPayNotifyData>(sourceJson);

                if (data == null)
                {
                    string errmsg = "解密微信支付回调数据失败！原文：" + sourceJson;
                    _logger.LogError(errmsg);

                    result.code = -1;
                    result.msg = errmsg;
                    return result;
                }

                //写入微信回调数据详情到数据库
                context.Set<TWxOrderCallback>().Add(new TWxOrderCallback()
                {
                    Appid = data.appid,
                    Attach = data.attach,
                    BankType = data.bank_type,
                    Currency = data.amount.currency,
                    DeviceId = data.scene_info.device_id,
                    Mchid = data.mchid,
                    Openid = data.payer.openid,
                    OutTradeNo = data.out_trade_no,
                    PayerCurrency = data.amount.payer_currency,
                    PayerTotal = data.amount.payer_total,
                    SuccessTime = data.success_time,
                    Total = data.amount.total,
                    TradeState = data.trade_state,
                    TradeStateDesc = data.trade_state_desc,
                    TradeType = data.trade_type,
                    TransactionId = data.transaction_id
                });

                //写入支付回调核心数据到数据库
                context.Set<TOrderCallback>().Add(new TOrderCallback()
                {
                    Crdate = data.success_time.Date,
                    Crtime = data.success_time,
                    Money = data.amount.payer_total,
                    Orderid = data.out_trade_no,
                    Userid = GetUserIdByCode(data.payer.openid)
                });

                context.SaveChanges();

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());

                result.code = -1;
                result.msg = "服务内部异常";
                return result;
            }

        }

    }
}
