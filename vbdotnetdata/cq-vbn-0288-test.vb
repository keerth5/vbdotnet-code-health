' VB.NET test file for cq-vbn-0288: Use String.Equals over String.Compare
' This rule detects String.Compare usage that can be replaced with String.Equals

Imports System

Public Class BadStringCompare
    ' BAD: String.Compare used for equality check
    Public Sub TestStringCompareForEquality()
        Dim str1 As String = "Hello"
        Dim str2 As String = "World"
        
        ' Violation: String.Compare result compared to 0
        If String.Compare(str1, str2) = 0 Then
            Console.WriteLine("Strings are equal")
        End If
        
        ' Violation: String.Compare with options compared to 0
        If String.Compare(str1, str2, StringComparison.OrdinalIgnoreCase) = 0 Then
            Console.WriteLine("Strings are equal (ignore case)")
        End If
        
        ' Violation: CompareTo compared to 0
        If str1.CompareTo(str2) = 0 Then
            Console.WriteLine("Strings are equal")
        End If
        
        ' Violation: Another CompareTo usage
        If str1.CompareTo("test") = 0 Then
            Console.WriteLine("Equal to test")
        End If
    End Sub
    
    ' GOOD: Using String.Equals for equality checks
    Public Sub TestCorrectStringEquality()
        Dim str1 As String = "Hello"
        Dim str2 As String = "World"
        
        ' Good: Using String.Equals
        If String.Equals(str1, str2) Then
            Console.WriteLine("Strings are equal")
        End If
        
        ' Good: Using String.Equals with options
        If String.Equals(str1, str2, StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Strings are equal (ignore case)")
        End If
        
        ' Good: Using instance Equals
        If str1.Equals(str2) Then
            Console.WriteLine("Strings are equal")
        End If
        
        ' Good: Using String.Compare for ordering (not equality)
        If String.Compare(str1, str2) < 0 Then
            Console.WriteLine("str1 comes before str2")
        End If
    End Sub
End Class
