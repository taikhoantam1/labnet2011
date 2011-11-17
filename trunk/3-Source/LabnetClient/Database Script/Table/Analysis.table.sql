CREATE TABLE [dbo].[Analysis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientItemId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CompletedDate] [datetime] NULL,
	[ReleasedDate] [datetime] NULL,
	[Note] [ntext] NULL,
 CONSTRAINT [PK_Analysis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

