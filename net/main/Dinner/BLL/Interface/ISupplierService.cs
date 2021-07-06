using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Database;
using Model.Request;
using Model.Response;
using Model.Response.Com;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BLL.Interface
{
    /// <summary>
    /// 供货商相关
    /// </summary>
    [TransientService]
    public interface ISupplierService
    {
        public Task<RespData<SpUser>> AddAsync(SpUserAdd data);

        public Task<RespData> DeleteAsync(int id);

        public Task<RespDataList<SupplierTask>> GetTasksAsync(DateTime date, int spuserid);

        public Task<RespDataToken<SpUser>> LoginAsync(LoginInfo data);

        public Task<RespData> ChangePasswordAsync(ModifyPassword data, int userid);
    }
}
