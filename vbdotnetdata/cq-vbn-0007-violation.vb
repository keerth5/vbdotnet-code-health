' Test file for cq-vbn-0007: Abstract types should not have public constructors
' This file should trigger violations for abstract classes with public constructors

Imports System

' Violation: Abstract class with public constructor (single line pattern)
Public MustInherit Class AbstractBase : Public Sub New() : End Sub : End Class

' Violation: Abstract class with public parameterized constructor (single line pattern)
Protected MustInherit Class AbstractProcessor : Public Sub New(name As String) : End Sub : End Class

' Violation: Another abstract class with public constructor (single line pattern)  
Friend MustInherit Class AbstractHandler : Public Sub New(id As Integer, name As String) : End Sub : End Class

' Non-violation: Abstract class with protected constructor (should not trigger)
Public MustInherit Class ProperAbstractBase
    Protected Sub New()
        ' Protected constructor is proper for abstract class
    End Sub
    
    Public MustOverride Sub Execute()
End Class

' Non-violation: Abstract class with private constructor (should not trigger)
Public MustInherit Class PrivateConstructorAbstract
    Private Sub New()
        ' Private constructor
    End Sub
    
    Public MustOverride Sub Run()
End Class

' Non-violation: Concrete class with public constructor (should not trigger)
Public Class ConcreteClass
    Public Sub New()
        ' Public constructor in concrete class is fine
    End Sub
    
    Public Sub DoSomething()
        ' Implementation
    End Sub
End Class

' Non-violation: Abstract class with Friend constructor (should not trigger)
Public MustInherit Class FriendConstructorAbstract
    Friend Sub New()
        ' Friend constructor
    End Sub
    
    Public MustOverride Sub Work()
End Class
