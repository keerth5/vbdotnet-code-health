' Test file for cq-vbn-0064: Runtime marshalling disabled
' Rule should detect DisableRuntimeMarshalling assembly attribute usage

Imports System
Imports System.Runtime.InteropServices

' Violation 1: DisableRuntimeMarshalling assembly attribute
<Assembly: DisableRuntimeMarshalling>

' This should NOT be detected - regular assembly attributes
<Assembly: System.Reflection.AssemblyTitle("Test Assembly")>
<Assembly: System.Reflection.AssemblyVersion("1.0.0.0")>

Public Class RuntimeMarshallingExamples
    
    ' This should NOT be detected - regular P/Invoke without marshalling issues
    <DllImport("user32.dll")>
    Public Shared Function MessageBox(hWnd As IntPtr, text As String, caption As String, type As UInteger) As Integer
    End Function
    
    ' This should NOT be detected - regular method
    Public Sub RegularMethod()
        Console.WriteLine("Regular method")
    End Sub
    
    ' This should NOT be detected - COM interop without marshalling disabled
    <ComImport>
    <Guid("12345678-1234-1234-1234-123456789012")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IComInterface
        Sub ComMethod()
    End Interface
    
End Class

' This should NOT be detected - regular class
Public Class RegularClass
    
    Public Sub DoSomething()
        Console.WriteLine("Doing something")
    End Sub
    
End Class

' This should NOT be detected - structure without marshalling concerns
Public Structure RegularStructure
    Public Value As Integer
    Public Name As String
End Structure

Module TestModule
    
    ' This should NOT be detected - regular module method
    Public Sub ModuleMethod()
        Console.WriteLine("Module method")
    End Sub
    
End Module
