CREATE TABLE [dbo].[Products]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] VARCHAR(40) NOT NULL,
	[Description] TEXT,
	[Image] IMAGE,

	[Price] MONEY NOT NULL,
	[Amount] DECIMAL NOT NULL	
)
