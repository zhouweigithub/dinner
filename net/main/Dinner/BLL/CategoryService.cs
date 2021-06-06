using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model.Database;
using Model.Request;
using Model.Response.Com;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(DbService context, ILogger<CategoryService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<RespData> AddAsync(CategoryAdd data)
        {
            RespData result = new RespData();

            try
            {
                var serverModel = context.Set<TCategory>().FirstOrDefaultAsync(a => a.Name == data.Name);

                if (serverModel == null)
                {
                    //添加新商品
                    await AddAsync(new TCategory()
                    {
                        Name = data.Name,
                        State = 0,
                        Crtime = DateTime.Now,
                    });
                    await context.SaveChangesAsync();
                }
                else
                {
                    result.code = -2;
                    result.msg = "该商品分类已存在";
                }

            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> UpdateCountAsync(CategoryUpdate data)
        {
            RespData result = new RespData();

            try
            {
                var model = new TCategory()
                {
                    Name = data.Name,
                    State = data.State,
                };

                //以下两行代码更新实体时，只会更新实体中有的属性
                context.Attach(model);
                context.Entry(model).Property(a => a.Name).IsModified = true;
                context.Entry(model).Property(a => a.State).IsModified = true;

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespDataList<TCategory>> GetListAsync()
        {
            RespDataList<TCategory> result = new RespDataList<TCategory>();
            try
            {
                var datas = await context.Set<TCategory>().AsNoTracking().Where(a => a.State == 0).ToListAsync();
                result.datas = datas;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> DeleteProductsAsync(int id)
        {
            RespData result = new RespData();

            try
            {
                List<TCategory> Categorys = new List<TCategory>();
                var mod = new TCategory()
                {
                    Id = id,
                };

                await DeleteAsync(Categorys);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }


    }
}
