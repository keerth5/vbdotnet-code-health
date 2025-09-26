' Test file for cq-vbn-0041: Implement IDisposable correctly
' Rule should detect classes implementing IDisposable that may not follow the dispose pattern correctly

Imports System

' Violation 1: Public class implementing IDisposable
Public Class DisposableResource1
    Implements IDisposable
    
    Private _disposed As Boolean = False
    Private _resource As IntPtr
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Dispose managed resources
            End If
            ' Dispose unmanaged resources
            _disposed = True
        End If
    End Sub
    
End Class

' Violation 2: Friend class implementing IDisposable
Friend Class DisposableResource2
    Implements IDisposable
    
    Private _fileHandle As IntPtr
    
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Simple dispose implementation
        If _fileHandle <> IntPtr.Zero Then
            ' Close file handle
            _fileHandle = IntPtr.Zero
        End If
    End Sub
    
End Class

' Violation 3: Another public class implementing IDisposable
Public Class DatabaseConnection
    Implements IDisposable
    
    Private _connection As Object
    Private _disposed As Boolean
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
    
    Private Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                If _connection IsNot Nothing Then
                    ' Close connection
                    _connection = Nothing
                End If
            End If
            _disposed = True
        End If
    End Sub
    
End Class

' This should NOT be detected - private class (less critical for public API)
Private Class PrivateDisposableResource
    Implements IDisposable
    
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Private implementation
    End Sub
    
End Class

' This should NOT be detected - class not implementing IDisposable
Public Class RegularClass
    
    Private _data As String
    
    Public Sub New(data As String)
        _data = data
    End Sub
    
    Public Sub ProcessData()
        Console.WriteLine(_data)
    End Sub
    
End Class

' This should NOT be detected - proper IDisposable implementation with finalizer
Public Class ProperDisposableResource
    Implements IDisposable
    
    Private _disposed As Boolean = False
    Private _unmanagedResource As IntPtr
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Dispose managed resources
            End If
            ' Dispose unmanaged resources
            If _unmanagedResource <> IntPtr.Zero Then
                ' Free unmanaged resource
                _unmanagedResource = IntPtr.Zero
            End If
            _disposed = True
        End If
    End Sub
    
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    
End Class
