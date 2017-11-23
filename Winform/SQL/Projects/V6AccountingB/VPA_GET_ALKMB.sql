USE [VIETHA16_CUST]
GO
/****** Object:  StoredProcedure [dbo].[VPA_Get_ALKMB]    Script Date: 11/23/2017 09:10:10 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
--V6 CORP
-- NEW   VPA_Get_ALKMB
-- Get promotion and  discount 
-- 21/11/2017
/*
exec VPA_Get_ALKMB @cStt_rec=N'N000981708SOH',@cMode=N'S',@cMa_ct=N'SOH',@dngay_ct=N'20171121',@cMa_kh=N'1...TTNT',@cMa_dvcs=N'SG',
@cMa_nt=N'VND',@nT_so_luong=120.000,@nTso_luong1=120.000,@nT_tien_nt2=750000.00,@nT_tien2=750000.00,
@Advance=N'1=1',@User_id=53,@lad01=N'00001;00002',@lad02=N'A123;A124',@lad03=N'20.000;100.000',
@lad04=N'25000.0000;2500.0000',@lad05=N'500000.00;250000.00',@lad06=N'HOP;HOP',@lad07=N'25000.0000;2500.0000',
@lad08=N'500000.00;250000.00',@lad09=N'25000.0000;2500.0000',@lad10=N'0TP;0TP',@Advance2=N'1=1'
*/

ALTER PROCEDURE [dbo].[VPA_Get_ALKMB]
	@cStt_rec NVARCHAR(13),
	@cMode VARCHAR(1),
	@cMa_ct VARCHAR(3), 
	@dngay_ct SMALLDATETIME, 
	@cMa_kh NVARCHAR(50), 
	@cMa_dvcs NVARCHAR(50),  
	@cMa_nt VARCHAR(3), 
	@nT_so_luong numeric(20,6),
	@nTso_luong1 numeric(20,6),
	@nT_tien_nt2 numeric(16,2),
	@nT_tien2 numeric(16,2),
	@Advance NVARCHAR(MAX),
	@User_id int=0,
	@lad01 NVARCHAR(MAX),--stt_rec0
	@lad02 NVARCHAR(MAX),--ma_vt
	@lad03 NVARCHAR(MAX),--so_luong1
	@lad04 NVARCHAR(MAX),--gia_nt21
	@lad05 NVARCHAR(MAX),--tien_nt2
	@lad06 NVARCHAR(MAX),--dvt1
	@lad07 NVARCHAR(MAX),--gia_nt2
	@lad08 NVARCHAR(MAX),--tien2
	@lad09 NVARCHAR(MAX),--gia2
	@lad10 NVARCHAR(MAX),--ma_kho_i
	@Advance2 NVARCHAR(MAX)
	
