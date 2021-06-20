using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Database;
using Model.Request;
using Model.Response;
using Model.Response.Com;

namespace BLL
{
    /// <summary>
    /// 供货商操作
    /// </summary>
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ILogger<ProductService> _logger;

        public SupplierService(DbService context, ILogger<ProductService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加供货商
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<RespData<SpUser>> AddAsync(SpUserAdd data)
        {
            RespData<SpUser> result = new();
            try
            {
                var t = new SpUser()
                {
                    Username = data.Username,
                    Password = ZqUtils.Core.Helpers.CryptHelper.MD5(data.Password),
                    Name = data.Name,
                    Address = data.Address,
                    Crtime = DateTime.Now,
                    State = 0,
                };

                result.data = await AddAsync(t);
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

        /// <summary>
        /// 删除供货商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RespData> DeleteAsync(Int32 id)
        {
            RespData result = new();
            try
            {
                context.Remove(new SpUser() { Id = id });
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

        /// <summary>
        /// 获取供货商当天的订单
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="spuserid">供货商代码</param>
        /// <returns></returns>
        public async Task<RespDataList<SupplierTask>> GetTasksAsync(DateTime date, int spuserid)
        {
            RespDataList<SupplierTask> result = new RespDataList<SupplierTask>();
            try
            {
                var datas = context.Set<ROrderproductSupplier>().AsNoTracking().Include(a => a.TOrderProduct).Where(b => b.Supplierid == spuserid && b.TOrderProduct.Order.Crdate == date).Select(c => new SupplierTask()
                {
                    ProductId = c.Productid,
                    ProductName = c.TOrderProduct.ProductName,
                    Count = c.TOrderProduct.Count,
                    Type = c.TOrderProduct.Type
                });

                result.datas = await datas.ToListAsync();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                result.datas = new List<SupplierTask>();
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public async Task<RespDataToken<SpUser>> LoginAsync(LoginInfo data)
        {
            RespDataToken<SpUser> result = new RespDataToken<SpUser>();
            try
            {
                if (data == null || string.IsNullOrWhiteSpace(data.Username) || string.IsNullOrWhiteSpace(data.Password))
                {
                    result.code = -2;
                    result.msg = "用户名或密码不能为空";
                    return result;
                }

                //密码加密
                string pswd = ZqUtils.Core.Helpers.CryptHelper.MD5(data.Password, 32);

                result.data = await context.Set<SpUser>().AsNoTracking().FirstOrDefaultAsync(a => a.Username == data.Username && a.Password == pswd);

                if (result.data == null)
                {
                    result.code = -3;
                    result.msg = "用户名或密码不正确";
                    return result;
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


        /// <summary>
        /// 修改自己密码
        /// </summary>
        /// <param name="data">参数</param>
        /// <param name="userid">登录的用户id</param>
        /// <returns></returns>
        public async Task<RespData> ChangePasswordAsync(ModifyPassword data, int userid)
        {
            RespData result = new RespData();
            try
            {
                if (data == null || data.Userid == 0 || data.Userid != userid || string.IsNullOrWhiteSpace(data.Password) || string.IsNullOrWhiteSpace(data.NewPassword))
                {
                    result.code = -2;
                    result.msg = "参数错误";
                    return result;
                }

                var pswd = ZqUtils.Core.Helpers.CryptHelper.MD5(data.Password, 32);
                var serverModel = await context.Set<SpUser>().FirstOrDefaultAsync(a => a.Id == data.Userid && data.Password == pswd);

                if (serverModel == null)
                {
                    result.code = -3;
                    result.msg = "用户名或密码不正确";
                    return result;
                }

                //更新新密码
                var newpswd = ZqUtils.Core.Helpers.CryptHelper.MD5(data.NewPassword, 32);
                serverModel.Password = newpswd;

                context.Update(serverModel);

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
