' VB.NET test file for cq-vbn-0150: Unnecessary call to 'Contains' for sets
' Rule: Both ISet<T>.Add(T) and ICollection<T>.Remove(T) perform a lookup, which makes it redundant to call ICollection<T>.Contains(T) beforehand. It's more efficient to call Add(T) or Remove(T) directly, which returns a Boolean value indicating whether the item was added or removed.

Imports System
Imports System.Collections.Generic

Public Class UnnecessarySetContainsExamples
    Private _stringSet As HashSet(Of String)
    Private _numberSet As HashSet(Of Integer)
    Private _userSet As HashSet(Of String)
    
    Public Sub New()
        _stringSet = New HashSet(Of String)()
        _numberSet = New HashSet(Of Integer)()
        _userSet = New HashSet(Of String)()
        
        ' Initialize with some data
        _stringSet.Add("apple")
        _stringSet.Add("banana")
        _numberSet.Add(1)
        _numberSet.Add(2)
        _userSet.Add("user1")
        _userSet.Add("user2")
    End Sub
    
    Public Sub TestBasicContainsBeforeAdd()
        Dim item As String = "cherry"
        
        ' Violation: Contains check before Add
        If Not _stringSet.Contains(item) Then
            _stringSet.Add(item)
        End If
        
        Dim number As Integer = 3
        
        ' Violation: Contains check before Add for numbers
        If Not _numberSet.Contains(number) Then
            _numberSet.Add(number)
        End If
    End Sub
    
    Public Sub AddItemIfNotExists(item As String)
        ' Violation: Contains before Add pattern
        If Not _stringSet.Contains(item) Then
            _stringSet.Add(item)
            Console.WriteLine($"Added item: {item}")
        Else
            Console.WriteLine($"Item {item} already exists")
        End If
    End Sub
    
    Public Sub AddUserIfNotExists(userName As String)
        ' Violation: Contains check before Add
        If Not _userSet.Contains(userName) Then
            _userSet.Add(userName)
            Console.WriteLine($"User {userName} added to set")
        End If
    End Sub
    
    Public Sub TestContainsBeforeRemove()
        Dim itemToRemove As String = "apple"
        
        ' Violation: Contains check before Remove
        If _stringSet.Contains(itemToRemove) Then
            _stringSet.Remove(itemToRemove)
            Console.WriteLine($"Removed item: {itemToRemove}")
        End If
        
        Dim numberToRemove As Integer = 1
        
        ' Violation: Contains check before Remove for numbers
        If _numberSet.Contains(numberToRemove) Then
            _numberSet.Remove(numberToRemove)
            Console.WriteLine($"Removed number: {numberToRemove}")
        End If
    End Sub
    
    Public Sub RemoveItemIfExists(item As String)
        ' Violation: Contains before Remove pattern
        If _stringSet.Contains(item) Then
            _stringSet.Remove(item)
            Console.WriteLine($"Removed item: {item}")
        Else
            Console.WriteLine($"Item {item} not found")
        End If
    End Sub
    
    Public Sub ProcessMultipleItems(items As List(Of String))
        For Each item In items
            ' Violation: Contains before Add in loop
            If Not _stringSet.Contains(item) Then
                _stringSet.Add(item)
            End If
        Next
    End Sub
    
    Public Sub ProcessItemRemovals(itemsToRemove As List(Of String))
        For Each item In itemsToRemove
            ' Violation: Contains before Remove in loop
            If _stringSet.Contains(item) Then
                _stringSet.Remove(item)
            End If
        Next
    End Sub
    
    Public Sub TestContainsAddWithNumbers()
        Dim numbersToAdd() As Integer = {10, 20, 30, 40, 50}
        
        ' Violation: Contains before Add for each number
        For Each num In numbersToAdd
            If Not _numberSet.Contains(num) Then
                _numberSet.Add(num)
                Console.WriteLine($"Added number: {num}")
            End If
        Next
    End Sub
    
    Public Sub TestContainsRemoveWithNumbers()
        Dim numbersToRemove() As Integer = {1, 2, 3, 100, 200}
        
        ' Violation: Contains before Remove for each number
        For Each num In numbersToRemove
            If _numberSet.Contains(num) Then
                _numberSet.Remove(num)
                Console.WriteLine($"Removed number: {num}")
            End If
        Next
    End Sub
    
    Public Function TryAddItem(item As String) As Boolean
        ' Violation: Contains check before Add with return value
        If Not _stringSet.Contains(item) Then
            _stringSet.Add(item)
            Return True
        Else
            Return False
        End If
    End Function
    
    Public Function TryRemoveItem(item As String) As Boolean
        ' Violation: Contains check before Remove with return value
        If _stringSet.Contains(item) Then
            _stringSet.Remove(item)
            Return True
        Else
            Return False
        End If
    End Function
    
    Public Sub TestComplexContainsPatterns()
        Dim tempSet As New HashSet(Of String)()
        Dim items() As String = {"red", "green", "blue", "yellow", "purple"}
        
        ' Violation: Contains before Add with complex logic
        For Each item In items
            If Not tempSet.Contains(item) AndAlso item.Length > 3 Then
                tempSet.Add(item)
                Console.WriteLine($"Added {item} (length > 3)")
            End If
        Next
        
        ' Violation: Contains before Remove with condition
        For Each item In items
            If tempSet.Contains(item) AndAlso item.StartsWith("g") Then
                tempSet.Remove(item)
                Console.WriteLine($"Removed {item} (starts with 'g')")
            End If
        Next
    End Sub
    
    Public Sub TestSetOperationsWithLogging()
        Dim logSet As New HashSet(Of String)()
        Dim logEntries() As String = {"INFO", "DEBUG", "ERROR", "WARN", "INFO"} ' Duplicate INFO
        
        For Each entry In logEntries
            ' Violation: Contains before Add with logging
            If Not logSet.Contains(entry) Then
                logSet.Add(entry)
                Console.WriteLine($"New log level added: {entry}")
            Else
                Console.WriteLine($"Log level {entry} already exists")
            End If
        Next
    End Sub
    
    Public Sub TestGenericSetOperations(Of T)(items As List(Of T), targetSet As HashSet(Of T))
        ' Violation: Contains before Add in generic method
        For Each item In items
            If Not targetSet.Contains(item) Then
                targetSet.Add(item)
            End If
        Next
    End Sub
    
    Public Sub TestISetInterfaceOperations()
        Dim interfaceSet As ISet(Of String) = New HashSet(Of String)()
        Dim item As String = "test"
        
        ' Violation: Contains before Add on ISet interface
        If Not interfaceSet.Contains(item) Then
            interfaceSet.Add(item)
        End If
        
        ' Violation: Contains before Remove on ISet interface
        If interfaceSet.Contains(item) Then
            interfaceSet.Remove(item)
        End If
    End Sub
    
    Public Sub TestNestedSetOperations()
        Dim outerSet As New HashSet(Of HashSet(Of String))()
        Dim innerSet1 As New HashSet(Of String) From {"a", "b", "c"}
        Dim innerSet2 As New HashSet(Of String) From {"x", "y", "z"}
        
        ' Violation: Contains before Add for nested sets
        If Not outerSet.Contains(innerSet1) Then
            outerSet.Add(innerSet1)
        End If
        
        If Not outerSet.Contains(innerSet2) Then
            outerSet.Add(innerSet2)
        End If
    End Sub
    
    Public Sub TestSetOperationsWithExceptionHandling()
        Dim safeSet As New HashSet(Of String)()
        
        Try
            Dim item As String = "testItem"
            
            ' Violation: Contains before Add even with exception handling
            If Not safeSet.Contains(item) Then
                safeSet.Add(item)
                Console.WriteLine("Item added safely")
            End If
            
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Sub
    
    Public Sub TestBatchSetOperations(itemsToAdd As List(Of String), itemsToRemove As List(Of String))
        ' Violation: Contains before Add in batch processing
        For Each item In itemsToAdd
            If Not _stringSet.Contains(item) Then
                _stringSet.Add(item)
            End If
        Next
        
        ' Violation: Contains before Remove in batch processing
        For Each item In itemsToRemove
            If _stringSet.Contains(item) Then
                _stringSet.Remove(item)
            End If
        Next
        
        Console.WriteLine($"Set now has {_stringSet.Count} items")
    End Sub
    
    Public Sub TestSetOperationsInConditionals()
        Dim condition As Boolean = True
        Dim item As String = "conditionalItem"
        
        ' Violation: Contains before Add in conditional
        If condition AndAlso Not _stringSet.Contains(item) Then
            _stringSet.Add(item)
        End If
        
        ' Violation: Contains before Remove in conditional
        If Not condition AndAlso _stringSet.Contains(item) Then
            _stringSet.Remove(item)
        End If
    End Sub
    
    Public Sub TestSetOperationsWithMethodChaining()
        Dim chainSet As New HashSet(Of String)()
        Dim items() As String = {"item1", "item2", "item3"}
        
        For Each item In items
            ' Violation: Contains before Add with method chaining
            If Not chainSet.Contains(item.ToLower()) Then
                chainSet.Add(item.ToLower())
            End If
        Next
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        ' Correct: Using Add return value
        Dim wasAdded As Boolean = _stringSet.Add("newItem")
        If wasAdded Then
            Console.WriteLine("Item was added")
        Else
            Console.WriteLine("Item already existed")
        End If
        
        ' Correct: Using Remove return value
        Dim wasRemoved As Boolean = _stringSet.Remove("oldItem")
        If wasRemoved Then
            Console.WriteLine("Item was removed")
        Else
            Console.WriteLine("Item didn't exist")
        End If
        
        ' Correct: Using Contains when not adding or removing
        If _stringSet.Contains("apple") Then
            Console.WriteLine("Set contains apple")
        End If
        
        ' Correct: Direct Add without checking (when duplicates are acceptable)
        _stringSet.Add("duplicateOkItem")
        
        ' Correct: Direct Remove without checking
        _stringSet.Remove("mayNotExistItem")
    End Sub
    
    Public Sub TestMoreSetContainsViolations()
        Dim categorySet As New HashSet(Of String)()
        Dim categories() As String = {"electronics", "books", "clothing", "home"}
        
        ' Violation: Contains before Add with transformation
        For Each category In categories
            Dim upperCategory As String = category.ToUpper()
            If Not categorySet.Contains(upperCategory) Then
                categorySet.Add(upperCategory)
            End If
        Next
        
        ' Violation: Contains before Remove with filtering
        Dim categoriesToRemove = categories.Where(Function(c) c.Length < 5).ToArray()
        For Each category In categoriesToRemove
            If categorySet.Contains(category.ToUpper()) Then
                categorySet.Remove(category.ToUpper())
            End If
        Next
    End Sub
    
    Public Sub TestSetOperationsWithCustomObjects()
        Dim personSet As New HashSet(Of Person)()
        Dim newPerson As New Person With {.Name = "John", .Age = 30}
        
        ' Violation: Contains before Add with custom objects
        If Not personSet.Contains(newPerson) Then
            personSet.Add(newPerson)
        End If
        
        ' Violation: Contains before Remove with custom objects
        If personSet.Contains(newPerson) Then
            personSet.Remove(newPerson)
        End If
    End Sub
    
    Public Class Person
        Public Property Name As String
        Public Property Age As Integer
        
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is Person Then
                Dim other As Person = CType(obj, Person)
                Return Name = other.Name AndAlso Age = other.Age
            End If
            Return False
        End Function
        
        Public Overrides Function GetHashCode() As Integer
            Return Name.GetHashCode() Xor Age.GetHashCode()
        End Function
    End Class
    
    Public Sub TestSetOperationsWithSortedSet()
        Dim sortedSet As New SortedSet(Of Integer)()
        Dim numbers() As Integer = {5, 2, 8, 1, 9, 3}
        
        For Each number In numbers
            ' Violation: Contains before Add on SortedSet
            If Not sortedSet.Contains(number) Then
                sortedSet.Add(number)
            End If
        Next
        
        For Each number In {1, 2, 10, 11}
            ' Violation: Contains before Remove on SortedSet
            If sortedSet.Contains(number) Then
                sortedSet.Remove(number)
            End If
        Next
    End Sub
    
    Public Sub TestSetOperationsInAsyncContext()
        ' Violation: Contains before Add (even in async context)
        Dim asyncSet As New HashSet(Of String)()
        Dim item As String = "asyncItem"
        
        If Not asyncSet.Contains(item) Then
            asyncSet.Add(item)
        End If
    End Sub
End Class
