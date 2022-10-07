INSERT INTO materialName
    (materialNameId,materialName,materialNameAbreviation,permitNumber,rawMaterialCode,productCode,carbonDrumRequired,carbonDrumDaysAllowed,carbonDrumWeightAllowed)
VALUES
    (1, 'Temperature', 'TEMP', 'ABC123-9876', 'AR', 'AP', 1, NULL, 18),
    (2, 'Silica', 'SIL ', NULL, 'BR', 'BP', 0, NULL, NULL),
    (3, 'Tamper', 'TAMP', 'ABC123-4567', 'CR', 'CP', 0, NULL, NULL),
    (4, 'Alcohol', 'ALC', NULL, NULL, NULL, 0, NULL, NULL),
    (5, 'Powder', 'PWDR', '123456', 'DR', 'DP', 0, NULL, NULL),
    (6, 'Whiskey', 'BOOZE', 'ABC123-0960', NULL, NULL, 0, NULL, NULL),
    (7, 'Orange Juice', 'OJ', 'ABC123-32542', 'ER', 'EP', 0, NULL, NULL),
    (8, 'Cow Juice', 'MILK', 'ABC123-0960', 'UR', 'UE', 1, 265, NULL),
    (9, 'Grape Soda', 'GPSA', 'ABC123-3545', 'FR', 'FP', 1, 16, NULL),
    (10, 'Drain Cleaner', 'DRAINO', 'ABC123-3374', 'GR', 'GP', 0, NULL, NULL),
    (11, 'Wood Polish', 'SHINE', 'ABC123-3353', 'HR', 'HP', 1, 265, NULL),
    (12, 'Glass CLeaner', 'WINX', 'ABC123-1435', 'IR', 'IP', 0, NULL, NULL),
    (13, 'Scott Tape', 'TAPE', 'ABC123-0349', 'JR', 'JP', 0, NULL, NULL),
    (14, 'Teflon', 'TEF', NULL, NULL, 'KP', 0, NULL, NULL),
    (15, 'Special', 'SPEC', 'ABC123-0293', 'LR', 'LP', 1, 33, NULL)


SELECT *
FROM materialName