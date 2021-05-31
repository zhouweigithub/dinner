using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EasyCaching;
using DAL;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLL.Interface
{
    /// <summary>
    /// 数据库基础操作
    /// </summary>
    public interface IBaseService
    {
        //同步方法

        //T Add<T>(T t) where T : class;

        //int Update<T>(T t) where T : class;

        //int Delete<T>(T t) where T : class;


        //异步方法

        public Task<T> AddAsync<T>(T t) where T : class;

        public Task<int> UpdateAsync<T>(T t) where T : class;

        public Task<int> DeleteAsync<T>(T t) where T : class;
    }
}
