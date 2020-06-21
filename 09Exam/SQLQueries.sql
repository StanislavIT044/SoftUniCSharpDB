USE [Trip Service]

--Problem02
INSERT INTO Accounts(Firstname, MiddleName, LastName, CityId, BirthDate, Email) VALUES
('John', 'Smith', 'Smith', 34, '1975-07-21', 'j_smith@gmail.com'),
('Gosho', NULL, 'Petrov', 1, '1978-05-16', 'g_petrov@gmail.com'),
('Ivan', 'Petrovich', 'Pavlov', 59, '1849-09-26', 'i_pavlov@softuni.bg'),
('Friedrich', 'Wilhelm', 'Nietzsche', 2, '1844-10-15', 'f_nietzsche@softuni.bg')

INSERT INTO Trips(RoomId, BookDate, ArrivalDate, ReturnDate, CancelDate) VALUES
(101, '2015-04-12', '2015-04-14', '2015-04-20', '2015-02-02'),				
(102, '2015-07-07', '2015-07-15', '2015-07-22', '2015-04-29'),
(103, '2013-07-17', '2013-07-23', '2013-07-24', NULL),
(104, '2012-03-17', '2012-03-31', '2012-04-01', '2012-01-10'),
(109, '2017-08-07', '2017-08-28', '2017-08-29', NULL)

--Problem03
UPDATE Rooms
SET Price += Price * 0.14				
WHERE HotelId IN (5, 7, 9)

--Problem04
DELETE FROM AccountsTrips  
WHERE AccountId = 47

--Problem05
SELECT FirstName,												 
	   LastName, 
	   FORMAT(BirthDate, 'MM-dd-yyyy'), 
	   c.[Name] AS Hometown,				
	   Email
  FROM Accounts AS a
  JOIN Cities AS c ON c.Id = a.CityId
 WHERE Email LIKE 'e%'
 ORDER BY Hometown ASC

--Problem06
SELECT c.[Name], COUNT(h.Id) AS Hotels
  FROM Cities AS c								
  RIGHT JOIN Hotels AS h ON c.Id = h.CityId
 GROUP BY c.[Name]
 ORDER BY Hotels DESC, c.[Name] ASC

--Problem07
SELECT a.Id AS AccountId,
	   a.FirstName + ' ' + a.LastName AS FullName,
	   MAX(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS LongesTrip,
	   MIN(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS ShortestTrip
  FROM Accounts AS a
  JOIN AccountsTrips AS [at] ON [at].AccountId = a.Id
  JOIN Trips AS t ON t.Id = [at].TripId											
 WHERE a.MiddleName IS NULL AND t.CancelDate IS NULL
 GROUP BY a.Id, a.FirstName, a.LastName
 ORDER BY LongesTrip DESC, ShortestTrip ASC

--Problem08
SELECT TOP(10)
	   c.Id, 
	   c.[Name], 
	   c.CountryCode AS Country,							
	   COUNT(a.Id) AS Accounts
  FROM Cities AS c
  JOIN Accounts AS a ON a.CityId = c.Id
 GROUP BY c.Id, c.[Name], c.CountryCode
 ORDER BY Accounts DESC--, c.Name ASC

--Problem09
SELECT a.Id, 
	   a.Email, 
	   c.[Name] AS City,
	   COUNT(t.Id) AS Trips
  FROM Accounts AS a
  JOIN Cities AS c ON a.CityId = c.Id
  JOIN AccountsTrips AS [at] ON [at].AccountId = a.Id			
  JOIN Trips AS t ON [at].TripId = t.Id
  JOIN Hotels AS h ON h.CityId = c.Id
  JOIN Rooms AS r ON t.RoomId = r.Id
  WHERE r.HotelId = h.Id AND h.CityId = a.CityId
 GROUP BY a.Id, a.Email, c.[Name]
 ORDER BY Trips DESC, a.Id ASC

--Problem10
SELECT t.Id,
	   a.FirstName + ' ' + ISNULL(MiddleName + ' ', '') + a.LastName AS [Full Name],
	   c.[Name] AS [From],
	   (SELECT [Name]
	      FROM Cities
		 WHERE Id = h.CityId) AS [To],
		 
	   CASE
	        WHEN t.CancelDate IS NOT NULL THEN 'Cenceled'
			ELSE CAST(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate) AS VARCHAR(10)) + ' days'			
	   END AS Duratiuon
  FROM Accounts AS a
  JOIN Cities AS c ON c.Id = a.CityId
  JOIN AccountsTrips AS [at] ON a.Id = [at].AccountId
  JOIN Trips AS t ON [at].TripId = t.Id
  JOIN Rooms AS r ON t.RoomId = r.Id
  JOIN Hotels AS h ON r.HotelId = h.Id
 ORDER BY [Full Name], t.Id

--Problem11
CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
RETURNS VARCHAR(MAX)
AS
BEGIN

END

--Problem12
CREATE PROCEDURE usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
	DECLARE @TargetRoomHotelsId INT = (SELECT TOP(1) h.Id
									     FROM Hotels AS h
										 JOIN Rooms AS r ON r.HotelId = h.Id
										WHERE r.Id = @TargetRoomId)
	IF(@TargetRoomId IS NULL)
	BEGIN
		RETURN 'Target room is in another hotel!';
	END

	DECLARE @ps INT = (SELECT Beds FROM Rooms  WHERE @TargetRoomId = Id);
	DECLARE @PSS INT = (SELECT  COUNT(*) FROM AccountsTrips WHERE TripId = @TripId);

	IF(@ps < @PSS)
	BEGIN
		RETURN 'Not enough beds in target room!';
	END

	 UPDATE Trips
     SET RoomId = @TargetRoomId
     WHERE Id = @TripId

	SELECT CONVERT(VARCHAR(10), @targetRoomId)
END