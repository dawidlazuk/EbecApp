CREATE PROCEDURE [dbo].[GetTeam]
	@Id int
AS
BEGIN
	SELECT  [Id],
			[Name],
			[Balance],
			[BlockedBalance]			
	FROM [dbo].[Teams]
	WHERE Id = @Id;

	SELECT  [Id],
			[Firstname],
			[Surname],
			[TeamId]
	FROM [dbo].[Participants]
	WHERE TeamId = @Id;

END