--select  MaterialNumber.MaterialNumber,Material.MaterialNameAbreviation, RawMaterialCode,ProductCode,VendorId,CurrentSequenceId,IsRawMaterial
--from Materials.MaterialNumber
--join Materials.Material on MaterialNumber.NameId = Material.NameId
--join Materials.MaterialId on MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
--where MaterialNumber.MaterialNumber = 45320 AND VendorId = (Select VendorId 
--                                                                From Vendors.Vendor
--                                                                where vendorName = 'Berje')

--insert into Vendors.Vendor(VendorName)
--VALUES('Finish Product')

--select Distillation.SetProductIdNumber(45320,'Berje')
--select Distillation.SetProductIdNumber(45329,'Finished Product')
--select Distillation.SetProductIdNumber(2308223,'Symerise')
--select Distillation.SetProductIdNumber(2302328,'Sivance')
--SELECT Distillation.SetProductIdNumber(45235,'Silabond')
--select Distillation.SetProductIdNumber(45269, 'Finished Product')
--select Distillation.SetProductIdNumber(45234,'Evonik')
select Distillation.SetProductIdNumber(45234, DEFAULT)

--DECLARE @id AS VARCHAR(6)
--SET @id = (select Distillation.SetProductIdNumber(2308223,'Symerise'))
--
--select Distillation.UpdateProductId(@id)

--Select * From Vendors.Vendor
--Select * From Materials.MaterialNumber 
--Join Materials.MaterialId on MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
--where MaterialNumber.MaterialNumber = 45234
