using System.Text.RegularExpressions;

namespace SoCraTes2024SourceGen.Playground;

public enum AwesomeEnum
{
    A,
    B,
    C,
}

public static class Class1
{
    public static int Match(this AwesomeEnum v, Func<int> onA, Func<int> onB, Func<int> onC) => v switch
    {
        AwesomeEnum.A => onA(),
        AwesomeEnum.B => onB(),
        AwesomeEnum.C => onC(),
    };
    
    public static void Problem()
    {
        var value = AwesomeEnum.A;

        var result = value switch
        {
            AwesomeEnum.A => 1,
            AwesomeEnum.B => 2,
            AwesomeEnum.C => 3,
        };

        var result2 = value.Match(
            () => 1,
            () => 2,
            () => 3
            );
    }
}
