-- MySQL dump 10.13  Distrib 8.0.24, for Win64 (x86_64)
--
-- Host: localhost    Database: dinner
-- ------------------------------------------------------
-- Server version	8.0.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `t_cart`
--

DROP TABLE IF EXISTS `t_cart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_cart` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `productid` int NOT NULL DEFAULT '0' COMMENT '商品id',
  `count` int NOT NULL DEFAULT '0' COMMENT '商品数量',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='购物车';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_category`
--

DROP TABLE IF EXISTS `t_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_category` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '商品分类id',
  `name` varchar(32) NOT NULL COMMENT '商品分类名称',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态0正常 1禁用',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='商品分类';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_comment`
--

DROP TABLE IF EXISTS `t_comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_comment` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `msg` varchar(256) NOT NULL COMMENT '内容',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='评论';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_company`
--

DROP TABLE IF EXISTS `t_company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_company` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '公司id',
  `name` varchar(64) DEFAULT NULL COMMENT '公司名字',
  `code` varchar(64) DEFAULT NULL COMMENT '唯一编码',
  `address` varchar(128) DEFAULT NULL COMMENT '公司地址',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `IX_CODE` (`code`),
  UNIQUE KEY `IX_NAME` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='公司';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_coupon`
--

DROP TABLE IF EXISTS `t_coupon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_coupon` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '优惠券id',
  `name` varchar(128) NOT NULL COMMENT '优惠卷名称',
  `money` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '优惠金额',
  `start_time` datetime NOT NULL COMMENT '使用开始日期',
  `end_time` datetime NOT NULL COMMENT '使用截止日期',
  `crtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '优惠卷创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='优惠卷信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_feedback`
--

DROP TABLE IF EXISTS `t_feedback`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_feedback` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `userid` int NOT NULL COMMENT '用户id',
  `msg` varchar(332) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '内容',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户反馈';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_message`
--

DROP TABLE IF EXISTS `t_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_message` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `message` varchar(640) NOT NULL COMMENT '消息内容',
  `isread` int NOT NULL DEFAULT '0' COMMENT '是否已读(0未读 1已读)',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='消息中心';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_order`
--

DROP TABLE IF EXISTS `t_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_order` (
  `id` varchar(32) NOT NULL COMMENT '订单编号',
  `userid` int NOT NULL DEFAULT '0' COMMENT '用户id',
  `money` decimal(32,2) NOT NULL DEFAULT '0.00' COMMENT '订单金额',
  `coupon_money` decimal(32,2) NOT NULL DEFAULT '0.00' COMMENT '优惠金额',
  `pay_money` decimal(32,2) NOT NULL DEFAULT '0.00' COMMENT '实际支付金额',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_order_callback`
--

DROP TABLE IF EXISTS `t_order_callback`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_order_callback` (
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `wx_orderid` varchar(32) DEFAULT NULL COMMENT '微信支付订单号',
  `state` int NOT NULL DEFAULT '0' COMMENT '状态',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`orderid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='微信支付回调';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_order_coupon`
--

DROP TABLE IF EXISTS `t_order_coupon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_order_coupon` (
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `couponid` int NOT NULL DEFAULT '0' COMMENT '优惠卷id',
  `count` int NOT NULL DEFAULT '0' COMMENT '数量',
  `money` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '总金额',
  PRIMARY KEY (`orderid`,`couponid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中使用的优惠卷';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_order_product`
--

DROP TABLE IF EXISTS `t_order_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_order_product` (
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `productid` int NOT NULL DEFAULT '0' COMMENT '商品id',
  `price` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '单价',
  `count` int NOT NULL DEFAULT '0' COMMENT '数量',
  `money` decimal(16,2) NOT NULL DEFAULT '0.00' COMMENT '金额',
  `crtime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`orderid`,`productid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='订单中的商品信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_pay`
--

DROP TABLE IF EXISTS `t_pay`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_pay` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `orderid` varchar(32) NOT NULL COMMENT '订单编号',
  `wx_orderid` varchar(64) NOT NULL COMMENT '微信订单号',
  `status` int NOT NULL COMMENT '状态',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='支付信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_product`
--

DROP TABLE IF EXISTS `t_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='商品';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_user`
--

DROP TABLE IF EXISTS `t_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `t_user` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '用户id',
  `code` varchar(64) NOT NULL COMMENT '唯一编码',
  `companyid` int NOT NULL DEFAULT '0' COMMENT '公司id',
  `nick` varchar(32) NOT NULL COMMENT '昵称',
  `headimg` varchar(256) NOT NULL COMMENT '头像图片',
  `phone` varchar(32) NOT NULL COMMENT '手机号',
  `crtime` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_code` (`code`),
  KEY `fk_company_idx` (`companyid`),
  CONSTRAINT `fk_company` FOREIGN KEY (`companyid`) REFERENCES `t_company` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_user_coupon`
--

DROP TABLE IF EXISTS `t_user_coupon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-05-29 17:24:25
