' Test file for cq-vbn-0280: Attribute string literals should parse correctly
' Rule should detect: Attributes with malformed URL, GUID, or version string literals

Imports System
Imports System.ComponentModel

' VIOLATION: Attribute with malformed URL
<Description("http://invalid-url-format")>
Public Class BadAttributeClass1
    
    Public Property Name As String
    
End Class

' VIOLATION: Attribute with malformed GUID
<Guid("12345678-1234-1234-1234-12345678901")>  ' Invalid GUID format
Friend Class BadAttributeClass2
    
    Public Property Value As Integer
    
End Class

' VIOLATION: Attribute with malformed version
<AssemblyVersion("1.2.3.4.5")>  ' Too many version parts
Public Class BadAttributeClass3
    
    Public Property Data As String
    
End Class

' VIOLATION: Custom attribute with malformed URI
<CustomUri("https://")>  ' Incomplete URI
Public Class BadAttributeClass4
    
    Public Property Info As String
    
End Class

' VIOLATION: Multiple attributes with parsing issues
<Description("urn:invalid:format"), Version("1.a.2")>
Public Structure BadAttributeStruct
    
    Public Value As Integer
    
End Structure

' GOOD: Attribute with valid URL - should NOT be flagged
<Description("https://www.example.com/docs")>
Public Class GoodAttributeClass1
    
    Public Property Name As String
    
End Class

' GOOD: Attribute with valid GUID - should NOT be flagged
<Guid("12345678-1234-1234-1234-123456789012")>
Public Class GoodAttributeClass2
    
    Public Property Value As Integer
    
End Class

' GOOD: Attribute with valid version - should NOT be flagged
<AssemblyVersion("1.2.3.4")>
Public Class GoodAttributeClass3
    
    Public Property Data As String
    
End Class

' GOOD: Attribute without URI/GUID/Version strings - should NOT be flagged
<Description("Simple description text")>
Public Class GoodAttributeClass4
    
    Public Property Info As String
    
End Class

' GOOD: No attributes - should NOT be flagged
Public Class NoAttributeClass
    
    Public Property Name As String
    Public Property Value As Integer
    
End Class

' Custom attribute for testing
Public Class CustomUriAttribute
    Inherits Attribute
    
    Public Sub New(uri As String)
        ' Constructor
    End Sub
    
End Class

Public Class VersionAttribute
    Inherits Attribute
    
    Public Sub New(version As String)
        ' Constructor
    End Sub
    
End Class
