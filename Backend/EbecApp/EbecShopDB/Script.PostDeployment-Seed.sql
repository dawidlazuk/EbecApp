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

INSERT [dbo].[Teams] ([Id],[Name],[Balance]) VALUES (1, 'Testowy', 0.0);
INSERT [dbo].[Teams] ([Id],[Name],[Balance]) VALUES (2, 'Best', -20.0);
INSERT [dbo].[Teams] ([Id],[Name],[Balance]) VALUES (3, 'Ebecowcy', 10.0);

INSERT [dbo].[Participants] ([Id],[Firstname],[Surname],[TeamId]) VALUES (1,'Imie','Nazwisko',1);
INSERT [dbo].[Participants] ([Id],[Firstname],[Surname],[TeamId]) VALUES (2,'Dawid','Bestowy',3);
INSERT [dbo].[Participants] ([Id],[Firstname],[Surname],[TeamId]) VALUES (3,'Name','Nieznam',2);
INSERT [dbo].[Participants] ([Id],[Firstname],[Surname],[TeamId]) VALUES (4,'Imie','Nazwisko',1);
INSERT [dbo].[Participants] ([Id],[Firstname],[Surname],[TeamId]) VALUES (5,'Olek','Etanolek',3);






