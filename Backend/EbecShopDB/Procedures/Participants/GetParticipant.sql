CREATE PROCEDURE [dbo].[GetParticipant]
	@Id int
AS
BEGIN

	SELECT	[Id],
			[Firstname],
			[Surname],
			[TeamId]
	FROM [dbo].[Participants]
	WHERE Id = @Id;

END
