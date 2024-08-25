//HintName: .Foo.g.cs
using System;

internal static class FooMatcher
{
    public static T Match<T>(this Foo v, Func<T> onBar, Func<T> onBarBar) =>
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
        v switch
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
        {
            Foo.Bar => onBar(),
            Foo.BarBar => onBarBar()
        };
}