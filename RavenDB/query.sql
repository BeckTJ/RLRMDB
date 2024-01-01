select * from Materials.VendorLot
where MaterialNumber = 3322187

GO
update QualityControl.SampleSubmit
set Approved = 1, SampleDate = DATEADD(DAY,-3,GETDATE())
where SampleType = 'RAW' and SampleId = 61560

select * from Distillation.RawMaterial
join QualityControl.SampleSubmit on SampleSubmit.SampleId = RawMaterial.SampleId
where MaterialNumber = 3081971 --3322187

select * from QualityControl.SampleRequired
where MaterialNumber = 58423
-- where MaterialNumber = (select ParentMaterialNumber from Materials.MaterialVendor
--                         where MaterialNumber = 3322187)

select * from Materials.MaterialVendor where ParentMaterialNumber = 58423
select * from Materials.VendorLot

select * from Distillation.AlphabeticDate

select * from QualityControl.SampleRequired

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
