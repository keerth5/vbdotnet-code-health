' Test file for cq-vbn-0021: Nested types should not be visible
' Rule should detect nested types that are public/protected/friend instead of private

Imports System

' Violation 1: Public class with public nested class
Public Class OuterClass1
    
    ' This nested class should be private - violation
    Public Class NestedClass1
        Public Property Name As String
    End Class
    
    ' This nested structure should be private - violation  
    Public Structure NestedStruct1
        Public Value As Integer
    End Structure
    
    ' This nested enum should be private - violation
    Public Enum NestedEnum1
        Option1
        Option2
    End Enum
    
    ' This nested interface should be private - violation
    Public Interface INestedInterface1
        Sub DoSomething()
    End Interface
    
End Class

' Violation 2: Protected class with protected nested types
Protected Class OuterClass2
    
    ' Protected nested class - violation
    Protected Class NestedClass2
        Public Property Data As String
    End Class
    
End Class

' Violation 3: Friend class with friend nested types
Friend Class OuterClass3
    
    ' Friend nested class - violation
    Friend Class NestedClass3
        Public Property Info As String
    End Class
    
End Class

' This should NOT be detected - private nested types are acceptable
Public Class OuterClass4
    
    ' Private nested types are fine - no violation
    Private Class PrivateNestedClass
        Public Property Value As String
    End Class
    
    Private Structure PrivateNestedStruct
        Public Data As Integer
    End Structure
    
End Class

' This should NOT be detected - no nested types
Public Class SimpleClass
    Public Property Name As String
End Class
