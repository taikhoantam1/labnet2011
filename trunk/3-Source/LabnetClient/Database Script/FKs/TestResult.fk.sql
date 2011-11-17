ALTER TABLE [dbo].[TestResult]  WITH CHECK ADD  CONSTRAINT [FK_TestResult_ResultDictionary] FOREIGN KEY([ResultDictionaryId])
REFERENCES [dbo].[ResultDictionary] ([Id])
GO

ALTER TABLE [dbo].[TestResult] CHECK CONSTRAINT [FK_TestResult_ResultDictionary]
GO

ALTER TABLE [dbo].[TestResult]  WITH CHECK ADD  CONSTRAINT [FK_TestResult_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO

ALTER TABLE [dbo].[TestResult] CHECK CONSTRAINT [FK_TestResult_Test]
GO
