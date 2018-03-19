CREATE PROCEDURE [dbo].[UpdateProductType]
	@Id			INT,
	@ProductId	INT,
	@Name		VARCHAR(40),
	@Price		MONEY,
	@Amount		DECIMAL
AS
BEGIN
	UPDATE [dbo].[ProductTypes] SET
		ProductId = @ProductId,
		Name = @Name,
		Price = @Price,
		Amount = @Amount
	WHERE Id = @Id;
END
