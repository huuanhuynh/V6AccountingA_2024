USE [V6Accounting]
GO
/****** Object:  Table [v6soft].[Menu]    Script Date: 12/05/2014 12:51:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[Menu](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](100) NOT NULL,
	[Icon] [nvarchar](30) NULL,
	[Description] [nvarchar](200) NULL,
	[Route] [nvarchar](200) NULL,
	[Position] [smallint] NOT NULL,
	[ParentOID] [int] NULL,
	[Metadata] [nvarchar](200) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UN_MenuLabel] UNIQUE NONCLUSTERED 
(
	[Label] ASC,
	[ParentOID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UN_MenuPosition] UNIQUE NONCLUSTERED 
(
	[ParentOID] ASC,
	[Position] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The position to appear in menu tree compared to a menu item''s siblings.' , @level0type=N'SCHEMA',@level0name=N'v6soft', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Position'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No label duplication in a menu level.' , @level0type=N'SCHEMA',@level0name=N'v6soft', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'CONSTRAINT',@level2name=N'UN_MenuLabel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No duplicated position in a menu level.' , @level0type=N'SCHEMA',@level0name=N'v6soft', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'CONSTRAINT',@level2name=N'UN_MenuPosition'
GO
ALTER TABLE [v6soft].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Menu] FOREIGN KEY([ParentOID])
REFERENCES [v6soft].[Menu] ([OID])
GO
ALTER TABLE [v6soft].[Menu] CHECK CONSTRAINT [FK_Menu_Menu]
GO
