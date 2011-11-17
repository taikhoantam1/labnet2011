ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Analysis] FOREIGN KEY([AnalysisId])
REFERENCES [dbo].[Analysis] ([Id])
GO

ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [FK_Result_Analysis]
GO

ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Analyte] FOREIGN KEY([AnalyteId])
REFERENCES [dbo].[Analyte] ([Id])
GO

ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [FK_Result_Analyte]
GO