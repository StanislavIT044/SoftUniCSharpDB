CREATE FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(18, 4), @YearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS DECIMAL(18, 4)
AS 
BEGIN 
	DECLARE @result DECIMAL(18, 4) = @sum * (POWER(1 + @YearlyInterestRate, @numberOfYears))
	RETURN @result
END