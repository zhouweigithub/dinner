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
    /// 供货商
    /// </summary>
    [Route("[controller]")]
    public class SupplierController : BaseAuthController
    {

        private readonly ISupplierService _services;

        public SupplierController(ISupplierService service)
        {
            _services = service;
        }



        /// <summary>
        /// 添加供货商信息
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(SpUserAdd data)
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
        /// 删除供货商
        /// </summary>
        /// <param name="id">供货商id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Delete(int id)
        {
            return await _services.DeleteAsync(id);
        }
    }
}
