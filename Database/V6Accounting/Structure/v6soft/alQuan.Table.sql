USE [V6Accounting]
GO
/****** Object:  Table [v6soft].[alQuan]    Script Date: 12/05/2014 12:51:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[alQuan](
	[UID] [uniqueidentifier] NOT NULL,
	[Ma] [nvarchar](16) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[TinhTrang] [bit] NOT NULL,
	[GhiChu] [nvarchar](255) NULL,
	[alTinhUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_alQuan] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_alQuan_alTinhUID] ON [v6soft].[alQuan] 
(
	[alTinhUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UN_alQuan_MaQuan] ON [v6soft].[alQuan] 
(
	[Ma] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [v6soft].[alQuan]  WITH CHECK ADD  CONSTRAINT [FK_alQuan_alQuan] FOREIGN KEY([alTinhUID])
REFERENCES [v6soft].[alTinh] ([UID])
GO
ALTER TABLE [v6soft].[alQuan] CHECK CONSTRAINT [FK_alQuan_alQuan]
GO
ALTER TABLE [v6soft].[alQuan] ADD  CONSTRAINT [DF_alQuan_UID]  DEFAULT (newsequentialid()) FOR [UID]
GO
ALTER TABLE [v6soft].[alQuan] ADD  CONSTRAINT [DF_alQuan_status]  DEFAULT ((1)) FOR [TinhTrang]
GO
