using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public interface ICompanyService : IBaseService
    {
        Task<RespData<TCompany>> AddAsync(CompanyAdd company);

        Task<RespData<TCompany>> GetEntity(string companyId);
    }
}
