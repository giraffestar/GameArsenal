using GameArsenal;

namespace GameArsenalTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.AreEqual(2, AnyTest.Add(1, 1));
    }
}