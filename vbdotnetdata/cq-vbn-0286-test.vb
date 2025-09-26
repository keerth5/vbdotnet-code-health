' VB.NET test file for cq-vbn-0286: Consider using String.Contains instead of String.IndexOf
' This rule detects IndexOf usage that can be replaced with Contains

Imports System

Public Class BadIndexOfUsage
    ' BAD: IndexOf used to check for substring presence
    Public Sub TestIndexOfForPresence()
        Dim text As String = "Hello World"
        
        ' Violation: IndexOf >= -1 (always true, should use Contains)
        If text.IndexOf("Hello") >= -1 Then
            Console.WriteLine("Found")
        End If
        
        ' Violation: IndexOf <> -1 (should use Contains)
        If text.IndexOf("World") <> -1 Then
            Console.WriteLine("Found")
        End If
        
        ' Violation: IndexOf > -1 (should use Contains)
        If text.IndexOf("test") > -1 Then
            Console.WriteLine("Found")
        End If
        
        ' Violation: IndexOf = -1 (should use Not Contains)
        If text.IndexOf("missing") = -1 Then
            Console.WriteLine("Not found")
        End If
    End Sub
    
    ' GOOD: Proper usage of IndexOf and Contains
    Public Sub TestCorrectUsage()
        Dim text As String = "Hello World"
        
        ' Good: Using Contains for presence check
        If text.Contains("Hello") Then
            Console.WriteLine("Found")
        End If
        
        ' Good: Using IndexOf for position
        Dim position As Integer = text.IndexOf("World")
        If position > 0 Then
            Console.WriteLine($"Found at position {position}")
        End If
        
        ' Good: Using IndexOf result directly
        Dim index As Integer = text.IndexOf("test")
        Console.WriteLine($"Index: {index}")
    End Sub
End Class
