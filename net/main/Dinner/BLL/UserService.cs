using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL
{
    public class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(DbService context, ILogger<UserService> logger) : base(context, logger)
        {
            _logger = logger;
        }



        public async Task<RespDataToken<TUser>> GetEntityAsync(String openid)
        {
            RespDataToken<TUser> result = new RespDataToken<TUser>();
            try
            {
                var user = await context.Set<TUser>().Include(a => a.Company).AsNoTracking().FirstOrDefaultAsync(a => a.Code == openid);
                if (user != null)
                {
                    result.data = user;
                }
                else
                {
                    result.code = -2;
                    result.msg = "未找到相关用户信息";
                    result.data = null;
                }
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                result.data = null;
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData<TUser>> AddAsync(UserAdd t)
        {
            RespData<TUser> result = new();
            try
            {
                //先检查用户是否已存在
                var serverInfo = context.Set<TUser>().FirstOrDefault(a => a.Code == t.OpenId);
                if (serverInfo == null)
                {
                    //检查公司信息是否正确
                    var company = context.Set<TCompany>().FirstOrDefault(a => a.Code == t.CompanyCode);
                    if (company == null)
                    {
                        result.code = -1;
                        result.msg = "公司信息不正确";
                        result.data = null;
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
                            Phone = t.Phone,
                            State = 0,
                            Gender = t.Gender,
                        };

                        result.data = await AddAsync(user);
                    }
                }
                else
                {
                    result.code = -2;
                    result.msg = "该用户已存在";
                    result.data = null;
                }
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                result.data = null;
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData<TUser>> UpdateAsync(UserUpdate data, string openid)
        {
            RespData<TUser> result = new();
            try
            {
                int userid = GetUserIdByCode(openid);
                var server = context.Set<TUser>().Find(userid);

                server.Nick = data.Nick;
                server.Phone = data.Phone;
                server.Headimg = data.HeadIng;

                await context.SaveChangesAsync();
                result.data = server;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                result.data = null;
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> DeleteAsync(String openid)
        {
            RespData result = new();
            try
            {
                int userid = GetUserIdByCode(openid);
                context.Remove(new TUser() { Id = userid });
                await context.SaveChangesAsync();
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