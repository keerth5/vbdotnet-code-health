' Test file for cq-vbn-0279: Test for NaN correctly
' Rule should detect: Direct comparisons with Single.NaN or Double.NaN instead of using IsNaN methods

Imports System

Public Class BadNaNTestingClass
    
    ' VIOLATION: Direct equality comparison with Double.NaN
    Public Function BadMethod1(value As Double) As Boolean
        Return value = Double.NaN
    End Function
    
    ' VIOLATION: Direct inequality comparison with Single.NaN
    Public Function BadMethod2(value As Single) As Boolean
        Return value <> Single.NaN
    End Function
    
    ' VIOLATION: Direct equality comparison with Single.NaN
    Public Sub BadMethod3()
        Dim result As Single = CalculateValue()
        If result = Single.NaN Then
            Console.WriteLine("Result is NaN")
        End If
    End Sub
    
    ' VIOLATION: Direct inequality comparison with Double.NaN
    Public Sub BadMethod4()
        Dim data As Double = GetData()
        If data <> Double.NaN Then
            Console.WriteLine("Data is not NaN")
        End If
    End Sub
    
    ' VIOLATION: Assignment and comparison with NaN
    Public Sub BadMethod5()
        Dim testValue As Double = Double.NaN
        Dim input As Double = 10.5
        If input = testValue Then
            Console.WriteLine("Input equals NaN")
        End If
    End Sub
    
    ' GOOD: Using Double.IsNaN method - should NOT be flagged
    Public Function GoodMethod1(value As Double) As Boolean
        Return Double.IsNaN(value)
    End Function
    
    ' GOOD: Using Single.IsNaN method - should NOT be flagged
    Public Function GoodMethod2(value As Single) As Boolean
        Return Single.IsNaN(value)
    End Function
    
    ' GOOD: Using IsNaN for checking - should NOT be flagged
    Public Sub GoodMethod3()
        Dim result As Single = CalculateValue()
        If Single.IsNaN(result) Then
            Console.WriteLine("Result is NaN")
        End If
    End Sub
    
    ' GOOD: Using IsNaN for inequality check - should NOT be flagged
    Public Sub GoodMethod4()
        Dim data As Double = GetData()
        If Not Double.IsNaN(data) Then
            Console.WriteLine("Data is not NaN")
        End If
    End Sub
    
    ' GOOD: Regular numeric comparisons - should NOT be flagged
    Public Function GoodMethod5(value As Double) As Boolean
        Return value = 0.0
    End Function
    
    Private Function CalculateValue() As Single
        Return 42.0F
    End Function
    
    Private Function GetData() As Double
        Return 3.14159
    End Function
    
End Class
