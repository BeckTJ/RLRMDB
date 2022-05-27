INSERT INTO Materials.Material
    (MaterialName, MaterialNameAbreviation, PermitNumber, RawMaterialCode, ProductCode, CarbonDrumRequired, CarbonDrumDaysAllowed, CarbonDrumWeightAllowed)
VALUES
    ('Alpha-Terpinene', 'ATRP', 'APCD2010-PTO-000602', 'TC', 'TP', 1, NULL, 18),
    ('Bis(tert-butylamino)silane', 'BTBAS SAFC', NULL, 'SD', 'SA', 0, NULL, NULL),
    ('Diethoxymethylsilane', 'DEMS', 'APCD2011-PTO-000926', 'SR', 'SE', 0, NULL, NULL),
    ('Hexane', 'Hexane', NULL, NULL, NULL, 0, NULL, NULL),
    ('Hafnium Tetrachloride', 'HFCL', '986660', 'HR', 'HC', 0, NULL, NULL),
    ('Isopropanol', 'IPA', 'APCD2010-PTO-000602', NULL, NULL, 0, NULL, NULL),
    ('Tetrakis(dimethylamino)titanium', 'TDMAT', 'APCD2008-PTO-976853', 'WR', 'WT', 0, NULL, NULL),
    ('Triethylborate', 'TEB', 'APCD2002-PTO-870666', 'UR', 'UE', 1, 265, NULL),
    ('Tetraethyl Orthosilicate', 'TEOS', 'APCD2009-PTO-950939', 'ER', 'EX', 1, 16, NULL),
    ('Triethyl Phosphate', 'TEPO', 'APCD1997-PTO-950407', 'PD', 'PT', 0, NULL, NULL),
    ('Trimethylborate', 'TMB', 'APCD2002-PTO-870666', 'TR', 'TX', 1, 265, NULL),
    ('Trimethyl Phosphite', 'TMPI', 'APCD1997-PTO-950407', 'IR', 'IT', 0, NULL, NULL),
    ('Trimethyl Phosphate', 'TMPO', 'APCD1997-PTO-950407', 'OR', 'OT', 0, NULL, NULL),
    ('Tetramethylcyclotetrasiloxane', 'TOMCATS', NULL, NULL, 'CT', 0, NULL, NULL),
    ('Trans-dichloroethylene', 'TRANS', 'APCD1999-PTO-940529', 'DR', 'DX', 1, 33, NULL),
    ('Bis(tert-butylamino)silane', 'BTBAS BANWOL', NULL, 'SD', 'SA', 0, NULL, NULL),
    ('Bis(tert-butylamino)silane', 'BTBAS 2NTE', NULL, NULL, 'SD', 0, NULL, NULL)



SELECT *
FROM Materials.Material