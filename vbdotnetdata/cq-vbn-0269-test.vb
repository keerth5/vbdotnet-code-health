' Test file for cq-vbn-0269: Override equals on overloading operator equals
' Rule should detect: Classes with equality operator but no Equals override

' VIOLATION: Class has equality operator but doesn't override Equals
Public Class BadOperatorClass1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Shared Operator =(left As BadOperatorClass1, right As BadOperatorClass1) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.value = right.value
    End Operator
    
    Public Shared Operator <>(left As BadOperatorClass1, right As BadOperatorClass1) As Boolean
        Return Not (left = right)
    End Operator
    
    ' Missing: Public Overrides Function Equals(obj As Object) As Boolean
    
End Class

' VIOLATION: Friend class has equality operator but doesn't override Equals
Friend Class BadOperatorClass2
    
    Private name As String
    
    Public Sub New(n As String)
        name = n
    End Sub
    
    Public Shared Operator =(left As BadOperatorClass2, right As BadOperatorClass2) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.name = right.name
    End Operator
    
    Public Shared Operator <>(left As BadOperatorClass2, right As BadOperatorClass2) As Boolean
        Return Not (left = right)
    End Operator
    
    ' Missing: Public Overrides Function Equals(obj As Object) As Boolean
    
End Class

' GOOD: Class has equality operator and overrides Equals - should NOT be flagged
Public Class GoodOperatorClass1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Shared Operator =(left As GoodOperatorClass1, right As GoodOperatorClass1) As Boolean
        If left Is Nothing AndAlso right Is Nothing Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.value = right.value
    End Operator
    
    Public Shared Operator <>(left As GoodOperatorClass1, right As GoodOperatorClass1) As Boolean
        Return Not (left = right)
    End Operator
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is GoodOperatorClass1 Then
            Return Me = DirectCast(obj, GoodOperatorClass1)
        End If
        Return False
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return value.GetHashCode()
    End Function
    
End Class

' GOOD: Class without equality operator - should NOT be flagged
Public Class NoOperatorClass
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Function IsEqual(other As NoOperatorClass) As Boolean
        Return value = other.value
    End Function
    
End Class
