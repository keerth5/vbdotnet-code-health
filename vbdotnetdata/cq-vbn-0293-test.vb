' VB.NET test file for cq-vbn-0293: All members declared in parent interfaces must have an implementation in a DynamicInterfaceCastableImplementation-attributed interface
' This rule detects incomplete implementations of DynamicInterfaceCastableImplementation interfaces

Imports System
Imports System.Runtime.InteropServices

' Base interface
Public Interface IBaseInterface
    Sub DoSomething()
    Function GetValue() As Integer
End Interface

' Extended interface
Public Interface IExtendedInterface
    Inherits IBaseInterface
    Sub DoMore()
End Interface

' BAD: Incomplete DynamicInterfaceCastableImplementation
<DynamicInterfaceCastableImplementation>
Public Interface IBadImplementation
    Inherits IBaseInterface
    ' Violation: Missing implementation of DoSomething from IBaseInterface
    ' Only implements GetValue
    Function GetValue() As Integer
End Interface

' BAD: Another incomplete implementation
<DynamicInterfaceCastableImplementation>
Public Interface IAnotherBadImplementation
    Inherits IExtendedInterface
    ' Violation: Missing implementations from parent interfaces
    Sub DoMore()
End Interface

' GOOD: Complete implementation
<DynamicInterfaceCastableImplementation>
Public Interface IGoodImplementation
    Inherits IBaseInterface
    ' Good: All members from parent interface implemented
    Sub DoSomething()
    Function GetValue() As Integer
End Interface

' GOOD: Regular interface without attribute
Public Interface IRegularInterface
    Inherits IBaseInterface
    ' Good: No DynamicInterfaceCastableImplementation attribute
    Sub AdditionalMethod()
End Interface
