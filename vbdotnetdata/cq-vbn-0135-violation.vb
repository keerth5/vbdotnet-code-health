' VB.NET test file for cq-vbn-0135: Possible multiple enumerations of IEnumerable collection
' Rule: Possible multiple enumerations of IEnumerable collection. Consider using an implementation that avoids multiple enumerations.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class MultipleEnumerationExamples
    
    Public Sub TestBasicMultipleEnumeration()
        Dim numbers As IEnumerable(Of Integer) = GetNumbers()
        
        ' Violation: Multiple enumerations of the same IEnumerable
        Dim count As Integer = numbers.Count()
        Dim firstNumber As Integer = numbers.First()
        
        Console.WriteLine($"Count: {count}, First: {firstNumber}")
    End Sub
    
    Public Sub TestEnumerationInConditions()
        Dim items As IEnumerable(Of String) = GetItems()
        
        ' Violation: Multiple enumerations - checking Any() and then using First()
        If items.Any() Then
            Dim firstItem As String = items.First()
            Console.WriteLine($"First item: {firstItem}")
        End If
        
        ' Violation: Multiple enumerations - Count() and then iteration
        If items.Count() > 0 Then
            For Each item In items
                Console.WriteLine(item)
            Next
        End If
    End Sub
    
    Public Sub TestEnumerationInLoop()
        Dim data As IEnumerable(Of Integer) = GetFilteredData()
        
        ' Violation: Multiple enumerations - Count() and then ForEach
        Console.WriteLine($"Processing {data.Count()} items")
        
        For Each value In data
            Console.WriteLine($"Value: {value}")
        Next
        
        ' Violation: Using Last() after iteration
        If data.Any() Then
            Dim lastValue As Integer = data.Last()
            Console.WriteLine($"Last value: {lastValue}")
        End If
    End Sub
    
    Public Sub TestLinqOperations()
        Dim source As IEnumerable(Of String) = GetStringData()
        
        ' Violation: Multiple LINQ operations on the same enumerable
        Dim filteredItems = source.Where(Function(x) x.Length > 3)
        Dim count As Integer = filteredItems.Count()
        Dim list As List(Of String) = filteredItems.ToList()
        
        Console.WriteLine($"Filtered count: {count}")
        Console.WriteLine($"List count: {list.Count}")
    End Sub
    
    Public Sub TestAggregationOperations()
        Dim values As IEnumerable(Of Double) = GetNumericValues()
        
        ' Violation: Multiple aggregation operations
        Dim sum As Double = values.Sum()
        Dim average As Double = values.Average()
        Dim max As Double = values.Max()
        Dim min As Double = values.Min()
        
        Console.WriteLine($"Sum: {sum}, Avg: {average}, Max: {max}, Min: {min}")
    End Sub
    
    Public Sub TestConditionalEnumeration()
        Dim collection As IEnumerable(Of Integer) = GetLargeDataSet()
        
        ' Violation: Multiple enumerations with conditions
        If collection.Any(Function(x) x > 100) Then
            Dim largeValues = collection.Where(Function(x) x > 100)
            Dim count As Integer = largeValues.Count()
            Dim first As Integer = largeValues.First()
            
            Console.WriteLine($"Large values count: {count}, First: {first}")
        End If
    End Sub
    
    Public Sub TestMethodChaining()
        Dim input As IEnumerable(Of String) = GetTextData()
        
        ' Violation: Multiple enumerations through method chaining
        Dim processed = input.Where(Function(x) Not String.IsNullOrEmpty(x))
        Dim uppercased = processed.Select(Function(x) x.ToUpper())
        
        ' Each of these causes enumeration
        Dim hasItems As Boolean = uppercased.Any()
        Dim itemCount As Integer = uppercased.Count()
        Dim firstItem As String = uppercased.FirstOrDefault()
        
        Console.WriteLine($"Has items: {hasItems}, Count: {itemCount}, First: {firstItem}")
    End Sub
    
    Public Sub TestEnumerationInDifferentMethods()
        Dim data As IEnumerable(Of Integer) = GetProcessingData()
        
        ' Violation: Passing enumerable to multiple methods that enumerate
        ProcessCount(data)
        ProcessItems(data)
        ProcessStatistics(data)
    End Sub
    
    Private Sub ProcessCount(items As IEnumerable(Of Integer))
        ' This method enumerates the collection
        Console.WriteLine($"Item count: {items.Count()}")
    End Sub
    
    Private Sub ProcessItems(items As IEnumerable(Of Integer))
        ' This method also enumerates the collection
        For Each item In items
            Console.WriteLine($"Processing: {item}")
        Next
    End Sub
    
    Private Sub ProcessStatistics(items As IEnumerable(Of Integer))
        ' This method enumerates multiple times
        Console.WriteLine($"Sum: {items.Sum()}")
        Console.WriteLine($"Average: {items.Average()}")
    End Sub
    
    Public Sub TestComplexScenario()
        Dim query As IEnumerable(Of String) = GetComplexQuery()
        
        ' Violation: Multiple complex enumerations
        Dim nonEmptyItems = query.Where(Function(x) Not String.IsNullOrWhiteSpace(x))
        
        ' Each operation causes enumeration
        If nonEmptyItems.Any() Then
            Dim count As Integer = nonEmptyItems.Count()
            Dim longestItem As String = nonEmptyItems.OrderByDescending(Function(x) x.Length).First()
            Dim shortestItem As String = nonEmptyItems.OrderBy(Function(x) x.Length).First()
            
            Console.WriteLine($"Count: {count}")
            Console.WriteLine($"Longest: {longestItem}")
            Console.WriteLine($"Shortest: {shortestItem}")
        End If
    End Sub
    
    Public Sub TestEnumerationWithGrouping()
        Dim items As IEnumerable(Of String) = GetCategorizedData()
        
        ' Violation: Multiple enumerations with grouping
        Dim grouped = items.GroupBy(Function(x) x.Substring(0, 1))
        
        Dim groupCount As Integer = grouped.Count()
        
        For Each group In grouped
            Console.WriteLine($"Group {group.Key}: {group.Count()} items")
        Next
        
        Console.WriteLine($"Total groups: {groupCount}")
    End Sub
    
    Public Function GetExpensiveEnumeration() As IEnumerable(Of Integer)
        ' Simulate expensive enumeration
        For i As Integer = 1 To 1000000
            If i Mod 2 = 0 Then
                Yield i
            End If
        Next
    End Function
    
    Public Sub TestExpensiveEnumeration()
        Dim expensiveData As IEnumerable(Of Integer) = GetExpensiveEnumeration()
        
        ' Violation: Multiple enumerations of expensive operation
        Dim hasData As Boolean = expensiveData.Any()
        Dim count As Integer = expensiveData.Count()
        Dim sum As Integer = expensiveData.Sum()
        
        Console.WriteLine($"Has data: {hasData}, Count: {count}, Sum: {sum}")
    End Sub
    
    ' Helper methods that return IEnumerable
    Private Function GetNumbers() As IEnumerable(Of Integer)
        Return Enumerable.Range(1, 10)
    End Function
    
    Private Function GetItems() As IEnumerable(Of String)
        Return New String() {"apple", "banana", "cherry", "date"}
    End Function
    
    Private Function GetFilteredData() As IEnumerable(Of Integer)
        Return Enumerable.Range(1, 100).Where(Function(x) x Mod 3 = 0)
    End Function
    
    Private Function GetStringData() As IEnumerable(Of String)
        Return New String() {"a", "bb", "ccc", "dddd", "eeeee"}
    End Function
    
    Private Function GetNumericValues() As IEnumerable(Of Double)
        Return New Double() {1.1, 2.2, 3.3, 4.4, 5.5}
    End Function
    
    Private Function GetLargeDataSet() As IEnumerable(Of Integer)
        Return Enumerable.Range(1, 1000)
    End Function
    
    Private Function GetTextData() As IEnumerable(Of String)
        Return New String() {"hello", "", "world", Nothing, "test"}
    End Function
    
    Private Function GetProcessingData() As IEnumerable(Of Integer)
        Return Enumerable.Range(1, 50).Where(Function(x) x Mod 2 = 0)
    End Function
    
    Private Function GetComplexQuery() As IEnumerable(Of String)
        Return New String() {"  ", "hello", "", "world", "   test   ", Nothing}
    End Function
    
    Private Function GetCategorizedData() As IEnumerable(Of String)
        Return New String() {"apple", "apricot", "banana", "blueberry", "cherry", "coconut"}
    End Function
    
    ' Example of correct usage (for reference)
    Public Sub CorrectUsageExample()
        Dim numbers As IEnumerable(Of Integer) = GetNumbers()
        
        ' Correct: Materialize the enumerable once
        Dim numbersList As List(Of Integer) = numbers.ToList()
        
        Dim count As Integer = numbersList.Count
        Dim firstNumber As Integer = numbersList.First()
        
        Console.WriteLine($"Count: {count}, First: {firstNumber}")
    End Sub
End Class
