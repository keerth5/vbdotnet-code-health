' Test file for cq-vbn-0012: Define accessors for attribute arguments
' Rule should detect attributes with constructor arguments but no property accessors

Imports System

' Violation 1: Attribute with constructor parameter but no accessor
Public Class ValidationAttribute
    Inherits Attribute
    
    Private _message As String
    
    Public Sub New(message As String)
        _message = message
    End Sub
    
    ' Missing property accessor for message
End Class

' Violation 2: Attribute with multiple constructor parameters but no accessors
Protected Class ConfigurationAttribute
    Inherits Attribute
    
    Private _name As String
    Private _value As String
    
    Public Sub New(name As String, value As String)
        _name = name
        _value = value
    End Sub
    
    ' Missing property accessors
End Class

' Violation 3: Attribute with constructor parameter and partial accessors
Friend Class SecurityAttribute
    Inherits Attribute
    
    Private _level As Integer
    Private _description As String
    
    Public Sub New(level As Integer, description As String)
        _level = level
        _description = description
    End Sub
    
    ' Only one accessor provided - still a violation for missing description accessor
    Public ReadOnly Property Level As Integer
        Get
            Return _level
        End Get
    End Property
End Class

' This should NOT be detected - attribute without constructor parameters
Public Class SimpleAttribute
    Inherits Attribute
End Class
