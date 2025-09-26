' Test file for cq-vbn-0273: Implement serialization constructors
' Rule should detect: ISerializable classes without proper serialization constructor

Imports System
Imports System.Runtime.Serialization

' VIOLATION: ISerializable class without serialization constructor
<Serializable>
Public Class BadSerializableClass1
    Implements ISerializable
    
    Private data As String
    
    Public Sub New()
        data = "default"
    End Sub
    
    Public Sub New(value As String)
        data = value
    End Sub
    
    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("data", data)
    End Sub
    
    ' Missing: Protected Sub New(info As SerializationInfo, context As StreamingContext)
    
End Class

' VIOLATION: Sealed ISerializable class without private serialization constructor
<Serializable>
Public NotInheritable Class BadSealedSerializableClass
    Implements ISerializable
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("value", value)
    End Sub
    
    ' Missing: Private Sub New(info As SerializationInfo, context As StreamingContext)
    
End Class

' GOOD: ISerializable class with proper protected serialization constructor - should NOT be flagged
<Serializable>
Public Class GoodSerializableClass1
    Implements ISerializable
    
    Private data As String
    
    Public Sub New()
        data = "default"
    End Sub
    
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        data = info.GetString("data")
    End Sub
    
    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("data", data)
    End Sub
    
End Class

' GOOD: Sealed ISerializable class with proper private serialization constructor - should NOT be flagged
<Serializable>
Public NotInheritable Class GoodSealedSerializableClass
    Implements ISerializable
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Private Sub New(info As SerializationInfo, context As StreamingContext)
        value = info.GetInt32("value")
    End Sub
    
    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("value", value)
    End Sub
    
End Class

' GOOD: Regular class without ISerializable - should NOT be flagged
Public Class RegularClass
    
    Private data As String
    
    Public Sub New(value As String)
        data = value
    End Sub
    
End Class
