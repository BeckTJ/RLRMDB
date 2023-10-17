CREATE TABLE Materials.Material
(
    MaterialNumber INT PRIMARY KEY,
    MaterialName VARCHAR(50) NOT NULL,
    MaterialNameAbreviation VARCHAR(15) NOT NULL,
    PermitNumber VARCHAR(25),
    UnitOfIssue VARCHAR(2) NOT NULL,
    BatchManaged BIT NOT NULL DEFAULT(0),
    MaterialCode VARCHAR(3) NOT NULL,
    SequenceId INT NOT NULL,
    TotalRecords INT NOT NULL,
)

CREATE TABLE Materials.MaterialVendor
(
    MaterialNumber INT PRIMARY KEY,
    VendorName VARCHAR(25) FOREIGN KEY REFERENCES Material.Vendor NOT NULL,
    ParentMaterialNumber INT FOREIGN KEY REFERENCES Materials.Material NOT NULL,
    MaterialCode VARCHAR(3) NOT NULL,
    SequenceId INT NOT NULL,
    TotalRecords INT NOT NULL,
    UnitOfIssue VARCHAR(2),
    BatchManaged BIT NOT NULL DEFAULT(0),
)

CREATE TABLE Engineering.SystemInformation
(
    MaterialNumber INT PRIMARY KEY,
    CarbonDrumRequired BIT DEFAULT(0) NOT NULL,
    CarbonDrumDaysAllowed INT,
    CarbonDrumWeightAllowed INT,
    CarbonDrumInstallDate DATE,
    VacuumTrapRequired BIT DEFAULT(0) NOT NULL,
    VacuumTrapInstallDate DATE,
    VacuumTrapDaysAllowed INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    HeelPumpFrequency INT,
)