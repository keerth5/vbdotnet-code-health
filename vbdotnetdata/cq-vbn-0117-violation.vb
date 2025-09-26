' VB.NET test file for cq-vbn-0117: Use AsSpan or AsMemory instead of Range-based indexers for getting ReadOnlySpan or ReadOnlyMemory portion of an array
' Rule: When using a range-indexer on an array and implicitly assigning the value to a ReadOnlySpan<T> or ReadOnlyMemory<T> type, 
' the method GetSubArray will be used instead of Slice, which produces a copy of requested portion of the array.

Imports System

Public Class ArrayRangeExamples
    
    Public Sub TestArrayRangeIndexers()
        Dim numbers As Integer() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
        Dim names As String() = {"Alice", "Bob", "Charlie", "David", "Eve"}
        Dim bytes As Byte() = {10, 20, 30, 40, 50, 60, 70, 80}
        
        ' Violation: Using range-based indexer on array assigned to ReadOnlySpan
        Dim span1 As ReadOnlySpan(Of Integer) = numbers(0...5)
        
        ' Violation: Using range-based indexer on string array assigned to ReadOnlySpan
        Dim span2 As ReadOnlySpan(Of String) = names(1...4)
        
        ' Violation: Using range-based indexer on byte array assigned to ReadOnlySpan
        Dim span3 As ReadOnlySpan(Of Byte) = bytes(2...6)
        
        ' Violation: Using range-based indexer assigned to ReadOnlyMemory
        Dim memory1 As ReadOnlyMemory(Of Integer) = numbers(3...8)
        
        ' Violation: Using range-based indexer on string array assigned to ReadOnlyMemory
        Dim memory2 As ReadOnlyMemory(Of String) = names(0...3)
        
        ' Violation: Using range-based indexer on byte array assigned to ReadOnlyMemory
        Dim memory3 As ReadOnlyMemory(Of Byte) = bytes(1...5)
        
        ' Violation: Using range-based indexer with variables
        Dim start As Integer = 2
        Dim endIndex As Integer = 7
        Dim span4 As ReadOnlySpan(Of Integer) = numbers(start...endIndex)
        
        ' Violation: Using range-based indexer with calculated indices
        Dim span5 As ReadOnlySpan(Of Integer) = numbers((start + 1)...(endIndex - 1))
        
        ' Violation: Using range-based indexer in method parameter
        ProcessReadOnlySpan(numbers(0...3))
        
        ' Violation: Using range-based indexer in method parameter for ReadOnlyMemory
        ProcessReadOnlyMemory(numbers(2...6))
        
        ' Violation: Using range-based indexer in assignment from method result
        Dim methodArray = GetNumbers()
        Dim span6 As ReadOnlySpan(Of Integer) = methodArray(1...4)
        
        ' Violation: Using range-based indexer in assignment from property
        Dim span7 As ReadOnlySpan(Of Integer) = Me.NumbersProperty(0...5)
        
    End Sub
    
    Public Sub ProcessArraySlices()
        Dim data As Double() = {1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9}
        
        ' Violation: Multiple range-based indexer assignments to ReadOnlySpan
        Dim firstHalf As ReadOnlySpan(Of Double) = data(0...4)
        Dim secondHalf As ReadOnlySpan(Of Double) = data(5...8)
        
        ' Violation: Multiple range-based indexer assignments to ReadOnlyMemory
        Dim firstMemory As ReadOnlyMemory(Of Double) = data(0...3)
        Dim secondMemory As ReadOnlyMemory(Of Double) = data(4...7)
        
        ProcessDoubleSpan(firstHalf)
        ProcessDoubleSpan(secondHalf)
        ProcessDoubleMemory(firstMemory)
        ProcessDoubleMemory(secondMemory)
    End Sub
    
    Public Function AnalyzeArraySegment() As ReadOnlySpan(Of Integer)
        Dim analysisData As Integer() = {100, 200, 300, 400, 500, 600, 700}
        
        ' Violation: Using range-based indexer in return statement
        Return analysisData(1...5)
    End Function
    
    Public Function GetArrayMemorySegment() As ReadOnlyMemory(Of String)
        Dim stringData As String() = {"first", "second", "third", "fourth", "fifth"}
        
        ' Violation: Using range-based indexer in return statement for ReadOnlyMemory
        Return stringData(1...4)
    End Function
    
    Public Sub ProcessMultipleArrays()
        Dim array1 As Integer() = {1, 2, 3, 4, 5}
        Dim array2 As Integer() = {10, 20, 30, 40, 50}
        Dim array3 As Integer() = {100, 200, 300, 400, 500}
        
        ' Violation: Multiple range-based indexer assignments
        Dim span1 As ReadOnlySpan(Of Integer) = array1(0...3)
        Dim span2 As ReadOnlySpan(Of Integer) = array2(1...4)
        Dim span3 As ReadOnlySpan(Of Integer) = array3(2...5)
        
        ' Process the spans
        ProcessIntegerSpan(span1)
        ProcessIntegerSpan(span2)
        ProcessIntegerSpan(span3)
    End Sub
    
    Public Sub LoopThroughArraySegments()
        Dim largeArray As Integer() = New Integer(99) {}
        
        ' Initialize array
        For i As Integer = 0 To largeArray.Length - 1
            largeArray(i) = i + 1
        Next
        
        ' Violation: Using range-based indexer in loop
        For i As Integer = 0 To largeArray.Length - 10 Step 10
            Dim segment As ReadOnlySpan(Of Integer) = largeArray(i...(i + 9))
            ProcessIntegerSpan(segment)
        Next
    End Sub
    
    Public Property NumbersProperty As Integer() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
    
    Private Function GetNumbers() As Integer()
        Return New Integer() {11, 12, 13, 14, 15, 16, 17, 18, 19, 20}
    End Function
    
    Private Sub ProcessReadOnlySpan(span As ReadOnlySpan(Of Integer))
        Console.WriteLine($"Processing span with {span.Length} elements")
    End Sub
    
    Private Sub ProcessReadOnlyMemory(memory As ReadOnlyMemory(Of Integer))
        Console.WriteLine($"Processing memory with {memory.Length} elements")
    End Sub
    
    Private Sub ProcessDoubleSpan(span As ReadOnlySpan(Of Double))
        Console.WriteLine($"Processing double span with {span.Length} elements")
    End Sub
    
    Private Sub ProcessDoubleMemory(memory As ReadOnlyMemory(Of Double))
        Console.WriteLine($"Processing double memory with {memory.Length} elements")
    End Sub
    
    Private Sub ProcessIntegerSpan(span As ReadOnlySpan(Of Integer))
        Console.WriteLine($"Processing integer span with {span.Length} elements")
    End Sub
    
