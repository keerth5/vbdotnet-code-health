' Test file for cq-vbn-0262: Disposable fields should be disposed
' Rule should detect: IDisposable classes with disposable fields not disposed in Dispose method

Imports System
Imports System.IO

' VIOLATION: Class implements IDisposable but doesn't dispose FileStream field
Public Class BadDisposableClass1
    Implements IDisposable
    
    Private fileStream As FileStream
    
    Public Sub New()
        fileStream = New FileStream("test.txt", FileMode.Create)
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Missing: fileStream.Dispose()
    End Sub
    
End Class

' VIOLATION: Class implements IDisposable but doesn't dispose StreamReader field
Public Class BadDisposableClass2
    Implements IDisposable
    
    Private reader As StreamReader
    Private writer As StreamWriter
    
    Public Sub New()
        reader = New StreamReader("input.txt")
        writer = New StreamWriter("output.txt")
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        reader.Dispose()
        ' Missing: writer.Dispose()
    End Sub
    
End Class

' GOOD: Class properly disposes all disposable fields - should NOT be flagged
Public Class GoodDisposableClass
    Implements IDisposable
    
    Private fileStream As FileStream
    Private reader As StreamReader
    
    Public Sub New()
        fileStream = New FileStream("test.txt", FileMode.Create)
        reader = New StreamReader("input.txt")
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        fileStream.Dispose()
        reader.Dispose()
    End Sub
    
End Class

' GOOD: Class without disposable fields - should NOT be flagged
Public Class NonDisposableClass
    Implements IDisposable
    
    Private value As Integer = 42
    Private name As String = "test"
    
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Nothing to dispose
    End Sub
    
End Class
