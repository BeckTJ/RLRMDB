EXEC MaterialInsertDB

--select * from Materials.Material
--select * from Materials.MaterialNumber
--Select * from materials.MaterialId

select * FROM Materials.MaterialView
order by ParentMaterialNumber
