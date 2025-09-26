' Test file for cq-vbn-0181: Do not use insecure deserializer BinaryFormatter
' This rule detects usage of BinaryFormatter which is insecure for deserializing untrusted data

Imports System
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class BinaryFormatterSecurityViolations
    
    ' Violation: Creating new BinaryFormatter instance
    Public Sub CreateBinaryFormatter()
        Dim formatter As New BinaryFormatter()
        ' Some code here
    End Sub
    
    ' Violation: Using BinaryFormatter.Deserialize
    Public Sub DeserializeWithBinaryFormatter()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream)
    End Sub
    
    ' Violation: Using BinaryFormatter.UnsafeDeserialize
    Public Sub UnsafeDeserializeWithBinaryFormatter()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.UnsafeDeserialize(stream, Nothing)
    End Sub
    
    ' Violation: BinaryFormatter creation and deserialization in same method
    Public Sub CreateAndDeserialize()
        Dim bf As New BinaryFormatter()
        Dim ms As New MemoryStream()
        Dim data = bf.Deserialize(ms)
    End Sub
    
    ' Violation: Multiple BinaryFormatter instances
    Public Sub MultipleBinaryFormatters()
        Dim formatter1 As New BinaryFormatter()
        Dim formatter2 As New BinaryFormatter()
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        Dim result1 = formatter1.Deserialize(stream1)
        Dim result2 = formatter2.Deserialize(stream2)
    End Sub
    
    ' Violation: BinaryFormatter in different contexts
    Public Function DeserializeData(data As Byte()) As Object
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream(data)
            Return formatter.Deserialize(stream)
        End Using
    End Function
    
    ' Violation: BinaryFormatter with field
    Private binaryFormatter As New BinaryFormatter()
    
    Public Sub UseFieldBinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = binaryFormatter.Deserialize(stream)
    End Sub
    
    ' Violation: BinaryFormatter in Try-Catch
    Public Sub DeserializeWithErrorHandling()
        Try
            Dim formatter As New BinaryFormatter()
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub
    
    ' Non-violation: Safe serialization (should not be detected)
    Public Sub SafeSerialization()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        formatter.Serialize(stream, "safe data")
    End Sub
    
    ' Non-violation: Other formatter types (should not be detected)
    Public Sub OtherFormatters()
        ' These should not trigger the rule
        Dim xmlFormatter As New System.Xml.Serialization.XmlSerializer(GetType(String))
        Dim jsonFormatter As String = "JsonSerializer"
    End Sub
    
    ' Non-violation: Comments and strings mentioning BinaryFormatter
    Public Sub CommentsAndStrings()
        ' This is a comment about BinaryFormatter
        Dim message As String = "Do not use BinaryFormatter for untrusted data"
        Console.WriteLine("BinaryFormatter is insecure")
    End Sub
    
    ' Violation: BinaryFormatter in conditional
    Public Sub ConditionalBinaryFormatter(useFormatter As Boolean)
        If useFormatter Then
            Dim formatter As New BinaryFormatter()
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
        End If
    End Sub
    
    ' Violation: BinaryFormatter in loop
    Public Sub BinaryFormatterInLoop()
        For i As Integer = 0 To 10
            Dim formatter As New BinaryFormatter()
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
        Next
    End Sub
    
    ' Violation: BinaryFormatter with variable assignment
    Public Sub AssignBinaryFormatter()
        Dim formatter As BinaryFormatter
        formatter = New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream)
    End Sub

End Class
