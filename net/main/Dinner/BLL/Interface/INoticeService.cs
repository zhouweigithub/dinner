using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 公告信息
    /// </summary>
    public interface INoticeService
    {
        public Task<RespDataList<TNotice>> GetListAsync(DateTime startDate, DateTime endDate, int pageSize, int page);

        public Task<RespData> AddAsync(NoticeAdd data);

        public Task<RespData> UpdateAsync(NoticeUpdate data);

        public Task<RespData> DeleteAsync(int id);
    }
}