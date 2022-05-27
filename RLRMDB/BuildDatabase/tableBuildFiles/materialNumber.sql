INSERT INTO Materials.MaterialNumber
	(MaterialNumber, NameId, IsRawMaterial, BatchManaged, RequiresProcessOrder, UnitOfIssue)
VALUES
	(37705, (select NameId From Materials.Material where MaterialNameAbreviation ='TDMAT'), 0, 1, 0, 'kg'),
	(45230, (select NameId From Materials.Material where MaterialNameAbreviation ='TEPO'), 0, 1, 0, 'kg'),
	(45231, (select NameId From Materials.Material where MaterialNameAbreviation ='TEPO'), 1, 0, 0, 'kg'),
	(45234, (select NameId From Materials.Material where MaterialNameAbreviation ='TEOS'), 0, 0, 0, 'kg'),
	(45235, (select NameId From Materials.Material where MaterialNameAbreviation ='TEOS'), 1, 0, 0, 'kg'),
	(45255, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 0, 1, 0, 'kg'),
	(45256, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 0, 1, 0, 'kg'),
	(45260, (select NameId From Materials.Material where MaterialNameAbreviation ='TMB'), 0, 0, 0, 'kg'),
	(45261, (select NameId From Materials.Material where MaterialNameAbreviation ='TMB'), 1, 0, 0, 'kg'),
	(45262, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPI'), 0, 0, 0, 'kg'),
	(45264, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPI'), 1, 0, 0, 'kg'),
	(45265, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPO'), 0, 0, 0, 'kg'),
	(45266, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPO'), 1, 0, 0, 'kg'),
	(45267, (select NameId From Materials.Material where MaterialNameAbreviation ='TOMCATS'), 1, 0, 0, 'kg'),
	(45269, (select NameId From Materials.Material where MaterialNameAbreviation ='TRANS'), 0, 0, 0, 'kg'),
	(45270, (select NameId From Materials.Material where MaterialNameAbreviation ='TRANS'), 1, 0, 0, 'kg'),
	(45275, (select NameId From Materials.Material where MaterialNameAbreviation ='TEB'), 0, 0, 0, 'kg'),
	(45276, (select NameId From Materials.Material where MaterialNameAbreviation ='TEB'), 1, 0, 0, 'kg'),
	(45320, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 0, 'kg'),
	(45329, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 0, 0, 0, 'kg'),
	(45333, (select NameId From Materials.Material where MaterialNameAbreviation ='DEMS'), 0, 1, 0, 'kg'),
	(45337, (select NameId From Materials.Material where MaterialNameAbreviation ='Hexane'), 1, 0, 0, 'kg'),
	(176164, (select NameId From Materials.Material where MaterialNameAbreviation ='IPA'), 1, 0, 0, 'kg'),
	(186924, (select NameId From Materials.Material where MaterialNameAbreviation ='HFCL'), 1, 0, 0, 'g'),
	(186997, (select NameId From Materials.Material where MaterialNameAbreviation ='HFCL'), 0, 0, 0, 'g'),
	(444807, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS BANWOL'), 1, 1, 0, 'kg'),
	(444808, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS BANWOL'), 0, 1, 0, 'kg'),
	(456324, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS 2NTE'), 1, 1, 0, 'kg'),
	(475743, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 1, 1, 0, 'kg'),
	(475744, (select NameId From Materials.Material where MaterialNameAbreviation ='TDMAT'), 1, 1, 0, 'kg'),
	(2302328, (select NameId From Materials.Material where MaterialNameAbreviation ='DEMS'), 1, 1, 0, 'kg'),
	(2308223, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 0, 'kg'),
	(2304780, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 1, 1, 1, 'kg'),
	(2304783, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 1, 1, 0, 'kg'),
	(2305864, (select NameId From Materials.Material where MaterialNameAbreviation ='DEMS'), 1, 1, 1, 'kg'),
	(2305935, (select NameId From Materials.Material where MaterialNameAbreviation ='TEOS'), 1, 0, 0, 'kg'),
	(2306209, (select NameId From Materials.Material where MaterialNameAbreviation ='TRANS'), 1, 0, 1, 'kg'),
	(2306574, (select NameId From Materials.Material where MaterialNameAbreviation ='TDMAT'), 1, 1, 1, 'kg'),
	(2308221, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 1, 'kg'),
	(2308222, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 1, 'kg'),
	(2308339, (select NameId From Materials.Material where MaterialNameAbreviation ='TEPO'), 1, 1, 0, 'kg')



select *
From Materials.MaterialNumber