' Test file for cq-vbn-0031: Do not declare visible instance fields
' Rule should detect public and protected fields that should be properties instead

Imports System

Public Class FieldExamples
    
    ' Violation 1: Public field - should be a property
    Public Name As String
    
    ' Violation 2: Public field with initialization
    Public Age As Integer = 0
    
    ' Violation 3: Protected field - should be a property
    Protected Email As String
    
    ' Violation 4: Public field with WithEvents
    Public WithEvents Button1 As Button
    
    ' Violation 5: Protected field with complex type
    Protected Data As Dictionary(Of String, Object)
    
    ' This should NOT be detected - private field is acceptable
    Private _internalValue As String
    
    ' This should NOT be detected - friend field (internal) is less problematic
    Friend InternalData As String
    
    ' This should NOT be detected - proper property
    Public Property Address As String
        Get
            Return _address
        End Get
        Set(value As String)
            _address = value
        End Set
    End Property
    
    ' This should NOT be detected - readonly field is acceptable
    Public ReadOnly CreatedDate As Date = Date.Now
    
    ' This should NOT be detected - const field is acceptable
    Public Const MaxItems As Integer = 100
    
    Private _address As String
    
End Class

Public Structure StructureExample
    
    ' Violation 6: Public field in structure
    Public X As Double
    
    ' Violation 7: Protected field in structure
    Protected Y As Double
    
    ' This should NOT be detected - private field
    Private _z As Double
    
End Structure

Public Class AnotherClass
    
    ' Violation 8: Another public field
    Public Status As String
    
    ' Violation 9: Public field with events
    Public WithEvents Timer1 As Timer
    
End Class
