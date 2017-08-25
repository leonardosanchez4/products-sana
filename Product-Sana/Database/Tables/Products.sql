CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[SKU] varchar(50),
	[ProductName] varchar(200),
	[ProductDescription] varchar(MAX),
	[CurrentUnitPrice] decimal(18,2)
)
