CREATE PROCEDURE [dbo].[InsertTeam]
	@Id				INT OUTPUT,
	@Name			VARCHAR(40),
	@Balance		MONEY,
	@BlockedBalance MONEY
AS
BEGIN

	INSERT INTO [dbo].[Teams] (
				Name,
				Balance,
				BlockedBalance
			) VALUES (
				@Name,
				@Balance,
				@BlockedBalance
			);

	SET @Id = CAST(SCOPE_IDENTITY() as int);

END