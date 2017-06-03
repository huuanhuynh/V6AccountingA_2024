IF EXISTS(select 1 from sysobjects  where name = 'SP_AUTHENTICATE')
	drop proc SP_AUTHENTICATE
GO
CREATE PROCEDURE SP_AUTHENTICATE
	@Username varchar(50),
	@Password varchar(50),
	@ResponseMessage nvarchar(250) = '' OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId INT
	IF EXISTS (SELECT TOP 1 user_id FROM V6User WHERE user_name = @Username)
	BEGIN
		SET @userId=(SELECT [user_id] FROM [V6User] WHERE user_name = @Username 
				AND PasswordHash=HASHBYTES('SHA2_512', @Password + CAST(Salt AS NVARCHAR(36))))
       IF(@userId IS NULL)
           SET @responseMessage='Incorrect password'
       ELSE
	   BEGIN 
           SET @responseMessage='User successfully logged in'
		   SELECT * FROM V6User WHERE user_id = @userId
	   END
    END
    ELSE
       SET @responseMessage='Invalid login'
END

/*==================HOW TO USE==================
	
	DECLARE	@responseMessage1 nvarchar(250)
	Exec SP_AUTHENTICATE 'tmtri', '123456', @ResponseMessage = @responseMessage1 OUTPUT
	SELECT	@responseMessage1 as N'@responseMessage'

*==============================================*/
