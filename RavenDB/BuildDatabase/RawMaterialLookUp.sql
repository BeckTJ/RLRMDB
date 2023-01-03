SELECT *
FROM Materials.Material
JOIN Materials.MaterialNumber on Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
JOIN Materials.MaterialId on MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
JOIN Distillation.ProductNumberSequence on MaterialId.SequenceId = ProductNumberSequence.SequenceId
