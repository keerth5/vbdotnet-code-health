' VB.NET test file for cq-vbn-0106: Remove empty finalizers
' Rule: Whenever you can, avoid finalizers because of the additional performance overhead that 
' is involved in tracking object lifetime. An empty finalizer incurs added overhead without any benefit.

Imports System

' Violation: Class with empty finalizer
Public Class EmptyFinalizerExample
    
    ' Violation: Empty finalizer (destructor)
    Protected Overrides Sub Finalize()
    End Sub
    
    Public Sub DoWork()
        ' Some work
    End Sub
    
End Class

' Violation: Another class with empty finalizer
Public Class ResourceManager
    
    Private _data As String
    
    Public Sub New()
        _data = "Initial data"
    End Sub
    
    ' Violation: Empty finalizer with no cleanup code
    Protected Overrides Sub Finalize()
    End Sub
    
    Public Sub ProcessData()
        Console.WriteLine(_data)
    End Sub
    
End Class

' Violation: Class with empty finalizer and other methods
Public Class CacheManager
    
    Private _cache As Dictionary(Of String, Object)
    
    Public Sub New()
        _cache = New Dictionary(Of String, Object)()
    End Sub
    
    Public Sub AddItem(key As String, value As Object)
        _cache(key) = value
    End Sub
    
    Public Function GetItem(key As String) As Object
        Return _cache(key)
    End Function
    
    ' Violation: Empty finalizer that does nothing
    Protected Overrides Sub Finalize()
    End Sub
    
End Class

' Violation: Class with empty finalizer containing only comments
Public Class LogManager
    
    ' Violation: Empty finalizer with only comments
    Protected Overrides Sub Finalize()
        ' TODO: Add cleanup code later
        ' Currently no cleanup needed
    End Sub
    
    Public Sub Log(message As String)
        Console.WriteLine(message)
    End Sub
    
End Class

' Violation: Class with empty finalizer containing only whitespace
Public Class ConfigManager
    
    ' Violation: Empty finalizer with whitespace
    Protected Overrides Sub Finalize()
        
        
    End Sub
    
    Public Property ConfigValue As String
    
End Class

' Non-violation examples (these should not be detected):

' Correct: Class with proper finalizer that does cleanup - should not be detected
Public Class ProperResourceManager
    
    Private _handle As IntPtr
    Private _disposed As Boolean = False
    
    ' Correct: Finalizer with actual cleanup code
    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Dispose managed resources
            End If
            
            ' Dispose unmanaged resources
            If _handle <> IntPtr.Zero Then
                ' Free unmanaged handle
                _handle = IntPtr.Zero
            End If
            
            _disposed = True
        End If
    End Sub
    
End Class

' Correct: Class without finalizer - should not be detected
Public Class SimpleClass
    
    Public Property Name As String
    
    Public Sub DoSomething()
        Console.WriteLine("Doing something")
    End Sub
    
End Class

' Correct: Class with non-empty finalizer - should not be detected
Public Class FileManager
    
    Private _fileStream As System.IO.FileStream
    
    ' Correct: Non-empty finalizer with cleanup
    Protected Overrides Sub Finalize()
        Try
            If _fileStream IsNot Nothing Then
                _fileStream.Close()
                _fileStream = Nothing
            End If
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    Public Sub OpenFile(fileName As String)
        _fileStream = New System.IO.FileStream(fileName, System.IO.FileMode.Open)
    End Sub
    
End Class

' Correct: Class implementing IDisposable properly - should not be detected
Public Class DisposableResource
    Implements IDisposable
    
    Private _disposed As Boolean = False
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Dispose managed resources
            End If
            _disposed = True
        End If
    End Sub
    
    ' This would be a violation if it were empty, but it's not
    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub
    
End Class
