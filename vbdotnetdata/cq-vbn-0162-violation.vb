' Test file for cq-vbn-0162: Do not create tasks without passing a TaskScheduler
' A task creation or continuation operation uses a method overload that does not specify a TaskScheduler parameter

Imports System
Imports System.Threading.Tasks

Public Class TaskSchedulerTests
    
    ' Violation: Task.Run without TaskScheduler
    Public Sub CreateTaskWithoutScheduler()
        ' Violation: Task.Run without TaskScheduler parameter
        Dim task As Task = Task.Run(Sub() Console.WriteLine("Running without scheduler"))
        
        task.Wait()
    End Sub
    
    ' Violation: Task.Factory.StartNew without TaskScheduler
    Public Sub CreateFactoryTaskWithoutScheduler()
        ' Violation: Task.Factory.StartNew without TaskScheduler
        Dim task As Task = Task.Factory.StartNew(Sub() Console.WriteLine("Factory task without scheduler"))
        
        task.Wait()
    End Sub
    
    ' Violation: New Task without TaskScheduler
    Public Sub CreateNewTaskWithoutScheduler()
        ' Violation: New Task constructor without TaskScheduler
        Dim task As New Task(Sub() Console.WriteLine("New task without scheduler"))
        
        task.Start()
        task.Wait()
    End Sub
    
    ' Violation: Task.ContinueWith without TaskScheduler
    Public Sub CreateContinuationWithoutScheduler()
        Dim initialTask As Task = Task.Run(Sub() Console.WriteLine("Initial task"))
        
        ' Violation: ContinueWith without TaskScheduler
        Dim continuationTask As Task = Task.ContinueWith(initialTask, Sub(t) Console.WriteLine("Continuation without scheduler"))
        
        continuationTask.Wait()
    End Sub
    
    ' Violation: Task.Run with generic type without TaskScheduler
    Public Sub CreateGenericTaskWithoutScheduler()
        ' Violation: Task.Run<T> without TaskScheduler
        Dim task As Task(Of String) = Task.Run(Function() "Result without scheduler")
        
        Dim result As String = task.Result
        Console.WriteLine($"Result: {result}")
    End Sub
    
    ' Violation: Multiple task creations without TaskScheduler
    Public Sub CreateMultipleTasksWithoutScheduler()
        ' Violation: First Task.Run without scheduler
        Dim task1 As Task = Task.Run(Sub() Console.WriteLine("Task 1"))
        
        ' Violation: Second Task.Factory.StartNew without scheduler
        Dim task2 As Task = Task.Factory.StartNew(Sub() Console.WriteLine("Task 2"))
        
        ' Violation: Third New Task without scheduler
        Dim task3 As New Task(Sub() Console.WriteLine("Task 3"))
        task3.Start()
        
        Task.WaitAll(task1, task2, task3)
    End Sub
    
    ' Violation: Task creation in loop without TaskScheduler
    Public Sub CreateTasksInLoop()
        Dim tasks As New List(Of Task)
        
        For i As Integer = 0 To 4
            ' Violation: Task.Run in loop without TaskScheduler
            Dim task As Task = Task.Run(Sub() Console.WriteLine($"Task {i}"))
            tasks.Add(task)
        Next
        
        Task.WaitAll(tasks.ToArray())
    End Sub
    
    ' Good practice: Using TaskScheduler (should not be detected)
    Public Sub CreateTaskWithScheduler()
        ' Good: Task.Factory.StartNew with TaskScheduler
        Dim task As Task = Task.Factory.StartNew(
            Sub() Console.WriteLine("Task with scheduler"),
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskScheduler.Default)
        
        task.Wait()
    End Sub
    
    ' Good: Using current TaskScheduler
    Public Sub CreateTaskWithCurrentScheduler()
        ' Good: Using TaskScheduler.Current
        Dim task As Task = Task.Factory.StartNew(
            Sub() Console.WriteLine("Task with current scheduler"),
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskScheduler.Current)
        
        task.Wait()
    End Sub
    
    ' Violation: ContinueWith with options but no TaskScheduler
    Public Sub CreateContinuationWithOptionsButNoScheduler()
        Dim initialTask As Task = Task.Run(Sub() Console.WriteLine("Initial"))
        
        ' Violation: ContinueWith with TaskContinuationOptions but no TaskScheduler
        Dim continuation As Task = initialTask.ContinueWith(
            Sub(t) Console.WriteLine("Continuation"),
            TaskContinuationOptions.OnlyOnRanToCompletion)
        
        continuation.Wait()
    End Sub
    
    ' Violation: Task.Factory.StartNew with CancellationToken but no TaskScheduler
    Public Sub CreateFactoryTaskWithTokenButNoScheduler()
        Dim cts As New CancellationTokenSource()
        
        ' Violation: StartNew with CancellationToken but no TaskScheduler
        Dim task As Task = Task.Factory.StartNew(
            Sub() Console.WriteLine("Factory task with token"),
            cts.Token)
        
        task.Wait()
    End Sub
    
    ' Violation: Complex task creation without TaskScheduler
    Public Sub CreateComplexTaskWithoutScheduler()
        Dim data As String = "test data"
        
        ' Violation: Task.Run with complex operation, no TaskScheduler
        Dim task As Task(Of String) = Task.Run(Function()
                                                   Thread.Sleep(100)
                                                   Return data.ToUpper()
                                               End Function)
        
        Dim result As String = task.Result
        Console.WriteLine($"Complex result: {result}")
    End Sub
    
