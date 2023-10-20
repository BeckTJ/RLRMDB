CREATE OR ALTER PROCEDURE Distillation.StartRunChecks(
    @vacuumTrapInstallDate DATE = NULL,
    @reboilerSkinTempBelowValue BIT = NULL,
    @knockOutPotDrained BIT = NULL,
    @heelsPumped BIT = NULL,
    @residualHeels INT = NULL,
    @HeliumOpen BIT = NULL,
    @HeliumCylinderPSI INT = NULL,
    @HeliumFlowPSI INT= NULL,
    @coolantLevel BIT = NULL,
    @coolantPurgeSet BIT = NULL,
    @nitrogenFlowRate INT = NULL,
    @nitrogenPurge INT = NULL,
    @heatingMantlePurgeSet INT = NULL,
    @nitrogenFlow INT = NULL,
    @aftercoolerPressure INT = NULL,
    @chillerSetting INT = NULL,
    @nitrogenToCondenserPurge INT = NULL,
    @secondaryPurgeSet BIT = NULL,
    @inspectLines BIT = NULL,
    @ControllerInitialSetBelowValue BIT = NULL
)
AS