End Class

' More violation examples in different contexts

Public Class DataProcessingExample
    
    Public Sub ProcessBinaryData()
        Dim binaryData As Byte() = New Byte(255) {}
        
        ' Initialize with sample data
        For i As Integer = 0 To binaryData.Length - 1
            binaryData(i) = CByte(i Mod 256)
        Next
        
        ' Violation: Using range-based indexer for binary data processing
        Dim header As ReadOnlySpan(Of Byte) = binaryData(0...16)
        Dim payload As ReadOnlySpan(Of Byte) = binaryData(16...240)
        Dim footer As ReadOnlySpan(Of Byte) = binaryData(240...255)
        
        ProcessBinaryHeader(header)
        ProcessBinaryPayload(payload)
        ProcessBinaryFooter(footer)
    End Sub
    
    Public Sub AnalyzeTextSegments()
        Dim textArray As String() = {
            "first", "second", "third", "fourth", "fifth",
            "sixth", "seventh", "eighth", "ninth", "tenth"
        }
        
        ' Violation: Using range-based indexer for text analysis
        Dim beginningSegment As ReadOnlyMemory(Of String) = textArray(0...3)
        Dim middleSegment As ReadOnlyMemory(Of String) = textArray(3...7)
        Dim endSegment As ReadOnlyMemory(Of String) = textArray(7...9)
        
        AnalyzeTextMemory(beginningSegment)
        AnalyzeTextMemory(middleSegment)
        AnalyzeTextMemory(endSegment)
    End Sub
    
    Private Sub ProcessBinaryHeader(header As ReadOnlySpan(Of Byte))
        Console.WriteLine($"Processing binary header: {header.Length} bytes")
    End Sub
    
    Private Sub ProcessBinaryPayload(payload As ReadOnlySpan(Of Byte))
        Console.WriteLine($"Processing binary payload: {payload.Length} bytes")
    End Sub
    
    Private Sub ProcessBinaryFooter(footer As ReadOnlySpan(Of Byte))
        Console.WriteLine($"Processing binary footer: {footer.Length} bytes")
    End Sub
    
    Private Sub AnalyzeTextMemory(memory As ReadOnlyMemory(Of String))
        Console.WriteLine($"Analyzing text memory: {memory.Length} strings")
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperArrayUsageExamples
    
    Public Sub TestProperUsage()
        Dim numbers As Integer() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
        
        ' Correct: Using AsSpan - should not be detected
        Dim span1 As ReadOnlySpan(Of Integer) = numbers.AsSpan(0, 5)
        Dim span2 As ReadOnlySpan(Of Integer) = numbers.AsSpan(5)
        
        ' Correct: Using AsMemory - should not be detected
        Dim memory1 As ReadOnlyMemory(Of Integer) = numbers.AsMemory(2, 4)
        Dim memory2 As ReadOnlyMemory(Of Integer) = numbers.AsMemory(6)
        
        ' Correct: Using regular array assignment - should not be detected
        Dim regularArray As Integer() = numbers
        
        ' Correct: Using array indexer for single element - should not be detected
        Dim singleElement = numbers(0)
        
        ' Correct: Using Span constructor - should not be detected
        Dim spanFromConstructor As New ReadOnlySpan(Of Integer)(numbers, 1, 3)
        
        ProcessReadOnlySpan(span1)
        ProcessReadOnlyMemory(memory1)
    End Sub
    
    Private Sub ProcessReadOnlySpan(span As ReadOnlySpan(Of Integer))
        Console.WriteLine($"Processing span with {span.Length} elements")
    End Sub
    
    Private Sub ProcessReadOnlyMemory(memory As ReadOnlyMemory(Of Integer))
        Console.WriteLine($"Processing memory with {memory.Length} elements")
    End Sub
    
End Class
