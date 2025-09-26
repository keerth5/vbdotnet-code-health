' VB.NET test file for cq-vbn-0120: Prefer IsEmpty over Count when available
' Rule: Prefer IsEmpty property that is more efficient than Count, Length, Count<TSource>(IEnumerable<TSource>) 
' or LongCount<TSource>(IEnumerable<TSource>) to determine whether the object contains or not any items.

Imports System
Imports System.Collections.Generic

Public Class IsEmptyExamples
    
    Public Sub TestCountComparisons()
        Dim numbers As List(Of Integer) = New List(Of Integer) From {1, 2, 3}
        Dim names As String() = {"Alice", "Bob", "Charlie"}
        Dim text As String = "Hello World"
        Dim emptyList As New List(Of String)()
        
        ' Violation: Using Count = 0 instead of IsEmpty (if available)
        If numbers.Count = 0 Then
            Console.WriteLine("Numbers list is empty")
        End If
        
        ' Violation: Using Length = 0 instead of IsEmpty (if available)
        If text.Length = 0 Then
            Console.WriteLine("Text is empty")
        End If
        
        ' Violation: Using Count = 0 on empty list
        If emptyList.Count = 0 Then
            Console.WriteLine("Empty list is empty")
        End If
        
        ' Violation: Using Count <> 0 instead of not IsEmpty
        If numbers.Count <> 0 Then
            Console.WriteLine("Numbers list is not empty")
        End If
        
        ' Violation: Using Length <> 0 instead of not IsEmpty
        If text.Length <> 0 Then
            Console.WriteLine("Text is not empty")
        End If
        
        ' Violation: Using Count <> 0 on list
        If emptyList.Count <> 0 Then
            Console.WriteLine("List is not empty")
        End If
        
    End Sub
    
    Public Sub TestMoreCountComparisons()
        Dim items As List(Of String) = GetItems()
        Dim data As String = GetData()
        
        ' Violation: Using Count = 0 with method result
        If GetItems().Count = 0 Then
            Console.WriteLine("Method returned empty list")
        End If
        
        ' Violation: Using Length = 0 with method result
        If GetData().Length = 0 Then
            Console.WriteLine("Method returned empty string")
        End If
        
        ' Violation: Using Count = 0 in assignment
        Dim isEmpty = items.Count = 0
        
        ' Violation: Using Length = 0 in assignment
        Dim isDataEmpty = data.Length = 0
        
        ' Violation: Using Count <> 0 in assignment
        Dim hasItems = items.Count <> 0
        
        ' Violation: Using Length <> 0 in assignment
        Dim hasData = data.Length <> 0
        
        ' Violation: Using Count = 0 in method parameter
        ProcessBoolean(items.Count = 0)
        
        ' Violation: Using Length = 0 in method parameter
        ProcessBoolean(data.Length = 0)
        
        ' Violation: Using Count = 0 in return statement
        Return items.Count = 0
        
        ' Violation: Using Count = 0 in complex condition
        If items.Count = 0 AndAlso data.Length = 0 Then
            Console.WriteLine("Both are empty")
        End If
        
        ' Violation: Using Count <> 0 in complex condition
        If items.Count <> 0 OrElse data.Length <> 0 Then
            Console.WriteLine("At least one is not empty")
        End If
        
    End Sub
    
    Public Function CheckEmptyStatus() As Boolean
        Dim collection As List(Of Integer) = GetNumbers()
        
        ' Violation: Using Count = 0 in function
        If collection.Count = 0 Then
            Return True
        End If
        
        Return False
    End Function
    
    Public Sub ProcessCollections()
        Dim collections As List(Of List(Of String)) = GetCollections()
        
        For Each collection In collections
            ' Violation: Using Count = 0 in loop
            If collection.Count = 0 Then
                Console.WriteLine("Found empty collection")
            End If
            
            ' Violation: Using Count <> 0 in loop
            If collection.Count <> 0 Then
                ProcessCollection(collection)
            End If
        Next
    End Sub
    
    Public Sub ValidateInputs()
        Dim input1 As String = GetInput1()
        Dim input2 As String = GetInput2()
        Dim inputList As List(Of String) = GetInputList()
        
        ' Violation: Using Length = 0 for validation
        If input1.Length = 0 Then
            Throw New ArgumentException("Input1 cannot be empty")
        End If
        
        ' Violation: Using Length = 0 for validation
        If input2.Length = 0 Then
            Throw New ArgumentException("Input2 cannot be empty")
        End If
        
        ' Violation: Using Count = 0 for validation
        If inputList.Count = 0 Then
            Throw New ArgumentException("Input list cannot be empty")
        End If
    End Sub
    
    Public Sub CheckMultipleConditions()
        Dim list1 As List(Of Integer) = GetList1()
        Dim list2 As List(Of Integer) = GetList2()
        Dim str1 As String = GetString1()
        Dim str2 As String = GetString2()
        
        ' Violation: Multiple Count/Length = 0 comparisons
        If list1.Count = 0 And list2.Count = 0 Then
            Console.WriteLine("Both lists are empty")
        End If
        
        ' Violation: Multiple Count/Length <> 0 comparisons
        If str1.Length <> 0 Or str2.Length <> 0 Then
            Console.WriteLine("At least one string is not empty")
        End If
        
        ' Violation: Mixed Count/Length comparisons
        If list1.Count = 0 But str1.Length <> 0 Then
            Console.WriteLine("List is empty but string is not")
        End If
    End Sub
    
    Private Function GetItems() As List(Of String)
        Return New List(Of String) From {"item1", "item2"}
    End Function
    
    Private Function GetData() As String
        Return "sample data"
    End Function
    
    Private Function GetNumbers() As List(Of Integer)
        Return New List(Of Integer) From {1, 2, 3}
    End Function
    
    Private Function GetCollections() As List(Of List(Of String))
        Return New List(Of List(Of String))()
    End Function
    
    Private Function GetInput1() As String
        Return "input1"
    End Function
    
    Private Function GetInput2() As String
        Return "input2"
    End Function
    
    Private Function GetInputList() As List(Of String)
        Return New List(Of String) From {"input"}
    End Function
    
    Private Function GetList1() As List(Of Integer)
        Return New List(Of Integer)()
    End Function
    
    Private Function GetList2() As List(Of Integer)
        Return New List(Of Integer) From {1}
    End Function
    
    Private Function GetString1() As String
        Return "string1"
    End Function
    
    Private Function GetString2() As String
        Return ""
    End Function
    
    Private Sub ProcessBoolean(value As Boolean)
        Console.WriteLine($"Boolean value: {value}")
    End Sub
    
    Private Sub ProcessCollection(collection As List(Of String))
        Console.WriteLine($"Processing collection with {collection.Count} items")
    End Sub
    
