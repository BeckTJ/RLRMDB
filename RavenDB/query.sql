select * from Materials.VendorLot
where MaterialNumber = 3322187

select * from Distillation.RawMaterial
join QualityControl.SampleSubmit on SampleSubmit.SampleSubmitNumber = RawMaterial.SampleSubmitNumber
where MaterialNumber = 3322187
GO
update QualityControl.SampleSubmit
set Approved = 1
where SampleSubmitNumber = 'RAW61560'


select * from QualityControl.SampleRequired
where MaterialNumber = (select ParentMaterialNumber from Materials.MaterialVendor
                        where MaterialNumber = 3322187)

select * from Distillation.AlphabeticDate

select * from QualityControl.SampleRequired


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
