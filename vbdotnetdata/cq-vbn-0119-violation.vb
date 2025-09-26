' VB.NET test file for cq-vbn-0119: Use StringBuilder.Append(char) for single character strings
' Rule: StringBuilder has an Append overload that takes a char as its argument. 
' Prefer calling the char overload to improve performance.

Imports System
Imports System.Text

Public Class StringBuilderCharExamples
    
    Public Sub TestSingleCharacterAppend()
        Dim sb As New StringBuilder()
        
        ' Violation: Using Append with single character string instead of char
        sb.Append("A")
        
        ' Violation: Using Append with single character string - different character
        sb.Append("B")
        
        ' Violation: Using Append with single character string - digit
        sb.Append("1")
        
        ' Violation: Using Append with single character string - special character
        sb.Append("!")
        
        ' Violation: Using Append with single character string - space
        sb.Append(" ")
        
        ' Violation: Using Append with single character string - punctuation
        sb.Append(".")
        
        ' Violation: Using Append with single character string - symbol
        sb.Append("@")
        
        ' Violation: Using Append with single character string - bracket
        sb.Append("(")
        
        ' Violation: Using Append with single character string - bracket close
        sb.Append(")")
        
        ' Violation: Using Append with single character string - comma
        sb.Append(",")
        
        ' Violation: Using Append with single character string - semicolon
        sb.Append(";")
        
        ' Violation: Using Append with single character string - colon
        sb.Append(":")
        
        ' Violation: Using Append with single character string - quote
        sb.Append("""")
        
        ' Violation: Using Append with single character string - apostrophe
        sb.Append("'")
        
        ' Violation: Using Append with single character string - hyphen
        sb.Append("-")
        
        ' Violation: Using Append with single character string - underscore
        sb.Append("_")
        
        ' Violation: Using Append with single character string - equals
        sb.Append("=")
        
        ' Violation: Using Append with single character string - plus
        sb.Append("+")
        
        ' Violation: Using Append with single character string - asterisk
        sb.Append("*")
        
        ' Violation: Using Append with single character string - forward slash
        sb.Append("/")
        
    End Sub
    
    Public Sub TestSingleCharacterAppendLine()
        Dim sb As New StringBuilder()
        
        ' Violation: Using AppendLine with single character string instead of char
        sb.AppendLine("X")
        
        ' Violation: Using AppendLine with single character string - digit
        sb.AppendLine("9")
        
        ' Violation: Using AppendLine with single character string - letter
        sb.AppendLine("Z")
        
        ' Violation: Using AppendLine with single character string - special
        sb.AppendLine("#")
        
        ' Violation: Using AppendLine with single character string - percent
        sb.AppendLine("%")
        
    End Sub
    
    Public Sub BuildFormattedString()
        Dim sb As New StringBuilder()
        
        ' Violation: Multiple single character appends in formatting
        sb.Append("Name").Append(":").Append(" ").Append("John")
        sb.Append(",").Append(" ").Append("Age").Append(":").Append(" ").Append("30")
        
        ' Violation: Single character appends in loop-like structure
        sb.Append("[")
        For i As Integer = 1 To 5
            sb.Append(i.ToString())
            If i < 5 Then
                sb.Append(",")  ' Violation
            End If
        Next
        sb.Append("]")  ' Violation
    End Sub
    
    Public Function CreateDelimitedString() As String
        Dim sb As New StringBuilder()
        Dim items As String() = {"apple", "banana", "cherry", "date"}
        
        For i As Integer = 0 To items.Length - 1
            sb.Append(items(i))
            If i < items.Length - 1 Then
                ' Violation: Using single character string for delimiter
                sb.Append("|")
            End If
        Next
        
        Return sb.ToString()
    End Function
    
    Public Sub GenerateReport()
        Dim sb As New StringBuilder()
        
        ' Violation: Building report with single character strings
        sb.Append("=")  ' Header separator
        sb.AppendLine()
        sb.Append("REPORT TITLE")
        sb.AppendLine()
        sb.Append("=")  ' Header separator
        sb.AppendLine()
        sb.AppendLine()
        
        sb.Append("Section 1")
        sb.AppendLine()
        sb.Append("-")  ' Section separator
        sb.AppendLine()
        sb.Append("Content goes here")
        sb.AppendLine()
        sb.AppendLine()
        
        sb.Append("Section 2")
        sb.AppendLine()
        sb.Append("-")  ' Section separator
        sb.AppendLine()
        sb.Append("More content")
        sb.AppendLine()
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub CreateCsvLine()
        Dim sb As New StringBuilder()
        Dim values As String() = {"John", "Doe", "30", "Engineer"}
        
        For i As Integer = 0 To values.Length - 1
            sb.Append(values(i))
            If i < values.Length - 1 Then
                ' Violation: Using single character string for CSV comma
                sb.Append(",")
            End If
        Next
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub BuildJsonString()
        Dim sb As New StringBuilder()
        
        ' Violation: Building JSON with single character strings
        sb.Append("{")
        sb.Append("""name""").Append(":").Append(" ").Append("""John""")
        sb.Append(",")
        sb.Append(" ").Append("""age""").Append(":").Append(" ").Append("30")
        sb.Append("}")
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub CreateXmlElement()
        Dim sb As New StringBuilder()
        Dim tagName As String = "person"
        Dim content As String = "John Doe"
        
        ' Violation: Building XML with single character strings
        sb.Append("<").Append(tagName).Append(">")
        sb.Append(content)
        sb.Append("<").Append("/").Append(tagName).Append(">")
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub ProcessStringArray()
        Dim sb As New StringBuilder()
        Dim words As String() = {"Hello", "World", "From", "VB.NET"}
        
        ' Violation: Processing array with single character separators
        sb.Append("[")
        For Each word In words
            sb.Append("""").Append(word).Append("""")
            If word <> words(words.Length - 1) Then
                sb.Append(",").Append(" ")
            End If
        Next
        sb.Append("]")
        
        Console.WriteLine(sb.ToString())
    End Sub
    
End Class

' More violation examples in different contexts

Public Class LoggingExample
    
    Public Sub LogMessage(level As String, message As String)
        Dim sb As New StringBuilder()
        
        ' Violation: Building log entry with single character strings
        sb.Append("[").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("]")
        sb.Append(" ").Append(level).Append(":").Append(" ").Append(message)
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub CreateLogSeparator()
        Dim sb As New StringBuilder()
        
        ' Violation: Creating separator line with single character
        For i As Integer = 1 To 50
            sb.Append("-")
        Next
        
        Console.WriteLine(sb.ToString())
    End Sub
    
End Class

Public Class DataFormattingExample
    
    Public Function FormatTableRow(columns As String()) As String
        Dim sb As New StringBuilder()
        
        ' Violation: Formatting table with single character strings
        sb.Append("|")
        For Each column In columns
            sb.Append(" ").Append(column).Append(" ").Append("|")
        Next
        
        Return sb.ToString()
    End Function
    
    Public Sub CreateProgressBar(progress As Integer, total As Integer)
        Dim sb As New StringBuilder()
        Dim percentage As Integer = CInt((progress / total) * 100)
        Dim barLength As Integer = 20
        Dim filledLength As Integer = CInt((percentage / 100.0) * barLength)
        
        ' Violation: Building progress bar with single character strings
        sb.Append("[")
        For i As Integer = 1 To barLength
            If i <= filledLength Then
                sb.Append("=")
            Else
                sb.Append(" ")
            End If
        Next
        sb.Append("]").Append(" ").Append(percentage.ToString()).Append("%")
        
        Console.WriteLine(sb.ToString())
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperStringBuilderUsage
    
    Public Sub TestProperUsage()
        Dim sb As New StringBuilder()
        
        ' Correct: Using char literal - should not be detected
        sb.Append("A"c)
        sb.Append("B"c)
        sb.Append("1"c)
        sb.Append("!"c)
        
        ' Correct: Using AppendLine with char - should not be detected
        sb.AppendLine("X"c)
        sb.AppendLine("Z"c)
        
        ' Correct: Using multi-character strings - should not be detected
        sb.Append("Hello")
        sb.Append("World")
        sb.AppendLine("Multiple characters")
        
        ' Correct: Using empty string - should not be detected
        sb.Append("")
        sb.AppendLine("")
        
        ' Correct: Using other overloads - should not be detected
        sb.Append(123)
        sb.Append(45.67)
        sb.Append(True)
        
        Console.WriteLine(sb.ToString())
    End Sub
    
End Class
