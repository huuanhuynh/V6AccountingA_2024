USE [ThienHa14]
GO
/****** Object:  StoredProcedure [dbo].[GetModelNames]    Script Date: 04/23/2015 19:15:17 ******/
DROP PROCEDURE [dbo].[GetModelNames]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 * Gets all dynamic model names.
 */
CREATE PROCEDURE [dbo].[GetModelNames]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Name FROM dbo.ModelDefinitions
END
GO
