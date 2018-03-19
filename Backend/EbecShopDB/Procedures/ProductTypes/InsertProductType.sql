CREATE PROCEDURE [dbo].[InsertProductType]
	@Id			INT OUTPUT,
	@ProductId	INT,
	@Name		VARCHAR(40),
	@Price		MONEY,
	@Amount		DECIMAL
AS
BEGIN
	
	INSERT INTO [dbo].[ProductTypes] (
		ProductId,
		Name,
		Price,
		Amount
	) VALUES (
		@ProductId,
		@Name,
		@Price,
		@Amount
	);
	SET @Id = CAST(SCOPE_IDENTITY() as int);

END
