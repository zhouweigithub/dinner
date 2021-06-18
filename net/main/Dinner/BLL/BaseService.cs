using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EasyCaching;
using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Database;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class BaseService
    {

        protected readonly DbService context;
        protected readonly ILogger<BaseService> logger;


        public BaseService(DbService context, ILogger<BaseService> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        #region 同步方法

        public virtual T Add<T>(T t) where T : class
        {
            context.Set<T>().Add(t);
            context.SaveChanges();
            return t;
        }

        public virtual Int32 Update<T>(T t) where T : class
        {
            context.Set<T>().Update(t);
            return context.SaveChanges();
        }

        public virtual Int32 Delete<T>(T t) where T : class
        {
            context.Entry(t).State = EntityState.Deleted;
            return context.SaveChanges();
        }

        public virtual int DeleteMultiple<T>(List<T> ts) where T : class
        {
            foreach (var item in ts)
            {
                context.Entry(item).State = EntityState.Deleted;
            }
            return context.SaveChanges();
        }

        #endregion

        #region 异步方法

        public virtual async Task<T> AddAsync<T>(T t) where T : class
        {
            await context.Set<T>().AddAsync(t);
            await context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<int> DeleteAsync<T>(T t) where T : class
        {
            context.Entry(t).State = EntityState.Deleted;
            return await context.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteMultipleAsync<T>(List<T> ts) where T : class
        {
            foreach (var item in ts)
            {
                context.Entry(item).State = EntityState.Deleted;
            }
            return await context.SaveChangesAsync();
        }


        public virtual async Task<int> UpdateAsync<T>(T t) where T : class
        {
            context.Set<T>().Update(t);
            return await context.SaveChangesAsync();
        }

        #endregion


        /// <summary>
        /// 根据UserCode获取Userid
        /// </summary>
        /// <param name="userCode">用户代码（openid）</param>
        /// <returns></returns>
        protected int GetUserIdByCode(string userCode)
        {
            try
            {
                var user = context.Set<TUser>().AsNoTracking().FirstOrDefault(a => a.Code == userCode);
                if (user != null)
                {
                    return user.Id;
                }
                else
                {
                    logger.LogError($"根据UserCode({userCode})获取Userid失败");
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.LogError($"根据UserCode({userCode})获取Userid失败：\r\n{e}");
                return 0;
            }
        }

    }
}
