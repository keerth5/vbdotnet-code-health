' VB.NET test file for cq-vbn-0116: Use AsSpan instead of Range-based indexers for string when appropriate
' Rule: When using a range-indexer on a string and implicitly assigning the value to a ReadOnlySpan<char> type, 
' the method Substring will be used instead of Slice, which produces a copy of requested portion of the string.

Imports System

Public Class StringRangeExamples
    
    Public Sub TestStringRangeIndexers()
        Dim text As String = "Hello World Example"
        
        ' Violation: Using range-based indexer on string (VB.NET syntax would be different)
        Dim part1 = text(0...5)
        
        ' Violation: Using range-based indexer with different range
        Dim part2 = text(6...11)
        
        ' Violation: Using range-based indexer with variables
        Dim start As Integer = 2
        Dim endIndex As Integer = 8
        Dim part3 = text(start...endIndex)
        
        ' Violation: Using Substring method which should be AsSpan
        Dim substring1 = text.Substring(0, 5)
        
        ' Violation: Using Substring with single parameter
        Dim substring2 = text.Substring(6)
        
        ' Violation: Using Substring with calculated parameters
        Dim substring3 = text.Substring(text.Length - 5, 5)
        
        ' Violation: Using Substring in assignment to ReadOnlySpan
        Dim span1 As ReadOnlySpan(Of Char) = text.Substring(0, 5)
        
        ' Violation: Using Substring in method parameter
        ProcessStringPart(text.Substring(0, 10))
        
        ' Violation: Using Substring in return statement
        Return text.Substring(5, 3)
        
        ' Violation: Using Substring with method call
        Dim methodResult = GetText().Substring(0, 8)
        
        ' Violation: Using Substring with property
        Dim propertyResult = Me.TextProperty.Substring(2, 6)
        
        ' Violation: Using Substring in conditional
        If text.Substring(0, 5) = "Hello" Then
            Console.WriteLine("Text starts with Hello")
        End If
        
        ' Violation: Using Substring in loop
        For i As Integer = 0 To text.Length - 5 Step 2
            Dim loopPart = text.Substring(i, 3)
            Console.WriteLine(loopPart)
        Next
        
    End Sub
    
    Public Function ProcessText() As String
        Dim input As String = "Processing Example Text"
        
        ' Violation: Using Substring for text processing
        Dim header = input.Substring(0, 10)
        Dim middle = input.Substring(10, 8)
        Dim footer = input.Substring(18)
        
        Return header & " | " & middle & " | " & footer
    End Function
    
    Public Sub AnalyzeString()
        Dim data As String = "Data Analysis Example String"
        
        ' Violation: Using Substring for analysis
        Dim firstWord = data.Substring(0, 4)
        Dim secondWord = data.Substring(5, 8)
        Dim lastPart = data.Substring(14)
        
        Console.WriteLine($"First: {firstWord}, Second: {secondWord}, Last: {lastPart}")
    End Sub
    
    Public Sub ExtractSubstrings()
        Dim source As String = "Extract Multiple Substrings From This Text"
        
        ' Violation: Multiple Substring calls
        Dim extract1 = source.Substring(0, 7)    ' "Extract"
        Dim extract2 = source.Substring(8, 8)    ' "Multiple"
        Dim extract3 = source.Substring(17, 10)  ' "Substrings"
        Dim extract4 = source.Substring(28, 4)   ' "From"
        Dim extract5 = source.Substring(33)      ' "This Text"
        
        ProcessExtracts(extract1, extract2, extract3, extract4, extract5)
    End Sub
    
    Public Property TextProperty As String = "Property Text Example"
    
    Private Function GetText() As String
        Return "Method Return Text Example"
    End Function
    
    Private Sub ProcessStringPart(part As String)
        Console.WriteLine($"Processing: {part}")
    End Sub
    
    Private Sub ProcessExtracts(ParamArray extracts As String())
        For Each extract In extracts
            Console.WriteLine($"Extract: {extract}")
        Next
    End Sub
    
End Class

' More violation examples in different contexts

Public Class StringParsingExample
    
    Public Sub ParseData()
        Dim csvLine As String = "John,Doe,30,Engineer,New York"
        
        ' Violation: Using Substring for CSV parsing
        Dim firstName = csvLine.Substring(0, csvLine.IndexOf(","c))
        Dim remaining = csvLine.Substring(csvLine.IndexOf(","c) + 1)
        
        ' Continue parsing with more Substring calls
        Dim lastNameStart = remaining.IndexOf(","c)
        Dim lastName = remaining.Substring(0, lastNameStart)
        
        Console.WriteLine($"Name: {firstName} {lastName}")
    End Sub
    
    Public Function ExtractFileExtension(filePath As String) As String
        Dim dotIndex = filePath.LastIndexOf("."c)
        If dotIndex >= 0 Then
            ' Violation: Using Substring to extract extension
            Return filePath.Substring(dotIndex + 1)
        End If
        Return String.Empty
    End Function
    
    Public Sub ProcessLogEntry()
        Dim logEntry As String = "[2023-10-15 14:30:25] INFO: Application started successfully"
        
        ' Violation: Using Substring to extract log components
        Dim timestamp = logEntry.Substring(1, 19)  ' Extract timestamp
        Dim level = logEntry.Substring(23, 4)      ' Extract level
        Dim message = logEntry.Substring(29)       ' Extract message
        
        Console.WriteLine($"Time: {timestamp}, Level: {level}, Message: {message}")
    End Sub
    
End Class

Public Class TextManipulationExample
    
    Public Function TrimText(input As String, maxLength As Integer) As String
        If input.Length > maxLength Then
            ' Violation: Using Substring for trimming
            Return input.Substring(0, maxLength) & "..."
        End If
        Return input
    End Function
    
    Public Sub SplitAndProcess()
        Dim longText As String = "This is a very long text that needs to be split into smaller parts for processing"
        Dim chunkSize As Integer = 10
        
        For i As Integer = 0 To longText.Length - 1 Step chunkSize
            Dim remainingLength = Math.Min(chunkSize, longText.Length - i)
            ' Violation: Using Substring in loop for chunking
            Dim chunk = longText.Substring(i, remainingLength)
            ProcessChunk(chunk)
        Next
    End Sub
    
    Private Sub ProcessChunk(chunk As String)
        Console.WriteLine($"Processing chunk: {chunk}")
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperStringUsageExamples
    
    Public Sub TestProperUsage()
        Dim text As String = "Hello World Example"
        
        ' Correct: Using AsSpan - should not be detected
        Dim span1 = text.AsSpan(0, 5)
        Dim span2 = text.AsSpan(6, 5)
        Dim span3 = text.AsSpan(12)
        
        ' Correct: Using string indexer for single character - should not be detected
        Dim firstChar = text(0)
        Dim lastChar = text(text.Length - 1)
        
        ' Correct: Using Substring when result is used as string - should not be detected
        Dim stringResult As String = text.Substring(0, 5)
        ProcessString(stringResult)
        
        ' Correct: Using string methods that don't involve ranges - should not be detected
        Dim upperText = text.ToUpper()
        Dim trimmedText = text.Trim()
        
    End Sub
    
    Private Sub ProcessString(text As String)
        Console.WriteLine(text)
    End Sub
    
End Class
