select * from Materials.VendorLot
select * from Distillation.RawMaterial
where MaterialNumber = 3322187
select * from QualityControl.SampleSubmit

GO



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
