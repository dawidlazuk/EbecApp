﻿CREATE TABLE [dbo].[Participants]
(
	[Id]			INT IDENTITY(1,1)	NOT NULL PRIMARY KEY,
	[Firstname]		VARCHAR(20)			NOT NULL,
	[Surname]		VARCHAR(20)			NOT NULL,
	[TeamId]		INT					NOT NULL FOREIGN KEY REFERENCES Teams(Id)
)
