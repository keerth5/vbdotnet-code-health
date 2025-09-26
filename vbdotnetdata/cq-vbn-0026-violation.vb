' Test file for cq-vbn-0026: Properties should not be write only
' Rule should detect properties that only have setters without getters

Imports System

Public Class WriteOnlyPropertyExamples
    
    Private _name As String
    Private _age As Integer
    Private _email As String
    Private _password As String
    
    ' Violation 1: Public write-only property
    Public WriteOnly Property Name As String
        Set(value As String)
            _name = value
        End Set
    End Property
    
    ' Violation 2: Protected write-only property
    Protected WriteOnly Property Age As Integer
        Set(value As Integer)
            _age = value
        End Set
    End Property
    
    ' Violation 3: Private write-only property
    Private WriteOnly Property Email As String
        Set(value As String)
            _email = value
        End Set
    End Property
    
    ' Violation 4: Friend write-only property
    Friend WriteOnly Property Password As String
        Set(value As String)
            _password = value
        End Set
    End Property
    
    ' This should NOT be detected - read-write property
    Public Property Address As String
        Get
            Return _address
        End Get
        Set(value As String)
            _address = value
        End Set
    End Property
    
    ' This should NOT be detected - read-only property
    Public ReadOnly Property FullName As String
        Get
            Return _name & " (Age: " & _age & ")"
        End Get
    End Property
    
    ' This should NOT be detected - auto-implemented property
    Public Property Phone As String
    
    Private _address As String
    
End Class

Public Class AnotherClass
    
    Private _value As Double
    
    ' Violation 5: Another write-only property
    Public WriteOnly Property Value As Double
        Set(val As Double)
            _value = val
        End Set
    End Property
    
End Class
