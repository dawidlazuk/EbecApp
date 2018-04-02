CREATE PROCEDURE [dbo].[GetProductsOfOrder]
	@OrderId int
AS
BEGIN

	SELECT	ProductTypeId,
			Amount
	FROM OrdersProducts
	WHERE OrderId = @OrderId;

END