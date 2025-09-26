' Test file for cq-vbn-0079: Make types internal by default
' Rule should detect public types that could be made internal

Imports System

' Violation 1: Public class that could be internal
Public Class PublicUtilityClass
    
    Public Shared Function CalculateSum(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    Public Shared Sub LogMessage(message As String)
        Console.WriteLine(message)
    End Sub
    
End Class

' Violation 2: Public structure that could be internal
Public Structure PublicDataStructure
    
    Public Value As Integer
    Public Name As String
    
    Public Sub Initialize()
        Value = 0
        Name = ""
    End Sub
    
End Structure

' This should NOT be detected - Friend (internal) class
Friend Class InternalHelperClass
    
    Public Sub DoInternalWork()
        Console.WriteLine("Internal work")
    End Sub
    
End Class

' This should NOT be detected - Private class
Private Class PrivateImplementationClass
    
    Public Sub DoPrivateWork()
        Console.WriteLine("Private work")
    End Sub
    
End Class

' Violation 3: Another public class
Public Class PublicServiceClass
    
    Private _data As String
    
    Public Property Data As String
        Get
            Return _data
        End Get
        Set(value As String)
            _data = value
        End Set
    End Property
    
    Public Sub ProcessData()
        If Not String.IsNullOrEmpty(_data) Then
            Console.WriteLine("Processing: " & _data)
        End If
    End Sub
    
End Class

' Violation 4: Public class with generic parameter
Public Class PublicGenericClass(Of T)
    
    Private _items As List(Of T)
    
    Public Sub New()
        _items = New List(Of T)()
    End Sub
    
    Public Sub AddItem(item As T)
        _items.Add(item)
    End Sub
    
    Public Function GetItems() As List(Of T)
        Return _items
    End Function
    
End Class

' This should NOT be detected - Interface (different scope rules)
Public Interface IPublicInterface
    
    Sub DoSomething()
    Function GetValue() As Integer
    
End Interface

' Violation 5: Public structure with methods
Public Structure PublicPointStructure
    
    Public X As Double
    Public Y As Double
    
    Public Sub New(x As Double, y As Double)
        Me.X = x
        Me.Y = y
    End Sub
    
    Public Function DistanceFromOrigin() As Double
        Return Math.Sqrt(X * X + Y * Y)
    End Function
    
End Structure

' This should NOT be detected - Module (different declaration)
Public Module PublicUtilityModule
    
    Public Sub ModuleMethod()
        Console.WriteLine("Module method")
    End Sub
    
End Module

' Violation 6: Public class with inheritance
Public Class PublicBaseClass
    
    Public Overridable Sub VirtualMethod()
        Console.WriteLine("Base virtual method")
    End Sub
    
End Class

' Violation 7: Public class inheriting from public class
Public Class PublicDerivedClass
    Inherits PublicBaseClass
    
    Public Overrides Sub VirtualMethod()
        Console.WriteLine("Derived virtual method")
    End Sub
    
End Class

' This should NOT be detected - Friend structure
Friend Structure InternalDataStructure
    
    Public Value As Integer
    Public Name As String
    
End Structure

' Violation 8: Public class with events
Public Class PublicEventClass
    
    Public Event DataChanged As EventHandler(Of EventArgs)
    
    Private _data As String
    
    Public Property Data As String
        Get
            Return _data
        End Get
        Set(value As String)
            _data = value
            RaiseEvent DataChanged(Me, EventArgs.Empty)
        End Set
    End Property
    
End Class

' Violation 9: Public class implementing interface
Public Class PublicImplementationClass
    Implements IPublicInterface
    
    Public Sub DoSomething() Implements IPublicInterface.DoSomething
        Console.WriteLine("Doing something")
    End Sub
    
    Public Function GetValue() As Integer Implements IPublicInterface.GetValue
        Return 42
    End Function
    
End Class

' This should NOT be detected - Abstract class (special case)
Public MustInherit Class AbstractBaseClass
    
    Public MustOverride Sub AbstractMethod()
    
    Public Overridable Sub VirtualMethod()
        Console.WriteLine("Abstract base virtual method")
    End Sub
    
End Class

' Violation 10: Public class with static members
Public Class PublicStaticUtilities
    
    Public Shared ReadOnly DefaultTimeout As Integer = 30000
    
    Public Shared Function FormatMessage(message As String) As String
        Return "[" & DateTime.Now.ToString() & "] " & message
    End Function
    
    Public Shared Sub LogError(error As Exception)
        Console.WriteLine("Error: " & error.Message)
    End Sub
    
End Class
