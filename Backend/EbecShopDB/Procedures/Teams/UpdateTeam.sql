CREATE PROCEDURE [dbo].[UpdateTeam]
	@Id			INT,
	@Name		VARCHAR(40),
	@Balance	MONEY,
	@BlockedBalance MONEY

AS
BEGIN

	UPDATE [dbo].[Teams] SET
		Name			= @Name,
		Balance			= @Balance,
		BlockedBalance	= @BlockedBalance
	WHERE Id = @Id;

END