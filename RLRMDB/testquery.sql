--select *
--from materialNumber
--    join material on materialNumber.materialNameId = material.materialNameId
--    join materialId on materialNumber.materialNumber = materialId.materialNumber
--    join vendor
--    on materialId.vendorId = vendor.vendorId
--    join productNumberSequence on materialId.sequenceId = productNumberSequence.sequenceId
--where materialNumber.materialNumber = 45235

--select *
--from materialNumber
--    join material on materialNumber.materialNameId = material.materialNameId
--    join materialId on materialNumber.materialNumber = materialId.materialNumber
--    --join vendor on materialId.vendorId = vendor.vendorId
--    join productNumberSequence on materialId.sequenceId = productNumberSequence.sequenceId
--where materialNumber.materialNumber = 45234


select * from rawMaterial