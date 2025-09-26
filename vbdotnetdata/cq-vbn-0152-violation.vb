' Test file for cq-vbn-0152: Use a cached 'SearchValues' instance
' Using a cached SearchValues<T> instance is more efficient than passing values to 'IndexOfAny' or 'ContainsAny' directly

Imports System
Imports System.Buffers

Public Class SearchValuesTests
    
    ' Violation: Creating SearchValues inline in IndexOfAny
    Public Sub SearchWithInlineValues()
        Dim text As String = "Hello World"
        
        ' Violation: Inline array creation for IndexOfAny
        Dim index As Integer = text.IndexOfAny({"a"c, "e"c, "i"c, "o"c, "u"c})
        
        If index >= 0 Then
            Console.WriteLine($"Found vowel at index: {index}")
        End If
    End Sub
    
    ' Violation: Creating SearchValues with New keyword
    Public Sub SearchWithNewSearchValues()
        Dim text As String = "Testing search functionality"
        
        ' Violation: Creating SearchValues instance inline
        Dim index As Integer = text.IndexOfAny(New Char() {"x"c, "y"c, "z"c})
        
        Console.WriteLine($"Index: {index}")
    End Sub
    
    ' Violation: ContainsAny with inline array
    Public Sub CheckContainsWithInlineArray()
        Dim input As String = "Sample text for testing"
        
        ' Violation: Inline array for ContainsAny
        Dim hasSpecialChars As Boolean = input.ContainsAny({"@"c, "#"c, "$"c, "%"c})
        
        Console.WriteLine($"Has special chars: {hasSpecialChars}")
    End Sub
    
    ' Violation: Multiple IndexOfAny calls with same values
    Public Sub MultipleSearchesWithSameValues()
        Dim texts() As String = {"first", "second", "third"}
        
        For Each text In texts
            ' Violation: Repeated inline array creation
            Dim index As Integer = text.IndexOfAny({"a"c, "e"c, "i"c, "o"c, "u"c})
            Console.WriteLine($"Text: {text}, Vowel index: {index}")
        Next
    End Sub
    
    ' Violation: SearchValues with complex array initialization
    Public Sub SearchWithComplexArray()
        Dim content As String = "Complex search pattern testing"
        
        ' Violation: Complex inline array
        Dim separators As Char() = {" "c, ","c, "."c, ";"c, ":"c}
        Dim index As Integer = content.IndexOfAny(separators)
        
        Console.WriteLine($"Separator found at: {index}")
    End Sub
    
    ' Violation: ContainsAny in loop with inline values
    Public Sub CheckMultipleStrings()
        Dim strings() As String = {"test1", "test2", "test3"}
        
        For Each str In strings
            ' Violation: Inline array in loop
            If str.ContainsAny({"1"c, "2"c, "3"c, "4"c, "5"c}) Then
                Console.WriteLine($"String {str} contains digits")
            End If
        Next
    End Sub
    
    ' Good practice: Using cached SearchValues (should not be detected)
    Private Shared ReadOnly VowelSearchValues As SearchValues(Of Char) = SearchValues.Create("aeiouAEIOU"c)
    Private Shared ReadOnly DigitSearchValues As SearchValues(Of Char) = SearchValues.Create("0123456789"c)
    
    ' Good: Using cached SearchValues
    Public Sub SearchWithCachedValues()
        Dim text As String = "Hello World"
        Dim index As Integer = text.IndexOfAny(VowelSearchValues)
        Console.WriteLine($"Vowel found at: {index}")
    End Sub
    
    ' Good: Using string methods without SearchValues
    Public Sub SimpleStringSearch()
        Dim text As String = "Simple search"
        Dim index As Integer = text.IndexOf("search")
        Console.WriteLine($"Found at: {index}")
    End Sub
    
    ' Violation: IndexOfAny with byte array
    Public Sub SearchInByteArray()
        Dim data() As Byte = {65, 66, 67, 68, 69}
        
        ' Violation: Inline byte array for IndexOfAny
        Dim index As Integer = Array.IndexOf(data, New Byte() {67, 68})
        
        Console.WriteLine($"Found at: {index}")
    End Sub
    
    ' Violation: SearchValues creation in method
    Public Function FindSpecialCharacters(input As String) As Integer
        ' Violation: Local SearchValues creation
        Dim specialChars As New SearchValues(Of Char)({"!", "@", "#", "$", "%"})
        
        Return input.IndexOfAny(specialChars)
    End Function
    
    ' Violation: ContainsAny with string array
    Public Sub CheckForKeywords()
        Dim text As String = "This is a sample document"
        
        ' Violation: Inline string array
        Dim hasKeywords As Boolean = text.ContainsAny({"sample", "test", "demo"})
        
        Console.WriteLine($"Contains keywords: {hasKeywords}")
    End Sub
    
End Class

' Additional test cases
Public Module SearchUtilities
    
    ' Violation: Utility method with inline SearchValues
    Public Function ContainsAnyVowel(text As String) As Boolean
        ' Violation: Creating SearchValues in utility
        Return text.ContainsAny(New Char() {"a"c, "e"c, "i"c, "o"c, "u"c})
    End Function
    
    ' Violation: Multiple SearchValues creations
    Public Sub AnalyzeText(input As String)
        ' Violation: First inline array
        Dim vowelIndex As Integer = input.IndexOfAny({"a"c, "e"c, "i"c, "o"c, "u"c})
        
        ' Violation: Second inline array
        Dim consonantIndex As Integer = input.IndexOfAny({"b"c, "c"c, "d"c, "f"c, "g"c})
        
        Console.WriteLine($"Vowel: {vowelIndex}, Consonant: {consonantIndex}")
    End Sub
    
    ' Violation: SearchValues in conditional
    Public Sub ProcessBasedOnContent(data As String)
        If data.Length > 0 Then
            ' Violation: Inline array in conditional
            Dim hasNumbers As Boolean = data.ContainsAny({"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c})
            Console.WriteLine($"Contains numbers: {hasNumbers}")
        End If
    End Sub
    
End Module
