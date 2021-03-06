USE [] --Your Database Name
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[CustomerID] [uniqueidentifier] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderLine](
	[OrderLineID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[OrdID] [uniqueidentifier] NOT NULL,
	[ItemName] [nvarchar](max) NOT NULL,
	[Cost] [money] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED 
(
	[OrderLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'99ba620b-960e-4df5-8aab-1e66d64e1aef', N'Patrick', N'Ruskowski')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'3f9f0e10-209c-400b-99ff-35a9d961688a', N'Jane', N'Doe')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'e1ede2ad-0df6-4dda-9eab-4300897e4fc6', N'Martin', N'Seashell')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'fa0fd43b-eeb1-469d-aba3-568b4a087606', N'Mary', N'Jane')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'4385968a-047b-44bb-bda5-5b1554caa219', N'John', N'Doe')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'c1463988-6e6b-41bb-9029-7716d0f6d46f', N'Dustin', N'Russell')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'0aea81ba-d44b-4c99-b202-7d2012e932be', N'John', N'Sivert')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName]) VALUES (N'8ef54af3-e39e-4de0-ae64-ba478f4e5e98', N'Tiim', N'Moscato')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'de50eb59-e9ca-447b-81cf-00f0a8ed5b55', N'99ba620b-960e-4df5-8aab-1e66d64e1aef', CAST(N'2021-12-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'7ca8bace-371c-4c33-9e74-1281c23acf35', N'fa0fd43b-eeb1-469d-aba3-568b4a087606', CAST(N'2005-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'885fd440-52d2-42eb-97dd-1c75770596aa', N'3f9f0e10-209c-400b-99ff-35a9d961688a', CAST(N'2021-05-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'b5a8350e-c1f2-4c54-bac3-5708c77280b1', N'8ef54af3-e39e-4de0-ae64-ba478f4e5e98', CAST(N'2021-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'bbbf42c3-a139-4e45-8648-7004c868aa11', N'e1ede2ad-0df6-4dda-9eab-4300897e4fc6', CAST(N'2020-11-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'b320d04d-cc5b-4b7c-8d5f-a004943f2a3a', N'4385968a-047b-44bb-bda5-5b1554caa219', CAST(N'2022-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'51be59fc-84a4-4a69-a9dd-dd51e9e76a5a', N'0aea81ba-d44b-4c99-b202-7d2012e932be', CAST(N'2021-11-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate]) VALUES (N'c760f857-ade9-43ea-86d9-fd38d7a41b96', N'c1463988-6e6b-41bb-9029-7716d0f6d46f', CAST(N'2023-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[OrderLine] ([OrderLineID], [OrdID], [ItemName], [Cost], [Quantity]) VALUES (N'68ce7b1e-26eb-4d53-9809-48ee2523eeb1', N'51be59fc-84a4-4a69-a9dd-dd51e9e76a5a', N'Hammer', 5.0000, 100)
INSERT [dbo].[OrderLine] ([OrderLineID], [OrdID], [ItemName], [Cost], [Quantity]) VALUES (N'4b66421d-0c6c-4799-b34f-665c40c435bf', N'bbbf42c3-a139-4e45-8648-7004c868aa11', N'Peanut Butter', 3.0000, 1000)
INSERT [dbo].[OrderLine] ([OrderLineID], [OrdID], [ItemName], [Cost], [Quantity]) VALUES (N'1a3d05e9-0ae6-4ac2-ba6d-aa5d9f6a4141', N'51be59fc-84a4-4a69-a9dd-dd51e9e76a5a', N'Muffler', 12.0000, 9)
INSERT [dbo].[OrderLine] ([OrderLineID], [OrdID], [ItemName], [Cost], [Quantity]) VALUES (N'6d68add7-2f20-423c-84b2-bc5a17863dc2', N'de50eb59-e9ca-447b-81cf-00f0a8ed5b55', N'Brake Pads', 22.0000, 5)
INSERT [dbo].[OrderLine] ([OrderLineID], [OrdID], [ItemName], [Cost], [Quantity]) VALUES (N'e348fce5-2deb-4abd-a7c9-c9bd32aed80a', N'b320d04d-cc5b-4b7c-8d5f-a004943f2a3a', N'Oil Pan', 15.0000, 20)
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CustID]  DEFAULT (newid()) FOR [CustID]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_OrderID]  DEFAULT (newid()) FOR [OrderID]
GO
ALTER TABLE [dbo].[OrderLine] ADD  CONSTRAINT [DF_OrderLine_OrderLineID]  DEFAULT (newid()) FOR [OrderLineID]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_Order] FOREIGN KEY([OrdID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_OrderLine_Order]
GO
