CREATE PROCEDURE [dbo].[InsertProduct]
	@Id				INT OUTPUT,
	@Name			VARCHAR(40),
	@Description	TEXT,
	@Image			IMAGE,
	@Price			MONEY,
	@Amount			DECIMAL
AS
BEGIN

	INSERT INTO [dbo].[Products] (
		Name,
		Description,
		Image,
		Price,
		Amount
	) VALUES (
		@Name,
		@Description,
		@Image,
		@Price,
		@Amount
	);

	SET @Id = CAST(SCOPE_IDENTITY() as int);

END
