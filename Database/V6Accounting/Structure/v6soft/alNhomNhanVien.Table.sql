USE [V6Accounting]
GO
/****** Object:  Table [v6soft].[alNhomNhanVien]    Script Date: 12/05/2014 12:51:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[alNhomNhanVien](
	[UID] [uniqueidentifier] NOT NULL,
	[Ma] [nvarchar](8) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[TinhTrang] [bit] NOT NULL,
	[GhiChu] [nvarchar](255) NULL,
 CONSTRAINT [PK_alNhomNhanVien] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UN_alNhomNhanVien_MaNhom] ON [v6soft].[alNhomNhanVien] 
(
	[Ma] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [v6soft].[alNhomNhanVien] ADD  CONSTRAINT [DF_alNhomNhanVien_UID]  DEFAULT (newsequentialid()) FOR [UID]
GO
ALTER TABLE [v6soft].[alNhomNhanVien] ADD  CONSTRAINT [DF_alNhomNhanVien_status]  DEFAULT ((1)) FOR [TinhTrang]
GO
