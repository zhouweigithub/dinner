using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model.Response.Com;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class DbInitService : BaseService, IDbInitService
    {
        public DbInitService(DbService context, ILogger<CompanyService> logger) : base(context, logger)
        {

        }

        /// <summary>
        /// 清空所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<RespData> ClearDatas()
        {
            RespData result = new RespData();
            try
            {
                string sql = @"
SET FOREIGN_KEY_CHECKS=0;
TRUNCATE TABLE t_cart;
TRUNCATE TABLE t_comment;
TRUNCATE TABLE t_feedback;
TRUNCATE TABLE t_message;
TRUNCATE TABLE t_pay;
TRUNCATE TABLE t_order_callback;
TRUNCATE TABLE t_order_coupon;
TRUNCATE TABLE t_order_product;
TRUNCATE TABLE t_order;
TRUNCATE TABLE t_product;
TRUNCATE TABLE t_user_coupon;
TRUNCATE TABLE t_coupon;
TRUNCATE TABLE t_user;
TRUNCATE TABLE t_category;
TRUNCATE TABLE t_company;
SET FOREIGN_KEY_CHECKS=1;
";

                await context.Database.ExecuteSqlRawAsync(sql);
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
            }

            return result;
        }

        /// <summary>
        /// 生成初始数据
        /// </summary>
        /// <returns></returns>
        public async Task<RespData> CreateInitDatas()
        {
            RespData result = new RespData();
            try
            {
                string sql = @"
INSERT INTO `t_category`(NAME,state,crtime)VALUES('主食',0,'2021-06-04'),('辅食',0,'2021-06-04'),('零食',0,'2021-06-04');
INSERT INTO `t_product` (`name`,`category`,`price`,`sales`,`img`,`crtime`)VALUES('鱼香肉丝盖饭','1','15','0','http://n.sinaimg.cn/sinacn10123/171/w640h331/20200229/1503-iqfqmas8437778.jpg','2021-06-04'),('鸡腿饭','2','12','0','http://n.sinaimg.cn/sinacn10123/216/w640h376/20200229/921c-iqfqmas8438134.jpg','2021-06-04'),('小炒肉炒饭','1','18','0','http://n.sinaimg.cn/sinacn10123/235/w640h395/20200229/e5cd-iqfqmas8438004.jpg','2021-06-05');
INSERT INTO `t_company` (`name`,`code`,`address`,`crtime`)VALUES('百度','220154','百度大厦','2021-06-02'),('腾讯','54215','南山腾讯大厦','2021-06-01'),('阿里巴巴','124561','阿里巴巴创新产业基地','2021-06-05');
INSERT INTO `t_user` (`code`,`companyid`,`nick`,`headimg`,`phone`,`gender`,`state`,`crtime`)VALUES('xijinpin','1','优雅的小辣椒','http://i0.hdslb.com/bfs/article/878a6c57bed136d9d176a6eb8289a04787b126bf.jpg','13255487458','0','0','2021-05-01'),('chenyuan','1','阿黎亚在苏黎世','https://c-ssl.duitang.com/uploads/item/201809/01/20180901190625_wmpeq.thumb.1000_0.jpeg','13255487458','1','0','2021-04-01');
INSERT INTO `t_coupon` (`name`,`money`,`start_time`,`end_time`,`crtime`)VALUES('新人大礼包','5','2021-01-01','2022-01-01','2021-01-01'),('4元优惠券','4','2021-01-01','2022-01-01','2021-01-02'),('2元优惠券','2','2021-01-01','2022-01-01','2021-02-01');
INSERT INTO `t_cart` (`userid`,`productid`,`count`,`crtime`)VALUES('1','1','1','2021-02-04'),('1','2','3','2021-02-02'),('2','3','1','2021-01-04');
INSERT INTO `t_user_coupon` (`userid`,`couponid`,`count`,`crtime`)VALUES('1','1','1','2021-05-01'),('1','2','2','2021-06-01');
INSERT INTO `t_feedback` (`userid`,`msg`,`crtime`,`replay`,`replay_time`)VALUES('1','太棒了','2021-09-01','谢谢','2021-10-02');
INSERT INTO `t_order` (id,`userid`,`money`,`coupon_money`,`pay_money`,`state`,`crtime`)VALUES('225533664411','1','100','10','90','0','2021-02-14');
INSERT INTO `t_order_coupon` (`orderid`,`couponid`,`couponName`,`price`,`count`,`money`)VALUES('225533664411','1','任意优惠','4','1','4');
INSERT INTO `t_order_product` (`orderid`,`productid`,`productName`,`price`,`count`,`money`,`img`)VALUES('225533664411','1','什么鸟装备','10','10','100','http://pic.ntimg.cn/file/20150514/3269097_162059846000_2.jpg');
";

                await context.Database.ExecuteSqlRawAsync(sql);
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
            }

            return result;
        }
    }
}
