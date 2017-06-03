USE [V6Accounting]
GO
/****** Object:  StoredProcedure [v6soft].[GenerateModelDefinition]    Script Date: 04/22/2014 21:45:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
 * Creates XML definition for specified table name.
 */
CREATE PROCEDURE [v6soft].[GenerateModelDefinition]
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
			 FROM v6soft.ModelDefinitions md
			 WHERE md.Name = @modelName)
	   )
    BEGIN
	   UPDATE v6soft.ModelDefinitions 
	   SET DefinitionXml = (SELECT XmlDef FROM @xmlTable),
		  Name = @modelName
	   WHERE OID = @modelId
    END
    ELSE
    BEGIN
	   INSERT INTO v6soft.ModelDefinitions
		  SELECT @modelId AS OID, @modelName AS Name, XmlDef AS DefinitionXml, NULL AS MappingXml
		  FROM @xmlTable
    END
END
