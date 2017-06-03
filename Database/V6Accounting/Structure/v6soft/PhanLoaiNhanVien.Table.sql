USE [V6Accounting]
GO
/****** Object:  Table [v6soft].[PhanLoaiNhanVien]    Script Date: 12/05/2014 12:51:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[PhanLoaiNhanVien](
	[NhanVienUID] [uniqueidentifier] NOT NULL,
	[NhomUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PhanLoaiNhanVien] PRIMARY KEY CLUSTERED 
(
	[NhanVienUID] ASC,
	[NhomUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [v6soft].[PhanLoaiNhanVien]  WITH CHECK ADD  CONSTRAINT [FK_PhanLoaiNhanVien_alNhanVien] FOREIGN KEY([NhanVienUID])
REFERENCES [v6soft].[alNhanVien] ([UID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [v6soft].[PhanLoaiNhanVien] CHECK CONSTRAINT [FK_PhanLoaiNhanVien_alNhanVien]
GO
ALTER TABLE [v6soft].[PhanLoaiNhanVien]  WITH CHECK ADD  CONSTRAINT [FK_PhanLoaiNhanVien_alNhomNhanVien] FOREIGN KEY([NhomUID])
REFERENCES [v6soft].[alNhomNhanVien] ([UID])
GO
ALTER TABLE [v6soft].[PhanLoaiNhanVien] CHECK CONSTRAINT [FK_PhanLoaiNhanVien_alNhomNhanVien]
GO
