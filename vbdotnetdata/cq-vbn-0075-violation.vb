' Test file for cq-vbn-0075: Use ArgumentException throw helper
' Rule should detect manual string validation that could use ArgumentException.ThrowIfNullOrEmpty

Imports System

Public Class ArgumentExceptionHelperExamples
    
    Public Sub ProcessString(input As String)
        ' Violation 1: Manual string empty check with ArgumentException
        If String.IsNullOrEmpty(input) Then
            Throw New ArgumentException("Input cannot be null or empty")
        End If
        
        Console.WriteLine(input.ToUpper())
    End Sub
    
    Public Sub ValidateParameter(parameter As String)
        ' Violation 2: Manual string empty check with ArgumentException
        If String.IsNullOrEmpty(parameter) Then
            Throw New ArgumentException("Parameter is required")
        End If
        
        Console.WriteLine("Parameter: " & parameter)
    End Sub
    
    Public Function ProcessName(name As String) As String
        ' Violation 3: Manual string empty check in function
        If String.IsNullOrEmpty(name) Then
            Throw New ArgumentException("Name cannot be empty")
        End If
        
        Return name.Trim()
    End Function
    
    ' This should NOT be detected - using modern throw helper (if available)
    Public Sub ModernStringCheck(value As String)
        ' ArgumentException.ThrowIfNullOrEmpty(value) ' Modern approach
        If Not String.IsNullOrEmpty(value) Then
            Console.WriteLine(value)
        End If
    End Sub
    
    Public Sub ProcessUserInput(userInput As String)
        ' Violation 4: User input validation
        If String.IsNullOrEmpty(userInput) Then
            Throw New ArgumentException("User input is required")
        End If
        
        ' Process the input
        Dim processed = userInput.Replace(" ", "_")
        Console.WriteLine(processed)
    End Sub
    
    ' This should NOT be detected - different exception type
    Public Sub DifferentException(input As String)
        If String.IsNullOrEmpty(input) Then
            Throw New InvalidOperationException("Input is required")
        End If
    End Sub
    
    ' This should NOT be detected - null check only
    Public Sub NullCheckOnly(input As String)
        If input Is Nothing Then
            Throw New ArgumentNullException("input")
        End If
    End Sub
    
    Public Sub ValidateMultipleStrings(first As String, second As String)
        ' Violation 5: First string validation
        If String.IsNullOrEmpty(first) Then
            Throw New ArgumentException("First parameter cannot be empty")
        End If
        
        ' Violation 6: Second string validation
        If String.IsNullOrEmpty(second) Then
            Throw New ArgumentException("Second parameter cannot be empty")
        End If
        
        Console.WriteLine(first & " " & second)
    End Sub
    
    Public Sub ProcessFileName(fileName As String)
        ' Violation 7: File name validation
        If String.IsNullOrEmpty(fileName) Then
            Throw New ArgumentException("File name is required")
        End If
        
        ' File processing logic
        Console.WriteLine("Processing file: " & fileName)
    End Sub
    
    ' This should NOT be detected - different condition
    Public Sub DifferentCondition(input As String)
        If input.Length = 0 Then
            Throw New ArgumentException("Input cannot be empty")
        End If
    End Sub
    
End Class

Public Class MoreStringValidationExamples
    
    Public Sub ValidateEmail(email As String)
        ' Violation 8: Email validation
        If String.IsNullOrEmpty(email) Then
            Throw New ArgumentException("Email address is required")
        End If
        
        ' Email processing
        Console.WriteLine("Email: " & email)
    End Sub
    
    Public Sub ProcessPath(path As String)
        ' Violation 9: Path validation
        If String.IsNullOrEmpty(path) Then
            Throw New ArgumentException("Path cannot be empty")
        End If
        
        ' Path processing
        Console.WriteLine("Processing path: " & path)
    End Sub
    
    Public Function FormatString(format As String) As String
        ' Violation 10: Format string validation
        If String.IsNullOrEmpty(format) Then
            Throw New ArgumentException("Format string is required")
        End If
        
        Return format.ToLower()
    End Function
    
    ' This should NOT be detected - IsNullOrWhiteSpace instead of IsNullOrEmpty
    Public Sub WhitespaceCheck(input As String)
        If String.IsNullOrWhiteSpace(input) Then
            Throw New ArgumentException("Input cannot be whitespace")
        End If
    End Sub
    
    Public Sub ValidateConnectionString(connectionString As String)
        ' Violation 11: Connection string validation
        If String.IsNullOrEmpty(connectionString) Then
            Throw New ArgumentException("Connection string cannot be empty")
        End If
        
        Console.WriteLine("Connection: " & connectionString)
    End Sub
    
End Class
