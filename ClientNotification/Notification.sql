IF  EXISTS (SELECT name FROM sys.schemas WHERE name = N'ClientNotification')
return
Exec ('CREATE SCHEMA ClientNotification')
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ClientNotification].[Clients]') AND type in (N'U'))
BEGIN
CREATE TABLE ClientNotification.[Clients](
	[ID] [uniqueidentifier] NOT NULL,
	[ClientName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END

GO





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ClientNotification].[NotificationQueue]') AND type in (N'U'))
BEGIN
CREATE TABLE ClientNotification.[NotificationQueue](
	[ID] [uniqueidentifier] NOT NULL,
	[CreatorID] [uniqueidentifier] NOT NULL,
	[NotificationDTOType] [nvarchar](255) NOT NULL,
	[NotificationDTOkey] [nvarchar](255) NOT NULL,
	[CreationDateTime] [datetime] NOT NULL,
	[OperationType] [int] NOT NULL,
 CONSTRAINT [PK_NotificationQueue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationQueue_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationQueue]'))
ALTER TABLE [ClientNotification].[NotificationQueue]  WITH CHECK ADD  CONSTRAINT FK_NotificationQueue_Clients FOREIGN KEY([CreatorID])
REFERENCES [ClientNotification].Clients ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationQueue_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationQueue]'))
ALTER TABLE [ClientNotification].NotificationQueue CHECK CONSTRAINT FK_NotificationQueue_Clients
GO




/****** Object:  Table [dbo].[NotificationToClient]    Script Date: 21/05/2018 13:18:06 ******/


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ClientNotification].[NotificationToClient]') AND type in (N'U'))
BEGIN
CREATE TABLE ClientNotification.[NotificationToClient](
	[ID] [uniqueidentifier] NOT NULL,
	[ClientID] [uniqueidentifier] NOT NULL,
	[NotificationID] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_NotificationToClient] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO



IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationToClient_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationToClient]'))
ALTER TABLE [ClientNotification].[NotificationToClient]  WITH CHECK ADD  CONSTRAINT [FK_NotificationToClient_Clients] FOREIGN KEY([ClientID])
REFERENCES [ClientNotification].[Clients] ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationToClient_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationToClient]'))
ALTER TABLE [ClientNotification].[NotificationToClient] CHECK CONSTRAINT [FK_NotificationToClient_Clients]
GO



IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationToClient_NotificationQueue]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationToClient]'))
ALTER TABLE [ClientNotification].[NotificationToClient]  WITH CHECK ADD  CONSTRAINT [FK_NotificationToClient_NotificationQueue] FOREIGN KEY([NotificationID])
REFERENCES [ClientNotification].[NotificationQueue] ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationToClient_NotificationQueue]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationToClient]'))
ALTER TABLE [ClientNotification].[NotificationToClient] CHECK CONSTRAINT [FK_NotificationToClient_NotificationQueue]
GO



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ClientNotification].[NotificationRules]') AND type in (N'U'))
BEGIN
CREATE TABLE ClientNotification.NotificationRules(
	[ID] [uniqueidentifier] NOT NULL,
	[NotificationDTOType] nVarchar(255)  NOT NULL,
	[ClientCreatorID] [uniqueidentifier] NOT NULL,
	[ClientReceiverID] [uniqueidentifier] NOT NULL
 CONSTRAINT [PK_NotificationRules] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationRulesClientCreatorID_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationRules]'))
ALTER TABLE [ClientNotification].NotificationRules  WITH CHECK ADD  CONSTRAINT FK_NotificationRulesClientCreatorID_Clients FOREIGN KEY([ClientCreatorID])
REFERENCES [ClientNotification].Clients ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationRulesClientCreatorID_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationRules]'))
ALTER TABLE [ClientNotification].NotificationRules CHECK CONSTRAINT FK_NotificationRulesClientCreatorID_Clients
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationRulesClientReceiverID_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationRules]'))
ALTER TABLE [ClientNotification].NotificationRules  WITH CHECK ADD  CONSTRAINT FK_NotificationRulesClientReceiverID_Clients FOREIGN KEY(ClientReceiverID)
REFERENCES [ClientNotification].Clients ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ClientNotification].[FK_NotificationRulesClientCreatorID_Clients]') AND parent_object_id = OBJECT_ID(N'[ClientNotification].[NotificationRules]'))
ALTER TABLE [ClientNotification].NotificationRules CHECK CONSTRAINT FK_NotificationRulesClientCreatorID_Clients
GO