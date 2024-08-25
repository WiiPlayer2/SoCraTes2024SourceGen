//HintName: Foooo.Baaaar.Foo.g.cs
using System;
namespace Foooo.Baaaar;
internal static class FooMatcher
{
   public static T Match<T>(this Foo v, Func<T> onBar) =>
       onBar();
}