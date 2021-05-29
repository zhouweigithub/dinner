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
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICouponService, CouponService>();
            services.AddTransient<IFeedBackService, FeedBackService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IMyCenter, MyCenter>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPayService, PayService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
