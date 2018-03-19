CREATE PROCEDURE [dbo].[GetProduct]
	@Id		INT
AS
BEGIN

	SELECT	Id,
			Name,
			Description,
			Image 
	FROM [dbo].[Products]
	WHERE Id = @Id;


	SELECT	[Id],
			[Name],
			[Price],
			[Amount]
	FROM [dbo].[ProductTypes]
	WHERE ProductId = @Id;

END