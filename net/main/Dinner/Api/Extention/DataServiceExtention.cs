using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interface;
using Microsoft.Extensions.DependencyInjection;
using Util.Tools.QrCode;
using Util.Tools.QrCode.QrCoder;

namespace Api
{
    /// <summary>
    /// 注册自定义服务
    /// </summary>
    public static class DataServiceExtention
    {
        /// <summary>
        /// 注册自定义服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomerServices(this IServiceCollection services)
        {
            //数据操作服务
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
            services.AddTransient<IWxService, WxService>();
            services.AddTransient<IDbInitService, DbInitService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IMiniPayNotifyService, MiniPayNotifyService>();
            services.AddTransient<IMiniPayService, MiniPayService>();
            services.AddTransient<IMiniPaySignService, MiniPaySignService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IDelivererService, DelivererService>();

        }
    }
}
