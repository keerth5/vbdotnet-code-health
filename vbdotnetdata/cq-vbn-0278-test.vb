' Test file for cq-vbn-0278: Provide correct arguments to formatting methods
' Rule should detect: String.Format and Console.WriteLine with mismatched format arguments

Imports System

Public Class BadFormattingClass
    
    ' VIOLATION: String.Format with format placeholder but no arguments
    Public Sub BadMethod1()
        Dim result As String = String.Format("Hello {0}!")
        Console.WriteLine(result)
    End Sub
    
    ' VIOLATION: Console.WriteLine with format placeholder but no arguments
    Public Sub BadMethod2()
        Console.WriteLine("Value is {0} and name is {1}")
    End Sub
    
    ' VIOLATION: String.Format with more placeholders than arguments
    Public Sub BadMethod3()
        Dim name As String = "John"
        Dim result As String = String.Format("Hello {0}, you are {1} years old!", name)
        Console.WriteLine(result)
    End Sub
    
    ' VIOLATION: Console.WriteLine with format placeholder but no arguments
    Public Sub BadMethod4()
        Dim count As Integer = 5
        Console.WriteLine("Count: {0}, Status: {1}")
    End Sub
    
    ' VIOLATION: String.Format with multiple placeholders but insufficient arguments
    Public Sub BadMethod5()
        Dim value1 As Integer = 10
        Dim result As String = String.Format("First: {0}, Second: {1}, Third: {2}", value1)
        Console.WriteLine(result)
    End Sub
    
    ' GOOD: String.Format with correct number of arguments - should NOT be flagged
    Public Sub GoodMethod1()
        Dim name As String = "John"
        Dim age As Integer = 25
        Dim result As String = String.Format("Hello {0}, you are {1} years old!", name, age)
        Console.WriteLine(result)
    End Sub
    
    ' GOOD: Console.WriteLine with correct arguments - should NOT be flagged
    Public Sub GoodMethod2()
        Dim count As Integer = 5
        Dim status As String = "Active"
        Console.WriteLine("Count: {0}, Status: {1}", count, status)
    End Sub
    
    ' GOOD: String without format placeholders - should NOT be flagged
    Public Sub GoodMethod3()
        Dim message As String = "Hello World!"
        Console.WriteLine(message)
    End Sub
    
    ' GOOD: String.Format without placeholders - should NOT be flagged
    Public Sub GoodMethod4()
        Dim result As String = String.Format("Simple message")
        Console.WriteLine(result)
    End Sub
    
    ' GOOD: Console.WriteLine with simple string - should NOT be flagged
    Public Sub GoodMethod5()
        Console.WriteLine("No formatting here")
    End Sub
    
End Class
