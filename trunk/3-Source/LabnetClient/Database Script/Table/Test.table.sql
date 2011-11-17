CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[LowIndex] [float] NULL,
	[HighIndex] [float] NULL,
	[Unit] [nvarchar](50) NULL,
	[Range] [nvarchar](100) NOT NULL,
	[DepartmentId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[TestSectionId] [int] NOT NULL,
	[ResultType] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[Cost] [decimal](18, 0) NOT NULL,
	[IsBold] [bit] NOT NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Test] ADD  CONSTRAINT [DF_Test_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Test] ADD  CONSTRAINT [DF_Test_IsBold]  DEFAULT ((0)) FOR [IsBold]
GO


