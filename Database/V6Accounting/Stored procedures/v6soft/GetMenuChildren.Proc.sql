USE [V6Accounting]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	Gets children of the menu item with specified OID.
*/
CREATE PROCEDURE [v6soft].[GetMenuChildren]
	@OID int
AS
BEGIN
	SELECT *
	FROM [v6soft].[Menu] m
	WHERE m.ParentOID = @OID
	ORDER BY m.Position
END
GO


