' Test file for cq-vbn-0193: Ensure that JsonSerializerSettings are secure
' This rule detects when JsonSerializerSettings are created and then configured with insecure TypeNameHandling

Imports System
Imports Newtonsoft.Json

Public Class JsonSerializerSettingsSecurityViolations
    
    ' Violation: Create JsonSerializerSettings and set TypeNameHandling to Objects
    Public Sub CreateAndSetToObjects()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
    End Sub
    
    ' Violation: Create JsonSerializerSettings and set TypeNameHandling to Arrays
    Public Sub CreateAndSetToArrays()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Arrays ' Violation
    End Sub
    
    ' Violation: Create JsonSerializerSettings and set TypeNameHandling to All
    Public Sub CreateAndSetToAll()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.All ' Violation
    End Sub
    
    ' Violation: Create JsonSerializerSettings and set TypeNameHandling to Auto
    Public Sub CreateAndSetToAuto()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Auto ' Violation
    End Sub
    
    ' Violation: Multiple operations between creation and setting
    Public Sub CreateWithMultipleOperations()
        Dim settings As New JsonSerializerSettings()
        settings.DateFormatHandling = DateFormatHandling.IsoDateFormat
        settings.NullValueHandling = NullValueHandling.Ignore
        settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
        settings.Formatting = Formatting.Indented
    End Sub
    
    ' Violation: Create in loop and set TypeNameHandling
    Public Sub CreateInLoopAndSet()
        For i As Integer = 0 To 3
            Dim settings As New JsonSerializerSettings()
            Select Case i
                Case 0
                    settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
                Case 1
                    settings.TypeNameHandling = TypeNameHandling.Arrays ' Violation
                Case 2
                    settings.TypeNameHandling = TypeNameHandling.All ' Violation
                Case 3
                    settings.TypeNameHandling = TypeNameHandling.Auto ' Violation
            End Select
        Next
    End Sub
    
    ' Violation: Create conditionally and set TypeNameHandling
    Public Sub CreateConditionallyAndSet(condition As Boolean)
        If condition Then
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
        Else
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.Arrays ' Violation
        End If
    End Sub
    
    ' Violation: Try-catch with create and set TypeNameHandling
    Public Sub TryCatchCreateAndSet()
        Try
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.All ' Violation
            Dim json As String = JsonConvert.SerializeObject(New Object(), settings)
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Using statement with create and set TypeNameHandling
    Public Sub UsingStatementCreateAndSet()
        Using writer As New System.IO.StringWriter()
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
            Dim serializer As JsonSerializer = JsonSerializer.Create(settings)
        End Using
    End Sub
    
    ' Violation: Method with return and insecure TypeNameHandling
    Public Function CreateInsecureSettings() As JsonSerializerSettings
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Auto ' Violation
        Return settings
    End Function
    
    ' Violation: Create with variable assignment and set TypeNameHandling
    Public Sub CreateWithVariableAssignment()
        Dim settings As JsonSerializerSettings
        settings = New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
    End Sub
    
    ' Non-violation: Create JsonSerializerSettings and set TypeNameHandling to None
    Public Sub CreateAndSetToNone()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.None ' No violation
    End Sub
    
    ' Non-violation: Create JsonSerializerSettings without setting TypeNameHandling
    Public Sub CreateWithoutSettingTypeNameHandling()
        Dim settings As New JsonSerializerSettings()
        settings.DateFormatHandling = DateFormatHandling.IsoDateFormat
        settings.NullValueHandling = NullValueHandling.Ignore
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This creates a New JsonSerializerSettings() and sets TypeNameHandling
        Dim message As String = "Dim settings As New JsonSerializerSettings() then settings.TypeNameHandling = TypeNameHandling.Objects"
        Console.WriteLine("Always use secure JsonSerializerSettings")
    End Sub
    
    ' Violation: Multiple settings with different insecure values
    Public Sub MultipleSettingsWithInsecureValues()
        Dim settings1 As New JsonSerializerSettings()
        settings1.TypeNameHandling = TypeNameHandling.Objects ' Violation
        
        Dim settings2 As New JsonSerializerSettings()
        settings2.TypeNameHandling = TypeNameHandling.Arrays ' Violation
        
        Dim settings3 As New JsonSerializerSettings()
        settings3.TypeNameHandling = TypeNameHandling.All ' Violation
    End Sub
    
    ' Violation: Select Case with creation and setting
    Public Sub SelectCaseCreateAndSet(option As Integer)
        Select Case option
            Case 1
                Dim settings1 As New JsonSerializerSettings()
                settings1.TypeNameHandling = TypeNameHandling.Objects ' Violation
            Case 2
                Dim settings2 As New JsonSerializerSettings()
                settings2.TypeNameHandling = TypeNameHandling.Arrays ' Violation
            Case Else
                Dim settings3 As New JsonSerializerSettings()
                settings3.TypeNameHandling = TypeNameHandling.All ' Violation
        End Select
    End Sub
    
    ' Violation: While loop with creation and setting
    Public Sub WhileLoopCreateAndSet()
        Dim counter As Integer = 0
        While counter < 3
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.Auto ' Violation
            counter += 1
        End While
    End Sub

End Class
