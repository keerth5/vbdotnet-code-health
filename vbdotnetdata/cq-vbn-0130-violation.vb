' VB.NET test file for cq-vbn-0130: Prefer AsSpan over Substring
' Rule: AsSpan is more efficient than Substring. Substring performs an O(n) string copy, while AsSpan does not and has a constant cost. AsSpan also does not perform any heap allocations.

Imports System

Public Class SubstringVsAsSpanExamples
    
    Public Sub TestSubstringViolations()
        
        Dim text As String = "Hello World Example Text"
        
        ' Violation: Using Substring for getting part of string
        Dim part1 = text.Substring(0, 5)
        
        ' Violation: Using Substring with start index only
        Dim part2 = text.Substring(6)
        
        ' Violation: Using Substring in variable assignment
        Dim greeting As String = text.Substring(0, 11)
        
        ' Violation: Using Substring in method parameter
        ProcessText(text.Substring(12))
        
        ' Violation: Using Substring in string comparison
        If text.Substring(0, 5) = "Hello" Then
            Console.WriteLine("Text starts with Hello")
        End If
        
        Console.WriteLine($"Part 1: {part1}")
        Console.WriteLine($"Part 2: {part2}")
        Console.WriteLine($"Greeting: {greeting}")
    End Sub
    
    Public Sub TestSubstringInMethods()
        
        Dim data As String = "Important Data Content"
        
        ' Violation: Using Substring in method call
        Dim length = GetTextLength(data.Substring(0, 9))
        
        ' Violation: Using Substring in return statement
        Dim result = ExtractData(data.Substring(10, 4))
        
        Console.WriteLine($"Length: {length}, Result: {result}")
    End Sub
    
    Public Function ExtractData(input As String) As String
        
        ' Violation: Using Substring in function body
        Return input.Substring(0, 2) + "..."
        
    End Function
    
    Public Sub TestSubstringInConditionals()
        
        Dim message As String = "Error: File not found"
        
        ' Violation: Using Substring in conditional expression
        If message.Substring(0, 5) = "Error" Then
            ' Violation: Using Substring in nested condition
            If message.Substring(7) = "File not found" Then
                Console.WriteLine("File error detected")
            End If
        End If
        
        ' Violation: Using Substring in conditional assignment
        Dim isWarning = message.Substring(0, 7) = "Warning"
        
        Console.WriteLine($"Is warning: {isWarning}")
    End Sub
    
    Public Sub TestSubstringInLoops()
        
        Dim sentences() As String = {
            "First sentence example",
            "Second sentence example",
            "Third sentence example"
        }
        
        For Each sentence In sentences
            ' Violation: Using Substring in loop body
            Dim firstWord = sentence.Substring(0, sentence.IndexOf(" "))
            
            ' Violation: Using Substring for remaining text
            Dim remaining = sentence.Substring(sentence.IndexOf(" ") + 1)
            
            Console.WriteLine($"First word: {firstWord}, Remaining: {remaining}")
        Next
        
    End Sub
    
    Public Sub TestSubstringWithStringOperations()
        
        Dim input As String = "Processing String Operations"
        
        ' Violation: Using Substring with ToUpper
        Dim upperPart = input.Substring(0, 10).ToUpper()
        
        ' Violation: Using Substring with ToLower
        Dim lowerPart = input.Substring(11).ToLower()
        
        ' Violation: Using Substring with Trim
        Dim trimmedPart = input.Substring(5, 15).Trim()
        
        ' Violation: Using Substring with Contains
        Dim containsString = input.Substring(0, 10).Contains("Process")
        
        Console.WriteLine($"Upper: {upperPart}")
        Console.WriteLine($"Lower: {lowerPart}")
        Console.WriteLine($"Trimmed: {trimmedPart}")
        Console.WriteLine($"Contains: {containsString}")
    End Sub
    
    Public Sub TestSubstringInStringInterpolation()
        
        Dim fullText As String = "Complete Example Text String"
        
        ' Violation: Using Substring in string interpolation
        Dim formatted = $"Start: {fullText.Substring(0, 8)}, End: {fullText.Substring(20)}"
        
        ' Violation: Using Substring in multiple interpolations
        Console.WriteLine($"First part: {fullText.Substring(0, 8)}")
        Console.WriteLine($"Middle part: {fullText.Substring(9, 7)}")
        Console.WriteLine($"Last part: {fullText.Substring(17)}")
        
        Console.WriteLine(formatted)
    End Sub
    
    Public Sub TestComplexSubstringViolations()
        
        Dim complexText As String = "Complex String Manipulation and Processing Example"
        
        ' Violation: Using Substring with mathematical operations
        Dim midPoint = complexText.Length \ 2
        Dim firstHalf = complexText.Substring(0, midPoint)
        Dim secondHalf = complexText.Substring(midPoint)
        
        ' Violation: Using Substring in nested method calls
        Dim processedFirst = ProcessAndTransform(complexText.Substring(0, 15))
        Dim processedSecond = ProcessAndTransform(complexText.Substring(16, 20))
        
        ' Violation: Using Substring with exception handling
        Try
            Dim safePart = complexText.Substring(0, Math.Min(10, complexText.Length))
            Console.WriteLine($"Safe part: {safePart}")
        Catch ex As ArgumentOutOfRangeException
            Console.WriteLine("Substring operation failed")
        End Try
        
        Console.WriteLine($"First half: {firstHalf}")
        Console.WriteLine($"Second half: {secondHalf}")
        Console.WriteLine($"Processed first: {processedFirst}")
        Console.WriteLine($"Processed second: {processedSecond}")
    End Sub
    
    Private Function GetTextLength(text As String) As Integer
        Return text.Length
    End Function
    
    Private Sub ProcessText(text As String)
        Console.WriteLine($"Processing: {text}")
    End Sub
    
    Private Function ProcessAndTransform(input As String) As String
        Return input.ToUpper().Replace(" ", "_")
    End Function
    
