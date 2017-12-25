CREATE PROCEDURE [dbo].[UpdateProduct]
	@Id				INT,
	@Name			VARCHAR(40),
	@Description	TEXT,
	@Image			IMAGE,
	@Price			MONEY,
	@Amount			DECIMAL
AS
BEGIN

	UPDATE [dbo].[Products] SET
		Name = @Name,
		Description = @Description,
		Image = @Image,
		Price = @Price,
		Amount = @Amount
	WHERE Id = @Id

END
