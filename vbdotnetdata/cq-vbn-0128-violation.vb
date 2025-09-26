' VB.NET test file for cq-vbn-0128: Provide memory-based overrides of async methods when subclassing 'Stream'
' Rule: To improve performance, override the memory-based async methods when subclassing 'Stream'. Then implement the array-based methods in terms of the memory-based methods.

Imports System
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks

' Violation: Stream subclass without memory-based async method overrides
Public Class BasicCustomStream
    Inherits Stream
    
    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return False
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property Length As Long
        Get
            Return 0
        End Get
    End Property
    
    Public Overrides Property Position As Long
        Get
            Return 0
        End Get
        Set(value As Long)
            ' No-op
        End Set
    End Property
    
    Public Overrides Sub Flush()
        ' No-op
    End Sub
    
    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return 0
    End Function
    
    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return 0
    End Function
    
    Public Overrides Sub SetLength(value As Long)
        ' No-op
    End Sub
    
    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        ' No-op
    End Sub
    
End Class

' Violation: Stream subclass with only array-based async methods
Public Class ArrayBasedAsyncStream
    Inherits Stream
    
    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return False
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property Length As Long
        Get
            Return 0
        End Get
    End Property
    
    Public Overrides Property Position As Long
        Get
            Return 0
        End Get
        Set(value As Long)
            ' No-op
        End Set
    End Property
    
    Public Overrides Sub Flush()
        ' No-op
    End Sub
    
    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return 0
    End Function
    
    ' Violation: Only array-based async read method without memory-based override
    Public Overrides Async Function ReadAsync(buffer() As Byte, offset As Integer, count As Integer, cancellationToken As CancellationToken) As Task(Of Integer)
        Return Await Task.FromResult(0)
    End Function
    
    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return 0
    End Function
    
    Public Overrides Sub SetLength(value As Long)
        ' No-op
    End Sub
    
    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        ' No-op
    End Sub
    
    ' Violation: Only array-based async write method without memory-based override
    Public Overrides Async Function WriteAsync(buffer() As Byte, offset As Integer, count As Integer, cancellationToken As CancellationToken) As Task
        Await Task.CompletedTask
    End Function
    
End Class

' Violation: Stream subclass with incomplete memory-based overrides
Public Class IncompleteMemoryBasedStream
    Inherits Stream
    
    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return False
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property Length As Long
        Get
            Return 0
        End Get
    End Property
    
    Public Overrides Property Position As Long
        Get
            Return 0
        End Get
        Set(value As Long)
            ' No-op
        End Set
    End Property
    
    Public Overrides Sub Flush()
        ' No-op
    End Sub
    
    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return 0
    End Function
    
    ' Has memory-based read but missing memory-based write - still a violation
    Public Overrides Async Function ReadAsync(buffer As ReadOnlyMemory(Of Byte), cancellationToken As CancellationToken) As ValueTask(Of Integer)
        Return New ValueTask(Of Integer)(0)
    End Function
    
    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return 0
    End Function
    
    Public Overrides Sub SetLength(value As Long)
        ' No-op
    End Sub
    
    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        ' No-op
    End Sub
    
    ' Violation: Only array-based async write method
    Public Overrides Async Function WriteAsync(buffer() As Byte, offset As Integer, count As Integer, cancellationToken As CancellationToken) As Task
        Await Task.CompletedTask
    End Function
    
End Class

' Violation: Another stream subclass without proper memory-based overrides
Public Class CustomFileStream
    Inherits Stream
    
    Private _position As Long = 0
    
    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property Length As Long
        Get
            Return 1024
        End Get
    End Property
    
    Public Overrides Property Position As Long
        Get
            Return _position
        End Get
        Set(value As Long)
            _position = value
        End Set
    End Property
    
    Public Overrides Sub Flush()
        ' Simulate flush operation
    End Sub
    
    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        ' Simulate read operation
        Return Math.Min(count, 10)
    End Function
    
    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Select Case origin
            Case SeekOrigin.Begin
                _position = offset
            Case SeekOrigin.Current
                _position += offset
            Case SeekOrigin.End
                _position = Length + offset
        End Select
        Return _position
    End Function
    
    Public Overrides Sub SetLength(value As Long)
        ' Simulate set length
    End Sub
    
    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        ' Simulate write operation
        _position += count
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperMemoryBasedStream
    Inherits Stream
    
    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return False
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property Length As Long
        Get
            Return 0
        End Get
    End Property
    
    Public Overrides Property Position As Long
        Get
            Return 0
        End Get
        Set(value As Long)
            ' No-op
        End Set
    End Property
    
    Public Overrides Sub Flush()
        ' No-op
    End Sub
    
    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return 0
    End Function
    
    ' Correct: Memory-based async read method - should not be detected
    Public Overrides Async Function ReadAsync(buffer As Memory(Of Byte), cancellationToken As CancellationToken) As ValueTask(Of Integer)
        Return New ValueTask(Of Integer)(0)
    End Function
    
    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return 0
    End Function
    
    Public Overrides Sub SetLength(value As Long)
        ' No-op
    End Sub
    
    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        ' No-op
    End Sub
    
    ' Correct: Memory-based async write method - should not be detected
    Public Overrides Async Function WriteAsync(buffer As ReadOnlyMemory(Of Byte), cancellationToken As CancellationToken) As ValueTask
        Await Task.CompletedTask
    End Function
    
End Class

' Non-Stream class - should not be detected
Public Class NonStreamClass
    
    Public Async Function ReadAsync(buffer() As Byte, offset As Integer, count As Integer, cancellationToken As CancellationToken) As Task(Of Integer)
        Return Await Task.FromResult(0)
    End Function
    
    Public Async Function WriteAsync(buffer() As Byte, offset As Integer, count As Integer, cancellationToken As CancellationToken) As Task
        Await Task.CompletedTask
    End Function
    
End Class
