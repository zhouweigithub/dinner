using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;
using Util;

namespace BLL
{
    public class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(DbService context, ILogger<UserService> logger) : base(context)
        {
            _logger = logger;
        }



        RespDataToken<TUser> IUserService.GetEntity(String openid)
        {
            RespDataToken<TUser> result = new RespDataToken<TUser>();
            try
            {
                var user = context.Set<TUser>().FirstOrDefault(a => a.Code == openid);
                if (user != null)
                {
                    result.data = user;
                }
                else
                {
                    result.code = -1;
                    result.msg = "未找到相关用户信息";
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

        public async Task<RespData<TUser>> AddAsync(UserAdd t)
        {
            RespData<TUser> result = new();
            try
            {
                //先检查是否已存在同名用户
                var serverInfo = context.Set<TUser>().FirstOrDefault(a => a.Code == t.OpenId);
                if (serverInfo == null)
                {
                    //检查公司信息是否正确
                    var company = context.Set<TCompany>().FirstOrDefault(a => a.Code == t.CompanyCode);
                    if (company == null)
                    {
                        result.code = -1;
                        result.msg = "公司信息不正确";
                    }
                    else
                    {
                        var user = new TUser()
                        {
                            Nick = t.Nick,
                            Companyid = company.Id,
                            Crtime = DateTime.Now,
                            Code = t.OpenId,
                            Headimg = t.HeadIng,
                            Phone = t.Phone
                        };

                        result.data = await AddAsync(user);
                    }
                }
                else
                {
                    result.code = -1;
                    result.msg = "该用户已存在";
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

    }
}