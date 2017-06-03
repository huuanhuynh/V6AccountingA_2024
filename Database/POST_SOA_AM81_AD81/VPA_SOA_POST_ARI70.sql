if exists (select 1 from sysobjects  where name = 'VPA_SOA_POST_ARI70')
	drop proc VPA_SOA_POST_ARI70
GO
print 'CREATE PROCEDURE [dbo].[VPA_SOA_POST_ARI70]'
GO
CREATE PROCEDURE VPA_SOA_POST_ARI70
	@Stt_rec varchar(13),
	@Ma_ct varchar(10),
	@Ma_nt varchar(10),
	@Ma_nx varchar(16),
	
	@UserID int
AS
BEGIN
	
	--================== Khai bao mot so bien su dung chung =====================================
		DECLARE @sql varchar(4000), @M_Ma_nt0 varchar(10), @Nxt tinyint
		Declare @ColumnName varchar(50), @DataType nvarchar(50), @nullable varchar(3), @DefaultValue varchar(10)
		SET @sql = '' SET @Nxt = 2 --Xuat
		--SET cMa_nt_0
		Select @M_Ma_nt0 = val from V6option where name = 'M_Ma_Nt0'
	
	Print 'VPA_SOA_POST_ARI70'
	
	DELETE FROM ARI70 WHERE Stt_rec = @Stt_rec
	
	BEGIN
		
		Set @sql = 'Insert into ARI70 Select a.stt_rec'
			
		Declare cur cursor for
			Select COLUMN_NAME, DATA_TYPE, IS_NULLABLE From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'ARI70'
			Order by ORDINAL_POSITION
		Open cur
		Fetch next from cur INTO @ColumnName, @DataType, @nullable
		while @@FETCH_STATUS = 0
		BEGIN
			If @ColumnName <> 'stt_rec'
			Begin--Các trường ở đây v ......................... sẽ lấy từ v		(Có thể lấy từ a hoặc b)
				
					 If @ColumnName = 'ma_kho'		Set @sql = @sql + ',b.ma_kho_i'
				Else If @ColumnName = 'ma_dvcs'		Set @sql = @sql + ',k.ma_dvcs'
				Else If @ColumnName = 'ma_vv'		Set @sql = @sql + ',b.ma_vv_i'
				Else If @ColumnName = 'ma_td'		Set @sql = @sql + ',b.ma_td_i'
				Else If @ColumnName = 'MA_LNX'		Set @sql = @sql + ',b.MA_LNX_I'
				
				
				Else If @ColumnName = 'Ma_nt' and (@Ma_nt = @M_Ma_nt0 OR @Ma_nt = '')
					Set @sql = @sql + ','''''
				Else If @ColumnName in('Gia_Nt','Tien_Nt_N','Tien_Nt_X','Gia_Nt0','Tien_Nt0','Cp_Nt','Nk_Nt','Gia_Nt2','Tien_Nt2','Thue_Nt','Ck_Nt','gg_nt','Ty_Gia','Gia_Nt1','Gia_Nt01','Gia_Nt21')
						and (@Ma_nt = @M_Ma_nt0 OR @Ma_nt = '')
					Set @sql = @sql + ',0'
				-- Cau nay loi.
				--Else If @ColumnName = 'Ma_Gd'		Set @sql = @sql + ',b.m_Ma_Gd'
				
				--Else If @ColumnName = 'pn_gia_tb'	Set @sql = @sql + ',b.????????'
				Else If @ColumnName = 'px_gia_dd'	Set @sql = @sql + ',CASE Rtrim(isnull(b.px_gia_ddi,0)) WHEN 0 THEN 0 ELSE 1 END'
				Else If @ColumnName = 'pt_cki'	Set @sql = @sql + ',CASE a.loai_ck WHEN ''1'' THEN a.pt_ck ELSE 0 END'
				
				Else If @ColumnName = 'Nxt'		Set @sql = @sql + ',' + CAST(@Nxt as varchar(2))
				
				Else If @ColumnName = 'Sl_Nhap' and @Nxt = 1	Set @sql = @sql + ',b.So_luong'
				Else If @ColumnName = 'Sl_Xuat'	and @Nxt = 1	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Tien_nhap'and @Nxt = 1	Set @sql = @sql + ',b.Tien'
				Else If @ColumnName = 'Tien_xuat'and @Nxt = 1	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Tien_nt_n'and @Nxt = 1	Set @sql = @sql + ',b.Tien_Nt'
				Else If @ColumnName = 'Tien_nt_x'and @Nxt = 1	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Sl_Nhap1'and @Nxt = 1	Set @sql = @sql + ',b.So_luong1'
				Else If @ColumnName = 'Sl_Xuat1'and @Nxt = 1	Set @sql = @sql + ',0'
				
				Else If @ColumnName = 'Sl_Nhap'	and @Nxt = 2	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Sl_Xuat'	and @Nxt = 2	Set @sql = @sql + ',b.So_luong'
				Else If @ColumnName = 'Tien_nhap'and @Nxt = 2	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Tien_xuat'and @Nxt = 2	Set @sql = @sql + ',b.Tien'
				Else If @ColumnName = 'Tien_nt_n'and @Nxt = 2	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Tien_nt_x'and @Nxt = 2	Set @sql = @sql + ',b.Tien_Nt'
				Else If @ColumnName = 'Sl_Nhap1'and @Nxt = 2	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Sl_Xuat1'and @Nxt = 2	Set @sql = @sql + ',b.So_luong1'
				
				--Đặc thù vạn phong
				Else If @ColumnName = 'Tien_nt2'and @Ma_ct = 'SOA'
					Set @sql = @sql + ',CASE ISNULL(B.SL_TD1,0) WHEN 0 THEN ISNULL(B.TIEN_NT2,0) ELSE case isnull(b.Tien_nt2,0) when 0 then isnull(b.SL_TD1,0) else (b.Tien_nt2-b.SL_TD1) end END'
				Else If @ColumnName = 'Tien1_nt'and @Ma_ct = 'SOA'	
					Set @sql = @sql + ',CASE ISNULL(B.SL_TD1,0) WHEN 0 THEN ISNULL(B.Tien1_nt,0) ELSE case isnull(b.Tien_nt2,0) when 0 then isnull(b.SL_TD1,0) else (b.Tien1_nt-b.SL_TD1) end END'
				
				Else If @ColumnName = 'Tien2'and @Ma_ct = 'SOA'
					Set @sql = @sql + ',CASE ISNULL(B.SL_TD1,0) WHEN 0 THEN ISNULL(B.Tien2,0) ELSE case isnull(b.Tien2,0) when 0 then isnull(b.SL_TD1,0) else (b.Tien2-b.SL_TD1) end END'
				Else If @ColumnName = 'Tien1'and @Ma_ct = 'SOA'	
					Set @sql = @sql + ',CASE ISNULL(B.SL_TD1,0) WHEN 0 THEN ISNULL(B.Tien1,0) ELSE case isnull(b.Tien2,0) when 0 then isnull(b.SL_TD1,0) else (b.Tien1-b.SL_TD1) end END'
								
				
					
				Else If @ColumnName = 'Ct_dc'	Set @sql = @sql + ',0'
				Else If @ColumnName = 'Stt_rec_dc'	Set @sql = @sql + ','''''
				
				--................................................................................
				-- Neu cac truong ton tai trong AM
				Else If exists (Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'AM81' and COLUMN_NAME = @ColumnName)
				begin
					if @nullable = 'YES'
						Set @sql = @sql + ',a.' + @ColumnName
					else
					begin
						If @DataType = 'numeric' or @DataType = 'tinyint' or @DataType = 'smallint' or @DataType = 'int' or @DataType = 'bigint'  or @DataType = 'money' or @DataType = 'bit' or @DataType = 'decimal'
							Set @DefaultValue = '0'
						Else If @DataType = 'char' or @DataType = 'nchar' or @DataType = 'varchar' or @DataType = 'nvarchar' or @DataType = 'nvarchar' or @DataType = 'text' or @DataType = 'ntext'
							Set @DefaultValue = '''''' -- hai dấu nháy đơn
						Else If @DataType = 'date' or @DataType = 'smalldatetime' or @DataType = 'datetime' or @DataType = 'datetime2'
							Set @DefaultValue = '''19000101'''
							
						Set @sql = @sql + ',ISNULL(a.'+@ColumnName+','+@DefaultValue+') '
					end
				end
				--Nếu trường có trong AD81 thì lấy luôn trường đó
				Else If exists (Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'AD81' and COLUMN_NAME = @ColumnName)
				begin	-- Ở đây có 2 trường hợp @nullable = NO và @nullable = YES
					if @nullable = 'YES'
						Set @sql = @sql + ',b.' + @ColumnName
					else
					begin
						If @DataType = 'numeric' or @DataType = 'tinyint' or @DataType = 'smallint' or @DataType = 'int' or @DataType = 'bigint'  or @DataType = 'money' or @DataType = 'bit' or @DataType = 'decimal'
							Set @DefaultValue = '0'
						Else If @DataType = 'char' or @DataType = 'nchar' or @DataType = 'varchar' or @DataType = 'nvarchar' or @DataType = 'nvarchar' or @DataType = 'text' or @DataType = 'ntext'
							Set @DefaultValue = '''''' -- hai dấu nháy đơn
						Else If @DataType = 'date' or @DataType = 'smalldatetime' or @DataType = 'datetime' or @DataType = 'datetime2'
							Set @DefaultValue = '''19000101'''
							
						Set @sql = @sql + ',ISNULL(b.'+@ColumnName+','+@DefaultValue+') '
					end
				end	
				Else
				Begin	-- Gán giá trị mặc định nếu chưa định nghĩa, Nếu định nghĩa đủ sẽ không chạy xuống tới đây
					If @DataType = 'numeric' or @DataType = 'tinyint' or @DataType = 'smallint' or @DataType = 'int' or @DataType = 'bigint'  or @DataType = 'money' or @DataType = 'bit' or @DataType = 'decimal'
						Set @sql = @sql + ',0 '
					Else If @DataType = 'char' or @DataType = 'nchar' or @DataType = 'varchar' or @DataType = 'nvarchar' or @DataType = 'nvarchar' or @DataType = 'text' or @DataType = 'ntext'
						Set @sql = @sql + ','''' '
					Else If @DataType = 'date' or @DataType = 'smalldatetime' or @DataType = 'datetime' or @DataType = 'datetime2'
						Set @sql = @sql + ',''19000101'' '
					Else
						Set @sql = @sql + ',null '	-- Nếu ở trên chưa đủ kiểu thì câu này có thể gây lỗi
					--Set @sql = @sql + ',''Chua co'' ' + @ColumnName + ':' + @DataType
				End
			End
			
			Fetch next from cur INTO @ColumnName, @DataType, @nullable
		END
		close cur
		deallocate cur
		Set @sql = @sql + ' From AM81 a join AD81 b on b.stt_rec = a.stt_rec'
		--Set @sql = @sql + ' left join ABvt13 c on c.ma_kho = b.ma_kho_i and c.ma_vt = b.ma_vt'
		Set @sql = @sql + ' left join Alkho k on k.ma_kho = b.ma_kho_i'
		Set @sql = @sql + ' Where a.stt_rec = ''' + @Stt_rec + ''''
		print @sql
		EXEC (@sql)
		
	END	 Print N'-- Kết thúc cập nhật ARI70 --'
END