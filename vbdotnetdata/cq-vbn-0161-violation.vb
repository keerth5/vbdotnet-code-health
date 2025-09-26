' Test file for cq-vbn-0161: Do not directly await a Task
' An asynchronous method awaits a Task directly

Imports System
Imports System.Threading.Tasks

Public Class TaskAwaitingTests
    
    ' Violation: Directly awaiting Task.Run
    Public Async Function DirectTaskRunAwait() As Task
        ' Violation: Await Task.Run directly
        Await Task.Run(Sub() Console.WriteLine("Running task"))
        
        Console.WriteLine("Task completed")
    End Function
    
    ' Violation: Directly awaiting Task.Delay
    Public Async Function DirectTaskDelayAwait() As Task
        ' Violation: Await Task.Delay directly
        Await Task.Delay(1000)
        
        Console.WriteLine("Delay completed")
    End Function
    
    ' Violation: Directly awaiting Task.FromResult
    Public Async Function DirectTaskFromResultAwait() As Task(Of String)
        ' Violation: Await Task.FromResult directly
        Dim result As String = Await Task.FromResult("Hello World")
        
        Return result
    End Function
    
    ' Violation: Directly awaiting Task.Factory.StartNew
    Public Async Function DirectTaskFactoryAwait() As Task
        ' Violation: Await Task.Factory.StartNew directly
        Await Task.Factory.StartNew(Sub() Console.WriteLine("Factory task"))
        
        Console.WriteLine("Factory task completed")
    End Function
    
    ' Violation: Directly awaiting Task.ContinueWith result
    Public Async Function DirectTaskContinueWithAwait() As Task
        Dim initialTask As Task = Task.Run(Sub() Console.WriteLine("Initial task"))
        
        ' Violation: Await Task.ContinueWith directly
        Await Task.ContinueWith(initialTask, Sub(t) Console.WriteLine("Continuation"))
        
        Console.WriteLine("Continuation completed")
    End Function
    
    ' Violation: Multiple direct Task awaits
    Public Async Function MultipleDirect TaskAwaits() As Task
        ' Violation: First direct await
        Await Task.Run(Sub() Console.WriteLine("First task"))
        
        ' Violation: Second direct await
        Await Task.Delay(500)
        
        ' Violation: Third direct await
        Await Task.FromResult(42)
        
        Console.WriteLine("All tasks completed")
    End Function
    
    ' Violation: Await Task in loop
    Public Async Function AwaitTaskInLoop() As Task
        For i As Integer = 0 To 4
            ' Violation: Await Task.Delay in loop
            Await Task.Delay(100)
            Console.WriteLine($"Iteration {i}")
        Next
    End Function
    
    ' Violation: Conditional Task await
    Public Async Function ConditionalTaskAwait(useDelay As Boolean) As Task
        If useDelay Then
            ' Violation: Await Task.Delay in conditional
            Await Task.Delay(1000)
        Else
            ' Violation: Await Task.Run in conditional
            Await Task.Run(Sub() Console.WriteLine("No delay"))
        End If
    End Function
    
    ' Good practice: Awaiting methods that return Task (should not be detected)
    Public Async Function GoodAsyncMethod() As Task
        ' Good: Awaiting a method that returns Task
        Await DoSomethingAsync()
        
        Console.WriteLine("Good async method completed")
    End Function
    
    Private Async Function DoSomethingAsync() As Task
        Await Task.Delay(100) ' This would be a violation but in a different method
    End Function
    
    ' Good: Using ConfigureAwait
    Public Async Function GoodConfigureAwait() As Task
        ' Good: Using ConfigureAwait (though still awaiting Task directly)
        Await Task.Delay(1000).ConfigureAwait(False)
    End Function
    
    ' Violation: Awaiting Task.WhenAll
    Public Async Function AwaitTaskWhenAll() As Task
        Dim task1 As Task = Task.Run(Sub() Console.WriteLine("Task 1"))
        Dim task2 As Task = Task.Run(Sub() Console.WriteLine("Task 2"))
        
        ' Violation: Await Task.WhenAll directly
        Await Task.WhenAll(task1, task2)
        
        Console.WriteLine("All tasks completed")
    End Function
    
    ' Violation: Awaiting Task.WhenAny
    Public Async Function AwaitTaskWhenAny() As Task
        Dim task1 As Task = Task.Delay(1000)
        Dim task2 As Task = Task.Delay(2000)
        
        ' Violation: Await Task.WhenAny directly
        Await Task.WhenAny(task1, task2)
        
        Console.WriteLine("First task completed")
    End Function
    
    ' Violation: Task await with generic type
    Public Async Function AwaitGenericTask() As Task(Of Integer)
        ' Violation: Await Task.FromResult with generic type
        Dim result As Integer = Await Task.FromResult(42)
        
        Return result
    End Function
    
    ' Violation: Complex Task await
    Public Async Function ComplexTaskAwait() As Task
        Dim data As String = "test data"
        
        ' Violation: Await Task.Run with complex lambda
        Dim result As String = Await Task.Run(Function() data.ToUpper())
        
        Console.WriteLine($"Result: {result}")
    End Function
    
End Class

' Additional test cases
Public Module TaskUtilities
    
    ' Violation: Module method with Task await
    Public Async Function ProcessDataAsync() As Task
        ' Violation: Await Task.Run in module method
        Await Task.Run(Sub() Console.WriteLine("Processing data"))
        
        Console.WriteLine("Data processed")
    End Function
    
    ' Violation: Multiple Task operations
    Public Async Function ComplexTaskOperations() As Task(Of String)
        ' Violation: First Task await
        Await Task.Delay(500)
        
        ' Violation: Second Task await
        Dim result As String = Await Task.FromResult("Initial")
        
        ' Violation: Third Task await
        Await Task.Run(Sub() Console.WriteLine("Processing"))
        
        Return result
    End Function
    
End Module

' Test with different Task patterns
Public Class AdvancedTaskPatterns
    
    ' Violation: Task.Yield await
    Public Async Function AwaitTaskYield() As Task
        ' Violation: Await Task.Yield directly
        Await Task.Yield()
        
        Console.WriteLine("Yielded")
    End Function
    
    ' Violation: Task with cancellation
    Public Async Function AwaitTaskWithCancellation() As Task
        Dim cts As New Threading.CancellationTokenSource()
        
        ' Violation: Await Task.Delay with cancellation token
        Await Task.Delay(1000, cts.Token)
        
        Console.WriteLine("Task with cancellation completed")
    End Function
    
    ' Violation: Task.CompletedTask await
    Public Async Function AwaitCompletedTask() As Task
        ' Violation: Await Task.CompletedTask
        Await Task.CompletedTask
        
        Console.WriteLine("Completed task awaited")
    End Function
    
End Class
