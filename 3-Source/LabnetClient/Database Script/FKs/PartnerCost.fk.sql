
ALTER TABLE [dbo].[PartnerCost]  WITH CHECK ADD  CONSTRAINT [FK_PartnerCost_Partner] FOREIGN KEY([PartnerId])
REFERENCES [dbo].[Partner] ([Id])
GO

ALTER TABLE [dbo].[PartnerCost] CHECK CONSTRAINT [FK_PartnerCost_Partner]
GO

ALTER TABLE [dbo].[PartnerCost]  WITH CHECK ADD  CONSTRAINT [FK_PartnerCost_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO

ALTER TABLE [dbo].[PartnerCost] CHECK CONSTRAINT [FK_PartnerCost_Test]
GO

ALTER TABLE [dbo].[PartnerCost] ADD  CONSTRAINT [DF_PartnerCost_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


