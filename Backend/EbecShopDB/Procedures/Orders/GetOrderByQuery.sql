CREATE PROCEDURE [dbo].[GetOrderByQuery]
	@Id		INT = NULL,
	@Status INT = NULL,
	@TeamId INT = NULL
AS
BEGIN

	SELECT	[Id],
			[Status],
			[TeamId]
	FROM [dbo].[Orders]
	WHERE (@Id IS NULL OR Id = @Id)
	AND	  (@Status IS NULL OR Status = @Status)
	AND	  (@TeamId IS NULL OR TeamId = @TeamId);

END
