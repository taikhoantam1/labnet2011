CREATE TABLE [dbo].[Partner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Owner] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](15) NULL,
	[Fax] [nvarchar](50) NULL,
	[LabId] [int] NULL,
	[Logo] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[BankAccountNumber] [char](50) NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



