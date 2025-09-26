' Test file for cq-vbn-0033: Static holder types should not have constructors
' Rule should detect modules with constructors (which is invalid in VB.NET)

Imports System

' Violation 1: Public module with constructor (this would be a compile error)
Public Module UtilityModule1
    
    ' This is invalid VB.NET - modules cannot have constructors
    Sub New()
        Console.WriteLine("Module constructor")
    End Sub
    
    Public Function Calculate(x As Integer) As Integer
        Return x * 2
    End Function
    
End Module

' Violation 2: Friend module with constructor
Friend Module HelperModule1
    
    ' Invalid constructor in module
    Sub New(value As String)
        Console.WriteLine("Helper constructor: " & value)
    End Sub
    
    Public Sub ProcessData()
        Console.WriteLine("Processing data")
    End Sub
    
End Module

' This should NOT be detected - regular class with constructor (valid)
Public Class RegularClass
    
    Private _name As String
    
    Public Sub New()
        _name = "Default"
    End Sub
    
    Public Sub New(name As String)
        _name = name
    End Sub
    
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
    
End Class

' This should NOT be detected - module without constructor (valid)
Public Module ValidModule
    
    Public Function GetCurrentTime() As String
        Return Date.Now.ToString()
    End Function
    
    Public Sub LogMessage(message As String)
        Console.WriteLine($"[{Date.Now}] {message}")
    End Sub
    
End Module

' This should NOT be detected - NotInheritable class with constructor (valid)
Public NotInheritable Class SealedClass
    
    Private _value As Integer
    
    Public Sub New(value As Integer)
        _value = value
    End Sub
    
    Public ReadOnly Property Value As Integer
        Get
            Return _value
        End Get
    End Property
    
End Class
