' Test file for cq-vbn-0011: Mark attributes with AttributeUsage
' Rule should detect custom attributes without AttributeUsage specification

Imports System

' Violation 1: Custom attribute without AttributeUsage
Public Class CustomValidationAttribute
    Inherits Attribute
    
    Public Sub New()
    End Sub
End Class

' Violation 2: Another custom attribute without AttributeUsage
Protected Class SecurityAttribute
    Inherits Attribute
    
    Private _level As Integer
    
    Public Sub New(level As Integer)
        _level = level
    End Sub
End Class

' Violation 3: Friend attribute without AttributeUsage
Friend Class LoggingAttribute
    Inherits Attribute
    
    Public Property Message As String
    
    Public Sub New(message As String)
        Me.Message = message
    End Sub
End Class

' Violation 4: Private attribute without AttributeUsage
Private Class InternalAttribute
    Inherits Attribute
End Class

' This should NOT be detected - not an attribute class
Public Class RegularClass
    Public Property Name As String
End Class
