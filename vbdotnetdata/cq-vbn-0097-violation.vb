' Test file for cq-vbn-0097: Do not initialize unnecessarily
' Rule should detect fields initialized to their default values unnecessarily

Imports System

Public Class UnnecessaryInitializations
    
    ' Violation 1: Integer field initialized to default value (0)
    Private count As Integer = 0
    
    ' Violation 2: String field initialized to default value (Nothing/empty string)
    Private name As String = Nothing
    
    ' Violation 3: Boolean field initialized to default value (False)
    Protected isActive As Boolean = False
    
    ' Violation 4: Double field initialized to default value (0)
    Public value As Double = 0
    
    ' Violation 5: Object field initialized to default value (Nothing)
    Friend data As Object = Nothing
    
    ' Violation 6: String field initialized to empty string
    Dim description As String = ""
    
    ' Violation 7: Integer field initialized to zero
    Public Shared counter As Integer = 0
    
    ' Violation 8: Boolean field initialized to False
    Private Shared isEnabled As Boolean = False
    
    ' Violation 9: String field initialized to Nothing
    Protected Shared message As String = Nothing
    
    ' Violation 10: Object field initialized to Nothing
    Friend Shared instance As Object = Nothing
    
    ' This should NOT be detected - field initialized to non-default value
    Private maxCount As Integer = 10
    
    ' This should NOT be detected - field initialized to non-default value
    Protected userName As String = "admin"
    
    ' This should NOT be detected - field initialized to non-default value
    Public isReady As Boolean = True
    
    ' This should NOT be detected - field not initialized
    Private uninitializedField As Integer
    
    ' This should NOT be detected - field initialized to non-default value
    Friend rate As Double = 3.14
    
End Class

Public Class MoreUnnecessaryInitializations
    
    ' Violation 11: Dim field initialized to default value
    Dim itemCount As Integer = 0
    
    ' Violation 12: Private field initialized to default value
    Private isComplete As Boolean = False
    
    ' Violation 13: Protected field initialized to default value
    Protected totalAmount As Double = 0
    
    ' Violation 14: Public field initialized to default value
    Public errorMessage As String = Nothing
    
    ' Violation 15: Friend field initialized to default value
    Friend result As Object = Nothing
    
    ' This should NOT be detected - constant declaration
    Public Const DefaultTimeout As Integer = 30
    
    ' This should NOT be detected - ReadOnly field
    Public ReadOnly CreatedDate As DateTime = DateTime.Now
    
End Class

Friend Class ThirdInitializationExample
    
    ' Violation 16: Integer initialized to zero
    Private pageNumber As Integer = 0
    
    ' Violation 17: String initialized to empty string
    Dim fileName As String = ""
    
    ' Violation 18: Boolean initialized to False
    Protected hasChanges As Boolean = False
    
    ' Violation 19: Object initialized to Nothing
    Public currentUser As Object = Nothing
    
    ' This should NOT be detected - array initialization
    Private items As String() = New String() {}
    
    ' This should NOT be detected - constructor call
    Friend list As New List(Of String)
    
End Class

Public Class EdgeCaseInitializations
    
    ' Violation 20: Double initialized to 0
    Private temperature As Double = 0
    
    ' Violation 21: String initialized to Nothing (explicit)
    Protected connectionString As String = Nothing
    
    ' Violation 22: Boolean initialized to False (explicit)
    Public isValid As Boolean = False
    
    ' This should NOT be detected - decimal with non-zero value
    Private price As Decimal = 0.0
    
    ' This should NOT be detected - string with actual content
    Friend title As String = "Untitled"
    
    ' This should NOT be detected - property instead of field
    Public Property Status As String = "Active"
    
End Class

Public Class StaticInitializations
    
    ' Violation 23: Static integer initialized to zero
    Private Shared totalUsers As Integer = 0
    
    ' Violation 24: Static string initialized to Nothing
    Protected Shared applicationTitle As String = Nothing
    
    ' Violation 25: Static boolean initialized to False
    Public Shared isInitialized As Boolean = False
    
    ' This should NOT be detected - static field with non-default value
    Friend Shared MaxRetries As Integer = 3
    
    ' This should NOT be detected - static ReadOnly field
    Public Shared ReadOnly Version As String = "1.0"
    
End Class
