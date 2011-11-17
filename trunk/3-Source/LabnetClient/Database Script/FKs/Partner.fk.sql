ALTER TABLE [dbo].[Partner] ADD  CONSTRAINT [DF_Partner_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO