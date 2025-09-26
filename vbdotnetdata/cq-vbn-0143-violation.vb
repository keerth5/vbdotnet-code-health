' VB.NET test file for cq-vbn-0143: Use concrete types when possible for improved performance
' Rule: Code uses interface types or abstract types, leading to unnecessary interface calls or virtual calls.

Imports System
Imports System.Collections.Generic
Imports System.IO

Public Class ConcreteTypesExamples
    
    ' Violation: Using interface type when concrete type could be used
    Private _items As IList(Of String) = New List(Of String)()
    Private _dictionary As IDictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
    Private _collection As ICollection(Of String) = New List(Of String)()
    Private _enumerable As IEnumerable(Of String) = New List(Of String)()
    
    Public Sub TestInterfaceVariables()
        ' Violation: Declaring variables with interface types
        Dim list As IList(Of Integer) = New List(Of Integer)()
        Dim dict As IDictionary(Of String, String) = New Dictionary(Of String, String)()
        Dim set As ISet(Of String) = New HashSet(Of String)()
        
        ' Using the variables
        list.Add(1)
        dict.Add("key", "value")
        set.Add("item")
        
        Console.WriteLine($"List count: {list.Count}")
        Console.WriteLine($"Dict count: {dict.Count}")
        Console.WriteLine($"Set count: {set.Count}")
    End Sub
    
    Public Sub TestMethodParameters()
        ' These methods will be called with interface parameters
        ProcessList(New List(Of String) From {"a", "b", "c"})
        ProcessDictionary(New Dictionary(Of String, Integer) From {{"one", 1}, {"two", 2}})
        ProcessCollection(New List(Of Integer) From {1, 2, 3})
    End Sub
    
    ' Violation: Method parameter using interface type when concrete type is always passed
    Private Sub ProcessList(data As IList(Of String))
        For Each item In data
            Console.WriteLine(item)
        Next
    End Sub
    
    ' Violation: Method parameter using interface type
    Private Sub ProcessDictionary(data As IDictionary(Of String, Integer))
        For Each kvp In data
            Console.WriteLine($"{kvp.Key}: {kvp.Value}")
        Next
    End Sub
    
    ' Violation: Method parameter using interface type
    Private Sub ProcessCollection(data As ICollection(Of Integer))
        Console.WriteLine($"Collection has {data.Count} items")
    End Sub
    
    Public Sub TestReturnTypes()
        ' Calling methods that return interface types
        Dim myList As IList(Of String) = GetStringList()
        Dim myDict As IDictionary(Of Integer, String) = GetIntegerStringDictionary()
        
        myList.Add("new item")
        myDict.Add(4, "four")
    End Sub
    
    ' Violation: Return type using interface when concrete type is always returned
    Private Function GetStringList() As IList(Of String)
        Return New List(Of String) From {"item1", "item2", "item3"}
    End Function
    
    ' Violation: Return type using interface when concrete type is always returned
    Private Function GetIntegerStringDictionary() As IDictionary(Of Integer, String)
        Return New Dictionary(Of Integer, String) From {{1, "one"}, {2, "two"}, {3, "three"}}
    End Function
    
    Public Sub TestStreamExamples()
        ' Violation: Using abstract Stream type when concrete type is known
        Dim stream As Stream = New MemoryStream()
        Dim fileStream As Stream = New FileStream("test.txt", FileMode.Create)
        
        stream.Write(New Byte() {1, 2, 3}, 0, 3)
        fileStream.Write(New Byte() {4, 5, 6}, 0, 3)
        
        stream.Close()
        fileStream.Close()
    End Sub
    
    Public Sub TestAbstractClassUsage()
        ' These would be violations if we had abstract classes being used as concrete types
        ' For demonstration purposes with Stream which is abstract
        ProcessStream(New MemoryStream(New Byte() {1, 2, 3, 4, 5}))
        ProcessStream(New FileStream("temp.dat", FileMode.Create))
    End Sub
    
    ' Violation: Using abstract type as parameter when concrete types are always passed
    Private Sub ProcessStream(stream As Stream)
        Dim buffer(1023) As Byte
        Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
        Console.WriteLine($"Read {bytesRead} bytes")
    End Sub
    
    Public Sub TestGenericInterfaceUsage()
        ' Violation: Using generic interface types
        Dim comparer As IComparer(Of String) = StringComparer.OrdinalIgnoreCase
        Dim equalityComparer As IEqualityComparer(Of String) = StringComparer.OrdinalIgnoreCase
        
        Dim result As Integer = comparer.Compare("hello", "HELLO")
        Dim isEqual As Boolean = equalityComparer.Equals("test", "TEST")
        
        Console.WriteLine($"Compare result: {result}, Equal: {isEqual}")
    End Sub
    
    Public Sub TestCollectionInitialization()
        ' Violation: Interface type with concrete initialization
        Dim numbers As IList(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim words As ICollection(Of String) = New HashSet(Of String) From {"apple", "banana", "cherry"}
        Dim pairs As IDictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
            {"first", 1}, {"second", 2}, {"third", 3}
        }
        
        numbers.Add(6)
        words.Add("date")
        pairs.Add("fourth", 4)
    End Sub
    
    Public Sub TestLINQWithInterfaces()
        ' Violation: Using interface types with LINQ operations
        Dim source As IEnumerable(Of Integer) = New List(Of Integer) From {1, 2, 3, 4, 5}
        Dim filtered As IEnumerable(Of Integer) = source.Where(Function(x) x > 2)
        Dim transformed As IEnumerable(Of String) = filtered.Select(Function(x) x.ToString())
        
        For Each item In transformed
            Console.WriteLine(item)
        Next
    End Sub
    
    Public Sub TestPropertyDeclarations()
        ' These properties use interface types
        Dim obj As New ClassWithInterfaceProperties()
        obj.Items.Add("test")
        obj.Lookup.Add("key", "value")
        
        Console.WriteLine($"Items count: {obj.Items.Count}")
        Console.WriteLine($"Lookup count: {obj.Lookup.Count}")
    End Sub
    
    ' Class with interface properties (violations)
    Public Class ClassWithInterfaceProperties
        ' Violation: Property using interface type
        Public Property Items As IList(Of String) = New List(Of String)()
        
        ' Violation: Property using interface type
        Public Property Lookup As IDictionary(Of String, String) = New Dictionary(Of String, String)()
        
        ' Violation: Property using interface type
        Public Property Data As ICollection(Of Integer) = New List(Of Integer)()
    End Class
    
    Public Sub TestFieldAssignments()
        ' Violation: Assigning concrete types to interface fields
        _items = New List(Of String) From {"a", "b", "c"}
        _dictionary = New Dictionary(Of String, Integer) From {{"x", 1}, {"y", 2}}
        _collection = New HashSet(Of String) From {"p", "q", "r"}
        _enumerable = New List(Of String) From {"m", "n", "o"}
        
        Console.WriteLine("Field assignments completed")
    End Sub
    
    Public Sub TestLocalVariableAssignments()
        ' Violation: Local variables with interface types assigned concrete implementations
        Dim localList As IList(Of Double) = New List(Of Double)()
        Dim localDict As IDictionary(Of Integer, Boolean) = New Dictionary(Of Integer, Boolean)()
        Dim localSet As ISet(Of Char) = New HashSet(Of Char)()
        
        localList.Add(3.14)
        localDict.Add(1, True)
        localSet.Add("A"c)
        
        Console.WriteLine("Local variable assignments completed")
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        ' Correct: Using concrete types directly
        Dim list As List(Of String) = New List(Of String)()
        Dim dict As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
        Dim hashSet As HashSet(Of String) = New HashSet(Of String)()
        
        list.Add("item")
        dict.Add("key", 1)
        hashSet.Add("element")
        
        ' Correct: Using interface types when polymorphism is actually needed
        Dim streams As List(Of Stream) = New List(Of Stream)()
        streams.Add(New MemoryStream())
        streams.Add(New FileStream("test.txt", FileMode.Create))
        
        For Each stream In streams
            ' Here interface type makes sense because we have different implementations
            stream.WriteByte(65)
        Next
    End Sub
    
    ' Method that actually benefits from interface parameter (correct usage)
    Public Sub ProcessAnyEnumerable(data As IEnumerable(Of String))
        ' This method can work with any IEnumerable implementation
        For Each item In data
            Console.WriteLine(item)
        Next
    End Sub
    
    Public Sub TestPolymorphicUsage()
        ' Correct: These calls demonstrate why interface parameters can be useful
        ProcessAnyEnumerable(New List(Of String) From {"list", "items"})
        ProcessAnyEnumerable(New HashSet(Of String) From {"set", "items"})
        ProcessAnyEnumerable(New String() {"array", "items"})
    End Sub
End Class
