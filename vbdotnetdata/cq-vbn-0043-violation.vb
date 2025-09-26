' Test file for cq-vbn-0043: Do not raise exceptions in unexpected locations
' Rule should detect exceptions thrown in property getters, finalizers, etc.

Imports System

Public Class PropertyExceptionExamples
    
    Private _value As String
    Private _count As Integer
    
    ' Violation 1: Public property getter throwing exception
    Public Property Name As String
        Get
            If _value Is Nothing Then
                Throw New InvalidOperationException("Name not initialized")
            End If
            Return _value
        End Get
        Set(value As String)
            _value = value
        End Set
    End Property
    
    ' Violation 2: Protected ReadOnly property throwing exception
    Protected ReadOnly Property Count As Integer
        Get
            If _count < 0 Then
                Throw New ArgumentException("Count cannot be negative")
            End If
            Return _count
        End Get
    End Property
    
    ' Violation 3: Friend property getter throwing exception
    Friend Property Status As String
        Get
            If String.IsNullOrEmpty(_value) Then
                Throw New InvalidDataException("Status is not set")
            End If
            Return _value.ToUpper()
        End Get
        Set(value As String)
            _value = value
        End Set
    End Property
    
    ' This should NOT be detected - property setter throwing exception (acceptable)
    Public Property ValidatedValue As String
        Get
            Return _value
        End Get
        Set(value As String)
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            _value = value
        End Set
    End Property
    
    ' This should NOT be detected - method throwing exception (acceptable)
    Public Sub ProcessData()
        If _value Is Nothing Then
            Throw New InvalidOperationException("Data not available")
        End If
        Console.WriteLine(_value)
    End Sub
    
End Class

Public Class FinalizerExceptionExamples
    
    Private _resource As IntPtr
    
    ' Violation 4: Finalizer throwing exception
    Protected Overrides Sub Finalize()
        Try
            If _resource <> IntPtr.Zero Then
                ' This would be a violation if it throws
                Throw New InvalidOperationException("Cannot release resource")
            End If
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    ' This should NOT be detected - proper finalizer without exceptions
    Public Sub Dispose()
        If _resource <> IntPtr.Zero Then
            ' Proper cleanup without exceptions
            _resource = IntPtr.Zero
        End If
        GC.SuppressFinalize(Me)
    End Sub
    
End Class

Public Class EventHandlerExamples
    
    Private _data As String
    
    ' This should NOT be detected - event handler can throw exceptions
    Public Sub OnDataChanged(sender As Object, e As EventArgs)
        If _data Is Nothing Then
            Throw New InvalidOperationException("Data is null")
        End If
    End Sub
    
    ' This should NOT be detected - regular method
    Public Function CalculateValue() As Integer
        If _data Is Nothing Then
            Throw New ArgumentNullException(NameOf(_data))
        End If
        Return _data.Length
    End Function
    
End Class

Public Class IndexerExamples
    
    Private _items As String()
    
    Public Sub New()
        _items = New String(9) {}
    End Sub
    
    ' Violation 5: Indexer getter throwing exception (similar to property)
    Default Public Property Item(index As Integer) As String
        Get
            If index < 0 OrElse index >= _items.Length Then
                Throw New IndexOutOfRangeException("Index is out of range")
            End If
            Return _items(index)
        End Get
        Set(value As String)
            If index < 0 OrElse index >= _items.Length Then
                Throw New IndexOutOfRangeException("Index is out of range")
            End If
            _items(index) = value
        End Set
    End Property
    
End Class
