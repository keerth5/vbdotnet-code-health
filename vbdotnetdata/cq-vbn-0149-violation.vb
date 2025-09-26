' VB.NET test file for cq-vbn-0149: Use char overload
' Rule: The char overload is a better performing overload for a string with a single char.

Imports System
Imports System.Text

Public Class CharOverloadExamples
    
    Public Sub TestStringMethods()
        Dim text As String = "Hello World"
        Dim document As String = "This is a sample document with various punctuation marks."
        
        ' Violation: Using string overload instead of char overload for single character
        If text.StartsWith("H") Then
            Console.WriteLine("Text starts with H")
        End If
        
        ' Violation: EndsWith with single character string
        If text.EndsWith("d") Then
            Console.WriteLine("Text ends with d")
        End If
        
        ' Violation: Contains with single character string
        If text.Contains("o") Then
            Console.WriteLine("Text contains o")
        End If
        
        ' Violation: IndexOf with single character string
        Dim index As Integer = text.IndexOf("W")
        Console.WriteLine($"Index of W: {index}")
        
        ' Violation: LastIndexOf with single character string
        Dim lastIndex As Integer = document.LastIndexOf(".")
        Console.WriteLine($"Last index of period: {lastIndex}")
        
        ' Violation: Replace with single character strings
        Dim replaced As String = text.Replace(" ", "_")
        Console.WriteLine($"Replaced: {replaced}")
        
        ' Violation: Split with single character string
        Dim words() As String = text.Split(" ")
        Console.WriteLine($"Word count: {words.Length}")
        
        ' Violation: Trim with single character string
        Dim padded As String = "   Hello   "
        Dim trimmed As String = padded.Trim(" ")
        Console.WriteLine($"Trimmed: '{trimmed}'")
        
        ' Violation: TrimStart with single character string
        Dim leftTrimmed As String = padded.TrimStart(" ")
        Console.WriteLine($"Left trimmed: '{leftTrimmed}'")
        
        ' Violation: TrimEnd with single character string
        Dim rightTrimmed As String = padded.TrimEnd(" ")
        Console.WriteLine($"Right trimmed: '{rightTrimmed}'")
    End Sub
    
    Public Sub TestStringBuilderMethods()
        Dim sb As New StringBuilder()
        
        ' Violation: StringBuilder.Append with single character string
        sb.Append("H")
        sb.Append("e")
        sb.Append("l")
        sb.Append("l")
        sb.Append("o")
        
        ' Violation: StringBuilder.Insert with single character string
        sb.Insert(0, ">")
        sb.Insert(sb.Length, "<")
        
        ' Violation: StringBuilder.Replace with single character strings
        sb.Replace("l", "L")
        sb.Replace(" ", "_")
        
        Console.WriteLine($"StringBuilder result: {sb.ToString()}")
    End Sub
    
    Public Sub TestStringMethodsInLoops()
        Dim texts() As String = {"apple", "banana", "cherry", "date", "elderberry"}
        Dim searchChar As String = "a"
        
        ' Violation: Single character string in loop
        For Each text In texts
            If text.Contains(searchChar) Then
                Console.WriteLine($"'{text}' contains '{searchChar}'")
            End If
        Next
        
        ' Violation: Multiple single character operations in loop
        Dim sb As New StringBuilder()
        For i As Integer = 0 To 9
            sb.Append(i.ToString())
            sb.Append(",")  ' Violation: single character string
        Next
        
        ' Remove last comma
        If sb.Length > 0 Then
            sb.Remove(sb.Length - 1, 1)
        End If
        
        Console.WriteLine($"Numbers: {sb.ToString()}")
    End Sub
    
    Public Sub TestStringComparisonMethods()
        Dim text1 As String = "Hello"
        Dim text2 As String = "World"
        
        ' Violation: StartsWith with single character and StringComparison
        If text1.StartsWith("H", StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Starts with H (case insensitive)")
        End If
        
        ' Violation: EndsWith with single character and StringComparison
        If text2.EndsWith("d", StringComparison.Ordinal) Then
            Console.WriteLine("Ends with d")
        End If
        
        ' Violation: IndexOf with single character and StringComparison
        Dim pos As Integer = text1.IndexOf("e", StringComparison.CurrentCulture)
        Console.WriteLine($"Position of 'e': {pos}")
        
        ' Violation: LastIndexOf with single character and StringComparison
        Dim lastPos As Integer = text2.LastIndexOf("l", StringComparison.OrdinalIgnoreCase)
        Console.WriteLine($"Last position of 'l': {lastPos}")
    End Sub
    
    Public Sub TestStringMethodsWithStartIndex()
        Dim text As String = "Hello World Hello"
        
        ' Violation: IndexOf with single character and start index
        Dim firstIndex As Integer = text.IndexOf("l", 0)
        Dim secondIndex As Integer = text.IndexOf("l", firstIndex + 1)
        
        Console.WriteLine($"First 'l' at: {firstIndex}, Second 'l' at: {secondIndex}")
        
        ' Violation: IndexOf with single character, start index, and count
        Dim limitedIndex As Integer = text.IndexOf("o", 0, 10)
        Console.WriteLine($"'o' in first 10 characters at: {limitedIndex}")
        
        ' Violation: LastIndexOf with single character and start index
        Dim lastFromIndex As Integer = text.LastIndexOf("l", 10)
        Console.WriteLine($"Last 'l' before index 10: {lastFromIndex}")
    End Sub
    
    Public Sub TestReplaceOperations()
        Dim input As String = "a,b,c,d,e"
        Dim csvData As String = "1,2,3,4,5"
        
        ' Violation: Replace single character with another single character
        Dim pipeSeparated As String = input.Replace(",", "|")
        Console.WriteLine($"Pipe separated: {pipeSeparated}")
        
        ' Violation: Replace single character with empty string (removal)
        Dim withoutCommas As String = csvData.Replace(",", "")
        Console.WriteLine($"Without commas: {withoutCommas}")
        
        ' Violation: Replace single character with space
        Dim spaceSeparated As String = input.Replace(",", " ")
        Console.WriteLine($"Space separated: {spaceSeparated}")
        
        ' Violation: Multiple replace operations with single characters
        Dim cleaned As String = input.Replace(",", "").Replace("a", "A").Replace("e", "E")
        Console.WriteLine($"Cleaned: {cleaned}")
    End Sub
    
    Public Sub TestSplitOperations()
        Dim data As String = "apple,banana,cherry"
        Dim path As String = "C:\folder\subfolder\file.txt"
        Dim sentence As String = "This is a test sentence"
        
        ' Violation: Split with single character
        Dim fruits() As String = data.Split(",")
        Console.WriteLine($"Fruits count: {fruits.Length}")
        
        ' Violation: Split with single character (backslash)
        Dim pathParts() As String = path.Split("\")
        Console.WriteLine($"Path parts: {pathParts.Length}")
        
        ' Violation: Split with single character (space)
        Dim words() As String = sentence.Split(" ")
        Console.WriteLine($"Words count: {words.Length}")
        
        ' Violation: Split with single character and options
        Dim nonEmptyWords() As String = sentence.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        Console.WriteLine($"Non-empty words: {nonEmptyWords.Length}")
        
        ' Violation: Split with single character and count
        Dim limitedSplit() As String = data.Split(",", 2)
        Console.WriteLine($"Limited split parts: {limitedSplit.Length}")
    End Sub
    
    Public Sub TestTrimOperations()
        Dim text1 As String = "   hello   "
        Dim text2 As String = "...text..."
        Dim text3 As String = "###data###"
        
        ' Violation: Trim with single character
        Dim trimmed1 As String = text1.Trim(" ")
        Console.WriteLine($"Trimmed spaces: '{trimmed1}'")
        
        ' Violation: TrimStart with single character
        Dim leftTrimmed As String = text2.TrimStart(".")
        Console.WriteLine($"Left trimmed dots: '{leftTrimmed}'")
        
        ' Violation: TrimEnd with single character
        Dim rightTrimmed As String = text3.TrimEnd("#")
        Console.WriteLine($"Right trimmed hashes: '{rightTrimmed}'")
        
        ' Violation: Multiple trim operations
        Dim multiTrimmed As String = text1.Trim(" ").TrimStart("h").TrimEnd("o")
        Console.WriteLine($"Multi-trimmed: '{multiTrimmed}'")
    End Sub
    
    Public Sub TestStringBuilderCharOperations()
        Dim sb As New StringBuilder("Hello World")
        
        ' Violation: StringBuilder operations with single character strings
        sb.Append("!")
        sb.Insert(5, ",")
        sb.Replace("l", "L")
        sb.Replace(" ", "_")
        sb.Replace("!", ".")
        
        Console.WriteLine($"StringBuilder result: {sb.ToString()}")
        
        ' Violation: StringBuilder in loop with single characters
        Dim numbers As New StringBuilder()
        For i As Integer = 0 To 9
            numbers.Append(i.ToString())
            If i < 9 Then
                numbers.Append("-")  ' Violation
            End If
        Next
        
        Console.WriteLine($"Numbers with dashes: {numbers.ToString()}")
    End Sub
    
    Public Sub TestMethodChainingWithSingleCharacters()
        Dim input As String = "  hello world  "
        
        ' Violation: Method chaining with single character operations
        Dim processed As String = input.Trim(" ").Replace(" ", "_").Replace("l", "L")
        Console.WriteLine($"Processed: {processed}")
        
        ' Violation: Complex method chaining
        Dim complex As String = input.TrimStart(" ").TrimEnd(" ").Replace("o", "0").Replace("l", "1")
        Console.WriteLine($"Complex: {complex}")
    End Sub
    
    Public Sub TestConditionalSingleCharacterOperations()
        Dim text As String = "sample text"
        Dim condition As Boolean = True
        
        ' Violation: Single character operations in conditionals
        If condition Then
            If text.Contains("s") Then
                Console.WriteLine("Contains 's'")
            End If
        Else
            If text.EndsWith("t") Then
                Console.WriteLine("Ends with 't'")
            End If
        End If
        
        ' Violation: Ternary with single character operations
        Dim result As String = If(text.StartsWith("s"), text.Replace("s", "S"), text.Replace("t", "T"))
        Console.WriteLine($"Conditional result: {result}")
    End Sub
    
    Public Sub TestSingleCharacterInVariousContexts()
        Dim data() As String = {"a,b,c", "x,y,z", "1,2,3"}
        
        ' Violation: Single character operations in LINQ
        Dim processed = data.Select(Function(s) s.Replace(",", "|")).ToArray()
        
        For Each item In processed
            Console.WriteLine($"Processed item: {item}")
        Next
        
        ' Violation: Single character operations in method parameters
        ProcessText("hello world".Replace(" ", "_"))
        ProcessText("test,data".Split(",")(0))
    End Sub
    
    Private Sub ProcessText(text As String)
        Console.WriteLine($"Processing: {text}")
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        Dim text As String = "Hello World"
        Dim sb As New StringBuilder()
        
        ' Correct: Using char overloads
        If text.StartsWith("H"c) Then
            Console.WriteLine("Starts with H (char)")
        End If
        
        If text.EndsWith("d"c) Then
            Console.WriteLine("Ends with d (char)")
        End If
        
        If text.Contains("o"c) Then
            Console.WriteLine("Contains o (char)")
        End If
        
        Dim index As Integer = text.IndexOf("W"c)
        Console.WriteLine($"Index of W (char): {index}")
        
        ' Correct: StringBuilder with char
        sb.Append("H"c)
        sb.Append("e"c)
        sb.Append("l"c)
        sb.Append("l"c)
        sb.Append("o"c)
        
        ' Correct: Replace with char
        Dim replaced As String = text.Replace(" "c, "_"c)
        Console.WriteLine($"Replaced (char): {replaced}")
        
        ' Correct: Split with char
        Dim words() As String = text.Split(" "c)
        Console.WriteLine($"Words (char split): {words.Length}")
        
        ' Correct: Multi-character operations (should use string overload)
        If text.StartsWith("Hello") Then
            Console.WriteLine("Starts with Hello (multi-char)")
        End If
    End Sub
    
    Public Sub TestMoreSingleCharacterViolations()
        Dim log As New StringBuilder()
        Dim delimiter As String = "|"  ' Single character stored in string variable
        
        ' Violation: Even when single character is in variable
        log.Append("Entry1")
        log.Append(delimiter)  ' This is still a violation conceptually
        log.Append("Entry2")
        log.Append(delimiter)
        log.Append("Entry3")
        
        Console.WriteLine($"Log entries: {log.ToString()}")
        
        ' Violation: Single character operations in string formatting
        Dim formatted As String = String.Format("Value: {0}", "test".Replace("t", "T"))
        Console.WriteLine(formatted)
    End Sub
    
    Public Sub TestFilePathOperations()
        Dim filePath As String = "C:\Users\John\Documents\file.txt"
        
        ' Violation: Single character operations on file paths
        Dim unixPath As String = filePath.Replace("\", "/")
        Console.WriteLine($"Unix path: {unixPath}")
        
        ' Violation: Split path by directory separator
        Dim pathParts() As String = filePath.Split("\")
        Console.WriteLine($"Path has {pathParts.Length} parts")
        
        ' Violation: Check if path ends with specific character
        If filePath.EndsWith("t") Then
            Console.WriteLine("Path ends with 't'")
        End If
    End Sub
End Class
