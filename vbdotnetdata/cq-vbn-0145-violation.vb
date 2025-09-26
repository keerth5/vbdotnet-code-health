' VB.NET test file for cq-vbn-0145: Avoid constant arrays as arguments
' Rule: Constant arrays passed as arguments are not reused which implies a performance overhead. Consider extracting them to 'static readonly' fields to improve performance.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class ConstantArrayArgumentsExamples
    
    Public Sub TestBasicConstantArrays()
        ' Violation: Constant array as method argument
        ProcessNumbers({1, 2, 3, 4, 5})
        
        ' Violation: Constant string array
        ProcessStrings({"apple", "banana", "cherry"})
        
        ' Violation: Constant byte array
        ProcessBytes({65, 66, 67, 68})
        
        ' Violation: Constant boolean array
        ProcessBooleans({True, False, True, True})
        
        ' Violation: Constant double array
        ProcessDoubles({1.1, 2.2, 3.3, 4.4})
    End Sub
    
    Private Sub ProcessNumbers(numbers() As Integer)
        For Each num In numbers
            Console.WriteLine($"Number: {num}")
        Next
    End Sub
    
    Private Sub ProcessStrings(strings() As String)
        For Each str In strings
            Console.WriteLine($"String: {str}")
        Next
    End Sub
    
    Private Sub ProcessBytes(bytes() As Byte)
        Console.WriteLine($"Byte array length: {bytes.Length}")
    End Sub
    
    Private Sub ProcessBooleans(booleans() As Boolean)
        Console.WriteLine($"Boolean array length: {booleans.Length}")
    End Sub
    
    Private Sub ProcessDoubles(doubles() As Double)
        For Each d In doubles
            Console.WriteLine($"Double: {d}")
        Next
    End Sub
    
    Public Sub TestConstantArraysInLoops()
        ' Violation: Constant array created in loop (very inefficient)
        For i As Integer = 1 To 5
            ProcessNumbers({10, 20, 30, 40, 50})
        Next
        
        ' Violation: Constant array in For Each loop
        Dim items() As String = {"a", "b", "c"}
        For Each item In items
            ProcessStrings({"prefix", item, "suffix"})
        Next
        
        ' Violation: Constant array in While loop
        Dim counter As Integer = 0
        While counter < 3
            ProcessDoubles({1.0, 2.0, 3.0})
            counter += 1
        End While
    End Sub
    
    Public Sub TestConstantArraysWithMethodChaining()
        ' Violation: Constant array with LINQ methods
        Dim result1 = ProcessAndFilter({1, 2, 3, 4, 5}, Function(x) x > 2)
        Dim result2 = ProcessAndTransform({"hello", "world"}, Function(s) s.ToUpper())
        
        Console.WriteLine($"Filtered result count: {result1.Count()}")
        Console.WriteLine($"Transformed result count: {result2.Count()}")
    End Sub
    
    Private Function ProcessAndFilter(numbers() As Integer, predicate As Func(Of Integer, Boolean)) As IEnumerable(Of Integer)
        Return numbers.Where(predicate)
    End Function
    
    Private Function ProcessAndTransform(strings() As String, transform As Func(Of String, String)) As IEnumerable(Of String)
        Return strings.Select(transform)
    End Function
    
    Public Sub TestConstantArraysInConditionals()
        Dim condition As Boolean = True
        
        ' Violation: Constant arrays in conditional branches
        If condition Then
            ProcessNumbers({100, 200, 300})
        Else
            ProcessNumbers({400, 500, 600})
        End If
        
        ' Violation: Constant array in Select Case
        Dim mode As Integer = 1
        Select Case mode
            Case 1
                ProcessStrings({"mode1", "option1", "setting1"})
            Case 2
                ProcessStrings({"mode2", "option2", "setting2"})
            Case Else
                ProcessStrings({"default", "option", "setting"})
        End Select
    End Sub
    
    Public Sub TestConstantArraysWithDifferentSyntax()
        ' Violation: Using New keyword with constant array
        ProcessNumbers(New Integer() {7, 8, 9})
        
        ' Violation: Explicit array type declaration
        ProcessStrings(New String() {"red", "green", "blue"})
        
        ' Violation: Mixed value constant array
        ProcessDoubles(New Double() {0.1, 0.2, 0.3, 0.4})
        
        ' Violation: Single element constant array
        ProcessNumbers({42})
        
        ' Violation: Empty constant array
        ProcessStrings({})
    End Sub
    
    Public Sub TestConstantArraysInMethodCalls()
        ' Violation: Nested method calls with constant arrays
        Dim max1 = FindMaximum({10, 5, 15, 3, 20})
        Dim max2 = FindMaximum({100, 50, 75})
        
        Console.WriteLine($"Max1: {max1}, Max2: {max2}")
        
        ' Violation: Constant array in constructor
        Dim list1 As New List(Of Integer)({1, 2, 3, 4})
        Dim list2 As New List(Of String)({"a", "b", "c"})
        
        Console.WriteLine($"List1 count: {list1.Count}, List2 count: {list2.Count}")
    End Sub
    
    Private Function FindMaximum(numbers() As Integer) As Integer
        If numbers.Length = 0 Then Return 0
        Return numbers.Max()
    End Function
    
    Public Sub TestConstantArraysWithVariousTypes()
        ' Violation: Constant arrays of different types
        ProcessChars({"A"c, "B"c, "C"c})
        ProcessDecimals({10.5D, 20.7D, 30.9D})
        ProcessLongs({1000L, 2000L, 3000L})
        ProcessShorts({1S, 2S, 3S})
        ProcessSBytes({-1, -2, -3})
        ProcessUIntegers({1UI, 2UI, 3UI})
        ProcessULongs({1UL, 2UL, 3UL})
        ProcessUShorts({1US, 2US, 3US})
    End Sub
    
    Private Sub ProcessChars(chars() As Char)
        Console.WriteLine($"Char array: {String.Join(",", chars)}")
    End Sub
    
    Private Sub ProcessDecimals(decimals() As Decimal)
        Console.WriteLine($"Decimal array length: {decimals.Length}")
    End Sub
    
    Private Sub ProcessLongs(longs() As Long)
        Console.WriteLine($"Long array length: {longs.Length}")
    End Sub
    
    Private Sub ProcessShorts(shorts() As Short)
        Console.WriteLine($"Short array length: {shorts.Length}")
    End Sub
    
    Private Sub ProcessSBytes(sbytes() As SByte)
        Console.WriteLine($"SByte array length: {sbytes.Length}")
    End Sub
    
    Private Sub ProcessUIntegers(uints() As UInteger)
        Console.WriteLine($"UInteger array length: {uints.Length}")
    End Sub
    
    Private Sub ProcessULongs(ulongs() As ULong)
        Console.WriteLine($"ULong array length: {ulongs.Length}")
    End Sub
    
    Private Sub ProcessUShorts(ushorts() As UShort)
        Console.WriteLine($"UShort array length: {ushorts.Length}")
    End Sub
    
    Public Sub TestConstantArraysInComplexExpressions()
        ' Violation: Constant arrays in complex expressions
        Dim result1 As Boolean = CheckAllPositive({1, 2, 3, 4, 5})
        Dim result2 As Boolean = ContainsValue({10, 20, 30}, 20)
        Dim result3 As Integer = SumArray({5, 10, 15, 20})
        
        Console.WriteLine($"All positive: {result1}")
        Console.WriteLine($"Contains value: {result2}")
        Console.WriteLine($"Sum: {result3}")
        
        ' Violation: Constant array in ternary expression
        Dim data() As Integer = If(result1, {1, 2, 3}, {-1, -2, -3})
        ProcessNumbers(data)
    End Sub
    
    Private Function CheckAllPositive(numbers() As Integer) As Boolean
        Return numbers.All(Function(x) x > 0)
    End Function
    
    Private Function ContainsValue(numbers() As Integer, value As Integer) As Boolean
        Return numbers.Contains(value)
    End Function
    
    Private Function SumArray(numbers() As Integer) As Integer
        Return numbers.Sum()
    End Function
    
    Public Sub TestConstantArraysWithStringInterpolation()
        Dim name As String = "Test"
        
        ' Violation: Constant arrays in string interpolation context
        Console.WriteLine($"Processing: {String.Join(",", {1, 2, 3})}")
        Console.WriteLine($"Items: {String.Join("|", {"a", "b", "c"})}")
        
        ' Violation: Constant array in method call within interpolation
        Console.WriteLine($"Max value: {FindMaximum({10, 20, 30, 40})}")
    End Sub
    
    Public Sub TestConstantArraysInDelegates()
        ' Violation: Constant arrays in delegate/lambda expressions
        Dim processor As Action(Of Integer()) = Sub(arr) ProcessNumbers(arr)
        processor({1, 2, 3, 4})
        
        Dim transformer As Func(Of String(), String) = Function(arr) String.Join(",", arr)
        Dim result As String = transformer({"x", "y", "z"})
        Console.WriteLine($"Transformed: {result}")
    End Sub
    
    ' Examples of correct usage (for reference)
    Private Shared ReadOnly ConstantNumbers() As Integer = {1, 2, 3, 4, 5}
    Private Shared ReadOnly ConstantStrings() As String = {"apple", "banana", "cherry"}
    Private Shared ReadOnly ConstantDoubles() As Double = {1.1, 2.2, 3.3}
    
    Public Sub TestCorrectUsage()
        ' Correct: Using static readonly arrays
        ProcessNumbers(ConstantNumbers)
        ProcessStrings(ConstantStrings)
        ProcessDoubles(ConstantDoubles)
        
        ' Correct: Using variables
        Dim dynamicNumbers() As Integer = {1, 2, 3}
        ProcessNumbers(dynamicNumbers)
        
        ' Correct: Arrays that are actually dynamic
        Dim userInput As String = "1,2,3,4"
        Dim parsedNumbers() As Integer = userInput.Split(","c).Select(Function(s) Integer.Parse(s)).ToArray()
        ProcessNumbers(parsedNumbers)
    End Sub
    
    Public Sub TestMoreConstantArrayViolations()
        ' Violation: Constant arrays in property access
        Dim obj As New TestClass()
        obj.SetData({10, 20, 30, 40})
        
        ' Violation: Constant arrays in indexer access
        Dim lookup As New Dictionary(Of String, Integer())()
        lookup("key1") = {1, 2, 3}
        lookup("key2") = {4, 5, 6}
        
        ' Violation: Constant arrays in event handler
        ' AddHandler SomeEvent, Sub() ProcessNumbers({100, 200, 300})
    End Sub
    
    Public Class TestClass
        Public Sub SetData(data() As Integer)
            Console.WriteLine($"Set data with {data.Length} elements")
        End Sub
    End Class
    
    Public Sub TestConstantArraysInGenericMethods()
        ' Violation: Constant arrays with generic methods
        ProcessGenericArray({1, 2, 3})
        ProcessGenericArray({"a", "b", "c"})
        ProcessGenericArray({True, False, True})
        
        ' Violation: Constant arrays in generic method with constraints
        ProcessComparableArray({10, 5, 15, 3})
        ProcessComparableArray({"zebra", "apple", "banana"})
    End Sub
    
    Private Sub ProcessGenericArray(Of T)(items() As T)
        Console.WriteLine($"Processing {items.Length} items of type {GetType(T).Name}")
    End Sub
    
    Private Sub ProcessComparableArray(Of T As IComparable(Of T))(items() As T)
        Array.Sort(items)
        Console.WriteLine($"Sorted {items.Length} comparable items")
    End Sub
End Class
