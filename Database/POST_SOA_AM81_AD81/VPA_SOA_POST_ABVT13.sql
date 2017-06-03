if exists (select 1 from sysobjects  where name = 'VPA_SOA_POST_ABVT13')
	drop proc VPA_SOA_POST_ABVT13
GO
print 'CREATE PROCEDURE [dbo].[VPA_SOA_POST_ABVT13]'
GO
CREATE PROCEDURE VPA_SOA_POST_ABVT13
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
	
	Print(N'-- Xử lý trên ABVT13')
		-- Gọi proceduce VPA_DELSDVT13 [Không rõ tham số]
		EXEC VPA_DELSDVT13 -1, @Stt_rec
				
		--Ở đây có thể là vòng lặp qua AD81 where stt_rec = @Stt_rec
		
		Declare @makho varchar(8), @mavt varchar(16),@so_luong numeric(18,6)
		Print N'Tạo bảng tạm #t'
		Declare @nreturn numeric
			Create table #t	(nreturn numeric)
		
		Print N'Mở cursor:'
		Declare cur cursor for
			Select ma_kho_i,ma_vt,so_luong From AD81 Where stt_rec = @Stt_rec
		Open cur
		Fetch next from cur INTO @makho, @mavt, @so_luong
		While @@FETCH_STATUS = 0
		BEGIN
			Print 'Delete #t'
			delete #t
			Set @makho = RTRIM(@makho)
			Set @mavt = RTRIM(@mavt)
			insert into #t Execute VPA_isValidTwoCode 'ABVT13', 1, 'Ma_kho', 'ma_vt', @makho, @mavt, @makho, @mavt
			select @nreturn = nreturn from #t
			
			Print 'Execute VPA_isValidTwoCode ''ABVT13'', 1, ''Ma_kho'', ''ma_vt'', '''+@makho+''', '''+@mavt+''', '''+@makho+''', '''+@mavt+''' '
			Print '@nreturn: ' + cast(@nreturn as varchar(10))
			IF 0 = @nreturn	-- Neu da ton tai
			BEGIN
				--Không rõ ở đây số lượng tồn bị thay đổi ngay cả khi update.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
				--Cần xử lý trường so_luong khi thay đổi so_luong1 trên giao diện
				Print 'Cap nhap xuat '+ @mavt + ': ' + Cast(@so_luong as varchar(25)) + '.'
				UPDATE ABVT13 SET Ton13=Ton13-@so_luong, date2 = GETDATE(), time2 = CONVERT(VARCHAR(8), GETDATE(), 114), user_id2 = @UserID
				WHERE Ma_kho=@makho AND Ma_vt=@mavt
			END
			ELSE BEGIN	-- Chưa hiểu tồn = số lượng
				Print 'Ma_kho:'+@makho+' , Ma_vt:'+@mavt+''
				INSERT INTO ABVT13 (MA_KHO, MA_VT, TON13, DU13, DU_NT13, DATE0, TIME0, USER_ID0, STATUS, DATE2, TIME2, USER_ID2)
							Values (@makho, @mavt, @so_luong,0, 0,GETDATE(), CONVERT(VARCHAR(8), GETDATE(), 114), @UserID,
															'1',  GETDATE(), CONVERT(VARCHAR(8), GETDATE(), 114), @UserID)
					--from AD81 where stt_rec = @Stt_rec and ma_kho_i = @makho and ma_vt = @mavt
			END
		
			fetch next from cur INTO @makho, @mavt,@so_luong
		END
		Close cur
		Deallocate cur
		drop table #t
END