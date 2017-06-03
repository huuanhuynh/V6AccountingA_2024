USE [ThienHa14]
GO
/****** Object:  StoredProcedure [dbo].[GetMenuChildren]    Script Date: 04/23/2015 19:15:17 ******/
DROP PROCEDURE [dbo].[GetMenuChildren]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Gets children of the menu item with specified OID.
*/
CREATE PROCEDURE [dbo].[GetMenuChildren]
	@OID int
AS
BEGIN
	SELECT *
	FROM [dbo].[Menu] m
	WHERE m.ParentOID = @OID
	ORDER BY m.Position
END
GO
