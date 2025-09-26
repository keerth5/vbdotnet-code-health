' Test file for cq-vbn-0094: Parameter names should match base declaration
' Rule should detect overridden methods with parameter names that don't match the base declaration

Imports System

Public MustInherit Class BaseClass
    
    Public MustOverride Sub ProcessData(ByVal inputData As String, ByVal options As Integer)
    
    Public MustOverride Function CalculateValue(ByVal amount As Decimal, ByVal rate As Double) As Decimal
    
    Public Overridable Sub HandleEvent(ByVal eventArgs As Object, ByVal context As String)
        Console.WriteLine("Base implementation")
    End Sub
    
    Public Overridable Function ValidateInput(ByVal data As String, ByVal rules As String()) As Boolean
        Return True
    End Function
    
End Class

Public Class DerivedClass
    Inherits BaseClass
    
    ' Violation 1: Override with different parameter names
    Public Overrides Sub ProcessData(ByVal differentName As String, ByVal differentOptions As Integer)
        Console.WriteLine("Processing with different parameter names")
    End Sub
    
    ' Violation 2: Override with different parameter names
    Public Overrides Function CalculateValue(ByVal value As Decimal, ByVal percentage As Double) As Decimal
        Return value * percentage
    End Function
    
    ' Violation 3: Override with different parameter names
    Public Overrides Sub HandleEvent(ByVal args As Object, ByVal ctx As String)
        Console.WriteLine("Handling event with different names")
    End Sub
    
    ' Violation 4: Override with different parameter names
    Public Overrides Function ValidateInput(ByVal input As String, ByVal validationRules As String()) As Boolean
        Return input.Length > 0
    End Function
    
End Class

Public MustInherit Class AnotherBaseClass
    
    Public MustOverride Sub SaveRecord(ByVal record As Object, ByVal fileName As String, ByVal overwrite As Boolean)
    
    Public Overridable Function LoadData(ByVal source As String, ByVal format As String) As Object
        Return Nothing
    End Function
    
End Class

Public Class AnotherDerivedClass
    Inherits AnotherBaseClass
    
    ' Violation 5: Override with different parameter names
    Public Overrides Sub SaveRecord(ByVal data As Object, ByVal file As String, ByVal replace As Boolean)
        Console.WriteLine("Saving record")
    End Sub
    
    ' Violation 6: Override with different parameter names
    Public Overrides Function LoadData(ByVal path As String, ByVal type As String) As Object
        Return New Object()
    End Function
    
End Class

Public Interface IProcessor
    Sub Execute(ByVal command As String, ByVal parameters As Object())
    Function GetResult(ByVal id As Integer, ByVal includeDetails As Boolean) As Object
End Interface

Public Class ProcessorImplementation
    Implements IProcessor
    
    ' Violation 7: Implementation with different parameter names
    Public Sub Execute(ByVal cmd As String, ByVal args As Object()) Implements IProcessor.Execute
        Console.WriteLine("Executing command")
    End Sub
    
    ' Violation 8: Implementation with different parameter names
    Public Function GetResult(ByVal identifier As Integer, ByVal withDetails As Boolean) As Object Implements IProcessor.GetResult
        Return Nothing
    End Function
    
End Class

Public MustInherit Class ThirdBaseClass
    
    Protected MustOverride Sub UpdateStatus(ByVal status As String, ByVal timestamp As DateTime, ByVal userId As Integer)
    
    Friend Overridable Function FormatMessage(ByVal message As String, ByVal level As Integer) As String
        Return message
    End Function
    
End Class

Public Class ThirdDerivedClass
    Inherits ThirdBaseClass
    
    ' Violation 9: Override with different parameter names
    Protected Overrides Sub UpdateStatus(ByVal state As String, ByVal time As DateTime, ByVal user As Integer)
        Console.WriteLine("Updating status")
    End Sub
    
    ' Violation 10: Override with different parameter names
    Friend Overrides Function FormatMessage(ByVal text As String, ByVal priority As Integer) As String
        Return text.ToUpper()
    End Function
    
End Class

' This should NOT be detected - proper parameter naming matching base
Public Class CorrectDerivedClass
    Inherits BaseClass
    
    Public Overrides Sub ProcessData(ByVal inputData As String, ByVal options As Integer)
        Console.WriteLine("Correct parameter names")
    End Sub
    
    Public Overrides Function CalculateValue(ByVal amount As Decimal, ByVal rate As Double) As Decimal
        Return amount * rate
    End Function
    
End Class

Public Interface ICorrectProcessor
    Sub Process(ByVal data As String)
End Interface

' This should NOT be detected - proper parameter naming matching interface
Public Class CorrectProcessorImplementation
    Implements ICorrectProcessor
    
    Public Sub Process(ByVal data As String) Implements ICorrectProcessor.Process
        Console.WriteLine("Processing data")
    End Sub
    
End Class

Public MustInherit Class FourthBaseClass
    
    Public MustOverride Sub Initialize(ByVal config As String, ByVal timeout As Integer)
    
End Class

Public Class FourthDerivedClass
    Inherits FourthBaseClass
    
    ' Violation 11: Override with different parameter names
    Public Overrides Sub Initialize(ByVal configuration As String, ByVal timeoutValue As Integer)
        Console.WriteLine("Initializing with different parameter names")
    End Sub
    
End Class
