CREATE PROCEDURE [dbo].[InsertOrderProduct]
	@OrderId	INT,
	@ProductTypeId	INT,
	@Amount		DECIMAL(18,0)
AS
BEGIN
	INSERT INTO [dbo].[OrdersProducts] (
				OrderId,
				ProductTypeId,
				Amount
			) VALUES (
				@OrderId,
				@ProductTypeId,
				@Amount
			);
END