End Class

' More violation examples in different contexts

Public Class DataValidationExample
    
    Public Function ValidateData(data As List(Of String), name As String) As Boolean
        ' Violation: Using Count = 0 for data validation
        If data.Count = 0 Then
            Console.WriteLine($"{name} data is empty")
            Return False
        End If
        
        ' Violation: Using Length = 0 for name validation
        If name.Length = 0 Then
            Console.WriteLine("Name is empty")
            Return False
        End If
        
        Return True
    End Function
    
    Public Sub ProcessUserInput()
        Dim userInputs As List(Of String) = GetUserInputs()
        
        ' Violation: Using Count = 0 for user input validation
        If userInputs.Count = 0 Then
            Console.WriteLine("No user inputs provided")
            Return
        End If
        
        For Each input In userInputs
            ' Violation: Using Length = 0 for individual input validation
            If input.Length = 0 Then
                Console.WriteLine("Skipping empty input")
                Continue For
            End If
            
            ProcessInput(input)
        Next
    End Sub
    
    Private Function GetUserInputs() As List(Of String)
        Return New List(Of String) From {"input1", "", "input3"}
    End Function
    
    Private Sub ProcessInput(input As String)
        Console.WriteLine($"Processing: {input}")
    End Sub
    
End Class

Public Class ConfigurationExample
    
    Public Sub LoadConfiguration()
        Dim configValues As List(Of String) = LoadConfigValues()
        Dim configFile As String = GetConfigFile()
        
        ' Violation: Using Count = 0 for configuration validation
        If configValues.Count = 0 Then
            Console.WriteLine("No configuration values found")
            UseDefaults()
        End If
        
        ' Violation: Using Length = 0 for config file validation
        If configFile.Length = 0 Then
            Console.WriteLine("Config file path is empty")
            configFile = "default.config"
        End If
        
        ApplyConfiguration(configValues, configFile)
    End Sub
    
    Private Function LoadConfigValues() As List(Of String)
        Return New List(Of String)()
    End Function
    
    Private Function GetConfigFile() As String
        Return ""
    End Function
    
    Private Sub UseDefaults()
        Console.WriteLine("Using default configuration")
    End Sub
    
    Private Sub ApplyConfiguration(values As List(Of String), file As String)
        Console.WriteLine($"Applying configuration from {file} with {values.Count} values")
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperEmptyCheckExamples
    
    Public Sub TestProperUsage()
        Dim numbers As List(Of Integer) = New List(Of Integer)()
        Dim text As String = ""
        
        ' Correct: Using Count > 0 - should not be detected
        If numbers.Count > 0 Then
            Console.WriteLine("Numbers list has items")
        End If
        
        ' Correct: Using Length > 0 - should not be detected
        If text.Length > 0 Then
            Console.WriteLine("Text has content")
        End If
        
        ' Correct: Using Count for specific number - should not be detected
        If numbers.Count = 5 Then
            Console.WriteLine("Numbers list has exactly 5 items")
        End If
        
        ' Correct: Using Count >= 1 - should not be detected
        If numbers.Count >= 1 Then
            Console.WriteLine("Numbers list has at least one item")
        End If
        
        ' Correct: Using String.IsNullOrEmpty - should not be detected
        If String.IsNullOrEmpty(text) Then
            Console.WriteLine("Text is null or empty")
        End If
        
        ' Correct: Using IsEmpty property (if available) - should not be detected
        ' Note: VB.NET doesn't have IsEmpty on most collections, but if it did:
        ' If numbers.IsEmpty Then
        '     Console.WriteLine("Numbers list is empty")
        ' End If
        
    End Sub
    
End Class
