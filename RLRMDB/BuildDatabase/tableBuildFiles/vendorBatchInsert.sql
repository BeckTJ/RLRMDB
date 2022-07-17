Insert Into QualityControl.SampleSubmit(SampleSubmitNumber)
values ('AAA12345'),
('AAA23456'),
('AAA12347') 

UPDATE QualityControl.SampleSubmit
SET Rejected = 1
WHERE SampleSubmitNumber = 'AAA12347'

UPDATE QualityControl.SampleSubmit
SET Rejected = 0
WHERE SampleSubmitNumber = 'AAA12345'

UPDATE QualityControl.SampleSubmit
SET Rejected = 0
WHERE SampleSubmitNumber = 'AAA23456'

INSERT INTO Vendors.VendorBatch
    (VendorId, VendorBatchNumber, Quantity, MaterialNumber , SampleSubmitNumber)
VALUES
    (5, '123ABC4567', 10,45235,(SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA12345')),
    (3, '987XYZ6543', 8,2308223, (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA23456')),
    (2, '543RST6789', 1,45320,NULL),
    (5, '123ABC9876', 5,45235, (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA12347')),
    (7, '111ABC1111', 1,45235, (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = 'AAA98765'))

SELECT *
FROM Vendors.VendorBatch

select *
from QualityControl.SampleSubmit