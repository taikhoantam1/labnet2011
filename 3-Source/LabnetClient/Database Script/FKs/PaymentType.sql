ALTER TABLE [dbo].[PaymentType] ADD  CONSTRAINT [DF_PaymentType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO