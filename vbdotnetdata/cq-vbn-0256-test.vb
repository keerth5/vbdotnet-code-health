' Test file for cq-vbn-0256: Call GC.SuppressFinalize correctly
' Rule should detect: Incorrect usage of GC.SuppressFinalize

Imports System

Public Class GCSuppressionTest
    Implements IDisposable
    
    ' VIOLATION: Dispose method without GC.SuppressFinalize(Me)
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose managed resources
        Console.WriteLine("Disposing resources")
    End Sub
    
    ' VIOLATION: GC.SuppressFinalize called with wrong parameter
    Public Sub BadDispose1() Implements IDisposable.Dispose
        Console.WriteLine("Disposing resources")
        GC.SuppressFinalize(Nothing)
    End Sub
    
    ' VIOLATION: GC.SuppressFinalize called with object other than Me
    Public Sub BadDispose2() Implements IDisposable.Dispose
        Dim obj As New Object()
        Console.WriteLine("Disposing resources")
        GC.SuppressFinalize(obj)
    End Sub
    
    ' GOOD: Dispose method with correct GC.SuppressFinalize(Me) - should NOT be flagged
    Public Sub GoodDispose() Implements IDisposable.Dispose
        Console.WriteLine("Disposing resources")
        GC.SuppressFinalize(Me)
    End Sub
    
    ' GOOD: Normal method - should NOT be flagged
    Public Sub NormalMethod()
        Console.WriteLine("Normal method")
    End Sub
    
End Class

' Another test class for different scenarios
Public Class AnotherGCTest
    Implements IDisposable
    
    ' VIOLATION: Non-Dispose method calling GC.SuppressFinalize
    Public Sub SomeOtherMethod()
        Console.WriteLine("Some method")
        GC.SuppressFinalize(Me)
    End Sub
    
    ' GOOD: Proper Dispose implementation
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If disposing Then
            ' Dispose managed resources
        End If
    End Sub
    
End Class
