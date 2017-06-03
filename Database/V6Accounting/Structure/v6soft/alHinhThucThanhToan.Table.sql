USE [V6Accounting]
GO
/****** Object:  Table [v6soft].[alHinhThucThanhToan]    Script Date: 12/05/2014 12:51:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[alHinhThucThanhToan](
	[UID] [uniqueidentifier] NOT NULL,
	[Ma] [nvarchar](2) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[TinhTrang] [bit] NOT NULL,
	[GhiChu] [nvarchar](255) NULL,
	[TenKhac] [nvarchar](50) NOT NULL,
	[HanThanhToan] [smallint] NULL,
 CONSTRAINT [PK_alHinhThucThanhToan] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UN_alHinhThucThanhToan_MaHttt] ON [v6soft].[alHinhThucThanhToan] 
(
	[Ma] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [v6soft].[alHinhThucThanhToan] ADD  CONSTRAINT [DF_alHinhThucThanhToan_UID]  DEFAULT (newsequentialid()) FOR [UID]
GO
ALTER TABLE [v6soft].[alHinhThucThanhToan] ADD  CONSTRAINT [DF_alHttt_status]  DEFAULT ((1)) FOR [TinhTrang]
GO
