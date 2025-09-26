' VB.NET test file for cq-vbn-0104: Properties should not return arrays
' Rule: Arrays that are returned by properties are not write-protected, even if the property 
' is read-only. To keep the array tamper-proof, the property must return a copy of the array.

Imports System
Imports System.Collections.Generic

Public Class ArrayPropertyExamples
    
    Private _data As Integer()
    Private _matrix As String()
    Private _values As Double()
    Private _items As Object()
    
    ' Violation: Public property returning array
    Public Property Data As Integer()
        Get
            Return _data
        End Get
        Set(value As Integer())
            _data = value
        End Set
    End Property
    
    ' Violation: Protected property returning array
    Protected Property Matrix As String()
        Get
            Return _matrix
        End Get
        Set(value As String())
            _matrix = value
        End Set
    End Property
    
    ' Violation: Friend property returning array
    Friend Property Values As Double()
        Get
            Return _values
        End Get
        Set(value As Double())
            _values = value
        End Set
    End Property
    
    ' Violation: Public ReadOnly property returning array
    Public ReadOnly Property Items As Object()
        Get
            Return _items
        End Get
    End Property
    
    ' Violation: Protected ReadOnly property returning array
    Protected ReadOnly Property Configuration As String()
        Get
            Return New String() {"config1", "config2", "config3"}
        End Get
    End Property
    
    ' Violation: Friend ReadOnly property returning array
    Friend ReadOnly Property DefaultValues As Integer()
        Get
            Return New Integer() {1, 2, 3, 4, 5}
        End Get
    End Property
    
    ' Violation: Property returning byte array
    Public Property Buffer As Byte()
        Get
            Return _buffer
        End Get
        Set(value As Byte())
            _buffer = value
        End Set
    End Property
    Private _buffer As Byte()
    
    ' Violation: Property returning char array
    Public ReadOnly Property Characters As Char()
        Get
            Return _text.ToCharArray()
        End Get
    End Property
    Private _text As String = "Hello World"
    
    ' Violation: Property returning custom type array
    Public Property Employees As Employee()
        Get
            Return _employees
        End Get
        Set(value As Employee())
            _employees = value
        End Set
    End Property
    Private _employees As Employee()
    
    ' Violation: Property returning generic array
    Public ReadOnly Property GenericItems As T()
        Get
            Return _genericItems
        End Get
    End Property
    Private _genericItems As T()
    
End Class

Public Class Employee
    Public Property Name As String
    Public Property Id As Integer
End Class

' More violation examples in different contexts

Public Class DataManager
    
    ' Violation: Static property returning array
    Public Shared ReadOnly Property DefaultSettings As String()
        Get
            Return New String() {"setting1", "setting2"}
        End Get
    End Property
    
    ' Violation: Property with complex array type
    Public Property ComplexData As Dictionary(Of String, Integer)()
        Get
            Return _complexData
        End Get
        Set(value As Dictionary(Of String, Integer)())
            _complexData = value
        End Set
    End Property
    Private _complexData As Dictionary(Of String, Integer)()
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperCollectionExamples
    
    ' Correct: Property returning List instead of array - should not be detected
    Public Property DataList As List(Of Integer)
        Get
            Return _dataList
        End Get
        Set(value As List(Of Integer))
            _dataList = value
        End Set
    End Property
    Private _dataList As List(Of Integer)
    
    ' Correct: Property returning IEnumerable - should not be detected
    Public ReadOnly Property Items As IEnumerable(Of String)
        Get
            Return _items.AsEnumerable()
        End Get
    End Property
    Private _items As String()
    
    ' Correct: Property returning single value - should not be detected
    Public Property Value As Integer
        Get
            Return _value
        End Get
        Set(value As Integer)
            _value = value
        End Set
    End Property
    Private _value As Integer
    
    ' Correct: Method returning array (not property) - should not be detected
    Public Function GetData() As Integer()
        Return _data
    End Function
    Private _data As Integer()
    
    ' Correct: Private property returning array - should not be detected by this pattern
    Private Property InternalData As String()
        Get
            Return _internalData
        End Get
        Set(value As String())
            _internalData = value
        End Set
    End Property
    Private _internalData As String()
    
End Class
