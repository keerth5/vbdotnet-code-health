' VB.NET test file for cq-vbn-0118: Use AsSpan or AsMemory instead of Range-based indexers for getting Span or Memory portion of an array
' Rule: When using a range-indexer on an array and implicitly assigning the value to a Span<T> or Memory<T> type, 
' the method GetSubArray will be used instead of Slice, which produces a copy of requested portion of the array.

Imports System

Public Class ArraySpanMemoryExamples
    
    Public Sub TestArraySpanAssignments()
        Dim numbers As Integer() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
        Dim names As String() = {"Alice", "Bob", "Charlie", "David", "Eve", "Frank"}
        Dim bytes As Byte() = {10, 20, 30, 40, 50, 60, 70, 80, 90}
        
        ' Violation: Using range-based indexer on array assigned to Span
        Dim span1 As Span(Of Integer) = numbers(0...5)
        
        ' Violation: Using range-based indexer on string array assigned to Span
        Dim span2 As Span(Of String) = names(1...4)
        
        ' Violation: Using range-based indexer on byte array assigned to Span
        Dim span3 As Span(Of Byte) = bytes(2...6)
        
        ' Violation: Using range-based indexer assigned to Memory
        Dim memory1 As Memory(Of Integer) = numbers(3...8)
        
        ' Violation: Using range-based indexer on string array assigned to Memory
        Dim memory2 As Memory(Of String) = names(0...3)
        
        ' Violation: Using range-based indexer on byte array assigned to Memory
        Dim memory3 As Memory(Of Byte) = bytes(1...5)
        
        ' Violation: Using range-based indexer with variables
        Dim start As Integer = 1
        Dim endIndex As Integer = 6
        Dim span4 As Span(Of Integer) = numbers(start...endIndex)
        
        ' Violation: Using range-based indexer with calculated indices
        Dim span5 As Span(Of Integer) = numbers((start + 1)...(endIndex - 1))
        
        ' Violation: Using range-based indexer in method parameter
        ProcessSpan(numbers(0...4))
        
        ' Violation: Using range-based indexer in method parameter for Memory
        ProcessMemory(numbers(2...7))
        
    End Sub
    
    Public Sub ModifyArraySegments()
        Dim data As Double() = {1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.0}
        
        ' Violation: Using range-based indexer for mutable Span
        Dim firstHalf As Span(Of Double) = data(0...4)
        Dim secondHalf As Span(Of Double) = data(5...9)
        
        ' Modify the spans (this is why we need mutable Span, not ReadOnlySpan)
        firstHalf(0) = 99.9
        secondHalf(0) = 88.8
        
        ' Violation: Using range-based indexer for mutable Memory
        Dim firstMemory As Memory(Of Double) = data(0...3)
        Dim secondMemory As Memory(Of Double) = data(4...7)
        
        ProcessDoubleSpan(firstHalf)
        ProcessDoubleSpan(secondHalf)
        ProcessDoubleMemory(firstMemory)
        ProcessDoubleMemory(secondMemory)
    End Sub
    
    Public Function GetMutableArraySegment() As Span(Of Integer)
        Dim mutableData As Integer() = {100, 200, 300, 400, 500, 600, 700}
        
        ' Violation: Using range-based indexer in return statement for Span
        Return mutableData(1...5)
    End Function
    
    Public Function GetArrayMemorySegment() As Memory(Of String)
        Dim stringData As String() = {"first", "second", "third", "fourth", "fifth", "sixth"}
        
        ' Violation: Using range-based indexer in return statement for Memory
        Return stringData(1...4)
    End Function
    
    Public Sub ProcessMutableArrays()
        Dim array1 As Integer() = {1, 2, 3, 4, 5, 6}
        Dim array2 As Integer() = {10, 20, 30, 40, 50, 60}
        Dim array3 As Integer() = {100, 200, 300, 400, 500, 600}
        
        ' Violation: Multiple range-based indexer assignments for mutable operations
        Dim span1 As Span(Of Integer) = array1(0...3)
        Dim span2 As Span(Of Integer) = array2(1...4)
        Dim span3 As Span(Of Integer) = array3(2...5)
        
        ' Modify the data through spans
        span1(0) = 999
        span2(1) = 888
        span3(2) = 777
        
        ProcessIntegerSpan(span1)
        ProcessIntegerSpan(span2)
        ProcessIntegerSpan(span3)
    End Sub
    
    Public Sub WorkWithMemorySegments()
        Dim workData As Byte() = New Byte(99) {}
        
        ' Initialize array
        For i As Integer = 0 To workData.Length - 1
            workData(i) = CByte(i Mod 256)
        Next
        
        ' Violation: Using range-based indexer for Memory segments
        Dim segment1 As Memory(Of Byte) = workData(0...24)
        Dim segment2 As Memory(Of Byte) = workData(25...49)
        Dim segment3 As Memory(Of Byte) = workData(50...74)
        Dim segment4 As Memory(Of Byte) = workData(75...99)
        
        ProcessByteMemory(segment1)
        ProcessByteMemory(segment2)
        ProcessByteMemory(segment3)
        ProcessByteMemory(segment4)
    End Sub
    
    Public Sub LoopThroughMutableSegments()
        Dim largeArray As Integer() = New Integer(49) {}
        
        ' Initialize array
        For i As Integer = 0 To largeArray.Length - 1
            largeArray(i) = i + 1
        Next
        
        ' Violation: Using range-based indexer in loop for mutable Span
        For i As Integer = 0 To largeArray.Length - 5 Step 5
            Dim segment As Span(Of Integer) = largeArray(i...(i + 4))
            
            ' Modify the segment
            segment(0) = segment(0) * 10
            
            ProcessIntegerSpan(segment)
        Next
    End Sub
    
    Private Sub ProcessSpan(span As Span(Of Integer))
        Console.WriteLine($"Processing span with {span.Length} elements")
        ' Can modify the span
        If span.Length > 0 Then
            span(0) = span(0) + 1000
        End If
    End Sub
    
    Private Sub ProcessMemory(memory As Memory(Of Integer))
        Console.WriteLine($"Processing memory with {memory.Length} elements")
    End Sub
    
    Private Sub ProcessDoubleSpan(span As Span(Of Double))
        Console.WriteLine($"Processing double span with {span.Length} elements")
    End Sub
    
    Private Sub ProcessDoubleMemory(memory As Memory(Of Double))
        Console.WriteLine($"Processing double memory with {memory.Length} elements")
    End Sub
    
    Private Sub ProcessIntegerSpan(span As Span(Of Integer))
        Console.WriteLine($"Processing integer span with {span.Length} elements")
    End Sub
    
    Private Sub ProcessByteMemory(memory As Memory(Of Byte))
        Console.WriteLine($"Processing byte memory with {memory.Length} elements")
    End Sub
    
