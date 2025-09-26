' Test file for cq-vbn-0088: Identifiers should have correct prefix
' Rule should detect interfaces without 'I' prefix and type parameters without 'T' prefix

Imports System

' Violation 1: Interface without 'I' prefix
Public Interface Comparable
    Function CompareTo(other As Object) As Integer
End Interface

' Violation 2: Interface without 'I' prefix (Friend)
Friend Interface Serializable
    Sub Serialize()
    Sub Deserialize()
End Interface

' Violation 3: Interface without 'I' prefix (different context)
Public Interface Disposable
    Sub Dispose()
End Interface

' Violation 4: Interface without 'I' prefix
Friend Interface Cloneable
    Function Clone() As Object
End Interface

' Violation 5: Interface without 'I' prefix
Public Interface Drawable
    Sub Draw()
    Sub Clear()
End Interface

' This should NOT be detected - interface with proper 'I' prefix
Public Interface IComparable
    Function CompareTo(other As Object) As Integer
End Interface

' This should NOT be detected - interface with proper 'I' prefix
Friend Interface ISerializable
    Sub Serialize()
    Sub Deserialize()
End Interface

' This should NOT be detected - interface with proper 'I' prefix
Public Interface IDisposable
    Sub Dispose()
End Interface

' This should NOT be detected - interface with proper 'I' prefix
Public Interface ICloneable
    Function Clone() As Object
End Interface

' Generic class with type parameter violations
Public Class GenericContainer(Of Element)  ' Violation 6: Type parameter without 'T' prefix
    Private _items As New List(Of Element)
    
    Public Sub Add(item As Element)
        _items.Add(item)
    End Sub
    
    Public Function Get(index As Integer) As Element
        Return _items(index)
    End Function
End Class

' Generic class with multiple type parameter violations
Public Class Dictionary(Of Key, Value)  ' Violation 7: Type parameters without 'T' prefix
    Private _dict As New Dictionary(Of Key, Value)
    
    Public Sub Add(key As Key, value As Value)
        _dict.Add(key, value)
    End Sub
    
    Public Function TryGetValue(key As Key, ByRef value As Value) As Boolean
        Return _dict.TryGetValue(key, value)
    End Function
End Class

' Generic interface with type parameter violation
Public Interface Repository(Of Entity)  ' Violation 8: Type parameter without 'T' prefix
    Sub Save(entity As Entity)
    Function GetById(id As Integer) As Entity
    Function GetAll() As IEnumerable(Of Entity)
End Interface

' Generic class with mixed type parameter violations
Friend Class Cache(Of KeyType, ValueType)  ' Violation 9: Type parameters without 'T' prefix
    Private _cache As New Dictionary(Of KeyType, ValueType)
    
    Public Sub Set(key As KeyType, value As ValueType)
        _cache(key) = value
    End Sub
    
    Public Function Get(key As KeyType) As ValueType
        Return _cache(key)
    End Function
End Class

' This should NOT be detected - generic class with proper 'T' prefix
Public Class GenericList(Of T)
    Private _items As New List(Of T)
    
    Public Sub Add(item As T)
        _items.Add(item)
    End Sub
    
    Public Function Get(index As Integer) As T
        Return _items(index)
    End Function
End Class

' This should NOT be detected - generic class with proper 'T' prefix for multiple parameters
Public Class GenericDictionary(Of TKey, TValue)
    Private _dict As New Dictionary(Of TKey, TValue)
    
    Public Sub Add(key As TKey, value As TValue)
        _dict.Add(key, value)
    End Sub
    
    Public Function TryGetValue(key As TKey, ByRef value As TValue) As Boolean
        Return _dict.TryGetValue(key, value)
    End Function
End Class

' This should NOT be detected - generic interface with proper 'T' prefix
Public Interface IRepository(Of T)
    Sub Save(entity As T)
    Function GetById(id As Integer) As T
    Function GetAll() As IEnumerable(Of T)
End Interface

' Generic method with type parameter violations
Public Class UtilityClass
    
    ' Violation 10: Generic method with type parameter without 'T' prefix
    Public Function Convert(Of Source, Target)(source As Source) As Target
        ' Conversion logic
        Return Nothing
    End Function
    
    ' This should NOT be detected - generic method with proper 'T' prefix
    Public Function Transform(Of TSource, TTarget)(source As TSource) As TTarget
        ' Transformation logic
        Return Nothing
    End Function
    
End Class

' Violation 11: Another interface without 'I' prefix
Public Interface Validator
    Function Validate(obj As Object) As Boolean
End Interface

' Violation 12: Interface without 'I' prefix (different naming)
Friend Interface Processor
    Sub Process()
    Function GetResult() As Object
End Interface

' Violation 13: Interface without 'I' prefix
Public Interface Manager
    Sub Initialize()
    Sub Shutdown()
End Interface

' This should NOT be detected - proper interface naming
Public Interface IValidator
    Function Validate(obj As Object) As Boolean
End Interface

' This should NOT be detected - proper interface naming
Friend Interface IProcessor
    Sub Process()
    Function GetResult() As Object
End Interface

' This should NOT be detected - proper interface naming
Public Interface IManager
    Sub Initialize()
    Sub Shutdown()
End Interface

' Generic delegate with type parameter violation
Public Delegate Function Converter(Of Input, Output)(input As Input) As Output  ' Violation 14: Type parameters without 'T' prefix

' This should NOT be detected - generic delegate with proper 'T' prefix
Public Delegate Function GenericConverter(Of TInput, TOutput)(input As TInput) As TOutput

' This should NOT be detected - regular class (not interface or generic)
Public Class RegularClass
    Public Property Name As String
End Class

' This should NOT be detected - regular class
Friend Class DataProcessor
    Public Sub ProcessData()
        Console.WriteLine("Processing data")
    End Sub
End Class
