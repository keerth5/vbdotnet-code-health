' VB.NET test file for cq-vbn-0242: Use antiforgery tokens in ASP.NET Core MVC controllers
' This rule detects action methods that handle POST/PUT/PATCH/DELETE without antiforgery tokens

Imports System
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Authorization

' Violation: POST action without antiforgery token
<Controller>
Public Class UserController
    Inherits ControllerBase
    
    ' Violation: HttpPost without ValidateAntiForgeryToken
    <HttpPost>
    Public Function CreateUser(user As User) As ActionResult
        ' Create user logic
        Return Ok(user)
    End Function
End Class

' Violation: Multiple HTTP verbs without antiforgery protection
Public Class ProductController
    Inherits Controller
    
    ' Violation: HttpPut without antiforgery token
    <HttpPut>
    Public Function UpdateProduct(id As Integer, product As Product) As IActionResult
        ' Update logic
        Return Ok()
    End Function
    
    ' Violation: HttpDelete without antiforgery token
    <HttpDelete>
    Public Sub DeleteProduct(id As Integer)
        ' Delete logic
    End Sub
    
    ' Violation: HttpPatch without antiforgery token
    <HttpPatch>
    Public Function PatchProduct(id As Integer) As ActionResult
        ' Patch logic
        Return NoContent()
    End Function
End Class

' Violation: Action method without HTTP verb attribute
Public Class OrderController
    Inherits Controller
    
    ' Violation: Action method that modifies data without proper protection
    Public Function CreateOrder(order As Order) As ActionResult
        ' This method creates data but has no HTTP verb attribute
        ' and no antiforgery protection
        Return Created("", order)
    End Function
    
    Public Function EditOrder(id As Integer, order As Order) As IActionResult
        ' Another violation - modifies data without protection
        Return Ok()
    End Function
    
    Public Function DeleteOrder(id As Integer) As ActionResult
        ' Violation - deletes data without protection
        Return NoContent()
    End Function
End Class

' Violation: Mixed scenarios
Public Class BlogController
    Inherits Controller
    
    ' Violation: HttpPost without ValidateAntiForgeryToken
    <HttpPost>
    <Route("api/blog/create")>
    Public Function CreatePost(post As BlogPost) As ActionResult
        Return Ok()
    End Function
    
    ' Violation: HttpPut with authorization but no antiforgery token
    <HttpPut>
    <Authorize>
    Public Function UpdatePost(id As Integer, post As BlogPost) As IActionResult
        Return Ok()
    End Function
    
    ' Violation: Action method that saves data
    Public Function SavePost(post As BlogPost) As ActionResult
        ' Saves data but has no protection
        Return Ok()
    End Function
    
    ' Violation: Action method that removes data
    Public Function RemovePost(id As Integer) As ActionResult
        ' Removes data but has no protection
        Return Ok()
    End Function
End Class

' Violation: Controller with multiple unsafe actions
Public Class AdminController
    Inherits Controller
    
    ' Violation: HttpPost without antiforgery token
    <HttpPost("admin/users")>
    Public Function CreateUser(user As AdminUser) As ActionResult
        Return Created("", user)
    End Function
    
    ' Violation: HttpDelete without antiforgery token
    <HttpDelete("admin/users/{id}")>
    Public Function DeleteUser(id As Integer) As IActionResult
        Return NoContent()
    End Function
    
    ' Violation: Update method without proper protection
    Public Function UpdateUserRole(userId As Integer, role As String) As ActionResult
        ' Updates user role without protection
        Return Ok()
    End Function
End Class

' Good examples (should not be detected as violations)
Public Class SecureController
    Inherits Controller
    
    ' Good: HttpPost with ValidateAntiForgeryToken
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function CreateSecureUser(user As User) As ActionResult
        Return Ok()
    End Function
    
    ' Good: HttpPut with AutoValidateAntiforgeryToken
    <HttpPut>
    <AutoValidateAntiforgeryToken>
    Public Function UpdateSecureUser(id As Integer, user As User) As IActionResult
        Return Ok()
    End Function
    
    ' Good: HttpDelete with ValidateAntiForgeryToken
    <HttpDelete>
    <ValidateAntiForgeryToken>
    Public Function DeleteSecureUser(id As Integer) As ActionResult
        Return NoContent()
    End Function
    
    ' Good: GET method (safe operation)
    <HttpGet>
    Public Function GetUsers() As IActionResult
        Return Ok()
    End Function
    
    ' Good: GET method without explicit attribute
    Public Function ListUsers() As ActionResult
        ' GET operations are safe and don't need antiforgery tokens
        Return Ok()
    End Function
End Class

' Data classes for the examples
Public Class User
    Public Property Id As Integer
    Public Property Name As String
    Public Property Email As String
End Class

Public Class Product
    Public Property Id As Integer
    Public Property Name As String
    Public Property Price As Decimal
End Class

Public Class Order
    Public Property Id As Integer
    Public Property UserId As Integer
    Public Property Total As Decimal
End Class

Public Class BlogPost
    Public Property Id As Integer
    Public Property Title As String
    Public Property Content As String
End Class

Public Class AdminUser
    Public Property Id As Integer
    Public Property Username As String
    Public Property Role As String
End Class
