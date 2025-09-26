' Test file for cq-vbn-0274: Overload operator equals on overriding ValueType.Equals
' Rule should detect: Value types that override Equals but don't implement equality operator

' VIOLATION: Structure overrides Equals but no equality operator
Public Structure BadValueType1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is BadValueType1 Then
            Dim other As BadValueType1 = DirectCast(obj, BadValueType1)
            Return value = other.value
        End If
        Return False
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return value.GetHashCode()
    End Function
    
    ' Missing: Public Shared Operator =(left As BadValueType1, right As BadValueType1) As Boolean
    
End Structure

' VIOLATION: Friend structure overrides Equals but no equality operator
Friend Structure BadValueType2
    
    Private name As String
    
    Public Sub New(n As String)
        name = n
    End Sub
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is BadValueType2 Then
            Dim other As BadValueType2 = DirectCast(obj, BadValueType2)
            Return name = other.name
        End If
        Return False
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return If(name?.GetHashCode(), 0)
    End Function
    
    ' Missing: Public Shared Operator =(left As BadValueType2, right As BadValueType2) As Boolean
    
End Structure

' GOOD: Structure overrides Equals and implements equality operator - should NOT be flagged
Public Structure GoodValueType1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is GoodValueType1 Then
            Dim other As GoodValueType1 = DirectCast(obj, GoodValueType1)
            Return value = other.value
        End If
        Return False
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return value.GetHashCode()
    End Function
    
    Public Shared Operator =(left As GoodValueType1, right As GoodValueType1) As Boolean
        Return left.Equals(right)
    End Operator
    
    Public Shared Operator <>(left As GoodValueType1, right As GoodValueType1) As Boolean
        Return Not (left = right)
    End Operator
    
End Structure

' GOOD: Structure without Equals override - should NOT be flagged
Public Structure SimpleValueType
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Function GetValue() As Integer
        Return value
    End Function
    
End Structure

' GOOD: Class (not structure) - should NOT be flagged
Public Class ReferenceType
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is ReferenceType Then
            Dim other As ReferenceType = DirectCast(obj, ReferenceType)
            Return value = other.value
        End If
        Return False
    End Function
    
End Class
