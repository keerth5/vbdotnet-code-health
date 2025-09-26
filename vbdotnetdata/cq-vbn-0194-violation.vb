' Test file for cq-vbn-0194: Do not deserialize with JsonSerializer using an insecure configuration
' This rule detects JsonConvert.DeserializeObject and PopulateObject calls with settings parameters

Imports System
Imports Newtonsoft.Json

Public Class JsonSerializerInsecureConfigViolations
    
    ' Violation: JsonConvert.DeserializeObject with settings parameter
    Public Sub DeserializeObjectWithSettings()
        Dim settings As New JsonSerializerSettings()
        Dim json As String = "{""key"":""value""}"
        Dim result = JsonConvert.DeserializeObject(json, settings) ' Violation
    End Sub
    
    ' Violation: JsonConvert.DeserializeObject with generic type and settings
    Public Sub DeserializeObjectGenericWithSettings()
        Dim settings As New JsonSerializerSettings()
        Dim json As String = "{""key"":""value""}"
        Dim result = JsonConvert.DeserializeObject(Of Object)(json, settings) ' Violation
    End Sub
    
    ' Violation: JsonConvert.DeserializeObject with type and settings
    Public Sub DeserializeObjectWithTypeAndSettings()
        Dim settings As New JsonSerializerSettings()
        Dim json As String = "{""key"":""value""}"
        Dim result = JsonConvert.DeserializeObject(json, GetType(Object), settings) ' Violation
    End Sub
    
    ' Violation: JsonConvert.PopulateObject with settings parameter
    Public Sub PopulateObjectWithSettings()
        Dim settings As New JsonSerializerSettings()
        Dim json As String = "{""key"":""value""}"
        Dim target As New Object()
        JsonConvert.PopulateObject(json, target, settings) ' Violation
    End Sub
    
    ' Violation: Multiple DeserializeObject calls with settings
    Public Sub MultipleDeserializeObjectWithSettings()
        Dim settings As New JsonSerializerSettings()
        Dim json1 As String = "{""key1"":""value1""}"
        Dim json2 As String = "{""key2"":""value2""}"
        
        Dim result1 = JsonConvert.DeserializeObject(json1, settings) ' Violation
        Dim result2 = JsonConvert.DeserializeObject(json2, settings) ' Violation
    End Sub
    
    ' Violation: DeserializeObject in loop with settings
    Public Sub DeserializeObjectInLoopWithSettings()
        Dim settings As New JsonSerializerSettings()
        For i As Integer = 0 To 10
            Dim json As String = "{""index"":" & i.ToString() & "}"
            Dim result = JsonConvert.DeserializeObject(json, settings) ' Violation
        Next
    End Sub
    
    ' Violation: DeserializeObject in conditional with settings
    Public Sub ConditionalDeserializeObjectWithSettings(condition As Boolean)
        Dim settings As New JsonSerializerSettings()
        If condition Then
            Dim json As String = "{""conditional"":true}"
            Dim result = JsonConvert.DeserializeObject(json, settings) ' Violation
        End If
    End Sub
    
    ' Violation: PopulateObject in Try-Catch with settings
    Public Sub PopulateObjectInTryCatchWithSettings()
        Try
            Dim settings As New JsonSerializerSettings()
            Dim json As String = "{""key"":""value""}"
            Dim target As New Object()
            JsonConvert.PopulateObject(json, target, settings) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: DeserializeObject with field settings
    Private sharedSettings As New JsonSerializerSettings()
    
    Public Sub DeserializeObjectWithFieldSettings()
        Dim json As String = "{""key"":""value""}"
        Dim result = JsonConvert.DeserializeObject(json, sharedSettings) ' Violation
    End Sub
    
    ' Violation: DeserializeObject with method parameter settings
    Public Sub DeserializeObjectWithParameterSettings(settings As JsonSerializerSettings)
        Dim json As String = "{""key"":""value""}"
        Dim result = JsonConvert.DeserializeObject(json, settings) ' Violation
    End Sub
    
    ' Non-violation: DeserializeObject without settings (should not be detected)
    Public Sub SafeDeserializeObject()
        Dim json As String = "{""key"":""value""}"
        Dim result = JsonConvert.DeserializeObject(json) ' No violation
    End Sub
    
    ' Non-violation: PopulateObject without settings (should not be detected)
    Public Sub SafePopulateObject()
        Dim json As String = "{""key"":""value""}"
        Dim target As New Object()
        JsonConvert.PopulateObject(json, target) ' No violation
    End Sub
    
    ' Non-violation: SerializeObject with settings (should not be detected)
    Public Sub SerializeObjectWithSettings()
        Dim settings As New JsonSerializerSettings()
        Dim obj As New Object()
        Dim json = JsonConvert.SerializeObject(obj, settings) ' No violation - serialize doesn't need detection
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about JsonConvert.DeserializeObject with settings
        Dim message As String = "JsonConvert.DeserializeObject(json, settings) is potentially insecure"
        Console.WriteLine("Use JsonConvert.PopulateObject carefully with settings")
    End Sub
    
    ' Violation: DeserializeObject in Select Case with settings
    Public Sub DeserializeObjectInSelectCaseWithSettings(option As Integer)
        Dim settings As New JsonSerializerSettings()
        Select Case option
            Case 1
                Dim json1 As String = "{""option"":1}"
                Dim result1 = JsonConvert.DeserializeObject(json1, settings) ' Violation
            Case 2
                Dim json2 As String = "{""option"":2}"
                Dim result2 = JsonConvert.DeserializeObject(json2, settings) ' Violation
            Case Else
                Dim json3 As String = "{""option"":""other""}"
                Dim target As New Object()
                JsonConvert.PopulateObject(json3, target, settings) ' Violation
        End Select
    End Sub
    
    ' Violation: DeserializeObject with Using statement and settings
    Public Sub DeserializeObjectWithUsingAndSettings()
        Using reader As New System.IO.StringReader("{""key"":""value""}")
            Dim settings As New JsonSerializerSettings()
            Dim json As String = reader.ReadToEnd()
            Dim result = JsonConvert.DeserializeObject(json, settings) ' Violation
        End Using
    End Sub
    
    ' Violation: PopulateObject with While loop and settings
    Public Sub PopulateObjectInWhileLoopWithSettings()
        Dim settings As New JsonSerializerSettings()
        Dim counter As Integer = 0
        While counter < 5
            Dim json As String = "{""counter"":" & counter.ToString() & "}"
            Dim target As New Object()
            JsonConvert.PopulateObject(json, target, settings) ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: DeserializeObject with anonymous settings
    Public Sub DeserializeObjectWithAnonymousSettings()
        Dim json As String = "{""key"":""value""}"
        Dim result = JsonConvert.DeserializeObject(json, New JsonSerializerSettings()) ' Violation
    End Sub

End Class
