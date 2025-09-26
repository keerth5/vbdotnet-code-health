' Test file for cq-vbn-0168: Do not define finalizers for types derived from MemoryManager<T>
' Adding a finalizer to a type derived from MemoryManager<T> may permit memory to be freed while it is still in use by a Span<T>

Imports System
Imports System.Buffers

' Violation: Class inheriting from MemoryManager with finalizer
Public Class CustomMemoryManager
    Inherits MemoryManager(Of Byte)
    
    Private _buffer As Byte()
    
    Public Sub New(size As Integer)
        _buffer = New Byte(size - 1) {}
    End Sub
    
    ' Violation: Finalizer in MemoryManager-derived class
    Protected Overrides Sub Finalize()
        Try
            Console.WriteLine("CustomMemoryManager finalizer called")
            ' This is dangerous - memory might still be in use by Span<T>
            _buffer = Nothing
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    Public Overrides Function GetSpan() As Span(Of Byte)
        Return New Span(Of Byte)(_buffer)
    End Function
    
    Public Overrides Function Pin(elementIndex As Integer) As MemoryHandle
        Throw New NotSupportedException()
    End Function
    
    Public Overrides Sub Unpin()
        ' Implementation
    End Sub
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _buffer = Nothing
        End If
    End Sub
    
End Class

' Violation: Another MemoryManager with finalizer
Public Class StringMemoryManager
    Inherits MemoryManager(Of Char)
    
    Private _data As String
    
    Public Sub New(data As String)
        _data = data
    End Sub
    
    ' Violation: Finalizer in MemoryManager-derived class
    Protected Overrides Sub Finalize()
        Try
            Console.WriteLine("StringMemoryManager finalizer called")
            _data = Nothing
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    Public Overrides Function GetSpan() As Span(Of Char)
        Return _data.AsSpan()
    End Function
    
    Public Overrides Function Pin(elementIndex As Integer) As MemoryHandle
        Throw New NotSupportedException()
    End Function
    
    Public Overrides Sub Unpin()
        ' Implementation
    End Sub
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _data = Nothing
        End If
    End Sub
    
End Class

' Violation: Generic MemoryManager with finalizer
Public Class GenericMemoryManager(Of T)
    Inherits MemoryManager(Of T)
    
    Private _items As T()
    
    Public Sub New(items As T())
        _items = items
    End Sub
    
    ' Violation: Finalizer in generic MemoryManager-derived class
    Protected Overrides Sub Finalize()
        Try
            Console.WriteLine("GenericMemoryManager finalizer called")
            _items = Nothing
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    Public Overrides Function GetSpan() As Span(Of T)
        Return New Span(Of T)(_items)
    End Function
    
    Public Overrides Function Pin(elementIndex As Integer) As MemoryHandle
        Throw New NotSupportedException()
    End Function
    
    Public Overrides Sub Unpin()
        ' Implementation
    End Sub
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _items = Nothing
        End If
    End Sub
    
End Class

' Good practice: MemoryManager without finalizer (should not be detected)
Public Class SafeMemoryManager
    Inherits MemoryManager(Of Integer)
    
    Private _data As Integer()
    
    Public Sub New(size As Integer)
        _data = New Integer(size - 1) {}
    End Sub
    
    ' Good: No finalizer defined
    
    Public Overrides Function GetSpan() As Span(Of Integer)
        Return New Span(Of Integer)(_data)
    End Function
    
    Public Overrides Function Pin(elementIndex As Integer) As MemoryHandle
        Throw New NotSupportedException()
    End Function
    
    Public Overrides Sub Unpin()
        ' Implementation
    End Sub
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _data = Nothing
        End If
    End Sub
    
End Class

