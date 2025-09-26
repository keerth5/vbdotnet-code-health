' Test file for cq-vbn-0163: Do not call ToImmutableCollection on an ImmutableCollection value
' ToImmutable method was unnecessarily called on an immutable collection from System.Collections.Immutable namespace

Imports System
Imports System.Collections.Immutable
Imports System.Linq

Public Class ImmutableCollectionTests
    
    ' Violation: ToImmutableArray on already immutable array
    Public Sub ConvertImmutableArrayToImmutableArray()
        Dim immutableArray As ImmutableArray(Of String) = ImmutableArray.Create("a", "b", "c")
        
        ' Violation: ToImmutableArray on ImmutableArray
        Dim redundantArray As ImmutableArray(Of String) = immutableArray.ToImmutableArray()
        
        Console.WriteLine($"Array count: {redundantArray.Length}")
    End Sub
    
    ' Violation: ToImmutableList on already immutable list
    Public Sub ConvertImmutableListToImmutableList()
        Dim immutableList As ImmutableList(Of Integer) = ImmutableList.Create(1, 2, 3)
        
        ' Violation: ToImmutableList on ImmutableList
        Dim redundantList As ImmutableList(Of Integer) = immutableList.ToImmutableList()
        
        Console.WriteLine($"List count: {redundantList.Count}")
    End Sub
    
    ' Violation: ToImmutableDictionary on already immutable dictionary
    Public Sub ConvertImmutableDictionaryToImmutableDictionary()
        Dim builder As ImmutableDictionary(Of String, Integer).Builder = ImmutableDictionary.CreateBuilder(Of String, Integer)()
        builder.Add("one", 1)
        builder.Add("two", 2)
        Dim immutableDict As ImmutableDictionary(Of String, Integer) = builder.ToImmutable()
        
        ' Violation: ToImmutableDictionary on ImmutableDictionary
        Dim redundantDict As ImmutableDictionary(Of String, Integer) = immutableDict.ToImmutableDictionary()
        
        Console.WriteLine($"Dictionary count: {redundantDict.Count}")
    End Sub
    
    ' Violation: ToImmutableHashSet on already immutable hash set
    Public Sub ConvertImmutableHashSetToImmutableHashSet()
        Dim immutableSet As ImmutableHashSet(Of String) = ImmutableHashSet.Create("x", "y", "z")
        
        ' Violation: ToImmutableHashSet on ImmutableHashSet
        Dim redundantSet As ImmutableHashSet(Of String) = immutableSet.ToImmutableHashSet()
        
        Console.WriteLine($"Set count: {redundantSet.Count}")
    End Sub
    
    ' Violation: ToImmutableSortedDictionary on already immutable sorted dictionary
    Public Sub ConvertImmutableSortedDictionaryToImmutableSortedDictionary()
        Dim immutableSortedDict As ImmutableSortedDictionary(Of String, Integer) = ImmutableSortedDictionary.CreateBuilder(Of String, Integer)().ToImmutable()
        
        ' Violation: ToImmutableSortedDictionary on ImmutableSortedDictionary
        Dim redundantSortedDict As ImmutableSortedDictionary(Of String, Integer) = immutableSortedDict.ToImmutableSortedDictionary()
        
        Console.WriteLine($"Sorted dictionary count: {redundantSortedDict.Count}")
    End Sub
    
    ' Violation: ToImmutableSortedSet on already immutable sorted set
    Public Sub ConvertImmutableSortedSetToImmutableSortedSet()
        Dim immutableSortedSet As ImmutableSortedSet(Of Integer) = ImmutableSortedSet.Create(3, 1, 2)
        
        ' Violation: ToImmutableSortedSet on ImmutableSortedSet
        Dim redundantSortedSet As ImmutableSortedSet(Of Integer) = immutableSortedSet.ToImmutableSortedSet()
        
        Console.WriteLine($"Sorted set count: {redundantSortedSet.Count}")
    End Sub
    
    ' Good practice: ToImmutable on mutable collections (should not be detected)
    Public Sub ConvertMutableToImmutable()
        Dim mutableList As New List(Of String) From {"a", "b", "c"}
        
        ' Good: ToImmutableList on List(Of T)
        Dim immutableList As ImmutableList(Of String) = mutableList.ToImmutableList()
        
        Console.WriteLine($"Converted list count: {immutableList.Count}")
    End Sub
    
    ' Good: ToImmutable on arrays
    Public Sub ConvertArrayToImmutable()
        Dim array() As Integer = {1, 2, 3, 4, 5}
        
        ' Good: ToImmutableArray on regular array
        Dim immutableArray As ImmutableArray(Of Integer) = array.ToImmutableArray()
        
        Console.WriteLine($"Converted array count: {immutableArray.Length}")
    End Sub
    
    ' Violation: Multiple redundant conversions
    Public Sub MultipleRedundantConversions()
        Dim immutableArray As ImmutableArray(Of String) = ImmutableArray.Create("test")
        Dim immutableList As ImmutableList(Of String) = ImmutableList.Create("test")
        
        ' Violation: First redundant conversion
        Dim redundantArray As ImmutableArray(Of String) = immutableArray.ToImmutableArray()
        
        ' Violation: Second redundant conversion
        Dim redundantList As ImmutableList(Of String) = immutableList.ToImmutableList()
        
        Console.WriteLine($"Arrays: {redundantArray.Length}, Lists: {redundantList.Count}")
    End Sub
    
    ' Violation: Redundant conversion in LINQ chain
    Public Sub RedundantConversionInLinq()
        Dim immutableList As ImmutableList(Of Integer) = ImmutableList.Create(1, 2, 3, 4, 5)
        
        ' Violation: ToImmutableList on already immutable collection in LINQ
        Dim filteredList As ImmutableList(Of Integer) = immutableList.Where(Function(x) x > 2).ToImmutableList()
        
        Console.WriteLine($"Filtered count: {filteredList.Count}")
    End Sub
    
    ' Violation: Redundant conversion in method chain
    Public Sub RedundantConversionInMethodChain()
        Dim immutableArray As ImmutableArray(Of String) = ImmutableArray.Create("hello", "world")
        
        ' Violation: ToImmutableArray after Select on immutable collection
        Dim upperArray As ImmutableArray(Of String) = immutableArray.Select(Function(s) s.ToUpper()).ToImmutableArray()
        
        Console.WriteLine($"Upper array count: {upperArray.Length}")
    End Sub
    
    ' Violation: Redundant conversion in conditional
    Public Sub RedundantConversionInConditional(useArray As Boolean)
        If useArray Then
            Dim immutableArray As ImmutableArray(Of Integer) = ImmutableArray.Create(1, 2, 3)
            
            ' Violation: ToImmutableArray in conditional
            Dim result As ImmutableArray(Of Integer) = immutableArray.ToImmutableArray()
            Console.WriteLine($"Array result: {result.Length}")
        Else
            Dim immutableList As ImmutableList(Of Integer) = ImmutableList.Create(1, 2, 3)
            
            ' Violation: ToImmutableList in conditional
            Dim result As ImmutableList(Of Integer) = immutableList.ToImmutableList()
            Console.WriteLine($"List result: {result.Count}")
        End If
    End Sub
    
