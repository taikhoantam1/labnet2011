ALTER TABLE [dbo].[Analyte] ADD  CONSTRAINT [DF_Analyte_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO