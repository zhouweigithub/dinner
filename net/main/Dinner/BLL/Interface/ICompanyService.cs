using Model;
using Model.Request;
using Model.Response.Com;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface ICompanyService : IBaseService
    {
        RespData<TCompany> Add(CompanyAdd company);

        RespData<TCompany> GetEntity(string companyId);
    }
}
