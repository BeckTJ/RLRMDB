CREATE OR ALTER VIEW material
AS
    SELECT materialNumber.materialNumber AS 'Material Number',
        materialNameAbreviation AS 'Material',
        materialGrade AS 'Grade',
        materialName AS 'Description',
        materialName.permitNumber AS 'Permit Number',
        rawMaterialCode AS 'Raw Material Code',
        productCode AS 'Product Code',
        carbonDrumRequired AS 'Carbon Drum',
        carbonDrumDaysAllowed AS 'Days Allowed',
        carbonDrumWeightAllowed AS 'Weight Allowed',
        batchManaged AS 'Batch Managed',
        requiresProcessOrder AS 'PO Required',
        unitOfIssue AS 'UI',
        vendorName AS 'Vendor Name',
        isRawMaterial AS 'Raw Material'
    FROM materialNumber
        JOIN materialName ON materialName.materialNameId = materialNumber.materialNameId
        JOIN materialId ON materialId.materialNumber = materialNumber.materialNumber
        JOIN vendor ON vendor.vendorId = materialId.vendorId
        JOIN productNumberSequence ON materialId.sequenceId = productNumberSequence.sequenceId
