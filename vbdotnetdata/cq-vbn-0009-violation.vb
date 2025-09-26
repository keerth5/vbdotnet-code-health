' Test file for cq-vbn-0009: Mark assemblies with assembly version
' This file should trigger violations for assemblies without AssemblyVersion attribute

Imports System
Imports System.Reflection

' Missing AssemblyVersion attribute - this should trigger violation
' Violation: Assembly without AssemblyVersion attribute
<Assembly: AssemblyTitle("Test Assembly Without Version")>
<Assembly: AssemblyDescription("Test assembly missing version info")>
<Assembly: AssemblyCompany("Test Company")>

' This assembly declaration should be detected but lacks AssemblyVersion
Module AssemblyInfo
    ' Assembly level attributes should be here
End Module

Public Class TestClassNoVersion
    Public Sub TestMethod()
        ' Some implementation
    End Sub
End Class
