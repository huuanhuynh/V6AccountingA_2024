if exists (select 1 from sysobjects  where name = 'VPA_SOA_POST_ARA00')
	drop proc VPA_SOA_POST_ARA00
GO
print 'CREATE PROCEDURE [dbo].[VPA_SOA_POST_ARA00]'
GO
CREATE PROCEDURE [dbo].[VPA_SOA_POST_ARA00]
	@Stt_rec varchar(13),
	@Ma_ct varchar(10),
	@Ma_nt varchar(10),
	@Ma_nx varchar(16),	
	@Loai_ck char(1),	
	@UserID int
AS
BEGIN
	--================== Khai bao mot so bien su dung chung ============================================
	DECLARE @sql varchar(4000), @M_Ma_nt0 varchar(10), @Nxt tinyint
	Declare @ColumnName varchar(50), @DataType nvarchar(50), @nullable varchar(3), @DefaultValue varchar(10)
	SET @sql = '' SET @Nxt = 2 --Xuat
	--SET cMa_nt_0
	Select @M_Ma_nt0 = val from V6option where name = 'M_Ma_Nt0'
	--Select val from V6option where name = 'M_Ma_Nt0'
	
	Declare
		@t_ck numeric(15,2),
		@t_ck_nt numeric(15,2),
		@tk_ck char(16),
		@t_thue numeric(15,2),
		@t_thue_nt numeric(15,2),
		@tk_thue_no char(16)
	Select
		@t_ck = t_ck,
		@t_ck_nt = t_ck_nt,
		@tk_ck = tk_ck,
		@t_thue = t_thue,
		@t_thue_nt = t_thue_nt,
		@tk_thue_no = tk_thue_no
	from AM81 where stt_rec = @Stt_rec
	--==================================================================================================
		
		-- Xóa hết dữ liệu cũ
		DELETE FROM ARA00 WHERE Stt_rec = @Stt_rec
		
		-- Select group gì đó từ AM, AD rồi insert vào ARA00.
	
		Declare @_Dk int, @_Dk1 int, @_Dk2 int, @m_Tk_tk_vt varchar(100)
		Set @_Dk = 0 Set @_Dk = 0 Set @_Dk2 = 0
		
		Select @m_Tk_tk_vt = val from V6option where name = 'm_Tk_tk_vt'
		
		If RTRIM(ltrim(@m_Tk_tk_vt)) = '' Set @_Dk1 = 0
		Else Set @_Dk1 = CHARINDEX(',' + @m_Tk_tk_vt + ',', ',' + @Ma_nx+',',1)
		 --  = ''' + STRTRAN(m_Tk_tk_vt, ',', ''' OR m.Ma_nx = ''') + ''')'
		
		--*Neu ma_nx bang mot trong cac tk_vt thi khong post
		IF Exists(select 1 from AD81 where stt_rec = @Stt_rec and tk_vt = @Ma_nx)
			Set @_Dk2 = 1
		Else Set @_Dk2 = 0
		
		If @_Dk1>0 or @_Dk2>0
			Set @_Dk = 1;
		
		Declare @_Nh_dk tinyint Set @_Nh_dk = 0 --???
		
		--======================== New ======================
		--======= Dùng bảng tạm đầy đủ các trường ========
		Select @_Nh_dk = COUNT(1)/2 from ARA00 where stt_rec = @Stt_rec
		Print '*Summary cac vat tu cung tai khoan - Tao bang tam ARA00_T_GROUP1'
		SELECT
			max(a.stt_rec)	[stt_rec]
			,max(Case When (k.ma_dvcs is not null) then k.ma_dvcs else '' end)	[ma_dvcs]	-------- Group 2
			,max(a.ma_ct)	[ma_ct]
			,max(a.ngay_ct)	[ngay_ct]
			,max(a.ngay_lct) [ngay_lct]
			,max(a.so_ct)	[so_ct]
			,max(a.so_lo)	[so_lo]
			,max(a.ngay_lo)	[ngay_lo]
			,max(a.ong_ba)	[ong_ba]
			,max(a.dien_giai) [dien_giaih]
			,max(a.dien_giai) [dien_giai]
			,@_Nh_dk	[nh_dk],
				b.ma_kho_i				--Group
			,b.tk_vt	[tk]			--Group
			,b.tk_gv	[tk_du]			--Group
			,0 as		[ps_no_nt]
			,0 as		[ps_co_nt]
			,max(a.ma_nt)	[ma_nt]
			,max(a.ty_gia) [ty_gia]
			,0 as [ps_no]
			,0 as [ps_co]
			,max(a.ma_kh) [ma_kh]
			,max(a.tk_cn)	[tk_cn]
			,b.ma_vv_i	[ma_vv]			--Group
			,max(a.ma_nk)	[ma_nk]
			,b.ma_td_i [ma_td]			--Group
			,	b.ma_ku					--Group
			,max(a.LOAI_CT0) as [loai_ct]
			,max(b.ma_sp)	[Ma_sp]
			,'' as [So_lsx]
			,	b.ma_hd					--Group
			,	b.Ma_phi				--Group
			,max(a.Ma_nvien)			[Ma_nvien]
			,max(b.Ma_bpht)			[Ma_bpht]
			,max(a.user_id0)		[user_id0]
			,max(a.date0)			[date0]
			,max(a.time0)			[time0]
			,max(a.user_id2)		[user_id2]
			,max(a.date2)			[date2]
			,max(a.time2)			[time2]
			,max(a.[status])		[status]
			,max(a.so_dh)			[so_dh]
			,max(a.so_ct)			[so_ct0]
			,max(a.ngay_ct) as		[ngay_ct0]
			,1 as [ct_nxt]		-----------------------------------------?????????????????????????
			,max(a.ma_gd)			[ma_gd]
			,max(b.ln)			[ln]
			,max(b.ma_td2) [ma_td2]
			,max(b.ma_td3) [ma_td3]
			,max(b.ngay_td1)			[ngay_td1]
			,max(b.sl_td1) [sl_td1]
			,max(b.sl_td2) [sl_td2]
			,max(b.sl_td3) [sl_td3]
			,max(b.gc_td1) [gc_td1]
			,max(b.gc_td2) [gc_td2]
			,max(b.gc_td3) [gc_td3]
			,max(b.ngay_td2) [ngay_td2]
			,max(b.ngay_td3) [ngay_td3]
			,max(a.ma_bp) [ma_bp]
			,'' as [LOAI_CTGS]
			,'' as [KIEU_CTGS]
			,'' as [SO_LO0]
			,SUM(b.tien_nt) as Tien_Nt11
			,SUM(b.tien) as Tien11
		INTO #ARA00_T_GROUP1
		FROM AM81 a join AD81 b on a.stt_rec = b.stt_rec
					left join ALvt v on b.ma_vt = v.ma_vt
					left join Alkho k on b.ma_kho_i = k.ma_kho
		WHERE a.stt_rec = @Stt_rec
			and RTRIM(b.tk_vt) <> ''
			and RTRIM (b.tk_gv) <> ''
		GROUP BY b.ma_kho_i, b.Tk_Vt, b.Tk_Gv, b.ma_vv_i, b.ma_td_i             ,ma_hd,ma_ku,ma_phi
		
		Print 'Tao bang tam ARA00_T_GROUP2'
		SELECT
			max(stt_rec)	[stt_rec]
			,ma_dvcs	[ma_dvcs]			--Group
			,max(ma_ct)	[ma_ct]
			,max(ngay_ct)	[ngay_ct]
			,max(ngay_lct) [ngay_lct]
			,max(so_ct)	[so_ct]
			,max(so_lo)	[so_lo]
			,max(ngay_lo)	[ngay_lo]
			,max(ong_ba)	[ong_ba]
			,max(dien_giai) [dien_giaih]
			,max(dien_giai) [dien_giai]
			,@_Nh_dk	[nh_dk]
			,max(ma_kho_i)	[ma_kho_i]
			,	[tk]			--Group
			,	[tk_du]			--Group
			,0 as		[ps_no_nt]
			,0 as		[ps_co_nt]
			,max(ma_nt)	[ma_nt]
			,max(ty_gia) [ty_gia]
			,0 as [ps_no]
			,0 as [ps_co]
			,max(ma_kh) [ma_kh]
			,max(tk_cn)	[tk_cn]
			,	[ma_vv]			--Group
			,max(ma_nk)	[ma_nk]
			,	[ma_td]			--Group
			,	ma_ku			--Group
			,max(LOAI_CT) as [loai_ct]
			,max(ma_sp)	[Ma_sp]
			,'' as [So_lsx]
			,	ma_hd
			,	Ma_phi			--Group
			,max(Ma_nvien)			[Ma_nvien]
			,max(Ma_bpht)			[Ma_bpht]
			,max(user_id0)			[user_id0]
			,max(date0)			[date0]
			,max(time0)			[time0]
			,max(user_id2)			[user_id2]
			,max(date2)			[date2]
			,max(time2)			[time2]
			,max([status])			[status]
			,max(so_dh)			[so_dh]
			,max(so_ct) as [so_ct0]
			,max(ngay_ct) as [ngay_ct0]
			,1 as [ct_nxt]
			,max(ma_gd)			[ma_gd]
			,max(ln)			[ln]
			,max(ma_td2) [ma_td2]
			,max(ma_td3) [ma_td3]
			,max(ngay_td1)			[ngay_td1]
			,max(sl_td1) [sl_td1]
			,max(sl_td2) [sl_td2]
			,max(sl_td3) [sl_td3]
			,max(gc_td1) [gc_td1]
			,max(gc_td2) [gc_td2]
			,max(gc_td3) [gc_td3]
			,max(ngay_td2) [ngay_td2]
			,max(ngay_td3) [ngay_td3]
			,max(ma_bp) [ma_bp]
			,'' as [LOAI_CTGS]
			,'' as [KIEU_CTGS]
			,'' as [SO_LO0]
			,SUM(Tien11) as TienARA
			,SUM(Tien_nt11) as Tien_nt1
		INTO #ARA00_T_GROUP2
		FROM #ARA00_T_GROUP1					
		GROUP BY ma_dvcs, Tk, Tk_du, ma_vv, ma_td,ma_hd,ma_ku,ma_phi
				
		Print '*Summary cac vat tu cung tai khoan ---Insert lần 1'
		Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
			Select
			[stt_rec],[ma_dvcs]	,[ma_ct],[ngay_ct],[ngay_lct],[so_ct],[so_lo],[ngay_lo]	,[ong_ba]
			,[dien_giaih]
			,[dien_giai]
			,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY ma_dvcs, Tk, Tk_du, ma_vv, ma_td ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
			,tk_du	as [tk]
			,tk		as [tk_du]
			,case when @Ma_nt = @M_Ma_nt0 then 0 else Tien_nt1 end as [ps_no_nt]
			,ps_no_nt as [ps_co_nt]
			,[ma_nt]
			,[ty_gia]
			,TienARA as [ps_no]
			,[ps_co]
			,[ma_kh] ,1 as [tk_cn],[ma_vv],[ma_nk],[ma_td],[ma_ku],[loai_ct],[Ma_sp],[So_lsx],[Ma_hd],[Ma_phi],[Ma_nvien],[Ma_bpht]
			,[user_id0],[date0],[time0],[user_id2],[date2],[time2],[status],[so_dh],[so_ct0],[ngay_ct0],[ct_nxt],[ma_gd],[ln]
			,[ma_td2],[ma_td3],[ngay_td1],[sl_td1],[sl_td2],[sl_td3],[gc_td1],[gc_td2],[gc_td3],[ngay_td2],[ngay_td3],[ma_bp]
			,[LOAI_CTGS]
			,[KIEU_CTGS]
			,[SO_LO0]
		From #ARA00_T_GROUP2
      
		Print '*Summary cac vat tu cung tai khoan ---Insert lần 2'
		Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
			Select
		[stt_rec]
      ,[ma_dvcs]
      ,[ma_ct]
      ,[ngay_ct]
      ,[ngay_lct]
      ,[so_ct]
      ,[so_lo]
      ,[ngay_lo]
      ,[ong_ba]
      ,[dien_giaih]
      ,[dien_giai]
      ,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY ma_dvcs, Tk, Tk_du, ma_vv, ma_td ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
      ,[tk]
      ,[tk_du]
      ,[ps_no_nt]
      ,case when @Ma_nt = @M_Ma_nt0 then 0 else Tien_nt1 end [ps_co_nt] ----------
      ,[ma_nt]
      ,[ty_gia]
      ,[ps_no]
      ,TienAra [ps_co] -----------------------------------------------------------
      ,[ma_kh]
      ,1 as [tk_cn]
      ,[ma_vv]
      ,[ma_nk]
      ,[ma_td]
      ,[ma_ku]
      ,[loai_ct]
      ,[Ma_sp]
      ,[So_lsx]
      ,[Ma_hd]
      ,[Ma_phi]
      ,[Ma_nvien]
      ,[Ma_bpht]
      ,[user_id0]
      ,[date0]
      ,[time0]
      ,[user_id2]
      ,[date2]
      ,[time2]
      ,[status]
      ,[so_dh]
      ,[so_ct0]
      ,[ngay_ct0]
      ,[ct_nxt]
      ,[ma_gd]
      ,[ln]
      ,[ma_td2]
      ,[ma_td3]
      ,[ngay_td1]
      ,[sl_td1]
      ,[sl_td2]
      ,[sl_td3]
      ,[gc_td1]
      ,[gc_td2]
      ,[gc_td3]
      ,[ngay_td2]
      ,[ngay_td3]
      ,[ma_bp]
      ,[LOAI_CTGS]
      ,[KIEU_CTGS]
      ,[SO_LO0]
		From #ARA00_T_GROUP2
		
		Drop table #ARA00_T_GROUP1, #ARA00_T_GROUP2	
		Print ' tam tam summary vat tu cung tai khoan '
            
		Print '&& Dinh khoan Tk Doanh thu
			*	Trong truong hop Left(M.Tk,3) $ M_Tk_Tk_Vt thi khong Post sang file ARA00'
		IF @_Dk <> 1
		BEGIN --ToanBo
			
			--*Summary cac vat tu cung tai khoan
			--*16/02/2009 Dac thu Viet ha (DOANH THU-CK)
			--_lcGroupBy = GetGroupKey("Ma_kho_i, Tk_Dt, Ma_vv_i, Ma_td_i")
			
			Select @_Nh_dk = COUNT(1)/2 from ARA00 where stt_rec = @Stt_rec
			Print 'Tao bang tam #ARA00_T_GROUP3'
			SELECT
				max(a.stt_rec)	[stt_rec]
				,max(Case When (k.ma_dvcs is not null) then k.ma_dvcs else '' end)	[ma_dvcs]
				,max(a.ma_ct)	[ma_ct]
				,max(a.ngay_ct)	[ngay_ct]
				,max(a.ngay_lct) [ngay_lct]
				,max(a.so_ct)	[so_ct]
				,max(a.so_lo)	[so_lo]
				,max(a.ngay_lo)	[ngay_lo]
				,max(a.ong_ba)	[ong_ba]
				,max(a.dien_giai) [dien_giaih]
				,max(a.dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk]
				,	b.ma_kho_i				--Group
				,	b.tk_dt					--Group
				,max(b.tk_dt)	[tk]
				,max(a.ma_nx)	[tk_du]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(a.ma_nt)	[ma_nt]
				,max(a.ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(a.ma_kh)	[ma_kh]
				,max(a.tk_cn)	[tk_cn]
				,b.ma_vv_i	[ma_vv]			--Group
				,max(a.ma_nk)	[ma_nk]
				,b.ma_td_i [ma_td]			--Group
				,	b.ma_ku					--Group
				,max(a.LOAI_CT0) as [loai_ct]
				,max(b.ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	b.ma_hd					--Group
				,	b.Ma_phi				--Group
				,max(a.Ma_nvien)			[Ma_nvien]
				,max(b.Ma_bpht)			[Ma_bpht]
				,max(a.user_id0)		[user_id0]
				,max(a.date0)			[date0]
				,max(a.time0)			[time0]
				,max(a.user_id2)		[user_id2]
				,max(a.date2)			[date2]
				,max(a.time2)			[time2]
				,max(a.[status])		[status]
				,max(a.so_dh)			[so_dh]
				,max(a.so_ct) as [so_ct0]
				,max(a.ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(a.ma_gd)			[ma_gd]
				,max(b.ln)			[ln]
				,max(b.ma_td2) [ma_td2]
				,max(b.ma_td3) [ma_td3]
				,max(b.ngay_td1)			[ngay_td1]
				,max(b.sl_td1) [sl_td1]
				,max(b.sl_td2) [sl_td2]
				,max(b.sl_td3) [sl_td3]
				,max(b.gc_td1) [gc_td1]
				,max(b.gc_td2) [gc_td2]
				,max(b.gc_td3) [gc_td3]
				,max(b.ngay_td2) [ngay_td2]
				,max(b.ngay_td3) [ngay_td3]
				,max(a.ma_bp) [ma_bp]
				,'' as [LOAI_CTGS]
				,'' as [KIEU_CTGS]
				,'' as [SO_LO0]				
				,SUM(Tien2-Ck-Gg) AS Tien11
				,SUM(Tien_Nt2-Ck_nt-Gg_nt) AS Tien_Nt11
				
				
			INTO #ARA00_T_GROUP3
			FROM AM81 a join AD81 b on a.stt_rec = b.stt_rec
						left join ALvt v on b.ma_vt = v.ma_vt
						left join Alkho k on b.ma_kho_i = k.ma_kho
			WHERE a.stt_rec = @Stt_rec
			GROUP BY b.Ma_kho_i, b.tk_dt, b.ma_vv_i, b.ma_td_i              ,ma_hd,ma_ku,ma_phi
			--============== GROUP BY Ma_kho_i, Tk_Dt, Ma_vv_i, Ma_td_i ==========
			
			Print 'Tao bang tam ARA00_T_GROUP4'
			SELECT
				max(stt_rec)	[stt_rec]
				,ma_dvcs			--Group
				,max(ma_ct)		[ma_ct]
				,max(ngay_ct)	[ngay_ct]
				,max(ngay_lct)	[ngay_lct]
				,max(so_ct)		[so_ct]
				,max(so_lo)		[so_lo]
				,max(ngay_lo)	[ngay_lo]
				,max(ong_ba)	[ong_ba]
				,max(dien_giaih) [dien_giaih]
				,max(dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk]
				,	max(ma_kho_i)[ma_kho_i]
				,	tk_dt			--Group
				,MAX(tk)	[tk]
				,MAX(tk_du)	[tk_du]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(ma_nt)	[ma_nt]
				,max(ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(ma_kh) [ma_kh]
				,max(tk_cn)	[tk_cn]
				,	ma_vv			--Group
				,max(ma_nk)	[ma_nk]
				,	ma_td			--Group
				,	ma_ku			--Group
				,max(LOAI_CT) as [loai_ct]
				,max(ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	ma_hd			--Group
				,	Ma_phi			--Group
				,max(Ma_nvien)			[Ma_nvien]
				,max(Ma_bpht)			[Ma_bpht]
				,max(user_id0)			[user_id0]
				,max(date0)			[date0]
				,max(time0)			[time0]
				,max(user_id2)			[user_id2]
				,max(date2)			[date2]
				,max(time2)			[time2]
				,max([status])			[status]
				,max(so_dh)			[so_dh]
				,max(so_ct) as [so_ct0]
				,max(ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(ma_gd)			[ma_gd]
				,max(ln)			[ln]
				,max(ma_td2) [ma_td2]
				,max(ma_td3) [ma_td3]
				,max(ngay_td1)		[ngay_td1]
				,max(sl_td1) [sl_td1]
				,max(sl_td2) [sl_td2]
				,max(sl_td3) [sl_td3]
				,max(gc_td1) [gc_td1]
				,max(gc_td2) [gc_td2]
				,max(gc_td3) [gc_td3]
				,max(ngay_td2) [ngay_td2]
				,max(ngay_td3) [ngay_td3]
				,max(ma_bp) [ma_bp]
				,'' as [LOAI_CTGS]
				,'' as [KIEU_CTGS]
				,'' as [SO_LO0]
				
				,SUM(Tien11) as TienARA
				,SUM(Tien_nt11) as Tien_nt1
			INTO #ARA00_T_GROUP4
			FROM #ARA00_T_GROUP3			
			GROUP BY ma_dvcs, tk_dt, ma_vv, ma_td,ma_hd,ma_ku,ma_phi
			
			Print 'Insert lan 3'
			Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
			Select
			[stt_rec]
			,[ma_dvcs]
			,[ma_ct]
			,[ngay_ct]
			,[ngay_lct]
			,[so_ct]
			,[so_lo]
			,[ngay_lo]
			,[ong_ba]
			,[dien_giaih]
			,[dien_giai]
			,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY ma_dvcs, tk_dt, ma_vv, ma_td             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
			,tk_du	as [tk]
			,tk_dt	as [tk_du]
			,case when @Ma_nt = @M_Ma_nt0 then 0 else Tien_nt1 end as [ps_no_nt]
			,ps_no_nt as [ps_co_nt]
			,[ma_nt]
			,[ty_gia]
			,TienARA as [ps_no]
			,[ps_co]
			,[ma_kh]
			,1 as [tk_cn]
			,[ma_vv]
			,[ma_nk]
			,[ma_td]
			,[ma_ku]
			,[loai_ct]
			,[Ma_sp]
			,[So_lsx]
			,[Ma_hd]
			,[Ma_phi]
			,[Ma_nvien]
			,[Ma_bpht]
			,[user_id0]
			,[date0]
			,[time0]
			,[user_id2]
			,[date2]
			,[time2]
			,[status]
			,[so_dh]
			,[so_ct0]
			,[ngay_ct0]
			,[ct_nxt]
			,[ma_gd]
			,[ln]
			,[ma_td2]
			,[ma_td3]
			,[ngay_td1]
			,[sl_td1]
			,[sl_td2]
			,[sl_td3]
			,[gc_td1]
			,[gc_td2]
			,[gc_td3]
			,[ngay_td2]
			,[ngay_td3]
			,[ma_bp]
			,[LOAI_CTGS]
			,[KIEU_CTGS]
			,[SO_LO0]
			From #ARA00_T_GROUP4
			WHERE TienARA <> 0 or Tien_nt1 <> 0

			Print '--Insert lần 4'
			Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
				Select
			[stt_rec]
			,[ma_dvcs]
			,[ma_ct]
			,[ngay_ct]
			,[ngay_lct]
			,[so_ct]
			,[so_lo]
			,[ngay_lo]
			,[ong_ba]
			,[dien_giaih]
			,[dien_giai]
			,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY ma_dvcs, tk_dt, ma_vv, ma_td             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
			
			,tk_dt as [tk]
			,[tk_du]
			,[ps_no_nt]
			,case when @Ma_nt = @M_Ma_nt0 then 0 else Tien_nt1 end [ps_co_nt]
			,[ma_nt]
			,[ty_gia]
			,[ps_no]
			,TienAra [ps_co]
			,[ma_kh]
			,1 as [tk_cn]
			,[ma_vv]
			,[ma_nk]
			,[ma_td]
			,[ma_ku]
			,[loai_ct]
			,[Ma_sp]
			,[So_lsx]
			,[Ma_hd]
			,[Ma_phi]
			,[Ma_nvien]
			,[Ma_bpht]
			,[user_id0]
			,[date0]
			,[time0]
			,[user_id2]
			,[date2]
			,[time2]
			,[status]
			,[so_dh]
			,[so_ct0]
			,[ngay_ct0]
			,[ct_nxt]
			,[ma_gd]
			,[ln]
			,[ma_td2]
			,[ma_td3]
			,[ngay_td1]
			,[sl_td1]
			,[sl_td2]
			,[sl_td3]
			,[gc_td1]
			,[gc_td2]
			,[gc_td3]
			,[ngay_td2]
			,[ngay_td3]
			,[ma_bp]
			,[LOAI_CTGS]
			,[KIEU_CTGS]
			,[SO_LO0]
			From #ARA00_T_GROUP4
			WHERE TienARA <> 0 or Tien_nt1 <> 0
			
			Drop table #ARA00_T_GROUP3, #ARA00_T_GROUP4
		
		--&& Dinh khoan chiet khau, giam gia
		IF  @Loai_ck= '0'	-- && Chi tiet
		BEGIN -- 5678
			If @t_ck + @t_ck_nt <> 0
			Begin --5,6
				Select @_Nh_dk = COUNT(1)/2 from ARA00 where stt_rec = @Stt_rec
				Print 'Tao bang tam #ARA00_T_GROUP5'
				SELECT
				max(a.stt_rec)	[stt_rec]
				,max(Case When (k.ma_dvcs is not null) then k.ma_dvcs else '' end)	[ma_dvcs]
				,isnull(b.tk_cki,'') [tk_cki]	--Group
				,max(a.ma_ct)	[ma_ct]
				,max(a.ngay_ct)	[ngay_ct]
				,max(a.ngay_lct) [ngay_lct]
				,max(a.so_ct)	[so_ct]
				,max(a.so_lo)	[so_lo]
				,max(a.ngay_lo)	[ngay_lo]
				,max(a.ong_ba)	[ong_ba]
				,max(a.dien_giai) [dien_giaih]
				,max(a.dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk],
				(b.ma_kho_i)[ma_kho_i]		--Group
				,max(b.tk_dt)[tk_dt]
				,max(a.ma_nx)	[tk]
				,max(b.tk_cki)	[tk_du]
				,MAX(a.ma_nx)	[ma_nx]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(a.ma_nt)	[ma_nt]
				,max(a.ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(a.ma_kh) [ma_kh]
				,max(a.tk_cn)	[tk_cn]
				,b.ma_vv_i	[ma_vv]			--Group
				,max(a.ma_nk)	[ma_nk]
				,b.ma_td_i [ma_td]			--Group
				,	b.ma_ku					--Group
				,max(a.LOAI_CT0) as [loai_ct]
				,max(b.ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	b.ma_hd					--Group
				,	b.Ma_phi				--Group
				,max(a.Ma_nvien)			[Ma_nvien]
				,max(b.Ma_bpht)			[Ma_bpht]
				,max(a.user_id0)			[user_id0]
				,max(a.date0)			[date0]
				,max(a.time0)			[time0]
				,max(a.user_id2)			[user_id2]
				,max(a.date2)			[date2]
				,max(a.time2)			[time2]
				,max(a.[status])			[status]
				,max(a.so_dh)			[so_dh]
				,max(a.so_ct) as [so_ct0]
				,max(a.ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(a.ma_gd)			[ma_gd]
				,max(b.ln)			[ln]
				,max(b.ma_td2) [ma_td2]
				,max(b.ma_td3) [ma_td3]
				,max(b.ngay_td1)			[ngay_td1]
				,max(b.sl_td1) [sl_td1]
				,max(b.sl_td2) [sl_td2]
				,max(b.sl_td3) [sl_td3]
				,max(b.gc_td1) [gc_td1]
				,max(b.gc_td2) [gc_td2]
				,max(b.gc_td3) [gc_td3]
				,max(b.ngay_td2) [ngay_td2]
				,max(b.ngay_td3) [ngay_td3]
				,max(a.ma_bp) [ma_bp]
				,'' as [LOAI_CTGS]
				,'' as [KIEU_CTGS]
				,'' as [SO_LO0]
				
				,SUM(ck_nt) AS Ck_nt11
				,SUM(Ck) AS Ck11 
				
				INTO #ARA00_T_GROUP5
				FROM AM81 a join AD81 b on a.stt_rec = b.stt_rec
							left join ALvt v on b.ma_vt = v.ma_vt
							left join Alkho k on b.ma_kho_i = k.ma_kho
				WHERE a.stt_rec = @Stt_rec
				GROUP BY Ma_kho_i, Ma_vv_i, Ma_td_i,Tk_cki              ,ma_hd,ma_ku,ma_phi
				
				Print 'Tao bang tam 6'
				SELECT
				max(stt_rec)	[stt_rec]
				,ma_dvcs	[ma_dvcs]			--Group
				,tk_cki							--Group
				,max(ma_ct)	[ma_ct]
				,max(ngay_ct)	[ngay_ct]
				,max(ngay_lct) [ngay_lct]
				,max(so_ct)	[so_ct]
				,max(so_lo)	[so_lo]
				,max(ngay_lo)	[ngay_lo]
				,max(ong_ba)	[ong_ba]
				,max(dien_giai) [dien_giaih]
				,max(dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk],
					max(ma_kho_i)	[ma_kho_i]
				,max(tk_dt)	[tk_dt]
				,MAX(tk)	[tk]
				,MAX(tk_du)	[tk_du]
				,max(ma_nx) [ma_nx]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(ma_nt)	[ma_nt]
				,max(ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(ma_kh) [ma_kh]
				,max(tk_cn)	[tk_cn]
				,	[ma_vv]			--Group
				,max(ma_nk)	[ma_nk]
				,	[ma_td]			--Group
				,	ma_ku			--Group
				,max(LOAI_CT) as [loai_ct]
				,max(ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	ma_hd			--Group
				,	Ma_phi			--Group
				,max(Ma_nvien)			[Ma_nvien]
				,max(Ma_bpht)			[Ma_bpht]
				,max(user_id0)			[user_id0]
				,max(date0)			[date0]
				,max(time0)			[time0]
				,max(user_id2)			[user_id2]
				,max(date2)			[date2]
				,max(time2)			[time2]
				,max([status])			[status]
				,max(so_dh)			[so_dh]
				,max(so_ct) as [so_ct0]
				,max(ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(ma_gd)			[ma_gd]
				,max(ln)			[ln]
				,max(ma_td2) [ma_td2]
				,max(ma_td3) [ma_td3]
				,max(ngay_td1)		[ngay_td1]
				,max(sl_td1) [sl_td1]
				,max(sl_td2) [sl_td2]
				,max(sl_td3) [sl_td3]
				,max(gc_td1) [gc_td1]
				,max(gc_td2) [gc_td2]
				,max(gc_td3) [gc_td3]
				,max(ngay_td2) [ngay_td2]
				,max(ngay_td3) [ngay_td3]
				,max(ma_bp) [ma_bp]
				,'' as [LOAI_CTGS]
				,'' as [KIEU_CTGS]
				,'' as [SO_LO0]
				
				,SUM(Ck11) as Ck1
				,SUM(Ck_nt11) AS Ck_nt1
				INTO #ARA00_T_GROUP6
				FROM #ARA00_T_GROUP5
				GROUP BY Ma_dvcs, Ma_vv, Ma_td,Tk_cki             ,ma_hd,ma_ku,ma_phi
			
				Print 'Insert lan 5'
			Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
			Select
			[stt_rec]
			,[ma_dvcs]
			,[ma_ct]
			,[ngay_ct]
			,[ngay_lct]
			,[so_ct]
			,[so_lo]
			,[ngay_lo]
			,[ong_ba]
			,[dien_giaih]
			,[dien_giai]
			,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY Ma_dvcs, Ma_vv, Ma_td,Tk_cki             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
			
			,isnull(ma_nx,'')	[tk]
			,isnull(tk_cki,'')	[tk_du]
			,[ps_no_nt]
			,case when @Ma_nt = @M_Ma_nt0 then 0 else Ck_nt1 end as  [ps_co_nt]
			,[ma_nt]
			,[ty_gia]
			,[ps_no]
			,Ck1 as [ps_co]
			,[ma_kh]
			,[tk_cn]
			,[ma_vv]
			,[ma_nk]
			,[ma_td]
			,[ma_ku]
			,[loai_ct]
			,[Ma_sp]
			,[So_lsx]
			,[Ma_hd]
			,[Ma_phi]
			,[Ma_nvien]
			,[Ma_bpht]
			,[user_id0]
			,[date0]
			,[time0]
			,[user_id2]
			,[date2]
			,[time2]
			,[status]
			,[so_dh]
			,[so_ct0]
			,[ngay_ct0]
			,[ct_nxt]
			,[ma_gd]
			,[ln]
			,[ma_td2]
			,[ma_td3]
			,[ngay_td1]
			,[sl_td1]
			,[sl_td2]
			,[sl_td3]
			,[gc_td1]
			,[gc_td2]
			,[gc_td3]
			,[ngay_td2]
			,[ngay_td3]
			,[ma_bp]
			,[LOAI_CTGS]
			,[KIEU_CTGS]
			,[SO_LO0]
			From #ARA00_T_GROUP6

			Print '--Insert lần 6'
			Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
			Select
			[stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct],[so_lo],[ngay_lo]	,[ong_ba],[dien_giaih],[dien_giai]
			,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY Ma_dvcs, Ma_vv, Ma_td,Tk_cki             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
			,isnull(tk_cki,'') as [tk]
			,isnull(ma_nx,'') as [tk_du]
			,case when @Ma_nt = @M_Ma_nt0 then 0 else Ck_nt1 end as [ps_no_nt]
			,[ps_co_nt]
			,[ma_nt]
			,[ty_gia]
			,Ck1 as [ps_no]
			,[ps_co]
			,[ma_kh],[tk_cn],[ma_vv],[ma_nk],[ma_td],[ma_ku],[loai_ct],[Ma_sp],[So_lsx],[Ma_hd],[Ma_phi],[Ma_nvien],[Ma_bpht]
			,[user_id0],[date0],[time0],[user_id2],[date2],[time2],[status],[so_dh],[so_ct0],[ngay_ct0],[ct_nxt],[ma_gd]
			,[ln],[ma_td2],[ma_td3]	,[ngay_td1]	,[sl_td1],[sl_td2],[sl_td3]	,[gc_td1],[gc_td2],[gc_td3]	,[ngay_td2]	,[ngay_td3]
			,[ma_bp]
			,[LOAI_CTGS]	,[KIEU_CTGS]	,[SO_LO0]
			From #ARA00_T_GROUP6
			
			Drop table #ARA00_T_GROUP5, #ARA00_T_GROUP6
			End -- 5,6
		END --IF 5678
		Else	-- 5678
		Begin
			If @t_ck + @t_ck_nt <> 0 and @tk_ck <> ''
			Begin -- 7,8
				Select @_Nh_dk = COUNT(1)/2 from ARA00 where stt_rec = @Stt_rec
				Print 'Tao bang tam #ARA00_T_GROUP7'
				SELECT
				max(a.stt_rec)	[stt_rec]
				,max(Case When (k.ma_dvcs is not null) then k.ma_dvcs else '' end)	[ma_dvcs]
				,MAX(tk_cki)	[tk_cki]
				,max(a.ma_ct)	[ma_ct]
				,max(a.ngay_ct)	[ngay_ct]
				,max(a.ngay_lct) [ngay_lct]
				,max(a.so_ct)	[so_ct]
				,max(a.so_lo)	[so_lo]
				,max(a.ngay_lo)	[ngay_lo]
				,max(a.ong_ba)	[ong_ba]
				,max(a.dien_giai) [dien_giaih]
				,max(a.dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk],
				(b.ma_kho_i)[ma_kho_i]		--Group
				,max(b.tk_dt)[tk_dt]
				,max(a.ma_nx)	[tk]
				,max(a.tk_ck)	[tk_du]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(a.ma_nt)	[ma_nt]
				,max(a.ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(a.ma_kh) [ma_kh]
				,max(a.tk_cn)	[tk_cn]
				,b.ma_vv_i	[ma_vv]			--Group
				,max(a.ma_nk)	[ma_nk]
				,b.ma_td_i [ma_td]			--Group
				,	b.ma_ku					--Group
				,max(a.LOAI_CT0) as [loai_ct]
				,max(b.ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	b.ma_hd					--Group
				,	b.Ma_phi				--Group
				,max(a.Ma_nvien)			[Ma_nvien]
				,max(b.Ma_bpht)			[Ma_bpht]
				,max(a.user_id0)			[user_id0]
				,max(a.date0)			[date0]
				,max(a.time0)			[time0]
				,max(a.user_id2)			[user_id2]
				,max(a.date2)			[date2]
				,max(a.time2)			[time2]
				,max(a.[status])			[status]
				,max(a.so_dh)			[so_dh]
				,max(a.so_ct) as [so_ct0]
				,max(a.ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(a.ma_gd)			[ma_gd]
				,max(b.ln)			[ln]
				,max(b.ma_td2) [ma_td2]
				,max(b.ma_td3) [ma_td3]
				,max(b.ngay_td1)			[ngay_td1]
				,max(b.sl_td1) [sl_td1]
				,max(b.sl_td2) [sl_td2]
				,max(b.sl_td3) [sl_td3]
				,max(b.gc_td1) [gc_td1]
				,max(b.gc_td2) [gc_td2]
				,max(b.gc_td3) [gc_td3]
				,max(b.ngay_td2) [ngay_td2]
				,max(b.ngay_td3) [ngay_td3]
				,max(a.ma_bp) [ma_bp]
				,'' as [LOAI_CTGS]
				,'' as [KIEU_CTGS]
				,'' as [SO_LO0]
				
				,SUM(ck_nt) AS Ck_nt11
				,SUM(Ck) AS Ck11 
				
				INTO #ARA00_T_GROUP7
				FROM AM81 a join AD81 b on a.stt_rec = b.stt_rec
							left join ALvt v on b.ma_vt = v.ma_vt
							left join Alkho k on b.ma_kho_i = k.ma_kho
				WHERE a.stt_rec = @Stt_rec
				
				GROUP BY Ma_kho_i, Ma_vv_i, Ma_td_i              ,ma_hd,ma_ku,ma_phi
				--IF !EMPTY(M.T_Ck_Nt + M.T_Ck)
				
				Print 'Tao bang tam #ARA00_T_GROUP8'
				SELECT
				max(stt_rec)	[stt_rec]
				,ma_dvcs	[ma_dvcs]			--Group
				,max(tk_cki)[tk_cki]
				,max(ma_ct)	[ma_ct]
				,max(ngay_ct)	[ngay_ct]
				,max(ngay_lct) [ngay_lct]
				,max(so_ct)	[so_ct]
				,max(so_lo)	[so_lo]
				,max(ngay_lo)	[ngay_lo]
				,max(ong_ba)	[ong_ba]
				,max(dien_giai) [dien_giaih]
				,max(dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk],
					max(ma_kho_i)	[ma_kho_i]
					,max(tk_dt)		[tk_dt]
				,MAX(tk)	[tk]
				,MAX(tk_du)	[tk_du]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(ma_nt)	[ma_nt]
				,max(ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(ma_kh) [ma_kh]
				,max(tk_cn)	[tk_cn]
				,	[ma_vv]			--Group
				,max(ma_nk)	[ma_nk]
				,	[ma_td]			--Group
				,	ma_ku			--Group
				,max(LOAI_CT) as [loai_ct]
				,max(ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	ma_hd			--Group
				,	Ma_phi			--Group
				,max(Ma_nvien)			[Ma_nvien]
				,max(Ma_bpht)			[Ma_bpht]
				,max(user_id0)			[user_id0]
				,max(date0)			[date0]
				,max(time0)			[time0]
				,max(user_id2)			[user_id2]
				,max(date2)			[date2]
				,max(time2)			[time2]
				,max([status])			[status]
				,max(so_dh)			[so_dh]
				,max(so_ct) as [so_ct0]
				,max(ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(ma_gd)			[ma_gd]
				,max(ln)			[ln]
				,max(ma_td2) [ma_td2]
				,max(ma_td3) [ma_td3]
				,max(ngay_td1)		[ngay_td1]
				,max(sl_td1) [sl_td1]
				,max(sl_td2) [sl_td2]
				,max(sl_td3) [sl_td3]
				,max(gc_td1) [gc_td1]
				,max(gc_td2) [gc_td2]
				,max(gc_td3) [gc_td3]
				,max(ngay_td2) [ngay_td2]
				,max(ngay_td3) [ngay_td3]
				,max(ma_bp) [ma_bp]
				,'' [LOAI_CTGS]
				,'' [KIEU_CTGS]
				,'' [SO_LO0]
				
				,SUM(Ck11) as Ck1
				,SUM(Ck_nt11) AS Ck_nt1
				INTO #ARA00_T_GROUP8
				FROM #ARA00_T_GROUP7
				GROUP BY Ma_dvcs, Ma_vv, Ma_td             ,ma_hd,ma_ku,ma_phi
			
				Print 'Insert lan 7'
				Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
				Select
				[stt_rec]
				,[ma_dvcs]
				,[ma_ct]
				,[ngay_ct]
				,[ngay_lct]
				,[so_ct]
				,[so_lo]
				,[ngay_lo]
				,[ong_ba]
				,[dien_giaih]
				,[dien_giai]
				,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY Ma_dvcs, Ma_vv, Ma_td             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
				,[tk]
				,[tk_du]
				,[ps_no_nt]
				,case when @Ma_nt = @M_Ma_nt0 then 0 else Ck_nt1 end as [ps_co_nt]
				,[ma_nt]
				,[ty_gia]
				,[ps_no]
				,ck1 [ps_co]
				,[ma_kh]
				,[tk_cn]
				,[ma_vv]
				,[ma_nk]
				,[ma_td]
				,[ma_ku]
				,[loai_ct]
				,[Ma_sp]
				,[So_lsx]
				,[Ma_hd]
				,[Ma_phi]
				,[Ma_nvien]
				,[Ma_bpht]
				,[user_id0]
				,[date0]
				,[time0]
				,[user_id2]
				,[date2]
				,[time2]
				,[status]
				,[so_dh]
				,[so_ct0]
				,[ngay_ct0]
				,[ct_nxt]
				,[ma_gd]
				,[ln]
				,[ma_td2]
				,[ma_td3]
				,[ngay_td1]
				,[sl_td1]
				,[sl_td2]
				,[sl_td3]
				,[gc_td1]
				,[gc_td2]
				,[gc_td3]
				,[ngay_td2]
				,[ngay_td3]
				,[ma_bp]
				,[LOAI_CTGS]
				,[KIEU_CTGS]
				,[SO_LO0]
				From #ARA00_T_GROUP8

				Print '--Insert lần 8'
				Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
					Select
				[stt_rec],[ma_dvcs]	,[ma_ct],[ngay_ct],[ngay_lct],[so_ct],[so_lo],[ngay_lo],[ong_ba],[dien_giaih],[dien_giai]
				,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY Ma_dvcs, Ma_vv, Ma_td             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
				,tk_du as [tk]
				,tk as [tk_du]
				,case when @Ma_nt = @M_Ma_nt0 then 0 else Ck_nt1 end as [ps_no_nt]
				,[ps_co_nt]
				,[ma_nt]
				,[ty_gia]
				,ck1 as [ps_no]
				,[ps_co]
				,[ma_kh],[tk_cn],[ma_vv],[ma_nk],[ma_td],[ma_ku],[loai_ct]
				,[Ma_sp]
				,[So_lsx],[Ma_hd],[Ma_phi],[Ma_nvien],[Ma_bpht]
				,[user_id0],[date0],[time0],[user_id2],[date2],[time2],[status],[so_dh],[so_ct0],[ngay_ct0],[ct_nxt],[ma_gd],[ln]
				,[ma_td2],[ma_td3],[ngay_td1],[sl_td1],[sl_td2],[sl_td3],[gc_td1],[gc_td2],[gc_td3],[ngay_td2],[ngay_td3]
				,[ma_bp]
				,[LOAI_CTGS],[KIEU_CTGS],[SO_LO0]
				From #ARA00_T_GROUP8
				--Lần 7
				--Lần 8
			End -- 7,8
		END --5678
		
		--&& Dinh khoan Thue GTGT
		IF @t_thue + @t_thue_nt <> 0 and @tk_thue_no <> ''-- !EMPTY(M.T_Thue_Nt + M.T_Thue)  .AND. !EMPTY(m.Tk_Thue_No)
		BEGIN -- 9,10
			Select @_Nh_dk = COUNT(1)/2 from ARA00 where stt_rec = @Stt_rec
			Print 'Tao bang tam #ARA00_T_GROUP9'
			SELECT
				max(a.stt_rec)	[stt_rec]
				,max(Case When (k.ma_dvcs is not null) then k.ma_dvcs else '' end)	[ma_dvcs]
				,MAX(tk_cki)	[tk_cki]
				,max(a.ma_ct)	[ma_ct]
				,max(a.ngay_ct)	[ngay_ct]
				,max(a.ngay_lct) [ngay_lct]
				,max(a.so_ct)	[so_ct]
				,max(a.so_lo)	[so_lo]
				,max(a.ngay_lo)	[ngay_lo]
				,max(a.ong_ba)	[ong_ba]
				,max(a.dien_giai) [dien_giaih]
				,max(a.dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk],
				(b.ma_kho_i)[ma_kho_i]		--Group
				,max(b.tk_dt)[tk_dt]
				,max(a.tk_thue_no)	[tk]
				,max(a.tk_thue_co)	[tk_du]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(a.ma_nt)	[ma_nt]
				,max(a.ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(a.ma_kh) [ma_kh]
				,max(a.tk_cn)	[tk_cn]
				,b.ma_vv_i	[ma_vv]			--Group
				,max(a.ma_nk)	[ma_nk]
				,b.ma_td_i [ma_td]			--Group
				,	b.ma_ku					--Group
				,max(a.LOAI_CT0) as [loai_ct]
				,max(b.ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	b.ma_hd					--Group
				,	b.Ma_phi				--Group
				,max(a.Ma_nvien)			[Ma_nvien]
				,max(b.Ma_bpht)			[Ma_bpht]
				,max(a.user_id0)			[user_id0]
				,max(a.date0)			[date0]
				,max(a.time0)			[time0]
				,max(a.user_id2)			[user_id2]
				,max(a.date2)			[date2]
				,max(a.time2)			[time2]
				,max(a.[status])			[status]
				,max(a.so_dh)			[so_dh]
				,max(a.so_ct) as [so_ct0]
				,max(a.ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(a.ma_gd)			[ma_gd]
				,max(b.ln)			[ln]
				,max(b.ma_td2) [ma_td2]
				,max(b.ma_td3) [ma_td3]
				,max(b.ngay_td1)			[ngay_td1]
				,max(b.sl_td1) [sl_td1]
				,max(b.sl_td2) [sl_td2]
				,max(b.sl_td3) [sl_td3]
				,max(b.gc_td1) [gc_td1]
				,max(b.gc_td2) [gc_td2]
				,max(b.gc_td3) [gc_td3]
				,max(b.ngay_td2) [ngay_td2]
				,max(b.ngay_td3) [ngay_td3]
				,max(a.ma_bp) [ma_bp]
				,'' as [LOAI_CTGS]	,'' as [KIEU_CTGS]	,'' as [SO_LO0]
				
				--,SUM(thue_nt) AS Thue_nt11
				--,SUM(thue) AS Thue11 
				,SUM((b.tien_nt2 -b.ck_nt) *a.thue_suat/100) as Thue_nt11--Cần sửa lại. tính thuế trên AD81 luôn ngay trên form
				,SUM((b.tien2 - b.ck) * a.thue_suat/100) as Thue11
				
			INTO #ARA00_T_GROUP9
			FROM AM81 a join AD81 b on a.stt_rec = b.stt_rec
						left join ALvt v on b.ma_vt = v.ma_vt
						left join Alkho k on b.ma_kho_i = k.ma_kho
			WHERE a.stt_rec = @Stt_rec
			
			GROUP BY Ma_kho_i, Ma_vv_i, Ma_td_i              ,ma_hd,ma_ku,ma_phi
			
			Print 'Tao bang tam #ARA00_T_GROUP10'
			SELECT
				max(stt_rec)	[stt_rec]
				,ma_dvcs	[ma_dvcs]			--Group
				,max(tk_cki)[tk_cki]
				,max(ma_ct)	[ma_ct]
				,max(ngay_ct)	[ngay_ct]
				,max(ngay_lct) [ngay_lct]
				,max(so_ct)	[so_ct]
				,max(so_lo)	[so_lo]
				,max(ngay_lo)	[ngay_lo]
				,max(ong_ba)	[ong_ba]
				,max(dien_giai) [dien_giaih]
				,max(dien_giai) [dien_giai]
				,@_Nh_dk	[nh_dk],
					max(ma_kho_i)	[ma_kho_i]
					,max(tk_dt)		[tk_dt]
				,MAX(tk)	[tk]
				,MAX(tk_du)	[tk_du]
				,0 as		[ps_no_nt]
				,0 as		[ps_co_nt]
				,max(ma_nt)	[ma_nt]
				,max(ty_gia) [ty_gia]
				,0 as [ps_no]
				,0 as [ps_co]
				,max(ma_kh) [ma_kh]
				,max(tk_cn)	[tk_cn]
				,	[ma_vv]			--Group
				,max(ma_nk)	[ma_nk]
				,	[ma_td]			--Group
				,	ma_ku			--Group
				,max(LOAI_CT) as [loai_ct]
				,max(ma_sp)	[Ma_sp]
				,'' as [So_lsx]
				,	ma_hd			--Group
				,	Ma_phi			--Group
				,max(Ma_nvien)		[Ma_nvien]
				,max(Ma_bpht)		[Ma_bpht]
				,max(user_id0)		[user_id0]
				,max(date0)			[date0]
				,max(time0)			[time0]
				,max(user_id2)		[user_id2]
				,max(date2)			[date2]
				,max(time2)			[time2]
				,max([status])		[status]
				,max(so_dh)			[so_dh]
				,max(so_ct) as [so_ct0]
				,max(ngay_ct) as [ngay_ct0]
				,1 as [ct_nxt]
				,max(ma_gd)			[ma_gd]
				,max(ln)			[ln]
				,max(ma_td2) [ma_td2]
				,max(ma_td3) [ma_td3]
				,max(ngay_td1)		[ngay_td1]
				,max(sl_td1) [sl_td1]
				,max(sl_td2) [sl_td2]
				,max(sl_td3) [sl_td3]
				,max(gc_td1) [gc_td1]
				,max(gc_td2) [gc_td2]
				,max(gc_td3) [gc_td3]
				,max(ngay_td2) [ngay_td2]
				,max(ngay_td3) [ngay_td3]
				,max(ma_bp) [ma_bp]
				,'' as [LOAI_CTGS],'' as [KIEU_CTGS],'' as [SO_LO0]
				
				,SUM(Thue11) as Thue1
				,SUM(Thue_nt11) AS Thue_nt1
				
			INTO #ARA00_T_GROUP10
			FROM #ARA00_T_GROUP9
			
			GROUP BY Ma_dvcs, Ma_vv, Ma_td             ,ma_hd,ma_ku,ma_phi
			
			Print '-- Insert 9 ==================================================================================='
			Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
			Select
			[stt_rec],[ma_dvcs],[ma_ct],[ngay_ct],[ngay_lct],[so_ct],[so_lo],[ngay_lo],[ong_ba],[dien_giaih],[dien_giai]
			,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY Ma_dvcs, Ma_vv, Ma_td             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
			,[tk]
			,[tk_du]
			,case when @Ma_nt = @M_Ma_nt0 then 0 else Thue_nt1 end as [ps_no_nt]
			,[ps_co_nt]
			,[ma_nt],[ty_gia]
			,Thue1 as [ps_no]
			,[ps_co]
			,[ma_kh],[tk_cn],[ma_vv],[ma_nk],[ma_td],[ma_ku],[loai_ct],[Ma_sp],[So_lsx],[Ma_hd],[Ma_phi],[Ma_nvien],[Ma_bpht]
			,[user_id0],[date0],[time0],[user_id2],[date2],[time2],[status],[so_dh],[so_ct0],[ngay_ct0],[ct_nxt],[ma_gd],[ln]
			,[ma_td2],[ma_td3],[ngay_td1],[sl_td1],[sl_td2],[sl_td3],[gc_td1],[gc_td2],[gc_td3],[ngay_td2],[ngay_td3]
			,[ma_bp]
			,[LOAI_CTGS],[KIEU_CTGS],[SO_LO0]
			From #ARA00_T_GROUP10

			Print '-- Insert 10 ==================================================================================='
			Insert into ARA00 ([stt_rec], [ma_dvcs], [ma_ct], [ngay_ct], [ngay_lct], [so_ct], [so_lo], [ngay_lo], [ong_ba], [dien_giaih], [dien_giai], [nh_dk], [tk], [tk_du], [ps_no_nt], [ps_co_nt], [ma_nt], [ty_gia], [ps_no], [ps_co], [ma_kh], [tk_cn], [ma_vv], [ma_nk], [ma_td], [ma_ku], [loai_ct], [Ma_sp], [So_lsx], [Ma_hd], [Ma_phi], [Ma_nvien], [Ma_bpht], [user_id0], [date0], [time0], [user_id2], [date2], [time2], [status], [so_dh], [so_ct0], [ngay_ct0], [ct_nxt], [ma_gd], [ln], [ma_td2], [ma_td3], [ngay_td1], [sl_td1], [sl_td2], [sl_td3], [gc_td1], [gc_td2], [gc_td3], [ngay_td2], [ngay_td3], [ma_bp], [LOAI_CTGS], [KIEU_CTGS], [SO_LO0])
				Select
			[stt_rec],[ma_dvcs],[ma_ct],[ngay_ct],[ngay_lct],[so_ct]
			,[so_lo]
			,[ngay_lo]
			,[ong_ba]
			,[dien_giaih]
			,[dien_giai]
			,Right('000' + convert(varchar(3), @_Nh_dk + ROW_NUMBER() over(ORDER BY Ma_dvcs, Ma_vv, Ma_td             ,ma_hd,ma_ku,ma_phi)), 3) as [nh_dk]
			,tk_du as [tk]
			,tk as [tk_du]
			,[ps_no_nt]
			,case when @Ma_nt = @M_Ma_nt0 then 0 else Thue_nt1 end as [ps_co_nt]
			,[ma_nt]
			,[ty_gia]
			,[ps_no]
			,Thue1 as [ps_co]
			,[ma_kh],[tk_cn],[ma_vv],[ma_nk],[ma_td],[ma_ku],[loai_ct]
			,[Ma_sp]
			,[So_lsx],[Ma_hd]
			,[Ma_phi],[Ma_nvien],[Ma_bpht]
			,[user_id0],[date0],[time0],[user_id2],[date2],[time2]
			,[status],[so_dh],[so_ct0],[ngay_ct0],[ct_nxt],[ma_gd],[ln]
			,[ma_td2],[ma_td3],[ngay_td1],[sl_td1],[sl_td2],[sl_td3],[gc_td1],[gc_td2],[gc_td3],[ngay_td2],[ngay_td3]
			,[ma_bp]
			,[LOAI_CTGS]
			,[KIEU_CTGS]
			,[SO_LO0]
			From #ARA00_T_GROUP10
			--Lần 9
			--Lần 10
		END -- 9,10
	END --@_Dk <> 1 ToanBo
		
		--Update ln
		Declare @ln numeric(5,0) Set @ln = 0
		Update ARA00 Set @ln = ln = @ln + 1 where stt_rec = @Stt_rec
		
		Print 'dang viet ARA00 chua xong., so lieu sai so voi chuong trinh fox
			.'
		Print N'-- Kết thúc cập nhập ARA00 --'	
	END -- PROC