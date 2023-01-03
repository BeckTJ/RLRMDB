INSERT INTO Distillation.AlphabeticDate
    (MonthNumber, AlphabeticCode)
VALUES
    (1, 'A'),
    (2, 'B'),
    (3, 'C'),
    (4, 'D'),
    (5, 'E'),
    (6, 'F'),
    (7, 'G'),
    (8, 'H'),
    (9, 'J'),
    (10, 'K'),
    (11, 'L'),
    (12, 'M')

GO
INSERT into Distillation.ProductNumberSequence
    (SequenceIdStart, SequenceIdEnd)
VALUES(001, 099),
    (100, 199),
    (200, 299),
    (300, 399),
    (400, 499),
    (500, 599),
    (600, 699),
    (700, 799),
    (800, 899),
    (900, 999),
    (1000, 1999),
    (2000, 2999),
    (3000, 3999),
    (4000, 4999),
    (5000, 5999),
    (6000, 6999),
    (7000, 7999),
    (8000, 8999),
    (9000, 9999)
GO

INSERT into HumanResources.Employee (EmployeeId, FirstName, LastName)
    VALUES  ('LAS1234', 'John', 'Smith'),
            ('LAS2345', 'Jane', 'Smith'),
            ('LAS3456', 'Brian', 'Squire')