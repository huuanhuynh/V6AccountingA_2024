USE [ThienHa14]
GO
/****** Object:  StoredProcedure [dbo].[GetModelDefinitions]    Script Date: 04/23/2015 19:15:17 ******/
DROP PROCEDURE [dbo].[GetModelDefinitions]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 * Gets all dynamic model definitions.
 */
CREATE PROCEDURE [dbo].[GetModelDefinitions]
AS
BEGIN
    SET NOCOUNT ON;
    SET ARITHABORT ON 
    SELECT 
	   OID,
	   Name, 
	   C.query('declare namespace xsd="http://www.w3.org/2001/XMLSchema";
	   //xsd:complexType') AS DefinitionXml,
	   MappingXml
    FROM dbo.ModelDefinitions
	   CROSS APPLY DefinitionXml.nodes('
	   declare namespace xsd="http://www.w3.org/2001/XMLSchema";
	   /xsd:schema') as T(C)
END
GO
