' Test file for cq-vbn-0188: Do not use insecure deserializer ObjectStateFormatter
' This rule detects usage of ObjectStateFormatter which is insecure for deserializing untrusted data

Imports System
Imports System.IO
Imports System.Web.UI

Public Class ObjectStateFormatterSecurityViolations
    
    ' Violation: Creating new ObjectStateFormatter instance
    Public Sub CreateObjectStateFormatter()
        Dim formatter As New ObjectStateFormatter()
        ' Some code here
    End Sub
    
    ' Violation: Using ObjectStateFormatter.Deserialize
    Public Sub DeserializeWithObjectStateFormatter()
        Dim formatter As New ObjectStateFormatter()
        Dim data As String = "serialized_data"
        Dim result = formatter.Deserialize(data)
    End Sub
    
    ' Violation: ObjectStateFormatter creation and deserialization in same method
    Public Sub CreateAndDeserialize()
        Dim osf As New ObjectStateFormatter()
        Dim serializedData As String = "some_data"
        Dim obj = osf.Deserialize(serializedData)
    End Sub
    
    ' Violation: Multiple ObjectStateFormatter instances
    Public Sub MultipleObjectStateFormatters()
        Dim formatter1 As New ObjectStateFormatter()
        Dim formatter2 As New ObjectStateFormatter()
        Dim data1 As String = "data1"
        Dim data2 As String = "data2"
        Dim result1 = formatter1.Deserialize(data1)
        Dim result2 = formatter2.Deserialize(data2)
    End Sub
    
    ' Violation: ObjectStateFormatter in different contexts
    Public Function DeserializeData(data As String) As Object
        Dim formatter As New ObjectStateFormatter()
        Return formatter.Deserialize(data)
    End Function
    
    ' Violation: ObjectStateFormatter with field
    Private objectStateFormatter As New ObjectStateFormatter()
    
    Public Sub UseFieldObjectStateFormatter()
        Dim data As String = "serialized_data"
        Dim result = objectStateFormatter.Deserialize(data)
    End Sub
    
    ' Violation: ObjectStateFormatter in Try-Catch
    Public Sub DeserializeWithErrorHandling()
        Try
            Dim formatter As New ObjectStateFormatter()
            Dim data As String = "serialized_data"
            Dim result = formatter.Deserialize(data)
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub
    
    ' Violation: ObjectStateFormatter deserialize with stream
    Public Sub DeserializeWithStream()
        Dim formatter As New ObjectStateFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream)
    End Sub
    
    ' Violation: ObjectStateFormatter deserialize with TextReader
    Public Sub DeserializeWithTextReader()
        Dim formatter As New ObjectStateFormatter()
        Dim reader As New StringReader("serialized_data")
        Dim result = formatter.Deserialize(reader)
    End Sub
    
    ' Non-violation: Safe serialization (should not be detected)
    Public Sub SafeSerialization()
        Dim formatter As New ObjectStateFormatter()
        Dim serializedData As String = formatter.Serialize("safe data")
    End Sub
    
    ' Non-violation: Other formatter types (should not be detected)
    Public Sub OtherFormatters()
        ' These should not trigger the rule
        Dim xmlFormatter As New System.Xml.Serialization.XmlSerializer(GetType(String))
        Dim jsonFormatter As String = "JsonSerializer"
    End Sub
    
    ' Non-violation: Comments and strings mentioning ObjectStateFormatter
    Public Sub CommentsAndStrings()
        ' This is a comment about ObjectStateFormatter
        Dim message As String = "Do not use ObjectStateFormatter for untrusted data"
        Console.WriteLine("ObjectStateFormatter is insecure")
    End Sub
    
    ' Violation: ObjectStateFormatter in conditional
    Public Sub ConditionalObjectStateFormatter(useFormatter As Boolean)
        If useFormatter Then
            Dim formatter As New ObjectStateFormatter()
            Dim data As String = "serialized_data"
            Dim result = formatter.Deserialize(data)
        End If
    End Sub
    
    ' Violation: ObjectStateFormatter in loop
    Public Sub ObjectStateFormatterInLoop()
        For i As Integer = 0 To 10
            Dim formatter As New ObjectStateFormatter()
            Dim data As String = "serialized_data_" & i.ToString()
            Dim result = formatter.Deserialize(data)
        Next
    End Sub
    
    ' Violation: ObjectStateFormatter with variable assignment
    Public Sub AssignObjectStateFormatter()
        Dim formatter As ObjectStateFormatter
        formatter = New ObjectStateFormatter()
        Dim data As String = "serialized_data"
        Dim result = formatter.Deserialize(data)
    End Sub
    
    ' Violation: ObjectStateFormatter deserialize in Select Case
    Public Sub ObjectStateFormatterInSelectCase(option As Integer)
        Select Case option
            Case 1
                Dim formatter As New ObjectStateFormatter()
                Dim data As String = "option1_data"
                Dim result = formatter.Deserialize(data)
            Case 2
                Dim formatter2 As New ObjectStateFormatter()
                Dim data2 As String = "option2_data"
                Dim result2 = formatter2.Deserialize(data2)
        End Select
    End Sub
    
    ' Violation: ObjectStateFormatter with Using statement
    Public Sub ObjectStateFormatterWithUsing()
        Using writer As New StringWriter()
            Dim formatter As New ObjectStateFormatter()
            Dim data As String = "serialized_data"
            Dim result = formatter.Deserialize(data)
        End Using
    End Sub
    
    ' Violation: ObjectStateFormatter with While loop
    Public Sub ObjectStateFormatterInWhileLoop()
        Dim counter As Integer = 0
        While counter < 5
            Dim formatter As New ObjectStateFormatter()
            Dim data As String = "data_" & counter.ToString()
            Dim result = formatter.Deserialize(data)
            counter += 1
        End While
    End Sub
    
    ' Violation: ObjectStateFormatter deserialize with binary data
    Public Sub DeserializeWithBinaryData()
        Dim formatter As New ObjectStateFormatter()
        Dim binaryData As Byte() = {1, 2, 3, 4, 5}
        Dim result = formatter.Deserialize(binaryData)
    End Sub

End Class
