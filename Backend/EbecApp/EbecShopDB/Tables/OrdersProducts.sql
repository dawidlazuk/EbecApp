﻿CREATE TABLE [dbo].[OrdersProducts]
(
	[OrderId] INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
	[ProductId] INT NOT NULL FOREIGN KEY REFERENCES Products(Id)
)
