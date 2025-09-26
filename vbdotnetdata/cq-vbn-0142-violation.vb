' VB.NET test file for cq-vbn-0142: Use StartsWith instead of IndexOf
' Rule: It's more efficient to call String.StartsWith than to call String.IndexOf to check whether a string starts with a given prefix.

Imports System

Public Class StartsWithIndexOfExamples
    
    Public Sub TestBasicIndexOfViolations()
        Dim text As String = "Hello World"
        Dim filename As String = "document.pdf"
        Dim url As String = "https://example.com"
        
        ' Violation: Using IndexOf to check if string starts with prefix
        If text.IndexOf("Hello") = 0 Then
            Console.WriteLine("Text starts with Hello")
        End If
        
        ' Violation: IndexOf with comparison to 0
        If filename.IndexOf("doc") = 0 Then
            Console.WriteLine("Filename starts with doc")
        End If
        
        ' Violation: Using IndexOf with StringComparison
        If url.IndexOf("https", StringComparison.OrdinalIgnoreCase) = 0 Then
            Console.WriteLine("URL starts with https")
        End If
        
        ' Violation: IndexOf in conditional expression
        Dim startsWithPrefix As Boolean = text.IndexOf("Hel") = 0
        
        ' Violation: IndexOf with variable prefix
        Dim prefix As String = "Hello"
        If text.IndexOf(prefix) = 0 Then
            Console.WriteLine("Starts with variable prefix")
        End If
    End Sub
    
    Public Sub TestIndexOfComparisonVariations()
        Dim input As String = "Example text"
        Dim path As String = "C:\temp\file.txt"
        
        ' Violation: IndexOf equals 0
        If input.IndexOf("Ex") = 0 Then
            Console.WriteLine("Starts with Ex")
        End If
        
        ' Violation: 0 equals IndexOf
        If 0 = input.IndexOf("Exam") Then
            Console.WriteLine("Starts with Exam")
        End If
        
        ' Violation: IndexOf comparison with parentheses
        If (path.IndexOf("C:\")) = 0 Then
            Console.WriteLine("Starts with C:\")
        End If
        
        ' Violation: IndexOf in complex expression
        If input.IndexOf("Example") = 0 AndAlso input.Length > 5 Then
            Console.WriteLine("Starts with Example and is long enough")
        End If
        
        ' Violation: IndexOf with negation check
        If Not (input.IndexOf("Test") = 0) Then
            Console.WriteLine("Does not start with Test")
        End If
    End Sub
    
    Public Sub TestIndexOfWithDifferentOverloads()
        Dim data As String = "Sample data string"
        Dim content As String = "Content to analyze"
        
        ' Violation: IndexOf with starting position 0
        If data.IndexOf("Sam", 0) = 0 Then
            Console.WriteLine("Starts with Sam")
        End If
        
        ' Violation: IndexOf with StringComparison parameter
        If content.IndexOf("content", StringComparison.CurrentCultureIgnoreCase) = 0 Then
            Console.WriteLine("Starts with content (case insensitive)")
        End If
        
        ' Violation: IndexOf with starting position and StringComparison
        If data.IndexOf("sample", 0, StringComparison.OrdinalIgnoreCase) = 0 Then
            Console.WriteLine("Starts with sample (case insensitive)")
        End If
    End Sub
    
    Public Sub TestIndexOfInLoops()
        Dim texts() As String = {"Hello world", "Hi there", "Hey you", "Howdy partner"}
        Dim prefixes() As String = {"He", "Hi", "Ho"}
        
        ' Violation: IndexOf in For Each loop
        For Each text In texts
            If text.IndexOf("H") = 0 Then
                Console.WriteLine($"Text starts with H: {text}")
            End If
        Next
        
        ' Violation: IndexOf with multiple prefixes
        For Each prefix In prefixes
            For Each text In texts
                If text.IndexOf(prefix) = 0 Then
                    Console.WriteLine($"'{text}' starts with '{prefix}'")
                End If
            Next
        Next
        
        ' Violation: IndexOf in traditional For loop
        For i As Integer = 0 To texts.Length - 1
            If texts(i).IndexOf("Hello") = 0 Then
                Console.WriteLine($"Item {i} starts with Hello")
            End If
        Next
    End Sub
    
    Public Sub TestIndexOfInMethods()
        Dim documents() As String = {"readme.txt", "license.md", "changelog.log"}
        
        ' Violation: IndexOf in method calls
        For Each doc In documents
            If CheckPrefix(doc, "read") Then
                Console.WriteLine($"Document {doc} starts with 'read'")
            End If
        Next
        
        ' Violation: IndexOf in lambda-like expression
        Dim hasPrefix As Func(Of String, String, Boolean) = Function(text, prefix) text.IndexOf(prefix) = 0
        
        If hasPrefix("test string", "test") Then
            Console.WriteLine("Lambda detected prefix")
        End If
    End Sub
    
    Private Function CheckPrefix(text As String, prefix As String) As Boolean
        ' Violation: IndexOf in helper method
        Return text.IndexOf(prefix) = 0
    End Function
    
    Public Sub TestIndexOfWithVariables()
        Dim message As String = "Important message"
        Dim searchTerm As String = "Import"
        Dim comparison As StringComparison = StringComparison.Ordinal
        
        ' Violation: IndexOf with variable comparison type
        If message.IndexOf(searchTerm, comparison) = 0 Then
            Console.WriteLine("Message starts with search term")
        End If
        
        ' Violation: IndexOf result stored in variable
        Dim indexResult As Integer = message.IndexOf("Imp")
        If indexResult = 0 Then
            Console.WriteLine("Stored index check")
        End If
        
        ' Violation: IndexOf in conditional assignment
        Dim startsWithImp As Boolean = If(message.IndexOf("Imp") = 0, True, False)
    End Sub
    
    Public Sub TestIndexOfInComplexExpressions()
        Dim input As String = "Processing data"
        Dim alternative As String = "Alternative text"
        
        ' Violation: IndexOf in ternary-like expression
        Dim result As String = If(input.IndexOf("Proc") = 0, "Starts with Proc", "Does not start")
        
        ' Violation: IndexOf in compound condition
        If input.IndexOf("Process") = 0 OrElse alternative.IndexOf("Alt") = 0 Then
            Console.WriteLine("One of the strings starts with expected prefix")
        End If
        
        ' Violation: IndexOf with method chaining
        If input.Trim().IndexOf("Processing") = 0 Then
            Console.WriteLine("Trimmed input starts with Processing")
        End If
        
        ' Violation: IndexOf with ToLower/ToUpper
        If input.ToLower().IndexOf("processing") = 0 Then
            Console.WriteLine("Lowercase input starts with processing")
        End If
    End Sub
    
    Public Sub TestIndexOfEdgeCases()
        Dim text As String = "Edge case testing"
        Dim emptyPrefix As String = ""
        Dim nullableText As String = Nothing
        
        ' Violation: IndexOf with empty string
        If text.IndexOf(emptyPrefix) = 0 Then
            Console.WriteLine("Starts with empty string")
        End If
        
        ' Violation: IndexOf with single character
        If text.IndexOf("E") = 0 Then
            Console.WriteLine("Starts with E")
        End If
        
        ' Violation: IndexOf with whitespace
        If text.IndexOf(" ") = 0 Then
            Console.WriteLine("Starts with space")
        End If
        
        ' Violation: IndexOf in null check context
        If nullableText IsNot Nothing AndAlso nullableText.IndexOf("test") = 0 Then
            Console.WriteLine("Non-null text starts with test")
        End If
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        Dim text As String = "Hello World"
        
        ' Correct: Using StartsWith
        If text.StartsWith("Hello") Then
            Console.WriteLine("Text starts with Hello")
        End If
        
        ' Correct: Using StartsWith with StringComparison
        If text.StartsWith("hello", StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Text starts with hello (case insensitive)")
        End If
        
        ' Correct: Using IndexOf for positions other than 0
        If text.IndexOf("World") > 0 Then
            Console.WriteLine("World found at position > 0")
        End If
        
        ' Correct: Using IndexOf to check if substring exists anywhere
        If text.IndexOf("llo") >= 0 Then
            Console.WriteLine("'llo' found somewhere in text")
        End If
    End Sub
    
    Public Sub TestMoreIndexOfViolations()
        Dim header As String = "HTTP/1.1 200 OK"
        Dim command As String = "GET /api/users"
        
        ' Violation: Protocol checking
        If header.IndexOf("HTTP") = 0 Then
            Console.WriteLine("Valid HTTP header")
        End If
        
        ' Violation: Command parsing
        If command.IndexOf("GET") = 0 Then
            Console.WriteLine("GET request detected")
        End If
        
        ' Violation: File extension checking
        Dim files() As String = {"image.jpg", "document.pdf", "script.js"}
        For Each file In files
            If file.IndexOf("image") = 0 Then
                Console.WriteLine($"Image file: {file}")
            End If
        Next
    End Sub
End Class
