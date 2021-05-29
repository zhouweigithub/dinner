using Model;
using Model.Database;
using Model.Request;
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
        RespDataToken<TUser> GetEntity(String openid);

        Task<RespData<TUser>> AddAsync(UserAdd t);
    }
}
