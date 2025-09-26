' Test file for cq-vbn-0165: Use ValueTasks correctly
' ValueTasks returned from member invocations are intended to be directly awaited. Attempts to consume a ValueTask multiple times or to directly access one's result before it's known to be completed may result in an exception or corruption.

Imports System
Imports System.Threading.Tasks

Public Class ValueTaskTests
    
    ' Violation: Storing ValueTask and accessing multiple times
    Public Async Function MultipleValueTaskAccess() As Task
        ' Violation: Storing ValueTask in variable and accessing multiple times
        Dim valueTask As ValueTask = GetValueTaskAsync()
        
        ' First access
        Await valueTask
        
        ' Violation: Second access to same ValueTask (should not be done)
        Await valueTask
        
        Console.WriteLine("Multiple access completed")
    End Function
    
    ' Violation: Accessing ValueTask.Result before completion
    Public Sub AccessValueTaskResultDirectly()
        ' Violation: Getting ValueTask and accessing Result without awaiting
        Dim valueTask As ValueTask(Of String) = GetValueTaskWithResultAsync()
        
        ' Violation: Accessing Result property directly
        Dim result As String = valueTask.Result
        
        Console.WriteLine($"Direct result: {result}")
    End Sub
    
    ' Violation: Storing ValueTask(Of T) and accessing multiple times
    Public Async Function MultipleGenericValueTaskAccess() As Task
        ' Violation: Storing generic ValueTask and accessing multiple times
        Dim valueTask As ValueTask(Of Integer) = GetIntegerValueTaskAsync()
        
        ' First access
        Dim result1 As Integer = Await valueTask
        
        ' Violation: Second access to same ValueTask
        Dim result2 As Integer = Await valueTask
        
        Console.WriteLine($"Results: {result1}, {result2}")
    End Function
    
    ' Violation: Passing ValueTask around and accessing multiple times
    Public Async Function PassValueTaskAround() As Task
        Dim valueTask As ValueTask(Of String) = GetValueTaskWithResultAsync()
        
        ' Pass to helper method
        Await ProcessValueTask(valueTask)
        
        ' Violation: Accessing again after passing to another method
        Dim result As String = Await valueTask
        
        Console.WriteLine($"Passed around result: {result}")
    End Function
    
    Private Async Function ProcessValueTask(vt As ValueTask(Of String)) As Task
        Dim result As String = Await vt
        Console.WriteLine($"Processed: {result}")
    End Function
    
    ' Violation: ValueTask in loop with multiple accesses
    Public Async Function ValueTaskInLoop() As Task
        For i As Integer = 0 To 2
            Dim valueTask As ValueTask(Of Integer) = GetIntegerValueTaskAsync()
            
            ' First access in loop
            Dim result1 As Integer = Await valueTask
            
            ' Violation: Second access to same ValueTask in loop
            Dim result2 As Integer = Await valueTask
            
            Console.WriteLine($"Loop {i}: {result1}, {result2}")
        Next
    End Function
    
    ' Violation: Storing ValueTask in field and accessing multiple times
    Private _storedValueTask As ValueTask(Of String)
    
    Public Async Function StoreValueTaskInField() As Task
        ' Violation: Storing ValueTask in field
        _storedValueTask = GetValueTaskWithResultAsync()
        
        ' First access
        Dim result1 As String = Await _storedValueTask
        
        ' Violation: Second access from field
        Dim result2 As String = Await _storedValueTask
        
        Console.WriteLine($"Field results: {result1}, {result2}")
    End Function
    
    ' Violation: ValueTask in conditional with multiple paths
    Public Async Function ValueTaskInConditional(useFirst As Boolean) As Task
        Dim valueTask As ValueTask(Of String) = GetValueTaskWithResultAsync()
        
        If useFirst Then
            ' First access
            Dim result1 As String = Await valueTask
            Console.WriteLine($"First path: {result1}")
        End If
        
        ' Violation: Potential second access
        Dim result2 As String = Await valueTask
        Console.WriteLine($"Second path: {result2}")
    End Function
    
    ' Good practice: Direct await of ValueTask (should not be detected)
    Public Async Function DirectValueTaskAwait() As Task
        ' Good: Direct await without storing
        Await GetValueTaskAsync()
        
        Console.WriteLine("Direct await completed")
    End Function
    
    ' Good: Direct await with result
    Public Async Function DirectValueTaskAwaitWithResult() As Task(Of String)
        ' Good: Direct await with result
        Dim result As String = Await GetValueTaskWithResultAsync()
        
        Return result
    End Function
    
    ' Violation: ValueTask.IsCompleted access
    Public Async Function AccessValueTaskIsCompleted() As Task
        ' Violation: Storing ValueTask and checking IsCompleted
        Dim valueTask As ValueTask(Of Integer) = GetIntegerValueTaskAsync()
        
        If valueTask.IsCompleted Then
            ' Violation: Accessing Result when completed
            Dim result As Integer = valueTask.Result
            Console.WriteLine($"Completed result: {result}")
        Else
            Dim result As Integer = Await valueTask
            Console.WriteLine($"Awaited result: {result}")
        End If
    End Function
    
    ' Violation: ValueTask.IsCompletedSuccessfully access
    Public Async Function AccessValueTaskIsCompletedSuccessfully() As Task
        ' Violation: Storing ValueTask and checking IsCompletedSuccessfully
        Dim valueTask As ValueTask(Of String) = GetValueTaskWithResultAsync()
        
        If valueTask.IsCompletedSuccessfully Then
            ' Violation: Direct Result access
            Dim result As String = valueTask.Result
            Console.WriteLine($"Successfully completed: {result}")
        End If
    End Function
    
    ' Helper methods that return ValueTask
    Private Async Function GetValueTaskAsync() As ValueTask
        Await Task.Delay(100)
        Console.WriteLine("ValueTask operation completed")
    End Function
    
    Private Async Function GetValueTaskWithResultAsync() As ValueTask(Of String)
        Await Task.Delay(100)
        Return "ValueTask result"
    End Function
    
    Private Async Function GetIntegerValueTaskAsync() As ValueTask(Of Integer)
        Await Task.Delay(50)
        Return 42
    End Function
    
    ' Violation: ValueTask in exception handling
    Public Async Function ValueTaskInExceptionHandling() As Task
        Dim valueTask As ValueTask(Of String) = GetValueTaskWithResultAsync()
        
        Try
            ' First access
            Dim result1 As String = Await valueTask
            Console.WriteLine($"Try result: {result1}")
        Catch ex As Exception
            ' Violation: Accessing ValueTask again in catch block
            Try
                Dim result2 As String = Await valueTask
                Console.WriteLine($"Catch result: {result2}")
            Catch innerEx As Exception
                Console.WriteLine($"Inner exception: {innerEx.Message}")
            End Try
        End Try
    End Function
    