End Class

' Additional test cases
Public Module ImmutableCollectionUtilities
    
    ' Violation: Utility method with redundant conversion
    Public Function ProcessImmutableArray(array As ImmutableArray(Of String)) As ImmutableArray(Of String)
        ' Violation: ToImmutableArray on parameter that's already immutable
        Return array.ToImmutableArray()
    End Function
    
    ' Violation: Generic utility method
    Public Function ProcessImmutableList(Of T)(list As ImmutableList(Of T)) As ImmutableList(Of T)
        ' Violation: ToImmutableList on generic immutable list
        Return list.ToImmutableList()
    End Function
    
    ' Good: Utility method converting mutable to immutable
    Public Function ConvertToImmutableArray(Of T)(items As IEnumerable(Of T)) As ImmutableArray(Of T)
        ' Good: Converting IEnumerable to ImmutableArray
        Return items.ToImmutableArray()
    End Function
    
    ' Violation: Dictionary processing with redundant conversion
    Public Function ProcessImmutableDictionary(dict As ImmutableDictionary(Of String, Integer)) As ImmutableDictionary(Of String, Integer)
        ' Violation: ToImmutableDictionary on already immutable dictionary
        Return dict.Where(Function(kvp) kvp.Value > 0).ToImmutableDictionary()
    End Function
    
End Module

' Test with complex scenarios
Public Class ComplexImmutableScenarios
    
    ' Violation: Builder pattern with redundant final conversion
    Public Sub BuilderPatternWithRedundantConversion()
        Dim builder As ImmutableList(Of String).Builder = ImmutableList.CreateBuilder(Of String)()
        builder.Add("item1")
        builder.Add("item2")
        
        Dim immutableList As ImmutableList(Of String) = builder.ToImmutable()
        
        ' Violation: ToImmutableList on result of builder.ToImmutable()
        Dim redundantList As ImmutableList(Of String) = immutableList.ToImmutableList()
        
        Console.WriteLine($"Builder result: {redundantList.Count}")
    End Sub
    
    ' Violation: Nested redundant conversions
    Public Sub NestedRedundantConversions()
        Dim immutableArray As ImmutableArray(Of Integer) = ImmutableArray.Create(1, 2, 3)
        
        ' Violation: Multiple nested ToImmutableArray calls
        Dim nested As ImmutableArray(Of Integer) = immutableArray.ToImmutableArray().ToImmutableArray()
        
        Console.WriteLine($"Nested result: {nested.Length}")
    End Sub
    
    ' Violation: Redundant conversion in async method
    Public Async Function RedundantConversionInAsync() As Task(Of ImmutableList(Of String))
        Await Task.Delay(100)
        
        Dim immutableList As ImmutableList(Of String) = ImmutableList.Create("async", "test")
        
        ' Violation: ToImmutableList in async method
        Return immutableList.ToImmutableList()
    End Function
    
    ' Violation: Property with redundant conversion
    Public ReadOnly Property RedundantProperty As ImmutableHashSet(Of String)
        Get
            Dim immutableSet As ImmutableHashSet(Of String) = ImmutableHashSet.Create("property", "test")
            
            ' Violation: ToImmutableHashSet in property getter
            Return immutableSet.ToImmutableHashSet()
        End Get
    End Property
    
End Class
