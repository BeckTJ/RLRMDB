using NUnit.Framework;
using RLRMBL;

namespace materialTest;

public class TestMaterial
{
    private material _material = new material();
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void MaterialFromDatabase()
    {
        material result = _material.getSingleMaterialFromDatabase(45235, 5);

        Assert.AreEqual(45235, result.number);
        Assert.AreEqual("TEOS", result.nameAbreviation);
        Assert.AreEqual(2000, result.sequenceIdStart);
        Assert.AreEqual(2999, result.sequenceIdEnd);
        Assert.AreEqual("Silabond", result.vendor);
    }

    [Test]
    public void MaterialToDatabase()
    {
        _material.addMaterialToDatabase(33333, "Liquid", "LIQD", "123ABC-9876", "AA", "BB", false, 0, 0, "NULL", true, true, "kg", false, "Silabond", 1000);

        material result = _material.getSingleMaterialFromDatabase(33333, 5);

        Assert.AreEqual(33333, result.number);
        Assert.AreEqual("LIQD", result.nameAbreviation);
        Assert.AreEqual(1000, result.sequenceIdStart);
        Assert.AreEqual("Silabond", result.vendor);
    }

}