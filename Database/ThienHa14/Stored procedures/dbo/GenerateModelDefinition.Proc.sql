USE [ThienHa14]
GO
/****** Object:  StoredProcedure [dbo].[GenerateModelDefinition]    Script Date: 04/23/2015 19:15:17 ******/
DROP PROCEDURE [dbo].[GenerateModelDefinition]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 * Creates XML definition for specified table name.
 */
CREATE PROCEDURE [dbo].[GenerateModelDefinition]
    @modelId smallint,
    @modelName varchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @query nvarchar(500);
    SET @query = N'SELECT TOP 0 * FROM ' + @modelName + N' AS Fields FOR XML AUTO, TYPE, XMLSCHEMA(''v6schema'')'
    
    DECLARE @xmlTable AS TABLE (XmlDef XML)  
    INSERT INTO @xmlTable 
	   EXECUTE  sp_executesql @query

    UPDATE @xmlTable 
    SET XmlDef.modify('delete /xs:schema[1]/@targetNamespace')
    
    IF (EXISTS (SELECT 1
			 FROM dbo.ModelDefinitions md
			 WHERE md.Name = @modelName)
	   )
    BEGIN
	   UPDATE dbo.ModelDefinitions 
	   SET DefinitionXml = (SELECT XmlDef FROM @xmlTable),
		  Name = @modelName
	   WHERE OID = @modelId
    END
    ELSE
    BEGIN
	   INSERT INTO dbo.ModelDefinitions
		  SELECT @modelId AS OID, @modelName AS Name, XmlDef AS DefinitionXml, NULL AS MappingXml
		  FROM @xmlTable
    END
END
GO
