ALTER TABLE [dbo].[Analysis]  WITH CHECK ADD  CONSTRAINT [FK_Analysis_PatientItem] FOREIGN KEY([PatientItemId])
REFERENCES [dbo].[PatientItem] ([Id])
GO

ALTER TABLE [dbo].[Analysis] CHECK CONSTRAINT [FK_Analysis_PatientItem]
GO

ALTER TABLE [dbo].[Analysis]  WITH CHECK ADD  CONSTRAINT [FK_Analysis_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO

ALTER TABLE [dbo].[Analysis] CHECK CONSTRAINT [FK_Analysis_Test]
GO
