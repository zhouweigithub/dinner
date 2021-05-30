﻿#region License
/***
 * Copyright © 2018-2021, 张强 (943620963@qq.com).
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * without warranties or conditions of any kind, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion

using System;
using System.Text;
using ZqUtils.Core.Extensions;
using ZqUtils.Core.WeChat.Interfaces;

namespace ZqUtils.Core.WeChat.Models
{
    /// <summary>
    /// 多客服消息
    /// </summary>
    public class CustomerXmlMsg : IXmlMsg
    {
        /// <summary>
        /// 接收方帐号(收到的OpenID)
        /// </summary>
        public string ToUserName { get; set; }
        
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; set; }
        
        /// <summary>
        /// 消息创建时间(整型)
        /// </summary>
        public int CreateTime { get; set; } = DateTime.Now.ToUnixTime();
        
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; } = "transfer_customer_service";
        
        /// <summary>
        /// 指定会话接入的客服账号
        /// </summary>
        public string KfAccount { get; set; }
        
        /// <summary>
        /// 转换成xml方法
        /// </summary>
        /// <returns>string</returns>
        public string ToXml()
        {
            var sb = new StringBuilder();
            sb.Append("<xml>")
              .Append($"<ToUserName><![CDATA[{ToUserName}]]></ToUserName>")
              .Append($"<FromUserName><![CDATA[{FromUserName}]]></FromUserName>")
              .Append($"<CreateTime>{CreateTime}</CreateTime>")
              .Append($"<MsgType><![CDATA[{MsgType}]]></MsgType>");
            if (!KfAccount.IsNull())
            {
                sb.Append("<TransInfo>")
                  .Append($"<KfAccount><![CDATA[{KfAccount}]]></KfAccount>")
                  .Append("</TransInfo>");
            }
            sb.Append("</xml>");
            return sb.ToString();
        }
    }
}
