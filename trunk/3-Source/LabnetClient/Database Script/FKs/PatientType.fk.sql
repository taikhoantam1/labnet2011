ALTER TABLE [dbo].[PatientType] ADD  CONSTRAINT [DF_PatientType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
