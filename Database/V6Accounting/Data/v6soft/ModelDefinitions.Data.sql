USE [ThienHa14]
GO
/****** Object:  Table [v6soft].[ModelDefinitions]    Script Date: 02/05/2015 20:11:30 ******/
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (0, N'v6soft.alKhachHang', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="16" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Ten" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="128" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TenKhac"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="128" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="HanThanhToan" type="sqltypes:tinyint" /><xsd:attribute name="MaSoThue"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="18" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="DiaChi"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="128" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="DienThoaiBan"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="16" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Fax"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="16" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Email"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="30" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Homepage"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="NguoiLienHe"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="128" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="DienThoaiDiDong"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="20" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="NguoiLienHeKhac"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="128" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="DienThoaiDiDongKhac"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="20" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="NganHang"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="128" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TaiKhoanNganHang"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="24" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TaiKhoanCongNo"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="24" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TienGioiHanCongNo"><xsd:simpleType><xsd:restriction base="sqltypes:numeric"><xsd:totalDigits value="16" /><xsd:fractionDigits value="2" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TienGioiHanHoaDon"><xsd:simpleType><xsd:restriction base="sqltypes:numeric"><xsd:totalDigits value="16" /><xsd:fractionDigits value="2" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="NgayGioiHan" type="sqltypes:smalldatetime" /><xsd:attribute name="LaKhachHang" type="sqltypes:bit" /><xsd:attribute name="LaNhaCungCap" type="sqltypes:bit" /><xsd:attribute name="LaNhanVien" type="sqltypes:bit" /><xsd:attribute name="alPhuongUID" type="sqltypes:uniqueidentifier" /><xsd:attribute name="alQuanUID" type="sqltypes:uniqueidentifier" /><xsd:attribute name="alTinhUID" type="sqltypes:uniqueidentifier" /><xsd:attribute name="alNhanVienUID" type="sqltypes:uniqueidentifier" /><xsd:attribute name="alHinhThucThanhToanUID" type="sqltypes:uniqueidentifier" /></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="Customer"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" label="Code" code="Code" /><map name="Ten" label="Name" code="Name" /><map name="TinhTrang" label="Status" code="Status" /><map name="GhiChu" label="Note" code="Note" /><map name="TenKhac" label="OtherName" code="OtherName" /><map name="HanThanhToan" label="Maturity" code="Maturity" /><map name="MaSoThue" label="TaxCode" code="TaxCode" /><map name="DiaChi" label="Address" code="Address" /><map name="DienThoaiBan" label="DeskPhone" code="DeskPhone" /><map name="Fax" label="Fax" code="Fax" /><map name="Email" label="Email" code="Email" /><map name="Homepage" label="Homepage" code="Homepage" /><map name="NguoiLienHe" label="Contact" code="Contact" /><map name="DienThoaiDiDong" label="Cellphone" code="Cellphone" /><map name="alPhuongUID" label="Ward" code="Ward" ref="4" /><map name="alQuanUID" label="District" code="District" ref="5" /><map name="alTinhUID" label="Province" code="Province" ref="6" /><map name="alNhanVienUID" label="Employee" code="Employee" ref="2" /><map name="alHinhThucThanhToanUID" label="PaymentMethod" code="PaymentMethod" ref="7" /></group><group label="Advanced"><map name="NguoiLienHeKhac" label="OtherContact" code="OtherContact" /><map name="DienThoaiDiDongKhac" label="OtherCellphone" code="OtherCellphone" /><map name="NganHang" label="Bank" code="Bank" /><map name="TaiKhoanNganHang" label="BankAccount" code="BankAccount" /><map name="TaiKhoanCongNo" label="LiabilitiesAccount" code="LiabilitiesAccount" /><map name="TienGioiHanCongNo" label="DebtLimit" code="DebtLimit" /><map name="TienGioiHanHoaDon" label="InvoiceLimit" code="InvoiceLimit" /><map name="NgayGioiHan" label="RestrictionDate" code="RestrictionDate" /><map name="LaKhachHang" label="IsCustomer" code="IsCustomer" /><map name="LaNhaCungCap" label="IsProvider" code="IsProvider" /><map name="LaNhanVien" label="IsEmployee" code="IsEmployee" /></group><group label="CustomerGroup"><link name="NhomKhachHang" label="CustomerGroup" multiple="True" cols="Code,Name,Note" get-api="~/Customer/GroupApi/GetGroupsByCustomerUid" post-api="~/Customer/GroupApi/AddGroupsToCustomerUid" /></group></mappings>')
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (1, N'v6soft.alNhomKhachHang', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="8" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Ten" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="CustomerGroup"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" label="Code" code="Code" /><map name="Ten" label="Name" code="Name" /></group><group label="Advanced"><map name="TinhTrang" label="Status" code="Status" /><map name="GhiChu" label="Note" code="Note" /></group></mappings>')
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (2, N'v6soft.alNhanVien', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="8" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="HoTen" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="HoTenKhac"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="HanThanhToan" type="sqltypes:smallint" /></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="Employee"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" code="Code" label="Code" /><map name="HoTen" code="FullName" label="FullName" /><map name="TinhTrang" code="Status" label="Status" /><map name="GhiChu" code="Note" label="Note" /><map name="HoTenKhac" code="OtherFullName" label="OtherFullName" /><map name="HanThanhToan" code="Maturity" label="Maturity" /></group></mappings>')
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (3, N'v6soft.alNhomNhanVien', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="8" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Ten" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="EmployeeGroup"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" code="Code" label="Code" /><map name="Ten" code="Name" label="Name" /><map name="TinhTrang" code="Status" label="Status" /><map name="GhiChu" code="Note" label="Note" /></group></mappings>')
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (4, N'v6soft.alPhuong', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="16" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Ten" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="alQuanUID" type="sqltypes:uniqueidentifier" /><xsd:attribute name="alTinhUID" type="sqltypes:uniqueidentifier" /></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="Ward"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" code="Code" label="Code" /><map name="Ten" code="Name" label="Name" /><map name="TinhTrang" code="Status" label="Status" /><map name="GhiChu" code="Note" label="Note" /><map name="alQuanUID" code="District" label="District" ref="5" /><map name="alTinhUID" code="Province" label="Province" ref="6" /></group></mappings>')
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (5, N'v6soft.alQuan', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="16" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Ten" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="alTinhUID" type="sqltypes:uniqueidentifier" use="required" /></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="District"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" code="Code" label="Code" /><map name="Ten" code="Name" label="Name" /><map name="TinhTrang" code="Status" label="Status" /><map name="GhiChu" code="Note" label="Note" /><map name="alTinhUID" code="Province" label="Province" ref="6" /></group></mappings>')
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (6, N'v6soft.alTinh', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="16" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Ten" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="Province"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" code="Code" label="Code" /><map name="Ten" code="Name" label="Name" /><map name="TinhTrang" code="Status" label="Status" /><map name="GhiChu" code="Note" label="Note" /></group></mappings>')
INSERT [v6soft].[ModelDefinitions] ([OID], [Name], [DefinitionXml], [MappingXml]) VALUES (7, N'v6soft.alHinhThucThanhToan', N'<xsd:schema xmlns:schema="v6schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="http://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified"><xsd:import namespace="http://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="http://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" /><xsd:element name="Fields"><xsd:complexType><xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" /><xsd:attribute name="Ma" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="2" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="Ten" use="required"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TinhTrang" type="sqltypes:bit" /><xsd:attribute name="GhiChu"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="255" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="TenKhac"><xsd:simpleType><xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52"><xsd:maxLength value="50" /></xsd:restriction></xsd:simpleType></xsd:attribute><xsd:attribute name="HanThanhToan" type="sqltypes:smallint" /></xsd:complexType></xsd:element></xsd:schema>', N'<mappings name="PaymentMethod"><group label="Basic"><map name="UID" code="UID" /><map name="Ma" code="Code" label="Code" /><map name="Ten" code="Name" label="Name" /><map name="TinhTrang" code="Status" label="Status" /><map name="GhiChu" code="Note" label="Note" /><map name="TenKhac" code="OtherName" label="OtherName" /><map name="HanThanhToan" code="Maturity" label="Maturity" /></group></mappings>')
