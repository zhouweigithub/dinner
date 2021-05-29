SET FOREIGN_KEY_CHECKS = 0;  
 
truncate table t_cart;
truncate table t_comment;
truncate table t_feedback;
truncate table t_message;
truncate table t_order_callback;
truncate table t_order_coupon;
truncate table t_order_product;
truncate table t_pay;
truncate table t_user_coupon;
truncate table t_order;
truncate table t_coupon;
truncate table t_product;
truncate table t_user;
truncate table t_category;
truncate table t_company;

SET FOREIGN_KEY_CHECKS = 1;  
 
INSERT INTO `dinner`.`t_category`(`name`,`state`,`crtime`)VALUES ('套餐', 0, '2021-05-5');  
INSERT INTO `dinner`.`t_category`(`name`,`state`,`crtime`)VALUES ('炒菜', 0, '2021-05-5');  
INSERT INTO `dinner`.`t_product` ( `name`, `category`, `price`, `sales`, `crtime`) VALUES ('蛋炒饭', 1, 10, 34, '2021-02-15'); 
INSERT INTO `dinner`.`t_product` ( `name`, `category`, `price`, `sales`, `crtime`) VALUES ('肉炒饭', 1, 12, 2, '2021-02-10'); 
INSERT INTO `dinner`.`t_company` ( `name`, `code`, `address`, `crtime`) VALUES ('北京中关村大酒店', '45184254', '中关村1号', '2021-06-04'); 
INSERT INTO `dinner`.`t_company` ( `name`, `code`, `address`, `crtime`) VALUES ('广州大饭店', '55124152', '广州市越秀区1号', '2021-04-12'); 
INSERT INTO `dinner`.`t_user` ( `code`, `companyid`, `nick`, `headimg`, `phone`, `crtime`) VALUES ('op1', 1, '小明', 'img1', '15499564875', '2021-05-6');
INSERT INTO `dinner`.`t_user` ( `code`, `companyid`, `nick`, `headimg`, `phone`, `crtime`) VALUES ('op2', 1, '小红', 'img2', '13255487541', '2021-02-2');