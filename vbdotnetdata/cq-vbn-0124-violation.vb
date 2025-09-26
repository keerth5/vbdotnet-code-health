' VB.NET test file for cq-vbn-0124: Use Environment.CurrentManagedThreadId instead of Thread.CurrentThread.ManagedThreadId
' Rule: Environment.CurrentManagedThreadId is more compact and efficient than Thread.CurrentThread.ManagedThreadId.

Imports System
Imports System.Threading

Public Class ThreadIdExamples
    
    Public Sub TestThreadId()
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId instead of Environment.CurrentManagedThreadId
        Dim currentThreadId = Thread.CurrentThread.ManagedThreadId
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId in assignment
        Dim threadId As Integer = Thread.CurrentThread.ManagedThreadId
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId in method parameter
        LogThreadId(Thread.CurrentThread.ManagedThreadId)
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId in string interpolation
        Console.WriteLine($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}")
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId in conditional
        If Thread.CurrentThread.ManagedThreadId = 1 Then
            Console.WriteLine("Running on main thread")
        End If
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId in return statement
        Return Thread.CurrentThread.ManagedThreadId
        
        Console.WriteLine($"Thread ID: {currentThreadId}")
    End Sub
    
    Public Function GetCurrentThreadId() As Integer
        ' Violation: Using Thread.CurrentThread.ManagedThreadId in function return
        Return Thread.CurrentThread.ManagedThreadId
    End Function
    
    Public Sub LogThreadInformation()
        ' Violation: Using Thread.CurrentThread.ManagedThreadId in logging
        Console.WriteLine($"[{DateTime.Now}] Thread {Thread.CurrentThread.ManagedThreadId} executing")
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId with other thread information
        Dim threadName = Thread.CurrentThread.Name
        Console.WriteLine($"Thread: {threadName} (ID: {Thread.CurrentThread.ManagedThreadId})")
    End Sub
    
    Public Sub CompareThreadIds()
        Dim savedThreadId As Integer = Thread.CurrentThread.ManagedThreadId
        
        ' Simulate some async work
        Task.Run(Sub()
                     ' Violation: Using Thread.CurrentThread.ManagedThreadId in comparison
                     If Thread.CurrentThread.ManagedThreadId <> savedThreadId Then
                         Console.WriteLine("Running on different thread")
                     End If
                 End Sub).Wait()
    End Sub
    
    Private Sub LogThreadId(threadId As Integer)
        Console.WriteLine($"Logged thread ID: {threadId}")
    End Sub
    
End Class

' More violation examples

Public Class ThreadTrackingExample
    
    Public Sub TrackThreadExecution()
        ' Violation: Using Thread.CurrentThread.ManagedThreadId for tracking
        Dim startThreadId = Thread.CurrentThread.ManagedThreadId
        
        Console.WriteLine($"Started on thread {startThreadId}")
        
        ' Do some work
        DoWork()
        
        ' Violation: Using Thread.CurrentThread.ManagedThreadId for verification
        Dim endThreadId = Thread.CurrentThread.ManagedThreadId
        
        If startThreadId = endThreadId Then
            Console.WriteLine("Completed on same thread")
        Else
            Console.WriteLine("Thread switched during execution")
        End If
    End Sub
    
    Private Sub DoWork()
        Threading.Thread.Sleep(100)
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperThreadUsageExamples
    
    Public Sub TestProperUsage()
        
        ' Correct: Using Environment.CurrentManagedThreadId - should not be detected
        Dim currentThreadId = Environment.CurrentManagedThreadId
        
        ' Correct: Using other Thread properties - should not be detected
        Dim threadName = Thread.CurrentThread.Name
        Dim isBackground = Thread.CurrentThread.IsBackground
        
        ' Correct: Using Thread.CurrentThread for other purposes - should not be detected
        Dim currentThread = Thread.CurrentThread
        Console.WriteLine($"Thread name: {currentThread.Name}")
        
        Console.WriteLine($"Current thread ID: {currentThreadId}")
    End Sub
    
End Class
