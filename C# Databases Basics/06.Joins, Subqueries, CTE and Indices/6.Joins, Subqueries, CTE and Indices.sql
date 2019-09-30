/*--01. Employee Address--*/

SELECT TOP(5) e.EmployeeID,e.JobTitle,e.AddressID,a.AddressText
	FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
ORDER BY AddressID

/*--02. Addresses with Towns--*/

SELECT TOP(50) e.FirstName,e.LastName,t.Name AS [Town],a.AddressText 
	FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
ORDER BY e.FirstName,e.LastName

/*--03. Sales Employees--*/

SELECT e.EmployeeID,e.FirstName,e.LastName,d.Name AS [DepartmentName]
	FROM Employees AS e
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY e.EmployeeID

/*--04. Employee Departments--*/

SELECT TOP(5) e.EmployeeID,e.FirstName,e.Salary,d.Name 
	FROM Employees AS e
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE E.Salary > 15000
ORDER BY D.DepartmentID

/*--05. Employees Without Projects--*/

SELECT TOP(3) e.EmployeeID,e.FirstName
	FROM Employees AS e
	LEFT JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
WHERE ep.EmployeeID IS NULL
ORDER BY e.EmployeeID

/*--06. Employees Hired After--*/

SELECT e.FirstName,e.LastName,e.HireDate,d.Name AS [DeptName]
	FROM Employees AS e
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE E.HireDate >  '1999/01/01' AND d.Name IN('Sales','Finance')
ORDER BY e.HireDate

/*--07. Employees With Project--*/

SELECT TOP(5) e.EmployeeID,e.FirstName,p.Name 
	FROM Employees AS e
	JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
	JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE p.StartDate > '2002.08.13' AND p.EndDate IS NULL
ORDER BY e.EmployeeID

/*--08. Employee 24--*/

SELECT e.EmployeeID,e.FirstName,
	CASE
		WHEN p.StartDate >= '2005/01/01' THEN NULL
		ELSE p.Name
	END AS ProjectName
	FROM Employees AS e
	JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
	JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE ep.EmployeeID = 24

/*--09. Employee Manager--*/

SELECT e1.EmployeeID,e1.FirstName ,e1.ManagerID,e2.FirstName AS [ManagerName]
	FROM Employees AS e1
	JOIN Employees AS e2 ON e2.EmployeeID = e1.ManagerID

WHERE e1.ManagerID IN(3,7)
ORDER BY e1.EmployeeID

/*--10. Employees Summary--*/

SELECT TOP(50) e.EmployeeID,e.FirstName + ' ' + e.LastName AS EmployeeName,
	m.FirstName + ' ' + m.LastName AS ManagerName,
	d.Name AS DepartmentName
	FROM Employees AS e
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
	JOIN Employees AS m ON e.ManagerID = m.EmployeeID
ORDER BY e.EmployeeID


/*--11. Min Average Salary--*/
SELECT MIN(e.AverageSalary) AS MinAverageSalary
	FROM(
SELECT AVG(Salary) AS AverageSalary
	FROM Employees
GROUP BY DepartmentID) AS e

/*--12. Highest Peaks in Bulgaria--*/

SELECT c.CountryCode,m.MountainRange,p.PeakName,p.Elevation 
	FROM Countries AS c 
	JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode AND c.CountryCode = 'BG'
	JOIN Mountains AS m ON m.Id = mc.MountainId
	JOIN Peaks AS p ON p.MountainId = mc.MountainId AND p.Elevation > 2835
ORDER BY p.Elevation DESC

/*--13. Count Mountain Ranges--*/

SELECT c.CountryCode ,COUNT(mc.MountainId) AS MountainRanges
	FROM Countries AS c
	JOIN MountainsCountries AS MC ON mc.CountryCode = c.CountryCode
WHERE c.CountryCode IN('US','RU','BG')
GROUP BY C.CountryCode

/*--14. Countries With or Without Rivers--*/

SELECT TOP(5) c.CountryName,r.RiverName
	FROM Countries AS c
	LEFT JOIN CountriesRivers AS cr ON cr.CountryCode= c.CountryCode
	LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
	WHERE c.ContinentCode = 'AF'
	ORDER BY c.CountryName

/*--15. Continents and Currencies--*/

WITH CTE_CountriesInfo(ContinentCode,CurrencyCode,CurrencyUsage) AS(
SELECT ContinentCode, CurrencyCode ,COUNT(CurrencyCode) AS CurrencyUsage
	FROM Countries
GROUP BY ContinentCode,CurrencyCode
HAVING COUNT(CurrencyCode)> 1)

SELECT e.ContinentCode,cci.CurrencyCode,e.MaxCurrency AS CurrencyUsage FROM(
SELECT ContinentCode,MAX(CurrencyUsage) AS MaxCurrency
	FROM CTE_CountriesInfo
GROUP BY ContinentCode) AS e
JOIN CTE_CountriesInfo AS cci ON cci.ContinentCode = e.ContinentCode AND cci.CurrencyUsage = e.MaxCurrency

/*--16. Countries Without any Mountains--*/

SELECT COUNT(*)
	FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	WHERE mc.CountryCode IS NULL

/*--17. Highest Peak and Longest River by Country--*/

SELECT TOP(5) c.CountryName ,MAX(p.Elevation) AS HighestPeak,MAX(r.Length) AS LongestRiver
	FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	LEFT JOIN Peaks AS p ON p.MountainId = mc.MountainId
	LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
	LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY HighestPeak DESC,LongestRiver DESC,c.CountryName


/*--18.	* Highest Peak Name and Elevation by Country--*/

WITH CTE_CountriesInfo(CountryName,PeakName,Elevation,Mountain) AS (
SELECT TOP(5) c.CountryName,p.PeakName,MAX(p.Elevation),m.MountainRange
	FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
	LEFT JOIN Peaks AS p ON p.MountainId = m.Id
GROUP BY c.CountryName,p.PeakName,m.MountainRange)
SELECT e.CountryName,
	ISNULL (cci.PeakName,'(no highest peak)') AS [Highest Peak Name],
	ISNULL (cci.Elevation,0) AS [Highest Peak Elevation],
	ISNULL (cci.Mountain,'(no mountain)') AS [Mountain]
	FROM(
SELECT CountryName,MAX(Elevation) AS MaxElevation
	FROM CTE_CountriesInfo
GROUP BY CountryName)AS e
	LEFT JOIN CTE_CountriesInfo AS cci ON cci.CountryName = e.CountryName AND cci.Elevation = e.MaxElevation
ORDER BY e.CountryName,cci.PeakName