End Class

' Additional test cases
Public Module ValueTaskUtilities
    
    ' Violation: Utility method that stores and reuses ValueTask
    Public Async Function ProcessValueTaskMultipleTimes(Of T)(valueTask As ValueTask(Of T)) As Task(Of T)
        ' First access
        Dim result1 As T = Await valueTask
        
        ' Violation: Second access to the same ValueTask
        Dim result2 As T = Await valueTask
        
        ' Return one of the results (they should be the same, but this is incorrect usage)
        Return result1
    End Function
    
    ' Violation: Generic method with ValueTask reuse
    Public Async Function ReuseGenericValueTask(Of T)(factory As Func(Of ValueTask(Of T))) As Task(Of T)
        Dim valueTask As ValueTask(Of T) = factory()
        
        ' First access
        Dim result1 As T = Await valueTask
        
        ' Violation: Second access
        Dim result2 As T = Await valueTask
        
        Return result1
    End Function
    
    ' Good: Utility method that properly handles ValueTask
    Public Async Function ProcessValueTaskOnce(Of T)(valueTask As ValueTask(Of T)) As Task(Of T)
        ' Good: Single access to ValueTask
        Return Await valueTask
    End Function
    
End Module

' Test with advanced scenarios
Public Class AdvancedValueTaskScenarios
    
    ' Violation: ValueTask in async enumerable
    Public Async Function ValueTaskInAsyncEnumerable() As Task
        Dim valueTasks As ValueTask(Of String)() = {
            GetValueTaskWithResultAsync(),
            GetValueTaskWithResultAsync(),
            GetValueTaskWithResultAsync()
        }
        
        For Each vt In valueTasks
            ' First access
            Dim result1 As String = Await vt
            
            ' Violation: Second access in loop
            Dim result2 As String = Await vt
            
            Console.WriteLine($"Enumerable results: {result1}, {result2}")
        Next
    End Function
    
    ' Violation: ValueTask with ConfigureAwait
    Public Async Function ValueTaskWithConfigureAwait() As Task
        Dim valueTask As ValueTask(Of Integer) = GetIntegerValueTaskAsync()
        
        ' First access with ConfigureAwait
        Dim result1 As Integer = Await valueTask.ConfigureAwait(False)
        
        ' Violation: Second access
        Dim result2 As Integer = Await valueTask.ConfigureAwait(False)
        
        Console.WriteLine($"ConfigureAwait results: {result1}, {result2}")
    End Function
    
    ' Helper methods for advanced scenarios
    Private Async Function GetValueTaskWithResultAsync() As ValueTask(Of String)
        Await Task.Delay(50)
        Return $"Result at {DateTime.Now:HH:mm:ss.fff}"
    End Function
    
    Private Async Function GetIntegerValueTaskAsync() As ValueTask(Of Integer)
        Await Task.Delay(25)
        Return Random.Shared.Next(1, 100)
    End Function
    
    ' Violation: Property returning ValueTask that gets accessed multiple times
    Public ReadOnly Property ValueTaskProperty As ValueTask(Of String)
        Get
            Return GetValueTaskWithResultAsync()
        End Get
    End Property
    
    Public Async Function AccessPropertyMultipleTimes() As Task
        ' First access to property
        Dim result1 As String = Await ValueTaskProperty
        
        ' Violation: Second access to same property (returns new ValueTask, but pattern is problematic)
        Dim result2 As String = Await ValueTaskProperty
        
        Console.WriteLine($"Property results: {result1}, {result2}")
    End Function
    
End Class
