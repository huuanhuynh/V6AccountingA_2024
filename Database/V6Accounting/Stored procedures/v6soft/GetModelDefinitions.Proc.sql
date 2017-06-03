USE [ThienHa14]
GO
/****** Object:  StoredProcedure [v6soft].[GetModelDefinitions]    Script Date: 04/19/2014 12:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
 * Gets all dynamic model definitions.
 */
CREATE PROCEDURE [v6soft].[GetModelDefinitions]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
	   OID,
	   Name, 
	   C.query('declare namespace xsd="http://www.w3.org/2001/XMLSchema";
	   //xsd:complexType') AS DefinitionXml,
	   MappingXml
    FROM v6soft.ModelDefinitions
	   CROSS APPLY DefinitionXml.nodes('
	   declare namespace xsd="http://www.w3.org/2001/XMLSchema";
	   /xsd:schema') as T(C)
END
