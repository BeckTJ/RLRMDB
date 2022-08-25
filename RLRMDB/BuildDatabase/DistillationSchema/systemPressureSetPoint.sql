Create TABLE Distillation.SystemPressureSetPoint(
    SystemId INT PRIMARY KEY,
    SystemPressure INT,
    SystemDifferentialPressure INT,
    ReboilerPressure INT,
    PrefractionFlaskPressure INT,
    ReceiverPressure INT,
    ChillerRecircPressure INT,
    NitrogenBleedRate INT ,
    CondenserCoolantFlowRate INT,
    AftercoolerCoolantFlowRate INT,
    CondenserCoolantPressure INT,
    AftercoolerCoolantPressure INT,
    HighPurityVentPurge INT,
    ColumnDPLevelSensePurge INT,
    ReboilerLevelSensePurge INT,
    RecieverPurge INT,
    ContainmentPurge INT,
    ReboilerPurge INT ,
    PrefractionLevelSencePurge INT ,
    SystemPurge INT,
    N2ToCondenserCoolantTank INT,
    HeatedTankPurge INT,
    CondenserPurge INT,
    ReboilerContainmentPressure INT,
    FeedDrumVentPurge INT,
    WasteDrumVentPurge INT,
    VentHeaderPurge INT,
    ReliefHeaderVentPurge INT,
    ParticleCounterContainmentPressure INT,
    VacuumBreakLinePurge INT,
    VacuumPumpCasePurge INT
)