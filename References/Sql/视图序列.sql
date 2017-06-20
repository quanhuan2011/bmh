
1、物料管理： v_bee_materialinfo 
	create or replace view v_bee_materialinfo as
	/*
		物料管理页面数据视图
	*/
	select a.materialid,
       a.aduserid,
       a.name mname,
       a.imageurl,
       a.linkurl,
       a.title,
       a.width || '*' || a.height sizepic,
       to_char(a.createtime,'yyyy-MM-dd') createtime,
       a.statustime,
       a.materialtype,
       adt.adtypename materialtypename,
       a.ismark,
       u.name username
    from bee_materialinfo a
    left join bee_aduserinfo u on a.operationid = u.aduserid
    left join bee_adinfotype adt on adt.adtypeid=a.materialtype
    where a.status=1 and u.status=1 and adt.status=1

2、物料表序列： SEQ_BEE_MATERIALINFO_ID
	CREATE SEQUENCE SEQ_BEE_MATERIALINFO_ID --序列名
	INCREMENT BY 1 -- 每次加几个 
	START WITH 1 -- 从1开始计数 
	NOMAXVALUE -- 不设置最大值 
	NOCYCLE -- 一直累加，不循环 
	CACHE 10
	
3、CREATETIME、STATUSTIME：默认值 sysdate     STATUS：默认值 1

4、物料历史表：bee_materialinfohis

