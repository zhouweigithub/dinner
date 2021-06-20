using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response.Com;

namespace Api.Controllers
{
    /// <summary>
    /// 送货员
    /// </summary>
    [Route("[controller]")]
    public class DeliverController : BaseAuthController
    {

        private readonly IDelivererService _services;

        public DeliverController(IDelivererService service)
        {
            _services = service;
        }


        /// <summary>
        /// 添加送货员信息
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(DlvUserAdd data)
        {
            return await _services.AddAsync(data);
        }


        /// <summary>
        /// 修改自己密码
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> ChangePassword(ModifyPassword data)
        {
            int userid = Convert.ToInt32(GetUserCode());
            return await _services.ChangePasswordAsync(data, userid);
        }


        /// <summary>
        /// 删除送货员
        /// </summary>
        /// <param name="id">送货员id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Delete(int id)
        {
            return await _services.DeleteAsync(id);
        }

    }
}