End Class

' More violation examples in different contexts

Public Class MutableDataProcessingExample
    
    Public Sub ProcessAndModifyData()
        Dim processingData As Single() = {1.0F, 2.0F, 3.0F, 4.0F, 5.0F, 6.0F, 7.0F, 8.0F}
        
        ' Violation: Using range-based indexer for mutable processing
        Dim firstQuarter As Span(Of Single) = processingData(0...1)
        Dim secondQuarter As Span(Of Single) = processingData(2...3)
        Dim thirdQuarter As Span(Of Single) = processingData(4...5)
        Dim fourthQuarter As Span(Of Single) = processingData(6...7)
        
        ' Apply transformations
        ApplyTransformation(firstQuarter, 1.1F)
        ApplyTransformation(secondQuarter, 1.2F)
        ApplyTransformation(thirdQuarter, 1.3F)
        ApplyTransformation(fourthQuarter, 1.4F)
    End Sub
    
    Public Sub BatchProcessArrays()
        Dim batchData As Long() = New Long(99) {}
        
        ' Initialize with sample data
        For i As Integer = 0 To batchData.Length - 1
            batchData(i) = i * 10L
        Next
        
        ' Violation: Using range-based indexer for batch processing with Memory
        Dim batch1 As Memory(Of Long) = batchData(0...19)
        Dim batch2 As Memory(Of Long) = batchData(20...39)
        Dim batch3 As Memory(Of Long) = batchData(40...59)
        Dim batch4 As Memory(Of Long) = batchData(60...79)
        Dim batch5 As Memory(Of Long) = batchData(80...99)
        
        ProcessLongBatch(batch1)
        ProcessLongBatch(batch2)
        ProcessLongBatch(batch3)
        ProcessLongBatch(batch4)
        ProcessLongBatch(batch5)
    End Sub
    
    Private Sub ApplyTransformation(data As Span(Of Single), multiplier As Single)
        For i As Integer = 0 To data.Length - 1
            data(i) *= multiplier
        Next
    End Sub
    
    Private Sub ProcessLongBatch(batch As Memory(Of Long))
        Console.WriteLine($"Processing batch with {batch.Length} elements")
        
        ' Can get a span from memory for direct manipulation
        Dim span = batch.Span
        For i As Integer = 0 To span.Length - 1
            span(i) += 1000L
        Next
    End Sub
    
