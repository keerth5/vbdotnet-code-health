' Test file for cq-vbn-0184: Do not use insecure deserializer LosFormatter
' This rule detects usage of LosFormatter which is insecure for deserializing untrusted data

Imports System
Imports System.IO
Imports System.Web.UI

Public Class LosFormatterSecurityViolations
    
    ' Violation: Creating new LosFormatter instance
    Public Sub CreateLosFormatter()
        Dim formatter As New LosFormatter()
        ' Some code here
    End Sub
    
    ' Violation: Using LosFormatter.Deserialize
    Public Sub DeserializeWithLosFormatter()
        Dim formatter As New LosFormatter()
        Dim data As String = "serialized_data"
        Dim result = formatter.Deserialize(data)
    End Sub
    
    ' Violation: LosFormatter creation and deserialization in same method
    Public Sub CreateAndDeserialize()
        Dim lf As New LosFormatter()
        Dim serializedData As String = "some_data"
        Dim obj = lf.Deserialize(serializedData)
    End Sub
    
    ' Violation: Multiple LosFormatter instances
    Public Sub MultipleLosFormatters()
        Dim formatter1 As New LosFormatter()
        Dim formatter2 As New LosFormatter()
        Dim data1 As String = "data1"
        Dim data2 As String = "data2"
        Dim result1 = formatter1.Deserialize(data1)
        Dim result2 = formatter2.Deserialize(data2)
    End Sub
    
    ' Violation: LosFormatter in different contexts
    Public Function DeserializeData(data As String) As Object
        Dim formatter As New LosFormatter()
        Return formatter.Deserialize(data)
    End Function
    
    ' Violation: LosFormatter with field
    Private losFormatter As New LosFormatter()
    
    Public Sub UseFieldLosFormatter()
        Dim data As String = "serialized_data"
        Dim result = losFormatter.Deserialize(data)
    End Sub
    
    ' Violation: LosFormatter in Try-Catch
    Public Sub DeserializeWithErrorHandling()
        Try
            Dim formatter As New LosFormatter()
            Dim data As String = "serialized_data"
            Dim result = formatter.Deserialize(data)
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub
    
    ' Violation: LosFormatter with TextWriter parameter
    Public Sub CreateLosFormatterWithTextWriter()
        Dim writer As New StringWriter()
        Dim formatter As New LosFormatter(False, writer.Encoding.WebName)
        Dim data As String = "serialized_data"
        Dim result = formatter.Deserialize(data)
    End Sub
    
    ' Violation: LosFormatter deserialize with stream
    Public Sub DeserializeWithStream()
        Dim formatter As New LosFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream)
    End Sub
    
    ' Non-violation: Safe serialization (should not be detected)
    Public Sub SafeSerialization()
        Dim formatter As New LosFormatter()
        Dim writer As New StringWriter()
        formatter.Serialize(writer, "safe data")
    End Sub
    
    ' Non-violation: Other formatter types (should not be detected)
    Public Sub OtherFormatters()
        ' These should not trigger the rule
        Dim xmlFormatter As New System.Xml.Serialization.XmlSerializer(GetType(String))
        Dim jsonFormatter As String = "JsonSerializer"
    End Sub
    
    ' Non-violation: Comments and strings mentioning LosFormatter
    Public Sub CommentsAndStrings()
        ' This is a comment about LosFormatter
        Dim message As String = "Do not use LosFormatter for untrusted data"
        Console.WriteLine("LosFormatter is insecure")
    End Sub
    
    ' Violation: LosFormatter in conditional
    Public Sub ConditionalLosFormatter(useFormatter As Boolean)
        If useFormatter Then
            Dim formatter As New LosFormatter()
            Dim data As String = "serialized_data"
            Dim result = formatter.Deserialize(data)
        End If
    End Sub
    
    ' Violation: LosFormatter in loop
    Public Sub LosFormatterInLoop()
        For i As Integer = 0 To 10
            Dim formatter As New LosFormatter()
            Dim data As String = "serialized_data_" & i.ToString()
            Dim result = formatter.Deserialize(data)
        Next
    End Sub
    
    ' Violation: LosFormatter with variable assignment
    Public Sub AssignLosFormatter()
        Dim formatter As LosFormatter
        formatter = New LosFormatter()
        Dim data As String = "serialized_data"
        Dim result = formatter.Deserialize(data)
    End Sub
    
    ' Violation: LosFormatter deserialize in Select Case
    Public Sub LosFormatterInSelectCase(option As Integer)
        Select Case option
            Case 1
                Dim formatter As New LosFormatter()
                Dim data As String = "option1_data"
                Dim result = formatter.Deserialize(data)
            Case 2
                Dim formatter2 As New LosFormatter()
                Dim data2 As String = "option2_data"
                Dim result2 = formatter2.Deserialize(data2)
        End Select
    End Sub
    
    ' Violation: LosFormatter with Using statement
    Public Sub LosFormatterWithUsing()
        Using writer As New StringWriter()
            Dim formatter As New LosFormatter()
            Dim data As String = "serialized_data"
            Dim result = formatter.Deserialize(data)
        End Using
    End Sub

End Class
