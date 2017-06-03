USE [V6Accounting]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 * Gets all menu items
 */
CREATE PROCEDURE [v6soft].[GetMenuItems]
AS
BEGIN
	SELECT *
	FROM [v6soft].[Menu]	
	ORDER BY [ParentOID]
END
GO
