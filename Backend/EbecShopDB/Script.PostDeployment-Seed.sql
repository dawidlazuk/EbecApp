/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT [dbo].[Teams] ([Name],[Balance],[BlockedBalance]) VALUES ('Testowy', 100.0, 0.0);
INSERT [dbo].[Teams] ([Name],[Balance],[BlockedBalance]) VALUES ('Best', -20.0, 0.0);
INSERT [dbo].[Teams] ([Name],[Balance],[BlockedBalance]) VALUES ('Ebecowcy', 10.0, 0.0);
INSERT [dbo].[Teams] ([Name],[Balance],[BlockedBalance]) VALUES ('Czwarty', 100.0, 0.0);

INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Imie','Nazwisko',1);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Dawid','Bestowy',3);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Name','Nieznam',2);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Imie','Nazwisko',1);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Olek','Etanolek',3);

INSERT [dbo].[Products] ([Name],[Description]) VALUES ('Gwozdz','Taki sobie przedmiot');
INSERT [dbo].[Products] ([Name],[Description]) VALUES ('Mlotek','Bez mlota to nie robota');
INSERT [dbo].[Products] ([Name],[Description]) VALUES ('Deska','W coś trzeba gwoździe wbijać');

INSERT [dbo].[ProductTypes] ([Name],[ProductId],[Price],[Amount]) VALUES ('Dlugi', 1, 1.0, 100.0);
INSERT [dbo].[ProductTypes] ([Name],[ProductId],[Price],[Amount]) VALUES ('Krotki', 1, 3.0, 100.5);
INSERT [dbo].[ProductTypes] ([Name],[ProductId],[Price],[Amount]) VALUES ('Szeroka', 3, 2.0, 200);

INSERT [dbo].[TeamProductLimits] ([TeamId],[ProductTypeId],[Limit]) VALUES (1, 1, 100)
INSERT [dbo].[TeamProductLimits] ([TeamId],[ProductTypeId],[Limit]) VALUES (1, 2, 100)
INSERT [dbo].[TeamProductLimits] ([TeamId],[ProductTypeId],[Limit]) VALUES (1, 3, 100)





