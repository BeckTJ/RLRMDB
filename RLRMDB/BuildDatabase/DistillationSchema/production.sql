CREATE TABLE Distillation.Production
(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductLotNumber VARCHAR(10),
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    ProductBatchNumber INT,
    ProcessOrder NUMERIC,
    ReceiverId INT FOREIGN KEY REFERENCES Distillation.Receiver,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    StartDate DATE,
)
CREATE NONCLUSTERED INDEX IX_Production_ProductLotNumber
ON Distillation.Production(ProductLotNumber ASC)

CREATE NONCLUSTERED INDEX IX_Production_ProductionBatchNumber
ON Distillation.Production(ProductionBatchNumber ASC)

CREATE NONCLUSTERED INDEX IX_Production_ProcessOrder
ON Distillation.Production(ProcessOrder ASC)
