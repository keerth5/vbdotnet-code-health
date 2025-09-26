' Test file for cq-vbn-0186: Do not deserialize without first setting NetDataContractSerializer.Binder
' This rule detects NetDataContractSerializer Deserialize/ReadObject calls without setting the Binder property

Imports System
Imports System.IO
Imports System.Runtime.Serialization

Public Class NetDataContractSerializerBinderViolations
    
    ' Violation: Deserialize without setting Binder
    Public Sub DeserializeWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream) ' Violation: No Binder set
    End Sub
    
    ' Violation: ReadObject without setting Binder
    Public Sub ReadObjectWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream) ' Violation: No Binder set
    End Sub
    
    ' Violation: Multiple deserialize calls without Binder
    Public Sub MultipleDeserializeWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        Dim result1 = serializer.Deserialize(stream1) ' Violation
        Dim result2 = serializer.ReadObject(stream2) ' Violation
    End Sub
    
    ' Violation: ReadObject in loop without Binder
    Public Sub ReadObjectInLoopWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        For i As Integer = 0 To 10
            Dim stream As New MemoryStream()
            Dim result = serializer.ReadObject(stream) ' Violation
        Next
    End Sub
    
    ' Violation: Deserialize in conditional without Binder
    Public Sub ConditionalDeserializeWithoutBinder(condition As Boolean)
        Dim serializer As New NetDataContractSerializer()
        If condition Then
            Dim stream As New MemoryStream()
            Dim result = serializer.Deserialize(stream) ' Violation
        End If
    End Sub
    
    ' Violation: Field serializer deserialize without Binder
    Private fieldSerializer As New NetDataContractSerializer()
    
    Public Sub UseFieldSerializerWithoutBinder()
        Dim stream As New MemoryStream()
        Dim result = fieldSerializer.ReadObject(stream) ' Violation
    End Sub
    
    ' Violation: Method parameter serializer without Binder
    Public Sub UseParameterSerializerWithoutBinder(serializer As NetDataContractSerializer)
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream) ' Violation
    End Sub
    
    ' Violation: Try-catch deserialize without Binder
    Public Sub TryCatchDeserializeWithoutBinder()
        Try
            Dim serializer As New NetDataContractSerializer()
            Dim stream As New MemoryStream()
            Dim result = serializer.ReadObject(stream) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: ReadObject with XmlReader without Binder
    Public Sub ReadObjectXmlReaderWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim reader As System.Xml.XmlReader = Nothing
        Dim result = serializer.ReadObject(reader) ' Violation
    End Sub
    
    ' Violation: ReadObject with verify parameter without Binder
    Public Sub ReadObjectWithVerifyWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream, False) ' Violation
    End Sub
    
    ' Non-violation: Deserialize with Binder set (should not be detected)
    Public Sub DeserializeWithBinder()
        Dim serializer As New NetDataContractSerializer()
        serializer.Binder = New CustomSerializationBinder()
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream) ' No violation
    End Sub
    
    ' Non-violation: WriteObject without Binder (should not be detected)
    Public Sub WriteObjectWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        serializer.WriteObject(stream, "data") ' No violation - write doesn't need Binder
    End Sub
    
    ' Non-violation: Binder set after creation but before deserialize
    Public Sub BinderSetBeforeDeserialize()
        Dim serializer As New NetDataContractSerializer()
        ' Some other code
        serializer.Binder = New CustomSerializationBinder()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream) ' No violation
    End Sub
    
    ' Violation: Binder set after deserialize call
    Public Sub BinderSetAfterDeserialize()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream) ' Violation - Binder set too late
        serializer.Binder = New CustomSerializationBinder()
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about serializer.Deserialize and Binder
        Dim message As String = "serializer.ReadObject requires Binder"
        Console.WriteLine("Set serializer.Binder before calling Deserialize")
    End Sub
    
    ' Violation: Using statement without Binder
    Public Sub UsingStatementWithoutBinder()
        Using stream As New MemoryStream()
            Dim serializer As New NetDataContractSerializer()
            Dim result = serializer.ReadObject(stream) ' Violation
        End Using
    End Sub
    
    ' Violation: Multiple serializers without Binder
    Public Sub MultipleSerializersWithoutBinder()
        Dim serializer1 As New NetDataContractSerializer()
        Dim serializer2 As New NetDataContractSerializer()
        
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        
        Dim result1 = serializer1.Deserialize(stream1) ' Violation
        Dim result2 = serializer2.ReadObject(stream2) ' Violation
    End Sub

End Class

' Helper class for testing
Public Class CustomSerializationBinder
    Inherits SerializationBinder
    
    Public Overrides Function BindToType(assemblyName As String, typeName As String) As Type
        Return Nothing
    End Function
End Class
