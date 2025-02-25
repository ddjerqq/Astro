using Domain.Common;

namespace Test.Architecture.Common;

public sealed class StringExtTest
{
    [Test]
    [Parallelizable]
    public void TestToSnakeCase()
    {
        var snake = "HelloWorld".ToSnakeCase();
        Assert.That(snake, Is.EqualTo("hello_world"));
    }
}