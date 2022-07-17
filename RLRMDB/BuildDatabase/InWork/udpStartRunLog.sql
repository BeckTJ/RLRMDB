CREATE OR ALTER PROCEDURE Distillation.StartRunLog(
    @materialNumber INT,
    @batchNumber INT,
    @processOrder NUMERIC,
    @receiver VARCHAR(5),
    @runNumber INT,
    @carbonDrumInstallDate DATE,
    @vacuumTrapInstallDate DATE,
    @reboilerSkinTempBelowValue BIT,
    @knockOutPotDrained BIT,
    @heelsPumped BIT,
    @residualHeels INT,
    @HeliumOpen BIT,
    @HeliumCylinderPSI INT,
    @HeliumFlowPSI INT,
    @coolantLevel BIT,
    @coolantPurgeSet BIT,
    @nitrogenFlowRate INT,
    @nitrogenPurge INT,
    @heatingMantlePurgeSet BIT,
    @nitrogenFlow INT,
    @aftercoolerPressure INT,
    @chillerSetting INT,
    @nitrogenToCondenserPurge INT,
    @secondaryPurgeSet BIT,
    @inspectLines BIT,
    @ControllerInitialSetBelowValue BIT)
AS
    BEGIN

        DECLARE @productLotNumber VARCHAR(7)
        DECLARE @receiverId INT
        DECLARE @startDate DATE
        DECLARE @carbonDrumDays INT

        SET @productLotNumber = (SELECT Distillation.SetProductIdNumber(@materialNumber,DEFAULT));

        SET @receiverId = (SELECT ReceiverId
                            FROM Distillation.Receiver
                            WHERE ReceiverName = @receiver)

        SET @carbonDrumDays = (SELECT Distillation.GetDaysOnCarbonDrum(@carbonDrumInstallDate))

    END