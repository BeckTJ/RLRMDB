CREATE OR ALTER PROCEDURE Distillation.StartRunLog(
    @materialNumber INT,
    @batchNumber INT,
    @processOrder NUMERIC,
    @receiver VARCHAR(5),
    @startDate DATE)
AS
    BEGIN

        DECLARE @productLotNumber VARCHAR(7)
        DECLARE @receiverId INT
        DECLARE @carbonDrumChangeOut BIT
        
        SET @productLotNumber = (SELECT Distillation.SetProductIdNumber(@materialNumber,DEFAULT))

        SET @receiverId = (SELECT ReceiverId
                            FROM Distillation.Receiver
                            WHERE ReceiverName = @receiver)

        SET @carbonDrumChangeOut = (SELECT Distillation.CheckCarbonDrum(@materialNumber))
        IF @carbonDrumChangeOut = 0
        BEGIN
            INSERT INTO Distillation.Production(ProductLotNumber,MaterialNumber,ProductBatchNumber,ProcessOrder,ReceiverId, StartDate)
            VALUES(@productLotNumber,@materialNumber, @batchNumber, @processOrder, @receiverId, @startDate)
        DECLARE @daysOn INT
        SET @daysOn = (SELECT Distillation.GetDaysOnCarbonDrum(@materialNumber))

        END
        ELSE
        BEGIN
            PRINT 'Change the Drum'
        END
    END