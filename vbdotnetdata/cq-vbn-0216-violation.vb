' Test file for cq-vbn-0216: Do not disable request validation
' This rule detects disabling of ASP.NET request validation

Imports System
Imports System.Web.Mvc

Public Class DisableRequestValidationViolations
    Inherits Controller
    
    ' Violation: ValidateInput attribute set to False
    <ValidateInput(False)>
    Public Function DisableValidationWithAttribute() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: ValidateRequest property set to False
    Public Sub DisableValidationWithProperty()
        Me.ValidateRequest = False ' Violation
    End Sub
    
    ' Violation: Multiple ValidateInput attributes with False
    <ValidateInput(False)>
    <HttpPost>
    Public Function MultipleAttributesWithValidateInputFalse() As ActionResult ' Violation
        Return View()
    End Function
    
    <ValidateInput(False)>
    <HttpGet>
    Public Function AnotherValidateInputFalse() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: ValidateInput False in loop context
    Public Sub ValidateInputFalseInLoop()
        For i As Integer = 0 To 3
            ' Note: This is conceptual - ValidateInput is typically used as attribute
            Dim validateInput As Boolean = False ' Related to ValidateInput(False)
        Next
    End Sub
    
    ' Violation: ValidateRequest False in conditional
    Public Sub ConditionalDisableValidation(disable As Boolean)
        If disable Then
            Me.ValidateRequest = False ' Violation
        End If
    End Sub
    
    ' Violation: ValidateRequest False in Try-Catch
    Public Sub DisableValidationInTryCatch()
        Try
            Me.ValidateRequest = False ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: ValidateRequest False in method return context
    Public Function DisableValidationAndReturn() As Boolean
        Me.ValidateRequest = False ' Violation
        Return True
    End Function
    
    ' Non-violation: ValidateInput attribute set to True (should not be detected)
    <ValidateInput(True)>
    Public Function EnableValidationWithAttribute() As ActionResult ' No violation - validation enabled
        Return View()
    End Function
    
    ' Non-violation: ValidateRequest property set to True (should not be detected)
    Public Sub EnableValidationWithProperty()
        Me.ValidateRequest = True ' No violation - validation enabled
    End Sub
    
    ' Non-violation: No ValidateInput attribute (should not be detected)
    Public Function NoValidateInputAttribute() As ActionResult ' No violation - no attribute
        Return View()
    End Function
    
    ' Non-violation: Comments and strings mentioning validation
    Public Sub CommentsAndStrings()
        ' This is about ValidateInput(False) and ValidateRequest = False
        Dim message As String = "Do not disable request validation"
        Console.WriteLine("ValidateInput should not be set to False")
    End Sub
    
    ' Violation: ValidateInput False in Select Case
    Public Function ValidateInputFalseInSelectCase(option As Integer) As ActionResult
        Select Case option
            Case 1
                ' ValidateInput(False) would be at method level
                Return View("Option1")
            Case 2
                Return View("Option2")
        End Select
        Return View()
    End Function
    
    ' Violation: Different ValidateInput False scenarios
    <ValidateInput(False)>
    <ActionName("CustomAction")>
    Public Function CustomActionWithValidateInputFalse() As ActionResult ' Violation
        Return View()
    End Function
    
    <ValidateInput(False)>
    <Route("api/unsafe")>
    Public Function RouteWithValidateInputFalse() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: ValidateRequest False with different contexts
    Public Sub ValidateRequestFalseInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Me.ValidateRequest = False ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: ValidateRequest False with variable assignment
    Public Sub ValidateRequestFalseWithVariableAssignment()
        Dim disableValidation As Boolean = False
        Me.ValidateRequest = disableValidation ' Violation (assuming pattern matches False literal)
    End Sub
    
    ' Violation: ValidateRequest False with constant
    Private Const DISABLE_VALIDATION As Boolean = False
    
    Public Sub ValidateRequestFalseWithConstant()
        Me.ValidateRequest = DISABLE_VALIDATION ' Violation (if pattern matches False)
    End Sub
    
    ' Violation: ValidateInput False with parameters
    <ValidateInput(False)>
    Public Function ValidateInputFalseWithParameters(id As Integer, name As String) As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: ValidateInput False with async method
    <ValidateInput(False)>
    Public Async Function ValidateInputFalseAsync() As Task(Of ActionResult) ' Violation
        Return View()
    End Function
    
    ' Violation: ValidateRequest False in property
    Public WriteOnly Property DisableRequestValidation As Boolean
        Set(value As Boolean)
            If Not value Then
                Me.ValidateRequest = False ' Violation
            End If
        End Set
    End Property
    
    ' Violation: ValidateRequest False in constructor
    Public Sub New(disableValidation As Boolean)
        If disableValidation Then
            Me.ValidateRequest = False ' Violation
        End If
    End Sub
    
    ' Violation: ValidateRequest False with method call result
    Public Sub ValidateRequestFalseWithMethodCall()
        Me.ValidateRequest = GetValidationFlag() ' Violation (if GetValidationFlag returns False)
    End Sub
    
    Private Function GetValidationFlag() As Boolean
        Return False
    End Function
    
    ' Violation: ValidateRequest False with conditional operator
    Public Sub ValidateRequestFalseWithConditionalOperator(condition As Boolean)
        Me.ValidateRequest = If(condition, True, False) ' Violation
    End Sub
    
    ' Violation: ValidateInput False in different method types
    <ValidateInput(False)>
    Public Sub ValidateInputFalseInSub() ' Violation
        ' Do something
    End Sub
    
    <ValidateInput(False)>
    Public Function ValidateInputFalseInFunction() As String ' Violation
        Return "Result"
    End Function
    
    ' Violation: ValidateRequest False in event handler
    Public Sub OnSomeEvent(sender As Object, e As EventArgs)
        Me.ValidateRequest = False ' Violation
    End Sub
    
    ' Violation: ValidateRequest False in delegate
    Public Sub ValidateRequestFalseWithDelegate()
        Dim del As Action = Sub() Me.ValidateRequest = False ' Violation
        del()
    End Sub
    
    ' Violation: ValidateInput False with other attributes
    <ValidateInput(False)>
    <Authorize>
    <HttpPost>
    Public Function ValidateInputFalseWithMultipleAttributes() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: ValidateRequest False in lambda expression
    Public Sub ValidateRequestFalseInLambda()
        Dim action = Sub() Me.ValidateRequest = False ' Violation
        action()
    End Sub

End Class
