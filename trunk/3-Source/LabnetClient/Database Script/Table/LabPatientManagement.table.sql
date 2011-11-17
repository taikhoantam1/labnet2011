CREATE TABLE [dbo].[LabPatientManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[TestIdentifierNumber] [nvarchar](7) NOT NULL,
	[Diagnosis] [nvarchar](500) NULL,
	[ReceivedDate] [datetime] NOT NULL,
	[EnteredDate] [datetime] NULL,
	[PaymentTypeId] [int] NULL,
	[PatientTypeId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[ProjectId] [int] NULL,
	[DepartmentId] [int] NULL,
	[Status] [int] NOT NULL,
	[OrderNumber] [int] NOT NULL,
 CONSTRAINT [PK_LabPatientManagement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LabPatientManagement] ADD  CONSTRAINT [DF_LabPatientManagement_Status]  DEFAULT ((1)) FOR [Status]
GO


