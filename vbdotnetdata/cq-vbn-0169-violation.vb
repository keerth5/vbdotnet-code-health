' Test file for cq-vbn-0169: Forward the CancellationToken parameter to methods that take one
' Forward the CancellationToken parameter to methods that take one to ensure the operation cancellation notifications gets properly propagated, or pass in CancellationToken.None explicitly to indicate intentionally not propagating the token

Imports System
Imports System.Threading
Imports System.Threading.Tasks

Public Class CancellationTokenForwardingTests
    
    ' Violation: Method has CancellationToken but doesn't forward it
    Public Async Function ProcessDataAsync(cancellationToken As CancellationToken) As Task
        ' Violation: Calling async method without forwarding CancellationToken
        Await Task.Delay(1000)
        
        Console.WriteLine("Data processed without token forwarding")
    End Function
    
    ' Violation: Method with CancellationToken calling multiple async methods without forwarding
    Public Async Function ProcessMultipleOperationsAsync(token As CancellationToken) As Task
        ' Violation: First async call without token
        Await Task.Run(Sub() Console.WriteLine("First operation"))
        
        ' Violation: Second async call without token
        Await Task.Delay(500)
        
        ' Violation: Third async call without token
        Await File.WriteAllTextAsync("test.txt", "content")
        
        Console.WriteLine("Multiple operations completed")
    End Function
    
    ' Violation: Generic method with CancellationToken not forwarding
    Public Async Function ProcessGenericDataAsync(Of T)(data As T, cancellationToken As CancellationToken) As Task(Of T)
        ' Violation: Task.Run without CancellationToken
        Dim result As T = Await Task.Run(Function() ProcessItem(data))
        
        Return result
    End Function
    
    Private Function ProcessItem(Of T)(item As T) As T
        Thread.Sleep(100)
        Return item
    End Function
    
    ' Violation: Method calling HttpClient methods without token forwarding
    Public Async Function FetchDataAsync(url As String, cancellationToken As CancellationToken) As Task(Of String)
        Using client As New Http.HttpClient()
            ' Violation: GetStringAsync without CancellationToken
            Dim content As String = Await client.GetStringAsync(url)
            
            Return content
        End Using
    End Function
    
    ' Violation: File operations without token forwarding
    Public Async Function ProcessFileAsync(filePath As String, token As CancellationToken) As Task
        ' Violation: ReadAllTextAsync without CancellationToken
        Dim content As String = Await IO.File.ReadAllTextAsync(filePath)
        
        ' Process content
        Dim processedContent As String = content.ToUpper()
        
        ' Violation: WriteAllTextAsync without CancellationToken
        Await IO.File.WriteAllTextAsync(filePath & ".processed", processedContent)
        
        Console.WriteLine("File processed")
    End Function
    
    ' Good practice: Properly forwarding CancellationToken (should not be detected)
    Public Async Function ProperTokenForwardingAsync(cancellationToken As CancellationToken) As Task
        ' Good: Forwarding CancellationToken
        Await Task.Delay(1000, cancellationToken)
        
        Console.WriteLine("Proper token forwarding")
    End Function
    
    ' Good: Using CancellationToken.None explicitly
    Public Async Function ExplicitNoneTokenAsync(cancellationToken As CancellationToken) As Task
        ' Good: Explicitly using CancellationToken.None
        Await Task.Delay(1000, CancellationToken.None)
        
        Console.WriteLine("Explicit none token")
    End Function
    
    ' Violation: Method with CancellationToken in loop not forwarding
    Public Async Function ProcessItemsInLoopAsync(items As String(), token As CancellationToken) As Task
        For Each item In items
            ' Violation: Task.Run in loop without CancellationToken
            Await Task.Run(Sub() Console.WriteLine($"Processing {item}"))
            
            ' Violation: Task.Delay in loop without CancellationToken
            Await Task.Delay(100)
        Next
    End Function
    
    ' Violation: Conditional async calls without token forwarding
    Public Async Function ConditionalProcessingAsync(useDelay As Boolean, cancellationToken As CancellationToken) As Task
        If useDelay Then
            ' Violation: Task.Delay without CancellationToken in conditional
            Await Task.Delay(2000)
        Else
            ' Violation: Task.Run without CancellationToken in conditional
            Await Task.Run(Sub() Console.WriteLine("No delay"))
        End If
    End Function
    
    ' Violation: Exception handling with async calls not forwarding token
    Public Async Function ProcessWithExceptionHandlingAsync(cancellationToken As CancellationToken) As Task
        Try
            ' Violation: Task.Delay without CancellationToken in try block
            Await Task.Delay(1000)
            
            ' Simulate potential exception
            Throw New InvalidOperationException("Test exception")
        Catch ex As Exception
            Console.WriteLine($"Exception: {ex.Message}")
            
            ' Violation: Task.Delay without CancellationToken in catch block
            Await Task.Delay(500)
        End Try
    End Function
    
    ' Violation: Method calling custom async methods without forwarding token
    Public Async Function CallCustomMethodsAsync(token As CancellationToken) As Task
        ' Violation: Calling custom async method without forwarding token
        Await CustomAsyncOperation()
        
        ' Violation: Another custom async call without token
        Await AnotherCustomAsyncOperation()
        
        Console.WriteLine("Custom methods called")
    End Function
    
    Private Async Function CustomAsyncOperation() As Task
        Await Task.Delay(200)
    End Function
    
    Private Async Function AnotherCustomAsyncOperation() As Task
        Await Task.Delay(300)
    End Function
    
