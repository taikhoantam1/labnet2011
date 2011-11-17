
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_TestSection] FOREIGN KEY([TestSectionId])
REFERENCES [dbo].[TestSection] ([Id])
GO

ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_TestSection]
GO