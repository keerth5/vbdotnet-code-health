' Test file for cq-vbn-0261: Non-constant fields should not be visible
' Rule should detect: Public/Protected static fields that are not Const or ReadOnly

Public Class NonConstantFieldsTest
    
    ' VIOLATION: Public static field that is not Const or ReadOnly
    Public Shared NonConstField As Integer = 42
    
    ' VIOLATION: Protected static field that is not Const or ReadOnly  
    Protected Shared ProtectedField As String = "test"
    
    ' VIOLATION: Public instance field that is not Const or ReadOnly
    Public InstanceField As Double = 3.14
    
    ' VIOLATION: Protected instance field that is not Const or ReadOnly
    Protected ProtectedInstanceField As Boolean = True
    
    ' GOOD: Public Const field - should NOT be flagged
    Public Const ConstantValue As Integer = 100
    
    ' GOOD: Public ReadOnly field - should NOT be flagged
    Public ReadOnly ReadOnlyValue As String = "readonly"
    
    ' GOOD: Public Shared Const field - should NOT be flagged
    Public Shared Const SharedConstant As Double = 2.718
    
    ' GOOD: Public Shared ReadOnly field - should NOT be flagged
    Public Shared ReadOnly SharedReadOnly As Boolean = False
    
    ' GOOD: Private field - should NOT be flagged (not visible)
    Private PrivateField As Integer = 123
    
    ' GOOD: Friend field - should NOT be flagged (internal visibility)
    Friend FriendField As String = "friend"
    
End Class
