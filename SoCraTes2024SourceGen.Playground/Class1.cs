using System.Text.RegularExpressions;

namespace SoCraTes2024SourceGen.Playground;

public enum AwesomeEnum
{
    A,
}

public static class Class1
{
    public static void Problem()
    {
        var value = AwesomeEnum.A;

        value.Match(() => 1);
    }
}
