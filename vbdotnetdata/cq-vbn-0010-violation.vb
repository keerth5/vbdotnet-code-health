' Test file for cq-vbn-0010: Mark assemblies with ComVisible
' Rule should detect assemblies that need ComVisible attribute for COM interop clarity

Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' This assembly declaration should be detected - missing ComVisible attribute
<Assembly: AssemblyTitle("TestAssembly")>
<Assembly: AssemblyDescription("Test Description")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("Test Company")>
<Assembly: AssemblyProduct("Test Product")>
<Assembly: AssemblyCopyright("Copyright Test 2023")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>

' Additional assembly attributes that should be detected
<Assembly: AssemblyMetadata("Author", "Test Author")>
<Assembly: AssemblyInformationalVersion("1.0.0-beta")>

Public Class TestClass
    Public Sub TestMethod()
        ' Regular code - should not be detected
        Console.WriteLine("Assembly test")
    End Sub
End Class