End Class

Public Class BufferManagementExample
    
    Public Sub ManageBuffers()
        Dim mainBuffer As Byte() = New Byte(1023) {}  ' 1KB buffer
        
        ' Fill buffer with sample data
        For i As Integer = 0 To mainBuffer.Length - 1
            mainBuffer(i) = CByte((i * 7) Mod 256)
        Next
        
        ' Violation: Using range-based indexer for buffer management
        Dim headerBuffer As Memory(Of Byte) = mainBuffer(0...63)      ' 64 bytes header
        Dim dataBuffer As Memory(Of Byte) = mainBuffer(64...959)      ' 896 bytes data
        Dim footerBuffer As Memory(Of Byte) = mainBuffer(960...1023)  ' 64 bytes footer
        
        ProcessBufferSegment("Header", headerBuffer)
        ProcessBufferSegment("Data", dataBuffer)
        ProcessBufferSegment("Footer", footerBuffer)
    End Sub
    
    Private Sub ProcessBufferSegment(name As String, buffer As Memory(Of Byte))
        Console.WriteLine($"Processing {name} buffer: {buffer.Length} bytes")
        
        ' Can modify the buffer through span
        Dim span = buffer.Span
        If span.Length > 0 Then
            span(0) = 255  ' Mark as processed
        End If
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperSpanMemoryUsageExamples
    
    Public Sub TestProperUsage()
        Dim numbers As Integer() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
        
        ' Correct: Using AsSpan - should not be detected
        Dim span1 As Span(Of Integer) = numbers.AsSpan(0, 5)
        Dim span2 As Span(Of Integer) = numbers.AsSpan(5)
        
        ' Correct: Using AsMemory - should not be detected
        Dim memory1 As Memory(Of Integer) = numbers.AsMemory(2, 4)
        Dim memory2 As Memory(Of Integer) = numbers.AsMemory(6)
        
        ' Correct: Using regular array assignment - should not be detected
        Dim regularArray As Integer() = numbers
        
        ' Correct: Using array indexer for single element - should not be detected
        Dim singleElement = numbers(0)
        
        ' Correct: Using Span constructor - should not be detected
        Dim spanFromConstructor As New Span(Of Integer)(numbers, 1, 3)
        
        ProcessSpan(span1)
        ProcessMemory(memory1)
    End Sub
    
    Private Sub ProcessSpan(span As Span(Of Integer))
        Console.WriteLine($"Processing span with {span.Length} elements")
    End Sub
    
    Private Sub ProcessMemory(memory As Memory(Of Integer))
        Console.WriteLine($"Processing memory with {memory.Length} elements")
    End Sub
    
End Class
