
1�����Ϲ��� v_bee_materialinfo 
	create or replace view v_bee_materialinfo as
	/*
		���Ϲ���ҳ��������ͼ
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

2�����ϱ����У� SEQ_BEE_MATERIALINFO_ID
	CREATE SEQUENCE SEQ_BEE_MATERIALINFO_ID --������
	INCREMENT BY 1 -- ÿ�μӼ��� 
	START WITH 1 -- ��1��ʼ���� 
	NOMAXVALUE -- ���������ֵ 
	NOCYCLE -- һֱ�ۼӣ���ѭ�� 
	CACHE 10
	
3��CREATETIME��STATUSTIME��Ĭ��ֵ sysdate     STATUS��Ĭ��ֵ 1

4��������ʷ��bee_materialinfohis

