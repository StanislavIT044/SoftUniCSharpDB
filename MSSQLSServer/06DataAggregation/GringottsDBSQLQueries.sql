USE Gringotts

--Problem01
SELECT COUNT(*) AS [Count]
  FROM WizzardDeposits

--Problem02
SELECT MAX(MagicWandSize)
  FROM WizzardDeposits

--Problem03
SELECT DepositGroup, MAX(MagicWandSize)
  FROM WizzardDeposits
 GROUP BY DepositGroup

--Problem04
SELECT TOP(2) DepositGroup
  FROM WizzardDeposits
 GROUP BY DepositGroup
 ORDER BY AVG(MagicWandSize) ASC

--Problem05
SELECT DepositGroup, SUM(DepositAmount)
  FROM WizzardDeposits
 GROUP BY DepositGroup

--Problem06
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
  FROM WizzardDeposits
 WHERE MagicWandCreator = 'Ollivander family'
 GROUP BY DepositGroup

--Problem07
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
  FROM WizzardDeposits
 WHERE MagicWandCreator = 'Ollivander family'
 GROUP BY DepositGroup 
HAVING SUM(DepositAmount) < 150000
 ORDER BY TotalSum DESC

--Problem08
SELECT DepositGroup, MagicWandCreator, MIN(DepositCharge) AS MinDepositCharge
  FROM WizzardDeposits
 GROUP BY DepositGroup, MagicWandCreator
 ORDER BY MagicWandCreator, DepositGroup

--Problem09
SELECT AgeGroupTable.AgeGroup, COUNT(AgeGroupTable.AgeGroup)
  FROM (
SELECT Age,
    CASE
    WHEN Age <= 10 THEN '[0-10]'
	WHEN Age <= 20 THEN '[11-20]'
	WHEN Age <= 30 THEN '[21-30]'
	WHEN Age <= 40 THEN '[31-40]'
	WHEN Age <= 50 THEN '[41-50]'
	WHEN Age <= 60 THEN '[51-60]'
	ELSE '[61+]' 
   END AS AgeGroup
  FROM WizzardDeposits) AS AgeGroupTable
 GROUP BY AgeGroupTable.AgeGroup

--Problem10
SELECT DISTINCT SUBSTRING(FirstName, 1, 1) AS FirstLetter
  FROM WizzardDeposits
 WHERE DepositGroup = 'Troll Chest'
 GROUP BY FirstName

--Problem11
SELECT DepositGroup,
	   IsDepositExpired,
	   AVG(DepositInterest) AS AverageInterest
	   FROM WizzardDeposits
 WHERE DepositStartDate > '01/01/1985'
 GROUP BY DepositGroup, IsDepositExpired
 ORDER BY DepositGroup DESC, IsDepositExpired ASC

--Problem12
SELECT SUM([Difference])
  FROM ( SELECT FirstName AS [Host Wizard],
			   DepositAmount AS [Host Wizard Deposit],
	           LEAD(FirstName) OVER(ORDER BY Id ASC) AS [Guest Wizard],
	           LEAD(DepositAmount) OVER (ORDER BY Id ASC) AS [Guest Wizard Deposit],
	           DepositAmount -  LEAD(DepositAmount) OVER (ORDER BY Id ASC) AS [Difference]
          FROM WizzardDeposits
	   ) AS [LeadQuery]
 WHERE [Guest Wizard] IS NOT NULL