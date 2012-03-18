USE [nhmamnnk_LabnetManager]
GO

/****** Object:  Table [dbo].[TestSectionCommission]    Script Date: 03/18/2012 22:31:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestSectionCommission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestSectionId] [int] NOT NULL,
	[PartnerId] [int] NOT NULL,
	[Cost] [decimal](18, 0) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TestSectionCommission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TestSectionCommission]  WITH CHECK ADD  CONSTRAINT [FK_TestSectionCommission_Partner] FOREIGN KEY([PartnerId])
REFERENCES [dbo].[Partner] ([Id])
GO

ALTER TABLE [dbo].[TestSectionCommission] CHECK CONSTRAINT [FK_TestSectionCommission_Partner]
GO

ALTER TABLE [dbo].[TestSectionCommission]  WITH CHECK ADD  CONSTRAINT [FK_TestSectionCommission_TestSection] FOREIGN KEY([TestSectionId])
REFERENCES [dbo].[TestSection] ([Id])
GO

ALTER TABLE [dbo].[TestSectionCommission] CHECK CONSTRAINT [FK_TestSectionCommission_TestSection]
GO


