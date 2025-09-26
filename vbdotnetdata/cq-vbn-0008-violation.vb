' Test file for cq-vbn-0008: Mark assemblies with CLSCompliant
' This file should trigger violations for assemblies without CLSCompliant attribute

Imports System
Imports System.Reflection

' Missing CLSCompliant attribute - this should trigger violation
' Violation: Assembly without CLSCompliant attribute
<Assembly: AssemblyTitle("Test Assembly")>
<Assembly: AssemblyDescription("Test assembly for VB.NET rules")>
<Assembly: AssemblyVersion("1.0.0.0")>

' This assembly declaration should be detected but lacks CLSCompliant
Module AssemblyInfo
    ' Assembly level attributes should be here
End Module

Public Class TestClass
    Public Sub TestMethod()
        ' Some implementation
    End Sub
End Class
