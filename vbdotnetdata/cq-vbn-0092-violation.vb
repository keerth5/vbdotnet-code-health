' Test file for cq-vbn-0092: Property names should not match get methods
' Rule should detect properties that have names matching get method names

Imports System

Public Class PropertyGetMethodConflicts
    
    ' Violation 1: Property with matching get method
    Public Property UserName As String
    
    Public Function GetUserName() As String
        Return "John Doe"
    End Function
    
    ' Violation 2: Property with matching get method
    Protected Property EmailAddress As String
    
    Protected Function GetEmailAddress() As String
        Return "user@example.com"
    End Function
    
    ' Violation 3: Property with matching get method
    Friend Property AccountBalance As Decimal
    
    Friend Function GetAccountBalance() As Decimal
        Return 1000.0
    End Function
    
    ' Violation 4: Property with matching get method
    Public Property IsActive As Boolean
    
    Public Function GetIsActive() As Boolean
        Return True
    End Function
    
    ' Violation 5: Property with matching get method
    Protected Property LastLoginDate As DateTime
    
    Protected Function GetLastLoginDate() As DateTime
        Return DateTime.Now
    End Function
    
    ' This should NOT be detected - property without matching get method
    Public Property CustomerName As String
    
    Public Function RetrieveCustomerName() As String
        Return "Customer"
    End Function
    
    ' This should NOT be detected - different access levels
    Private Property InternalId As Integer
    
    Public Function GetInternalId() As Integer
        Return 123
    End Function
    
End Class

Public Class AnotherConflictExample
    
    ' Violation 6: Property with matching get method
    Public Property ProductName As String
    
    Public Function GetProductName() As String
        Return "Product A"
    End Function
    
    ' Violation 7: Property with matching get method
    Friend Property CategoryId As Integer
    
    Friend Function GetCategoryId() As Integer
        Return 1
    End Function
    
    ' Violation 8: Property with matching get method
    Protected Property Description As String
    
    Protected Function GetDescription() As String
        Return "Product description"
    End Function
    
    ' This should NOT be detected - no matching get method
    Public Property Price As Decimal
    
    Public Function CalculatePrice() As Decimal
        Return 99.99
    End Function
    
End Class

Friend Class ThirdConflictExample
    
    ' Violation 9: Property with matching get method
    Public Property Status As String
    
    Public Function GetStatus() As String
        Return "Active"
    End Function
    
    ' Violation 10: Property with matching get method
    Protected Property Priority As Integer
    
    Protected Function GetPriority() As Integer
        Return 1
    End Function
    
    ' Violation 11: Property with matching get method
    Friend Property CreatedDate As DateTime
    
    Friend Function GetCreatedDate() As DateTime
        Return DateTime.Now
    End Function
    
End Class

Public Class NoConflictExample
    
    ' This should NOT be detected - no matching get method
    Public Property OrderId As Integer
    
    Public Function GenerateOrderId() As Integer
        Return 12345
    End Function
    
    ' This should NOT be detected - no matching get method
    Protected Property Quantity As Integer
    
    Protected Function ValidateQuantity() As Boolean
        Return True
    End Function
    
    ' This should NOT be detected - no matching get method
    Friend Property ShippingAddress As String
    
    Friend Function FormatShippingAddress() As String
        Return "Formatted address"
    End Function
    
End Class

Public Class EdgeCaseExample
    
    ' Violation 12: Property with matching get method (case insensitive)
    Public Property itemCount As Integer
    
    Public Function GetItemCount() As Integer
        Return 10
    End Function
    
    ' Violation 13: Property with matching get method
    Protected Property TotalAmount As Decimal
    
    Protected Function GetTotalAmount() As Decimal
        Return 500.0
    End Function
    
    ' This should NOT be detected - method doesn't start with "Get"
    Public Property DiscountRate As Double
    
    Public Function CalculateDiscountRate() As Double
        Return 0.1
    End Function
    
End Class
