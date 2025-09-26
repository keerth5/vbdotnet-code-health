' Test file for cq-vbn-0024: Provide ObsoleteAttribute message
' Rule should detect Obsolete attributes without descriptive messages

Imports System

Public Class ObsoleteExamples
    
    ' Violation 1: Obsolete attribute without message
    <Obsolete()>
    Public Sub OldMethod1()
        Console.WriteLine("This method is obsolete")
    End Sub
    
    ' Violation 2: Another obsolete attribute without message
    <Obsolete()>
    Public Function OldFunction1() As String
        Return "obsolete"
    End Function
    
    ' Violation 3: Obsolete property without message
    <Obsolete()>
    Public Property OldProperty1 As String
    
    ' Violation 4: Obsolete attribute with empty parentheses
    <Obsolete()>
    Public Sub AnotherOldMethod()
        ' Implementation
    End Sub
    
    ' This should NOT be detected - Obsolete with message
    <Obsolete("Use NewMethod instead")>
    Public Sub OldMethodWithMessage()
        Console.WriteLine("This method has a message")
    End Sub
    
    ' This should NOT be detected - Obsolete with detailed message
    <Obsolete("This method is deprecated. Use the new ProcessData method instead.")>
    Public Function OldProcessing() As Boolean
        Return True
    End Function
    
    ' This should NOT be detected - Obsolete with message and error flag
    <Obsolete("This property is no longer supported", True)>
    Public Property DeprecatedProperty As Integer
    
    ' This should NOT be detected - no Obsolete attribute
    Public Sub CurrentMethod()
        Console.WriteLine("This is a current method")
    End Sub
    
End Class

' Violation 5: Obsolete class without message
<Obsolete()>
Public Class OldClass1
    Public Property Name As String
End Class

' This should NOT be detected - Obsolete class with message
<Obsolete("Use NewClass instead")>
Public Class OldClassWithMessage
    Public Property Value As String
End Class
