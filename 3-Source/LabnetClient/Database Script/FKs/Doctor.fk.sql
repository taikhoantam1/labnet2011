ALTER TABLE [dbo].[Doctor] ADD  CONSTRAINT [DF_Doctor_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO