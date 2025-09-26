' Test file for cq-vbn-0218: Do Not Disable HTTP Header Checking
' This rule detects disabling of HTTP header checking

Imports System
Imports System.Configuration
Imports System.Web.Configuration

Public Class DisableHttpHeaderCheckingViolations
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing set to True
    Public Sub DisableHeaderCheckingWithHttpRuntimeSection()
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = True ' Violation
    End Sub
    
    ' Violation: Multiple HttpRuntimeSection.UseUnsafeHeaderParsing set to True
    Public Sub MultipleDisableHeaderChecking()
        Dim httpRuntime1 As New HttpRuntimeSection()
        Dim httpRuntime2 As New HttpRuntimeSection()
        
        httpRuntime1.UseUnsafeHeaderParsing = True ' Violation
        httpRuntime2.UseUnsafeHeaderParsing = True ' Violation
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in loop
    Public Sub DisableHeaderCheckingInLoop()
        For i As Integer = 0 To 3
            Dim httpRuntime As New HttpRuntimeSection()
            httpRuntime.UseUnsafeHeaderParsing = True ' Violation
        Next
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in conditional
    Public Sub ConditionalDisableHeaderChecking(disable As Boolean)
        Dim httpRuntime As New HttpRuntimeSection()
        If disable Then
            httpRuntime.UseUnsafeHeaderParsing = True ' Violation
        End If
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in Try-Catch
    Public Sub DisableHeaderCheckingInTryCatch()
        Try
            Dim httpRuntime As New HttpRuntimeSection()
            httpRuntime.UseUnsafeHeaderParsing = True ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing with field
    Private httpRuntimeField As New HttpRuntimeSection()
    
    Public Sub FieldDisableHeaderChecking()
        httpRuntimeField.UseUnsafeHeaderParsing = True ' Violation
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing with parameter
    Public Sub ParameterDisableHeaderChecking(httpRuntime As HttpRuntimeSection)
        httpRuntime.UseUnsafeHeaderParsing = True ' Violation
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in method return context
    Public Function CreateInsecureHttpRuntimeSection() As HttpRuntimeSection
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = True ' Violation
        Return httpRuntime
    End Function
    
    ' Non-violation: HttpRuntimeSection.UseUnsafeHeaderParsing set to False (should not be detected)
    Public Sub EnableHeaderChecking()
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = False ' No violation - enabling header checking
    End Sub
    
    ' Non-violation: Other HttpRuntimeSection properties (should not be detected)
    Public Sub OtherHttpRuntimeSectionProperties()
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.MaxRequestLength = 4096 ' No violation - different property
        httpRuntime.ExecutionTimeout = TimeSpan.FromSeconds(110) ' No violation - different property
    End Sub
    
    ' Non-violation: Comments and strings mentioning UseUnsafeHeaderParsing
    Public Sub CommentsAndStrings()
        ' This is about HttpRuntimeSection.UseUnsafeHeaderParsing = True
        Dim message As String = "Do not set UseUnsafeHeaderParsing to True"
        Console.WriteLine("HTTP header checking should not be disabled")
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in Select Case
    Public Sub DisableHeaderCheckingInSelectCase(option As Integer)
        Dim httpRuntime As New HttpRuntimeSection()
        Select Case option
            Case 1
                httpRuntime.UseUnsafeHeaderParsing = True ' Violation
            Case 2
                httpRuntime.UseUnsafeHeaderParsing = True ' Violation
        End Select
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in Using statement
    Public Sub DisableHeaderCheckingInUsing()
        Using stream As New System.IO.MemoryStream()
            Dim httpRuntime As New HttpRuntimeSection()
            httpRuntime.UseUnsafeHeaderParsing = True ' Violation
        End Using
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in While loop
    Public Sub DisableHeaderCheckingInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim httpRuntime As New HttpRuntimeSection()
            httpRuntime.UseUnsafeHeaderParsing = True ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing with variable assignment
    Public Sub DisableHeaderCheckingWithVariableAssignment()
        Dim httpRuntime As New HttpRuntimeSection()
        Dim disableHeaderChecking As Boolean = True
        httpRuntime.UseUnsafeHeaderParsing = disableHeaderChecking ' Violation (assuming pattern matches True literal)
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing with constant
    Private Const DISABLE_HEADER_CHECKING As Boolean = True
    
    Public Sub DisableHeaderCheckingWithConstant()
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = DISABLE_HEADER_CHECKING ' Violation (if pattern matches True)
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in lambda expression
    Public Sub DisableHeaderCheckingInLambda()
        Dim action = Sub()
                         Dim httpRuntime As New HttpRuntimeSection()
                         httpRuntime.UseUnsafeHeaderParsing = True ' Violation
                     End Sub
        action()
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in property
    Public WriteOnly Property DisableHeaderChecking As Boolean
        Set(value As Boolean)
            If value Then
                Dim httpRuntime As New HttpRuntimeSection()
                httpRuntime.UseUnsafeHeaderParsing = True ' Violation
            End If
        End Set
    End Property
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in constructor
    Public Sub New(disableHeaderChecking As Boolean)
        If disableHeaderChecking Then
            Dim httpRuntime As New HttpRuntimeSection()
            httpRuntime.UseUnsafeHeaderParsing = True ' Violation
        End If
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing with method call result
    Public Sub DisableHeaderCheckingWithMethodCall()
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = GetDisableFlag() ' Violation (if GetDisableFlag returns True)
    End Sub
    
    Private Function GetDisableFlag() As Boolean
        Return True
    End Function
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing with conditional operator
    Public Sub DisableHeaderCheckingWithConditionalOperator(condition As Boolean)
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = If(condition, True, False) ' Violation
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in event handler
    Public Sub OnConfigurationEvent(sender As Object, e As EventArgs)
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = True ' Violation
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in delegate
    Public Sub DisableHeaderCheckingWithDelegate()
        Dim del As Action = Sub()
                                Dim httpRuntime As New HttpRuntimeSection()
                                httpRuntime.UseUnsafeHeaderParsing = True ' Violation
                            End Sub
        del()
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in static context
    Shared Sub New()
        Dim httpRuntime As New HttpRuntimeSection()
        httpRuntime.UseUnsafeHeaderParsing = True ' Violation
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing with object initializer
    Public Sub DisableHeaderCheckingWithObjectInitializer()
        Dim httpRuntime As New HttpRuntimeSection() With {
            .UseUnsafeHeaderParsing = True ' Violation
        }
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in array context
    Public Sub DisableHeaderCheckingInArray()
        Dim httpRuntimes() As HttpRuntimeSection = {
            New HttpRuntimeSection() With {.UseUnsafeHeaderParsing = True} ' Violation
        }
    End Sub
    
    ' Violation: HttpRuntimeSection.UseUnsafeHeaderParsing in collection
    Public Sub DisableHeaderCheckingInCollection()
        Dim httpRuntimes As New List(Of HttpRuntimeSection) From {
            New HttpRuntimeSection() With {.UseUnsafeHeaderParsing = True} ' Violation
        }
    End Sub
    
    ' Note: XML configuration violations would typically be in web.config files
    ' These are conceptual VB.NET representations of the XML pattern
    Public Sub ConceptualXmlConfigViolation()
        ' This represents: <httpRuntime useUnsafeHeaderParsing="true" />
        Dim xmlConfig As String = "<httpRuntime useUnsafeHeaderParsing=""true"" />" ' Violation pattern in XML
    End Sub
    
    Public Sub AnotherConceptualXmlConfigViolation()
        ' This represents: <httpRuntime maxRequestLength="4096" useUnsafeHeaderParsing="true" />
        Dim xmlConfig As String = "<httpRuntime maxRequestLength=""4096"" useUnsafeHeaderParsing=""true"" />" ' Violation pattern in XML
    End Sub

End Class
