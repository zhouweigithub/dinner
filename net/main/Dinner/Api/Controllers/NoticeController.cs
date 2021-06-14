using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace Api.Controllers
{
    /// <summary>
    /// 公告信息
    /// </summary>
    [Route("[controller]")]
    public class NoticeController : BaseAuthController
    {
        private readonly INoticeService _services;

        /// <summary>
        /// 公告
        /// </summary>
        /// <param name="service"></param>
        public NoticeController(INoticeService service)
        {
            _services = service;
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TNotice>> GetList(DateTime startDate, DateTime endDate, int pageSize, int page)
        {
            return await _services.GetListAsync(startDate, endDate, pageSize, page);
        }

        /// <summary>
        /// 修改公告信息
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Update(NoticeUpdate data)
        {
            return await _services.UpdateAsync(data);
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id">公告id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Delete(int id)
        {
            return await _services.DeleteAsync(id);
        }


        /// <summary>
        /// 添加公告信息
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(NoticeAdd data)
        {
            return await _services.AddAsync(data);
        }
    }
}
