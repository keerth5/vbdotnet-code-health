' Test file for cq-vbn-0167: Do not use stackalloc in loops
' Stack space allocated by a stackalloc is only released at the end of the current method's invocation. Using it in a loop can result in unbounded stack growth and eventual stack overflow conditions.

Imports System

Public Class StackallocInLoopTests
    
    ' Note: VB.NET doesn't have direct stackalloc keyword like C#, but we'll test patterns that might be detected
    ' The regex patterns are looking for stackalloc usage in loops
    
    ' Violation: stackalloc in For loop
    Public Sub StackallocInForLoop()
        For i As Integer = 0 To 9
            ' Violation: Using stackalloc inside For loop
            ' Note: This is conceptual - VB.NET syntax would be different
            ' stackalloc int[100];
            Console.WriteLine($"For loop iteration {i} with stackalloc")
        Next
    End Sub
    
    ' Violation: stackalloc in While loop
    Public Sub StackallocInWhileLoop()
        Dim counter As Integer = 0
        While counter < 10
            ' Violation: Using stackalloc inside While loop
            ' stackalloc byte[256];
            Console.WriteLine($"While loop iteration {counter} with stackalloc")
            counter += 1
        End While
    End Sub
    
    ' Violation: stackalloc in Do loop
    Public Sub StackallocInDoLoop()
        Dim index As Integer = 0
        Do
            ' Violation: Using stackalloc inside Do loop
            ' stackalloc char[50];
            Console.WriteLine($"Do loop iteration {index} with stackalloc")
            index += 1
        Loop While index < 5
    End Sub
    
    ' Violation: stackalloc in Do Until loop
    Public Sub StackallocInDoUntilLoop()
        Dim count As Integer = 0
        Do Until count >= 8
            ' Violation: Using stackalloc inside Do Until loop
            ' stackalloc double[25];
            Console.WriteLine($"Do Until loop iteration {count} with stackalloc")
            count += 1
        Loop
    End Sub
    
    ' Violation: stackalloc in For Each loop
    Public Sub StackallocInForEachLoop()
        Dim items() As String = {"item1", "item2", "item3"}
        
        For Each item In items
            ' Violation: Using stackalloc inside For Each loop
            ' stackalloc int[200];
            Console.WriteLine($"For Each loop with item {item} and stackalloc")
        Next
    End Sub
    
    ' Violation: Nested loops with stackalloc
    Public Sub StackallocInNestedLoops()
        For i As Integer = 0 To 2
            For j As Integer = 0 To 2
                ' Violation: Using stackalloc inside nested For loops
                ' stackalloc float[75];
                Console.WriteLine($"Nested loop [{i}][{j}] with stackalloc")
            Next
        Next
    End Sub
    
    ' Good practice: stackalloc outside of loops (should not be detected)
    Public Sub StackallocOutsideLoop()
        ' Good: Using stackalloc outside of loop
        ' stackalloc int[100];
        Console.WriteLine("stackalloc used outside of loop")
        
        For i As Integer = 0 To 4
            ' Good: Regular operations in loop without stackalloc
            Console.WriteLine($"Loop iteration {i} without stackalloc")
        Next
    End Sub
    
    ' Violation: stackalloc in complex loop structure
    Public Sub ComplexLoopWithStackalloc()
        Dim data As New List(Of Integer) From {1, 2, 3, 4, 5}
        
        For Each value In data.Where(Function(x) x > 2)
            ' Violation: Using stackalloc in complex For Each with LINQ
            ' stackalloc long[30];
            Console.WriteLine($"Complex loop with value {value} and stackalloc")
        Next
    End Sub
    
    ' Violation: stackalloc in conditional loop
    Public Sub ConditionalLoopWithStackalloc()
        Dim useLoop As Boolean = True
        
        If useLoop Then
            For i As Integer = 0 To 3
                ' Violation: Using stackalloc inside conditional For loop
                ' stackalloc short[150];
                Console.WriteLine($"Conditional loop iteration {i} with stackalloc")
            Next
        End If
    End Sub
    
    ' Violation: stackalloc in loop with exception handling
    Public Sub LoopWithExceptionHandlingAndStackalloc()
        For i As Integer = 0 To 5
            Try
                ' Violation: Using stackalloc inside For loop within try block
                ' stackalloc byte[300];
                Console.WriteLine($"Exception handling loop iteration {i} with stackalloc")
                
                If i = 3 Then
                    Throw New InvalidOperationException("Test exception")
                End If
            Catch ex As Exception
                Console.WriteLine($"Caught exception in iteration {i}: {ex.Message}")
            End Try
        Next
    End Sub
    
    ' Violation: Multiple stackalloc calls in same loop
    Public Sub MultipleStackallocInLoop()
        For i As Integer = 0 To 2
            ' Violation: First stackalloc in loop
            ' stackalloc int[50];
            
            ' Violation: Second stackalloc in same loop iteration
            ' stackalloc char[25];
            
            Console.WriteLine($"Multiple stackalloc calls in iteration {i}")
        Next
    End Sub
    
End Class

