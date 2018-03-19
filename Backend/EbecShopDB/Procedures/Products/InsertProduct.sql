CREATE PROCEDURE [dbo].[InsertProduct]
	@Id				INT OUTPUT,
	@Name			VARCHAR(40),
	@Description	TEXT,
	@Image			IMAGE
AS
BEGIN

	INSERT INTO [dbo].[Products] (
		Name,
		Description,
		Image
	) VALUES (
		@Name,
		@Description,
		@Image
	);

	SET @Id = CAST(SCOPE_IDENTITY() as int);

END
