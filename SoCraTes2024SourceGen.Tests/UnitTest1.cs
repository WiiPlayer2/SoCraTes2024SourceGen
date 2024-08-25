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
    public async Task test()
    {
        var source = """
                     using System;
                     public partial class TestMe { }
                     """;

        await Verify(source);
    }
}
