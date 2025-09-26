' VB.NET test file for cq-vbn-0302: Do not compare Span<T> to null or default
' This rule detects Span(Of T) comparisons to Nothing or Default

Imports System

Public Class BadSpanComparison
    ' BAD: Comparing Span(Of T) to Nothing or Default
    Public Sub TestBadSpanComparison()
        Dim span1 As Span(Of Integer) = New Span(Of Integer)()
        
        ' Violation: Comparing Span to Nothing
        If span1 = Nothing Then
            Console.WriteLine("Span is Nothing")
        End If
        
        ' Violation: Comparing Span to Nothing with <>
        If span1 <> Nothing Then
            Console.WriteLine("Span is not Nothing")
        End If
        
        ' Violation: Comparing Span to Default
        If span1 = Default Then
            Console.WriteLine("Span is Default")
        End If
        
        ' Violation: Comparing Span to Default with <>
        If span1 <> Default Then
            Console.WriteLine("Span is not Default")
        End If
    End Sub
    
    Public Sub TestMoreBadComparisons()
        Dim byteSpan As Span(Of Byte) = New Span(Of Byte)()
        Dim charSpan As Span(Of Char) = New Span(Of Char)()
        
        ' Violation: Byte span compared to Nothing
        If byteSpan = Nothing Then
            Console.WriteLine("Byte span is Nothing")
        End If
        
        ' Violation: Char span compared to Default
        If charSpan <> Default Then
            Console.WriteLine("Char span is not Default")
        End If
    End Sub
    
    ' GOOD: Proper Span(Of T) usage
    Public Sub TestGoodSpanUsage()
        Dim span1 As Span(Of Integer) = New Span(Of Integer)()
        
        ' Good: Check if span is empty
        If span1.IsEmpty Then
            Console.WriteLine("Span is empty")
        End If
        
        ' Good: Compare span length
        If span1.Length = 0 Then
            Console.WriteLine("Span has no elements")
        End If
        
        ' Good: Compare with Span.Empty
        If span1.Equals(Span(Of Integer).Empty) Then
            Console.WriteLine("Span equals empty span")
        End If
        
        ' Good: Use Span methods
        If span1.Length > 0 Then
            Console.WriteLine($"Span has {span1.Length} elements")
        End If
    End Sub
    
    Public Sub TestProperSpanChecks()
        Dim data As Integer() = {1, 2, 3, 4, 5}
        Dim span As Span(Of Integer) = data.AsSpan()
        
        ' Good: Proper span operations
        If Not span.IsEmpty Then
            Console.WriteLine($"First element: {span(0)}")
        End If
        
        ' Good: Length comparison
        If span.Length > 3 Then
            Console.WriteLine("Span has more than 3 elements")
        End If
    End Sub
End Class
