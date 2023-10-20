select * from Materials.VendorLot
select * from Distillation.RawMaterial
select * from QualityControl.SampleSubmit





-- CREATE TABLE #tmpReceiver(
--     Id int PRIMARY KEY IDENTITY(1,1),
--     MaterialName VARCHAR(10),
--     MaterialNumber INT,
--     ReceiverName VARCHAR(6),
--     MaxReceiverLevel INT,
-- )

-- BULK INSERT #tmpReceiver FROM '..\..\usr\raven\BuildFiles\ReceiverData.csv'
-- WITH(
--     FORMAT = 'csv',
--     FIRSTROW = 2,
--     FIELDTERMINATOR = ',',
--     ROWTERMINATOR = '\n',
--     KEEPNULLS
-- )
