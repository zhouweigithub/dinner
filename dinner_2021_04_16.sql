/*
SQLyog Community v13.1.6 (64 bit)
MySQL - 8.0.23 : Database - dinner
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dinner` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

/*Table structure for table `t_category` */

DROP TABLE IF EXISTS `t_category`;

CREATE TABLE `t_category` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '自增id',
  `name` varchar(20) DEFAULT NULL COMMENT '名称',
  `state` int DEFAULT '0' COMMENT '状态0正常 1禁用',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='商品分类';

/*Table structure for table `t_comment` */

DROP TABLE IF EXISTS `t_comment`;

CREATE TABLE `t_comment` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '自增id',
  `orderid` varchar(30) DEFAULT NULL COMMENT '订单编号',
  `msg` varchar(200) DEFAULT NULL COMMENT '内容',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='评论';

/*Table structure for table `t_company` */

DROP TABLE IF EXISTS `t_company`;

CREATE TABLE `t_company` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `name` varchar(50) DEFAULT NULL COMMENT '公司名字',
  `code` varchar(50) DEFAULT NULL COMMENT '唯一编码',
  `address` varchar(100) DEFAULT NULL COMMENT '公司地址',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `IX_CODE` (`code`),
  UNIQUE KEY `IX_NAME` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='公司';

/*Table structure for table `t_coupon` */

DROP TABLE IF EXISTS `t_coupon`;

CREATE TABLE `t_coupon` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(100) NOT NULL COMMENT '优惠卷名称',
  `money` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '优惠金额',
  `start_time` datetime NOT NULL COMMENT '使用开始日期',
  `end_time` datetime NOT NULL COMMENT '使用截止日期',
  `crtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '优惠卷创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='优惠卷信息';

/*Table structure for table `t_feedback` */

DROP TABLE IF EXISTS `t_feedback`;

CREATE TABLE `t_feedback` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '自增id',
  `userid` int NOT NULL COMMENT '用户id',
  `msg` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '内容',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户反馈';

/*Table structure for table `t_order` */

DROP TABLE IF EXISTS `t_order`;

CREATE TABLE `t_order` (
  `id` varchar(30) NOT NULL COMMENT '订单编号',
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `money` decimal(20,2) NOT NULL DEFAULT '0.00' COMMENT '订单金额',
  `coupon_money` decimal(20,2) NOT NULL DEFAULT '0.00' COMMENT '优惠金额',
  `pay_money` decimal(20,2) NOT NULL DEFAULT '0.00' COMMENT '实际支付金额',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单信息';

/*Table structure for table `t_order_callback` */

DROP TABLE IF EXISTS `t_order_callback`;

CREATE TABLE `t_order_callback` (
  `orderid` varchar(30) NOT NULL COMMENT '订单编号',
  `wx_orderid` varchar(30) DEFAULT NULL COMMENT '微信支付订单号',
  `state` int DEFAULT '0' COMMENT '状态',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`orderid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='微信支付回调';

/*Table structure for table `t_order_coupon` */

DROP TABLE IF EXISTS `t_order_coupon`;

CREATE TABLE `t_order_coupon` (
  `orderid` varchar(30) NOT NULL COMMENT '订单编号',
  `couponid` int NOT NULL DEFAULT '0' COMMENT '优惠卷id',
  `count` int DEFAULT '0' COMMENT '数量',
  `money` decimal(10,2) DEFAULT '0.00' COMMENT '总金额',
  PRIMARY KEY (`orderid`,`couponid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中使用的优惠卷';

/*Table structure for table `t_order_product` */

DROP TABLE IF EXISTS `t_order_product`;

CREATE TABLE `t_order_product` (
  `orderid` varchar(30) NOT NULL COMMENT '订单编号',
  `productid` int NOT NULL DEFAULT '0' COMMENT '商品id',
  `price` decimal(10,2) DEFAULT '0.00' COMMENT '单价',
  `count` int DEFAULT '0' COMMENT '数量',
  `money` decimal(10,2) DEFAULT '0.00' COMMENT '金额',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`orderid`,`productid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中的商品信息';

/*Table structure for table `t_product` */

DROP TABLE IF EXISTS `t_product`;

CREATE TABLE `t_product` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `name` varchar(100) DEFAULT NULL COMMENT '商品名称',
  `category` int DEFAULT '0' COMMENT '商品分类',
  `price` decimal(10,2) DEFAULT '0.00' COMMENT '价格',
  `sales` int DEFAULT '0' COMMENT '销量',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  KEY `category` (`category`),
  CONSTRAINT `t_product_ibfk_1` FOREIGN KEY (`category`) REFERENCES `t_category` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='商品';

/*Table structure for table `t_user` */

DROP TABLE IF EXISTS `t_user`;

CREATE TABLE `t_user` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '自增ID',
  `code` varchar(50) NOT NULL COMMENT '唯一编码',
  `companyid` int DEFAULT '0' COMMENT '公司id',
  `nick` varchar(20) DEFAULT NULL COMMENT '昵称',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  KEY `companyid` (`companyid`),
  CONSTRAINT `t_user_ibfk_1` FOREIGN KEY (`companyid`) REFERENCES `t_company` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户';

/*Table structure for table `t_user_coupon` */

DROP TABLE IF EXISTS `t_user_coupon`;

CREATE TABLE `t_user_coupon` (
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `couponid` int NOT NULL DEFAULT '0' COMMENT '优惠卷id',
  `count` int DEFAULT '0' COMMENT '数量',
  PRIMARY KEY (`userid`,`couponid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户的优惠卷';

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
