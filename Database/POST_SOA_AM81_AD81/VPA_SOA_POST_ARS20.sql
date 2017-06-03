if exists (select 1 from sysobjects  where name = 'VPA_SOA_POST_ARS20')
	drop proc VPA_SOA_POST_ARS20
GO
print 'CREATE PROCEDURE [dbo].[VPA_SOA_POST_ARS20]'
GO
CREATE PROCEDURE VPA_SOA_POST_ARS20
	@Stt_rec varchar(13),
	@Ma_ct varchar(10),
	@Ma_nt varchar(10),
	@Ma_nx varchar(16),	
	@UserID int
AS
BEGIN
	--================== Khai bao mot so bien su dung chung =====================================
	DECLARE @sql varchar(4000), @M_Ma_nt0 varchar(10)--, @Nxt tinyint
	Declare @ColumnName varchar(50), @DataType nvarchar(50), @nullable varchar(3), @DefaultValue varchar(10)
	SET @sql = ''-- SET @Nxt = 2 --Xuat
	--SET cMa_nt_0
	Select @M_Ma_nt0 = val from V6option where name = 'M_Ma_Nt0'
	--==================================================================================================
		
		Declare @Tat_Toan numeric(1,0), @T_tt_qd numeric(15,2),
			@T_tt1 numeric(15,2),@T_tt_nt1 numeric(15,2),
			@Ngay_Tt varchar(10), @_Mode varchar(2)--, strSQL
			
		Set @Tat_Toan = 0
		Set @T_tt_qd = 0
		Set @T_tt1 = 0
		Set @T_tt_nt1 = 0
		Set @Ngay_Tt = '1900/01/01'
		Set @_Mode = 'M'
				
		--Print 'Delete If...Xoa loan ngoan... chua thong'
		--If (select tk_cn from ALtk where tk = @Ma_nx) = 0
		--Begin
		--	If @_Mode = 'S'--??????????????????????????????????????????????????????????????
		--		Delete ARS20 where stt_rec = @Stt_rec
		--End
		
		--Nếu đã có bản ghi cũ thì lấy các giá trị cũ lên rồi xóa dữ liệu cũ đi. ây chà chà
		IF Exists(select 1 from ARS20 where stt_rec = @Stt_rec)
		Begin
			Print 'IF Exists(select 1 from ARS20 where stt_rec = @Stt_rec)'
			Select top 1
				@Tat_Toan	= isnull(tat_toan,0),
				@T_tt_qd	= isnull(t_tt_qd,0),
				@T_tt1		= isnull(t_tt1,0),
				@T_tt_nt1	= isnull(t_tt_nt1,0),
				@Ngay_Tt	= CONVERT(VARCHAR(10), ngay_tt, 111),
				@_Mode		= 'S'
			from ARS20 where stt_rec = @Stt_rec
			
			DELETE FROM ARS20 WHERE Stt_rec = @Stt_rec
		End
		
		Declare @_Dk int, @m_Tk_tk_vt varchar(100)		
		Select @m_Tk_tk_vt = val from V6option where name = 'm_Tk_tk_vt'
		Set @_Dk = CHARINDEX(',' + Rtrim(@m_Tk_tk_vt) + ',', ',' + @Ma_nx+',',1)
				
		Declare @Tk char(16), @Ma_tt tinyint, @Ma_gd char(1), @T_tt_nt0 numeric(15,2), @T_tt0 numeric(15,2)
		
		Select top 1 
			 @Tk		= a.ma_nx
			,@Ma_tt		= 0
			,@Ma_gd		= '0'
			,@T_tt_nt0	= isnull(a.t_tt_nt,0)
			,@T_tt0		= isnull(a.t_tt,0)
			,@Tat_Toan = Case @_Mode When 'S' Then
							Case When  @Tat_Toan = 2 Then @Tat_Toan Else
								Case When @T_tt_qd < @T_tt_nt0 Then 0 Else 1 End
							End
						Else 0 End
			,@T_tt_qd	= Case When @_Dk > 0 Then @T_tt_nt0 Else @T_tt_qd End
			,@Tat_Toan	= Case When @_Dk > 0 Then 1			Else @Tat_Toan End
			,@Ngay_Tt	= Case When @_Dk > 0 Then CONVERT(VARCHAR(10), a.ngay_ct, 111) Else @Ngay_Tt End
		From AM81 a where stt_rec = @Stt_rec
		
		
		SET @sql = 'Insert into ARS20 Select Top 1 0 as Ma_tt' -- Sai tam Top 1
		
		Declare cur cursor for
			Select COLUMN_NAME, DATA_TYPE, IS_NULLABLE From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'ARS20'
			Order by ORDINAL_POSITION
		Open cur
		Fetch next from cur INTO @ColumnName, @DataType, @nullable
		while @@FETCH_STATUS = 0
		BEGIN
			If @ColumnName <> 'Ma_tt'
			Begin
					 If @ColumnName = 'Tk'		Set @sql = @sql + ',''' + @Tk + ''''
				--Else If @ColumnName = 'Ma_tt'	Set @sql = @sql + ',0'
				Else If @ColumnName = 'T_Tt_Nt0'Set @sql = @sql + ',' + CAST(@T_tt_nt0 as varchar(20))
				Else If @ColumnName = 'T_Tt0'	Set @sql = @sql + ',' + CAST(@T_tt0 as varchar(20))
								
				Else If @ColumnName = 'T_tt_qd' Set @sql = @sql + ',' + CAST(@T_tt_qd as varchar) + ' as T_tt_qd'
				--Else If @ColumnName = 'T_tt_qd' Set @sql = 'biloi kho hieu'
				Else If @ColumnName = 'T_tt1'	Set @sql = @sql + ',' + CAST(@T_tt1 as varchar(20))
				Else If @ColumnName = 'T_tt_nt1'Set @sql = @sql + ',' + CAST(@T_tt_nt1 as varchar(20))
				
				Else If @ColumnName = 'Ngay_Tt' Set @sql = @sql + ',''' + @Ngay_Tt + ''''
				Else If @ColumnName = 'Tat_Toan' Set @sql = @sql + ',' + CAST(@Tat_Toan as varchar)
				
				Else If @ColumnName = 'Ma_Gd'	Set @sql = @sql + ',''0'''
				Else If @ColumnName = 'Ma_kho'	Set @sql = @sql + ',b.Ma_kho_i'
				
				--IF m.Ma_nt = m_Ma_nt0 Store 0 to...
				Else If @ColumnName in('Ty_gia','T_tien_nt2','T_thue_nt','T_ck_nt','T_tt_nt','T_tt_nt0','Ma_nt')
					and @Ma_nt = @M_Ma_nt0		Set @sql = @sql + ',0'
					
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
							Set @DefaultValue = '''''' -- hai d?u nháy ??n
						Else If @DataType = 'date' or @DataType = 'smalldatetime' or @DataType = 'datetime' or @DataType = 'datetime2'
							Set @DefaultValue = '''19000101'''
							
						Set @sql = @sql + ',ISNULL(a.'+@ColumnName+','+@DefaultValue+')'
					end
				end
				-- N?u tr??ng có trong AD81 thì l?y luôn tr??ng ?ó
				Else If exists (Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'AD81' and COLUMN_NAME = @ColumnName)
				begin
					if @nullable = 'YES'
						Set @sql = @sql + ',b.' + @ColumnName
					else
					begin
						If @DataType = 'numeric' or @DataType = 'tinyint' or @DataType = 'smallint' or @DataType = 'int' or @DataType = 'bigint'  or @DataType = 'money' or @DataType = 'bit' or @DataType = 'decimal'
							Set @DefaultValue = '0'
						Else If @DataType = 'char' or @DataType = 'nchar' or @DataType = 'varchar' or @DataType = 'nvarchar' or @DataType = 'nvarchar' or @DataType = 'text' or @DataType = 'ntext'
							Set @DefaultValue = '''''' -- hai d?u nháy ??n
						Else If @DataType = 'date' or @DataType = 'smalldatetime' or @DataType = 'datetime' or @DataType = 'datetime2'
							Set @DefaultValue = '''19000101'''
							
						Set @sql = @sql + ',ISNULL(b.'+@ColumnName+','+@DefaultValue+')'
					end
				end
				-- Gán giá tr? m?c ??nh n?u ch?a ??nh ngh?a, N?u ??nh ngh?a ?? s? không ch?y xu?ng t?i ?ây	
				Else
				Begin	
					If @DataType = 'numeric' or @DataType = 'tinyint' or @DataType = 'smallint' or @DataType = 'int' or @DataType = 'bigint'  or @DataType = 'money' or @DataType = 'bit' or @DataType = 'decimal'
						Set @sql = @sql + ',0'
					Else If @DataType = 'char' or @DataType = 'nchar' or @DataType = 'varchar' or @DataType = 'nvarchar' or @DataType = 'nvarchar' or @DataType = 'text' or @DataType = 'ntext'
						Set @sql = @sql + ','''''
					Else If @DataType = 'date' or @DataType = 'smalldatetime' or @DataType = 'datetime' or @DataType = 'datetime2'
						Set @sql = @sql + ',''19000101'''
					Else
						Set @sql = @sql + ','''''	-- N?u ? trên ch?a ?? ki?u thì câu này có th? gây l?i
					--Set @sql = @sql + ',''Chua co'' ' + @ColumnName + ':' + @DataType
				End
			End
			--==========================
			--Print @ColumnName
			--Print 'sql: ' + @sql
			--==========================
			Fetch next from cur INTO @ColumnName, @DataType, @nullable
		END
		close cur
		deallocate cur
		Set @sql = @sql + ' From AM81 a join AD81 b on a.stt_rec = b.stt_rec' 
		--Set @sql = @sql + ' left join Alvt v on b.ma_vt = v.ma_vt'
		Set @sql = @sql + ' Where a.stt_rec = ''' + @Stt_rec + ''''
		Set @sql = @sql + ' Order by b.tien2 desc'
		Print @sql
		EXEC (@sql)
	END		Print 'Ket thuc cap nhap ARS20'