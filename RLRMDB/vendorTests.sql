--Select vendorName
--From vendor
--join materialId on vendor.vendorId = materialId.vendorId
--where materialId.materialNumber = 45235
--


--select * from materialNumber
--select * from materialId
--select * from material

select materialNumber.materialNumber
from materialNumber
where materialNumber.materialNameId = 9 and materialNumber.isRawMaterial = 1
