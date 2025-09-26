' VB.NET test file for cq-vbn-0131: Use char literal for a single character lookup
' Rule: Use String.Contains(char) instead of String.Contains(string) when searching for a single character.

Imports System

Public Class CharLiteralExamples
    
    Public Sub TestStringContainsMethods()
        Dim text As String = "Hello World"
        Dim name As String = "Alice"
        Dim path As String = "C:\temp\file.txt"
        
        ' Violation: Using Contains with single character string
        If text.Contains("H") Then
            Console.WriteLine("Found H")
        End If
        
        ' Violation: Using Contains with single character string
        If name.Contains("A") Then
            Console.WriteLine("Found A")
        End If
        
        ' Violation: Using Contains with single character string
        If path.Contains("\") Then
            Console.WriteLine("Found backslash")
        End If
        
        ' Violation: Using Contains with single character string in expression
        Dim hasSpace As Boolean = text.Contains(" ")
        
        ' Violation: Using Contains with single character string
        If text.Contains("o") Then
            Console.WriteLine("Found o")
        End If
        
        ' Violation: Using Contains with single character string
        If text.Contains("!") Then
            Console.WriteLine("Found exclamation")
        End If
        
        ' Violation: Using Contains with single character string
        If text.Contains("?") Then
            Console.WriteLine("Found question mark")
        End If
        
        ' Violation: Using Contains with single character string
        If text.Contains(".") Then
            Console.WriteLine("Found period")
        End If
        
        ' Violation: Using Contains with single character string
        If text.Contains(",") Then
            Console.WriteLine("Found comma")
        End If
        
        ' Violation: Using Contains with single character string
        If text.Contains(";") Then
            Console.WriteLine("Found semicolon")
        End If
    End Sub
    
    Public Sub TestIndexOfMethods()
        Dim text As String = "Hello World"
        Dim data As String = "abc123def"
        
        ' Violation: Using IndexOf with single character string
        Dim index1 As Integer = text.IndexOf("H")
        
        ' Violation: Using IndexOf with single character string
        Dim index2 As Integer = text.IndexOf("o")
        
        ' Violation: Using IndexOf with single character string
        Dim index3 As Integer = data.IndexOf("1")
        
        ' Violation: Using IndexOf with single character string
        If text.IndexOf("W") >= 0 Then
            Console.WriteLine("Found W")
        End If
        
        ' Violation: Using IndexOf with single character string
        If data.IndexOf("d") <> -1 Then
            Console.WriteLine("Found d")
        End If
        
        ' Violation: Using IndexOf with single character string
        Dim pos As Integer = text.IndexOf(" ")
        
        ' Violation: Using IndexOf with single character string
        Dim charPos As Integer = data.IndexOf("a")
    End Sub
    
    Public Sub TestValidCases()
        Dim text As String = "Hello World"
        
        ' Valid: Using Contains with multi-character string
        If text.Contains("Hello") Then
            Console.WriteLine("Found Hello")
        End If
        
        ' Valid: Using Contains with char literal (if VB.NET supports it)
        ' If text.Contains("H"c) Then
        '     Console.WriteLine("Found H")
        ' End If
        
        ' Valid: Using IndexOf with multi-character string
        Dim index As Integer = text.IndexOf("World")
        
        ' Valid: Using IndexOf with starting position
        Dim pos As Integer = text.IndexOf("o", 2)
    End Sub
    
    Public Sub TestVariousScenarios()
        Dim input As String = "test@example.com"
        Dim filename As String = "document.pdf"
        
        ' Violation: Single character search
        If input.Contains("@") Then
            Console.WriteLine("Email format")
        End If
        
        ' Violation: Single character search
        If filename.Contains(".") Then
            Console.WriteLine("Has extension")
        End If
        
        ' Violation: Single character search in method call
        ProcessText(input.Contains("-"))
        
        ' Violation: Single character search in return statement
        ' Return input.Contains("#")
    End Sub
    
    Private Sub ProcessText(hasChar As Boolean)
        ' Helper method
    End Sub
    
    Public Sub TestComplexExpressions()
        Dim text As String = "Hello, World!"
        
        ' Violation: Single character in complex expression
        Dim result As Boolean = text.Contains(",") AndAlso text.Contains("!")
        
        ' Violation: Single character in nested call
        If Not text.Contains("?") Then
            Console.WriteLine("No question mark")
        End If
        
        ' Violation: Single character with variable
        Dim searchChar As String = "H"
        ' This would be harder to detect with regex, but the pattern should catch literal strings
        If text.Contains("H") Then
            Console.WriteLine("Direct literal found")
        End If
    End Sub
    
    Public Sub TestEdgeCases()
        Dim text As String = "Sample text"
        
        ' Violation: Single character with escape sequences
        If text.Contains("""") Then
            Console.WriteLine("Found quote")
        End If
        
        ' Violation: Single character
        If text.Contains("'") Then
            Console.WriteLine("Found apostrophe")
        End If
        
        ' Violation: Single digit
        If text.Contains("1") Then
            Console.WriteLine("Found digit")
        End If
        
        ' Violation: Single letter
        If text.Contains("S") Then
            Console.WriteLine("Found S")
        End If
    End Sub
End Class
