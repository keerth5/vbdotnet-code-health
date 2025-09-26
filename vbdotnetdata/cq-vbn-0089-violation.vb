' Test file for cq-vbn-0089: Identifiers should not match keywords
' Rule should detect identifiers that use VB.NET keywords as names

Imports System

' Violation 1: Class named with keyword
Public Class [If]
    Public Property Value As Boolean
End Class

' Violation 2: Class named with keyword
Public Class [Then]
    Public Property Action As String
End Class

' Violation 3: Class named with keyword
Friend Class [Else]
    Public Property Alternative As String
End Class

' Violation 4: Class named with keyword
Public Class [End]
    Public Property IsFinished As Boolean
End Class

' Violation 5: Class named with keyword
Public Class [For]
    Public Property LoopCount As Integer
End Class

Public Class KeywordExamples
    
    ' Violation 6: Method named with keyword
    Public Sub [Next]()
        Console.WriteLine("Next method")
    End Sub
    
    ' Violation 7: Method named with keyword
    Protected Sub [While]()
        Console.WriteLine("While method")
    End Sub
    
    ' Violation 8: Function named with keyword
    Public Function [Do]() As String
        Return "Do function"
    End Function
    
    ' Violation 9: Function named with keyword
    Friend Function [Loop]() As Integer
        Return 42
    End Function
    
    ' Violation 10: Property named with keyword
    Public Property [Select] As String
    
    ' Violation 11: Property named with keyword
    Protected Property [Case] As Integer
    
    ' Violation 12: Property named with keyword
    Friend Property [Dim] As Boolean
    
    Public Sub TestMethod(
        ByVal [As] As String,      ' Violation 13: Parameter named with keyword
        ByVal [New] As Integer,    ' Violation 14: Parameter named with keyword
        ByVal [Nothing] As Boolean ' Violation 15: Parameter named with keyword
    )
        Console.WriteLine([As])
        Console.WriteLine([New])
        Console.WriteLine([Nothing])
    End Sub
    
    Public Sub AnotherMethod(
        ByVal [True] As String,    ' Violation 16: Parameter named with keyword
        ByVal [False] As Integer   ' Violation 17: Parameter named with keyword
    )
        Console.WriteLine([True])
        Console.WriteLine([False])
    End Sub
    
    Public Sub MethodWithMoreKeywords(
        ByVal [Public] As String,    ' Violation 18: Parameter named with keyword
        ByVal [Private] As Integer,  ' Violation 19: Parameter named with keyword
        ByVal [Protected] As Boolean ' Violation 20: Parameter named with keyword
    )
        Console.WriteLine("Method with keyword parameters")
    End Sub
    
    ' This should NOT be detected - proper naming
    Public Sub ProcessData()
        Console.WriteLine("Proper method name")
    End Sub
    
    ' This should NOT be detected - proper naming
    Public Property UserName As String
    
    ' This should NOT be detected - proper naming
    Public Function CalculateTotal() As Double
        Return 0.0
    End Function
    
End Class

' Violation 21: Class named with keyword
Friend Class [Friend]
    Public Property Name As String
End Class

' Violation 22: Class named with keyword
Public Class [Shared]
    Public Property IsStatic As Boolean
End Class

' Violation 23: Class named with keyword
Public Class [Overrides]
    Public Property BaseValue As String
End Class

Public Class MoreKeywordExamples
    
    ' Violation 24: Method named with keyword
    Public Sub [Overridable]()
        Console.WriteLine("Overridable method")
    End Sub
    
    ' Violation 25: Method named with keyword
    Protected Sub [NotOverridable]()
        Console.WriteLine("NotOverridable method")
    End Sub
    
    ' Violation 26: Function named with keyword
    Public Function [MustOverride]() As String
        Return "MustOverride function"
    End Function
    
    ' Violation 27: Function named with keyword
    Friend Function [MustInherit]() As Boolean
        Return True
    End Function
    
    ' Violation 28: Property named with keyword
    Public Property [NotInheritable] As String
    
    ' Violation 29: Property named with keyword
    Protected Property [Shadows] As Integer
    
    ' Violation 30: Property named with keyword
    Friend Property [Overloads] As Boolean
    
End Class

' Violation 31: Class named with keyword
Public Class [Default]
    Public Property Value As String
End Class

' Violation 32: Class named with keyword
Friend Class [ReadOnly]
    Public Property Data As String
End Class

' Violation 33: Class named with keyword
Public Class [WriteOnly]
    Public Property Output As String
End Class

Public Class PropertyKeywordExamples
    
    ' Violation 34: Property named with keyword
    Public Property [Property] As String
    
    ' Violation 35: Property named with keyword
    Protected Property [Get] As Integer
    
    ' Violation 36: Property named with keyword
    Friend Property [Set] As Boolean
    
    ' Violation 37: Property named with keyword
    Public Property [Event] As String
    
    ' Violation 38: Property named with keyword
    Protected Property [RaiseEvent] As Integer
    
End Class

Public Class EventKeywordExamples
    
    ' Violation 39: Method named with keyword
    Public Sub [AddHandler]()
        Console.WriteLine("AddHandler method")
    End Sub
    
    ' Violation 40: Method named with keyword
    Protected Sub [RemoveHandler]()
        Console.WriteLine("RemoveHandler method")
    End Sub
    
    ' Violation 41: Function named with keyword
    Public Function [WithEvents]() As String
        Return "WithEvents function"
    End Function
    
    ' Violation 42: Function named with keyword
    Friend Function [Handles]() As Boolean
        Return True
    End Function
    
End Class

' Violation 43: Class named with keyword
Public Class [Implements]
    Public Property InterfaceName As String
End Class

' Violation 44: Class named with keyword
Friend Class [Inherits]
    Public Property BaseClassName As String
End Class

' Violation 45: Class named with keyword
Public Class [Interface]
    Public Property Definition As String
End Class

' This should NOT be detected - proper class naming
Public Class DataProcessor
    Public Sub ProcessInformation()
        Console.WriteLine("Processing information")
    End Sub
End Class

' This should NOT be detected - proper class naming
Friend Class ConfigurationManager
    Public Property Settings As String
End Class
