using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;
using ZwUtil;

namespace BLL
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(DbService context, ILogger<CompanyService> logger) : base(context, logger)
        {
            _logger = logger;
        }



        public async Task<RespData<TCompany>> GetEntity(int companyId)
        {
            RespData<TCompany> result = new RespData<TCompany>();
            try
            {
                result.data = await context.FindAsync<TCompany>(companyId);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData<TCompany>> AddAsync(CompanyAdd company)
        {
            RespData<TCompany> result = new();
            try
            {
                //先检查是否已存在同名公司
                var tmpCompany = context.Set<TCompany>().FirstOrDefault(a => a.Name == company.Name);
                if (tmpCompany == null)
                {
                    var t = new TCompany()
                    {
                        Name = company.Name,
                        Address = company.Address,
                        Crtime = DateTime.Now,
                        Code = Strings.GetRandomString(5, Strings.RandStringType.NumberOnly, Strings.LetterType.LowerOnly)
                    };

                    result.data = await AddAsync(t);
                }
                else
                {
                    result.code = -1;
                    result.msg = "该公司已存在";
                }
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData<TCompany>> UpdateAsync(TCompany data)
        {
            RespData<TCompany> result = new();
            try
            {
                context.Update(data);
                await context.SaveChangesAsync();
                result.data = data;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> DeleteAsync(Int32 companyid)
        {
            RespData result = new();
            try
            {
                context.Remove(new TCompany() { Id = companyid });
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }
    }
}
