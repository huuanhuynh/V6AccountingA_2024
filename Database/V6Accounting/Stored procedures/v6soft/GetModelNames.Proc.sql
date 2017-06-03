USE [V6Accounting]
GO
/****** Object:  StoredProcedure [v6soft].[GetModelNames]    Script Date: 04/19/2014 12:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
 * Gets all dynamic model names.
 */
CREATE PROCEDURE [v6soft].[GetModelNames]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Name FROM v6soft.ModelDefinitions
END
