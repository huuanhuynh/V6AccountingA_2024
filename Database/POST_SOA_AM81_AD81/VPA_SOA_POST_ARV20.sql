if exists (select 1 from sysobjects  where name = 'VPA_SOA_POST_ARV20')
	drop proc VPA_SOA_POST_ARV20
GO
print 'CREATE PROCEDURE [dbo].[VPA_SOA_POST_ARV20]'
GO
CREATE PROCEDURE VPA_SOA_POST_ARV20
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
	
		Print 'VPA_SOA_POST_ARV20'
	
		DELETE FROM ARV20 WHERE Stt_rec = @Stt_rec
		
		Set @sql = 'Insert into ARV20 Select Top 1 a.stt_rec' -- Sai tam Top 1
		-- L?y top 1 vì th?c ch?t ch? l?y thông tin t? AM nh?ng l?i có thêm thông tin
		-- v?t t? thu? (nôm na là v?t t? chính trong ch?ng t? -- lay theo gia tien2 cao nhat)
			
		Declare cur cursor for
			Select COLUMN_NAME, DATA_TYPE, IS_NULLABLE From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = 'ARV20'
			Order by ORDINAL_POSITION
		Open cur
		Fetch next from cur INTO @ColumnName, @DataType, @nullable
		while @@FETCH_STATUS = 0
		BEGIN
			If @ColumnName <> 'stt_rec'
			Begin--Các tr??ng ? ?ây v ......................... s? l?y t? v		(Có th? l?y t? a ho?c b)
				
				--*19/02/2009 Dac thu Viet Ha (2 dong)
					 If @ColumnName = 'T_Tien'		Set @sql = @sql + ',a.T_Tien2-a.T_ck'
				Else If @ColumnName = 'Tk_Du'		Set @sql = @sql + ',a.Tk_Thue_No'
				Else If @ColumnName = 'Ten_kh'		Set @sql = @sql + ',k.Ten_kh'
				Else If @ColumnName = 'Ma_kho'		Set @sql = @sql + ',b.Ma_kho_i'
				
				Else If @ColumnName = 'Ten_Vt'	Set @sql = @sql + ',Case isnull(a.ten_vtthue,'''') When''''Then v.ten_vt Else a.ten_vtthue End'
				Else If @ColumnName = 'Ghi_chu'		Set @sql = @sql + ','''''
				--IF (m.Ma_nt = m_Ma_nt0) OR EMPTY(m.Ma_nt)
				--	SQLReplace(nConnHandle, "ARV20", "Ma_nt, T_tien_nt2, T_Thue_nt", "''; 0; 0", "Stt_rec = '" + m.Stt_rec + "'")
				--ENDIF
				Else If @ColumnName in ('T_tien_nt2', 'T_Thue_nt') and (@Ma_nt = @M_Ma_nt0 or @Ma_nt = '')
						Set @sql = @sql + ',0'
				Else If @ColumnName in ('Ma_nt') and (@Ma_nt = @M_Ma_nt0 or @Ma_nt = '')
						Set @sql = @sql + ','''''
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
						Set @sql = @sql + ',null'	-- N?u ? trên ch?a ?? ki?u thì câu này có th? gây l?i
					--Set @sql = @sql + ',''Chua co'' ' + @ColumnName + ':' + @DataType
				End
			End
			
			
			Fetch next from cur INTO @ColumnName, @DataType, @nullable
		END
		close cur
		deallocate cur
		Set @sql = @sql + ' From AM81 a join AD81 b on a.stt_rec = b.stt_rec' 
		Set @sql = @sql + ' left join Alvt v on b.ma_vt = v.ma_vt'
		Set @sql = @sql + ' left join Alkh k on k.ma_kh = a.ma_kh'
		Set @sql = @sql + ' Where a.stt_rec = ''' + @Stt_rec + ''''
		Set @sql = @sql + ''
		Set @sql = @sql + ' Order by b.tien2 desc'
		print @sql
		EXEC (@sql)
	END		Print 'Ket thuc cap nhap ARV20'