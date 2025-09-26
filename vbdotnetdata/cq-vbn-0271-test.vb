' Test file for cq-vbn-0271: Operators should have symmetrical overloads
' Rule should detect: Types with equality or inequality operator but missing the opposite operator

' VIOLATION: Has equality operator but missing inequality operator
Public Class BadSymmetryClass1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Shared Operator =(left As BadSymmetryClass1, right As BadSymmetryClass1) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.value = right.value
    End Operator
    
    ' Missing: Public Shared Operator <>(left As BadSymmetryClass1, right As BadSymmetryClass1) As Boolean
    
End Class

' VIOLATION: Has inequality operator but missing equality operator
Friend Class BadSymmetryClass2
    
    Private name As String
    
    Public Sub New(n As String)
        name = n
    End Sub
    
    Public Shared Operator <>(left As BadSymmetryClass2, right As BadSymmetryClass2) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then Return False
        If left Is Nothing OrElse right Is Nothing Then Return True
        Return left.name <> right.name
    End Operator
    
    ' Missing: Public Shared Operator =(left As BadSymmetryClass2, right As BadSymmetryClass2) As Boolean
    
End Class

' GOOD: Has both equality and inequality operators - should NOT be flagged
Public Class GoodSymmetryClass1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Shared Operator =(left As GoodSymmetryClass1, right As GoodSymmetryClass1) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.value = right.value
    End Operator
    
    Public Shared Operator <>(left As GoodSymmetryClass1, right As GoodSymmetryClass1) As Boolean
        Return Not (left = right)
    End Operator
    
End Class

' GOOD: No operator overloads - should NOT be flagged
Public Class NoOperatorsClass
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Function IsEqual(other As NoOperatorsClass) As Boolean
        Return value = other.value
    End Function
    
End Class
