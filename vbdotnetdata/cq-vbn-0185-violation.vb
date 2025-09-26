' Test file for cq-vbn-0185: Do not use insecure deserializer NetDataContractSerializer
' This rule detects usage of NetDataContractSerializer which is insecure for deserializing untrusted data

Imports System
Imports System.IO
Imports System.Runtime.Serialization

Public Class NetDataContractSerializerViolations
    
    ' Violation: Creating new NetDataContractSerializer instance
    Public Sub CreateNetDataContractSerializer()
        Dim serializer As New NetDataContractSerializer()
        ' Some code here
    End Sub
    
    ' Violation: Using NetDataContractSerializer.Deserialize
    Public Sub DeserializeWithNetDataContractSerializer()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream)
    End Sub
    
    ' Violation: Using NetDataContractSerializer.ReadObject
    Public Sub ReadObjectWithNetDataContractSerializer()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream)
    End Sub
    
    ' Violation: NetDataContractSerializer creation and deserialization in same method
    Public Sub CreateAndDeserialize()
        Dim ndcs As New NetDataContractSerializer()
        Dim ms As New MemoryStream()
        Dim data = ndcs.Deserialize(ms)
    End Sub
    
    ' Violation: Multiple NetDataContractSerializer instances
    Public Sub MultipleNetDataContractSerializers()
        Dim serializer1 As New NetDataContractSerializer()
        Dim serializer2 As New NetDataContractSerializer()
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        Dim result1 = serializer1.Deserialize(stream1)
        Dim result2 = serializer2.ReadObject(stream2)
    End Sub
    
    ' Violation: NetDataContractSerializer in different contexts
    Public Function DeserializeData(data As Byte()) As Object
        Dim serializer As New NetDataContractSerializer()
        Using stream As New MemoryStream(data)
            Return serializer.Deserialize(stream)
        End Using
    End Function
    
    ' Violation: NetDataContractSerializer with field
    Private netDataSerializer As New NetDataContractSerializer()
    
    Public Sub UseFieldNetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = netDataSerializer.ReadObject(stream)
    End Sub
    
    ' Violation: NetDataContractSerializer in Try-Catch
    Public Sub DeserializeWithErrorHandling()
        Try
            Dim serializer As New NetDataContractSerializer()
            Dim stream As New MemoryStream()
            Dim result = serializer.Deserialize(stream)
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub
    
    ' Violation: NetDataContractSerializer with XmlReader
    Public Sub DeserializeWithXmlReader()
        Dim serializer As New NetDataContractSerializer()
        Dim reader As System.Xml.XmlReader = Nothing
        Dim result = serializer.ReadObject(reader)
    End Sub
    
    ' Violation: NetDataContractSerializer with XmlDictionaryReader
    Public Sub DeserializeWithXmlDictionaryReader()
        Dim serializer As New NetDataContractSerializer()
        Dim reader As System.Xml.XmlDictionaryReader = Nothing
        Dim result = serializer.ReadObject(reader, True)
    End Sub
    
    ' Non-violation: Safe serialization (should not be detected)
    Public Sub SafeSerialization()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        serializer.WriteObject(stream, "safe data")
    End Sub
    
    ' Non-violation: Other serializer types (should not be detected)
    Public Sub OtherSerializers()
        ' These should not trigger the rule
        Dim dataContractSerializer As New DataContractSerializer(GetType(String))
        Dim xmlSerializer As New System.Xml.Serialization.XmlSerializer(GetType(String))
    End Sub
    
    ' Non-violation: Comments and strings mentioning NetDataContractSerializer
    Public Sub CommentsAndStrings()
        ' This is a comment about NetDataContractSerializer
        Dim message As String = "Do not use NetDataContractSerializer for untrusted data"
        Console.WriteLine("NetDataContractSerializer is insecure")
    End Sub
    
    ' Violation: NetDataContractSerializer in conditional
    Public Sub ConditionalNetDataContractSerializer(useSerializer As Boolean)
        If useSerializer Then
            Dim serializer As New NetDataContractSerializer()
            Dim stream As New MemoryStream()
            Dim result = serializer.Deserialize(stream)
        End If
    End Sub
    
    ' Violation: NetDataContractSerializer in loop
    Public Sub NetDataContractSerializerInLoop()
        For i As Integer = 0 To 10
            Dim serializer As New NetDataContractSerializer()
            Dim stream As New MemoryStream()
            Dim result = serializer.ReadObject(stream)
        Next
    End Sub
    
    ' Violation: NetDataContractSerializer with variable assignment
    Public Sub AssignNetDataContractSerializer()
        Dim serializer As NetDataContractSerializer
        serializer = New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream)
    End Sub
    
    ' Violation: NetDataContractSerializer with constructor parameters
    Public Sub NetDataContractSerializerWithParameters()
        Dim serializer As New NetDataContractSerializer("rootName", "rootNamespace")
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream)
    End Sub
    
    ' Violation: NetDataContractSerializer ReadObject with verify parameter
    Public Sub ReadObjectWithVerify()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream, False)
    End Sub
    
    ' Violation: Multiple ReadObject calls
    Public Sub MultipleReadObjectCalls()
        Dim serializer As New NetDataContractSerializer()
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        Dim result1 = serializer.ReadObject(stream1)
        Dim result2 = serializer.ReadObject(stream2)
    End Sub

End Class
