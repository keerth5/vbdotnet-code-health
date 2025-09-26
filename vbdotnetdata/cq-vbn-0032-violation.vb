' Test file for cq-vbn-0032: Static holder types should be Static or NotInheritable
' Rule should detect classes with multiple static members that should be marked NotInheritable or Module

Imports System

' Violation 1: Public class with multiple static methods - should be NotInheritable or Module
Public Class UtilityClass1
    
    Public Shared Function CalculateArea(radius As Double) As Double
        Return Math.PI * radius * radius
    End Function
    
    Public Shared Function CalculateVolume(radius As Double, height As Double) As Double
        Return Math.PI * radius * radius * height
    End Function
    
    Public Shared Property DefaultTimeout As Integer = 30
    
End Class

' Violation 2: Friend class with multiple static members
Friend Class HelperClass1
    
    Public Shared Sub LogMessage(message As String)
        Console.WriteLine($"[{Date.Now}] {message}")
    End Sub
    
    Public Shared Function FormatString(input As String) As String
        Return input.Trim().ToUpper()
    End Function
    
    Public Shared Property ApplicationName As String = "MyApp"
    
End Class

' Violation 3: Another public class with static members
Public Class MathUtils
    
    Public Shared Function Add(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    Public Shared Function Multiply(a As Integer, b As Integer) As Integer
        Return a * b
    End Function
    
End Class

' This should NOT be detected - NotInheritable class (sealed)
Public NotInheritable Class SealedUtilityClass
    
    Public Shared Function GetCurrentTime() As Date
        Return Date.Now
    End Function
    
    Public Shared Function GetRandomNumber() As Integer
        Return New Random().Next()
    End Function
    
End Class

' This should NOT be detected - Module (VB.NET static class equivalent)
Public Module StringUtilities
    
    Public Function Reverse(input As String) As String
        Return New String(input.Reverse().ToArray())
    End Function
    
    Public Function CountWords(input As String) As Integer
        Return input.Split(" "c, StringSplitOptions.RemoveEmptyEntries).Length
    End Function
    
End Module

' This should NOT be detected - class with instance members
Public Class RegularClass
    
    Private _value As String
    
    Public Property Value As String
        Get
            Return _value
        End Get
        Set(val As String)
            _value = val
        End Set
    End Property
    
    Public Sub ProcessData()
        Console.WriteLine("Processing: " & _value)
    End Sub
    
End Class

' This should NOT be detected - class with only one static member
Public Class SingleStaticMember
    
    Public Shared Function GetVersion() As String
        Return "1.0.0"
    End Function
    
    Public Sub InstanceMethod()
        Console.WriteLine("Instance method")
    End Sub
    
End Class
