' Test file for cq-vbn-0160: Do not lock on objects with weak identity
' An object is said to have a weak identity when it can be directly accessed across application domain boundaries. A thread that tries to acquire a lock on an object that has a weak identity can be blocked by a second thread in a different application domain that has a lock on the same object.

Imports System
Imports System.Threading

Public Class WeakIdentityLockingTests
    
    ' Violation: Locking on Me (this instance)
    Public Sub LockOnMe()
        ' Violation: SyncLock on Me
        SyncLock Me
            Console.WriteLine("Locked on Me (this instance)")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Locking on MyBase
    Public Sub LockOnMyBase()
        ' Violation: SyncLock on MyBase
        SyncLock MyBase
            Console.WriteLine("Locked on MyBase")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Locking on MyClass
    Public Sub LockOnMyClass()
        ' Violation: SyncLock on MyClass
        SyncLock MyClass
            Console.WriteLine("Locked on MyClass")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Locking on Type
    Public Sub LockOnType()
        ' Violation: SyncLock on Type
        SyncLock Type(WeakIdentityLockingTests)
            Console.WriteLine("Locked on Type")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Locking on GetType
    Public Sub LockOnGetType()
        ' Violation: SyncLock on GetType result
        SyncLock GetType(WeakIdentityLockingTests)
            Console.WriteLine("Locked on GetType result")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Locking on string literal
    Public Sub LockOnStringLiteral()
        ' Violation: SyncLock on string literal
        SyncLock "StringLiteral"
            Console.WriteLine("Locked on string literal")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Locking on object's GetType
    Public Sub LockOnObjectGetType()
        Dim obj As New Object()
        
        ' Violation: SyncLock on object's GetType
        SyncLock obj.GetType()
            Console.WriteLine("Locked on object's GetType")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Locking on another object's GetType
    Public Sub LockOnAnotherObjectGetType()
        Dim list As New List(Of String)
        
        ' Violation: SyncLock on list's GetType
        SyncLock list.GetType()
            Console.WriteLine("Locked on list's GetType")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Good practice: Locking on private object (should not be detected)
    Private ReadOnly lockObject As New Object()
    
    Public Sub LockOnPrivateObject()
        ' Good: SyncLock on private object
        SyncLock lockObject
            Console.WriteLine("Locked on private object")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Good: Locking on local object
    Public Sub LockOnLocalObject()
        Dim localLock As New Object()
        
        ' Good: SyncLock on local object
        SyncLock localLock
            Console.WriteLine("Locked on local object")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Multiple weak identity locks
    Public Sub MultipleLockViolations()
        ' Violation: First lock on Me
        SyncLock Me
            Console.WriteLine("First lock on Me")
            
            ' Violation: Nested lock on string literal
            SyncLock "NestedString"
                Console.WriteLine("Nested lock on string literal")
                Thread.Sleep(50)
            End SyncLock
        End SyncLock
    End Sub
    
    ' Violation: Lock in loop
    Public Sub LockInLoop()
        For i As Integer = 0 To 4
            ' Violation: SyncLock on Me in loop
            SyncLock Me
                Console.WriteLine($"Iteration {i} - locked on Me")
                Thread.Sleep(10)
            End SyncLock
        Next
    End Sub
    
    ' Violation: Conditional locking on weak identity
    Public Sub ConditionalLocking(useWeakLock As Boolean)
        If useWeakLock Then
            ' Violation: SyncLock on Type in conditional
            SyncLock Type(String)
                Console.WriteLine("Conditional lock on String type")
                Thread.Sleep(100)
            End SyncLock
        Else
            ' Good: Lock on private object
            SyncLock lockObject
                Console.WriteLine("Conditional lock on private object")
                Thread.Sleep(100)
            End SyncLock
        End If
    End Sub
    
End Class

' Shared/Static context violations
Public Class SharedContextLocking
    
    ' Violation: Shared method locking on Type
    Public Shared Sub SharedLockOnType()
        ' Violation: SyncLock on Type in shared method
        SyncLock Type(SharedContextLocking)
            Console.WriteLine("Shared method locked on Type")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Shared method locking on GetType
    Public Shared Sub SharedLockOnGetType()
        Dim obj As New Object()
        
        ' Violation: SyncLock on GetType in shared method
        SyncLock obj.GetType()
            Console.WriteLine("Shared method locked on GetType")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Good: Shared method with private static lock object
    Private Shared ReadOnly sharedLockObject As New Object()
    
    Public Shared Sub SharedLockOnPrivateObject()
        ' Good: SyncLock on private shared object
        SyncLock sharedLockObject
            Console.WriteLine("Shared method locked on private object")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
End Class

' Module with locking violations
Public Module LockingModule
    
    ' Violation: Module method locking on Type
    Public Sub ModuleLockOnType()
        ' Violation: SyncLock on Type in module
        SyncLock Type(LockingModule)
            Console.WriteLine("Module method locked on Type")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Module method locking on string
    Public Sub ModuleLockOnString()
        ' Violation: SyncLock on string literal in module
        SyncLock "ModuleString"
            Console.WriteLine("Module method locked on string")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
End Module

' Inheritance scenario
Public Class BaseClass
    
    ' Violation: Base class locking on Me
    Public Overridable Sub BaseLockOnMe()
        ' Violation: SyncLock on Me in base class
        SyncLock Me
            Console.WriteLine("Base class locked on Me")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
End Class

Public Class DerivedClass
    Inherits BaseClass
    
    ' Violation: Derived class locking on MyBase
    Public Overrides Sub BaseLockOnMe()
        ' Violation: SyncLock on MyBase in override
        SyncLock MyBase
            Console.WriteLine("Derived class locked on MyBase")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Derived class locking on Me
    Public Sub DerivedLockOnMe()
        ' Violation: SyncLock on Me in derived class
        SyncLock Me
            Console.WriteLine("Derived class locked on Me")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
End Class

' Generic class with locking violations
Public Class GenericLocking(Of T)
    
    ' Violation: Generic class locking on GetType
    Public Sub GenericLockOnGetType()
        ' Violation: SyncLock on GetType in generic class
        SyncLock GetType(T)
            Console.WriteLine($"Generic class locked on GetType of {GetType(T).Name}")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
    ' Violation: Generic class locking on Me
    Public Sub GenericLockOnMe()
        ' Violation: SyncLock on Me in generic class
        SyncLock Me
            Console.WriteLine("Generic class locked on Me")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
End Class

' Interface implementation with locking
Public Interface ILockingInterface
    Sub DoWork()
End Interface

Public Class InterfaceImplementation
    Implements ILockingInterface
    
    ' Violation: Interface implementation locking on Me
    Public Sub DoWork() Implements ILockingInterface.DoWork
        ' Violation: SyncLock on Me in interface implementation
        SyncLock Me
            Console.WriteLine("Interface implementation locked on Me")
            Thread.Sleep(100)
        End SyncLock
    End Sub
    
End Class

' Nested class with locking violations
Public Class OuterClass
    
    Public Class NestedClass
        
        ' Violation: Nested class locking on Me
        Public Sub NestedLockOnMe()
            ' Violation: SyncLock on Me in nested class
            SyncLock Me
                Console.WriteLine("Nested class locked on Me")
                Thread.Sleep(100)
            End SyncLock
        End Sub
        
        ' Violation: Nested class locking on Type
        Public Sub NestedLockOnType()
            ' Violation: SyncLock on Type in nested class
            SyncLock Type(NestedClass)
                Console.WriteLine("Nested class locked on Type")
                Thread.Sleep(100)
            End SyncLock
        End Sub
        
    End Class
    
End Class
