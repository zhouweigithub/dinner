using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BaseService : IBaseService
    {

        protected readonly DbService context;


        public BaseService(DbService context)
        {
            this.context = context;
        }

        public virtual T Add<T>(T t) where T : class
        {
            context.Set<T>().Add(t);
            context.SaveChanges();
            return t;
        }

        public virtual int Delete<T>(T t) where T : class
        {
            context.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return context.SaveChanges();
        }

        public virtual int Update<T>(T t) where T : class
        {
            context.Set<T>().Update(t);
            return context.SaveChanges();
        }
    }
}
