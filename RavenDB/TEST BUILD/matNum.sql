INSERT INTO materialNumber
    (materialNumber,materialNameId,materialGrade,isRawMaterial,batchManaged,requiresProcessOrder,unitOfIssue)
VALUES
    (12345, 7, NULL, 0, 1, 0, 'kg'),
    (23456, 10, NULL , 0, 1, 0, 'kg'),
    (34567, 10, NULL , 1, 0, 0, 'kg'),
    (45678, 9, NULL , 0, 0, 0, 'kg'),
    (56789, 9, NULL , 1, 0, 0, 'kg'),
    (67891, 2, 'TEMP', 0, 1, 0, 'kg'),
    (78912, 2, 'TEMP', 1, 1, 0, 'kg'),
    (89123, 11, NULL , 0, 0, 0, 'kg'),
    (91234, 11, NULL, 1, 0, 0, 'kg'),
    (11111, 12, NULL , 0, 0, 0, 'kg'),
    (22222, 12, NULL , 1, 0, 0, 'kg'),
    (33333, 13, NULL , 0, 0, 0, 'kg'),
    (44444, 13, NULL, 1, 0, 0, 'kg'),
    (55555, 14, NULL , 1, 0, 0, 'kg'),
    (66666, 15, NULL , 0, 0, 0, 'kg'),
    (77777, 15, NULL , 1, 0, 0, 'kg'),
    (88888, 8, NULL , 0, 0, 0, 'kg'),
    (99999, 8, NULL , 1, 0, 0, 'kg'),
    (22999, 1, NULL , 1, 0, 0, 'kg'),
    (33888, 1, NULL , 0, 0, 0, 'kg'),
    (44777, 3, NULL , 0, 1, 0, 'kg'),
    (55666, 4, NULL, 1, 0, 0, 'kg'),
    (123456, 6, NULL , 1, 0, 0, 'kg'),
    (234567, 5, NULL , 1, 0, 0, 'kg'),
    (345678, 5, NULL , 0, 0, 0, 'kg'),
    (456789, 2, 'SHELL', 1, 1, 0, 'kg'),
    (567890, 2, 'SHELL', 0, 1, 0, 'kg'),
    (678901, 2, 'LIST', 1, 1, 0, 'kg'),
    (789012, 2, 'MIX', 1, 1, 0, 'kg'),
    (890123, 7, NULL , 1, 1, 0, 'kg'),
    (1234567, 3, NULL , 1, 1, 0, 'kg'),
    (2345678, 2, NULL , 1, 1, 1, 'kg'),
    (3456789, 2, 'FAIL', 1, 1, 0, 'kg'),
    (4567890, 3, NULL , 1, 1, 1, 'kg'),
    (5678901, 9, NULL , 1, 0, 0, 'kg'),
    (6789012, 15, NULL , 1, 0, 1, 'kg'),
    (7890123, 7, NULL, 1, 1, 1, 'kg'),
    (8901234, 1, NULL, 1, 0, 1, 'kg'),
    (9012345, 1, NULL, 1, 0, 1, 'kg'),
    (9876543, 10, NULL, 1, 1, 0, 'kg')



select *
From materialNumber