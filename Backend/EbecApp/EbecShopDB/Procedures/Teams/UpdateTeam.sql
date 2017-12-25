CREATE PROCEDURE [dbo].[UpdateTeam]
	@Id			INT,
	@Name		VARCHAR(40),
	@Balance	MONEY
AS
BEGIN

	UPDATE [dbo].[Teams] SET
		Name	= @Name,
		Balance	= @Balance
	WHERE Id = @Id;

END