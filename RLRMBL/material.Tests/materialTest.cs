using System.Threading.Tasks;
using NUnit.Framework;
using RLRMBL;

namespace material.Test;

public class materialTest
{

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void veiwMaterial()
    {
        Product m = new Product();
        var result = m.getProductByMaterialNumber(45235, "Silabond");

        Assert.AreEqual(45235, result.number);
        StringAssert.Contains("TEOS", result.nameAbreviation);
        Assert.AreEqual("Silabond", result.vendor);

    }
    [Test]
    public void getMaterialFromDatabase()
    {
        Product p = new Product();
        var result = p.MaterialLookupByNameAbreviation("TEOS", "Silabond");

        Assert.AreEqual(45235, result.number);
        StringAssert.Contains("ER", result.rawMaterialCode);
        StringAssert.Contains("Silabond", result.vendor);
    }
    [Test]
    public void addMaterialToDatabase()
    {
        var number = 12345;
        var name = "Liquid Nitrogen";
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
        p.addNewMaterialToDatabase(number, name, nameAbreviation, productCode, rawMaterialCode, permitNumber, unitOfIssue, carbonDrumRequired, carbonDrumDaysAllowed, carbonDrumWeightAllowed, batchManaged, isRawMaterial, processOrderRequired, sequenceIdStart, vendor);

        var result = p.MaterialLookupByNameAbreviation("LIN", "Sivance");
        Assert.AreEqual(12345, result.number);
        StringAssert.Contains("AR", result.rawMaterialCode);
        StringAssert.Contains("Sivance", result.vendor);
    }
    [Test]
    public void updateMaterial()
    {
        int input = 12345;
        string vendor = "Sivance";
        Product p = new Product();
        p.getProductByMaterialNumber(input, vendor);
        p.updateMaterial(input);
    }
    [Test]
    public void removeMaterial()
    {
        Product p = new Product();
        p.removeMaterialFromDatabase(12345);
    }
}