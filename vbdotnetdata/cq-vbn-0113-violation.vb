' VB.NET test file for cq-vbn-0113: Do not use CountAsync/LongCountAsync when AnyAsync can be used
' Rule: CountAsync or LongCountAsync method was used where AnyAsync method would be more efficient.

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks

Public Class AsyncCountUsageExamples
    
    Public Async Function TestAsyncCountUsage() As Task
        Dim numbers As IAsyncEnumerable(Of Integer) = GetAsyncNumbers()
        Dim items As IAsyncEnumerable(Of String) = GetAsyncItems()
        
        ' Violation: Using CountAsync() > 0 instead of AnyAsync()
        If Await numbers.CountAsync() > 0 Then
            Console.WriteLine("Numbers collection has items")
        End If
        
        ' Violation: Using CountAsync() > 0 on different collection
        If Await items.CountAsync() > 0 Then
            Console.WriteLine("Items collection has items")
        End If
        
        ' Violation: Using LongCountAsync() > 0 instead of AnyAsync()
        If Await numbers.LongCountAsync() > 0 Then
            Console.WriteLine("Numbers collection has items")
        End If
        
        ' Violation: Using LongCountAsync() > 0 on different collection
        If Await items.LongCountAsync() > 0 Then
            Console.WriteLine("Items collection has items")
        End If
        
        ' Violation: Using CountAsync() >= 1 instead of AnyAsync()
        If Await numbers.CountAsync() >= 1 Then
            Console.WriteLine("Numbers collection has at least one item")
        End If
        
        ' Violation: Using LongCountAsync() >= 1 instead of AnyAsync()
        If Await numbers.LongCountAsync() >= 1 Then
            Console.WriteLine("Numbers collection has at least one item")
        End If
        
        ' Violation: Using CountAsync() > 0 in assignment
        Dim hasItems = Await numbers.CountAsync() > 0
        
        ' Violation: Using LongCountAsync() > 0 in assignment
        Dim hasLongItems = Await numbers.LongCountAsync() > 0
        
        ' Violation: Using CountAsync() >= 1 in assignment
        Dim hasAtLeastOne = Await numbers.CountAsync() >= 1
        
        ' Violation: Using LongCountAsync() >= 1 in assignment
        Dim hasAtLeastOneLong = Await numbers.LongCountAsync() >= 1
        
        ' Violation: Using CountAsync() > 0 in method parameter
        ProcessBooleanAsync(Await numbers.CountAsync() > 0)
        
        ' Violation: Using LongCountAsync() > 0 in method parameter
        ProcessBooleanAsync(Await numbers.LongCountAsync() > 0)
        
    End Function
    
    Public Async Function CheckAsyncCollectionStatus() As Task(Of Boolean)
        Dim data As IAsyncEnumerable(Of Integer) = GetAsyncData()
        
        ' Violation: Using CountAsync() > 0 in async function
        If Await data.CountAsync() > 0 Then
            Return True
        End If
        
        Return False
    End Function
    
    Public Async Function ValidateAsyncInput(input As IAsyncEnumerable(Of String)) As Task(Of Boolean)
        ' Violation: Using CountAsync() > 0 for validation
        If Await input.CountAsync() > 0 Then
            Return True
        End If
        
        Return False
    End Function
    
    Public Async Function ProcessAsyncCollections() As Task
        Dim collections As IAsyncEnumerable(Of IAsyncEnumerable(Of String)) = GetAsyncCollections()
        
        Await For Each collection In collections
            ' Violation: Using CountAsync() > 0 in async loop
            If Await collection.CountAsync() > 0 Then
                Await ProcessAsyncCollection(collection)
            End If
            
            ' Violation: Using LongCountAsync() > 0 in async loop
            If Await collection.LongCountAsync() > 0 Then
                Await ProcessAsyncCollection(collection)
            End If
        Next
    End Function
    
    Public Async Function CheckMultipleAsyncConditions() As Task
        Dim list1 As IAsyncEnumerable(Of Integer) = GetAsyncList1()
        Dim list2 As IAsyncEnumerable(Of Integer) = GetAsyncList2()
        
        ' Violation: Using CountAsync() > 0 in complex condition
        If Await list1.CountAsync() > 0 AndAlso Await list2.CountAsync() > 0 Then
            Console.WriteLine("Both lists have items")
        End If
        
        ' Violation: Using LongCountAsync() >= 1 in complex condition
        If Await list1.LongCountAsync() >= 1 OrElse Await list2.LongCountAsync() >= 1 Then
            Console.WriteLine("At least one list has items")
        End If
    End Function
    
    Private Async Function GetAsyncNumbers() As IAsyncEnumerable(Of Integer)
        ' Simulate async enumerable
        Yield 1
        Yield 2
        Yield 3
    End Function
    
    Private Async Function GetAsyncItems() As IAsyncEnumerable(Of String)
        ' Simulate async enumerable
        Yield "item1"
        Yield "item2"
        Yield "item3"
    End Function
    
    Private Async Function GetAsyncData() As IAsyncEnumerable(Of Integer)
        ' Simulate async enumerable
        Yield 10
        Yield 20
    End Function
    
    Private Async Function GetAsyncCollections() As IAsyncEnumerable(Of IAsyncEnumerable(Of String))
        ' Simulate nested async enumerable
        Yield GetAsyncItems()
    End Function
    
    Private Async Function GetAsyncList1() As IAsyncEnumerable(Of Integer)
        Yield 1
        Yield 2
    End Function
    
    Private Async Function GetAsyncList2() As IAsyncEnumerable(Of Integer)
        Yield 3
        Yield 4
    End Function
    
    Private Sub ProcessBooleanAsync(value As Boolean)
        Console.WriteLine($"Boolean value: {value}")
    End Sub
    
    Private Async Function ProcessAsyncCollection(collection As IAsyncEnumerable(Of String)) As Task
        Console.WriteLine("Processing async collection")
    End Function
    
