USE Gringotts
--01. Records’ Count
SELECT COUNT(*) AS [Count]
FROM WizzardDeposits
--02. Longest Magic Wand
SELECT MAX(MagicWandSize) AS LongestMagicWand
	FROM WizzardDeposits
--03. Longest Magic Wand per Deposit Groups
SELECT DepositGroup , MAX(MagicWandSize) AS LongestMagicWand
	FROM WizzardDeposits
	GROUP BY DepositGroup
--04. Smallest Deposit Group per Magic Wand Size
SELECT TOP(2) DepositGroup 
	FROM WizzardDeposits
	GROUP BY DepositGroup
	ORDER BY AVG(MagicWandSize)
--05. Deposits Sum
SELECT DepositGroup , SUM(DepositAmount) AS TotalDepositAmount
	FROM WizzardDeposits
	GROUP BY DepositGroup
--06. Deposits Sum for Ollivander Family
SELECT DepositGroup ,SUM(DepositAmount) AS TotalDepositAmount
	FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup
--07. Deposits Filter
SELECT DepositGroup ,SUM(DepositAmount) AS TotalSum
	FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup
	HAVING SUM(DepositAmount) < 150000
	ORDER BY TotalSum DESC
--08. Deposit Charge
SELECT DepositGroup,MagicWandCreator,MIN(DepositCharge) AS 'MinDepositCharge'
	FROM WizzardDeposits
	GROUP BY DepositGroup,MagicWandCreator
	ORDER BY MagicWandCreator,DepositGroup
--09. Age Groups
SELECT AgeGroupTable.AgeGroup,COUNT(AgeGroupTable.AgeGroup) AS WizardCount
	FROM(
SELECT
	CASE
		WHEN Age <= 10 THEN '[0-10]'
		WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
		WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
		WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
		WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
		WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
		ELSE '[61+]' 
	END AS AgeGroup
	FROM WizzardDeposits
	) AS AgeGroupTable
GROUP BY AgeGroupTable.AgeGroup
--10. First Letter
SELECT LEFT(FirstName,1)
	FROM WizzardDeposits
	WHERE DepositGroup = 'Troll Chest'
	GROUP BY  LEFT(FirstName,1)
--11. Average Interest
SELECT DepositGroup,IsDepositExpired,AVG(DepositInterest) AS AverageInterest
	FROM WizzardDeposits
	WHERE DepositStartDate >= '01/01/1985'
	GROUP BY DepositGroup,IsDepositExpired
	ORDER BY DepositGroup DESC,IsDepositExpired
--12. Rich Wizard, Poor Wizard
SELECT SUM(k.SumDiff) AS SumDiff
	FROM(
SELECT DepositAmount - LEAD(DepositAmount,1) OVER (ORDER BY Id) AS SumDiff
	FROM WizzardDeposits) AS k


USE SoftUni
--13. Departments Total Salaries
SELECT DepartmentID,SUM(Salary) AS TotalSum
	FROM Employees
	GROUP BY DepartmentID
--14. Employees Minimum Salaries
SELECT DepartmentID,MIN(Salary) AS MinimumSalary
	FROM Employees
	WHERE DepartmentID IN(2,5,7) AND  HireDate > '01.01.2000'
	GROUP BY DepartmentID
--15. Employees Average Salaries
SELECT * INTO NewEmployeeTable
	FROM Employees
	WHERE Salary > 30000
DELETE FROM NewEmployeeTable
WHERE ManagerID = 42

UPDATE NewEmployeeTable
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID,AVG(Salary) AS AverageSalary
	FROM NewEmployeeTable
GROUP BY DepartmentID
--16. Employees Maximum Salaries
SELECT DepartmentID,MAX(Salary) AS MaxSalary
	FROM Employees
GROUP BY DepartmentID
HAVING MAX(salary) NOT BETWEEN 30000 AND 70000
--17. Employees Count Salaries
SELECT COUNT(*)
	FROM Employees
	WHERE ManagerID IS NULL
--18. 3rd Highest Salary
SELECT k.DepartmentID,k.Salary
	FROM(
SELECT DepartmentId,Salary,
DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC)
AS SalaryRank
	FROM Employees) AS k
WHERE K.SalaryRank = 3
GROUP BY K.DepartmentID,K.Salary
--19. Salary Challenge
SELECT TOP(10) FirstName,LastName,DepartmentID
	FROM Employees AS e
	WHERE Salary > (SELECT AVG(Salary) From Employees AS em WHERE em.DepartmentID = e.DepartmentID)
	ORDER BY DepartmentID