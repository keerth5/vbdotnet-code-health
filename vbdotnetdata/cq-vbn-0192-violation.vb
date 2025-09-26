' Test file for cq-vbn-0192: Do not use insecure JsonSerializerSettings
' This rule detects JsonSerializerSettings created with insecure TypeNameHandling values

Imports System
Imports Newtonsoft.Json

Public Class JsonSerializerSettingsViolations
    
    ' Violation: JsonSerializerSettings with TypeNameHandling.Objects
    Public Sub CreateSettingsWithObjects()
        Dim settings As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.Objects ' Violation
        }
    End Sub
    
    ' Violation: JsonSerializerSettings with TypeNameHandling.Arrays
    Public Sub CreateSettingsWithArrays()
        Dim settings As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.Arrays ' Violation
        }
    End Sub
    
    ' Violation: JsonSerializerSettings with TypeNameHandling.All
    Public Sub CreateSettingsWithAll()
        Dim settings As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.All ' Violation
        }
    End Sub
    
    ' Violation: JsonSerializerSettings with TypeNameHandling.Auto
    Public Sub CreateSettingsWithAuto()
        Dim settings As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.Auto ' Violation
        }
    End Sub
    
    ' Violation: JsonSerializerSettings with multiple properties including insecure TypeNameHandling
    Public Sub CreateSettingsWithMultipleProperties()
        Dim settings As New JsonSerializerSettings() With {
            .DateFormatHandling = DateFormatHandling.IsoDateFormat,
            .TypeNameHandling = TypeNameHandling.Objects, ' Violation
            .NullValueHandling = NullValueHandling.Ignore
        }
    End Sub
    
    ' Violation: JsonSerializerSettings in conditional
    Public Sub ConditionalSettingsCreation(useObjects As Boolean)
        If useObjects Then
            Dim settings As New JsonSerializerSettings() With {
                .TypeNameHandling = TypeNameHandling.Objects ' Violation
            }
        Else
            Dim settings As New JsonSerializerSettings() With {
                .TypeNameHandling = TypeNameHandling.Arrays ' Violation
            }
        End If
    End Sub
    
    ' Violation: JsonSerializerSettings in loop
    Public Sub SettingsCreationInLoop()
        For i As Integer = 0 To 3
            Select Case i
                Case 0
                    Dim settings0 As New JsonSerializerSettings() With {
                        .TypeNameHandling = TypeNameHandling.Objects ' Violation
                    }
                Case 1
                    Dim settings1 As New JsonSerializerSettings() With {
                        .TypeNameHandling = TypeNameHandling.Arrays ' Violation
                    }
                Case 2
                    Dim settings2 As New JsonSerializerSettings() With {
                        .TypeNameHandling = TypeNameHandling.All ' Violation
                    }
                Case 3
                    Dim settings3 As New JsonSerializerSettings() With {
                        .TypeNameHandling = TypeNameHandling.Auto ' Violation
                    }
            End Select
        Next
    End Sub
    
    ' Violation: JsonSerializerSettings in Try-Catch
    Public Sub SettingsInTryCatch()
        Try
            Dim settings As New JsonSerializerSettings() With {
                .TypeNameHandling = TypeNameHandling.All, ' Violation
                .DateFormatHandling = DateFormatHandling.IsoDateFormat
            }
            Dim json As String = JsonConvert.SerializeObject(New Object(), settings)
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: JsonSerializerSettings with constructor parameters
    Public Sub SettingsWithConstructorParams()
        Dim settings As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.Objects, ' Violation
            .Formatting = Formatting.Indented
        }
    End Sub
    
    ' Non-violation: JsonSerializerSettings with TypeNameHandling.None (should not be detected)
    Public Sub SafeSettingsCreation()
        Dim settings As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.None ' No violation
        }
    End Sub
    
    ' Non-violation: JsonSerializerSettings without TypeNameHandling
    Public Sub SettingsWithoutTypeNameHandling()
        Dim settings As New JsonSerializerSettings() With {
            .DateFormatHandling = DateFormatHandling.IsoDateFormat,
            .NullValueHandling = NullValueHandling.Ignore
        }
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This creates JsonSerializerSettings With TypeNameHandling.Objects
        Dim message As String = "New JsonSerializerSettings() With { .TypeNameHandling = TypeNameHandling.Objects }"
        Console.WriteLine("Avoid insecure JsonSerializerSettings")
    End Sub
    
    ' Violation: JsonSerializerSettings in method return
    Public Function CreateInsecureSettings() As JsonSerializerSettings
        Return New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.Objects ' Violation
        }
    End Function
    
    ' Violation: JsonSerializerSettings in Using statement
    Public Sub SettingsInUsing()
        Using writer As New System.IO.StringWriter()
            Dim settings As New JsonSerializerSettings() With {
                .TypeNameHandling = TypeNameHandling.Arrays ' Violation
            }
            Dim serializer As JsonSerializer = JsonSerializer.Create(settings)
        End Using
    End Sub
    
    ' Violation: Multiple JsonSerializerSettings with different insecure values
    Public Sub MultipleInsecureSettings()
        Dim settings1 As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.Objects ' Violation
        }
        
        Dim settings2 As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.Arrays ' Violation
        }
        
        Dim settings3 As New JsonSerializerSettings() With {
            .TypeNameHandling = TypeNameHandling.All ' Violation
        }
    End Sub

End Class
