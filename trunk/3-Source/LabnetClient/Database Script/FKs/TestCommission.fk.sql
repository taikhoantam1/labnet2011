
ALTER TABLE [dbo].[TestCommission]  WITH CHECK ADD  CONSTRAINT [FK_TestCommission_Doctor] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO

ALTER TABLE [dbo].[TestCommission] CHECK CONSTRAINT [FK_TestCommission_Doctor]
GO

ALTER TABLE [dbo].[TestCommission]  WITH CHECK ADD  CONSTRAINT [FK_TestCommission_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO

ALTER TABLE [dbo].[TestCommission] CHECK CONSTRAINT [FK_TestCommission_Test]
GO
