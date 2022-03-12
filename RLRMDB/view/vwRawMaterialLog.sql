CREATE OR ALTER VIEW RawMaterialLog
AS
    SELECT dateUsed,
        rawMaterial.drumLotNumber,
        rawMaterial.processOrder,
        rawMaterial.materialNumber,
        materialName.materialNameAbreviation,
        vendor.vendorName,
        vendorBatchInformation.vendorBatchNumber,
        qualityControl.sampleSubmitNumber,
        qualityControl.inspectionLotNumber,
        CONCAT(rawMaterial.drumWeight,' ', materialNumber.unitOfIssue) AS 'Drum Weight',
        qualityControl.approvalDate,
        qualityControl.experiationDate
    FROM rawMaterial
        JOIN qualityControl ON qualityControl.sampleSubmitNumber = rawMaterial.sampleSubmitNumber
        JOIN vendor ON vendor.vendorId = rawMaterial.vendorId
        JOIN vendorBatchInformation ON rawMaterial.vendorBatchId = vendorBatchInformation.batchId
        JOIN materialNumber ON materialNumber.materialNumber = rawMaterial.materialNumber
        JOIN materialName ON materialName.materialNameId = materialNumber.materialNameId
