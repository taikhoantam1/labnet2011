ALTER TABLE [dbo].[PanelItem]  WITH CHECK ADD  CONSTRAINT [FK_PanelItem_Panel] FOREIGN KEY([PanelId])
REFERENCES [dbo].[Panel] ([Id])
GO

ALTER TABLE [dbo].[PanelItem] CHECK CONSTRAINT [FK_PanelItem_Panel]
GO

ALTER TABLE [dbo].[PanelItem]  WITH CHECK ADD  CONSTRAINT [FK_PanelItem_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO

ALTER TABLE [dbo].[PanelItem] CHECK CONSTRAINT [FK_PanelItem_Test]
GO

ALTER TABLE [dbo].[PanelItem] ADD  CONSTRAINT [DF_PanelItem_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


