CREATE TABLE [dbo].[ProductXProductCategories]
(
	[IdProduct] INT NOT NULL PRIMARY KEY, 
    [IdCategory] INT NOT NULL, 
    CONSTRAINT [FK_ProductXProductCategories_Product] FOREIGN KEY (IdProduct) REFERENCES Products(Id), 
    CONSTRAINT [FK_ProductXProductCategories_ProductCategories] FOREIGN KEY (IdCategory) REFERENCES ProductsCategories(Id)
)
