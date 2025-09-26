' Test file for cq-vbn-0030: Declare types in namespaces
' Rule should detect types declared outside of namespaces

Imports System

' Violation 1: Public class declared outside namespace
Public Class GlobalClass1
    Public Property Name As String
End Class

' Violation 2: Protected class declared outside namespace
Protected Class GlobalClass2
    Public Property Value As Integer
End Class

' Violation 3: Friend class declared outside namespace
Friend Class GlobalClass3
    Public Property Data As String
End Class

' Violation 4: Private class declared outside namespace
Private Class GlobalClass4
    Public Property Info As String
End Class

' Violation 5: Public structure declared outside namespace
Public Structure GlobalStruct1
    Public X As Integer
    Public Y As Integer
End Structure

' Violation 6: Protected structure declared outside namespace
Protected Structure GlobalStruct2
    Public Value As Double
End Structure

' Violation 7: Friend structure declared outside namespace
Friend Structure GlobalStruct3
    Public Name As String
End Structure

' Violation 8: Private structure declared outside namespace
Private Structure GlobalStruct4
    Public Count As Integer
End Structure

' Violation 9: Public enum declared outside namespace
Public Enum GlobalEnum1
    Option1
    Option2
    Option3
End Enum

' Violation 10: Protected enum declared outside namespace
Protected Enum GlobalEnum2
    Value1
    Value2
End Enum

' Violation 11: Friend enum declared outside namespace
Friend Enum GlobalEnum3
    Status1
    Status2
End Enum

' Violation 12: Private enum declared outside namespace
Private Enum GlobalEnum4
    Type1
    Type2
End Enum

' This section shows proper namespace usage - should NOT be detected
Namespace MyCompany.MyProject.Models
    
    ' These types are properly declared within a namespace
    Public Class NamespacedClass1
        Public Property Name As String
    End Class
    
    Protected Class NamespacedClass2
        Public Property Value As Integer
    End Class
    
    Friend Class NamespacedClass3
        Public Property Data As String
    End Class
    
    Private Class NamespacedClass4
        Public Property Info As String
    End Class
    
    Public Structure NamespacedStruct1
        Public X As Integer
        Public Y As Integer
    End Structure
    
    Public Enum NamespacedEnum1
        Option1
        Option2
    End Enum
    
End Namespace

Namespace AnotherNamespace
    
    Public Class AnotherNamespacedClass
        Public Property Description As String
    End Class
    
End Namespace
