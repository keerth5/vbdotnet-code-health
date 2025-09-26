' Test file for cq-vbn-0187: Ensure NetDataContractSerializer.Binder is set before deserializing
' This rule detects when NetDataContractSerializer is created and Deserialize/ReadObject is called without setting Binder

Imports System
Imports System.IO
Imports System.Runtime.Serialization

Public Class NetDataContractSerializerBinderEnsureViolations
    
    ' Violation: Create NetDataContractSerializer and Deserialize without setting Binder
    Public Sub CreateAndDeserializeWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream) ' Violation: No Binder set
    End Sub
    
    ' Violation: Create NetDataContractSerializer and ReadObject without setting Binder
    Public Sub CreateAndReadObjectWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream) ' Violation: No Binder set
    End Sub
    
    ' Violation: Multiple operations without Binder
    Public Sub MultipleOperationsWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        
        ' Some other operations
        Dim settings = serializer.Settings
        
        Dim result1 = serializer.Deserialize(stream1) ' Violation
        Dim result2 = serializer.ReadObject(stream2) ' Violation
    End Sub
    
    ' Violation: Create in loop and deserialize without Binder
    Public Sub CreateInLoopWithoutBinder()
        For i As Integer = 0 To 5
            Dim serializer As New NetDataContractSerializer()
            Dim stream As New MemoryStream()
            Dim result = serializer.ReadObject(stream) ' Violation
        Next
    End Sub
    
    ' Violation: Create conditionally and deserialize without Binder
    Public Sub CreateConditionallyWithoutBinder(condition As Boolean)
        If condition Then
            Dim serializer As New NetDataContractSerializer()
            Dim stream As New MemoryStream()
            Dim result = serializer.Deserialize(stream) ' Violation
        End If
    End Sub
    
    ' Violation: Try-catch with create and deserialize without Binder
    Public Sub TryCatchCreateWithoutBinder()
        Try
            Dim serializer As New NetDataContractSerializer()
            Dim stream As New MemoryStream()
            Dim result = serializer.ReadObject(stream) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Using statement with create and deserialize without Binder
    Public Sub UsingStatementWithoutBinder()
        Using stream As New MemoryStream()
            Dim serializer As New NetDataContractSerializer()
            Dim result = serializer.Deserialize(stream) ' Violation
        End Using
    End Sub
    
    ' Violation: Method with return and no Binder
    Public Function DeserializeDataWithoutBinder(data As Byte()) As Object
        Dim serializer As New NetDataContractSerializer()
        Using stream As New MemoryStream(data)
            Return serializer.ReadObject(stream) ' Violation
        End Using
    End Function
    
    ' Violation: Create with parameters and deserialize without Binder
    Public Sub CreateWithParametersWithoutBinder()
        Dim serializer As New NetDataContractSerializer("root", "namespace")
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream) ' Violation
    End Sub
    
    ' Violation: ReadObject with XmlReader without Binder
    Public Sub CreateAndReadObjectXmlReaderWithoutBinder()
        Dim serializer As New NetDataContractSerializer()
        Dim reader As System.Xml.XmlReader = Nothing
        Dim result = serializer.ReadObject(reader) ' Violation
    End Sub
    
    ' Non-violation: Create NetDataContractSerializer and set Binder before Deserialize
    Public Sub CreateAndDeserializeWithBinder()
        Dim serializer As New NetDataContractSerializer()
        serializer.Binder = New CustomSerializationBinder()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream) ' No violation
    End Sub
    
    ' Non-violation: Create NetDataContractSerializer and only WriteObject
    Public Sub CreateAndWriteObjectOnly()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        serializer.WriteObject(stream, "data") ' No violation - only writing
    End Sub
    
    ' Non-violation: Binder set immediately after creation
    Public Sub BinderSetImmediately()
        Dim serializer As New NetDataContractSerializer()
        serializer.Binder = New CustomSerializationBinder()
        ' Some other operations
        Dim stream As New MemoryStream()
        Dim result = serializer.Deserialize(stream) ' No violation
    End Sub
    
    ' Violation: Binder set after ReadObject call
    Public Sub BinderSetTooLate()
        Dim serializer As New NetDataContractSerializer()
        Dim stream As New MemoryStream()
        Dim result = serializer.ReadObject(stream) ' Violation - Binder not set yet
        serializer.Binder = New CustomSerializationBinder() ' Too late
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This creates a New NetDataContractSerializer() and calls ReadObject
        Dim message As String = "Dim serializer As New NetDataContractSerializer() then serializer.ReadObject(stream)"
        Console.WriteLine("Always set Binder before ReadObject")
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
    
    ' Violation: Select Case with creation and deserialization
    Public Sub SelectCaseWithoutBinder(option As Integer)
        Select Case option
            Case 1
                Dim serializer As New NetDataContractSerializer()
                Dim stream As New MemoryStream()
                Dim result = serializer.ReadObject(stream) ' Violation
            Case 2
                Dim serializer2 As New NetDataContractSerializer()
                Dim stream2 As New MemoryStream()
                Dim result2 = serializer2.Deserialize(stream2) ' Violation
        End Select
    End Sub

End Class

' Helper class for testing
Public Class CustomSerializationBinder
    Inherits SerializationBinder
    
    Public Overrides Function BindToType(assemblyName As String, typeName As String) As Type
        Return Nothing
    End Function
End Class
