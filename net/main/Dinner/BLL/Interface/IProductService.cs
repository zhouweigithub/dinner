using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public interface IProductService : IBaseService
    {
        public Task<RespDataList<TProduct>> GetListAsync(int categoryid, int pageSize, int page);

        public Task<RespData<TProduct>> GetEntityAsync(int productid);
    }
}
