SELECT *
FROM Materials.Material
JOIN Materials.MaterialNumber on Material.NameId = MaterialNumber.NameId
JOIN Materials.MaterialId on MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
JOIN Distillation.ProductNumberSequence on MaterialId.SequenceId = ProductNumberSequence.SequenceId
WHERE  MaterialNameAbreviation = 'ATRP' AND IsRawMaterial = 0