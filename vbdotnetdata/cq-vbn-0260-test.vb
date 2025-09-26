' Test file for cq-vbn-0260: Instantiate argument exceptions correctly
' Rule should detect: ArgumentException types with incorrect constructor usage

Public Class ArgumentExceptionTest
    
    ' VIOLATION: ArgumentException with no parameters
    Public Sub BadException1()
        Throw New ArgumentException()
    End Sub
    
    ' VIOLATION: ArgumentNullException with no parameters
    Public Sub BadException2()
        Throw New ArgumentNullException()
    End Sub
    
    ' VIOLATION: ArgumentOutOfRangeException with no parameters
    Public Sub BadException3()
        Throw New ArgumentOutOfRangeException()
    End Sub
    
    ' VIOLATION: ArgumentException with incorrect parameter order (message, paramName)
    Public Sub BadException4(paramName As String)
        If String.IsNullOrEmpty(paramName) Then
            Throw New ArgumentException("Parameter is invalid", "paramName")
        End If
    End Sub
    
    ' VIOLATION: ArgumentNullException with incorrect parameter order
    Public Sub BadException5(data As Object)
        If data Is Nothing Then
            Throw New ArgumentNullException("Data cannot be null", "data")
        End If
    End Sub
    
    ' VIOLATION: ArgumentOutOfRangeException with incorrect parameter order
    Public Sub BadException6(index As Integer)
        If index < 0 Then
            Throw New ArgumentOutOfRangeException("Index must be non-negative", "index")
        End If
    End Sub
    
    ' GOOD: ArgumentException with correct parameter order (paramName, message) - should NOT be flagged
    Public Sub GoodException1(paramName As String)
        If String.IsNullOrEmpty(paramName) Then
            Throw New ArgumentException("paramName", "Parameter is invalid")
        End If
    End Sub
    
    ' GOOD: ArgumentNullException with correct parameter - should NOT be flagged
    Public Sub GoodException2(data As Object)
        If data Is Nothing Then
            Throw New ArgumentNullException("data")
        End If
    End Sub
    
    ' GOOD: ArgumentOutOfRangeException with correct parameter - should NOT be flagged
    Public Sub GoodException3(index As Integer)
        If index < 0 Then
            Throw New ArgumentOutOfRangeException("index", "Index must be non-negative")
        End If
    End Sub
    
    ' GOOD: ArgumentException with just message - should NOT be flagged
    Public Sub GoodException4()
        Throw New ArgumentException("Invalid argument provided")
    End Sub
    
    ' GOOD: Other exception types - should NOT be flagged
    Public Sub GoodException5()
        Throw New InvalidOperationException("Operation not allowed")
    End Sub
    
End Class
