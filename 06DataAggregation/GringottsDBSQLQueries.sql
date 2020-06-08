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