' Test file for cq-vbn-0272: Collection properties should be read only
' Rule should detect: Writable collection properties that allow replacement

Imports System.Collections.Generic

' VIOLATION: Writable List property with setter
Public Class BadCollectionClass1
    
    Private _items As List(Of String)
    
    Public Sub New()
        _items = New List(Of String)()
    End Sub
    
    Public Property Items As List(Of String)
        Get
            Return _items
        End Get
        Set(value As List(Of String))
            _items = value
        End Set
    End Property
    
End Class

' VIOLATION: Writable Collection property with setter
Public Class BadCollectionClass2
    
    Private _data As Collection(Of Integer)
    
    Public Sub New()
        _data = New Collection(Of Integer)()
    End Sub
    
    Protected Property Data As Collection(Of Integer)
        Get
            Return _data
        End Get
        Set(value As Collection(Of Integer))
            _data = value
        End Set
    End Property
    
End Class

' VIOLATION: Writable Dictionary property with setter
Friend Class BadCollectionClass3
    
    Private _map As Dictionary(Of String, Integer)
    
    Public Sub New()
        _map = New Dictionary(Of String, Integer)()
    End Sub
    
    Public Property Map As Dictionary(Of String, Integer)
        Get
            Return _map
        End Get
        Set(value As Dictionary(Of String, Integer))
            _map = value
        End Set
    End Property
    
End Class

' GOOD: Read-only List property - should NOT be flagged
Public Class GoodCollectionClass1
    
    Private _items As List(Of String)
    
    Public Sub New()
        _items = New List(Of String)()
    End Sub
    
    Public ReadOnly Property Items As List(Of String)
        Get
            Return _items
        End Get
    End Property
    
End Class

' GOOD: Property with only getter - should NOT be flagged
Public Class GoodCollectionClass2
    
    Private _data As Collection(Of Integer)
    
    Public Sub New()
        _data = New Collection(Of Integer)()
    End Sub
    
    Public Property Data As Collection(Of Integer)
        Get
            Return _data
        End Get
    End Property
    
End Class

' GOOD: Non-collection property - should NOT be flagged
Public Class NonCollectionClass
    
    Private _name As String
    
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
    
End Class
