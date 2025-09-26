' VB.NET test file for cq-vbn-0114: Use Length/Count property instead of Enumerable.Count method
' Rule: Count LINQ method was used on a type that supports an equivalent, more efficient Length or Count property.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class CountMethodExamples
    
    Public Sub TestCountMethod()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim names As String() = {"Alice", "Bob", "Charlie", "David"}
        Dim text As String = "Hello World"
        Dim items As ICollection(Of String) = New List(Of String) From {"item1", "item2", "item3"}
        
        ' Violation: Using Count() method on List instead of Count property
        Dim listCount = numbers.Count()
        
        ' Violation: Using Count() method on array instead of Length property
        Dim arrayCount = names.Count()
        
        ' Violation: Using Count() method on string instead of Length property
        Dim stringCount = text.Count()
        
        ' Violation: Using Count() method on ICollection instead of Count property
        Dim collectionCount = items.Count()
        
        ' Violation: Using Enumerable.Count() method explicitly
        Dim explicitCount = Enumerable.Count(numbers)
        
        ' Violation: Using Enumerable.Count() method on array
        Dim explicitArrayCount = Enumerable.Count(names)
        
        ' Violation: Using Count() method in conditional
        If numbers.Count() > 0 Then
            Console.WriteLine("List has items")
        End If
        
        ' Violation: Using Count() method in assignment
        Dim totalItems = numbers.Count() + names.Count()
        
        ' Violation: Using Count() method in method parameter
        ProcessCount(numbers.Count())
        
        ' Violation: Using Count() method in return statement
        Return numbers.Count()
        
        ' Violation: Using Count() method with method call
        Dim methodCount = GetNumbers().Count()
        
        ' Violation: Using Enumerable.Count() with method call
        Dim explicitMethodCount = Enumerable.Count(GetNumbers())
        
        ' Violation: Using Count() method on property
        Dim propertyCount = Me.Items.Count()
        
        ' Violation: Using Count() method in complex expression
        Dim result = numbers.Count() * 2 + names.Count()
        
        ' Violation: Using Count() method in loop condition
        For i As Integer = 0 To numbers.Count() - 1
            Console.WriteLine(numbers(i))
        Next
        
        ' Violation: Using Count() method in While loop
        Dim index As Integer = 0
        While index < numbers.Count()
            Console.WriteLine(numbers(index))
            index += 1
        End While
        
    End Sub
    
    Public Function CalculateTotalCount() As Integer
        Dim list1 As List(Of String) = GetList1()
        Dim list2 As List(Of String) = GetList2()
        Dim array1 As Integer() = GetArray1()
        
        ' Violation: Using Count() method in calculation
        Return list1.Count() + list2.Count() + array1.Count()
    End Function
    
    Public Sub ProcessCollections()
        Dim collections As List(Of List(Of Integer)) = GetCollections()
        
        For Each collection In collections
            ' Violation: Using Count() method in loop
            If collection.Count() > 0 Then
                ProcessCollection(collection)
            End If
        Next
    End Sub
    
    Public Property Items As List(Of String) = New List(Of String)()
    
    Private Function GetNumbers() As List(Of Integer)
        Return New List(Of Integer) From {10, 20, 30}
    End Function
    
    Private Function GetList1() As List(Of String)
        Return New List(Of String) From {"a", "b"}
    End Function
    
    Private Function GetList2() As List(Of String)
        Return New List(Of String) From {"c", "d", "e"}
    End Function
    
    Private Function GetArray1() As Integer()
        Return New Integer() {1, 2, 3, 4}
    End Function
    
    Private Function GetCollections() As List(Of List(Of Integer))
        Return New List(Of List(Of Integer))()
    End Function
    
    Private Sub ProcessCount(count As Integer)
        Console.WriteLine($"Count: {count}")
    End Sub
    
    Private Sub ProcessCollection(collection As List(Of Integer))
        Console.WriteLine($"Processing collection with {collection.Count} items")
    End Sub
    
End Class

' More violation examples in different contexts

Public Class DataAnalysis
    
    Public Sub AnalyzeData()
        Dim dataset As List(Of Double) = GetDataset()
        Dim categories As String() = GetCategories()
        
        ' Violation: Using Count() method for analysis
        Dim dataPoints = dataset.Count()
        Dim categoryCount = categories.Count()
        
        Console.WriteLine($"Dataset has {dataPoints} points across {categoryCount} categories")
        
        ' Violation: Using Enumerable.Count() for analysis
        Dim explicitDataPoints = Enumerable.Count(dataset)
        Dim explicitCategoryCount = Enumerable.Count(categories)
        
        Console.WriteLine($"Explicit count: {explicitDataPoints}, {explicitCategoryCount}")
    End Sub
    
    Public Function GetStatistics() As Dictionary(Of String, Integer)
        Dim data As List(Of Integer) = GetData()
        Dim labels As String() = GetLabels()
        
        Return New Dictionary(Of String, Integer) From {
            {"DataCount", data.Count()},  ' Violation
            {"LabelCount", labels.Count()}  ' Violation
        }
    End Function
    
    Private Function GetDataset() As List(Of Double)
        Return New List(Of Double) From {1.1, 2.2, 3.3}
    End Function
    
    Private Function GetCategories() As String()
        Return New String() {"A", "B", "C"}
    End Function
    
    Private Function GetData() As List(Of Integer)
        Return New List(Of Integer) From {1, 2, 3, 4, 5}
    End Function
    
    Private Function GetLabels() As String()
        Return New String() {"Label1", "Label2", "Label3"}
    End Function
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperUsageExamples
    
    Public Sub TestProperUsage()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim names As String() = {"Alice", "Bob", "Charlie"}
        Dim text As String = "Hello World"
        
        ' Correct: Using Count property on List - should not be detected
        Dim listCount = numbers.Count
        
        ' Correct: Using Length property on array - should not be detected
        Dim arrayLength = names.Length
        
        ' Correct: Using Length property on string - should not be detected
        Dim stringLength = text.Length
        
        ' Correct: Using Count() with predicate - should not be detected
        Dim evenCount = numbers.Count(Function(n) n Mod 2 = 0)
        
        ' Correct: Using Count() on IEnumerable without Length/Count property - should not be detected
        Dim enumerable As IEnumerable(Of Integer) = numbers.Where(Function(n) n > 2)
        Dim enumerableCount = enumerable.Count()
        
    End Sub
    
End Class
