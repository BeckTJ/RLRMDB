CREATE OR ALTER PROCEDURE Materials.InsertMaterialNumber(@materialNumber INT,@parentMaterialNumber INT,@batchManaged BIT,@requiresProcessOrder BIT,@unitOfIssue CHAR(2),@isRawMaterial BIT)
AS
BEGIN

    IF NOT EXISTS(SELECT 1 FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = @materialNumber)
    INSERT INTO Materials.MaterialNumber(MaterialNumber,ParentMaterialNumber,BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial)
    VALUES(@materialNumber,@parentMaterialNumber,@batchManaged,@requiresProcessOrder,@unitOfIssue,@isRawMaterial)

END
  