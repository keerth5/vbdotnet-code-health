' VB.NET test file for cq-vbn-0139: Use Span<T>.Clear() instead of Span<T>.Fill()
' Rule: It's more efficient to call Span<T>.Clear() than to call Span<T>.Fill(T) to fill the elements of the span with a default value.

Imports System

Public Class SpanClearExamples
    
    Public Sub TestSpanFillWithZero()
        Dim buffer(1023) As Byte
        Dim span As Span(Of Byte) = buffer.AsSpan()
        
        ' Violation: Using Fill(0) instead of Clear()
        span.Fill(0)
        
        Console.WriteLine("Buffer filled with zeros")
    End Sub
    
    Public Sub TestIntegerSpanFill()
        Dim numbers(99) As Integer
        Dim span As Span(Of Integer) = numbers.AsSpan()
        
        ' Violation: Using Fill(0) for default value
        span.Fill(0)
        
        Console.WriteLine("Integer array filled with zeros")
    End Sub
    
    Public Sub TestDoubleSpanFill()
        Dim values(49) As Double
        Dim span As Span(Of Double) = values.AsSpan()
        
        ' Violation: Using Fill(0.0) instead of Clear()
        span.Fill(0.0)
        
        Console.WriteLine("Double array filled with zeros")
    End Sub
    
    Public Sub TestCharSpanFill()
        Dim characters(255) As Char
        Dim span As Span(Of Char) = characters.AsSpan()
        
        ' Violation: Using Fill with null character
        span.Fill(ChrW(0))
        
        Console.WriteLine("Character array filled with null characters")
    End Sub
    
    Public Sub TestBooleanSpanFill()
        Dim flags(31) As Boolean
        Dim span As Span(Of Boolean) = flags.AsSpan()
        
        ' Violation: Using Fill(False) instead of Clear()
        span.Fill(False)
        
        Console.WriteLine("Boolean array filled with false values")
    End Sub
    
    Public Sub TestLongSpanFill()
        Dim longNumbers(63) As Long
        Dim span As Span(Of Long) = longNumbers.AsSpan()
        
        ' Violation: Using Fill(0L) instead of Clear()
        span.Fill(0L)
        
        Console.WriteLine("Long array filled with zeros")
    End Sub
    
    Public Sub TestShortSpanFill()
        Dim shortNumbers(127) As Short
        Dim span As Span(Of Short) = shortNumbers.AsSpan()
        
        ' Violation: Using Fill(0) instead of Clear()
        span.Fill(0S)
        
        Console.WriteLine("Short array filled with zeros")
    End Sub
    
    Public Sub TestFloatSpanFill()
        Dim floatValues(79) As Single
        Dim span As Span(Of Single) = floatValues.AsSpan()
        
        ' Violation: Using Fill(0.0F) instead of Clear()
        span.Fill(0.0F)
        
        Console.WriteLine("Float array filled with zeros")
    End Sub
    
    Public Sub TestDecimalSpanFill()
        Dim decimalValues(39) As Decimal
        Dim span As Span(Of Decimal) = decimalValues.AsSpan()
        
        ' Violation: Using Fill(0D) instead of Clear()
        span.Fill(0D)
        
        Console.WriteLine("Decimal array filled with zeros")
    End Sub
    
    Public Sub TestSByteSpanFill()
        Dim signedBytes(255) As SByte
        Dim span As Span(Of SByte) = signedBytes.AsSpan()
        
        ' Violation: Using Fill(0) instead of Clear()
        span.Fill(0)
        
        Console.WriteLine("SByte array filled with zeros")
    End Sub
    
    Public Sub TestUIntSpanFill()
        Dim uintValues(99) As UInteger
        Dim span As Span(Of UInteger) = uintValues.AsSpan()
        
        ' Violation: Using Fill(0UI) instead of Clear()
        span.Fill(0UI)
        
        Console.WriteLine("UInteger array filled with zeros")
    End Sub
    
    Public Sub TestULongSpanFill()
        Dim ulongValues(63) As ULong
        Dim span As Span(Of ULong) = ulongValues.AsSpan()
        
        ' Violation: Using Fill(0UL) instead of Clear()
        span.Fill(0UL)
        
        Console.WriteLine("ULong array filled with zeros")
    End Sub
    
    Public Sub TestUShortSpanFill()
        Dim ushortValues(127) As UShort
        Dim span As Span(Of UShort) = ushortValues.AsSpan()
        
        ' Violation: Using Fill(0US) instead of Clear()
        span.Fill(0US)
        
        Console.WriteLine("UShort array filled with zeros")
    End Sub
    
    Public Sub TestSpanFillInLoop()
        Dim buffers(9)() As Byte
        
        For i As Integer = 0 To 9
            buffers(i) = New Byte(1023) {}
            Dim span As Span(Of Byte) = buffers(i).AsSpan()
            
            ' Violation: Using Fill(0) in loop instead of Clear()
            span.Fill(0)
        Next
        
        Console.WriteLine("All buffers filled with zeros")
    End Sub
    
    Public Sub TestConditionalSpanFill(clearBuffer As Boolean)
        Dim data(511) As Integer
        Dim span As Span(Of Integer) = data.AsSpan()
        
        If clearBuffer Then
            ' Violation: Using Fill(0) conditionally instead of Clear()
            span.Fill(0)
        End If
    End Sub
    
    Public Sub TestPartialSpanFill()
        Dim largeBuffer(2047) As Byte
        Dim partialSpan As Span(Of Byte) = largeBuffer.AsSpan(0, 1024)
        
        ' Violation: Using Fill(0) on partial span instead of Clear()
        partialSpan.Fill(0)
        
        Console.WriteLine("Partial buffer filled with zeros")
    End Sub
    
    Public Sub TestSlicedSpanFill()
        Dim originalArray(1023) As Double
        Dim slicedSpan As Span(Of Double) = originalArray.AsSpan().Slice(100, 500)
        
        ' Violation: Using Fill(0.0) on sliced span instead of Clear()
        slicedSpan.Fill(0.0)
        
        Console.WriteLine("Sliced span filled with zeros")
    End Sub
    
    Public Sub TestSpanFillWithVariable()
        Dim buffer(255) As Integer
        Dim span As Span(Of Integer) = buffer.AsSpan()
        Dim defaultValue As Integer = 0
        
        ' Violation: Using Fill with zero variable instead of Clear()
        span.Fill(defaultValue)
        
        Console.WriteLine("Buffer filled with default value")
    End Sub
    
    Public Sub TestMultipleSpanFills()
        Dim buffer1(127) As Byte
        Dim buffer2(127) As Integer
        Dim buffer3(127) As Double
        
        Dim span1 As Span(Of Byte) = buffer1.AsSpan()
        Dim span2 As Span(Of Integer) = buffer2.AsSpan()
        Dim span3 As Span(Of Double) = buffer3.AsSpan()
        
        ' Violation: Multiple Fill calls with default values
        span1.Fill(0)
        span2.Fill(0)
        span3.Fill(0.0)
        
        Console.WriteLine("All buffers filled with zeros")
    End Sub
    
    Public Sub TestSpanFillInMethod(span As Span(Of Byte))
        ' Violation: Using Fill(0) in method parameter
        span.Fill(0)
    End Sub
    
    Public Function CreateAndFillSpan(size As Integer) As Span(Of Integer)
        Dim array(size - 1) As Integer
        Dim span As Span(Of Integer) = array.AsSpan()
        
        ' Violation: Using Fill(0) before returning
        span.Fill(0)
        
        Return span
    End Function
    
    Public Sub TestReadOnlySpanScenario()
        Dim sourceData(99) As Byte
        Dim span As Span(Of Byte) = sourceData.AsSpan()
        
        ' Violation: Fill before creating ReadOnlySpan
        span.Fill(0)
        
        Dim readOnlySpan As ReadOnlySpan(Of Byte) = span
        Console.WriteLine($"ReadOnlySpan length: {readOnlySpan.Length}")
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub CorrectSpanClear()
        Dim buffer(1023) As Byte
        Dim span As Span(Of Byte) = buffer.AsSpan()
        
        ' Correct: Using Clear() instead of Fill(0)
        span.Clear()
        
        Console.WriteLine("Buffer cleared correctly")
    End Sub
    
    Public Sub CorrectSpanFillWithNonDefaultValue()
        Dim buffer(255) As Integer
        Dim span As Span(Of Integer) = buffer.AsSpan()
        
        ' Correct: Using Fill with non-default value
        span.Fill(42)
        
        Console.WriteLine("Buffer filled with 42")
    End Sub
    
    Public Sub TestStructSpanFill()
        Dim points(99) As Point
        Dim span As Span(Of Point) = points.AsSpan()
        
        ' Violation: Using Fill with default struct value
        span.Fill(New Point())
        
        Console.WriteLine("Points filled with default values")
    End Sub
    
    Public Structure Point
        Public X As Integer
        Public Y As Integer
        
        Public Sub New(x As Integer, y As Integer)
            Me.X = x
            Me.Y = y
        End Sub
    End Structure
    
    Public Sub TestGenericSpanFill(Of T As Structure)(values() As T)
        Dim span As Span(Of T) = values.AsSpan()
        
        ' Violation: Using Fill with default generic value
        span.Fill(Nothing)  ' This would be the default value for T
    End Sub
End Class
