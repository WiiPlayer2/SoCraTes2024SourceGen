//HintName: .Foo.g.cs
using System;

internal static class FooMatcher
{
   public static T Match<T>(this Foo v, Func<T> onBar) =>
       onBar();
} 
