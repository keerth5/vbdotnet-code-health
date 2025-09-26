' Test file for cq-vbn-0207: Insecure XSLT Script Execution
' This rule detects insecure XSLT script execution configurations

Imports System
Imports System.Xml
Imports System.Xml.Xsl

Public Class InsecureXsltScriptExecutionViolations
    
    ' Violation: XslCompiledTransform with TrustedXslt
    Public Sub XslCompiledTransformWithTrustedXslt()
        Dim transform As New XslCompiledTransform()
        transform.Load("stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
    End Sub
    
    ' Violation: XslCompiledTransform Load with TrustedXslt and resolver
    Public Sub LoadWithTrustedXsltAndResolver()
        Dim transform As New XslCompiledTransform()
        Dim resolver As New XmlUrlResolver()
        transform.Load("stylesheet.xsl", XsltSettings.TrustedXslt, resolver) ' Violation
    End Sub
    
    ' Violation: EnableScript set to True
    Public Sub EnableScriptSetToTrue()
        Dim settings As New XsltSettings()
        settings.EnableScript = True ' Violation
    End Sub
    
    ' Violation: XsltSettings with EnableScript = True
    Public Sub XsltSettingsWithEnableScriptTrue()
        Dim settings As New XsltSettings() With {
            .EnableScript = True ' Violation
        }
    End Sub
    
    ' Violation: Multiple XslCompiledTransform with TrustedXslt
    Public Sub MultipleXslCompiledTransformWithTrustedXslt()
        Dim transform1 As New XslCompiledTransform()
        Dim transform2 As New XslCompiledTransform()
        
        transform1.Load("stylesheet1.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
        transform2.Load("stylesheet2.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt in loop
    Public Sub XslCompiledTransformWithTrustedXsltInLoop()
        For i As Integer = 0 To 5
            Dim transform As New XslCompiledTransform()
            transform.Load($"stylesheet{i}.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
        Next
    End Sub
    
    ' Violation: EnableScript = True in loop
    Public Sub EnableScriptTrueInLoop()
        For i As Integer = 0 To 3
            Dim settings As New XsltSettings()
            settings.EnableScript = True ' Violation
        Next
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt in conditional
    Public Sub ConditionalXslCompiledTransformWithTrustedXslt(useTrusted As Boolean)
        Dim transform As New XslCompiledTransform()
        If useTrusted Then
            transform.Load("trusted-stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
        End If
    End Sub
    
    ' Violation: EnableScript = True in conditional
    Public Sub ConditionalEnableScriptTrue(enableScript As Boolean)
        Dim settings As New XsltSettings()
        If enableScript Then
            settings.EnableScript = True ' Violation
        End If
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt in Try-Catch
    Public Sub XslCompiledTransformWithTrustedXsltInTryCatch()
        Try
            Dim transform As New XslCompiledTransform()
            transform.Load("stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: EnableScript = True in Try-Catch
    Public Sub EnableScriptTrueInTryCatch()
        Try
            Dim settings As New XsltSettings()
            settings.EnableScript = True ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: XslCompiledTransform field with TrustedXslt
    Private transform As New XslCompiledTransform()
    
    Public Sub FieldXslCompiledTransformWithTrustedXslt()
        transform.Load("field-stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
    End Sub
    
    ' Violation: XsltSettings field with EnableScript = True
    Private xsltSettings As New XsltSettings()
    
    Public Sub FieldEnableScriptTrue()
        xsltSettings.EnableScript = True ' Violation
    End Sub
    
    ' Violation: XslCompiledTransform parameter with TrustedXslt
    Public Sub ParameterXslCompiledTransformWithTrustedXslt(xsl As XslCompiledTransform)
        xsl.Load("parameter-stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
    End Sub
    
    ' Violation: XsltSettings parameter with EnableScript = True
    Public Sub ParameterEnableScriptTrue(settings As XsltSettings)
        settings.EnableScript = True ' Violation
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt in method return context
    Public Function CreateInsecureXslCompiledTransform() As XslCompiledTransform
        Dim transform As New XslCompiledTransform()
        transform.Load("return-stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
        Return transform
    End Function
    
    ' Non-violation: XslCompiledTransform with Default settings (should not be detected)
    Public Sub SafeXslCompiledTransformWithDefault()
        Dim transform As New XslCompiledTransform()
        transform.Load("stylesheet.xsl", XsltSettings.Default, New XmlUrlResolver()) ' No violation - safe setting
    End Sub
    
    ' Non-violation: EnableScript set to False (should not be detected)
    Public Sub SafeEnableScriptFalse()
        Dim settings As New XsltSettings()
        settings.EnableScript = False ' No violation - safe setting
    End Sub
    
    ' Non-violation: XsltSettings without EnableScript (should not be detected)
    Public Sub SafeXsltSettingsWithoutEnableScript()
        Dim settings As New XsltSettings() With {
            .EnableDocumentFunction = False
        }
    End Sub
    
    ' Non-violation: Comments and strings mentioning TrustedXslt and EnableScript
    Public Sub CommentsAndStrings()
        ' This is about XsltSettings.TrustedXslt and EnableScript = True
        Dim message As String = "XsltSettings.TrustedXslt can be dangerous"
        Console.WriteLine("Avoid EnableScript = True for security")
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt in Select Case
    Public Sub XslCompiledTransformWithTrustedXsltInSelectCase(option As Integer)
        Dim transform As New XslCompiledTransform()
        Select Case option
            Case 1
                transform.Load("option1-stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
            Case 2
                transform.Load("option2-stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
        End Select
    End Sub
    
    ' Violation: EnableScript = True in Select Case
    Public Sub EnableScriptTrueInSelectCase(option As Integer)
        Dim settings As New XsltSettings()
        Select Case option
            Case 1, 2, 3
                settings.EnableScript = True ' Violation
        End Select
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt in Using statement
    Public Sub XslCompiledTransformWithTrustedXsltInUsing()
        Using transform As New XslCompiledTransform()
            transform.Load("using-stylesheet.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
        End Using
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt in While loop
    Public Sub XslCompiledTransformWithTrustedXsltInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim transform As New XslCompiledTransform()
            transform.Load($"while-stylesheet-{counter}.xsl", XsltSettings.TrustedXslt, New XmlUrlResolver()) ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: EnableScript = True in While loop
    Public Sub EnableScriptTrueInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim settings As New XsltSettings()
            settings.EnableScript = True ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: XslCompiledTransform with TrustedXslt and custom resolver
    Public Sub XslCompiledTransformWithTrustedXsltAndCustomResolver()
        Dim transform As New XslCompiledTransform()
        Dim customResolver As New XmlSecureResolver(New XmlUrlResolver(), "http://example.com")
        transform.Load("custom-stylesheet.xsl", XsltSettings.TrustedXslt, customResolver) ' Violation
    End Sub
    
    ' Violation: EnableScript = True with conditional assignment
    Public Sub ConditionalEnableScriptTrueAssignment(enable As Boolean)
        Dim settings As New XsltSettings()
        settings.EnableScript = If(enable, True, False) ' Violation
    End Sub

End Class
