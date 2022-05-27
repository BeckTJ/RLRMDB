--delete from rawMaterial
--delete from qualityControl
--delete From vendorBatchInformation
Insert Into QualityControl.SampleSubmit(SampleSubmitNumber, InspectionLotNumber, Rejected, RejectedDate, ApprovalDate, ExperiationDate)
values ('AAA12345', null,  0, null, GETDATE(), DateAdd(year,1,GETDATE())),
('AAA23456', null, 0, null, dateadd(mm,-5,GETDATE()), DateAdd(year,1,GETDATE())),
('AAA12347', null, 1, getdate(), null,null) 

INSERT INTO Vendors.VendorBatch
    (VendorId, VendorBatchNumber, Quantity, MaterialNumber , SampleSubmitNumber)
VALUES
    (5, '123ABC4567', 10,45235,(SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA12345')),
    (3, '987XYZ6543', 8,2308223, (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA23456')),
    (2, '543RST6789', 1,45320,NULL),
    (5, '123ABC9876', 5,45235, (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA12347')),
    (7, '111ABC1111', 1,45235, (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA98765'))

Insert Into QualityControl.SampleSubmit(SampleSubmitNumber, InspectionLotNumber, Rejected, RejectedDate, ApprovalDate, ExperiationDate)
values ('AAA12345', null,  0, null, GETDATE(), DateAdd(year,1,GETDATE())),
('AAA23456', null, 0, null, dateadd(mm,-5,GETDATE()), DateAdd(year,1,GETDATE())),
('AAA12347', null, 1, getdate(), null,null)
--update Vendors.VendorBatch
--set sampleSubmitNumber = (select sampleSubmitNumber 
--                            from QualityControl.SampleSubmit 
--                            where )

SELECT *
FROM Vendors.VendorBatch

select *
from QualityControl.SampleSubmit