CREATE PROCEDURE [dbo].[InsertParticipant]
	@Id			INT OUTPUT,
	@Firstname	VARCHAR(20),
	@Surname	VARCHAR(20),
	@TeamId		INT
AS
BEGIN

	INSERT INTO [dbo].Participants(
				Firstname,
				Surname,
				TeamId
			) VALUES (
				@Firstname,
				@Surname,
				@TeamId
			);

	SET @Id = CAST(SCOPE_IDENTITY() as int);

END