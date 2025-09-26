' Test file for cq-vbn-0023: Avoid empty interfaces
' Rule should detect interfaces that have no members

Imports System

' Violation 1: Public empty interface
Public Interface IEmptyInterface1
End Interface

' Violation 2: Protected empty interface
Protected Interface IEmptyInterface2
End Interface

' Violation 3: Friend empty interface
Friend Interface IEmptyInterface3
End Interface

' Violation 4: Private empty interface
Private Interface IEmptyInterface4
End Interface

' Violation 5: Empty interface with only comments
Public Interface IEmptyInterface5
    ' This interface has no members
    ' It should be avoided
End Interface

' Violation 6: Empty interface with whitespace
Public Interface IEmptyInterface6

End Interface

' This should NOT be detected - interface with members
Public Interface IValidInterface1
    Sub DoSomething()
    Function GetValue() As String
    Property Name As String
End Interface

' This should NOT be detected - interface with single member
Public Interface IValidInterface2
    Sub Process()
End Interface

' This should NOT be detected - interface with property
Public Interface IValidInterface3
    Property Value As Integer
End Interface

' This should NOT be detected - interface with event
Public Interface IValidInterface4
    Event DataChanged As EventHandler
End Interface
