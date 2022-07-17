CREATE OR ALTER FUNCTION HumanResources.EmployeeInitials(@employeeId AS CHAR(7) = 'NA')
RETURNS CHAR(2)
AS
BEGIN
DECLARE @employeeInit AS CHAR(2)
SET @employeeInit = (CONCAT(Left(1,(SELECT FirstName FROM HumanResources.Employee WHERE EmployeeId = @employeeId)),Left(1,(SELECT LastName FROM HumanResources.Employee WHERE EmployeeId = @employeeId))))

RETURN @employeeInit;
END