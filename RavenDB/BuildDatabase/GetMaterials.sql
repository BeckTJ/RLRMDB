Select * FROM Materials.MaterialNumber
JOIN Materials.Material on Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
join Materials.MaterialId on Material.MaterialNumber = MaterialId.MaterialNumber
where materialnumber.materialnumber = materialnumber.ParentMaterialNumber 
-- ORDER BY Material.MaterialNumber
-- select * from Materials.VendorBatch