CREATE PROCEDURE [dbo].[InsertTeam]
	@Id			INT OUTPUT,
	@Name		VARCHAR(40),
	@Balance	MONEY
AS
BEGIN

	INSERT INTO [dbo].[Teams] (
				Name,
				Balance
			) VALUES (
				@Name,
				@Balance
			);

	SET @Id = CAST(SCOPE_IDENTITY() as int);

END