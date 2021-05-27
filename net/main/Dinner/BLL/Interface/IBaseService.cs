using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBaseService
    {
        public T Add<T>(T t) where T : class;

        public int Update<T>(T t) where T : class;

        public int Delete<T>(T t) where T : class;
    }
}
