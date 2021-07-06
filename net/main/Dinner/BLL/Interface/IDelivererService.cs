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
    /// 送货员相关
    /// </summary>
    [TransientService]
    public interface IDelivererService
    {
        public Task<RespData<DlvUser>> AddAsync(DlvUserAdd data);

        public Task<RespData> DeleteAsync(int id);

        public Task<RespDataToken<DlvUser>> LoginAsync(LoginInfo data);

        public Task<RespData> ChangePasswordAsync(ModifyPassword data, int userid);

    }
}