End Class

' Additional test cases
Public Module CancellationTokenUtilities
    
    ' Violation: Module method with CancellationToken not forwarding
    Public Async Function UtilityProcessingAsync(data As String, cancellationToken As CancellationToken) As Task(Of String)
        ' Violation: Task.Run without CancellationToken in utility method
        Dim result As String = Await Task.Run(Function() data.ToUpper())
        
        Return result
    End Function
    
    ' Violation: Generic utility method not forwarding token
    Public Async Function ProcessCollectionAsync(Of T)(items As IEnumerable(Of T), token As CancellationToken) As Task(Of List(Of T))
        Dim results As New List(Of T)
        
        For Each item In items
            ' Violation: Task.Run without CancellationToken
            Await Task.Run(Sub() results.Add(item))
        Next
        
        Return results
    End Function
    
    ' Good: Utility method properly forwarding token
    Public Async Function ProperUtilityAsync(delay As Integer, cancellationToken As CancellationToken) As Task
        ' Good: Forwarding CancellationToken
        Await Task.Delay(delay, cancellationToken)
    End Function
    
End Module

' Test with different async patterns
Public Class AdvancedAsyncPatterns
    
    ' Violation: Task.WhenAll without forwarding tokens
    Public Async Function ProcessMultipleTasksAsync(cancellationToken As CancellationToken) As Task
        ' Violation: Task.Run calls without CancellationToken
        Dim task1 As Task = Task.Run(Sub() Console.WriteLine("Task 1"))
        Dim task2 As Task = Task.Run(Sub() Console.WriteLine("Task 2"))
        Dim task3 As Task = Task.Run(Sub() Console.WriteLine("Task 3"))
        
        ' Wait for all tasks
        Await Task.WhenAll(task1, task2, task3)
    End Function
    
    ' Violation: Task.WhenAny without forwarding tokens
    Public Async Function ProcessFirstCompletedAsync(token As CancellationToken) As Task
        ' Violation: Task.Delay calls without CancellationToken
        Dim task1 As Task = Task.Delay(1000)
        Dim task2 As Task = Task.Delay(2000)
        
        ' Wait for first to complete
        Await Task.WhenAny(task1, task2)
    End Function
    
    ' Violation: Parallel.ForEachAsync without forwarding token
    Public Async Function ProcessInParallelAsync(items As String(), cancellationToken As CancellationToken) As Task
        ' Violation: Parallel.ForEachAsync without CancellationToken
        Await Parallel.ForEachAsync(items, Async Function(item, ct)
                                                ' Even though ct is available here, the outer call doesn't forward the token
                                                Await Task.Delay(100)
                                                Console.WriteLine($"Processed {item}")
                                            End Function)
    End Function
    
    ' Violation: ConfigureAwait with async calls not forwarding token
    Public Async Function ProcessWithConfigureAwaitAsync(cancellationToken As CancellationToken) As Task
        ' Violation: Task.Delay without CancellationToken, even with ConfigureAwait
        Await Task.Delay(1000).ConfigureAwait(False)
        
        Console.WriteLine("ConfigureAwait processing completed")
    End Function
    
    ' Violation: Method with multiple CancellationToken parameters
    Public Async Function ProcessWithMultipleTokensAsync(primaryToken As CancellationToken, secondaryToken As CancellationToken) As Task
        ' Violation: Using neither token parameter
        Await Task.Delay(1000)
        
        ' Good: Using one of the tokens
        Await Task.Delay(500, primaryToken)
        
        Console.WriteLine("Multiple tokens processing")
    End Function
    
    ' Violation: Async method in property
    Public ReadOnly Property AsyncPropertyAsync(cancellationToken As CancellationToken) As Task(Of String)
        Get
            Return GetAsyncValueAsync(cancellationToken)
        End Get
    End Property
    
    Private Async Function GetAsyncValueAsync(token As CancellationToken) As Task(Of String)
        ' Violation: Task.Delay without forwarding token
        Await Task.Delay(100)
        
        Return "Property value"
    End Function
    
End Class

' Test with inheritance
Public Class BaseAsyncClass
    
    ' Violation: Virtual method with CancellationToken not forwarding
    Public Overridable Async Function ProcessVirtualAsync(cancellationToken As CancellationToken) As Task
        ' Violation: Task.Delay without CancellationToken
        Await Task.Delay(500)
        
        Console.WriteLine("Base virtual processing")
    End Function
    
End Class

Public Class DerivedAsyncClass
    Inherits BaseAsyncClass
    
    ' Violation: Override method not forwarding token
    Public Overrides Async Function ProcessVirtualAsync(cancellationToken As CancellationToken) As Task
        ' Call base method properly
        Await MyBase.ProcessVirtualAsync(cancellationToken)
        
        ' Violation: Additional processing without token forwarding
        Await Task.Delay(200)
        
        Console.WriteLine("Derived processing")
    End Function
    
End Class

' Test with interface implementation
Public Interface IAsyncProcessor
    Function ProcessAsync(cancellationToken As CancellationToken) As Task
End Interface

Public Class AsyncProcessor
    Implements IAsyncProcessor
    
    ' Violation: Interface implementation not forwarding token
    Public Async Function ProcessAsync(cancellationToken As CancellationToken) As Task Implements IAsyncProcessor.ProcessAsync
        ' Violation: Task.Run without CancellationToken
        Await Task.Run(Sub() Console.WriteLine("Interface implementation processing"))
        
        Console.WriteLine("Interface processing completed")
    End Function
    
End Class
