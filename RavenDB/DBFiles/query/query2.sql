insert into Distillation.Production(ProductLotNumber,MaterialNumber,ProductBatchNumber,ProcessOrder,ReceiverName)
values('998AB',58971,1239999,123999999,'B')

select * from Distillation.Production

select * from Materials.MaterialId

select * from Distillation.RawMaterial
where MaterialNumber = 32716 and VendorBatchNumber = (select VendorBatchNumber
                                                        from Materials.VendorBatch
                                                        where VendorName = 'Liquor Store')

select * from Materials.VendorBatch
where MaterialNumber = 32716 and VendorName = 'Liquor Store'