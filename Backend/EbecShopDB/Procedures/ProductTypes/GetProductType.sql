CREATE PROCEDURE [dbo].[GetProductType]
	@Id		INT
AS
BEGIN

	SELECT	[Id],
			[ProductId],
			[Name],
			[Price],
			[Amount]
	FROM [dbo].[ProductTypes]
	WHERE Id = @Id;

	SELECT	product.Id,
			product.Name,
			product.Description,
			product.Image 
	FROM [dbo].[Products] product
	JOIN [dbo].[ProductTypes] productType ON productType.ProductId = product.Id
	WHERE productType.ProductId = @Id;
END