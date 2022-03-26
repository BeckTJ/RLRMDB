using System;
using RLRMBL.Models;

namespace RLRMBL
{

    public class Program
    {
        static void Main(string[] args)
        {

            int number = 12345;
            string name = "Liquid Nitrogen";
            var nameAbreviation = "LIN";
            var productCode = "AB";
            var rawMaterialCode = "AR";
            var permitNumber = "123-ABC-1234";
            var unitOfIssue = "kg";
            var carbonDrumRequired = true;
            var carbonDrumDaysAllowed = 125;
            var carbonDrumWeightAllowed = 0;
            var batchManaged = false;
            var isRawMaterial = true;
            var processOrderRequired = false;
            var sequenceIdStart = 100;
            var vendor = "Sivance";

            Product p = new Product();
            p.addNewMaterialToDatabase(number,
                                       name,
                                       nameAbreviation,
                                       productCode,
                                       rawMaterialCode,
                                       permitNumber,
                                       unitOfIssue,
                                       carbonDrumRequired,
                                       carbonDrumDaysAllowed,
                                       carbonDrumWeightAllowed,
                                       batchManaged,
                                       isRawMaterial,
                                       processOrderRequired,
                                       sequenceIdStart,
                                       vendor);

            var r = p.MaterialLookupByNameAbreviation("LIN", "Sivance");
            Console.WriteLine($"{r.number}\t{r.nameAbreviation}\t {r.isRawMaterial}\t{r.unitOfIssue}");

            p.removeMaterialFromDatabase(12345);
        }
    }
}