AS
	BEGIN
		
		IF ISNULL(@Advance,'')=''
			SET @Advance='1=1'
		
		IF ISNULL(@Advance2,'')=''
			SET @Advance2='1=1'
		
		DECLARE @strsql NVARCHAR(MAX) 
		DECLARE @LNH_KH1 NVARCHAR(MAX) 
		DECLARE @LNH_KH2 NVARCHAR(MAX) 
		DECLARE @LNH_KH3 NVARCHAR(MAX) 
		DECLARE @LNH_KH4 NVARCHAR(MAX) 
		DECLARE @LNH_KH5 NVARCHAR(MAX) 
		DECLARE @LNH_KH6 NVARCHAR(MAX) 
		DECLARE @LNH_KH9 NVARCHAR(MAX) 
		
		SELECT @LNH_KH1=NH_KH1,@LNH_KH2=NH_KH2,@LNH_KH3=NH_KH3,@LNH_KH4=NH_KH4,@LNH_KH5=NH_KH5,@LNH_KH6=NH_KH9,@LNH_KH6=NH_KH9  
			 FROM ALKH WHERE ma_kh =LTRIM(rtrim(@cMa_kh))
		IF ISNULL(@LNH_KH1,'')=''
			SET @LNH_KH1=''
		IF ISNULL(@LNH_KH2,'')=''
			SET @LNH_KH2=''
		IF ISNULL(@LNH_KH3,'')=''
			SET @LNH_KH3=''
		IF ISNULL(@LNH_KH4,'')=''
			SET @LNH_KH4=''
		IF ISNULL(@LNH_KH5,'')=''
			SET @LNH_KH5=''
		IF ISNULL(@LNH_KH6,'')=''
			SET @LNH_KH6=''					
		IF ISNULL(@LNH_KH9,'')=''
			SET @LNH_KH9=''		
			
		SELECT * into #alkmb1 FROM ALKMB WHERE ngay_hl<=@dngay_ct and (ngay_hl2 is null or ngay_hl2 >@dngay_ct)
				AND (ISNULL(LNH_KH1,'')='' OR (ISNULL(LNH_KH1,'')<>'' AND dbo.VFV_InList0(@LNH_KH1,ISNULL(LNH_KH1,''),',')=1))
				AND (ISNULL(LNH_KH2,'')='' OR (ISNULL(LNH_KH2,'')<>'' AND dbo.VFV_InList0(@LNH_KH2,ISNULL(LNH_KH2,''),',')=1))
				AND (ISNULL(LNH_KH3,'')='' OR (ISNULL(LNH_KH3,'')<>'' AND dbo.VFV_InList0(@LNH_KH3,ISNULL(LNH_KH3,''),',')=1))
				AND (ISNULL(LNH_KH4,'')='' OR (ISNULL(LNH_KH4,'')<>'' AND dbo.VFV_InList0(@LNH_KH4,ISNULL(LNH_KH4,''),',')=1))
				AND (ISNULL(LNH_KH5,'')='' OR (ISNULL(LNH_KH5,'')<>'' AND dbo.VFV_InList0(@LNH_KH5,ISNULL(LNH_KH5,''),',')=1))
				AND (ISNULL(LNH_KH6,'')='' OR (ISNULL(LNH_KH6,'')<>'' AND dbo.VFV_InList0(@LNH_KH6,ISNULL(LNH_KH6,''),',')=1))
				AND (ISNULL(LNH_KH9,'')='' OR (ISNULL(LNH_KH9,'')<>'' AND dbo.VFV_InList0(@LNH_KH9,ISNULL(LNH_KH9,''),',')=1))
				
		SELECT * INTO #alkmbct1 FROM 	ALKMBCT WHERE MA_KM IN (SELECT MA_KM FROM #alkmb1)	
		
		
				
		SELECT ma_km,Ma_km0,Ma_kmm,ma_vt,sl_km as so_luong1,sl_km as so_luong,tien_km as gg,tien_km as gg_nt INTO #ctkm1 FROM ALKMBCT WHERE 1=0 
		SELECT ma_km,Ma_km0,Ma_kmm,stt_rec0,ma_vt,sl_km as so_luong1,sl_km as so_luong,tien_km as ck,tien_km as ck_nt,pt_ck						
				INTO #ctck1 FROM ALKMBCT WHERE 1=0 
		
		
		--{Add empty data 04/05/2016
	DECLARE @i int
	DECLARE @nCount1 NUMERIC(16,0)
	SELECT @nCount1=COUNT(*) FROM #alkmb1
	
	
	IF 	@nCount1=0
		BEGIN
			SELECT * FROM #ctkm1			
			SELECT * FROM #ctck1			
						
			SELECT a.ma_km,count(a.ma_km) as cong_ctkm,MAX(b.ghi_chu) AS ghi_chu,MAX(b.ngay_hl) AS ngay_hl,MAX(b.ngay_hl2) AS  ngay_hl2
			 FROM #ctkm1 a 
				LEFT JOIN ALKMB b on a.ma_km=b.ma_km
			group by 	a.ma_km			
				
			SELECT a.ma_km,count(a.ma_km) as cong_ctck,MAX(b.ghi_chu) AS ghi_chu,MAX(b.ngay_hl) AS ngay_hl,MAX(b.ngay_hl2) AS ngay_hl2 
			FROM #ctck1 a 
				LEFT JOIN ALKMB b on a.ma_km=b.ma_km
				group by 	a.ma_km	
				
		END
	ELSE
		BEGIN
				----0.Cau truc AD
				SELECT STT_REC,STT_REC0,MA_VT,SO_LUONG1,GIA_NT21,TIEN_NT2,DVT1,GIA_NT2,TIEN2,GIA2,MA_KHO_I INTO #DataAD FROM AD81 WHERE 1=0
				--- 1. Chuyen ad->data sql (#DataAD)
				SELECT @nCount1=DBO.[VFV_GetWordCount0](@lad01,';')
				set @i=1
				WHILE @i<=@nCount1
					BEGIN
						
						    SET @strsql='INSERT INTO #DataAD (STT_REC,STT_REC0,MA_VT,SO_LUONG1,GIA_NT21,TIEN_NT2,DVT1,GIA_NT2,TIEN2,GIA2,MA_KHO_I) VALUES ('							
							
							SET @strsql=@strsql+CHAR(39)+@cStt_rec+CHAR(39)
							SET @strsql=@strsql+','+CHAR(39)+DBO.VFV_sGetWordNum0(@lad01,@i,';')+CHAR(39)
							SET @strsql=@strsql+','+CHAR(39)+DBO.VFV_sGetWordNum0(@lad02,@i,';')+CHAR(39)
							
							SET @strsql=@strsql+','+DBO.VFV_sGetWordNum0(@lad03,@i,';')
							SET @strsql=@strsql+','+DBO.VFV_sGetWordNum0(@lad04,@i,';')
							SET @strsql=@strsql+','+DBO.VFV_sGetWordNum0(@lad05,@i,';')
							
							SET @strsql=@strsql+','+CHAR(39)+DBO.VFV_sGetWordNum0(@lad06,@i,';')+CHAR(39)
							
							SET @strsql=@strsql+','+DBO.VFV_sGetWordNum0(@lad07,@i,';')
							SET @strsql=@strsql+','+DBO.VFV_sGetWordNum0(@lad08,@i,';')
							SET @strsql=@strsql+','+DBO.VFV_sGetWordNum0(@lad09,@i,';')
							
							SET @strsql=@strsql+','+CHAR(39)+DBO.VFV_sGetWordNum0(@lad10,@i,';')+CHAR(39)
							
							
							set @strsql=@strsql+')'	
										
							Exec sp_executesql @strsql
							
						SET @i=@i+1
					END	
				
				select * from #DataAD				
				--- 2. Tinh dua tren  #DataAD va (#alkmb1 va #alkmbct1)
				---print 'Tinh khuyen mai'
				
			print 'Tinh chiet khau'

			Declare @Kieu_ck as varchar(10)
			if @Kieu_ck = 'CTL01' -- Tính tổng các mã hàng có trong chi tiết.
			Begin--
				Print 'abc'
			End
			
			SELECT * FROM #ctkm1			
			SELECT * FROM #ctck1
						
			SELECT a.ma_km,count(a.ma_km) as cong_ctkm,MAX(b.ghi_chu) AS ghi_chu,MAX(b.ngay_hl) AS ngay_hl,MAX(b.ngay_hl2) AS ngay_hl2
			 FROM #ctkm1 a 
				LEFT JOIN ALKMB b on a.ma_km=b.ma_km
			group by 	a.ma_km			
				
			SELECT a.ma_km,count(a.ma_km) as cong_ctck,MAX(b.ghi_chu) AS ghi_chu,MAX(b.ngay_hl) AS ngay_hl,MAX(b.ngay_hl2) AS ngay_hl2
			 FROM #ctck1 a 
				LEFT JOIN ALKMB b on a.ma_km=b.ma_km
				group by 	a.ma_km	
				
		END	
		
	
				

		
	END

