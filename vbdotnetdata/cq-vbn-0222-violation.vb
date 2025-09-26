' VB.NET test file for cq-vbn-0222: Use XmlReader for Deserialize
' This rule detects XML deserialization without using XmlReader for security

Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

Public Class XmlDeserializationViolations
    
    ' Violation: Deserialize with Stream without XmlReader
    Public Sub DeserializeFromStream()
        Dim serializer As New XmlSerializer(GetType(String))
        Dim stream As New FileStream("data.xml", FileMode.Open)
        
        ' Violation: Using Stream directly without XmlReader
        Dim result = serializer.Deserialize(stream)
        
        stream.Close()
    End Sub
    
    ' Violation: Deserialize with String parameter
    Public Sub DeserializeFromString()
        Dim serializer As New XmlSerializer(GetType(Object))
        Dim xmlString As String = "<root><data>value</data></root>"
        
        ' Violation: Using String directly for deserialization
        Dim result = serializer.Deserialize(xmlString)
    End Sub
    
    ' Violation: Multiple deserializations with Stream
    Public Sub MultipleDeserializations()
        Dim serializer1 As New XmlSerializer(GetType(String))
        Dim serializer2 As New XmlSerializer(GetType(Integer))
        
        Dim stream1 As New MemoryStream()
        Dim stream2 As New FileStream("file.xml", FileMode.Open)
        
        ' Violation: Multiple Stream deserializations
        Dim result1 = serializer1.Deserialize(stream1)
        Dim result2 = serializer2.Deserialize(stream2)
        
        stream1.Close()
        stream2.Close()
    End Sub
    
    ' Violation: Deserialize with TextReader
    Public Sub DeserializeFromTextReader()
        Dim serializer As New XmlSerializer(GetType(Object))
        Dim reader As New StringReader("<data>test</data>")
        
        ' Violation: Using TextReader without XmlReader
        Dim result = serializer.Deserialize(reader)
        
        reader.Close()
    End Sub
    
    ' Violation: Generic deserializer with Stream
    Public Sub GenericDeserialize(Of T)()
        Dim serializer As New XmlSerializer(GetType(T))
        Dim stream As New MemoryStream()
        
        ' Violation: Generic deserialization with Stream
        Dim result As T = CType(serializer.Deserialize(stream), T)
        
        stream.Close()
    End Sub
    
    ' Violation: Deserialize in try-catch block
    Public Sub DeserializeWithErrorHandling()
        Try
            Dim serializer As New XmlSerializer(GetType(String))
            Dim stream As New FileStream("config.xml", FileMode.Open)
            
            ' Violation: Stream deserialization in try block
            Dim config = serializer.Deserialize(stream)
            
            stream.Close()
        Catch ex As Exception
            Console.WriteLine("Deserialization failed: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: Deserialize with variable stream
    Public Sub DeserializeWithVariable()
        Dim serializer As New XmlSerializer(GetType(Object))
        Dim fileStream As Stream = New FileStream("data.xml", FileMode.Open)
        
        ' Violation: Variable stream deserialization
        Dim data = serializer.Deserialize(fileStream)
        
        fileStream.Dispose()
    End Sub
    
    ' Violation: Deserialize with method parameter
    Public Sub DeserializeFromParameter(inputStream As Stream)
        Dim serializer As New XmlSerializer(GetType(String))
        
        ' Violation: Parameter stream deserialization
        Dim result = serializer.Deserialize(inputStream)
    End Sub
    
    ' Violation: Multiple serializers with strings
    Public Sub DeserializeMultipleStrings()
        Dim serializer1 As New XmlSerializer(GetType(String))
        Dim serializer2 As New XmlSerializer(GetType(Integer))
        
        Dim xml1 As String = "<string>test</string>"
        Dim xml2 As String = "<int>123</int>"
        
        ' Violation: Multiple string deserializations
        Dim result1 = serializer1.Deserialize(xml1)
        Dim result2 = serializer2.Deserialize(xml2)
    End Sub
    
    ' Good example (should not be detected - uses XmlReader)
    Public Sub SecureDeserialize()
        Dim serializer As New XmlSerializer(GetType(String))
        Dim stream As New FileStream("data.xml", FileMode.Open)
        Dim xmlReader As XmlReader = XmlReader.Create(stream)
        
        ' Good: Using XmlReader for secure deserialization
        Dim result = serializer.Deserialize(xmlReader)
        
        xmlReader.Close()
        stream.Close()
    End Sub
    
    ' Good example (should not be detected - not deserialization)
    Public Sub SerializeData()
        Dim serializer As New XmlSerializer(GetType(String))
        Dim stream As New FileStream("output.xml", FileMode.Create)
        
        ' Good: This is serialization, not deserialization
        serializer.Serialize(stream, "test data")
        
        stream.Close()
    End Sub
    
    ' Violation: Deserialize with different stream types
    Public Sub DeserializeDifferentStreams()
        Dim serializer As New XmlSerializer(GetType(Object))
        
        ' Violation: MemoryStream deserialization
        Dim memStream As New MemoryStream()
        Dim result1 = serializer.Deserialize(memStream)
        
        ' Violation: FileStream deserialization
        Dim fileStream As New FileStream("test.xml", FileMode.Open)
        Dim result2 = serializer.Deserialize(fileStream)
        
        memStream.Close()
        fileStream.Close()
    End Sub
    
    ' Violation: Deserialize with string in conditional
    Public Sub ConditionalDeserialize(useString As Boolean)
        Dim serializer As New XmlSerializer(GetType(String))
        
        If useString Then
            Dim xmlData As String = GetXmlString()
            
            ' Violation: String deserialization in conditional
            Dim result = serializer.Deserialize(xmlData)
        End If
    End Sub
    
    Private Function GetXmlString() As String
        Return "<data>sample</data>"
    End Function
    
    ' Violation: Deserialize in loop
    Public Sub DeserializeInLoop()
        Dim serializer As New XmlSerializer(GetType(String))
        Dim files() As String = {"file1.xml", "file2.xml", "file3.xml"}
        
        For Each fileName As String In files
            Dim stream As New FileStream(fileName, FileMode.Open)
            
            ' Violation: Stream deserialization in loop
            Dim data = serializer.Deserialize(stream)
            
            stream.Close()
        Next
    End Sub
    
    ' Violation: Deserialize with return statement
    Public Function DeserializeAndReturn() As Object
        Dim serializer As New XmlSerializer(GetType(Object))
        Dim stream As New MemoryStream()
        
        ' Violation: Stream deserialization with return
        Return serializer.Deserialize(stream)
    End Function
End Class
