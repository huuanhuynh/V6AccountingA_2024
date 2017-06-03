USE [V6Accounting]
GO
/****** Object:  Table [v6soft].[alKhachHang]    Script Date: 12/05/2014 12:51:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[alKhachHang](
	[UID] [uniqueidentifier] NOT NULL,
	[Ma] [nvarchar](16) NOT NULL,
	[Ten] [nvarchar](128) NOT NULL,
	[TinhTrang] [bit] NOT NULL,
	[GhiChu] [nvarchar](255) NULL,
	[TenKhac] [nvarchar](128) NULL,
	[HanThanhToan] [tinyint] NULL,
	[MaSoThue] [nvarchar](18) NULL,
	[DiaChi] [nvarchar](128) NULL,
	[DienThoaiBan] [nvarchar](16) NULL,
	[Fax] [nvarchar](16) NULL,
	[Email] [nvarchar](30) NULL,
	[Homepage] [nvarchar](50) NULL,
	[NguoiLienHe] [nvarchar](128) NULL,
	[DienThoaiDiDong] [nvarchar](20) NULL,
	[NguoiLienHeKhac] [nvarchar](128) NULL,
	[DienThoaiDiDongKhac] [nvarchar](20) NULL,
	[NganHang] [nvarchar](128) NULL,
	[TaiKhoanNganHang] [nvarchar](24) NULL,
	[TaiKhoanCongNo] [nvarchar](24) NULL,
	[TienGioiHanCongNo] [numeric](16, 2) NULL,
	[TienGioiHanHoaDon] [numeric](16, 2) NULL,
	[NgayGioiHan] [smalldatetime] NULL,
	[LaKhachHang] [bit] NULL,
	[LaNhaCungCap] [bit] NULL,
	[LaNhanVien] [bit] NULL,
	[alPhuongUID] [uniqueidentifier] NULL,
	[alQuanUID] [uniqueidentifier] NULL,
	[alTinhUID] [uniqueidentifier] NULL,
	[alNhanVienUID] [uniqueidentifier] NULL,
	[alHinhThucThanhToanUID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_alKhachHang] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_alKhachHang_alHinhThucThanhToanUID] ON [v6soft].[alKhachHang] 
(
	[alHinhThucThanhToanUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_alKhachHang_alNhanVienUID] ON [v6soft].[alKhachHang] 
(
	[alNhanVienUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_alKhachHang_alPhuongUID] ON [v6soft].[alKhachHang] 
(
	[alPhuongUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_alKhachHang_alQuanUID] ON [v6soft].[alKhachHang] 
(
	[alQuanUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_alKhachHang_alTinhUID] ON [v6soft].[alKhachHang] 
(
	[alTinhUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UN_alKhachHang_MaKH] ON [v6soft].[alKhachHang] 
(
	[Ma] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [v6soft].[alKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_alKhachHang_alHinhThucThanhToan] FOREIGN KEY([alHinhThucThanhToanUID])
REFERENCES [v6soft].[alHinhThucThanhToan] ([UID])
GO
ALTER TABLE [v6soft].[alKhachHang] CHECK CONSTRAINT [FK_alKhachHang_alHinhThucThanhToan]
GO
ALTER TABLE [v6soft].[alKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_alKhachHang_alNhanVien] FOREIGN KEY([alNhanVienUID])
REFERENCES [v6soft].[alNhanVien] ([UID])
GO
ALTER TABLE [v6soft].[alKhachHang] CHECK CONSTRAINT [FK_alKhachHang_alNhanVien]
GO
ALTER TABLE [v6soft].[alKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_alKhachHang_alPhuong] FOREIGN KEY([alPhuongUID])
REFERENCES [v6soft].[alPhuong] ([UID])
GO
ALTER TABLE [v6soft].[alKhachHang] CHECK CONSTRAINT [FK_alKhachHang_alPhuong]
GO
ALTER TABLE [v6soft].[alKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_alKhachHang_alQuan] FOREIGN KEY([alQuanUID])
REFERENCES [v6soft].[alQuan] ([UID])
GO
ALTER TABLE [v6soft].[alKhachHang] CHECK CONSTRAINT [FK_alKhachHang_alQuan]
GO
ALTER TABLE [v6soft].[alKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_alKhachHang_alTinh] FOREIGN KEY([alTinhUID])
REFERENCES [v6soft].[alTinh] ([UID])
GO
ALTER TABLE [v6soft].[alKhachHang] CHECK CONSTRAINT [FK_alKhachHang_alTinh]
GO
ALTER TABLE [v6soft].[alKhachHang] ADD  CONSTRAINT [DF_alKhachHang_SUID]  DEFAULT (newsequentialid()) FOR [UID]
GO
ALTER TABLE [v6soft].[alKhachHang] ADD  CONSTRAINT [DF_alKhachHang_TinhTrang]  DEFAULT ((1)) FOR [TinhTrang]
GO
