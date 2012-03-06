CREATE TABLE [dbo].[TestSection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Cost] [decimal](18,0) NULL,
	[SortOrder] [int] NULL
 CONSTRAINT [PK_TestSection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TestSection] ADD  CONSTRAINT [DF_TestSection_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


