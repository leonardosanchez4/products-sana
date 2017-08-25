CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdOrder] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
	[QuantityProduct] [decimal](18, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NULL,
	[TotalPrice]  AS ([QuantityProduct]*[UnitPrice]),
 CONSTRAINT [PK__OrderDet__3214EC0745B282A4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

