' VB.NET test file for cq-vbn-0112: Do not use Count/LongCount when Any can be used
' Rule: Count or LongCount method was used where Any method would be more efficient.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class CountUsageExamples
    
    Public Sub TestCountUsage()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim names As String() = {"Alice", "Bob", "Charlie", "David"}
        Dim items As IEnumerable(Of String) = {"item1", "item2", "item3"}
        
        ' Violation: Using Count() > 0 instead of Any()
        If numbers.Count() > 0 Then
            Console.WriteLine("Numbers collection has items")
        End If
        
        ' Violation: Using Count() > 0 on array
        If names.Count() > 0 Then
            Console.WriteLine("Names array has items")
        End If
        
        ' Violation: Using Count() > 0 on IEnumerable
        If items.Count() > 0 Then
            Console.WriteLine("Items enumerable has items")
        End If
        
        ' Violation: Using LongCount() > 0 instead of Any()
        If numbers.LongCount() > 0 Then
            Console.WriteLine("Numbers collection has items")
        End If
        
        ' Violation: Using LongCount() > 0 on array
        If names.LongCount() > 0 Then
            Console.WriteLine("Names array has items")
        End If
        
        ' Violation: Using Count() >= 1 instead of Any()
        If numbers.Count() >= 1 Then
            Console.WriteLine("Numbers collection has at least one item")
        End If
        
        ' Violation: Using LongCount() >= 1 instead of Any()
        If numbers.LongCount() >= 1 Then
            Console.WriteLine("Numbers collection has at least one item")
        End If
        
        ' Violation: Using Count() > 0 in assignment
        Dim hasItems = numbers.Count() > 0
        
        ' Violation: Using LongCount() > 0 in assignment
        Dim hasLongItems = numbers.LongCount() > 0
        
        ' Violation: Using Count() >= 1 in assignment
        Dim hasAtLeastOne = numbers.Count() >= 1
        
        ' Violation: Using LongCount() >= 1 in assignment
        Dim hasAtLeastOneLong = numbers.LongCount() >= 1
        
        ' Violation: Using Count() > 0 in method parameter
        ProcessBoolean(numbers.Count() > 0)
        
        ' Violation: Using LongCount() > 0 in method parameter
        ProcessBoolean(numbers.LongCount() > 0)
        
        ' Violation: Using Count() > 0 in return statement
        Return numbers.Count() > 0
        
        ' Violation: Using Count() > 0 with method call
        If GetNumbers().Count() > 0 Then
            Console.WriteLine("Method returned non-empty collection")
        End If
        
        ' Violation: Using LongCount() > 0 with method call
        If GetNumbers().LongCount() > 0 Then
            Console.WriteLine("Method returned non-empty collection")
        End If
        
        ' Violation: Using Count() >= 1 with method call
        If GetNumbers().Count() >= 1 Then
            Console.WriteLine("Method returned at least one item")
        End If
        
        ' Violation: Using LongCount() >= 1 with method call
        If GetNumbers().LongCount() >= 1 Then
            Console.WriteLine("Method returned at least one item")
        End If
        
    End Sub
    
    Public Function CheckCollectionStatus() As Boolean
        Dim data As IEnumerable(Of Integer) = GetData()
        
        ' Violation: Using Count() > 0 in function
        If data.Count() > 0 Then
            Return True
        End If
        
        Return False
    End Function
    
    Public Sub ProcessCollections()
        Dim collections As List(Of List(Of String)) = GetCollections()
        
        For Each collection In collections
            ' Violation: Using Count() > 0 in loop
            If collection.Count() > 0 Then
                ProcessCollection(collection)
            End If
            
            ' Violation: Using LongCount() > 0 in loop
            If collection.LongCount() > 0 Then
                ProcessCollection(collection)
            End If
        Next
    End Sub
    
    Private Function GetNumbers() As List(Of Integer)
        Return New List(Of Integer) From {10, 20, 30}
    End Function
    
    Private Function GetData() As IEnumerable(Of Integer)
        Return New List(Of Integer) From {1, 2, 3}
    End Function
    
    Private Function GetCollections() As List(Of List(Of String))
        Return New List(Of List(Of String))()
    End Function
    
    Private Sub ProcessBoolean(value As Boolean)
        Console.WriteLine($"Boolean value: {value}")
    End Sub
    
    Private Sub ProcessCollection(collection As List(Of String))
        Console.WriteLine($"Processing collection with {collection.Count} items")
    End Sub
    
End Class

' More violation examples in different contexts

Public Class ValidationExamples
    
    Public Function ValidateInput(input As IEnumerable(Of String)) As Boolean
        ' Violation: Using Count() > 0 for validation
        If input.Count() > 0 Then
            Return True
        End If
        
        Return False
    End Function
    
    Public Sub CheckMultipleConditions()
        Dim list1 As List(Of Integer) = GetList1()
        Dim list2 As List(Of Integer) = GetList2()
        
        ' Violation: Using Count() > 0 in complex condition
        If list1.Count() > 0 AndAlso list2.Count() > 0 Then
            Console.WriteLine("Both lists have items")
        End If
        
        ' Violation: Using LongCount() >= 1 in complex condition
        If list1.LongCount() >= 1 OrElse list2.LongCount() >= 1 Then
            Console.WriteLine("At least one list has items")
        End If
    End Sub
    
    Private Function GetList1() As List(Of Integer)
        Return New List(Of Integer) From {1, 2}
    End Function
    
    Private Function GetList2() As List(Of Integer)
        Return New List(Of Integer) From {3, 4}
    End Function
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperUsageExamples
    
    Public Sub TestProperUsage()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        
        ' Correct: Using Any() - should not be detected
        If numbers.Any() Then
            Console.WriteLine("Numbers collection has items")
        End If
        
        ' Correct: Using Count() for exact count comparison - should not be detected
        If numbers.Count() = 5 Then
            Console.WriteLine("Numbers collection has exactly 5 items")
        End If
        
        ' Correct: Using Count() > 1 for multiple items - should not be detected
        If numbers.Count() > 1 Then
            Console.WriteLine("Numbers collection has multiple items")
        End If
        
        ' Correct: Using Count() with different comparison - should not be detected
        If numbers.Count() < 10 Then
            Console.WriteLine("Numbers collection has less than 10 items")
        End If
        
        ' Correct: Using Count property instead of method - should not be detected
        If numbers.Count > 0 Then
            Console.WriteLine("Numbers collection has items")
        End If
        
        ' Correct: Using Any() with predicate - should not be detected
        If numbers.Any(Function(n) n > 3) Then
            Console.WriteLine("Numbers collection has items greater than 3")
        End If
        
    End Sub
    
End Class
