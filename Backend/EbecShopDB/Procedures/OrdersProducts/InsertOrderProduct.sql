CREATE PROCEDURE [dbo].[InsertOrderProduct]
	@OrderId	INT,
	@ProductId	INT,
	@Amount		DECIMAL(18,0)
AS
BEGIN
	INSERT INTO [dbo].[OrdersProducts] (
				OrderId,
				ProductId,
				Amount
			) VALUES (
				@OrderId,
				@ProductId,
				@Amount
			);
END
