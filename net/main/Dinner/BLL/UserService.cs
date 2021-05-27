
using System;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model;
using Model.Response.Com;

namespace BLL
{
    public class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(DbService context, ILogger<UserService> logger) : base(context)
        {
            _logger = logger;
        }

        public TUser GetEntity(String openid)
        {
            try
            {
                return context.Find<TUser>(openid);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return null;
        }
    }
}