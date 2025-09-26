' VB.NET test file for cq-vbn-0126: Do not use 'WhenAll' with a single task
' Rule: Using WhenAll with a single task may result in performance loss. Await or return the task instead.

Imports System
Imports System.Threading.Tasks

Public Class WhenAllSingleTaskExamples
    
    Public Async Function TestWhenAllSingleTask() As Task
        
        ' Violation: Using Task.WhenAll with a single task
        Dim result = Await Task.WhenAll(GetDataAsync())
        
        ' Violation: Using Task.WhenAll with a single task in variable assignment
        Dim whenAllTask = Task.WhenAll(ProcessDataAsync())
        Await whenAllTask
        
        ' Violation: Using Task.WhenAll with a single task and accessing result
        Dim results = Await Task.WhenAll(CalculateAsync())
        Console.WriteLine($"Result: {results(0)}")
        
    End Function
    
    Public Async Function TestWhenAllSingleTaskWithReturn() As Task(Of Integer)
        
        ' Violation: Using Task.WhenAll with a single task in return statement
        Dim results = Await Task.WhenAll(GetNumberAsync())
        Return results(0)
        
    End Function
    
    Public Async Sub TestWhenAllSingleTaskInConditional()
        
        ' Violation: Using Task.WhenAll with a single task in conditional
        Dim results = Await Task.WhenAll(CheckConditionAsync())
        If results(0) Then
            Console.WriteLine("Condition is true")
        End If
        
    End Sub
    
    Public Async Function TestWhenAllSingleTaskWithTimeout() As Task
        
        ' Violation: Using Task.WhenAll with a single task and timeout
        Dim timeoutTask = Task.Delay(5000)
        Dim dataTask = GetDataAsync()
        
        Dim completedTask = Await Task.WhenAny(Task.WhenAll(dataTask), timeoutTask)
        
        If completedTask Is timeoutTask Then
            Console.WriteLine("Operation timed out")
        End If
        
    End Function
    
    Public Async Function TestWhenAllSingleTaskWithContinuation() As Task
        
        ' Violation: Using Task.WhenAll with a single task and continuation
        Await Task.WhenAll(ProcessDataAsync()).ContinueWith(Sub(t) Console.WriteLine("Processing completed"))
        
    End Function
    
    Public Async Function TestWhenAllSingleTaskGeneric() As Task
        
        ' Violation: Using Task.WhenAll with a single generic task
        Dim results = Await Task.WhenAll(GetStringAsync())
        Console.WriteLine($"String result: {results(0)}")
        
    End Function
    
    Public Async Function TestWhenAllSingleTaskInTryCatch() As Task
        
        Try
            ' Violation: Using Task.WhenAll with a single task in try-catch
            Await Task.WhenAll(RiskyOperationAsync())
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
        
    End Function
    
    Public Async Function TestWhenAllSingleTaskWithConfigureAwait() As Task
        
        ' Violation: Using Task.WhenAll with a single task and ConfigureAwait
        Await Task.WhenAll(GetDataAsync()).ConfigureAwait(False)
        
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
    
End Class

' More violation examples

Public Class AdditionalWhenAllExamples
    
    Public Async Function TestWhenAllSingleTaskInLoop() As Task
        
        For i = 1 To 3
            ' Violation: Using Task.WhenAll with a single task in loop
            Await Task.WhenAll(ProcessItemAsync(i))
        Next
        
    End Function
    
    Public Async Function TestWhenAllSingleTaskWithMultipleAwaits() As Task
        
        ' Violation: Using Task.WhenAll with a single task - first call
        Await Task.WhenAll(Step1Async())
        
        ' Violation: Using Task.WhenAll with a single task - second call
        Await Task.WhenAll(Step2Async())
        
    End Function
    
    Private Async Function ProcessItemAsync(item As Integer) As Task
        Await Task.Delay(100)
        Console.WriteLine($"Processed item {item}")
    End Function
    
    Private Async Function Step1Async() As Task
        Await Task.Delay(100)
        Console.WriteLine("Step 1 completed")
    End Function
    
    Private Async Function Step2Async() As Task
        Await Task.Delay(100)
        Console.WriteLine("Step 2 completed")
    End Function
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperTaskUsageExamples
    
    Public Async Function TestProperUsage() As Task
        
        ' Correct: Direct await of single task - should not be detected
        Dim result = Await GetDataAsync()
        Console.WriteLine($"Result: {result}")
        
        ' Correct: Using Task.WhenAll with multiple tasks - should not be detected
        Dim task1 = GetDataAsync()
        Dim task2 = GetNumberAsync()
        Dim task3 = CheckConditionAsync()
        
        Await Task.WhenAll(task1, task2, task3)
        
        ' Correct: Using Task.WhenAll with array of multiple tasks - should not be detected
        Dim tasks() As Task = {ProcessDataAsync(), ProcessDataAsync(), ProcessDataAsync()}
        Await Task.WhenAll(tasks)
        
        ' Correct: Using Task.WhenAny with single task - should not be detected
        Await Task.WhenAny(GetDataAsync())
        
    End Function
    
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
