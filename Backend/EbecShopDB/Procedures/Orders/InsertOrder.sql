CREATE PROCEDURE [dbo].[InsertOrder]
	@Id		INT OUTPUT,
	@Status	INT,
	@TeamId INT
AS
BEGIN

	INSERT INTO [dbo].[Orders] (
				Status,
				TeamId,
				CreatedDate,
				ModifiedDate
			) VALUES (
				@Status,
				@TeamId,
				GETDATE(),
				GETDATE()
			);

	SET @Id = CAST(SCOPE_IDENTITY() as int);
END