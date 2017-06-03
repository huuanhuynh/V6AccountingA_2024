USE [ThienHa14]
GO
/****** Object:  StoredProcedure [dbo].[GetMenuItems]    Script Date: 04/23/2015 19:15:17 ******/
DROP PROCEDURE [dbo].[GetMenuItems]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 * Gets all menu items
 */
CREATE PROCEDURE [dbo].[GetMenuItems]
AS
BEGIN
	SELECT *
	FROM [dbo].[Menu]	
	ORDER BY [ParentOID]
END
GO
