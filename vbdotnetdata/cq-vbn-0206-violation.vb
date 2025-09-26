' Test file for cq-vbn-0206: Insecure DTD Processing
' This rule detects insecure DTD processing configurations

Imports System
Imports System.Xml

Public Class InsecureDtdProcessingViolations
    
    ' Violation: DtdProcessing set to Parse
    Public Sub SetDtdProcessingToParse()
        Dim settings As New XmlReaderSettings()
        settings.DtdProcessing = DtdProcessing.Parse ' Violation
    End Sub
    
    ' Violation: XmlReaderSettings with DtdProcessing.Parse in object initializer
    Public Sub XmlReaderSettingsWithDtdProcessingParse()
        Dim settings As New XmlReaderSettings() With {
            .DtdProcessing = DtdProcessing.Parse ' Violation
        }
    End Sub
    
    ' Violation: XmlReaderSettings with DtdProcessing.Parse and other properties
    Public Sub XmlReaderSettingsWithMultipleProperties()
        Dim settings As New XmlReaderSettings() With {
            .IgnoreWhitespace = True,
            .DtdProcessing = DtdProcessing.Parse, ' Violation
            .ValidationType = ValidationType.DTD
        }
    End Sub
    
    ' Violation: Multiple DtdProcessing assignments to Parse
    Public Sub MultipleDtdProcessingAssignments()
        Dim settings1 As New XmlReaderSettings()
        Dim settings2 As New XmlReaderSettings()
        
        settings1.DtdProcessing = DtdProcessing.Parse ' Violation
        settings2.DtdProcessing = DtdProcessing.Parse ' Violation
    End Sub
    
    ' Violation: DtdProcessing.Parse in loop
    Public Sub DtdProcessingParseInLoop()
        For i As Integer = 0 To 5
            Dim settings As New XmlReaderSettings()
            settings.DtdProcessing = DtdProcessing.Parse ' Violation
        Next
    End Sub
    
    ' Violation: DtdProcessing.Parse in conditional
    Public Sub ConditionalDtdProcessingParse(enableDtd As Boolean)
        Dim settings As New XmlReaderSettings()
        If enableDtd Then
            settings.DtdProcessing = DtdProcessing.Parse ' Violation
        End If
    End Sub
    
    ' Violation: DtdProcessing.Parse in Try-Catch
    Public Sub DtdProcessingParseInTryCatch()
        Try
            Dim settings As New XmlReaderSettings()
            settings.DtdProcessing = DtdProcessing.Parse ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: DtdProcessing.Parse with field settings
    Private xmlSettings As New XmlReaderSettings()
    
    Public Sub FieldDtdProcessingParse()
        xmlSettings.DtdProcessing = DtdProcessing.Parse ' Violation
    End Sub
    
    ' Violation: DtdProcessing.Parse with parameter settings
    Public Sub ParameterDtdProcessingParse(settings As XmlReaderSettings)
        settings.DtdProcessing = DtdProcessing.Parse ' Violation
    End Sub
    
    ' Violation: DtdProcessing.Parse in method return context
    Public Function CreateInsecureXmlReaderSettings() As XmlReaderSettings
        Dim settings As New XmlReaderSettings()
        settings.DtdProcessing = DtdProcessing.Parse ' Violation
        Return settings
    End Function
    
    ' Non-violation: DtdProcessing set to Prohibit (should not be detected)
    Public Sub SafeDtdProcessingProhibit()
        Dim settings As New XmlReaderSettings()
        settings.DtdProcessing = DtdProcessing.Prohibit ' No violation - secure setting
    End Sub
    
    ' Non-violation: DtdProcessing set to Ignore (should not be detected)
    Public Sub SafeDtdProcessingIgnore()
        Dim settings As New XmlReaderSettings()
        settings.DtdProcessing = DtdProcessing.Ignore ' No violation - secure setting
    End Sub
    
    ' Non-violation: XmlReaderSettings without DtdProcessing (should not be detected)
    Public Sub SafeXmlReaderSettingsWithoutDtdProcessing()
        Dim settings As New XmlReaderSettings() With {
            .IgnoreWhitespace = True,
            .ValidationType = ValidationType.Schema
        }
    End Sub
    
    ' Non-violation: Comments and strings mentioning DtdProcessing.Parse
    Public Sub CommentsAndStrings()
        ' This is about settings.DtdProcessing = DtdProcessing.Parse
        Dim message As String = "DtdProcessing.Parse can be dangerous"
        Console.WriteLine("Avoid DtdProcessing.Parse for security")
    End Sub
    
    ' Violation: DtdProcessing.Parse in Select Case
    Public Sub DtdProcessingParseInSelectCase(option As Integer)
        Dim settings As New XmlReaderSettings()
        Select Case option
            Case 1
                settings.DtdProcessing = DtdProcessing.Parse ' Violation
            Case 2
                settings.DtdProcessing = DtdProcessing.Parse ' Violation
            Case Else
                settings.DtdProcessing = DtdProcessing.Parse ' Violation
        End Select
    End Sub
    
    ' Violation: DtdProcessing.Parse in Using statement
    Public Sub DtdProcessingParseInUsing()
        Using reader As XmlReader = XmlReader.Create("data.xml")
            Dim settings As New XmlReaderSettings()
            settings.DtdProcessing = DtdProcessing.Parse ' Violation
        End Using
    End Sub
    
    ' Violation: DtdProcessing.Parse in While loop
    Public Sub DtdProcessingParseInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim settings As New XmlReaderSettings()
            settings.DtdProcessing = DtdProcessing.Parse ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: XmlReaderSettings with DtdProcessing.Parse in nested object initializer
    Public Sub NestedObjectInitializerWithDtdProcessingParse()
        Dim config As New With {
            .Settings = New XmlReaderSettings() With {
                .DtdProcessing = DtdProcessing.Parse ' Violation
            }
        }
    End Sub
    
    ' Violation: DtdProcessing.Parse with chained assignment
    Public Sub ChainedDtdProcessingParseAssignment()
        Dim settings1, settings2 As New XmlReaderSettings()
        settings1.DtdProcessing = settings2.DtdProcessing = DtdProcessing.Parse ' Violation
    End Sub
    
    ' Violation: DtdProcessing.Parse with conditional assignment
    Public Sub ConditionalDtdProcessingParseAssignment(useParse As Boolean)
        Dim settings As New XmlReaderSettings()
        settings.DtdProcessing = If(useParse, DtdProcessing.Parse, DtdProcessing.Prohibit) ' Violation
    End Sub
    
    ' Violation: DtdProcessing.Parse in lambda expression
    Public Sub DtdProcessingParseInLambda()
        Dim settingsList As New List(Of XmlReaderSettings)()
        settingsList.ForEach(Sub(s) s.DtdProcessing = DtdProcessing.Parse) ' Violation
    End Sub
    
    ' Violation: XmlReaderSettings with DtdProcessing.Parse in array initialization
    Public Sub ArrayInitializationWithDtdProcessingParse()
        Dim settingsArray() As XmlReaderSettings = {
            New XmlReaderSettings() With {.DtdProcessing = DtdProcessing.Parse}, ' Violation
            New XmlReaderSettings() With {.DtdProcessing = DtdProcessing.Parse} ' Violation
        }
    End Sub

End Class
