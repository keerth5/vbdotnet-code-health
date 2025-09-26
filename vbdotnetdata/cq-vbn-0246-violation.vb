' VB.NET test file for cq-vbn-0246: Miss HttpVerb attribute for action methods
' This rule detects action methods that modify data without proper HttpVerb attributes

Imports System
Imports Microsoft.AspNetCore.Mvc

' Violation: Action methods that modify data without HttpVerb attributes
Public Class UserController
    Inherits Controller
    
    ' Violation: Create method without HttpPost attribute
    Public Function CreateUser(user As User) As ActionResult
        ' This method creates data but lacks HttpPost attribute
        Return Ok(user)
    End Function
    
    ' Violation: Edit method without HttpPut/HttpPost attribute
    Public Function EditUser(id As Integer, user As User) As ActionResult
        ' This method edits data but lacks proper HTTP verb
        Return Ok()
    End Function
    
    ' Violation: Delete method without HttpDelete attribute
    Public Function DeleteUser(id As Integer) As ActionResult
        ' This method deletes data but lacks HttpDelete attribute
        Return NoContent()
    End Function
    
    ' Violation: Update method without HttpPut attribute
    Public Function UpdateUser(id As Integer, user As User) As IActionResult
        ' This method updates data but lacks HttpPut attribute
        Return Ok()
    End Function
End Class

' Violation: Product controller with unsafe methods
Public Class ProductController
    Inherits Controller
    
    ' Violation: Save method without proper HTTP verb
    Public Function SaveProduct(product As Product) As ActionResult
        ' Saves data but no HTTP verb attribute
        Return Created("", product)
    End Function
    
    ' Violation: Remove method without HttpDelete
    Public Function RemoveProduct(id As Integer) As ActionResult
        ' Removes data but no HttpDelete attribute
        Return Ok()
    End Function
    
    ' Violation: Create method with parameters
    Public Function CreateProduct(name As String, price As Decimal) As IActionResult
        ' Creates data but no HttpPost attribute
        Return Ok()
    End Function
    
    ' Violation: Edit method with multiple parameters
    Public Function EditProduct(id As Integer, name As String, price As Decimal) As ActionResult
        ' Edits data but no HTTP verb attribute
        Return Ok()
    End Function
End Class

' Violation: Order controller with various unsafe methods
Public Class OrderController
    Inherits Controller
    
    ' Violation: Create order without HttpPost
    Public Function CreateOrder(customerId As Integer, items As OrderItem()) As ActionResult
        ' Creates order but no HttpPost attribute
        Return Ok()
    End Function
    
    ' Violation: Update order status without HttpPut
    Public Function UpdateOrderStatus(orderId As Integer, status As String) As IActionResult
        ' Updates order but no HttpPut attribute
        Return Ok()
    End Function
    
    ' Violation: Delete order without HttpDelete
    Public Sub DeleteOrder(orderId As Integer)
        ' Deletes order but no HttpDelete attribute
    End Sub
    
    ' Violation: Save order changes without proper verb
    Public Function SaveOrderChanges(order As Order) As ActionResult
        ' Saves changes but no HTTP verb attribute
        Return Ok()
    End Function
End Class

' Violation: Admin controller with management methods
Public Class AdminController
    Inherits Controller
    
    ' Violation: Create admin user without HttpPost
    Public Function CreateAdminUser(username As String, role As String) As ActionResult
        ' Creates admin user but no HttpPost
        Return Ok()
    End Function
    
    ' Violation: Delete all logs without HttpDelete
    Public Function DeleteAllLogs() As IActionResult
        ' Deletes all logs but no HttpDelete
        Return Ok()
    End Function
    
    ' Violation: Update system settings without HttpPut
    Public Function UpdateSystemSettings(settings As SystemSettings) As ActionResult
        ' Updates settings but no HttpPut
        Return Ok()
    End Function
    
    ' Violation: Remove expired sessions without proper verb
    Public Function RemoveExpiredSessions() As ActionResult
        ' Removes sessions but no HTTP verb
        Return Ok()
    End Function
End Class

' Violation: Blog controller with content management
Public Class BlogController
    Inherits Controller
    
    ' Violation: Create post without HttpPost
    Public Function CreatePost(title As String, content As String, authorId As Integer) As ActionResult
        ' Creates blog post but no HttpPost
        Return Ok()
    End Function
    
    ' Violation: Edit post without HttpPut
    Public Function EditPost(id As Integer, title As String, content As String) As IActionResult
        ' Edits post but no HttpPut
        Return Ok()
    End Function
    
    ' Violation: Delete comment without HttpDelete
    Public Function DeleteComment(commentId As Integer) As ActionResult
        ' Deletes comment but no HttpDelete
        Return Ok()
    End Function
    
    ' Violation: Update post tags without HttpPut
    Public Function UpdatePostTags(postId As Integer, tags As String()) As ActionResult
        ' Updates tags but no HttpPut
        Return Ok()
    End Function
End Class

' Violation: API controller with data modification methods
Public Class ApiController
    Inherits ControllerBase
    
    ' Violation: Create resource without HttpPost
    Public Function CreateResource(resource As ApiResource) As ActionResult
        ' Creates resource but no HttpPost
        Return Ok()
    End Function
    
    ' Violation: Update resource without HttpPut
    Public Function UpdateResource(id As Integer, resource As ApiResource) As IActionResult
        ' Updates resource but no HttpPut
        Return Ok()
    End Function
    
    ' Violation: Delete resource without HttpDelete
    Public Function DeleteResource(id As Integer) As ActionResult
        ' Deletes resource but no HttpDelete
        Return NoContent()
    End Function
    
    ' Violation: Save batch changes without proper verb
    Public Function SaveBatchChanges(changes As BatchChanges) As ActionResult
        ' Saves batch changes but no HTTP verb
        Return Ok()
    End Function
End Class

' Good examples (should not be detected as violations)
Public Class SecureController
    Inherits Controller
    
    ' Good: Create method with HttpPost
    <HttpPost>
    Public Function CreateSecureUser(user As User) As ActionResult
        Return Ok()
    End Function
    
    ' Good: Update method with HttpPut
    <HttpPut>
    Public Function UpdateSecureUser(id As Integer, user As User) As IActionResult
        Return Ok()
    End Function
    
    ' Good: Delete method with HttpDelete
    <HttpDelete>
    Public Function DeleteSecureUser(id As Integer) As ActionResult
        Return NoContent()
    End Function
    
    ' Good: GET method (safe operation, doesn't need special protection)
    <HttpGet>
    Public Function GetUsers() As IActionResult
        Return Ok()
    End Function
    
    ' Good: Safe read operation without explicit HttpGet (default is GET)
    Public Function ListUsers() As ActionResult
        ' This is a safe read operation
        Return Ok()
    End Function
    
    ' Good: Safe query operation
    Public Function SearchUsers(query As String) As ActionResult
        ' This is a safe search operation
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
    Public Property CustomerId As Integer
    Public Property Items As OrderItem()
    Public Property Status As String
End Class

Public Class OrderItem
    Public Property ProductId As Integer
    Public Property Quantity As Integer
    Public Property Price As Decimal
End Class

Public Class SystemSettings
    Public Property Theme As String
    Public Property Language As String
    Public Property TimeZone As String
End Class

Public Class ApiResource
    Public Property Id As Integer
    Public Property Name As String
    Public Property Data As String
End Class

Public Class BatchChanges
    Public Property Operations As String()
    Public Property Timestamp As DateTime
End Class
