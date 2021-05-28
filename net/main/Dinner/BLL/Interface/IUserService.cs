using Model;
using Model.Response.Com;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public interface IUserService : IBaseService
    {
        Task<RespDataToken<TUser>> GetEntity(String openid);
    }
}
