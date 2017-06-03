USE [V6Accounting]
GO
/****** Object:  Table [v6soft].[PhanLoaiKhachHang]    Script Date: 12/05/2014 12:51:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[PhanLoaiKhachHang](
	[KhachHangUID] [uniqueidentifier] NOT NULL,
	[NhomUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PhanLoaiKhachHang] PRIMARY KEY CLUSTERED 
(
	[KhachHangUID] ASC,
	[NhomUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [v6soft].[PhanLoaiKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_PhanLoaiKhachHang_alKhachHang] FOREIGN KEY([KhachHangUID])
REFERENCES [v6soft].[alKhachHang] ([UID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [v6soft].[PhanLoaiKhachHang] CHECK CONSTRAINT [FK_PhanLoaiKhachHang_alKhachHang]
GO
ALTER TABLE [v6soft].[PhanLoaiKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_PhanLoaiKhachHang_alNhomKhachHang] FOREIGN KEY([NhomUID])
REFERENCES [v6soft].[alNhomKhachHang] ([UID])
GO
ALTER TABLE [v6soft].[PhanLoaiKhachHang] CHECK CONSTRAINT [FK_PhanLoaiKhachHang_alNhomKhachHang]
GO
