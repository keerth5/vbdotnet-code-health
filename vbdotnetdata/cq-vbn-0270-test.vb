' Test file for cq-vbn-0270: Operator overloads have named alternates
' Rule should detect: Operator overloads without corresponding named methods

' VIOLATION: Class has + operator but no Add method
Public Class BadOperatorAlternates1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Shared Operator +(left As BadOperatorAlternates1, right As BadOperatorAlternates1) As BadOperatorAlternates1
        Return New BadOperatorAlternates1(left.value + right.value)
    End Operator
    
    ' Missing: Public Shared Function Add(left As BadOperatorAlternates1, right As BadOperatorAlternates1) As BadOperatorAlternates1
    
End Class

' VIOLATION: Class has - operator but no Subtract method
Public Class BadOperatorAlternates2
    
    Private value As Double
    
    Public Sub New(val As Double)
        value = val
    End Sub
    
    Public Shared Operator -(left As BadOperatorAlternates2, right As BadOperatorAlternates2) As BadOperatorAlternates2
        Return New BadOperatorAlternates2(left.value - right.value)
    End Operator
    
    ' Missing: Public Shared Function Subtract(left As BadOperatorAlternates2, right As BadOperatorAlternates2) As BadOperatorAlternates2
    
End Class

' VIOLATION: Friend class has + operator but no Add method
Friend Class BadOperatorAlternates3
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Friend Shared Operator +(left As BadOperatorAlternates3, right As BadOperatorAlternates3) As BadOperatorAlternates3
        Return New BadOperatorAlternates3(left.value + right.value)
    End Operator
    
    ' Missing: Public Function Add or Public Shared Function Add
    
End Class

' GOOD: Class has + operator and Add method - should NOT be flagged
Public Class GoodOperatorAlternates1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Shared Operator +(left As GoodOperatorAlternates1, right As GoodOperatorAlternates1) As GoodOperatorAlternates1
        Return New GoodOperatorAlternates1(left.value + right.value)
    End Operator
    
    Public Shared Function Add(left As GoodOperatorAlternates1, right As GoodOperatorAlternates1) As GoodOperatorAlternates1
        Return left + right
    End Function
    
End Class

' GOOD: Class has - operator and Subtract method - should NOT be flagged
Public Class GoodOperatorAlternates2
    
    Private value As Double
    
    Public Sub New(val As Double)
        value = val
    End Sub
    
    Public Shared Operator -(left As GoodOperatorAlternates2, right As GoodOperatorAlternates2) As GoodOperatorAlternates2
        Return New GoodOperatorAlternates2(left.value - right.value)
    End Operator
    
    Public Function Subtract(right As GoodOperatorAlternates2) As GoodOperatorAlternates2
        Return Me - right
    End Function
    
End Class

' GOOD: Class without operator overloads - should NOT be flagged
Public Class NoOperators
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Function Add(other As NoOperators) As NoOperators
        Return New NoOperators(value + other.value)
    End Function
    
End Class
