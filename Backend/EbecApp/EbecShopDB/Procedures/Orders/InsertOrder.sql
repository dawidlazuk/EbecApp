CREATE PROCEDURE [dbo].[InsertOrder]
	@Id		INT OUTPUT,
	@Status	INT,
	@TeamId INT
AS
BEGIN

	INSERT INTO [dbo].[Orders] (
				Status,
				TeamId
			) VALUES (
				@Status,
				@TeamId
			);

	SET @Id = CAST(SCOPE_IDENTITY() as int);
END