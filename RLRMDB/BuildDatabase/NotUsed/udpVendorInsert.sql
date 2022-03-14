CREATE OR ALTER PROCEDURE vendorInsert(@vendorName AS VARCHAR(25))
AS

INSERT INTO vendor
    (vendorName)
VALUES(@vendorName);