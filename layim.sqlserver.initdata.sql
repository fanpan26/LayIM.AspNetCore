truncate table layim_big_group
truncate table layim_chat_record
truncate table layim_friend_group
truncate table layim_friend_relation
truncate table layim_group_member
truncate table layim_user

--user
insert into layim_user (id,name,avatar,[sign]) values (10001,'小盘子','/images/photo/xiaopanzi.jpg','LayIM.AspNetCore开发中')
insert into layim_user (id,name,avatar,[sign]) values (10002,'华妃','/images/photo/huafei.jpg','贱人就是侨情')

insert into layim_user (id,name,avatar,[sign]) values (10003,'皇后','/images/photo/huanghou.jpg','臣妾做不到啊！！')
insert into layim_user (id,name,avatar,[sign]) values (10004,'皇上','/images/photo/huangshang.jpg','前朝政事繁忙，后宫也不得安宁')

insert into layim_user (id,name,avatar,[sign]) values (10005,'媚姐姐','/images/photo/meijiejie.jpg','皇上，臣妾冤枉啊，没有假孕争宠')
insert into layim_user (id,name,avatar,[sign]) values (10006,'安陵容','/images/photo/xiaoniao.jpg','比我出身好的人都得死')

insert into layim_user (id,name,avatar,[sign]) values (10007,'公公','/images/photo/gonggong.jpg','唉，奴才得命啊')
insert into layim_user (id,name,avatar,[sign]) values (10008,'甄嬛','/images/photo/zhenhuan.jpg','呵呵')

--好友分组
insert into layim_friend_group (id,name,create_by) values (1000101,'我的好友',10001)
insert into layim_friend_group (id,name,create_by) values (1000102,'高中同学',10001)
insert into layim_friend_group (id,name,create_by) values (1000103,'大学同学',10001)
insert into layim_friend_group (id,name,create_by) values (1000104,'测试分组',10001)
insert into layim_friend_group (id,name,create_by) values (1000105,'测试分组',10001)
insert into layim_friend_group (id,name,create_by) values (1000106,'测试分组',10001)
insert into layim_friend_group (id,name,create_by) values (1000107,'测试分组',10001)
insert into layim_friend_group (id,name,create_by) values (1000108,'测试分组',10001)
insert into layim_friend_group (id,name,create_by) values (1000109,'测试分组',10001)
insert into layim_friend_group (id,name,create_by) values (1000110,'测试分组',10001)
insert into layim_friend_group (id,name,create_by) values (1000111,'测试分组',10001)

insert into layim_friend_group (id,name,create_by) values (1000201,'我的好友',10002)
insert into layim_friend_group (id,name,create_by) values (1000202,'后宫敌人',10002)

--好友关系
insert into layim_friend_relation (uid1,uid2,friend_group_1,friend_group_2) values (10001,10002,1000101,1000201)
insert into layim_friend_relation (uid1,uid2,friend_group_1,friend_group_2) values (10001,10003,1000101,0)
insert into layim_friend_relation (uid1,uid2,friend_group_1,friend_group_2) values (10001,10004,1000101,0)
insert into layim_friend_relation (uid1,uid2,friend_group_1,friend_group_2) values (10001,10005,1000101,0)
insert into layim_friend_relation (uid1,uid2,friend_group_1,friend_group_2) values (10001,10006,1000101,0)
insert into layim_friend_relation (uid1,uid2,friend_group_1,friend_group_2) values (10001,10007,1000101,0)
insert into layim_friend_relation (uid1,uid2,friend_group_1,friend_group_2) values (10001,10008,1000101,0)

--群组

insert into layim_big_group values ('皇族大院','/images/photo/huangzu.jpg',10001)

insert into layim_group_member values (1,10001,0)
insert into layim_group_member values (1,10002,0)
insert into layim_group_member values (1,10003,0)
insert into layim_group_member values (1,10004,0)
insert into layim_group_member values (1,10005,0)
insert into layim_group_member values (1,10006,0)
insert into layim_group_member values (1,10007,0)
insert into layim_group_member values (1,10008,0)
