using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Response.Com;

namespace BLL.Interface
{
    public interface IDbInitService
    {
        public Task<RespData> ClearDatas();

        public Task<RespData> CreateInitDatas();

    }
}
