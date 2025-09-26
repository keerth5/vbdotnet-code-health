' VB.NET test file for cq-vbn-0111: Use property instead of Linq Enumerable method
' Rule: Enumerable LINQ method was used on a type that supports an equivalent, more efficient property.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class LinqMethodExamples
    
    Public Sub TestLinqMethods()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim names As String() = {"Alice", "Bob", "Charlie", "David", "Eve"}
        Dim items As IEnumerable(Of String) = {"item1", "item2", "item3"}
        
        ' Violation: Using First() method instead of indexer or First property
        Dim firstNumber = numbers.First()
        
        ' Violation: Using First() method on array
        Dim firstName = names.First()
        
        ' Violation: Using First() method on IEnumerable
        Dim firstItem = items.First()
        
        ' Violation: Using Last() method instead of indexer or Last property
        Dim lastNumber = numbers.Last()
        
        ' Violation: Using Last() method on array
        Dim lastName = names.Last()
        
        ' Violation: Using Last() method on IEnumerable
        Dim lastItem = items.Last()
        
        ' Violation: Using First() in assignment
        Dim result1 = GetNumbers().First()
        
        ' Violation: Using Last() in assignment
        Dim result2 = GetNumbers().Last()
        
        ' Violation: Using First() in method call
        ProcessItem(numbers.First())
        
        ' Violation: Using Last() in method call
        ProcessItem(numbers.Last())
        
        ' Violation: Using First() in conditional
        If numbers.First() > 0 Then
            Console.WriteLine("First number is positive")
        End If
        
        ' Violation: Using Last() in conditional
        If numbers.Last() < 10 Then
            Console.WriteLine("Last number is less than 10")
        End If
        
        ' Violation: Using First() with property access
        Dim customer = GetCustomers().First()
        Console.WriteLine(customer.Name)
        
        ' Violation: Using Last() with property access
        Dim lastCustomer = GetCustomers().Last()
        Console.WriteLine(lastCustomer.Name)
        
        ' Violation: Using First() in return statement
        Return GetItems().First()
        
        ' Violation: Using Last() in return statement
        Return GetItems().Last()
        
        ' Violation: Chained First() calls
        Dim nestedFirst = GetListOfLists().First().First()
        
        ' Violation: Chained Last() calls
        Dim nestedLast = GetListOfLists().Last().Last()
        
    End Sub
    
    Private Function GetNumbers() As List(Of Integer)
        Return New List(Of Integer) From {10, 20, 30}
    End Function
    
    Private Function GetCustomers() As List(Of Customer)
        Return New List(Of Customer)()
    End Function
    
    Private Function GetItems() As String()
        Return New String() {"a", "b", "c"}
    End Function
    
    Private Function GetListOfLists() As List(Of List(Of Integer))
        Return New List(Of List(Of Integer))()
    End Function
    
    Private Sub ProcessItem(item As Object)
        ' Process the item
    End Sub
    
End Class

Public Class Customer
    Public Property Name As String
    Public Property Id As Integer
End Class

' More violation examples in different contexts

Public Class DataProcessor
    
    Public Function ProcessData() As String
        Dim data As String() = {"first", "middle", "last"}
        
        ' Violation: Using First() to get first element
        Dim header = data.First()
        
        ' Violation: Using Last() to get last element
        Dim footer = data.Last()
        
        Return header & " - " & footer
    End Function
    
    Public Sub AnalyzeCollection()
        Dim collection As ICollection(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        
        ' Violation: Using First() on collection
        Dim firstValue = collection.First()
        
        ' Violation: Using Last() on collection
        Dim lastValue = collection.Last()
        
        Console.WriteLine($"Range: {firstValue} to {lastValue}")
    End Sub
    
End Class

Public Class QueryExamples
    
    Public Sub ExecuteQueries()
        Dim query As IQueryable(Of String) = GetQueryableData()
        
        ' Violation: Using First() on queryable
        Dim firstResult = query.First()
        
        ' Violation: Using Last() on queryable
        Dim lastResult = query.Last()
        
        Console.WriteLine($"Results: {firstResult}, {lastResult}")
    End Sub
    
    Private Function GetQueryableData() As IQueryable(Of String)
        Return New List(Of String) From {"query1", "query2", "query3"}.AsQueryable()
    End Function
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperUsageExamples
    
    Public Sub TestProperUsage()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim names As String() = {"Alice", "Bob", "Charlie"}
        
        ' Correct: Using indexer for arrays - should not be detected
        Dim firstName = names(0)
        Dim lastName = names(names.Length - 1)
        
        ' Correct: Using indexer for lists - should not be detected
        Dim firstNumber = numbers(0)
        Dim lastNumber = numbers(numbers.Count - 1)
        
        ' Correct: Using First() with predicate - should not be detected
        Dim firstEven = numbers.First(Function(n) n Mod 2 = 0)
        
        ' Correct: Using Last() with predicate - should not be detected
        Dim lastOdd = numbers.Last(Function(n) n Mod 2 = 1)
        
        ' Correct: Using FirstOrDefault() - should not be detected
        Dim firstOrDefault = numbers.FirstOrDefault()
        
        ' Correct: Using LastOrDefault() - should not be detected
        Dim lastOrDefault = numbers.LastOrDefault()
        
    End Sub
    
End Class
