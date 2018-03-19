CREATE TABLE [dbo].[TeamProductLimits]
(
	[TeamId] INT FOREIGN KEY REFERENCES Teams(Id),
	[ProductTypeId] INT FOREIGN KEY REFERENCES ProductTypes(Id),

	[Limit] DECIMAL
)
