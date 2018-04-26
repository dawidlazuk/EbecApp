CREATE PROCEDURE [dbo].[UpdateOrder]
	@Id		INT,
	@Status	INT,
	@TeamId INT
AS
BEGIN

	UPDATE [dbo].[Orders] SET
		Status = @Status,
		TeamId = @TeamId,
		ModifiedDate = GETDATE()
	WHERE Id = @Id;

END