End Class

' More violation examples in different async contexts

Public Class AsyncValidationExamples
    
    Public Async Function ValidateAsyncData() As Task(Of Boolean)
        Dim asyncData As IAsyncEnumerable(Of String) = GetValidationData()
        
        ' Violation: Using CountAsync() > 0 for async validation
        Return Await asyncData.CountAsync() > 0
    End Function
    
    Public Async Function CheckAsyncDataAvailability() As Task
        Dim source1 As IAsyncEnumerable(Of Integer) = GetAsyncSource1()
        Dim source2 As IAsyncEnumerable(Of Integer) = GetAsyncSource2()
        
        ' Violation: Using CountAsync() >= 1 in async method
        If Await source1.CountAsync() >= 1 Then
            Console.WriteLine("Source 1 has data")
        End If
        
        ' Violation: Using LongCountAsync() >= 1 in async method
        If Await source2.LongCountAsync() >= 1 Then
            Console.WriteLine("Source 2 has data")
        End If
    End Function
    
    Private Async Function GetValidationData() As IAsyncEnumerable(Of String)
        Yield "validation1"
        Yield "validation2"
    End Function
    
    Private Async Function GetAsyncSource1() As IAsyncEnumerable(Of Integer)
        Yield 100
    End Function
    
    Private Async Function GetAsyncSource2() As IAsyncEnumerable(Of Integer)
        Yield 200
    End Function
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperAsyncUsageExamples
    
    Public Async Function TestProperAsyncUsage() As Task
        Dim numbers As IAsyncEnumerable(Of Integer) = GetAsyncNumbers()
        
        ' Correct: Using AnyAsync() - should not be detected
        If Await numbers.AnyAsync() Then
            Console.WriteLine("Numbers collection has items")
        End If
        
        ' Correct: Using CountAsync() for exact count comparison - should not be detected
        If Await numbers.CountAsync() = 5 Then
            Console.WriteLine("Numbers collection has exactly 5 items")
        End If
        
        ' Correct: Using CountAsync() > 1 for multiple items - should not be detected
        If Await numbers.CountAsync() > 1 Then
            Console.WriteLine("Numbers collection has multiple items")
        End If
        
        ' Correct: Using AnyAsync() with predicate - should not be detected
        If Await numbers.AnyAsync(Function(n) n > 3) Then
            Console.WriteLine("Numbers collection has items greater than 3")
        End If
        
    End Function
    
    Private Async Function GetAsyncNumbers() As IAsyncEnumerable(Of Integer)
        Yield 1
        Yield 2
        Yield 3
    End Function
    
End Class
