using System.Runtime.CompilerServices;

namespace SoCraTes2024SourceGen.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Enable();
}
