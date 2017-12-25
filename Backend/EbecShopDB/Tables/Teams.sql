﻿CREATE TABLE [dbo].[Teams]
(
	[Id]		INT IDENTITY(1,1)	NOT NULL PRIMARY KEY,
	[Name]		VARCHAR(40)			NOT NULL,

	[Balance]	MONEY				NOT NULL, 
    [BlockedBalance] MONEY NOT NULL,
)
