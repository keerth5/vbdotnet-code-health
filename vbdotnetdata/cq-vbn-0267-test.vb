' Test file for cq-vbn-0267: Override GetHashCode on overriding Equals
' Rule should detect: Classes that override Equals but not GetHashCode

' VIOLATION: Class overrides Equals but not GetHashCode
Public Class BadEqualsClass1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is BadEqualsClass1 Then
            Return value = DirectCast(obj, BadEqualsClass1).value
        End If
        Return False
    End Function
    
    ' Missing: Public Overrides Function GetHashCode() As Integer
    
End Class

' VIOLATION: Friend class overrides Equals but not GetHashCode
Friend Class BadEqualsClass2
    
    Private name As String
    
    Public Sub New(n As String)
        name = n
    End Sub
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is BadEqualsClass2 Then
            Return name = DirectCast(obj, BadEqualsClass2).name
        End If
        Return False
    End Function
    
    ' Missing: Public Overrides Function GetHashCode() As Integer
    
End Class

' GOOD: Class overrides both Equals and GetHashCode - should NOT be flagged
Public Class GoodEqualsClass1
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is GoodEqualsClass1 Then
            Return value = DirectCast(obj, GoodEqualsClass1).value
        End If
        Return False
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return value.GetHashCode()
    End Function
    
End Class

' GOOD: Class doesn't override Equals - should NOT be flagged
Public Class NoEqualsOverride
    
    Private value As Integer
    
    Public Sub New(val As Integer)
        value = val
    End Sub
    
    Public Function Compare(other As NoEqualsOverride) As Boolean
        Return value = other.value
    End Function
    
End Class