End Class

' Additional test cases
Public Module TaskCreationUtilities
    
    ' Violation: Utility method creating task without TaskScheduler
    Public Function CreateBackgroundTask(action As Action) As Task
        ' Violation: Task.Run without TaskScheduler in utility method
        Return Task.Run(action)
    End Function
    
    ' Violation: Generic task creation utility
    Public Function CreateBackgroundTask(Of T)(func As Func(Of T)) As Task(Of T)
        ' Violation: Task.Run<T> without TaskScheduler
        Return Task.Run(func)
    End Function
    
    ' Violation: Task continuation utility
    Public Function CreateContinuation(Of T)(task As Task(Of T), action As Action(Of T)) As Task
        ' Violation: ContinueWith without TaskScheduler
        Return task.ContinueWith(Sub(t) action(t.Result))
    End Function
    
    ' Good: Utility method with TaskScheduler
    Public Function CreateScheduledTask(action As Action, scheduler As TaskScheduler) As Task
        ' Good: Using provided TaskScheduler
        Return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, scheduler)
    End Function
    
End Module

' Test with different task creation patterns
Public Class AdvancedTaskCreation
    
    ' Violation: Task.Factory.StartNew with TaskCreationOptions but no TaskScheduler
    Public Sub CreateTaskWithOptionsButNoScheduler()
        ' Violation: StartNew with TaskCreationOptions but no TaskScheduler
        Dim task As Task = Task.Factory.StartNew(
            Sub() Console.WriteLine("Task with options"),
            TaskCreationOptions.LongRunning)
        
        task.Wait()
    End Sub
    
    ' Violation: Async task creation without TaskScheduler
    Public Async Function CreateAsyncTaskWithoutScheduler() As Task
        ' Violation: Task.Run in async method without TaskScheduler
        Await Task.Run(Sub() Console.WriteLine("Async task creation"))
        
        Console.WriteLine("Async task completed")
    End Function
    
    ' Violation: Conditional task creation without TaskScheduler
    Public Sub ConditionalTaskCreation(useFactory As Boolean)
        If useFactory Then
            ' Violation: Task.Factory.StartNew without TaskScheduler
            Dim factoryTask As Task = Task.Factory.StartNew(Sub() Console.WriteLine("Factory"))
            factoryTask.Wait()
        Else
            ' Violation: Task.Run without TaskScheduler
            Dim runTask As Task = Task.Run(Sub() Console.WriteLine("Run"))
            runTask.Wait()
        End If
    End Sub
    
    ' Violation: Task creation with exception handling
    Public Sub TaskCreationWithExceptionHandling()
        Try
            ' Violation: Task.Run without TaskScheduler
            Dim task As Task = Task.Run(Sub()
                                            Throw New InvalidOperationException("Test exception")
                                        End Sub)
            task.Wait()
        Catch ex As AggregateException
            Console.WriteLine($"Caught exception: {ex.InnerException.Message}")
        End Try
    End Sub
    
End Class
