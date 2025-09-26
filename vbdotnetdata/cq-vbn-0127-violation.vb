' VB.NET test file for cq-vbn-0127: Do not use 'WaitAll' with a single task
' Rule: Using WaitAll with a single task may result in performance loss. Await or return the task instead.

Imports System
Imports System.Threading.Tasks

Public Class WaitAllSingleTaskExamples
    
    Public Sub TestWaitAllSingleTask()
        
        ' Violation: Using Task.WaitAll with a single task
        Task.WaitAll(GetDataAsync())
        
        ' Violation: Using Task.WaitAll with a single task in variable
        Dim task = ProcessDataAsync()
        Task.WaitAll(task)
        
        ' Violation: Using Task.WaitAll with a single task and timeout
        Task.WaitAll(CalculateAsync(), TimeSpan.FromSeconds(5))
        
        ' Violation: Using Task.WaitAll with a single task and cancellation token
        Dim cancellationToken As New Threading.CancellationToken()
        Task.WaitAll({GetNumberAsync()}, cancellationToken)
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskWithTimeout()
        
        ' Violation: Using Task.WaitAll with a single task and millisecond timeout
        Dim success = Task.WaitAll({CheckConditionAsync()}, 5000)
        
        If success Then
            Console.WriteLine("Task completed within timeout")
        Else
            Console.WriteLine("Task timed out")
        End If
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskInConditional()
        
        ' Violation: Using Task.WaitAll with a single task in conditional
        If Task.WaitAll({ProcessDataAsync()}, 1000) Then
            Console.WriteLine("Processing completed on time")
        End If
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskInTryCatch()
        
        Try
            ' Violation: Using Task.WaitAll with a single task in try-catch
            Task.WaitAll(RiskyOperationAsync())
        Catch ex As AggregateException
            Console.WriteLine($"Error: {ex.Message}")
        End Try
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskWithArray()
        
        ' Violation: Using Task.WaitAll with single task array
        Dim tasks() As Task = {GetDataAsync()}
        Task.WaitAll(tasks)
        
        ' Violation: Using Task.WaitAll with single task array and timeout
        Task.WaitAll(tasks, TimeSpan.FromMinutes(1))
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskGeneric()
        
        ' Violation: Using Task.WaitAll with a single generic task
        Task.WaitAll(GetStringAsync())
        
        ' Violation: Using Task.WaitAll with a single task returning value
        Task.WaitAll(CalculateAsync())
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskInLoop()
        
        For i = 1 To 3
            ' Violation: Using Task.WaitAll with a single task in loop
            Task.WaitAll(ProcessItemAsync(i))
        Next
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskWithCancellation()
        
        Dim cts As New Threading.CancellationTokenSource(TimeSpan.FromSeconds(10))
        
        ' Violation: Using Task.WaitAll with a single task and cancellation
        Try
            Task.WaitAll({LongRunningTaskAsync()}, cts.Token)
        Catch ex As OperationCanceledException
            Console.WriteLine("Operation was cancelled")
        End Try
        
    End Sub
    
    Public Function TestWaitAllSingleTaskWithReturn() As Boolean
        
        ' Violation: Using Task.WaitAll with a single task in return statement
        Return Task.WaitAll({CheckConditionAsync()}, 2000)
        
    End Function
    
    ' Helper methods for testing
    
    Private Async Function GetDataAsync() As Task(Of String)
        Await Task.Delay(100)
        Return "Sample data"
    End Function
    
    Private Async Function ProcessDataAsync() As Task
        Await Task.Delay(100)
        Console.WriteLine("Data processed")
    End Function
    
    Private Async Function CalculateAsync() As Task(Of Integer)
        Await Task.Delay(100)
        Return 42
    End Function
    
    Private Async Function GetNumberAsync() As Task(Of Integer)
        Await Task.Delay(100)
        Return 123
    End Function
    
    Private Async Function CheckConditionAsync() As Task(Of Boolean)
        Await Task.Delay(100)
        Return True
    End Function
    
    Private Async Function GetStringAsync() As Task(Of String)
        Await Task.Delay(100)
        Return "Hello World"
    End Function
    
    Private Async Function RiskyOperationAsync() As Task
        Await Task.Delay(100)
        ' Simulate potential exception
        If DateTime.Now.Millisecond Mod 2 = 0 Then
            Throw New InvalidOperationException("Random error")
        End If
    End Function
    
    Private Async Function ProcessItemAsync(item As Integer) As Task
        Await Task.Delay(100)
        Console.WriteLine($"Processed item {item}")
    End Function
    
    Private Async Function LongRunningTaskAsync() As Task
        Await Task.Delay(30000) ' 30 seconds
        Console.WriteLine("Long running task completed")
    End Function
    
End Class

' More violation examples

Public Class AdditionalWaitAllExamples
    
    Public Sub TestWaitAllSingleTaskWithMultipleCalls()
        
        ' Violation: Using Task.WaitAll with a single task - first call
        Task.WaitAll(Step1Async())
        
        ' Violation: Using Task.WaitAll with a single task - second call
        Task.WaitAll(Step2Async())
        
        ' Violation: Using Task.WaitAll with a single task - third call
        Task.WaitAll(Step3Async())
        
    End Sub
    
    Public Sub TestWaitAllSingleTaskInMethod()
        
        Dim dataTask = FetchDataAsync()
        
        ' Violation: Using Task.WaitAll with a single task variable
        Task.WaitAll(dataTask)
        
        Console.WriteLine("Data fetching completed")
    End Sub
    
    Private Async Function Step1Async() As Task
        Await Task.Delay(100)
        Console.WriteLine("Step 1 completed")
    End Function
    
    Private Async Function Step2Async() As Task
        Await Task.Delay(100)
        Console.WriteLine("Step 2 completed")
    End Function
    
    Private Async Function Step3Async() As Task
        Await Task.Delay(100)
        Console.WriteLine("Step 3 completed")
    End Function
    
    Private Async Function FetchDataAsync() As Task(Of String)
        Await Task.Delay(200)
        Return "Fetched data"
    End Function
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperTaskUsageExamples
    
    Public Sub TestProperUsage()
        
        ' Correct: Direct Wait on single task - should not be detected
        Dim task = GetDataAsync()
        task.Wait()
        
        ' Correct: Using Task.WaitAll with multiple tasks - should not be detected
        Dim task1 = GetDataAsync()
        Dim task2 = GetNumberAsync()
        Dim task3 = CheckConditionAsync()
        
        Task.WaitAll(task1, task2, task3)
        
        ' Correct: Using Task.WaitAll with array of multiple tasks - should not be detected
        Dim tasks() As Task = {ProcessDataAsync(), ProcessDataAsync(), ProcessDataAsync()}
        Task.WaitAll(tasks)
        
        ' Correct: Using Task.WaitAny with single task - should not be detected
        Task.WaitAny(GetDataAsync())
        
        ' Correct: Using Result property on single task - should not be detected
        Dim result = GetNumberAsync().Result
        Console.WriteLine($"Result: {result}")
        
    End Sub
    
    Private Async Function GetDataAsync() As Task(Of String)
        Await Task.Delay(100)
        Return "Sample data"
    End Function
    
    Private Async Function GetNumberAsync() As Task(Of Integer)
        Await Task.Delay(100)
        Return 42
    End Function
    
    Private Async Function CheckConditionAsync() As Task(Of Boolean)
        Await Task.Delay(100)
        Return True
    End Function
    
    Private Async Function ProcessDataAsync() As Task
        Await Task.Delay(100)
        Console.WriteLine("Data processed")
    End Function
    
End Class
