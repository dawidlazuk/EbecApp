CREATE PROCEDURE [dbo].[GetOrder]
	@Id		INT
AS
BEGIN

	SELECT	[Id],
			[Status]
			[TeamId]
	FROM [dbo].[Orders]
	WHERE Id = @Id;

	SELECT	[ProductTypeId],
			[Amount]
	FROM [dbo].[OrdersProducts]
	WHERE OrderId = @Id;

END
