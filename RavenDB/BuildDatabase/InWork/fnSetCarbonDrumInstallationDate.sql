Update Materials.Material
SET CarbonDrumInstallDate = DATEADD(m,-2,GETDATE())
WHERE CarbonDrumRequired = 1

select * From Materials.Material Where CarbonDrumRequired = 1