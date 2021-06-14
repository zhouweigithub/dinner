using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL
{
    /// <summary>
    /// 公告信息
    /// </summary>
    public class NoticeService : BaseService, INoticeService
    {
        private readonly ILogger<NoticeService> _logger;

        public NoticeService(DbService context, ILogger<NoticeService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public async Task<RespData> AddAsync(NoticeAdd data)
        {
            RespData result = new RespData();
            try
            {
                var model = new TNotice()
                {
                    Content = data.Content,
                    StartDate = data.StartDate,
                    EndDate = data.EndDate,
                    Crtime = DateTime.Now,
                };

                await AddAsync(model);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id">公告id</param>
        /// <returns></returns>
        public async Task<RespData> DeleteAsync(Int32 id)
        {
            RespData result = new RespData();
            try
            {
                var model = new TNotice()
                {
                    Id = id
                };

                await DeleteAsync(model);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public async Task<RespDataList<TNotice>> GetListAsync(DateTime startDate, DateTime endDate, int pageSize, int page)
        {
            RespDataList<TNotice> result = new RespDataList<TNotice>();
            try
            {
                var datas = context.Set<TNotice>().AsNoTracking().Where(a => a.StartDate >= startDate && a.EndDate <= endDate);

                datas = datas.Skip(pageSize * (page - 1)).Take(pageSize);

                result.datas = await datas.ToListAsync();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                result.datas = new List<TNotice>();
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 更新公告信息
        /// </summary>
        /// <param name="data">公告信息</param>
        /// <returns></returns>
        public async Task<RespData> UpdateAsync(NoticeUpdate data)
        {
            RespData result = new();
            try
            {
                var model = new TNotice()
                {
                    Id = data.Id,
                    Content = data.Content,
                    EndDate = data.EndDate,
                    StartDate = data.StartDate
                };

                context.Attach(model);
                context.Entry(model).Property(a => a.Content).IsModified = true;
                context.Entry(model).Property(a => a.StartDate).IsModified = true;
                context.Entry(model).Property(a => a.EndDate).IsModified = true;
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
