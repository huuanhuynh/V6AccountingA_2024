-- Generate definition
EXEC dbo.GenerateModelDefinition 0, 'dbo.Alkh'
EXEC dbo.GenerateModelDefinition 1, 'dbo.ALnhkh'
EXEC v6soft.GenerateModelDefinition 2, 'v6soft.alNhanVien'
EXEC v6soft.GenerateModelDefinition 3, 'v6soft.alNhomNhanVien'
EXEC v6soft.GenerateModelDefinition 4, 'v6soft.alPhuong'
EXEC v6soft.GenerateModelDefinition 5, 'v6soft.alQuan'
EXEC v6soft.GenerateModelDefinition 6, 'v6soft.alTinh'
EXEC v6soft.GenerateModelDefinition 7, 'v6soft.alHinhThucThanhToan'
---

-- Add XML Mapping
DECLARE @xml XML
SET @xml =N'<mappings name="CustomerGroup">
  <map name="UID" label="UID" code="UID" />
  <map name="loai_nh" label="Type" code="Type" />
  <map name="ma_nh" label="GroupCode" code="GroupCode" />
  <map name="ten_nh" label="Name" code="Name" />
  <map name="ten_nh2" label="OtherName" code="OtherName" />
  <map name="status" label="Status" code="Status" />
  <!--Deprecated-->
  <map name="date0" label="Date0" code="Date0" />
  <map name="time0" label="Time0" code="Time0" />
  <map name="user_id0" label="UserId0" code="UserId0" />
  <map name="date2" label="Date2" code="Date2" />
  <map name="time2" label="Time2" code="Time2" />
  <map name="user_id2" label="UserId2" code="UserId2" />
  <map name="CHECK_SYNC" label="CheckSync" code="CheckSync" />
</mappings>'
UPDATE dbo.ModelDefinitions SET MappingXml=@xml WHERE OID=1
-------
select * from v6soft.ModelDefinitions where OID=5
EXEC v6soft.GetModelDefinitions


-- Create XML from table
DECLARE @myXsd XML
SET @myXsd = (
	SELECT TOP 0 * FROM v6soft.alTinh
	AS Fields 
	FOR XML AUTO, XMLSCHEMA('v6schema')
	)
SET @myXsd.modify('delete /xs:schema[1]/@targetNamespace')

SELECT @myXsd


-- Add field
ALTER TABLE v6soft.alNhanVien
    ADD
	[Ma2] [nvarchar](8) NULL,
	[Tuoi] [int] NULL
	
UPDATE v6soft.ModelDefinitions 
SET MappingXml.modify('
		insert <map name="NewCol" label="MyNewCol" />        
		as last into (/mappings)[1]
	')
WHERE OID = 6