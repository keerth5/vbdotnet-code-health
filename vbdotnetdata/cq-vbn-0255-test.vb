' Test file for cq-vbn-0255: Review unused parameters
' Rule should detect: Method parameters that are not used in method body

Public Class UnusedParameterTest
    
    ' VIOLATION: Parameter 'unusedParam' is not used in method body
    Public Function BadMethod1(usedParam As String, unusedParam As Integer) As String
        Return usedParam.ToUpper()
    End Function
    
    ' VIOLATION: Parameter 'unusedData' is not used in method body
    Public Sub BadMethod2(name As String, unusedData As Byte())
        Console.WriteLine("Hello " & name)
    End Sub
    
    ' VIOLATION: Parameter 'unusedConfig' is not used in method body
    Private Function BadMethod3(input As String, unusedConfig As Object) As Boolean
        Return input.Length > 0
    End Function
    
    ' VIOLATION: Multiple unused parameters
    Friend Sub BadMethod4(used As String, unused1 As Integer, unused2 As Double)
        Console.WriteLine(used)
    End Sub
    
    ' GOOD: All parameters are used - should NOT be flagged
    Public Function GoodMethod1(param1 As String, param2 As Integer) As String
        Return param1 & param2.ToString()
    End Function
    
    ' GOOD: Single parameter used - should NOT be flagged
    Public Sub GoodMethod2(message As String)
        Console.WriteLine(message)
    End Sub
    
    ' GOOD: No parameters - should NOT be flagged
    Public Function GoodMethod3() As String
        Return "No parameters"
    End Function
    
    ' GOOD: Parameter used in conditional - should NOT be flagged
    Public Function GoodMethod4(value As Integer, threshold As Integer) As Boolean
        If value > threshold Then
            Return True
        End If
        Return False
    End Function
    
End Class
