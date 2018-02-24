CREATE PROCEDURE [dbo].[GetTeamProductLimit]
	@TeamId		INT,
	@ProductId	INT
AS
BEGIN

	SELECT Limit FROM [dbo].[TeamProductLimits]
	WHERE
		TeamId = @TeamId
	AND ProductId = @ProductId;

END
