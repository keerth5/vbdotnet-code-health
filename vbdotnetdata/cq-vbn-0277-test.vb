' Test file for cq-vbn-0277: Mark ISerializable types with SerializableAttribute
' Rule should detect: ISerializable classes not marked with Serializable attribute

Imports System
Imports System.Runtime.Serialization

' VIOLATION: ISerializable class without Serializable attribute
Public Class BadISerializableClass1
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

' VIOLATION: Friend ISerializable class without Serializable attribute
Friend Class BadISerializableClass2
    Implements ISerializable
    
    Private value As Integer
    Private name As String
    
    Public Sub New(v As Integer, n As String)
        value = v
        name = n
    End Sub
    
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        value = info.GetInt32("value")
        name = info.GetString("name")
    End Sub
    
    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("value", value)
        info.AddValue("name", name)
    End Sub
    
End Class

' GOOD: ISerializable class with Serializable attribute - should NOT be flagged
<Serializable>
Public Class GoodISerializableClass1
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

' GOOD: ISerializable class with Serializable attribute (Friend) - should NOT be flagged
<Serializable>
Friend Class GoodISerializableClass2
    Implements ISerializable
    
    Private value As Integer
    
    Public Sub New(v As Integer)
        value = v
    End Sub
    
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
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

' GOOD: Serializable class without ISerializable - should NOT be flagged
<Serializable>
Public Class SimpleSerializableClass
    
    Private data As String
    Private value As Integer
    
    Public Sub New(d As String, v As Integer)
        data = d
        value = v
    End Sub
    
End Class
