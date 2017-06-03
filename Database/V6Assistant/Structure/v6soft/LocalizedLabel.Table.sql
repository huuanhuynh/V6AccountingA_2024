USE [V6Assistant]
GO
/****** Object:  Table [v6soft].[LocalizedLabel]    Script Date: 06/17/2014 08:25:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [v6soft].[LocalizedLabel](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[LanguageCode] [nvarchar](2) NOT NULL,
	[Label] [nvarchar](100) NOT NULL,
	[LocalizedString] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LocalizedLabel] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UN_LocalizedLabelLangCode] UNIQUE NONCLUSTERED 
(
	[LanguageCode] ASC,
	[Label] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Localized strings with length not greater than 100 characters' , @level0type=N'SCHEMA',@level0name=N'v6soft', @level1type=N'TABLE',@level1name=N'LocalizedLabel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A label is unique for each language' , @level0type=N'SCHEMA',@level0name=N'v6soft', @level1type=N'TABLE',@level1name=N'LocalizedLabel', @level2type=N'CONSTRAINT',@level2name=N'UN_LocalizedLabelLangCode'
GO
ALTER TABLE [v6soft].[LocalizedLabel]  WITH CHECK ADD  CONSTRAINT [FK_LocalizedLabel_Language] FOREIGN KEY([LanguageCode])
REFERENCES [v6soft].[Language] ([Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [v6soft].[LocalizedLabel] CHECK CONSTRAINT [FK_LocalizedLabel_Language]
GO
