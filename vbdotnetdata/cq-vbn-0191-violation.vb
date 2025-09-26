' Test file for cq-vbn-0191: Do not use TypeNameHandling values other than None
' This rule detects usage of insecure TypeNameHandling values in JSON serialization

Imports System
Imports Newtonsoft.Json

Public Class TypeNameHandlingViolations
    
    ' Violation: Using TypeNameHandling.Objects
    Public Sub UseTypeNameHandlingObjects()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
    End Sub
    
    ' Violation: Using TypeNameHandling.Arrays
    Public Sub UseTypeNameHandlingArrays()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Arrays ' Violation
    End Sub
    
    ' Violation: Using TypeNameHandling.All
    Public Sub UseTypeNameHandlingAll()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.All ' Violation
    End Sub
    
    ' Violation: Using TypeNameHandling.Auto
    Public Sub UseTypeNameHandlingAuto()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.Auto ' Violation
    End Sub
    
    ' Violation: Direct usage of TypeNameHandling.Objects
    Public Sub DirectTypeNameHandlingObjects()
        Dim json As String = JsonConvert.SerializeObject(New Object(), TypeNameHandling.Objects) ' Violation
    End Sub
    
    ' Violation: Direct usage of TypeNameHandling.Arrays
    Public Sub DirectTypeNameHandlingArrays()
        Dim json As String = JsonConvert.SerializeObject(New Object(), TypeNameHandling.Arrays) ' Violation
    End Sub
    
    ' Violation: Direct usage of TypeNameHandling.All
    Public Sub DirectTypeNameHandlingAll()
        Dim json As String = JsonConvert.SerializeObject(New Object(), TypeNameHandling.All) ' Violation
    End Sub
    
    ' Violation: Direct usage of TypeNameHandling.Auto
    Public Sub DirectTypeNameHandlingAuto()
        Dim json As String = JsonConvert.SerializeObject(New Object(), TypeNameHandling.Auto) ' Violation
    End Sub
    
    ' Violation: TypeNameHandling in method parameter
    Public Sub SerializeWithTypeNameHandling(handling As TypeNameHandling)
        If handling = TypeNameHandling.Objects Then ' Violation
            Console.WriteLine("Using Objects")
        ElseIf handling = TypeNameHandling.Arrays Then ' Violation
            Console.WriteLine("Using Arrays")
        ElseIf handling = TypeNameHandling.All Then ' Violation
            Console.WriteLine("Using All")
        ElseIf handling = TypeNameHandling.Auto Then ' Violation
            Console.WriteLine("Using Auto")
        End If
    End Sub
    
    ' Violation: TypeNameHandling in conditional
    Public Sub ConditionalTypeNameHandling(useObjects As Boolean)
        If useObjects Then
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.Objects ' Violation
        Else
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.Arrays ' Violation
        End If
    End Sub
    
    ' Violation: TypeNameHandling in loop
    Public Sub TypeNameHandlingInLoop()
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
    
    ' Violation: TypeNameHandling in Try-Catch
    Public Sub TypeNameHandlingInTryCatch()
        Try
            Dim settings As New JsonSerializerSettings()
            settings.TypeNameHandling = TypeNameHandling.All ' Violation
            Dim json As String = JsonConvert.SerializeObject(New Object(), settings)
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Non-violation: Using TypeNameHandling.None (should not be detected)
    Public Sub SafeTypeNameHandling()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = TypeNameHandling.None ' No violation
    End Sub
    
    ' Non-violation: Not using TypeNameHandling at all
    Public Sub NoTypeNameHandling()
        Dim settings As New JsonSerializerSettings()
        settings.DateFormatHandling = DateFormatHandling.IsoDateFormat
    End Sub
    
    ' Non-violation: Comments and strings mentioning TypeNameHandling
    Public Sub CommentsAndStrings()
        ' This is a comment about TypeNameHandling.Objects
        Dim message As String = "Do not use TypeNameHandling.Objects for security"
        Console.WriteLine("TypeNameHandling.Arrays is insecure")
    End Sub
    
    ' Violation: TypeNameHandling in field assignment
    Private Shared ReadOnly UnsafeHandling As TypeNameHandling = TypeNameHandling.Objects ' Violation
    
    Public Sub UseFieldTypeNameHandling()
        Dim settings As New JsonSerializerSettings()
        settings.TypeNameHandling = UnsafeHandling
    End Sub
    
    ' Violation: TypeNameHandling in Select Case
    Public Sub TypeNameHandlingInSelectCase(option As Integer)
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

End Class
