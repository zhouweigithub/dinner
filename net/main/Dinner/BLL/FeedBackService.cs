using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL
{
    public class FeedBackService : BaseService, IFeedBackService
    {
        private readonly ILogger<FeedBackService> _logger;

        public FeedBackService(DbService context, ILogger<FeedBackService> logger) : base(context, logger)
        {
            _logger = logger;
        }


        public async Task<RespData> AddAsync(string openid, FeedbackAdd data)
        {
            RespData result = new RespData();
            try
            {
                int userid = GetUserIdByCode(openid);
                var user = new TFeedback()
                {
                    Msg = data.content,
                    Userid = userid,
                    Crtime = DateTime.Now,
                };

                await AddAsync(user);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }


        public async Task<RespDataList<TFeedback>> GetListAsync(String openid)
        {
            RespDataList<TFeedback> result = new RespDataList<TFeedback>();
            try
            {
                int userid = GetUserIdByCode(openid);
                var datas = await context.Set<TFeedback>().AsNoTracking().Where(a => a.Userid == userid).ToListAsync();
                result.datas = datas;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                result.datas = new List<TFeedback>();
                _logger.LogError(e.ToString());
            }

            return result;
        }
    }
}
