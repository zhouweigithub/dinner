using Model;
using Model.Response.Com;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IUserService : IBaseService
    {
        TUser GetEntity(string openid);
    }
}
