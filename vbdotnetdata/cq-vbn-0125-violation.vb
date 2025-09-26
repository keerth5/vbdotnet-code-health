' VB.NET test file for cq-vbn-0125: Prefer Dictionary Contains methods
' Rule: Calling Contains on the Keys or Values collection may often be more expensive than calling ContainsKey or ContainsValue on the dictionary itself.

Imports System
Imports System.Collections.Generic

Public Class DictionaryContainsExamples
    
    Public Sub TestDictionaryContains()
        
        Dim userDict As New Dictionary(Of String, Integer)()
        userDict.Add("John", 25)
        userDict.Add("Jane", 30)
        userDict.Add("Bob", 35)
        
        ' Violation: Using Keys.Contains instead of ContainsKey
        If userDict.Keys.Contains("John") Then
            Console.WriteLine("John found in dictionary")
        End If
        
        ' Violation: Using Values.Contains instead of ContainsValue
        If userDict.Values.Contains(25) Then
            Console.WriteLine("Age 25 found in dictionary")
        End If
        
        ' Violation: Using Keys.Contains in variable assignment
        Dim hasJohn = userDict.Keys.Contains("John")
        
        ' Violation: Using Values.Contains in variable assignment
        Dim hasAge30 = userDict.Values.Contains(30)
        
        ' Violation: Using Keys.Contains in method parameter
        ProcessKeyExists(userDict.Keys.Contains("Jane"))
        
        ' Violation: Using Values.Contains in method parameter
        ProcessValueExists(userDict.Values.Contains(35))
        
        Console.WriteLine($"Has John: {hasJohn}, Has Age 30: {hasAge30}")
    End Sub
    
    Public Sub TestNestedDictionaryContains()
        
        Dim nestedDict As New Dictionary(Of String, Dictionary(Of String, String))()
        nestedDict.Add("users", New Dictionary(Of String, String)())
        nestedDict("users").Add("admin", "password123")
        
        ' Violation: Using Keys.Contains on nested dictionary
        If nestedDict.Keys.Contains("users") Then
            ' Violation: Using Keys.Contains on inner dictionary
            If nestedDict("users").Keys.Contains("admin") Then
                Console.WriteLine("Admin user found")
            End If
        End If
        
        ' Violation: Using Values.Contains with complex check
        If nestedDict("users").Values.Contains("password123") Then
            Console.WriteLine("Weak password found")
        End If
        
    End Sub
    
    Public Function CheckDictionaryContent() As Boolean
        
        Dim configDict As New Dictionary(Of String, Object)()
        configDict.Add("debug", True)
        configDict.Add("timeout", 30)
        configDict.Add("server", "localhost")
        
        ' Violation: Using Keys.Contains in return statement
        Return configDict.Keys.Contains("debug") AndAlso configDict.Values.Contains(True)
        
    End Function
    
    Public Sub ProcessMultipleDictionaries()
        
        Dim dict1 As New Dictionary(Of Integer, String)()
        Dim dict2 As New Dictionary(Of String, Double)()
        
        dict1.Add(1, "One")
        dict1.Add(2, "Two")
        
        dict2.Add("pi", 3.14)
        dict2.Add("e", 2.71)
        
        ' Violation: Multiple Keys.Contains calls
        If dict1.Keys.Contains(1) AndAlso dict2.Keys.Contains("pi") Then
            Console.WriteLine("Both keys found")
        End If
        
        ' Violation: Multiple Values.Contains calls
        If dict1.Values.Contains("One") OrElse dict2.Values.Contains(3.14) Then
            Console.WriteLine("At least one value found")
        End If
        
    End Sub
    
    Private Sub ProcessKeyExists(exists As Boolean)
        Console.WriteLine($"Key exists: {exists}")
    End Sub
    
    Private Sub ProcessValueExists(exists As Boolean)
        Console.WriteLine($"Value exists: {exists}")
    End Sub
    
End Class

' More violation examples with different dictionary types

Public Class GenericDictionaryExamples
    
    Public Sub TestGenericDictionary(Of TKey, TValue)(dict As Dictionary(Of TKey, TValue), key As TKey, value As TValue)
        
        ' Violation: Using Keys.Contains on generic dictionary
        If dict.Keys.Contains(key) Then
            Console.WriteLine("Key found in generic dictionary")
        End If
        
        ' Violation: Using Values.Contains on generic dictionary
        If dict.Values.Contains(value) Then
            Console.WriteLine("Value found in generic dictionary")
        End If
        
    End Sub
    
    Public Sub TestConcurrentDictionary()
        
        Dim concurrentDict As New System.Collections.Concurrent.ConcurrentDictionary(Of String, Integer)()
        concurrentDict.TryAdd("test", 42)
        
        ' Violation: Using Keys.Contains on ConcurrentDictionary
        If concurrentDict.Keys.Contains("test") Then
            Console.WriteLine("Key found in concurrent dictionary")
        End If
        
        ' Violation: Using Values.Contains on ConcurrentDictionary
        If concurrentDict.Values.Contains(42) Then
            Console.WriteLine("Value found in concurrent dictionary")
        End If
        
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperDictionaryUsageExamples
    
    Public Sub TestProperUsage()
        
        Dim userDict As New Dictionary(Of String, Integer)()
        userDict.Add("John", 25)
        userDict.Add("Jane", 30)
        
        ' Correct: Using ContainsKey instead of Keys.Contains - should not be detected
        If userDict.ContainsKey("John") Then
            Console.WriteLine("John found in dictionary")
        End If
        
        ' Correct: Using ContainsValue instead of Values.Contains - should not be detected
        If userDict.ContainsValue(25) Then
            Console.WriteLine("Age 25 found in dictionary")
        End If
        
        ' Correct: Using Keys collection for other purposes - should not be detected
        Dim keyCount = userDict.Keys.Count
        For Each key In userDict.Keys
            Console.WriteLine($"Key: {key}")
        Next
        
        ' Correct: Using Values collection for other purposes - should not be detected
        Dim valueSum = userDict.Values.Sum()
        For Each value In userDict.Values
            Console.WriteLine($"Value: {value}")
        Next
        
        Console.WriteLine($"Total keys: {keyCount}, Sum of values: {valueSum}")
    End Sub
    
End Class
