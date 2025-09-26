' Test file for cq-vbn-0053: Specify StringComparison for clarity
' Rule should detect string comparison methods without StringComparison parameter

Imports System

Public Class StringComparisonExamples
    
    Public Sub CompareStrings()
        Dim text1 As String = "Hello"
        Dim text2 As String = "hello"
        
        ' Violation 1: Equals without StringComparison
        Dim isEqual = text1.Equals(text2)
        
        ' Violation 2: CompareTo without StringComparison
        Dim comparison = text1.CompareTo(text2)
        
        ' Violation 3: StartsWith without StringComparison
        Dim startsWith = text1.StartsWith("He")
        
        ' Violation 4: EndsWith without StringComparison
        Dim endsWith = text1.EndsWith("lo")
        
        ' This should NOT be detected - proper StringComparison usage
        Dim properEquals = text1.Equals(text2, StringComparison.OrdinalIgnoreCase)
        Dim properStartsWith = text1.StartsWith("He", StringComparison.Ordinal)
        
    End Sub
    
    Public Sub SearchStrings()
        Dim text As String = "Hello World"
        Dim searchTerm As String = "world"
        
        ' Violation 5: IndexOf without StringComparison
        Dim index = text.IndexOf(searchTerm)
        
        ' Violation 6: Contains without StringComparison
        Dim contains = text.Contains(searchTerm)
        
        ' This should NOT be detected - proper StringComparison usage
        Dim properIndex = text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase)
        
        ' This should NOT be detected - Contains with char parameter (no StringComparison overload)
        Dim containsChar = text.Contains("W"c)
        
    End Sub
    
    Public Sub ProcessUserInput(input As String)
        Dim expectedValue As String = "Expected"
        
        ' Violation 7: Equals in conditional
        If input.Equals(expectedValue) Then
            Console.WriteLine("Match found")
        End If
        
        ' Violation 8: StartsWith in conditional
        If input.StartsWith("prefix") Then
            Console.WriteLine("Has prefix")
        End If
        
        ' This should NOT be detected - proper usage
        If input.Equals(expectedValue, StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Case-insensitive match")
        End If
        
    End Sub
    
    Public Function FindText(source As String, target As String) As Boolean
        
        ' Violation 9: Contains in return statement
        Return source.Contains(target)
        
    End Function
    
    Public Sub StringArrayProcessing()
        Dim items() As String = {"Apple", "Banana", "Cherry"}
        Dim searchItem As String = "apple"
        
        For Each item In items
            ' Violation 10: Equals in loop
            If item.Equals(searchItem) Then
                Console.WriteLine("Found: " & item)
            End If
            
            ' Violation 11: StartsWith in loop
            If item.StartsWith("A") Then
                Console.WriteLine("Starts with A: " & item)
            End If
        Next
        
        ' This should NOT be detected - proper usage in loop
        For Each item In items
            If item.Equals(searchItem, StringComparison.OrdinalIgnoreCase) Then
                Console.WriteLine("Case-insensitive match: " & item)
            End If
        Next
        
    End Sub
    
End Class

Public Class MoreStringExamples
    
    Public Sub ValidateInput(input As String)
        
        ' Violation 12: EndsWith without StringComparison
        If input.EndsWith(".txt") Then
            Console.WriteLine("Text file detected")
        End If
        
        ' Violation 13: CompareTo in sorting logic
        Dim result = input.CompareTo("reference")
        
        ' This should NOT be detected - Length property (not a comparison method)
        Dim length = input.Length
        
        ' This should NOT be detected - Substring method (not a comparison method)
        Dim substring = input.Substring(0, 5)
        
    End Sub
    
End Class
