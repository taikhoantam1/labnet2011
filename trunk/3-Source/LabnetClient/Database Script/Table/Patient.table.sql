CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientNumber] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NULL,
	[Gender] [bit] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[District] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Ward] [nvarchar](50) NULL,
	[BirthDate] [nvarchar](50) NOT NULL,
	[Age] [nvarchar](4) NOT NULL,
	[Status] [int] NOT NULL,
	[Occupation] [nvarchar](100) NULL,
	[ChartNumber] [nvarchar](20) NULL,
	[MedicalInsurance] [nvarchar](20) NULL,
	[NationalId] [nvarchar](100) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](15) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


