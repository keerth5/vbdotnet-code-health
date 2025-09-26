' Test file for cq-vbn-0195: Ensure that JsonSerializer has a secure configuration when deserializing
' This rule detects JsonSerializer.Create calls with settings followed by Deserialize calls

Imports System
Imports System.IO
Imports Newtonsoft.Json

Public Class JsonSerializerSecureConfigViolations
    
    ' Violation: JsonSerializer.Create with settings and Deserialize
    Public Sub CreateWithSettingsAndDeserialize()
        Dim settings As New JsonSerializerSettings()
        Dim serializer = JsonSerializer.Create(settings)
        Using reader As New StringReader("{""key"":""value""}")
            Using jsonReader As New JsonTextReader(reader)
                Dim result = serializer.Deserialize(jsonReader) ' Violation
            End Using
        End Using
    End Sub
    
    ' Violation: JsonSerializer.Create with settings and generic Deserialize
    Public Sub CreateWithSettingsAndDeserializeGeneric()
        Dim settings As New JsonSerializerSettings()
        Dim serializer = JsonSerializer.Create(settings)
        Using reader As New StringReader("{""key"":""value""}")
            Using jsonReader As New JsonTextReader(reader)
                Dim result = serializer.Deserialize(Of Object)(jsonReader) ' Violation
            End Using
        End Using
    End Sub
    
    ' Violation: JsonSerializer.Create with settings and Deserialize with type
    Public Sub CreateWithSettingsAndDeserializeWithType()
        Dim settings As New JsonSerializerSettings()
        Dim serializer = JsonSerializer.Create(settings)
        Using reader As New StringReader("{""key"":""value""}")
            Using jsonReader As New JsonTextReader(reader)
                Dim result = serializer.Deserialize(jsonReader, GetType(Object)) ' Violation
            End Using
        End Using
    End Sub
    
    ' Violation: Multiple operations between Create and Deserialize
    Public Sub CreateWithMultipleOperationsAndDeserialize()
        Dim settings As New JsonSerializerSettings()
        Dim serializer = JsonSerializer.Create(settings)
        
        ' Some other operations
        serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat
        serializer.NullValueHandling = NullValueHandling.Ignore
        
        Using reader As New StringReader("{""key"":""value""}")
            Using jsonReader As New JsonTextReader(reader)
                Dim result = serializer.Deserialize(jsonReader) ' Violation
            End Using
        End Using
    End Sub
    
    ' Violation: Create in loop and deserialize
    Public Sub CreateInLoopAndDeserialize()
        For i As Integer = 0 To 5
            Dim settings As New JsonSerializerSettings()
            Dim serializer = JsonSerializer.Create(settings)
            Using reader As New StringReader("{""index"":" & i.ToString() & "}")
                Using jsonReader As New JsonTextReader(reader)
                    Dim result = serializer.Deserialize(jsonReader) ' Violation
                End Using
            End Using
        Next
    End Sub
    
    ' Violation: Create conditionally and deserialize
    Public Sub CreateConditionallyAndDeserialize(condition As Boolean)
        If condition Then
            Dim settings As New JsonSerializerSettings()
            Dim serializer = JsonSerializer.Create(settings)
            Using reader As New StringReader("{""conditional"":true}")
                Using jsonReader As New JsonTextReader(reader)
                    Dim result = serializer.Deserialize(jsonReader) ' Violation
                End Using
            End Using
        End If
    End Sub
    
    ' Violation: Try-catch with create and deserialize
    Public Sub TryCatchCreateAndDeserialize()
        Try
            Dim settings As New JsonSerializerSettings()
            Dim serializer = JsonSerializer.Create(settings)
            Using reader As New StringReader("{""key"":""value""}")
                Using jsonReader As New JsonTextReader(reader)
                    Dim result = serializer.Deserialize(jsonReader) ' Violation
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Method with return and deserialize
    Public Function CreateAndDeserializeData(jsonData As String) As Object
        Dim settings As New JsonSerializerSettings()
        Dim serializer = JsonSerializer.Create(settings)
        Using reader As New StringReader(jsonData)
            Using jsonReader As New JsonTextReader(reader)
                Return serializer.Deserialize(jsonReader) ' Violation
            End Using
        End Using
    End Function
    
    ' Violation: Create with field settings and deserialize
    Private fieldSettings As New JsonSerializerSettings()
    
    Public Sub CreateWithFieldSettingsAndDeserialize()
        Dim serializer = JsonSerializer.Create(fieldSettings)
        Using reader As New StringReader("{""key"":""value""}")
            Using jsonReader As New JsonTextReader(reader)
                Dim result = serializer.Deserialize(jsonReader) ' Violation
            End Using
        End Using
    End Sub
    
    ' Violation: Create with parameter settings and deserialize
    Public Sub CreateWithParameterSettingsAndDeserialize(settings As JsonSerializerSettings)
        Dim serializer = JsonSerializer.Create(settings)
        Using reader As New StringReader("{""key"":""value""}")
            Using jsonReader As New JsonTextReader(reader)
                Dim result = serializer.Deserialize(jsonReader) ' Violation
            End Using
        End Using
    End Sub
    
    ' Non-violation: JsonSerializer.Create with settings and only Serialize
    Public Sub CreateWithSettingsAndSerializeOnly()
        Dim settings As New JsonSerializerSettings()
        Dim serializer = JsonSerializer.Create(settings)
        Using writer As New StringWriter()
            Using jsonWriter As New JsonTextWriter(writer)
                serializer.Serialize(jsonWriter, New Object()) ' No violation - only serializing
            End Using
        End Using
    End Sub
    
    ' Non-violation: JsonSerializer.Create without settings
    Public Sub CreateWithoutSettings()
        Dim serializer = JsonSerializer.Create()
        Using reader As New StringReader("{""key"":""value""}")
            Using jsonReader As New JsonTextReader(reader)
                Dim result = serializer.Deserialize(jsonReader) ' No violation - no settings
            End Using
        End Using
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This creates JsonSerializer.Create(settings) and calls Deserialize
        Dim message As String = "JsonSerializer.Create(settings) followed by Deserialize can be insecure"
        Console.WriteLine("Always use secure JsonSerializer configuration")
    End Sub
    
    ' Violation: Multiple serializers with settings and deserialize
    Public Sub MultipleSerializersWithSettingsAndDeserialize()
        Dim settings1 As New JsonSerializerSettings()
        Dim serializer1 = JsonSerializer.Create(settings1)
        
        Dim settings2 As New JsonSerializerSettings()
        Dim serializer2 = JsonSerializer.Create(settings2)
        
        Using reader1 As New StringReader("{""data1"":""value1""}")
            Using jsonReader1 As New JsonTextReader(reader1)
                Dim result1 = serializer1.Deserialize(jsonReader1) ' Violation
            End Using
        End Using
        
        Using reader2 As New StringReader("{""data2"":""value2""}")
            Using jsonReader2 As New JsonTextReader(reader2)
                Dim result2 = serializer2.Deserialize(jsonReader2) ' Violation
            End Using
        End Using
    End Sub
    
    ' Violation: Select Case with creation and deserialization
    Public Sub SelectCaseCreateAndDeserialize(option As Integer)
        Select Case option
            Case 1
                Dim settings1 As New JsonSerializerSettings()
                Dim serializer1 = JsonSerializer.Create(settings1)
                Using reader As New StringReader("{""option"":1}")
                    Using jsonReader As New JsonTextReader(reader)
                        Dim result = serializer1.Deserialize(jsonReader) ' Violation
                    End Using
                End Using
            Case 2
                Dim settings2 As New JsonSerializerSettings()
                Dim serializer2 = JsonSerializer.Create(settings2)
                Using reader As New StringReader("{""option"":2}")
                    Using jsonReader As New JsonTextReader(reader)
                        Dim result = serializer2.Deserialize(jsonReader) ' Violation
                    End Using
                End Using
        End Select
    End Sub
    
    ' Violation: While loop with creation and deserialization
    Public Sub WhileLoopCreateAndDeserialize()
        Dim counter As Integer = 0
        While counter < 3
            Dim settings As New JsonSerializerSettings()
            Dim serializer = JsonSerializer.Create(settings)
            Using reader As New StringReader("{""counter"":" & counter.ToString() & "}")
                Using jsonReader As New JsonTextReader(reader)
                    Dim result = serializer.Deserialize(jsonReader) ' Violation
                End Using
            End Using
            counter += 1
        End While
    End Sub

End Class
