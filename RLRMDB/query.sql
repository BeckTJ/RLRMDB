--materialInsert 33333,'Liquid', 'LIQD', '123ABC-9876', 'AA', 'BB', FALSE, NULL,NULL,NULL,TRUE,TRUE,'kg',FALSE,'Silabond',1000

SELECT *
FROM material



--(@materialNumber AS INT,
--@materialName AS VARCHAR(50),
--@nameAbreviation AS VARCHAR(10),
--@permitNumber AS VARCHAR(25),
--@rawMaterialCode AS VARCHAR(3),
--@productCode AS VARCHAR(3),
--@carbonDrumRequired AS BIT,
--@carbonDrumDaysAllowed AS INT,
--@carbonDrumWeightAllowed AS INT,
--@materialGrade AS CHAR(10),
--@batchManaged AS BIT,
--@requiresProcessOrder AS BIT,
--@unitOfIssue AS CHAR(2),
--@isRawMaterial AS BIT,
--@vendorName AS VARCHAR(25),
--@sequenceNumber AS INT)


delete materialNumber where materialNumber = 33333
delete materialName where materialName = 'liquid'
delete materialId where materialNumber = 33333

--SELECT *
--FROM materialId
--where materialNumber = 33333
--SELECT *
--FROM materialNumber
--where materialNumber = 33333
--SELECT *
--FROM materialName
--where materialName = 'liquid'