End Class

' More violation examples

Public Class AdditionalSubstringExamples
    
    Public Sub TestSubstringInCollections()
        
        Dim baseText As String = "Collection Item Text"
        
        ' Violation: Using Substring in List initialization
        Dim items As New List(Of String) From {
            baseText.Substring(0, 10),
            baseText.Substring(11, 4),
            baseText.Substring(16)
        }
        
        ' Violation: Using Substring in LINQ operations
        Dim shortItems = items.Where(Function(x) x.Length > 3).Select(Function(x) x.Substring(0, 3)).ToList()
        
        For Each item In shortItems
            Console.WriteLine($"Short item: {item}")
        Next
    End Sub
    
    Public Sub TestSubstringWithFileOperations()
        
        Dim fileName As String = "example_file.txt"
        
        ' Violation: Using Substring to get file name without extension
        Dim nameWithoutExt = fileName.Substring(0, fileName.LastIndexOf("."))
        
        ' Violation: Using Substring to get file extension
        Dim extension = fileName.Substring(fileName.LastIndexOf(".") + 1)
        
        Console.WriteLine($"Name: {nameWithoutExt}, Extension: {extension}")
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperStringUsageExamples
    
    Public Sub TestProperUsage()
        
        Dim text As String = "Hello World Example Text"
        
        ' Correct: Using AsSpan instead of Substring - should not be detected
        Dim span1 = text.AsSpan(0, 5)
        Dim span2 = text.AsSpan(6)
        
        ' Correct: Using ReadOnlySpan for text processing - should not be detected
        Dim readOnlySpan = text.AsSpan(0, 11)
        
        ' Correct: Using string methods that don't involve Substring - should not be detected
        Dim upperText = text.ToUpper()
        Dim length = text.Length
        Dim contains = text.Contains("World")
        
        ' Correct: Using string without any slicing - should not be detected
        Console.WriteLine($"Full text: {text}")
        Console.WriteLine($"Upper text: {upperText}")
        Console.WriteLine($"Length: {length}")
        Console.WriteLine($"Contains World: {contains}")
        
        ' Note: We can't directly print spans, but this shows proper usage
        Console.WriteLine($"Span length 1: {span1.Length}")
        Console.WriteLine($"Span length 2: {span2.Length}")
    End Sub
    
    Public Sub TestStringBuilderUsage()
        
        ' Correct: Using StringBuilder without Substring - should not be detected
        Dim sb As New System.Text.StringBuilder()
        sb.Append("Hello")
        sb.Append(" ")
        sb.Append("World")
        
        Dim result = sb.ToString()
        Console.WriteLine($"StringBuilder result: {result}")
    End Sub
    
End Class
