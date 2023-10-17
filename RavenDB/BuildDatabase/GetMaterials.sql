Select * FROM Materials.MaterialNumber
JOIN Materials.Material on Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
join Materials.MaterialId on Material.MaterialNumber = MaterialId.MaterialNumber
where materialnumber.materialnumber = materialnumber.ParentMaterialNumber 
-- ORDER BY Material.MaterialNumber
-- select * from Materials.VendorBatch

INSERT INTO Distillation.RawMaterial(ProductLotNumber,MaterialNumber,DrumWeight,VendorBatchNumber)
VALUES(Distillation.SetRawMaterialProductId(3322187),3322187,180,'999-999-001')



SELECT ProductLotNumber FROM Distillation.RawMaterial
                WHERE MaterialNumber = 3322187
                ORDER BY ProductLotNumber DESC

SELECT Distillation.GetRawMaterialProductId(3322187)
SELECT Distillation.SetProductId(58423)

