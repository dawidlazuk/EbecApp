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

INSERT [dbo].[Teams] ([Name],[Balance]) VALUES ('Testowy', 0.0);
INSERT [dbo].[Teams] ([Name],[Balance]) VALUES ('Best', -20.0);
INSERT [dbo].[Teams] ([Name],[Balance]) VALUES ('Ebecowcy', 10.0);
INSERT [dbo].[Teams] ([Name],[Balance]) VALUES ('Czwarty', 100.0);

INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Imie','Nazwisko',1);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Dawid','Bestowy',3);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Name','Nieznam',2);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Imie','Nazwisko',1);
INSERT [dbo].[Participants] ([Firstname],[Surname],[TeamId]) VALUES ('Olek','Etanolek',3);






