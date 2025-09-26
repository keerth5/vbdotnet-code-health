' Test file for cq-vbn-0056: Specify CultureInfo for StringComparison
' Rule should detect String.Compare calls that need CultureInfo specification

Imports System
Imports System.Globalization

Public Class StringCompareExamples
    
    Public Sub BasicStringComparison()
        Dim text1 As String = "Hello"
        Dim text2 As String = "World"
        
        ' Violation 1: String.Compare without CultureInfo
        Dim result1 = String.Compare(text1, text2)
        
        ' Violation 2: String.Compare with case sensitivity but no CultureInfo
        Dim result2 = String.Compare(text1, text2, True)
        
        ' Violation 3: String.Compare with case insensitivity but no CultureInfo
        Dim result3 = String.Compare(text1, text2, False)
        
        ' This should NOT be detected - proper CultureInfo usage
        Dim properResult1 = String.Compare(text1, text2, CultureInfo.CurrentCulture, CompareOptions.None)
        Dim properResult2 = String.Compare(text1, text2, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase)
        
    End Sub
    
    Public Sub SortingComparison()
        Dim names() As String = {"Alice", "Bob", "Charlie"}
        
        ' Violation 4: String.Compare in sorting without CultureInfo
        Array.Sort(names, Function(x, y) String.Compare(x, y))
        
        ' This should NOT be detected - proper CultureInfo usage in sorting
        Array.Sort(names, Function(x, y) String.Compare(x, y, CultureInfo.CurrentCulture, CompareOptions.None))
        
    End Sub
    
    Public Sub ConditionalComparison(input1 As String, input2 As String)
        
        ' Violation 5: String.Compare in conditional
        If String.Compare(input1, input2) = 0 Then
            Console.WriteLine("Strings are equal")
        End If
        
        ' Violation 6: String.Compare with case sensitivity in conditional
        If String.Compare(input1, input2, False) > 0 Then
            Console.WriteLine("First string is greater")
        End If
        
        ' This should NOT be detected - proper CultureInfo usage
        If String.Compare(input1, input2, CultureInfo.InvariantCulture, CompareOptions.None) = 0 Then
            Console.WriteLine("Strings are equal (invariant)")
        End If
        
    End Sub
    
    Public Function CompareUserInput(userInput As String, expectedValue As String) As Integer
        
        ' Violation 7: String.Compare in return statement
        Return String.Compare(userInput, expectedValue)
        
    End Function
    
    Public Sub ProcessTextData()
        Dim data1 As String = "Data1"
        Dim data2 As String = "data1"
        
        ' Violation 8: String.Compare with case insensitive comparison
        Dim comparison = String.Compare(data1, data2, True)
        
        ' This should NOT be detected - using StringComparison enum instead
        Dim properComparison = String.Compare(data1, data2, StringComparison.OrdinalIgnoreCase)
        
    End Sub
    
    Public Sub DatabaseStringComparison()
        Dim dbValue As String = "DatabaseValue"
        Dim searchValue As String = "databasevalue"
        
        ' Violation 9: String.Compare for database comparison
        If String.Compare(dbValue, searchValue, True) = 0 Then
            Console.WriteLine("Database match found")
        End If
        
        ' This should NOT be detected - proper CultureInfo specification
        If String.Compare(dbValue, searchValue, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) = 0 Then
            Console.WriteLine("Database match found (invariant)")
        End If
        
    End Sub
    
End Class

Public Class AdvancedComparisonExamples
    
    Public Sub MultipleComparisons()
        Dim values() As String = {"Apple", "Banana", "Cherry"}
        Dim target As String = "banana"
        
        For Each value In values
            ' Violation 10: String.Compare in loop
            If String.Compare(value, target, True) = 0 Then
                Console.WriteLine("Found: " & value)
            End If
        Next
        
        ' This should NOT be detected - proper CultureInfo usage
        For Each value In values
            If String.Compare(value, target, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) = 0 Then
                Console.WriteLine("Found (culture-aware): " & value)
            End If
        Next
        
    End Sub
    
    Public Sub ValidationComparison(input As String)
        Dim validValues() As String = {"Yes", "No", "Maybe"}
        
        For Each validValue In validValues
            ' Violation 11: String.Compare for validation
            If String.Compare(input, validValue, False) = 0 Then
                Console.WriteLine("Valid input: " & input)
                Exit For
            End If
        Next
        
    End Sub
    
End Class
