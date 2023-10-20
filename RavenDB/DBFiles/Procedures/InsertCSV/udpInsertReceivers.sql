drop table #tmpReceiver
CREATE TABLE #tmpReceiver(
    Id int PRIMARY KEY IDENTITY(1,1),
    MaterialName VARCHAR(10),
    MaterialNumber INT,
    ReceiverName VARCHAR(6),
    MaxReceiverLevel INT,
)

BULK INSERT #tmpReceiver FROM '..\..\usr\dbfiles\BuildFiles\ReceiverData.csv'
WITH
(
    FORMAT = 'csv',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    KEEPNULLS
)

INSERT INTO Engineering.SystemReceivers(ReceiverId,MaterialNumber,ReceiverName,MaxReceiverLevel)
SELECT Id,
(SELECT MaterialNumber FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = #tmpReceiver.MaterialNumber),
(SELECT ReceiverName FROM Engineering.Receiver WHERE Receiver.ReceiverName = #tmpReceiver.ReceiverName),
MaxReceiverLevel 
FROM #tmpReceiver

select * from Engineering.SystemReceivers