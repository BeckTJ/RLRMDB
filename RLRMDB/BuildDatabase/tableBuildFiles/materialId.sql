--delete from materialId

INSERT INTO Materials.MaterialId
	(MaterialNumber, VendorId, CurrentSequenceId)
VALUES
	(2308223, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Symerise'), 1000),
	(45320, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Berje'), 3000),
	(45329, 0, 600),
	(2308221, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 8000),
	(2308222, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Column Drain'),  9000),
	(2304780, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 1),
	(2304780, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Heels and Prefraction'), 1),
	(456324, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), NULL),
	(444807, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), 200),
	(444808, 0, 400),
	(475743, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 700),
	(45255, 0, 300),
	(45256, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 100),
	(2304783, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SVM'), 500),
	(45333, 0, 700),
	(2302328, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Wacker'), 900),
	(2302328, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Sivance'), 800),
	(2305864, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Heels and Prefraction'), 2000),
	(45337, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), 100),
	(186924, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'ATI') , 100),
	(186997, 0, 700),
	(37705, 0, 900),
	(475744, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Boulder'), 600),
	(475744, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Evonik'), 700),
	(2306574, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 200),
	(45275, 0, 200),
	(45276, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Anderson'), 200),
	(45234, 0, 500),
	(45234, 0, 700),
	(45235, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Silabond'), 2000),
	(45235, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Wacker'), 3000),
	(2305935, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 4000),
	(45230, 0, 200),
	(45231, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Heels and Prefraction'), 600),
	(2308339, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Treated'), 700),
	(45260, 0, 100),
	(45261, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Solvay'), 300),
	(45262, 0, 200),
	(45264, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Hacros'), 400),
	(45264, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Solvay'), 300),
	(45265, 0, 200),
	(45266, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), 500),
	(45267, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Sivance'), 400),
	(45269, 0, 600),
	(45270, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Axiall'), 1000),
	(2306209, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 500);

	UPDATE Materials.MaterialId
	SET SequenceId = (SELECT SequenceId 
						FROM Distillation.ProductNumberSequence
						WHERE SequenceIdStart = CurrentSequenceId);

SELECT *
FROM Materials.MaterialId
