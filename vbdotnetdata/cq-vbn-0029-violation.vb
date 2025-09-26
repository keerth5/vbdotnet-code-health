' Test file for cq-vbn-0029: Do not declare protected members in sealed types
' Rule should detect NotInheritable (sealed) classes with Protected members

Imports System

' Violation 1: Public NotInheritable class with protected members
Public NotInheritable Class SealedClass1
    
    Private _value As String
    
    ' Protected method in sealed class - violation
    Protected Sub ProtectedMethod()
        Console.WriteLine("This method is protected but class is sealed")
    End Sub
    
    ' Protected function in sealed class - violation
    Protected Function ProtectedFunction() As String
        Return "protected function"
    End Function
    
    ' Protected property in sealed class - violation
    Protected Property ProtectedProperty As String
        Get
            Return _value
        End Get
        Set(value As String)
            _value = value
        End Set
    End Property
    
    ' Protected field in sealed class - violation
    Protected ProtectedField As Integer
    
    ' Public members are fine in sealed classes
    Public Sub PublicMethod()
        Console.WriteLine("Public method is fine")
    End Sub
    
    ' Private members are fine in sealed classes
    Private Sub PrivateMethod()
        Console.WriteLine("Private method is fine")
    End Sub
    
End Class

' Violation 2: Private NotInheritable class with protected members
Private NotInheritable Class SealedClass2
    
    ' Protected method in sealed class - violation
    Protected Sub AnotherProtectedMethod()
        Console.WriteLine("Another protected method")
    End Sub
    
End Class

' Violation 3: Friend NotInheritable class with protected members
Friend NotInheritable Class SealedClass3
    
    ' Protected property in sealed class - violation
    Protected Property Data As Integer
    
    ' Protected function in sealed class - violation
    Protected Function GetData() As Integer
        Return Data
    End Function
    
End Class

' This should NOT be detected - inheritable class with protected members
Public Class InheritableClass
    
    ' Protected members are fine in inheritable classes
    Protected Sub ProtectedMethod()
        Console.WriteLine("Protected method in inheritable class is fine")
    End Sub
    
    Protected Property ProtectedProperty As String
    
End Class

' This should NOT be detected - NotInheritable class without protected members
Public NotInheritable Class SealedClassWithoutProtected
    
    Public Sub PublicMethod()
        Console.WriteLine("Public method")
    End Sub
    
    Private Sub PrivateMethod()
        Console.WriteLine("Private method")
    End Sub
    
    Friend Sub FriendMethod()
        Console.WriteLine("Friend method")
    End Sub
    
End Class

' This should NOT be detected - MustInherit class with protected members
Public MustInherit Class AbstractClass
    
    ' Protected members are expected in abstract classes
    Protected MustOverride Sub AbstractMethod()
    
    Protected Overridable Sub VirtualMethod()
        Console.WriteLine("Virtual method")
    End Sub
    
End Class
