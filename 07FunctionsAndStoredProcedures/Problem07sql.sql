CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(50), @word NVARCHAR(25)) 
RETURNS BIT
AS
BEGIN
	DECLARE @result BIT = 1;
	DECLARE @counter INT = 1;
	DECLARE @wordLen INT = LEN(@word);
		WHILE(@counter <= @wordLen)
		BEGIN
		DECLARE @currentLetter CHAR(1)= SUBSTRING(@word, @counter, 1);
			IF(@setOfLetters NOT LIKE '%'+@currentLetter + '%')
			BEGIN
			SET @result = 0;
			END
		SET @counter += 1;
		END

	RETURN @result;
END

SELECT FirstName, dbo.ufn_IsWordComprised('j', FirstName) AS [Result] FROM Employees