
ALTER TABLE [dbo].[PatientItem]  WITH CHECK ADD  CONSTRAINT [FK_PatientItem_LabPatientManagement] FOREIGN KEY([LabPatientManagementId])
REFERENCES [dbo].[LabPatientManagement] ([Id])
GO

ALTER TABLE [dbo].[PatientItem] CHECK CONSTRAINT [FK_PatientItem_LabPatientManagement]
GO

ALTER TABLE [dbo].[PatientItem]  WITH CHECK ADD  CONSTRAINT [FK_PatientItem_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[PatientItem] CHECK CONSTRAINT [FK_PatientItem_Patient]
GO

ALTER TABLE [dbo].[PatientItem]  WITH CHECK ADD  CONSTRAINT [FK_PatientItem_Source] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Source] ([Id])
GO

ALTER TABLE [dbo].[PatientItem] CHECK CONSTRAINT [FK_PatientItem_Source]
GO

ALTER TABLE [dbo].[PatientItem]  WITH CHECK ADD  CONSTRAINT [FK_PatientItem_TypeOfSample] FOREIGN KEY([TypeOfSample])
REFERENCES [dbo].[TypeOfSample] ([Id])
GO

ALTER TABLE [dbo].[PatientItem] CHECK CONSTRAINT [FK_PatientItem_TypeOfSample]
GO


