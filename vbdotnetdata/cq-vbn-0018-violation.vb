' Test file for cq-vbn-0018: Do not catch general exception types
' Rule should detect catch blocks using general Exception types

Imports System
Imports System.IO

Public Class ExceptionHandler
    
    Public Sub ProcessFile(filePath As String)
        Try
            ' Some file processing logic
            Dim content As String = File.ReadAllText(filePath)
            Console.WriteLine(content)
        ' Violation 1: Catching general Exception
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub
    
    Public Sub DatabaseOperation()
        Try
            ' Database operation
            Console.WriteLine("Executing database operation")
        ' Violation 2: Catching Exception without variable
        Catch Exception
            Console.WriteLine("Database error occurred")
        End Try
    End Sub
    
    Public Sub NetworkCall()
        Try
            ' Network operation
            Console.WriteLine("Making network call")
        ' Violation 3: Catching SystemException
        Catch ex As SystemException
            Console.WriteLine("System error: " & ex.Message)
        End Try
    End Sub
    
    Public Sub AnotherOperation()
        Try
            ' Some operation
            Console.WriteLine("Performing operation")
        ' Violation 4: Another general Exception catch
        Catch generalEx As Exception
            Console.WriteLine("General error: " & generalEx.Message)
        End Try
    End Sub
    
    Public Sub MultipleOperations()
        Try
            ' Multiple operations
            Console.WriteLine("Multiple operations")
        ' Violation 5: SystemException without variable
        Catch SystemException
            Console.WriteLine("System exception occurred")
        End Try
    End Sub
    
    ' This should NOT be detected - catching specific exception
    Public Sub SpecificExceptionHandling()
        Try
            Dim number As Integer = Integer.Parse("invalid")
        Catch ex As FormatException
            Console.WriteLine("Format error: " & ex.Message)
        Catch ex As OverflowException
            Console.WriteLine("Overflow error: " & ex.Message)
        End Try
    End Sub
    
    ' This should NOT be detected - no exception handling
    Public Sub NoExceptionHandling()
        Console.WriteLine("No try-catch here")
    End Sub
End Class
