    DECLARE @materialNumber int
    declare @vendorName varchar(25)
    declare @sampleDate DATE
    DECLARE @newDrumId INT
    DECLARE @drumLotNumber AS VARCHAR(10)
    DECLARE @alphabeticDate AS CHAR(1)
    DECLARE @drumId VARCHAR(10)
    DECLARE @sequenceId INT

    set @materialNumber = 32716
    set @vendorName = 'Liquor Store'
    set @sampleDate = '2021-10-02'

    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM Distillation.AlphabeticDate
    WHERE MonthNumber = MONTH(GETDATE()));

    SET @drumId = (SELECT TOP(1) DrumLotNumber from Distillation.RawMaterial
    WHERE materialnumber = @materialNumber
    ORDER BY DrumLotNumber DESC)

        IF (@sampleDate = NULL)
    BEGIN
        SET @sampleDate = GETDATE()
    END

    IF (@drumId = NULL) OR (NOT EXISTS (SELECT TOP(1) DrumLotNumber from Distillation.RawMaterial
    WHERE materialnumber = @materialNumber
    ORDER BY DrumLotNumber DESC))
        BEGIN
        SET @sequenceId = (SELECT SequenceIdStart FROM Distillation.ProductNumberSequence 
                            JOIN Materials.MaterialId ON ProductNumberSequence.SequenceId = MaterialId.SequenceId
                            WHERE MaterialId.MaterialNumber = @materialNumber AND MaterialId.VendorName = @vendorName)

        SET @drumLotNumber = (SELECT CONCAT(@sequenceId, Material.RawMaterialCode,RIGHT(YEAR(@sampleDate),1),@alphabeticDate,FORMAT(@sampleDate,'dd'))
        FROM Materials.MaterialNumber
            JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
            JOIN Materials.Material ON Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
        WHERE MaterialNumber.MaterialNumber = @materialNumber)
        END

    ELSE
        BEGIN
        IF (LEN(@drumId)=10 OR LEN(@drumId)=6)
            BEGIN
            SET @newDrumId = CAST(LEFT(@drumId,4)AS INT)+1
            END
        ELSE
            BEGIN
            SET @newDrumId = CAST(LEFT(@drumId,3)AS INT)+1
            END

        SET @drumLotNumber = (SELECT CONCAT(@newdrumId, Material.RawMaterialCode,RIGHT(YEAR(@sampleDate),1),@alphabeticDate,FORMAT(@sampleDate,'dd')) AS 'Drum ID'
                                FROM Materials.MaterialNumber
                                    JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
                                    JOIN Materials.Material ON Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
                                WHERE MaterialNumber.MaterialNumber = @materialNumber)
        END

        select @drumLotNumber
