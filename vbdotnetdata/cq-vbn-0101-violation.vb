' VB.NET test file for cq-vbn-0101: Avoid unsealed attributes
' Rule: .NET provides methods for retrieving custom attributes. By default, these methods search 
' the attribute inheritance hierarchy. Sealing the attribute eliminates the search through the 
' inheritance hierarchy and can improve performance.

Imports System

' Violation: Public attribute class that is not sealed (NotInheritable in VB.NET)
Public Class CustomAttribute
    Inherits Attribute
    
    Public Property Value As String
    
    Public Sub New()
    End Sub
    
    Public Sub New(value As String)
        Me.Value = value
    End Sub
End Class

' Violation: Friend attribute class that is not sealed
Friend Class InternalAttribute
    Inherits Attribute
    
    Public Property Name As String
End Class

' Violation: Public attribute with suffix that is not sealed
Public Class ValidationAttribute
    Inherits Attribute
    
    Public Property ErrorMessage As String
End Class

' Violation: Public attribute without suffix that is not sealed  
Public Class MyCustom
    Inherits Attribute
    
    Public Property Description As String
End Class

' Violation: Friend attribute with different naming pattern
Friend Class SecurityAttribute
    Inherits Attribute
    
    Public Property Level As Integer
End Class

' Violation: Public attribute with complex inheritance
Public Class BaseCustomAttribute
    Inherits Attribute
    
    Public Property BaseValue As String
End Class

' Violation: Another unsealed attribute
Public Class LoggingAttribute
    Inherits Attribute
    
    Public Property Category As String
    Public Property Level As String
End Class

' Violation: Friend attribute with multiple properties
Friend Class ConfigurationAttribute
    Inherits Attribute
    
    Public Property Section As String
    Public Property Key As String
    Public Property DefaultValue As Object
End Class

' Violation: Public attribute for documentation
Public Class DocumentationAttribute
    Inherits Attribute
    
    Public Property Summary As String
    Public Property Example As String
    Public Property Version As String
End Class

' Violation: Simple attribute without properties
Public Class MarkerAttribute
    Inherits Attribute
End Class

' Non-violation examples (these should not be detected):

' Sealed attribute (NotInheritable) - should not be detected
Public NotInheritable Class SealedCustomAttribute
    Inherits Attribute
    
    Public Property Value As String
End Class

' Non-attribute class - should not be detected
Public Class RegularClass
    Public Property Name As String
End Class

' Private class inheriting from Attribute - should not be detected by this pattern
Private Class PrivateAttribute
    Inherits Attribute
End Class

' Protected class inheriting from Attribute - should not be detected by this pattern  
Protected Class ProtectedAttribute
    Inherits Attribute
End Class
