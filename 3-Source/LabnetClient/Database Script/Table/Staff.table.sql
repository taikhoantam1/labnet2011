CREATE TABLE [dbo].[Staff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fisrtname] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NULL,
	[IdentifierNumber] [nvarchar](50) NULL,
	[Level] [nvarchar](100) NULL,
	[BirthDate] [date] NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](15) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


