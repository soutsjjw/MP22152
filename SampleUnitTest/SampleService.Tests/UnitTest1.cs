namespace SampleService.Tests;

[TestFixture]
public class Tests
{
    private DemoService _demoService;

    [SetUp]
    public void Setup()
    {
        _demoService = new DemoService();
    }

    [Test]
    public void Test1()
    {
        var result = _demoService.IsMoreThan2(1);

        Assert.IsFalse(result, "1 比 2 小");
    }

    [TestCase(0)]
    [TestCase(1)]
    public void IsMoreThan2_ValuesLessThan2_ReturnFalse(int value)
    {
        var result = _demoService.IsMoreThan2(value);

        Assert.IsFalse(result, $"{value} 比 2 小");
    }

    [TestCase(3)]
    [TestCase(4)]
    public void IsMoreThan2_ValuesLessThan2_ReturnTrue(int value)
    {
        var result = _demoService.IsMoreThan2(value);

        Assert.IsTrue(result, $"{value} 比 2 小");
    }
}