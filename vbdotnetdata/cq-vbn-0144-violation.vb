' VB.NET test file for cq-vbn-0144: Avoid using 'Enumerable.Any()' extension method
' Rule: It's more efficient and clearer to use Length, Count, or IsEmpty (if possible) than to call Enumerable.Any to determine whether a collection type has any elements.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class AvoidEnumerableAnyExamples
    
    Public Sub TestBasicAnyUsage()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim words As String() = {"apple", "banana", "cherry"}
        Dim emptyList As List(Of String) = New List(Of String)()
        
        ' Violation: Using Any() on List when Count property is available
        If numbers.Any() Then
            Console.WriteLine("Numbers list has elements")
        End If
        
        ' Violation: Using Any() on Array when Length property is available
        If words.Any() Then
            Console.WriteLine("Words array has elements")
        End If
        
        ' Violation: Using Any() in negative check
        If Not emptyList.Any() Then
            Console.WriteLine("Empty list is empty")
        End If
        
        ' Violation: Using Any() in conditional expression
        Dim hasNumbers As Boolean = numbers.Any()
        
        ' Violation: Using Any() in return statement
        ' Return numbers.Any()
    End Sub
    
    Public Sub TestAnyWithCollectionTypes()
        Dim hashSet As HashSet(Of String) = New HashSet(Of String) From {"a", "b", "c"}
        Dim dictionary As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {{"one", 1}, {"two", 2}}
        Dim queue As Queue(Of Integer) = New Queue(Of Integer)()
        Dim stack As Stack(Of String) = New Stack(Of String)()
        
        ' Violation: Any() on HashSet
        If hashSet.Any() Then
            Console.WriteLine("HashSet has items")
        End If
        
        ' Violation: Any() on Dictionary
        If dictionary.Any() Then
            Console.WriteLine("Dictionary has entries")
        End If
        
        ' Violation: Any() on Queue
        If queue.Any() Then
            Console.WriteLine("Queue has items")
        End If
        
        ' Violation: Any() on Stack
        If stack.Any() Then
            Console.WriteLine("Stack has items")
        End If
    End Sub
    
    Public Sub TestAnyInLoops()
        Dim collections As List(Of List(Of Integer)) = New List(Of List(Of Integer))()
        collections.Add(New List(Of Integer) From {1, 2})
        collections.Add(New List(Of Integer)())
        collections.Add(New List(Of Integer) From {3, 4, 5})
        
        ' Violation: Using Any() in loop
        For Each collection In collections
            If collection.Any() Then
                Console.WriteLine($"Collection has {collection.Count} items")
            End If
        Next
        
        ' Violation: Any() in For loop
        For i As Integer = 0 To collections.Count - 1
            If collections(i).Any() Then
                Console.WriteLine($"Collection {i} is not empty")
            End If
        Next
    End Sub
    
    Public Sub TestAnyInComplexExpressions()
        Dim list1 As List(Of String) = New List(Of String) From {"a", "b"}
        Dim list2 As List(Of String) = New List(Of String)()
        Dim list3 As List(Of String) = New List(Of String) From {"c"}
        
        ' Violation: Any() in compound boolean expression
        If list1.Any() AndAlso list2.Any() Then
            Console.WriteLine("Both lists have items")
        End If
        
        ' Violation: Any() in Or expression
        If list1.Any() OrElse list2.Any() OrElse list3.Any() Then
            Console.WriteLine("At least one list has items")
        End If
        
        ' Violation: Any() in nested condition
        If list1.Any() Then
            If list2.Any() Then
                Console.WriteLine("Both lists have elements")
            End If
        End If
        
        ' Violation: Any() with Not operator
        If Not list2.Any() Then
            Console.WriteLine("List2 is empty")
        End If
    End Sub
    
    Public Sub TestAnyWithMethodChaining()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim words As List(Of String) = New List(Of String) From {"hello", "world", "test"}
        
        ' Violation: Any() after Where (but checking existence, not filtering)
        If numbers.Where(Function(x) x > 0).Any() Then
            Console.WriteLine("Has positive numbers")
        End If
        
        ' Violation: Any() after Select
        If words.Select(Function(w) w.ToUpper()).Any() Then
            Console.WriteLine("Has uppercase words")
        End If
        
        ' Violation: Any() after Take
        If numbers.Take(3).Any() Then
            Console.WriteLine("Has at least some numbers")
        End If
        
        ' Violation: Any() after Skip
        If numbers.Skip(2).Any() Then
            Console.WriteLine("Has numbers after skipping 2")
        End If
    End Sub
    
    Public Sub TestAnyInMethodCalls()
        Dim data As List(Of Integer) = New List(Of Integer) From {10, 20, 30}
        
        ' Violation: Any() passed to method
        ProcessCollectionStatus(data.Any())
        
        ' Violation: Any() in method parameter
        LogCollectionInfo("Data", data.Any())
        
        ' Violation: Any() in string interpolation
        Console.WriteLine($"Data has elements: {data.Any()}")
        
        ' Violation: Any() in ternary expression
        Dim status As String = If(data.Any(), "Has data", "Empty")
    End Sub
    
    Private Sub ProcessCollectionStatus(hasElements As Boolean)
        Console.WriteLine($"Collection has elements: {hasElements}")
    End Sub
    
    Private Sub LogCollectionInfo(name As String, hasElements As Boolean)
        Console.WriteLine($"{name} collection status: {If(hasElements, "Not empty", "Empty")}")
    End Sub
    
    Public Sub TestAnyWithDifferentCollectionSources()
        Dim arrayOfInts() As Integer = {1, 2, 3}
        Dim listOfStrings As List(Of String) = New List(Of String) From {"a", "b"}
        Dim hashSetOfChars As HashSet(Of Char) = New HashSet(Of Char) From {"x"c, "y"c}
        
        ' Violation: Any() on different collection types
        If arrayOfInts.Any() Then
            Console.WriteLine("Array has elements")
        End If
        
        If listOfStrings.Any() Then
            Console.WriteLine("List has elements")
        End If
        
        If hashSetOfChars.Any() Then
            Console.WriteLine("HashSet has elements")
        End If
    End Sub
    
    Public Sub TestAnyInProperties()
        Dim obj As New ClassWithCollections()
        
        ' Violation: Any() on property collections
        If obj.Numbers.Any() Then
            Console.WriteLine("Numbers property has items")
        End If
        
        If obj.Words.Any() Then
            Console.WriteLine("Words property has items")
        End If
    End Sub
    
    Public Class ClassWithCollections
        Public Property Numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3}
        Public Property Words As List(Of String) = New List(Of String) From {"hello", "world"}
    End Class
    
    Public Sub TestAnyWithFieldAccess()
        ' Violation: Any() on field collections
        If _privateNumbers.Any() Then
            Console.WriteLine("Private numbers field has items")
        End If
        
        If _privateWords.Any() Then
            Console.WriteLine("Private words field has items")
        End If
    End Sub
    
    Private _privateNumbers As List(Of Integer) = New List(Of Integer) From {10, 20}
    Private _privateWords As List(Of String) = New List(Of String) From {"test", "data"}
    
    Public Function HasAnyElements(data As List(Of String)) As Boolean
        ' Violation: Any() in return statement
        Return data.Any()
    End Function
    
    Public Sub TestAnyInVariousContexts()
        Dim items As List(Of String) = New List(Of String) From {"item1", "item2"}
        
        ' Violation: Any() in variable assignment
        Dim isEmpty As Boolean = Not items.Any()
        
        ' Violation: Any() in Select Case (VB.NET equivalent of switch)
        Select Case items.Any()
            Case True
                Console.WriteLine("Has items")
            Case False
                Console.WriteLine("No items")
        End Select
        
        ' Violation: Any() in While loop condition
        While items.Any()
            items.RemoveAt(0)
        End While
        
        ' Violation: Any() in Do While loop
        Do While items.Any()
            items.Clear()
        Loop
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3}
        Dim words() As String = {"hello", "world"}
        
        ' Correct: Using Count property
        If numbers.Count > 0 Then
            Console.WriteLine("Numbers list has elements")
        End If
        
        ' Correct: Using Length property
        If words.Length > 0 Then
            Console.WriteLine("Words array has elements")
        End If
        
        ' Correct: Using Any() with predicate (this is appropriate)
        If numbers.Any(Function(x) x > 10) Then
            Console.WriteLine("Has numbers greater than 10")
        End If
        
        ' Correct: Using Any() when working with IEnumerable from unknown source
        Dim enumerable As IEnumerable(Of String) = GetUnknownEnumerable()
        If enumerable.Any() Then
            Console.WriteLine("Enumerable has elements")
        End If
    End Sub
    
    Private Function GetUnknownEnumerable() As IEnumerable(Of String)
        ' This could return various IEnumerable implementations
        Return New List(Of String) From {"a", "b"}
    End Function
    
    Public Sub TestMoreAnyViolations()
        Dim data As New Dictionary(Of String, List(Of Integer))()
        data.Add("set1", New List(Of Integer) From {1, 2, 3})
        data.Add("set2", New List(Of Integer)())
        
        ' Violation: Any() on dictionary values
        For Each kvp In data
            If kvp.Value.Any() Then
                Console.WriteLine($"{kvp.Key} has values")
            End If
        Next
        
        ' Violation: Any() in LINQ Where clause (checking existence)
        Dim nonEmptySets = data.Where(Function(kvp) kvp.Value.Any()).ToList()
        Console.WriteLine($"Found {nonEmptySets.Count} non-empty sets")
    End Sub
End Class
