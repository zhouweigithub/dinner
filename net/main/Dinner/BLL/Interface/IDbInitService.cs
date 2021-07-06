using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Response.Com;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BLL.Interface
{
    [TransientService]
    public interface IDbInitService
    {
        public Task<RespData> ClearDatas();

        public Task<RespData> CreateInitDatas();

    }
}