' Violation: Complex MemoryManager with finalizer
Public Class ComplexMemoryManager
    Inherits MemoryManager(Of Double)
    
    Private _buffer As Double()
    Private _disposed As Boolean
    
    Public Sub New(capacity As Integer)
        _buffer = New Double(capacity - 1) {}
    End Sub
    
    ' Violation: Complex finalizer in MemoryManager-derived class
    Protected Overrides Sub Finalize()
        Try
            If Not _disposed Then
                Console.WriteLine("ComplexMemoryManager finalizer - disposing unmanaged resources")
                ' This is problematic - Span<T> might still reference this memory
                CleanupUnmanagedResources()
            End If
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    Private Sub CleanupUnmanagedResources()
        ' Simulate cleanup that could interfere with Span<T> usage
        _buffer = Nothing
        _disposed = True
    End Sub
    
    Public Overrides Function GetSpan() As Span(Of Double)
        If _disposed Then
            Throw New ObjectDisposedException(NameOf(ComplexMemoryManager))
        End If
        Return New Span(Of Double)(_buffer)
    End Function
    
    Public Overrides Function Pin(elementIndex As Integer) As MemoryHandle
        Throw New NotSupportedException()
    End Function
    
    Public Overrides Sub Unpin()
        ' Implementation
    End Sub
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Proper cleanup in Dispose
                _buffer = Nothing
            End If
            _disposed = True
        End If
    End Sub
    
End Class

' Violation: Friend class with MemoryManager and finalizer
Friend Class InternalMemoryManager
    Inherits MemoryManager(Of Byte)
    
    Private _data As Byte()
    
    Public Sub New(data As Byte())
        _data = data
    End Sub
    
    ' Violation: Finalizer in Friend MemoryManager-derived class
    Protected Overrides Sub Finalize()
        Try
            Console.WriteLine("InternalMemoryManager finalizer")
            _data = Nothing
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
    Public Overrides Function GetSpan() As Span(Of Byte)
        Return New Span(Of Byte)(_data)
    End Function
    
    Public Overrides Function Pin(elementIndex As Integer) As MemoryHandle
        Throw New NotSupportedException()
    End Function
    
    Public Overrides Sub Unpin()
        ' Implementation
    End Sub
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _data = Nothing
        End If
    End Sub
    
End Class

' Good: Regular class with finalizer (should not be detected)
Public Class RegularClassWithFinalizer
    
    Private _resource As String
    
    Public Sub New(resource As String)
        _resource = resource
    End Sub
    
    ' Good: Finalizer in regular class (not MemoryManager)
    Protected Overrides Sub Finalize()
        Try
            Console.WriteLine("RegularClassWithFinalizer finalizer")
            _resource = Nothing
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    
End Class

' Violation: Nested class inheriting from MemoryManager with finalizer
Public Class OuterClass
    
    Public Class NestedMemoryManager
        Inherits MemoryManager(Of Single)
        
        Private _values As Single()
        
        Public Sub New(values As Single())
            _values = values
        End Sub
        
        ' Violation: Finalizer in nested MemoryManager-derived class
        Protected Overrides Sub Finalize()
            Try
                Console.WriteLine("NestedMemoryManager finalizer")
                _values = Nothing
            Finally
                MyBase.Finalize()
            End Try
        End Sub
        
        Public Overrides Function GetSpan() As Span(Of Single)
            Return New Span(Of Single)(_values)
        End Function
        
        Public Overrides Function Pin(elementIndex As Integer) As MemoryHandle
            Throw New NotSupportedException()
        End Function
        
        Public Overrides Sub Unpin()
            ' Implementation
        End Sub
        
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                _values = Nothing
            End If
        End Sub
        
    End Class
    
End Class

' Test usage of the problematic classes
Public Class MemoryManagerUsageTests
    
    Public Sub TestCustomMemoryManager()
        Using manager As New CustomMemoryManager(1024)
            Dim span As Span(Of Byte) = manager.GetSpan()
            
            ' This span could be invalidated if the finalizer runs
            span(0) = 255
            
            Console.WriteLine($"First byte: {span(0)}")
        End Using
        
        ' Force garbage collection to potentially trigger finalizer
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    
    Public Sub TestStringMemoryManager()
        Using manager As New StringMemoryManager("Hello World")
            Dim span As Span(Of Char) = manager.GetSpan()
            
            ' This span could be invalidated if the finalizer runs
            Console.WriteLine($"First char: {span(0)}")
        End Using
    End Sub
    
    Public Sub TestGenericMemoryManager()
        Dim data() As Integer = {1, 2, 3, 4, 5}
        
        Using manager As New GenericMemoryManager(Of Integer)(data)
            Dim span As Span(Of Integer) = manager.GetSpan()
            
            ' This span could be invalidated if the finalizer runs
            Console.WriteLine($"First integer: {span(0)}")
        End Using
    End Sub
    
End Class
