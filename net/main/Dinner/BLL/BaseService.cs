using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLL
{
    public class BaseService : IBaseService
    {

        protected readonly DbService context;


        public BaseService(DbService context)
        {
            this.context = context;
        }


        public virtual async Task<T> AddAsync<T>(T t) where T : class
        {
            await context.Set<T>().AddAsync(t);
            await context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<int> DeleteAsync<T>(T t) where T : class
        {
            context.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return await context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync<T>(T t) where T : class
        {
            context.Set<T>().Update(t);
            return await context.SaveChangesAsync();
        }
    }
}
