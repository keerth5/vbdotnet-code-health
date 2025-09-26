' Test file for cq-vbn-0063: Provide a parameterless constructor for COM interop
' Rule should detect ComVisible classes that may need parameterless constructors

Imports System
Imports System.Runtime.InteropServices

' Violation 1: ComVisible class without explicit parameterless constructor
<ComVisible(True)>
Public Class ComVisibleClass1
    
    Public Sub New(value As Integer)
        ' Only parameterized constructor - violation
    End Sub
    
    Public Sub DoSomething()
        Console.WriteLine("COM visible functionality")
    End Sub
    
End Class

' Violation 2: ComVisible class with only private parameterless constructor
<ComVisible(True)>
Public Class ComVisibleClass2
    
    Private Sub New()
        ' Private constructor - violation for COM interop
    End Sub
    
    Public Sub New(name As String)
        ' Only public parameterized constructor
    End Sub
    
    Public Sub ProcessData()
        Console.WriteLine("Processing data")
    End Sub
    
End Class

' This should NOT be detected - ComVisible class with public parameterless constructor
<ComVisible(True)>
Public Class ComVisibleClass3
    
    Public Sub New()
        ' Public parameterless constructor - compliant
    End Sub
    
    Public Sub New(value As Integer)
        ' Additional parameterized constructor is fine
    End Sub
    
    Public Sub Execute()
        Console.WriteLine("Executing")
    End Sub
    
End Class

' Violation 3: ComVisible Friend class
<ComVisible(True)>
Friend Class ComVisibleFriendClass
    
    Public Sub New(data As String, count As Integer)
        ' Only parameterized constructor - violation
    End Sub
    
    Public Sub HandleData()
        Console.WriteLine("Handling data")
    End Sub
    
End Class

' This should NOT be detected - class not marked as ComVisible
Public Class RegularClass
    
    Public Sub New(value As Integer)
        ' Not ComVisible, so no requirement for parameterless constructor
    End Sub
    
    Public Sub RegularMethod()
        Console.WriteLine("Regular method")
    End Sub
    
End Class

' Violation 4: ComVisible class with complex constructor requirements
<ComVisible(True)>
Public Class ComVisibleClass4
    
    Public Sub New(param1 As String, param2 As Integer, param3 As Boolean)
        ' Complex constructor - violation
    End Sub
    
    Public Sub ComplexOperation()
        Console.WriteLine("Complex operation")
    End Sub
    
End Class

' This should NOT be detected - ComVisible(False)
<ComVisible(False)>
Public Class NotComVisibleClass
    
    Public Sub New(value As Integer)
        ' Not COM visible, so no requirement
    End Sub
    
    Public Sub SomeMethod()
        Console.WriteLine("Some method")
    End Sub
    
End Class

' Violation 5: ComVisible class with static factory pattern
<ComVisible(True)>
Public Class ComVisibleClass5
    
    Private Sub New()
        ' Private constructor with factory pattern - violation for COM
    End Sub
    
    Public Shared Function Create() As ComVisibleClass5
        Return New ComVisibleClass5()
    End Function
    
    Public Sub FactoryMethod()
        Console.WriteLine("Factory method")
    End Sub
    
End Class

' Violation 6: ComVisible class with only Friend constructor
<ComVisible(True)>
Public Class ComVisibleClass6
    
    Friend Sub New()
        ' Friend constructor - violation for COM interop
    End Sub
    
    Public Sub InteropMethod()
        Console.WriteLine("Interop method")
    End Sub
    
End Class

' This should NOT be detected - interface (not applicable)
<ComVisible(True)>
Public Interface IComVisibleInterface
    
    Sub InterfaceMethod()
    
End Interface

' Violation 7: ComVisible class with Protected constructor
<ComVisible(True)>
Public Class ComVisibleClass7
    
    Protected Sub New()
        ' Protected constructor - violation for COM interop
    End Sub
    
    Public Sub New(value As String)
        Me.New()
    End Sub
    
    Public Sub ProtectedMethod()
        Console.WriteLine("Protected method")
    End Sub
    
End Class
