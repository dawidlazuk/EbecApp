CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR(40) NOT NULL,
	[Description] TEXT,
	[Image] IMAGE,

	[Price] MONEY NOT NULL,
	[Amount] DECIMAL NOT NULL	
)
