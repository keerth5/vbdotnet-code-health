' Test file for cq-vbn-0209: Mark verb handlers with ValidateAntiForgeryToken
' This rule detects HTTP verb handlers that should be marked with ValidateAntiForgeryToken

Imports System
Imports System.Web.Mvc

Public Class AntiForgeryTokenViolations
    Inherits Controller
    
    ' Violation: HttpPost without ValidateAntiForgeryToken
    <HttpPost>
    Public Function PostWithoutAntiForgeryToken() As ActionResult ' Violation - missing ValidateAntiForgeryToken
        Return View()
    End Function
    
    ' Violation: HttpPut without ValidateAntiForgeryToken
    <HttpPut>
    Public Function PutWithoutAntiForgeryToken() As ActionResult ' Violation - missing ValidateAntiForgeryToken
        Return View()
    End Function
    
    ' Violation: HttpDelete without ValidateAntiForgeryToken
    <HttpDelete>
    Public Function DeleteWithoutAntiForgeryToken() As ActionResult ' Violation - missing ValidateAntiForgeryToken
        Return View()
    End Function
    
    ' Violation: HttpPatch without ValidateAntiForgeryToken
    <HttpPatch>
    Public Function PatchWithoutAntiForgeryToken() As ActionResult ' Violation - missing ValidateAntiForgeryToken
        Return View()
    End Function
    
    ' Violation: Multiple HttpPost methods without ValidateAntiForgeryToken
    <HttpPost>
    Public Function FirstPostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    
    <HttpPost>
    Public Function SecondPostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPost with parameters but without ValidateAntiForgeryToken
    <HttpPost>
    Public Function PostWithParametersWithoutAntiForgeryToken(id As Integer, name As String) As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPut with model parameter but without ValidateAntiForgeryToken
    <HttpPut>
    Public Function PutWithModelWithoutAntiForgeryToken(model As Object) As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpDelete with ID parameter but without ValidateAntiForgeryToken
    <HttpDelete>
    Public Function DeleteWithIdWithoutAntiForgeryToken(id As Integer) As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: Friend HttpPost without ValidateAntiForgeryToken
    <HttpPost>
    Friend Function FriendPostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPost Sub without ValidateAntiForgeryToken
    <HttpPost>
    Public Sub PostSubWithoutAntiForgeryToken() ' Violation
        ' Do something
    End Sub
    
    ' Violation: HttpPost Function returning different types without ValidateAntiForgeryToken
    <HttpPost>
    Public Function PostReturningJsonWithoutAntiForgeryToken() As JsonResult ' Violation
        Return Json(New With {.success = True})
    End Function
    
    <HttpPost>
    Public Function PostReturningPartialViewWithoutAntiForgeryToken() As PartialViewResult ' Violation
        Return PartialView()
    End Function
    
    ' Non-violation: HttpPost with ValidateAntiForgeryToken (should not be detected)
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function SafePostWithAntiForgeryToken() As ActionResult ' No violation - has ValidateAntiForgeryToken
        Return View()
    End Function
    
    ' Non-violation: HttpPut with ValidateAntiForgeryToken (should not be detected)
    <HttpPut>
    <ValidateAntiForgeryToken>
    Public Function SafePutWithAntiForgeryToken() As ActionResult ' No violation - has ValidateAntiForgeryToken
        Return View()
    End Function
    
    ' Non-violation: HttpGet without ValidateAntiForgeryToken (should not be detected)
    <HttpGet>
    Public Function SafeGetWithoutAntiForgeryToken() As ActionResult ' No violation - GET method
        Return View()
    End Function
    
    ' Non-violation: Regular method without HTTP attributes (should not be detected)
    Public Function RegularMethodWithoutHttpAttribute() As ActionResult ' No violation - not an HTTP verb handler
        Return View()
    End Function
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about <HttpPost> Public Function without ValidateAntiForgeryToken
        Dim message As String = "HttpPost methods should have ValidateAntiForgeryToken"
        Console.WriteLine("Always use ValidateAntiForgeryToken with state-changing HTTP verbs")
    End Sub
    
    ' Violation: HttpPost with custom action name without ValidateAntiForgeryToken
    <HttpPost>
    <ActionName("CustomPost")>
    Public Function CustomPostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPost with route attribute without ValidateAntiForgeryToken
    <HttpPost>
    <Route("api/custom-post")>
    Public Function RoutePostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPost with multiple attributes but no ValidateAntiForgeryToken
    <HttpPost>
    <Authorize>
    <ActionName("AuthorizedPost")>
    Public Function AuthorizedPostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    
    ' Non-violation: HttpPost with ValidateAntiForgeryToken and other attributes (should not be detected)
    <HttpPost>
    <ValidateAntiForgeryToken>
    <Authorize>
    Public Function SafeAuthorizedPostWithAntiForgeryToken() As ActionResult ' No violation - has ValidateAntiForgeryToken
        Return View()
    End Function
    
    ' Violation: HttpPost in conditional compilation without ValidateAntiForgeryToken
    #If DEBUG Then
    <HttpPost>
    Public Function DebugPostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    #End If
    
    ' Violation: HttpPost with async method without ValidateAntiForgeryToken
    <HttpPost>
    Public Async Function AsyncPostWithoutAntiForgeryToken() As Task(Of ActionResult) ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPost with overloaded methods without ValidateAntiForgeryToken
    <HttpPost>
    Public Function OverloadedPostWithoutAntiForgeryToken() As ActionResult ' Violation
        Return View()
    End Function
    
    <HttpPost>
    Public Function OverloadedPostWithoutAntiForgeryToken(id As Integer) As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPost with generic return type without ValidateAntiForgeryToken
    <HttpPost>
    Public Function GenericPostWithoutAntiForgeryToken(Of T)() As ActionResult(Of T) ' Violation
        Return Nothing
    End Function
    
    ' Violation: HttpPost with optional parameters without ValidateAntiForgeryToken
    <HttpPost>
    Public Function OptionalParamsPostWithoutAntiForgeryToken(Optional id As Integer = 0, Optional name As String = "") As ActionResult ' Violation
        Return View()
    End Function
    
    ' Violation: HttpPost with ParamArray without ValidateAntiForgeryToken
    <HttpPost>
    Public Function ParamArrayPostWithoutAntiForgeryToken(ParamArray values As String()) As ActionResult ' Violation
        Return View()
    End Function

End Class
