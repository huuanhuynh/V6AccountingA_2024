if exists (select 1 from sysobjects  where name = 'VPA_SOA_POST_MAIN')
	drop proc VPA_SOA_POST_MAIN
GO
print 'CREATE PROCEDURE [dbo].[VPA_SOA_POST_MAIN]'
GO
CREATE PROCEDURE VPA_SOA_POST_MAIN
	@Stt_rec varchar(13),
	@Ma_ct varchar(10),
	@Ma_nt varchar(10),
	@Ma_nx varchar(16),
	@Loai_ck char(1),		
	@nRows INT,
	@nKieuPost INT,-- KIEU_POST
	@UserID int
	/*
		Định nghĩa giá trị trả về:
		true: Thành công mỹ mãn
		e-nr: Lỗi số dòng trong bảng tạm chưa đúng
		error: Lỗi trong quá trình thực thi lệnh trong Transaction
	*/
AS
BEGIN

		
		
Print('Bat dau Transaction')
SET XACT_ABORT ON
BEGIN TRAN
	BEGIN TRY
		--Bắt đầu các lệnh trong Transaction
		--Dữ liệu mới được lưu tạm trong 2 bảng #AM81TMP và #AD81TMP <<<<<<<<<<<<<<<<<<<<<
		--Các hành động trong Transaction nếu sảy ra lỗi thì
									-- coi như chưa xảy ra.
	
	-- Post order:
		--1. Post to AM81  - key stt_rec
		--2. Post to AD81  - key stt_rec
		--3. Post to ABVT13  - key stt_rec
		--4. Post to ARI70  - key stt_rec
		--5. Post to ARV20  - key stt_rec
		--6. Post to ARS20  - key stt_rec
		--7. Post to ARA00  - key stt_rec
		

	   --{ Test: insert AM81 va AD81 tai day .
	   --- POST AM81, AD81  Tai day
		--}
		
		SELECT * INTO #AM81Tmp FROM AM81 WHERE stt_rec = @Stt_rec  
		SELECT * INTO #AD81TMP FROm AD81 WHERE stt_rec = @Stt_rec 
	 /*
		Declare @nRowCount int;
		Set @nRowCount = -1;
		Select @nRowCount = COUNT(1) from #AD81TMP where stt_rec = @Stt_rec
		
		If @nRowCount <> @nRows
		Begin
			Select 'e-nr' as [Result], 'Co loi trong qua trinh truyen du lieu. Xin vui long thuc hien lai' as [Message]
			-- Ở đây cũng có thể lỗi do nhiều người mở cùng một chứng từ (chua xu ly van de nay - tuy nhien it gap)...
			Return
		end
	*/
	
				
	--=================== ABVT13 =======================================================================
		EXEC VPA_SOA_POST_ABVT13    @Stt_rec,@Ma_ct,@Ma_nt,@Ma_nx,@UserID
		
	--==================================================================================================
	--====================================== ARI70 =====================================================
		If @nKieuPost = 1 or @nKieuPost = 2
			EXEC VPA_SOA_POST_ARI70    @Stt_rec,@Ma_ct,@Ma_nt,@Ma_nx,@UserID
		Else
			DELETE FROM ARI70 WHERE Stt_rec = @Stt_rec
	
	--==================================================================================================
	--====================================== ARV20 =====================================================
		Print('VPA_SOA_POST_ARV20...')
		If @nKieuPost = 1 or @nKieuPost = 2
			EXEC VPA_SOA_POST_ARV20    @Stt_rec,@Ma_ct,@Ma_nt,@Ma_nx,@UserID
		Else
			DELETE FROM ARV20 WHERE Stt_rec = @Stt_rec
	
	--==================================================================================================
	--====================================== ARS20 =====================================================
		-- Lam theo cach ARI70 ??? ... cho no nhanh.
		Print('VPA_SOA_POST_ARS20...')
		If @nKieuPost = 1 or @nKieuPost = 2
			EXEC VPA_SOA_POST_ARS20    @Stt_rec,@Ma_ct,@Ma_nt,@Ma_nx,@UserID
	
	--==================================================================================================
	--====================================== ARA00 =====================================================
		Print 'VPA_SOA_POST_ARA00...'
		If @nKieuPost = 1
			EXEC VPA_SOA_POST_ARA00    @Stt_rec,@Ma_ct,@Ma_nt,@Ma_nx,@Loai_ck,@UserID
		Else
			DELETE FROM ARA00 WHERE Stt_rec = @Stt_rec
	
	--==================================================================================================
	--==================================================================================================
		Print 'Ket thuc POST thanh cong'
		Select 'success' as [Result], 'Post successfull' as [Message]
	--==================================================================================================
	COMMIT		--Kết thúc các lệnh trong Transaction
	END TRY
	BEGIN CATCH -- Net co loi say ra
	
	   ROLLBACK	-- Huy bo het cac thao tac trong Transaction
	   Print 'ROLLBACK	-- Huy bo het cac thao tac trong Transaction'
	   
	   DECLARE @ErrorMessage NVARCHAR(2000)
	   SELECT @ErrorMessage = 'Loi tai dong: '+ CONVERT(varchar, ERROR_LINE()) + --(Ký tự xuống dòng)
'
' +ERROR_MESSAGE()
	   
	   Print 'Error: ' + @ErrorMessage
	   Select 'error' as [Result], @ErrorMessage as [Message]
	   --RAISERROR(@ErrorMessage, 16, 1)
	END CATCH
	
	--Print('Xoa du lieu trong bang tam.')        -- Tạm bỏ đi để debug. bỏ đi vẫn chạy được nhưnng 
	--Delete #AD81TMP where stt_rec = @Stt_rec			sẽ tạo rác do không xóa tạm sau khi xong
	--Delete #AM81TMP where stt_rec = @Stt_rec			(mục đích: gọi Proc từ sql để debug)
	
--END TRAN without END keyword, TRAN end at COMMIT
END--Proc

--Đoạn lệnh trên kết hợp transaction với xử lý lỗi.
--- Nó bắt đầu bằng việc đặt lựa chọn XACT_ABORT là ON để đảm bảo transaction hoạt động đúng như mong muốn.
--- Sau đó là BEGIN TRAN để mở transaction.
--- Tiếp đến là BEGIN TRY để mở ra khối try block (giống như try catch trong C#)
--- Khối try block sẽ chứa các lệnh cần thực hiện trong transaction
--- Rồi đến COMMIT để kết thúc transaction và END TRY để kết thúc khối try block
--- Sau đó là BEGIN CATCH (giống như catch block trong C#). Đây là phần chứa đoạn lệnh sẽ được thực hiện khi có lỗi trong phần try block.
--- Trong phần catch lệnh đầu tiên là ROLLBACK để quay lui transaction.
--- Sau đó dùng một biến để chứa thông báo lỗi. Bạn cũng có thể thêm các bước như lưu thông tin về lỗi vào một bảng audit, hoặc gửi email cho DBA…
--- Kết thúc là RAISERROR để báo cho ứng dụng biết thủ tục đã gây ra lỗi và truyền thông báo lỗi cho ứng dụng.

--Phiên bản áp dụng: SQL Server 2005 trở lên