' Additional test cases
Public Module StackallocUtilities
    
    ' Violation: Utility method with stackalloc in loop
    Public Sub ProcessDataWithStackalloc()
        Dim items() As Integer = {10, 20, 30, 40, 50}
        
        For Each item In items
            ' Violation: Using stackalloc in utility method loop
            ' stackalloc int[item];
            Console.WriteLine($"Processing item {item} with stackalloc")
        Next
    End Sub
    
    ' Violation: Generic method with stackalloc in loop
    Public Sub ProcessGenericDataWithStackalloc(Of T)(data As IEnumerable(Of T))
        For Each item In data
            ' Violation: Using stackalloc in generic method loop
            ' stackalloc byte[128];
            Console.WriteLine($"Processing generic item {item} with stackalloc")
        Next
    End Sub
    
    ' Good: Utility method with stackalloc outside loop
    Public Sub ProcessDataWithoutLoopStackalloc()
        ' Good: Using stackalloc outside of loop
        ' stackalloc int[1000];
        
        Dim items() As String = {"a", "b", "c"}
        For Each item In items
            ' Good: No stackalloc in loop
            Console.WriteLine($"Processing {item} without stackalloc in loop")
        Next
    End Sub
    
End Module

' Test with advanced loop patterns
Public Class AdvancedLoopPatterns
    
    ' Violation: stackalloc in parallel loop
    Public Sub StackallocInParallelLoop()
        Parallel.For(0, 10, Sub(i)
                                ' Violation: Using stackalloc in parallel loop
                                ' stackalloc double[64];
                                Console.WriteLine($"Parallel iteration {i} with stackalloc")
                            End Sub)
    End Sub
    
    ' Violation: stackalloc in LINQ loop operations
    Public Sub StackallocInLinqLoop()
        Dim numbers As Integer() = {1, 2, 3, 4, 5}
        
        Dim results = numbers.Select(Function(n)
                                         ' Violation: Using stackalloc in LINQ Select
                                         ' stackalloc int[n * 10];
                                         Return n * 2
                                     End Function).ToArray()
        
        Console.WriteLine($"LINQ results with stackalloc: {String.Join(", ", results)}")
    End Sub
    
    ' Violation: stackalloc in async loop
    Public Async Function StackallocInAsyncLoop() As Task
        For i As Integer = 0 To 3
            ' Violation: Using stackalloc in async method loop
            ' stackalloc byte[256];
            
            Await Task.Delay(100)
            Console.WriteLine($"Async loop iteration {i} with stackalloc")
        Next
    End Function
    
    ' Violation: stackalloc in recursive method with loop
    Public Sub RecursiveMethodWithStackallocLoop(depth As Integer)
        If depth > 0 Then
            For i As Integer = 0 To 1
                ' Violation: Using stackalloc in recursive method loop
                ' stackalloc int[depth * 10];
                Console.WriteLine($"Recursive depth {depth}, iteration {i} with stackalloc")
            Next
            
            RecursiveMethodWithStackallocLoop(depth - 1)
        End If
    End Sub
    
    ' Violation: stackalloc in loop with goto
    Public Sub LoopWithGotoAndStackalloc()
        Dim i As Integer = 0
        
LoopStart:
        If i < 5 Then
            ' Violation: Using stackalloc in goto loop
            ' stackalloc char[100];
            Console.WriteLine($"Goto loop iteration {i} with stackalloc")
            i += 1
            GoTo LoopStart
        End If
    End Sub
    
    ' Violation: stackalloc in loop with complex control flow
    Public Sub ComplexControlFlowWithStackalloc()
        For i As Integer = 0 To 10
            If i Mod 2 = 0 Then
                ' Violation: Using stackalloc in conditional within loop
                ' stackalloc int[i + 10];
                Console.WriteLine($"Even iteration {i} with stackalloc")
                Continue For
            End If
            
            Select Case i
                Case 1, 3, 5
                    ' Violation: Using stackalloc in Select Case within loop
                    ' stackalloc byte[i * 20];
                    Console.WriteLine($"Odd case {i} with stackalloc")
                Case Else
                    Console.WriteLine($"Other case {i}")
            End Select
        Next
    End Sub
    
End Class

' Test with different data types and sizes
Public Class StackallocDataTypeTests
    
    ' Violation: Different stackalloc sizes in loops
    Public Sub DifferentStackallocSizesInLoops()
        ' Small stackalloc in loop
        For i As Integer = 0 To 2
            ' Violation: Small stackalloc in loop
            ' stackalloc byte[10];
            Console.WriteLine($"Small stackalloc iteration {i}")
        Next
        
        ' Medium stackalloc in loop
        While True
            ' Violation: Medium stackalloc in While loop
            ' stackalloc int[500];
            Console.WriteLine("Medium stackalloc in while")
            Exit While
        End While
        
        ' Large stackalloc in loop
        Do
            ' Violation: Large stackalloc in Do loop
            ' stackalloc double[1000];
            Console.WriteLine("Large stackalloc in do loop")
            Exit Do
        Loop
    End Sub
    
End Class
