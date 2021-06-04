using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public interface ICompanyService : IBaseService
    {
        public Task<RespData<TCompany>> AddAsync(CompanyAdd company);

        public Task<RespData<TCompany>> UpdateAsync(TCompany data);

        public Task<RespData> DeleteAsync(int companyid);

        public Task<RespData<TCompany>> GetEntity(int companyId);
    }
}
