using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class DataServiceExtention
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<ICompanyService, CompanyService>();
        }
    }
}
