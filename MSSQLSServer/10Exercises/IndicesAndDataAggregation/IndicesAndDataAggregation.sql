USE Gringotts

--Problem01
SELECT COUNT(*) AS [Count]
  FROM WizzardDeposits

--Problem02
SELECT MAX(MagicWandSize) AS LongestMagicWand
  FROM WizzardDeposits

--Problem03
SELECT DepositGroup, MAX(MagicWandSize) AS LongestMagicWand
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
