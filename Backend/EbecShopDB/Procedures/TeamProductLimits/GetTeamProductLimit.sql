CREATE PROCEDURE [dbo].GetTeamProductTypeLimit
	@TeamId		INT,
	@ProductTypeId	INT
AS
BEGIN

	SELECT Limit FROM [dbo].[TeamProductLimits]
	WHERE
		TeamId = @TeamId
	AND ProductTypeId = @ProductTypeId;

END
