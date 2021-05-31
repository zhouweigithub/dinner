using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public interface IUserService : IBaseService
    {
        public Task<RespDataToken<TUser>> GetEntity(String openid);

        public Task<RespData<TUser>> AddAsync(UserAdd t);
    }
}
