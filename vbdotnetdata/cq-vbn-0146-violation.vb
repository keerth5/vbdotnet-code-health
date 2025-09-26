' VB.NET test file for cq-vbn-0146: Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
' Rule: When code calls ToLower() or ToUpper() to perform a case-insensitive string comparison, an unnecessary allocation is performed.

Imports System

Public Class StringComparisonExamples
    
    Public Sub TestBasicToLowerComparisons()
        Dim text1 As String = "Hello World"
        Dim text2 As String = "HELLO WORLD"
        Dim text3 As String = "hello world"
        
        ' Violation: Using ToLower() for comparison
        If text1.ToLower() = text2.ToLower() Then
            Console.WriteLine("Texts are equal (case insensitive)")
        End If
        
        ' Violation: Using ToLower() with Equals method
        If text1.ToLower().Equals(text3.ToLower()) Then
            Console.WriteLine("Texts match")
        End If
        
        ' Violation: ToLower() in comparison chain
        If text1.ToLower() = text2.ToLower() AndAlso text2.ToLower() = text3.ToLower() Then
            Console.WriteLine("All texts are equal")
        End If
        
        ' Violation: ToLower() with Contains
        If text1.ToLower().Contains(text2.ToLower()) Then
            Console.WriteLine("Text1 contains text2")
        End If
        
        ' Violation: ToLower() with StartsWith
        If text1.ToLower().StartsWith(text3.ToLower()) Then
            Console.WriteLine("Text1 starts with text3")
        End If
        
        ' Violation: ToLower() with EndsWith
        If text1.ToLower().EndsWith(text2.ToLower()) Then
            Console.WriteLine("Text1 ends with text2")
        End If
    End Sub
    
    Public Sub TestBasicToUpperComparisons()
        Dim name1 As String = "Alice"
        Dim name2 As String = "alice"
        Dim name3 As String = "ALICE"
        
        ' Violation: Using ToUpper() for comparison
        If name1.ToUpper() = name2.ToUpper() Then
            Console.WriteLine("Names are equal (case insensitive)")
        End If
        
        ' Violation: Using ToUpper() with Equals method
        If name1.ToUpper().Equals(name3.ToUpper()) Then
            Console.WriteLine("Names match")
        End If
        
        ' Violation: ToUpper() with IndexOf
        If name1.ToUpper().IndexOf(name2.ToUpper()) >= 0 Then
            Console.WriteLine("Name1 contains name2")
        End If
        
        ' Violation: ToUpper() with Replace
        Dim result As String = name1.ToUpper().Replace(name2.ToUpper(), "REPLACEMENT")
        Console.WriteLine($"Replaced result: {result}")
    End Sub
    
    Public Sub TestToLowerInLoops()
        Dim names() As String = {"John", "JANE", "bob", "ALICE"}
        Dim searchName As String = "JOHN"
        
        ' Violation: ToLower() in For Each loop
        For Each name In names
            If name.ToLower() = searchName.ToLower() Then
                Console.WriteLine($"Found matching name: {name}")
            End If
        Next
        
        ' Violation: ToLower() in traditional For loop
        For i As Integer = 0 To names.Length - 1
            If names(i).ToLower().Contains(searchName.ToLower()) Then
                Console.WriteLine($"Name at index {i} matches")
            End If
        Next
        
        ' Violation: ToLower() in While loop
        Dim index As Integer = 0
        While index < names.Length
            If names(index).ToLower().StartsWith(searchName.ToLower()) Then
                Console.WriteLine($"Name {names(index)} starts with search term")
            End If
            index += 1
        End While
    End Sub
    
    Public Sub TestToUpperInCollections()
        Dim words As New List(Of String) From {"Apple", "BANANA", "cherry", "DATE"}
        Dim searchTerm As String = "apple"
        
        ' Violation: ToUpper() with LINQ Where
        Dim matches = words.Where(Function(w) w.ToUpper() = searchTerm.ToUpper()).ToList()
        Console.WriteLine($"Found {matches.Count} matches")
        
        ' Violation: ToUpper() with LINQ Any
        If words.Any(Function(w) w.ToUpper().Contains(searchTerm.ToUpper())) Then
            Console.WriteLine("Found at least one match")
        End If
        
        ' Violation: ToUpper() with LINQ First
        Try
            Dim first As String = words.First(Function(w) w.ToUpper() = searchTerm.ToUpper())
            Console.WriteLine($"First match: {first}")
        Catch ex As InvalidOperationException
            Console.WriteLine("No match found")
        End Try
    End Sub
    
    Public Sub TestMixedCaseComparisons()
        Dim text As String = "Mixed Case Text"
        Dim pattern As String = "MIXED case TEXT"
        
        ' Violation: Mixed ToLower() and ToUpper() usage
        If text.ToLower() = pattern.ToLower() Then
            Console.WriteLine("Mixed case comparison 1")
        End If
        
        If text.ToUpper() = pattern.ToUpper() Then
            Console.WriteLine("Mixed case comparison 2")
        End If
        
        ' Violation: ToLower() in string concatenation comparison
        If (text + " suffix").ToLower() = (pattern + " SUFFIX").ToLower() Then
            Console.WriteLine("Concatenated strings match")
        End If
        
        ' Violation: ToUpper() with Trim
        If text.Trim().ToUpper() = pattern.Trim().ToUpper() Then
            Console.WriteLine("Trimmed strings match")
        End If
    End Sub
    
    Public Sub TestToLowerToUpperInMethods()
        ' Violation: ToLower() in method calls
        Dim result1 As Boolean = CompareStringsLower("Hello", "HELLO")
        Dim result2 As Boolean = CompareStringsUpper("World", "world")
        
        Console.WriteLine($"Lower comparison: {result1}")
        Console.WriteLine($"Upper comparison: {result2}")
        
        ' Violation: ToLower() in return statements
        ' Return text1.ToLower() = text2.ToLower()
    End Sub
    
    Private Function CompareStringsLower(str1 As String, str2 As String) As Boolean
        ' Violation: ToLower() in method implementation
        Return str1.ToLower() = str2.ToLower()
    End Function
    
    Private Function CompareStringsUpper(str1 As String, str2 As String) As Boolean
        ' Violation: ToUpper() in method implementation
        Return str1.ToUpper() = str2.ToUpper()
    End Function
    
    Public Sub TestToLowerToUpperInConditionals()
        Dim input As String = "User Input"
        Dim command As String = "COMMAND"
        
        ' Violation: ToLower() in Select Case
        Select Case input.ToLower()
            Case command.ToLower()
                Console.WriteLine("Command matched")
            Case "other".ToLower()
                Console.WriteLine("Other matched")
        End Select
        
        ' Violation: ToUpper() in If-ElseIf chain
        If input.ToUpper() = "OPTION1".ToUpper() Then
            Console.WriteLine("Option 1")
        ElseIf input.ToUpper() = "OPTION2".ToUpper() Then
            Console.WriteLine("Option 2")
        ElseIf input.ToUpper().StartsWith("PREFIX".ToUpper()) Then
            Console.WriteLine("Starts with prefix")
        End If
    End Sub
    
    Public Sub TestToLowerToUpperWithStringMethods()
        Dim source As String = "Source String"
        Dim target As String = "TARGET string"
        Dim replacement As String = "REPLACEMENT"
        
        ' Violation: ToLower() with string methods
        If source.ToLower().Contains("source".ToLower()) Then
            Console.WriteLine("Contains check with ToLower")
        End If
        
        If source.ToLower().StartsWith("sour".ToLower()) Then
            Console.WriteLine("StartsWith check with ToLower")
        End If
        
        If source.ToLower().EndsWith("ring".ToLower()) Then
            Console.WriteLine("EndsWith check with ToLower")
        End If
        
        ' Violation: ToUpper() with Replace
        Dim replaced As String = source.ToUpper().Replace(target.ToUpper(), replacement.ToUpper())
        Console.WriteLine($"Replaced: {replaced}")
        
        ' Violation: ToLower() with IndexOf
        Dim index As Integer = source.ToLower().IndexOf(target.ToLower())
        Console.WriteLine($"Index: {index}")
        
        ' Violation: ToUpper() with LastIndexOf
        Dim lastIndex As Integer = source.ToUpper().LastIndexOf("S".ToUpper())
        Console.WriteLine($"Last index: {lastIndex}")
    End Sub
    
    Public Sub TestToLowerToUpperInVariableAssignments()
        Dim text1 As String = "Hello"
        Dim text2 As String = "HELLO"
        
        ' Violation: ToLower() in boolean variable assignment
        Dim isEqual As Boolean = text1.ToLower() = text2.ToLower()
        
        ' Violation: ToUpper() in string variable assignment
        Dim upperComparison As String = If(text1.ToUpper() = text2.ToUpper(), "Equal", "Not Equal")
        
        ' Violation: ToLower() in complex expression
        Dim result As Boolean = text1.ToLower().Length = text2.ToLower().Length AndAlso 
                               text1.ToLower() = text2.ToLower()
        
        Console.WriteLine($"Equal: {isEqual}, Comparison: {upperComparison}, Result: {result}")
    End Sub
    
    Public Sub TestToLowerToUpperWithNullChecks()
        Dim text1 As String = "Test"
        Dim text2 As String = Nothing
        
        ' Violation: ToLower() with null checks
        If text1 IsNot Nothing AndAlso text2 IsNot Nothing AndAlso 
           text1.ToLower() = text2.ToLower() Then
            Console.WriteLine("Both non-null and equal")
        End If
        
        ' Violation: ToUpper() with null coalescing
        Dim safeText1 As String = If(text1, "").ToUpper()
        Dim safeText2 As String = If(text2, "").ToUpper()
        
        If safeText1 = safeText2 Then
            Console.WriteLine("Safe comparison equal")
        End If
    End Sub
    
    Public Sub TestToLowerToUpperInStringInterpolation()
        Dim name As String = "John"
        Dim searchName As String = "JOHN"
        
        ' Violation: ToLower() in string interpolation
        Console.WriteLine($"Comparing {name.ToLower()} with {searchName.ToLower()}")
        
        ' Violation: ToUpper() in string interpolation
        Console.WriteLine($"Result: {If(name.ToUpper() = searchName.ToUpper(), "Match", "No match")}")
        
        ' Violation: ToLower() in formatted string
        Dim message As String = String.Format("Names {0}: {1} vs {2}", 
                                             If(name.ToLower() = searchName.ToLower(), "match", "differ"),
                                             name.ToLower(), searchName.ToLower())
        Console.WriteLine(message)
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        Dim text1 As String = "Hello World"
        Dim text2 As String = "HELLO WORLD"
        
        ' Correct: Using StringComparison overloads
        If text1.Equals(text2, StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Strings are equal (case insensitive)")
        End If
        
        If text1.StartsWith("hello", StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Starts with hello (case insensitive)")
        End If
        
        If text1.EndsWith("WORLD", StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Ends with WORLD (case insensitive)")
        End If
        
        If text1.Contains("WORLD", StringComparison.OrdinalIgnoreCase) Then
            Console.WriteLine("Contains WORLD (case insensitive)")
        End If
        
        If text1.IndexOf("HELLO", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Console.WriteLine("IndexOf with StringComparison")
        End If
        
        ' Correct: Using String.Compare
        If String.Compare(text1, text2, StringComparison.OrdinalIgnoreCase) = 0 Then
            Console.WriteLine("String.Compare with StringComparison")
        End If
    End Sub
    
    Public Sub TestMoreToLowerToUpperViolations()
        Dim filenames() As String = {"Document.txt", "IMAGE.JPG", "script.js"}
        Dim extension As String = ".TXT"
        
        ' Violation: ToLower() in file processing
        For Each filename In filenames
            If filename.ToLower().EndsWith(extension.ToLower()) Then
                Console.WriteLine($"Text file: {filename}")
            End If
        Next
        
        ' Violation: ToUpper() in configuration checking
        Dim configValue As String = "enabled"
        Dim expectedValue As String = "ENABLED"
        
        If configValue.ToUpper() = expectedValue.ToUpper() Then
            Console.WriteLine("Configuration is enabled")
        End If
        
        ' Violation: ToLower() in URL processing
        Dim url As String = "HTTP://EXAMPLE.COM/PATH"
        Dim protocol As String = "http"
        
        If url.ToLower().StartsWith(protocol.ToLower() + "://") Then
            Console.WriteLine("HTTP URL detected")
        End If
    End Sub
    
    Public Sub TestComplexToLowerToUpperScenarios()
        Dim userInput As String = "Admin"
        Dim allowedRoles() As String = {"ADMIN", "user", "Moderator"}
        
        ' Violation: ToLower() in role checking
        Dim hasRole As Boolean = allowedRoles.Any(Function(role) role.ToLower() = userInput.ToLower())
        Console.WriteLine($"User has role: {hasRole}")
        
        ' Violation: ToUpper() in dictionary key comparison
        Dim settings As New Dictionary(Of String, String)()
        settings.Add("DEBUG", "true")
        settings.Add("verbose", "false")
        
        Dim key As String = "debug"
        Dim hasKey As Boolean = settings.Keys.Any(Function(k) k.ToUpper() = key.ToUpper())
        Console.WriteLine($"Has key: {hasKey}")
    End Sub
End Class
