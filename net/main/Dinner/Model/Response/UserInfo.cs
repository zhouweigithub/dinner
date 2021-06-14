using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Database;

namespace Model.Response
{
    /// <summary>
    /// 带QrCode的TUser
    /// </summary>
    public class UserInfo : TUser
    {
        /// <summary>
        /// 二维码名片路径
        /// </summary>
        public string QrCode { get; set; }

        /// <summary>
        /// 根据父类创建新的实例
        /// </summary>
        /// <param name="user">用户人信息</param>
        public UserInfo(TUser user)
        {
            this.Code = user.Code;
            this.Id = user.Id;
            this.Company = user.Company;
            this.Companyid = user.Companyid;
            this.Crtime = user.Crtime;
            this.Gender = user.Gender;
            this.Headimg = user.Headimg;
            this.Nick = user.Nick;
            this.Phone = user.Phone;
            this.State = user.State;
            this.TCart = user.TCart;
            this.TFeedback = user.TFeedback;
            this.TMessage = user.TMessage;
            this.TUserCoupon = user.TUserCoupon;
        }
    }
}
