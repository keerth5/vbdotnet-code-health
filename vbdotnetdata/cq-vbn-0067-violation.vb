' Test file for cq-vbn-0067: Avoid excessive inheritance
' Rule should detect classes with inheritance that may indicate excessive inheritance depth

Imports System

' Base class
Public Class BaseClass
    Public Overridable Sub BaseMethod()
        Console.WriteLine("Base method")
    End Sub
End Class

' Violation 1: First level inheritance
Public Class DerivedClass1
    Inherits BaseClass
    
    Public Overrides Sub BaseMethod()
        Console.WriteLine("Derived1 method")
    End Sub
    
    Public Sub DerivedMethod1()
        Console.WriteLine("Derived1 specific method")
    End Sub
End Class

' Violation 2: Second level inheritance
Public Class DerivedClass2
    Inherits DerivedClass1
    
    Public Overrides Sub BaseMethod()
        Console.WriteLine("Derived2 method")
    End Sub
    
    Public Sub DerivedMethod2()
        Console.WriteLine("Derived2 specific method")
    End Sub
End Class

' Violation 3: Third level inheritance
Public Class DerivedClass3
    Inherits DerivedClass2
    
    Public Overrides Sub BaseMethod()
        Console.WriteLine("Derived3 method")
    End Sub
    
    Public Sub DerivedMethod3()
        Console.WriteLine("Derived3 specific method")
    End Sub
End Class

' Violation 4: Another inheritance chain - Animal hierarchy
Public Class Animal
    Public Overridable Sub MakeSound()
        Console.WriteLine("Generic animal sound")
    End Sub
End Class

' Violation 5: Mammal inherits from Animal
Public Class Mammal
    Inherits Animal
    
    Public Overrides Sub MakeSound()
        Console.WriteLine("Mammal sound")
    End Sub
    
    Public Overridable Sub GiveBirth()
        Console.WriteLine("Giving birth")
    End Sub
End Class

' Violation 6: Dog inherits from Mammal
Public Class Dog
    Inherits Mammal
    
    Public Overrides Sub MakeSound()
        Console.WriteLine("Woof!")
    End Sub
    
    Public Sub Bark()
        Console.WriteLine("Barking")
    End Sub
End Class

' Violation 7: Labrador inherits from Dog
Public Class Labrador
    Inherits Dog
    
    Public Overrides Sub MakeSound()
        Console.WriteLine("Friendly woof!")
    End Sub
    
    Public Sub Retrieve()
        Console.WriteLine("Retrieving")
    End Sub
End Class

' This should NOT be detected - class without inheritance
Public Class StandaloneClass
    
    Public Sub StandaloneMethod()
        Console.WriteLine("Standalone method")
    End Sub
    
End Class

' Violation 8: Vehicle hierarchy
Public Class Vehicle
    Public Overridable Sub Start()
        Console.WriteLine("Starting vehicle")
    End Sub
End Class

' Violation 9: Car inherits from Vehicle
Public Class Car
    Inherits Vehicle
    
    Public Overrides Sub Start()
        Console.WriteLine("Starting car engine")
    End Sub
    
    Public Sub Drive()
        Console.WriteLine("Driving")
    End Sub
End Class

' Violation 10: SportsCar inherits from Car
Public Class SportsCar
    Inherits Car
    
    Public Overrides Sub Start()
        Console.WriteLine("Starting sports car with roar")
    End Sub
    
    Public Sub Accelerate()
        Console.WriteLine("Accelerating quickly")
    End Sub
End Class

' Violation 11: Friend class with inheritance
Friend Class FriendBaseClass
    Public Overridable Sub FriendMethod()
        Console.WriteLine("Friend base method")
    End Sub
End Class

' Violation 12: Friend class inheriting from Friend class
Friend Class FriendDerivedClass
    Inherits FriendBaseClass
    
    Public Overrides Sub FriendMethod()
        Console.WriteLine("Friend derived method")
    End Sub
End Class

' This should NOT be detected - interface (not class inheritance)
Public Interface ITestInterface
    Sub InterfaceMethod()
End Interface

' This should NOT be detected - class implementing interface (not inheritance)
Public Class InterfaceImplementer
    Implements ITestInterface
    
    Public Sub InterfaceMethod() Implements ITestInterface.InterfaceMethod
        Console.WriteLine("Interface implementation")
    End Sub
End Class

' Violation 13: Abstract base class inheritance
Public MustInherit Class AbstractBase
    Public MustOverride Sub AbstractMethod()
End Class

' Violation 14: Concrete class inheriting from abstract
Public Class ConcreteImplementation
    Inherits AbstractBase
    
    Public Overrides Sub AbstractMethod()
        Console.WriteLine("Concrete implementation")
    End Sub
End Class

' This should NOT be detected - structure (not class)
Public Structure TestStructure
    Public Value As Integer
End Structure
