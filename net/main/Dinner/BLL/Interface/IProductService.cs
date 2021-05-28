using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public interface IProductService : IBaseService
    {
        RespDataList<TProduct> GetList(int categoryid, int pageSize, int page);

        Task<RespData<TProduct>> GetEntityAsync(int productid);
    }
}
