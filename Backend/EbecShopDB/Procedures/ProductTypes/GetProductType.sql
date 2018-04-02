CREATE PROCEDURE [dbo].[GetProductType]
	@Id		INT
AS
BEGIN
	DECLARE @ProductId INT;

	SELECT	[Id],
			[ProductId],
			[Name],
			[Price],
			[Amount]
	FROM [dbo].[ProductTypes]
	WHERE Id = @Id;

	SET @ProductId = (SELECT ProductId FROM dbo.ProductTypes WHERE Id = @Id);

	SELECT	Id,
			Name,
			Description,
			Image 
	FROM [dbo].[Products]
	WHERE Id = @ProductId;
END