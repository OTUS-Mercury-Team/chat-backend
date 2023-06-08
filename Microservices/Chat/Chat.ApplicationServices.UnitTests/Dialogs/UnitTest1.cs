using FluentAssertions;

namespace Chat.ApplicationServices.UnitTests.Dialogs;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var list1 = new List<int> { 1, 2, 3, 4 };
        var list2 = new List<int> { 1, 2, 3, 4 };

        list1.Should().HaveCount(4);
        list1.Should().HaveCountGreaterThan(3);
        list1.Should().NotBeNull();
        list1.Should().BeEquivalentTo(list2);
    }

    [Test]
    public void Test2()
    {
        var obj1 = new { Id = 1, Name = "Ivan" };
        var obj2 = new { Id = 2, Name = "Ivan2" };

        obj1.Should().NotBeNull();
        obj1.Should().NotBeSameAs(obj2);
    }
}