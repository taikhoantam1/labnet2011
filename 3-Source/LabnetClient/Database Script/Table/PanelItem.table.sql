CREATE TABLE [dbo].[PanelItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PanelId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[TestName] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_PanelItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
