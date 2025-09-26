' VB.NET test file for cq-vbn-0129: Use span-based 'string.Concat'
' Rule: It is more efficient to use AsSpan and string.Concat, instead of Substring and a concatenation operator.

Imports System

Public Class StringConcatExamples
    
    Public Sub TestStringConcatViolations()
        
        Dim text As String = "Hello World Example"
        Dim prefix As String = "Prefix"
        Dim suffix As String = "Suffix"
        
        ' Violation: Using Substring with concatenation operator
        Dim result1 = text.Substring(0, 5) + " " + text.Substring(6)
        
        ' Violation: Using Substring with concatenation in assignment
        Dim result2 = prefix + text.Substring(5) + suffix
        
        ' Violation: Using Substring with string concatenation
        Dim result3 = text.Substring(0, 3) & text.Substring(7, 5) & text.Substring(13)
        
        ' Violation: Using Substring in String.Concat call
        Dim result4 = String.Concat(text.Substring(0, 5), " - ", text.Substring(6, 5))
        
        ' Violation: Using multiple Substring calls with concatenation
        Dim result5 = text.Substring(0, 2) + text.Substring(3, 4) + text.Substring(8)
        
        Console.WriteLine($"Result 1: {result1}")
        Console.WriteLine($"Result 2: {result2}")
        Console.WriteLine($"Result 3: {result3}")
        Console.WriteLine($"Result 4: {result4}")
        Console.WriteLine($"Result 5: {result5}")
    End Sub
    
    Public Sub TestStringConcatInMethods()
        
        Dim input As String = "Programming Language"
        
        ' Violation: Using Substring with concatenation in method parameter
        ProcessString(input.Substring(0, 11) + " " + input.Substring(12))
        
        ' Violation: Using Substring with concatenation in return statement
        Dim transformed = TransformString(input.Substring(0, 7), input.Substring(8))
        
        Console.WriteLine($"Transformed: {transformed}")
    End Sub
    
    Public Function TransformString(part1 As String, part2 As String) As String
        
        ' Violation: Using Substring with concatenation in function
        Return part1.Substring(0, 3) + "-" + part2.Substring(0, 4)
        
    End Function
    
    Public Sub TestStringConcatInConditionals()
        
        Dim data As String = "Important Data Content"
        
        ' Violation: Using Substring with concatenation in conditional
        If data.Substring(0, 9) + data.Substring(10) = "Important Data Content" Then
            Console.WriteLine("Data matches expected format")
        End If
        
        ' Violation: Using Substring with concatenation in conditional expression
        Dim isValid = (data.Substring(0, 5) & " " & data.Substring(6, 4)) = "Important Data"
        
        Console.WriteLine($"Is valid: {isValid}")
    End Sub
    
    Public Sub TestStringConcatInLoops()
        
        Dim texts() As String = {"First", "Second", "Third"}
        
        For Each text In texts
            ' Violation: Using Substring with concatenation in loop
            Dim modified = text.Substring(0, 2) + "..." + text.Substring(text.Length - 2)
            Console.WriteLine($"Modified: {modified}")
        Next
        
    End Sub
    
    Public Sub TestStringConcatWithInterpolation()
        
        Dim message As String = "Hello World from VB.NET"
        
        ' Violation: Using Substring with concatenation in string interpolation
        Dim formatted = $"{message.Substring(0, 5)} + {message.Substring(6, 5)} = {message.Substring(0, 5) + message.Substring(6, 5)}"
        
        Console.WriteLine(formatted)
    End Sub
    
    Public Sub TestComplexStringConcatViolations()
        
        Dim source As String = "Complex String Manipulation Example"
        
        ' Violation: Complex Substring concatenation
        Dim result1 = source.Substring(0, 7) + " " + source.Substring(8, 6) + " " + source.Substring(15)
        
        ' Violation: Substring concatenation with additional strings
        Dim result2 = "Start: " + source.Substring(0, 10) + " Middle: " + source.Substring(11, 12) + " End: " + source.Substring(24)
        
        ' Violation: Nested Substring concatenation
        Dim result3 = (source.Substring(0, 4) + source.Substring(5, 3)) + " | " + (source.Substring(9, 6) + source.Substring(16))
        
        Console.WriteLine($"Complex Result 1: {result1}")
        Console.WriteLine($"Complex Result 2: {result2}")
        Console.WriteLine($"Complex Result 3: {result3}")
    End Sub
    
    Private Sub ProcessString(input As String)
        Console.WriteLine($"Processing: {input}")
    End Sub
    
End Class

' More violation examples

Public Class AdditionalStringConcatExamples
    
    Public Sub TestStringBuilderWithSubstring()
        
        Dim text As String = "StringBuilder Example Text"
        Dim sb As New System.Text.StringBuilder()
        
        ' Violation: Using Substring with StringBuilder.Append
        sb.Append(text.Substring(0, 12) + " " + text.Substring(13))
        
        ' Violation: Using Substring concatenation before StringBuilder.Append
        sb.Append(text.Substring(0, 6) & text.Substring(7, 7))
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub TestStringConcatInArrays()
        
        Dim baseText As String = "Array Element Text"
        
        ' Violation: Using Substring concatenation in array initialization
        Dim results() As String = {
            baseText.Substring(0, 5) + " " + baseText.Substring(6),
            baseText.Substring(0, 8) & baseText.Substring(9),
            baseText.Substring(0, 3) + "..." + baseText.Substring(baseText.Length - 4)
        }
        
        For Each result In results
            Console.WriteLine($"Array result: {result}")
        Next
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperStringUsageExamples
    
    Public Sub TestProperUsage()
        
        Dim text As String = "Hello World Example"
        
        ' Correct: Using AsSpan with string.Concat - should not be detected
        Dim result1 = String.Concat(text.AsSpan(0, 5), " ", text.AsSpan(6))
        
        ' Correct: Using simple string concatenation without Substring - should not be detected
        Dim prefix As String = "Start"
        Dim suffix As String = "End"
        Dim result2 = prefix + " " + suffix
        
        ' Correct: Using Substring alone without concatenation - should not be detected
        Dim part = text.Substring(0, 5)
        
        ' Correct: Using string interpolation without Substring concatenation - should not be detected
        Dim result3 = $"Text: {text}"
        
        ' Correct: Using String.Join - should not be detected
        Dim parts() As String = {"Hello", "World", "Example"}
        Dim result4 = String.Join(" ", parts)
        
        Console.WriteLine($"Result 1: {result1}")
        Console.WriteLine($"Result 2: {result2}")
        Console.WriteLine($"Part: {part}")
        Console.WriteLine($"Result 3: {result3}")
        Console.WriteLine($"Result 4: {result4}")
    End Sub
    
End Class
