/*
SQLyog Community v13.1.6 (64 bit)
MySQL - 8.0.24 : Database - dinner
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dinner` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `dinner`;

/*Table structure for table `dlv_deliver_detail` */

DROP TABLE IF EXISTS `dlv_deliver_detail`;

CREATE TABLE `dlv_deliver_detail` (
  `crdate` date NOT NULL COMMENT '日期',
  `deliverid` int NOT NULL COMMENT '送货人id',
  `supplierid` int NOT NULL COMMENT '供货商id',
  `productid` int NOT NULL COMMENT '商品id',
  `product_count` int DEFAULT NULL COMMENT '商品数量',
  PRIMARY KEY (`crdate`,`deliverid`,`supplierid`,`productid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='送货员送货详情';

/*Table structure for table `dlv_deliver_track` */

DROP TABLE IF EXISTS `dlv_deliver_track`;

CREATE TABLE `dlv_deliver_track` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '送货追踪id',
  `deliverid` int NOT NULL COMMENT '送货员id',
  `crdate` date NOT NULL COMMENT '详细日期',
  `crtime` datetime NOT NULL COMMENT '详细时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='送货员送货情况追踪';

/*Table structure for table `dlv_deliver_track_detail` */

DROP TABLE IF EXISTS `dlv_deliver_track_detail`;

CREATE TABLE `dlv_deliver_track_detail` (
  `trackid` int NOT NULL COMMENT '送货追踪id',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态 1到达取货地点 2离开取货地点 3到达送货地点',
  `supplierid` int NOT NULL DEFAULT '0' COMMENT '供货商id',
  `companyid` int NOT NULL DEFAULT '0' COMMENT '送达公司id',
  `crtime` date NOT NULL COMMENT '时间',
  PRIMARY KEY (`trackid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='送货行为追踪';

/*Table structure for table `dlv_exception` */

DROP TABLE IF EXISTS `dlv_exception`;

CREATE TABLE `dlv_exception` (
  `crdate` date NOT NULL COMMENT '日期',
  `delivererid` int NOT NULL COMMENT '送货人id',
  `msg` varchar(256) NOT NULL COMMENT '异常描述',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态 0未处理 1已处理',
  `product_count` int NOT NULL DEFAULT '0' COMMENT '受影响的商品数量',
  `loss_value` int NOT NULL DEFAULT '0' COMMENT '损失评论（元）',
  PRIMARY KEY (`crdate`,`delivererid`),
  KEY `fk_delivererid` (`delivererid`),
  CONSTRAINT `fk_delivererid` FOREIGN KEY (`delivererid`) REFERENCES `dlv_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='送货人送货异常情况';

/*Table structure for table `dlv_user` */

DROP TABLE IF EXISTS `dlv_user`;

CREATE TABLE `dlv_user` (
  `id` int NOT NULL,
  `name` varchar(128) NOT NULL COMMENT '送货人名称',
  `address` varchar(128) NOT NULL COMMENT '送货人地址',
  `state` int NOT NULL COMMENT '状态 0正常 1禁用',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='配送人信息';

/*Table structure for table `his_company_supplier` */

DROP TABLE IF EXISTS `his_company_supplier`;

CREATE TABLE `his_company_supplier` (
  `crdate` date NOT NULL COMMENT '日期',
  `companyid` int NOT NULL COMMENT '公司id',
  `suplierid` int NOT NULL COMMENT '供货商id',
  PRIMARY KEY (`crdate`,`companyid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='公司的主供货商历史记录';

/*Table structure for table `r_company_supplier` */

DROP TABLE IF EXISTS `r_company_supplier`;

CREATE TABLE `r_company_supplier` (
  `companyid` int NOT NULL COMMENT '公司id',
  `suplierid` int NOT NULL COMMENT '供货商id',
  `start_date` date NOT NULL COMMENT '起始日期',
  `end_date` date NOT NULL COMMENT '结束日期',
  PRIMARY KEY (`companyid`,`start_date`),
  KEY `fk_suplierid_2` (`suplierid`),
  CONSTRAINT `fk_companyid` FOREIGN KEY (`companyid`) REFERENCES `t_company` (`id`),
  CONSTRAINT `fk_suplierid_2` FOREIGN KEY (`suplierid`) REFERENCES `sp_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='公司与供货商关系';

/*Table structure for table `r_orderproduct_deliver` */

DROP TABLE IF EXISTS `r_orderproduct_deliver`;

CREATE TABLE `r_orderproduct_deliver` (
  `orderid` varchar(32) NOT NULL COMMENT '订单号',
  `productid` int NOT NULL DEFAULT '0' COMMENT '商品id',
  `delivererid` int NOT NULL DEFAULT '0' COMMENT '送货人',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态 0正常 1未送达',
  `msg` varchar(256) DEFAULT NULL COMMENT '说明',
  PRIMARY KEY (`orderid`,`productid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中的商品的送货人';

/*Table structure for table `r_orderproduct_supplier` */

DROP TABLE IF EXISTS `r_orderproduct_supplier`;

CREATE TABLE `r_orderproduct_supplier` (
  `orderid` varchar(32) NOT NULL COMMENT '订单号',
  `productid` int NOT NULL DEFAULT '0' COMMENT '商品id',
  `supplierid` int NOT NULL DEFAULT '0' COMMENT '供货商',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态 0正常 1未出货',
  `msg` varchar(256) DEFAULT NULL COMMENT '说明',
  PRIMARY KEY (`orderid`,`productid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中的商品的供货商';

/*Table structure for table `r_product_suplier` */

DROP TABLE IF EXISTS `r_product_suplier`;

CREATE TABLE `r_product_suplier` (
  `productid` int NOT NULL COMMENT '商品id',
  `suplierid` int NOT NULL COMMENT '供货商id',
  `type` int NOT NULL DEFAULT '0' COMMENT '类型 0由其供货 1不由其供货',
  `start_date` date NOT NULL COMMENT '开始日期',
  `end_date` date NOT NULL COMMENT '结束日期',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`productid`,`suplierid`,`type`,`start_date`),
  KEY `fk_suplierid` (`suplierid`),
  CONSTRAINT `fk_productid` FOREIGN KEY (`productid`) REFERENCES `t_product` (`id`),
  CONSTRAINT `fk_suplierid` FOREIGN KEY (`suplierid`) REFERENCES `sp_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='部分商品的指定供货商';

/*Table structure for table `sp_exception` */

DROP TABLE IF EXISTS `sp_exception`;

CREATE TABLE `sp_exception` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '异常id',
  `supplierid` int NOT NULL COMMENT '供货商id',
  `crdate` date NOT NULL COMMENT '日期',
  `msg` varchar(256) NOT NULL COMMENT '异常描述',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态 0未处理 1已处理',
  `product_count` int NOT NULL DEFAULT '0' COMMENT '受影响的商品数量',
  `loss_value` int NOT NULL DEFAULT '0' COMMENT '损失评论（元）',
  PRIMARY KEY (`id`),
  KEY `fk_spuser` (`supplierid`),
  CONSTRAINT `fk_spuser` FOREIGN KEY (`supplierid`) REFERENCES `sp_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='供货商供货异常情况';

/*Table structure for table `sp_user` */

DROP TABLE IF EXISTS `sp_user`;

CREATE TABLE `sp_user` (
  `id` int NOT NULL,
  `name` varchar(128) NOT NULL COMMENT '供货商名称',
  `address` varchar(128) NOT NULL COMMENT '供货商地址',
  `state` int NOT NULL COMMENT '状态 0正常 1禁用',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='供货商信息';

/*Table structure for table `t_cart` */

DROP TABLE IF EXISTS `t_cart`;

CREATE TABLE `t_cart` (
  `userid` int NOT NULL COMMENT '用户id',
  `productid` int NOT NULL COMMENT '商品id',
  `count` int NOT NULL DEFAULT '0' COMMENT '商品数量',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`userid`,`productid`),
  KEY `fkuserid_idx` (`userid`),
  KEY `fkproductid_idx` (`productid`) /*!80000 INVISIBLE */,
  CONSTRAINT `fkproductid` FOREIGN KEY (`productid`) REFERENCES `t_product` (`id`),
  CONSTRAINT `fkuseridtt` FOREIGN KEY (`userid`) REFERENCES `t_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='购物车';

/*Table structure for table `t_category` */

DROP TABLE IF EXISTS `t_category`;

CREATE TABLE `t_category` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '商品分类id',
  `name` varchar(32) NOT NULL COMMENT '商品分类名称',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态0正常 1禁用',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='商品分类';

/*Table structure for table `t_comment` */

DROP TABLE IF EXISTS `t_comment`;

CREATE TABLE `t_comment` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `msg` varchar(256) NOT NULL COMMENT '内容',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  KEY `fkorderid_idx` (`orderid`),
  CONSTRAINT `fk_orderid` FOREIGN KEY (`orderid`) REFERENCES `t_order` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='评论';

/*Table structure for table `t_company` */

DROP TABLE IF EXISTS `t_company`;

CREATE TABLE `t_company` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '公司id',
  `name` varchar(64) DEFAULT NULL COMMENT '公司名字',
  `code` varchar(64) DEFAULT NULL COMMENT '唯一编码',
  `address` varchar(128) DEFAULT NULL COMMENT '公司地址',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `IX_CODE` (`code`),
  UNIQUE KEY `IX_NAME` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='公司';

/*Table structure for table `t_coupon` */

DROP TABLE IF EXISTS `t_coupon`;

CREATE TABLE `t_coupon` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '优惠券id',
  `name` varchar(128) NOT NULL COMMENT '优惠卷名称',
  `money` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '优惠金额',
  `start_time` datetime NOT NULL COMMENT '使用开始日期',
  `end_time` datetime NOT NULL COMMENT '使用截止日期',
  `crtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '优惠卷创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='优惠卷信息';

/*Table structure for table `t_feedback` */

DROP TABLE IF EXISTS `t_feedback`;

CREATE TABLE `t_feedback` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `userid` int NOT NULL COMMENT '用户id',
  `msg` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '内容',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  KEY `fkuserid_idx` (`userid`),
  CONSTRAINT `fk_userid` FOREIGN KEY (`userid`) REFERENCES `t_user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户反馈';

/*Table structure for table `t_message` */

DROP TABLE IF EXISTS `t_message`;

CREATE TABLE `t_message` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `message` varchar(640) NOT NULL COMMENT '消息内容',
  `isread` int NOT NULL DEFAULT '0' COMMENT '是否已读(0未读 1已读)',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  KEY `fkuserid_idx` (`userid`),
  CONSTRAINT `fkuserid` FOREIGN KEY (`userid`) REFERENCES `t_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='消息中心';

/*Table structure for table `t_notice` */

DROP TABLE IF EXISTS `t_notice`;

CREATE TABLE `t_notice` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '公告id',
  `content` varchar(512) NOT NULL COMMENT '公告内容',
  `start_date` date NOT NULL COMMENT '公告生效起始日期',
  `end_date` date NOT NULL COMMENT '公告生效结束日期',
  `crtime` datetime NOT NULL COMMENT '创建日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='公告信息';

/*Table structure for table `t_order` */

DROP TABLE IF EXISTS `t_order`;

CREATE TABLE `t_order` (
  `id` varchar(32) NOT NULL COMMENT '订单编号',
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `money` decimal(32,2) NOT NULL DEFAULT '0.00' COMMENT '订单金额',
  `coupon_money` decimal(32,2) NOT NULL DEFAULT '0.00' COMMENT '优惠金额',
  `pay_money` decimal(32,2) NOT NULL DEFAULT '0.00' COMMENT '实际支付金额',
  `phone` varchar(20) DEFAULT NULL COMMENT '手机号',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态（0待支付，1已支付，2已完成，9已取消，10已删除）',
  `crdate` date NOT NULL COMMENT '创建日期',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  KEY `fk_order_userid` (`userid`),
  CONSTRAINT `fk_order_userid` FOREIGN KEY (`userid`) REFERENCES `t_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单信息';

/*Table structure for table `t_order_callback` */

DROP TABLE IF EXISTS `t_order_callback`;

CREATE TABLE `t_order_callback` (
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `money` int NOT NULL DEFAULT '0' COMMENT '金额',
  `crdate` date NOT NULL COMMENT '支付完成日期',
  `crtime` datetime NOT NULL COMMENT '支付完成时间',
  PRIMARY KEY (`orderid`),
  CONSTRAINT `fkorderid` FOREIGN KEY (`orderid`) REFERENCES `t_order` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='微信支付回调';

/*Table structure for table `t_order_coupon` */

DROP TABLE IF EXISTS `t_order_coupon`;

CREATE TABLE `t_order_coupon` (
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `couponid` int NOT NULL DEFAULT '0' COMMENT '优惠卷id',
  `couponName` varchar(128) NOT NULL COMMENT '优惠券名称',
  `price` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '优惠券面额',
  `count` int NOT NULL DEFAULT '0' COMMENT '数量',
  `money` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '总金额',
  PRIMARY KEY (`orderid`,`couponid`),
  KEY `fkcouponss` (`couponid`),
  CONSTRAINT `fkorder` FOREIGN KEY (`orderid`) REFERENCES `t_order` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中使用的优惠卷';

/*Table structure for table `t_order_product` */

DROP TABLE IF EXISTS `t_order_product`;

CREATE TABLE `t_order_product` (
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `productid` int NOT NULL DEFAULT '0' COMMENT '商品id',
  `productName` varchar(128) NOT NULL COMMENT '商品名称',
  `price` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '单价',
  `count` int NOT NULL DEFAULT '0' COMMENT '数量',
  `money` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '金额',
  `type` int NOT NULL DEFAULT '0' COMMENT '类别 0普通 1早餐 2午餐 3晚餐',
  `img` varchar(256) NOT NULL COMMENT '商品图片',
  PRIMARY KEY (`orderid`,`productid`),
  KEY `fkproducts` (`productid`),
  CONSTRAINT `fkorders` FOREIGN KEY (`orderid`) REFERENCES `t_order` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中的商品信息';

/*Table structure for table `t_pay` */

DROP TABLE IF EXISTS `t_pay`;

CREATE TABLE `t_pay` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `wx_orderid` varchar(64) NOT NULL COMMENT '微信订单号',
  `status` int NOT NULL COMMENT '状态',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  KEY `fk_orderid2` (`orderid`),
  CONSTRAINT `fk_orderid2` FOREIGN KEY (`orderid`) REFERENCES `t_order` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='支付信息';

/*Table structure for table `t_product` */

DROP TABLE IF EXISTS `t_product`;

CREATE TABLE `t_product` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '商品id',
  `name` varchar(128) NOT NULL COMMENT '商品名称',
  `category` int NOT NULL DEFAULT '0' COMMENT '商品分类',
  `price` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '价格',
  `sales` int NOT NULL DEFAULT '0' COMMENT '销量',
  `img` varchar(256) DEFAULT NULL COMMENT '商品图片',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_name` (`name`),
  KEY `category` (`category`),
  CONSTRAINT `t_product_ibfk_1` FOREIGN KEY (`category`) REFERENCES `t_category` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='商品';

/*Table structure for table `t_product_company` */

DROP TABLE IF EXISTS `t_product_company`;

CREATE TABLE `t_product_company` (
  `productid` int NOT NULL COMMENT '商品id',
  `companyid` int NOT NULL COMMENT '公司id',
  `start_date` date NOT NULL COMMENT '起始日期',
  `end_date` date NOT NULL COMMENT '结束日期',
  PRIMARY KEY (`productid`,`companyid`,`start_date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='商品定向公司展示';

/*Table structure for table `t_user` */

DROP TABLE IF EXISTS `t_user`;

CREATE TABLE `t_user` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '用户id',
  `code` varchar(64) NOT NULL COMMENT '唯一编码',
  `companyid` int NOT NULL DEFAULT '0' COMMENT '公司id',
  `nick` varchar(32) NOT NULL COMMENT '昵称',
  `headimg` varchar(256) NOT NULL COMMENT '头像图片',
  `phone` varchar(32) NOT NULL COMMENT '手机号',
  `gender` int NOT NULL DEFAULT '0' COMMENT '性别 0女 1男',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态 0正常 1禁用',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_code` (`code`),
  KEY `fk_company_idx` (`companyid`),
  CONSTRAINT `fk_company` FOREIGN KEY (`companyid`) REFERENCES `t_company` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户';

/*Table structure for table `t_user_coupon` */

DROP TABLE IF EXISTS `t_user_coupon`;

CREATE TABLE `t_user_coupon` (
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `couponid` int NOT NULL DEFAULT '0' COMMENT '优惠卷id',
  `count` int NOT NULL DEFAULT '0' COMMENT '数量',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`userid`,`couponid`),
  KEY `fk_coupon_idx` (`couponid`),
  CONSTRAINT `fk_coupon` FOREIGN KEY (`couponid`) REFERENCES `t_coupon` (`id`),
  CONSTRAINT `fk_user` FOREIGN KEY (`userid`) REFERENCES `t_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户的优惠卷';

/*Table structure for table `t_wx_order_callback` */

DROP TABLE IF EXISTS `t_wx_order_callback`;

CREATE TABLE `t_wx_order_callback` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '自增ID',
  `transaction_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '微信支付订单号',
  `out_trade_no` varchar(128) DEFAULT NULL COMMENT '商户订单号',
  `appid` varchar(64) DEFAULT NULL COMMENT '应用ID',
  `mchid` varchar(64) DEFAULT NULL COMMENT '商户号',
  `trade_type` varchar(64) DEFAULT NULL COMMENT '交易类型',
  `trade_state` varchar(64) DEFAULT NULL COMMENT '交易状态',
  `trade_state_desc` varchar(256) DEFAULT NULL COMMENT '交易状态描述',
  `bank_type` varchar(64) DEFAULT NULL COMMENT '付款银行',
  `attach` varchar(256) DEFAULT NULL COMMENT '附加数据，在查询API和支付通知中原样返可作为自定义参数使用',
  `success_time` datetime DEFAULT NULL COMMENT '支付完成时间',
  `openid` varchar(64) DEFAULT NULL COMMENT '支付者标识',
  `total` int DEFAULT '0' COMMENT '总金额',
  `payer_total` int DEFAULT '0' COMMENT '用户支付金额',
  `currency` varchar(64) DEFAULT NULL COMMENT '货币类型',
  `payer_currency` varchar(64) DEFAULT NULL COMMENT '用户支付币种',
  `device_id` varchar(64) DEFAULT NULL COMMENT '商户端设备号',
  PRIMARY KEY (`id`),
  KEY `PK_out_trade_no` (`out_trade_no`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='微信支付回调数据';

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
