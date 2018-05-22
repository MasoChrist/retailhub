

/****** Object:  Table [dbo].[Products]    Script Date: 22/05/2018 14:42:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Products](
	[ID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ShortDescription] [nvarchar](255) NOT NULL,
	[SKU] [nvarchar](255) NOT NULL
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO



IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PriceList_PriceList]') AND parent_object_id = OBJECT_ID(N'[dbo].[PriceList]'))
ALTER TABLE [dbo].[PriceList] DROP CONSTRAINT [FK_PriceList_PriceList]
GO



/****** Object:  Table [dbo].[PriceList]    Script Date: 22/05/2018 14:42:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PriceList](
	[ID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_PriceList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PriceList_PriceList]') AND parent_object_id = OBJECT_ID(N'[dbo].[PriceList]'))
ALTER TABLE [dbo].[PriceList]  WITH CHECK ADD  CONSTRAINT [FK_PriceList_PriceList] FOREIGN KEY([ID])
REFERENCES [dbo].[PriceList] ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PriceList_PriceList]') AND parent_object_id = OBJECT_ID(N'[dbo].[PriceList]'))
ALTER TABLE [dbo].[PriceList] CHECK CONSTRAINT [FK_PriceList_PriceList]
GO


/****** Object:  Table [dbo].[Price]    Script Date: 22/05/2018 14:41:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Price]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Price](
	[ProductID] [uniqueidentifier] NOT NULL,
	[PriceListID] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[Discount] [decimal](18, 4) NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Price_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Price_PriceList]') AND parent_object_id = OBJECT_ID(N'[dbo].[Price]'))
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_PriceList] FOREIGN KEY([PriceListID])
REFERENCES [dbo].[PriceList] ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Price_PriceList]') AND parent_object_id = OBJECT_ID(N'[dbo].[Price]'))
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_PriceList]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Price_Products]') AND parent_object_id = OBJECT_ID(N'[dbo].[Price]'))
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Price_Products]') AND parent_object_id = OBJECT_ID(N'[dbo].[Price]'))
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_Products]
GO


