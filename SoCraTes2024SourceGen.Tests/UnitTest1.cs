namespace SoCraTes2024SourceGen.Tests;

[TestClass]
public class UnitTest1 : VerifySourceGenerator
{
    [TestMethod]
    public async Task Empty()
    {
        var source = string.Empty;

        await Verify(source);
    }

    [TestMethod]
    public async Task EnumWithOneMember()
    {
        var source = """
                     enum Foo
                     {
                        Bar,
                     }
                     """;

        await Verify(source);
    }

    [TestMethod]
    public async Task EnumInsideNestedNamespace()
    {
        var source = """
                     namespace Foooo.Baaaar;
                     
                     enum Foo
                     {
                        Bar,
                     }
                     """;

        await Verify(source);
    }

    [TestMethod]
    public async Task EnumWithTwoMembers()
    {
        var source = """
                     enum Foo
                     {
                        Bar,
                        BarBar,
                     }
                     """;

        await Verify(source);
    }

    [TestMethod]
    public async Task EnumWithThreeMembers()
    {
        var source = """
                     enum Foo
                     {
                        Bar,
                        BarBar,
                        BarBarBar,
                     }
                     """;

        await Verify(source);
    }
}
