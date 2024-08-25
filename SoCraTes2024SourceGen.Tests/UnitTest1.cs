using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

namespace SoCraTes2024SourceGen.Tests;

[TestClass]
public class UnitTest1 : VerifySourceGenerator
{
    [TestMethod]
    public async Task Empty()
    {
        await Verify(string.Empty);
    }

    [TestMethod]
    public async Task EnumWithoutMembers()
    {
        await Verify("""
                     enum Foo
                     {
                     }
                     """);
    }

    [TestMethod]
    public async Task EnumWithOneMember()
    {
        await Verify("""
                     enum Foo
                     {
                        Bar,
                     }
                     """);
    }

    [TestMethod]
    public async Task EnumInsideNestedNamespace()
    {
        await Verify("""
                     namespace Foooo.Baaaar;

                     enum Foo
                     {
                        Bar,
                     }
                     """);
    }

    [TestMethod]
    public async Task EnumWithTwoMembers()
    {
        await Verify("""
                     enum Foo
                     {
                        Bar,
                        BarBar,
                     }
                     """);
    }

    [TestMethod]
    public async Task EnumWithThreeMembers()
    {
        await Verify("""
                     enum Foo
                     {
                        Bar,
                        BarBar,
                        BarBarBar,
                     }
                     """);
    }

    [TestMethod]
    public async Task EnumWithoutMembersRaisesWarning()
    {
        await Verify("""
                     enum Foo
                     {
                     }
                     """,
            (compilation, diagnostics) =>
            {
                Assert.IsTrue(diagnostics.Any(x =>
                    x.Severity == DiagnosticSeverity.Warning
                    && x.GetMessage() == "No matcher for zero member enums!"));
            });
    }
}
