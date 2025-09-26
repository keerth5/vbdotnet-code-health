' VB.NET test file for cq-vbn-0105: Test for empty strings using string length
' Rule: Comparing strings by using the String.Length property or the String.IsNullOrEmpty method 
' is significantly faster than using Equals.

Imports System

Public Class StringComparisonExamples
    
    Public Sub TestStringComparisons()
        Dim input As String = GetUserInput()
        Dim name As String = "John"
        Dim value As String = GetValue()
        
        ' Violation: Using Equals to compare with empty string
        If input.Equals("") Then
            Console.WriteLine("Input is empty")
        End If
        
        ' Violation: Using direct comparison with empty string
        If name = "" Then
            Console.WriteLine("Name is empty")
        End If
        
        ' Violation: Another Equals comparison with empty string
        If value.Equals("") Then
            ProcessEmptyValue()
        End If
        
        ' Violation: Using Equals with different variable
        If GetText().Equals("") Then
            HandleEmptyText()
        End If
        
        ' Violation: Direct comparison in different context
        If GetDescription() = "" Then
            SetDefaultDescription()
        End If
        
        ' Violation: Using Equals in conditional expression
        Dim isEmpty As Boolean = input.Equals("")
        
        ' Violation: Direct comparison in conditional expression
        Dim isNameEmpty As Boolean = name = ""
        
        ' Violation: Using Equals with property
        If Me.Title.Equals("") Then
            Me.Title = "Default Title"
        End If
        
        ' Violation: Direct comparison with property
        If Me.Description = "" Then
            Me.Description = "No description available"
        End If
        
        ' Violation: Using Equals with field
        If _data.Equals("") Then
            _data = "Default data"
        End If
        
        ' Violation: Direct comparison with field
        If _content = "" Then
            _content = "Empty content"
        End If
        
        ' Violation: Using Equals in method parameter
        ProcessString(input.Equals(""))
        
        ' Violation: Direct comparison in method parameter
        ValidateInput(name = "")
        
        ' Violation: Using Equals in return statement
        Return input.Equals("")
        
        ' Violation: Direct comparison in return statement
        Return value = ""
        
        ' Violation: Using Equals with static method result
        If GetStaticValue().Equals("") Then
            Console.WriteLine("Static value is empty")
        End If
        
        ' Violation: Direct comparison with static method result
        If GetDefaultText() = "" Then
            Console.WriteLine("Default text is empty")
        End If
        
    End Sub
    
    Private Function GetUserInput() As String
        Return Console.ReadLine()
    End Function
    
    Private Function GetValue() As String
        Return "test"
    End Function
    
    Private Function GetText() As String
        Return ""
    End Function
    
    Private Function GetDescription() As String
        Return "Sample description"
    End Function
    
    Private Sub ProcessEmptyValue()
        ' Method implementation
    End Sub
    
    Private Sub HandleEmptyText()
        ' Method implementation
    End Sub
    
    Private Sub SetDefaultDescription()
        ' Method implementation
    End Sub
    
    Private Sub ProcessString(isEmpty As Boolean)
        ' Method implementation
    End Sub
    
    Private Sub ValidateInput(isEmpty As Boolean)
        ' Method implementation
    End Sub
    
    Private Shared Function GetStaticValue() As String
        Return ""
    End Function
    
    Private Shared Function GetDefaultText() As String
        Return "Default"
    End Function
    
    Public Property Title As String
    Public Property Description As String
    Private _data As String
    Private _content As String
    
End Class

' More violation examples in different classes

Public Class ValidationClass
    
    Public Function IsEmpty(text As String) As Boolean
        ' Violation: Using Equals to check for empty string
        Return text.Equals("")
    End Function
    
    Public Function IsNotEmpty(text As String) As Boolean
        ' Violation: Using direct comparison to check for empty string
        Return Not (text = "")
    End Function
    
    Public Sub ProcessData()
        Dim data As String() = GetDataArray()
        
        For Each item As String In data
            ' Violation: Using Equals in loop
            If item.Equals("") Then
                Continue For
            End If
            
            ' Process non-empty items
            ProcessItem(item)
        Next
        
        For i As Integer = 0 To data.Length - 1
            ' Violation: Using direct comparison in loop
            If data(i) = "" Then
                data(i) = "Default"
            End If
        Next
    End Sub
    
    Private Function GetDataArray() As String()
        Return New String() {"item1", "", "item3", ""}
    End Function
    
    Private Sub ProcessItem(item As String)
        ' Method implementation
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperStringComparisons
    
    Public Sub TestProperComparisons()
        Dim input As String = GetInput()
        
        ' Correct: Using String.IsNullOrEmpty - should not be detected
        If String.IsNullOrEmpty(input) Then
            Console.WriteLine("Input is null or empty")
        End If
        
        ' Correct: Using Length property - should not be detected
        If input.Length = 0 Then
            Console.WriteLine("Input is empty")
        End If
        
        ' Correct: Using String.IsNullOrWhiteSpace - should not be detected
        If String.IsNullOrWhiteSpace(input) Then
            Console.WriteLine("Input is null, empty, or whitespace")
        End If
        
        ' Correct: Comparing with non-empty string - should not be detected
        If input.Equals("test") Then
            Console.WriteLine("Input is 'test'")
        End If
        
        ' Correct: Direct comparison with non-empty string - should not be detected
        If input = "value" Then
            Console.WriteLine("Input is 'value'")
        End If
        
    End Sub
    
    Private Function GetInput() As String
        Return "sample"
    End Function
    
End Class
