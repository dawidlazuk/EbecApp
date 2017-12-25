CREATE PROCEDURE [dbo].[UpdateParticipant]
	@Id			INT,
	@Firstname	VARCHAR(20),
	@Surname	VARCHAR(20),
	@TeamId		INT
AS
BEGIN


	UPDATE [dbo].[Participants] SET
		Firstname = @Firstname,
		Surname = @Surname,
		TeamId = @TeamId
	WHERE Id = @Id;

END