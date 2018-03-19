CREATE TABLE [dbo].[OrdersProducts]
(
	[OrderId] INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
	[ProductTypeId] INT NOT NULL FOREIGN KEY REFERENCES ProductTypes(Id), 
    [Amount] DECIMAL NULL
)
