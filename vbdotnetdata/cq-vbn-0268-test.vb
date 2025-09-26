' Test file for cq-vbn-0268: Do not raise exceptions in exception clauses
' Rule should detect: Throwing exceptions in Finally or Catch When clauses

Imports System

Public Class ExceptionInClausesTest
    
    ' VIOLATION: Throwing exception in Finally block
    Public Sub BadMethod1()
        Try
            Dim result As Integer = 10 / 0
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        Finally
            Throw New InvalidOperationException("Error in finally")
        End Try
    End Sub
    
    ' VIOLATION: Throwing exception in Finally block (different exception)
    Public Sub BadMethod2()
        Try
            Dim obj As Object = Nothing
            obj.ToString()
        Finally
            Throw New ArgumentException("Cleanup failed")
        End Try
    End Sub
    
    ' VIOLATION: Throwing exception in Catch When filter
    Public Sub BadMethod3()
        Try
            Dim result As Integer = 10 / 0
        Catch ex As Exception When Throw New NotSupportedException("Filter error")
            Console.WriteLine("Should not reach here")
        End Try
    End Sub
    
    ' VIOLATION: Another case of exception in Catch When
    Public Sub BadMethod4()
        Try
            ProcessData()
        Catch ex As ArgumentException When Throw New InvalidOperationException("Filter failed")
            HandleError()
        End Try
    End Sub
    
    ' GOOD: Normal exception handling without throwing in Finally - should NOT be flagged
    Public Sub GoodMethod1()
        Try
            Dim result As Integer = 10 / 0
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        Finally
            Console.WriteLine("Cleanup completed")
        End Try
    End Sub
    
    ' GOOD: Rethrowing in Catch block (not in Finally) - should NOT be flagged
    Public Sub GoodMethod2()
        Try
            ProcessData()
        Catch ex As Exception
            Console.WriteLine("Error occurred")
            Throw ' Rethrow is OK in catch block
        Finally
            CleanupResources()
        End Try
    End Sub
    
    ' GOOD: Throwing new exception in Catch block (not in Finally) - should NOT be flagged
    Public Sub GoodMethod3()
        Try
            ProcessData()
        Catch ex As ArgumentException
            Throw New InvalidOperationException("Operation failed", ex)
        End Try
    End Sub
    
    Private Sub ProcessData()
        ' Some processing
    End Sub
    
    Private Sub HandleError()
        ' Error handling
    End Sub
    
    Private Sub CleanupResources()
        ' Cleanup code
    End Sub
    
End Class
