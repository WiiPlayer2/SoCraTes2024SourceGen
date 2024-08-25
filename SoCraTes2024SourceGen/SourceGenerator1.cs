using System.Collections.Immutable;

namespace SoCraTes2024SourceGen;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

internal record EnumInfo(
    string? Namespace,
    string Name,
    IReadOnlyList<string> Members);

[Generator]
public class SourceGenerator1 : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var enumInfoProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                (node, _) => node is EnumDeclarationSyntax,
                (syntaxContext, token) =>
                {
                    var declaredSymbol = (INamedTypeSymbol) syntaxContext.SemanticModel
                        .GetDeclaredSymbol(syntaxContext.Node, token)!;
                    var namespaceName = declaredSymbol.ContainingNamespace.IsGlobalNamespace
                        ? default
                        : declaredSymbol.ContainingNamespace?.ToString();
                    var name = declaredSymbol.Name;
                    var members = declaredSymbol.MemberNames.ToImmutableList();
                    return new EnumInfo(namespaceName, name, members);
                });
        context.RegisterSourceOutput(
            enumInfoProvider,
            (productionContext, info) =>
            {
                productionContext.AddSource(
                    $"{info.Namespace}.{info.Name}.g.cs",
                    info.Members.Count == 1
                        ? RenderSingleMemberEnumMatcher(info)
                        : RenderMultipleMemberEnumMatcher(info));
            });
    }

    private static string RenderMultipleMemberEnumMatcher(EnumInfo info)
    {
        var parameters = string.Join(", ",
            info.Members
                .Select(x => $"Func<T> on{x}"));
        var arms = string.Join(
            ",\n            ",
            info.Members
                .Select(x => $"{info.Name}.{x} => on{x}()"));
        return $$"""
              using System;
              {{(!string.IsNullOrEmpty(info.Namespace) ? $"namespace {info.Namespace};": string.Empty)}}
              internal static class {{info.Name}}Matcher
              {
                  public static T Match<T>(this {{info.Name}} v, {{parameters}}) =>
              #pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
                      v switch
              #pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
                      {
                          {{arms}}
                      };
              }
              """;
    }

    private static string RenderSingleMemberEnumMatcher(EnumInfo info) =>
        $$"""
          using System;
          {{(!string.IsNullOrEmpty(info.Namespace) ? $"namespace {info.Namespace};": string.Empty)}}
          internal static class {{info.Name}}Matcher
          {
             public static T Match<T>(this {{info.Name}} v, Func<T> on{{info.Members.First()}}) =>
                 on{{info.Members.First()}}();
          }
          """;
}
