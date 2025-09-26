' Test file for cq-vbn-0020: Interface methods should be callable by child types
' Rule should detect explicit interface implementations that are not accessible to derived types

Imports System

Public Interface IProcessor
    Sub ProcessData(data As String)
    Function GetResult() As String
End Interface

Public Interface IValidator
    Function Validate(input As String) As Boolean
End Interface

' Violation 1: Class with private explicit interface implementation
Public Class DataProcessor
    Implements IProcessor
    
    ' Private implementation - not accessible to derived types
    Private Sub ProcessData_IProcessor(data As String) Implements IProcessor.ProcessData
        Console.WriteLine("Processing: " & data)
    End Sub
    
    Private Function GetResult_IProcessor() As String Implements IProcessor.GetResult
        Return "Result"
    End Function
End Class

' Violation 2: Protected class with private interface methods
Protected Class ValidationProcessor
    Implements IValidator, IProcessor
    
    ' Private implementations - should be protected or public for inheritance
    Private Function Validate_IValidator(input As String) As Boolean Implements IValidator.Validate
        Return input.Length > 0
    End Function
    
    Private Sub ProcessData_IProcessor(data As String) Implements IProcessor.ProcessData
        Console.WriteLine("Validating and processing: " & data)
    End Sub
    
    Private Function GetResult_IProcessor() As String Implements IProcessor.GetResult
        Return "Validated result"
    End Function
End Class

' Violation 3: Friend class with private interface implementation
Friend Class InternalProcessor
    Implements IProcessor
    
    Private Sub ProcessData_IProcessor(data As String) Implements IProcessor.ProcessData
        Console.WriteLine("Internal processing: " & data)
    End Sub
    
    Private Function GetResult_IProcessor() As String Implements IProcessor.GetResult
        Return "Internal result"
    End Function
End Class

' This should NOT be detected - public interface implementations
Public Class PublicProcessor
    Implements IProcessor
    
    Public Sub ProcessData(data As String) Implements IProcessor.ProcessData
        Console.WriteLine("Public processing: " & data)
    End Sub
    
    Public Function GetResult() As String Implements IProcessor.GetResult
        Return "Public result"
    End Function
End Class

' This should NOT be detected - protected interface implementations
Public Class ProtectedProcessor
    Implements IProcessor
    
    Protected Sub ProcessData(data As String) Implements IProcessor.ProcessData
        Console.WriteLine("Protected processing: " & data)
    End Sub
    
    Protected Function GetResult() As String Implements IProcessor.GetResult
        Return "Protected result"
    End Function
End Class
