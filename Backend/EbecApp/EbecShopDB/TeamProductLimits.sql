CREATE TABLE [dbo].[TeamProductLimits]
(
	[TeamId] INT FOREIGN KEY REFERENCES Teams(Id),
	[ProductId] INT FOREIGN KEY REFERENCES Products(Id),

	[Limit] DECIMAL
)
