CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @result NVARCHAR(50);
	
	IF(@salary < 30000)
	BEGIN
	SET @result = 'Low'
	END

	ELSE IF(@salary <= 50000)
	BEGIN
	SET @result = 'Average'
	END

	ELSE 
	BEGIN
	SET @result = 'High'
	END

	RETURN @result;
END