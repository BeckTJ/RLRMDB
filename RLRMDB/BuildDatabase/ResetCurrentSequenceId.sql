update Materials.MaterialId
set CurrentSequenceId = (select ProductNumberSequence.SequenceIdStart from Distillation.ProductNumberSequence
join Materials.MaterialId on MaterialId.SequenceId = ProductNumberSequence.SequenceId
where MaterialNumber = 45234 and VendorId = (select VendorId from Vendors.Vendor where VendorName = 'Finished Product') )
where MaterialNumber = 45234 and VendorId = (select VendorId from Vendors.Vendor where VendorName = 'Finished Product')
