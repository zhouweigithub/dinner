using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Database;
using Model.Request;
using Model.Response.Com;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BLL.Interface
{
    /// <summary>
    /// 商品分类
    /// </summary>
    [TransientService]
    public interface ICategoryService
    {
        public Task<RespDataList<TCategory>> GetListAsync();

        public Task<RespData> AddAsync(CategoryAdd data);

        public Task<RespData> UpdateCountAsync(CategoryUpdate data);

        public Task<RespData> DeleteProductsAsync(int id);
    }
}
