' Test file for cq-vbn-0257: Rethrow to preserve stack details
' Rule should detect: Exception rethrowing that loses stack trace

Public Class ExceptionRethrowTest
    
    ' VIOLATION: Rethrowing exception by name (loses stack trace)
    Public Sub BadRethrow1()
        Try
            Dim result As Integer = 10 / 0
        Catch ex As DivideByZeroException
            Console.WriteLine("Error occurred")
            Throw ex ' This loses the stack trace
        End Try
    End Sub
    
    ' VIOLATION: Rethrowing different exception type by name
    Public Sub BadRethrow2()
        Try
            Dim obj As Object = Nothing
            obj.ToString()
        Catch nullEx As NullReferenceException
            Console.WriteLine("Null reference error")
            Throw nullEx ' This loses the stack trace
        End Try
    End Sub
    
    ' VIOLATION: Rethrowing in nested catch
    Public Sub BadRethrow3()
        Try
            Try
                Dim result As Integer = 10 / 0
            Catch inner As Exception
                Throw inner ' This loses the stack trace
            End Try
        Catch outer As Exception
            Console.WriteLine("Outer catch")
        End Try
    End Sub
    
    ' GOOD: Proper rethrow without specifying exception - should NOT be flagged
    Public Sub GoodRethrow1()
        Try
            Dim result As Integer = 10 / 0
        Catch ex As DivideByZeroException
            Console.WriteLine("Error occurred")
            Throw ' This preserves the stack trace
        End Try
    End Sub
    
    ' GOOD: Throwing new exception - should NOT be flagged
    Public Sub GoodRethrow2()
        Try
            Dim obj As Object = Nothing
            obj.ToString()
        Catch ex As NullReferenceException
            Throw New InvalidOperationException("Operation failed", ex)
        End Try
    End Sub
    
    ' GOOD: Normal exception handling without rethrow - should NOT be flagged
    Public Sub GoodExceptionHandling()
        Try
            Dim result As Integer = 10 / 0
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub
    
End Class
