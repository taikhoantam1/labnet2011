

ALTER TABLE [dbo].[LabPatientManagement]  WITH CHECK ADD  CONSTRAINT [FK_LabPatientManagement_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[LabPatientManagement] CHECK CONSTRAINT [FK_LabPatientManagement_Department]
GO

ALTER TABLE [dbo].[LabPatientManagement]  WITH CHECK ADD  CONSTRAINT [FK_LabPatientManagement_PatientType] FOREIGN KEY([PatientTypeId])
REFERENCES [dbo].[PatientType] ([Id])
GO

ALTER TABLE [dbo].[LabPatientManagement] CHECK CONSTRAINT [FK_LabPatientManagement_PatientType]
GO

ALTER TABLE [dbo].[LabPatientManagement]  WITH CHECK ADD  CONSTRAINT [FK_LabPatientManagement_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO

ALTER TABLE [dbo].[LabPatientManagement] CHECK CONSTRAINT [FK_LabPatientManagement_Project]
GO