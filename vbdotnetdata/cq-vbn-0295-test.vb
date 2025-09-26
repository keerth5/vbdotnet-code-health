' VB.NET test file for cq-vbn-0295: Providing a 'DynamicInterfaceCastableImplementation' interface in Visual Basic is unsupported
' This rule detects DynamicInterfaceCastableImplementation usage in VB.NET (unsupported)

Imports System
Imports System.Runtime.InteropServices

' BAD: DynamicInterfaceCastableImplementation in VB.NET (unsupported)
<DynamicInterfaceCastableImplementation>
Public Interface IBadVBInterface
    ' Violation: DynamicInterfaceCastableImplementation is unsupported in VB.NET
    Function GetValue() As Integer
    Sub DoSomething()
End Interface

' BAD: Another DynamicInterfaceCastableImplementation interface
<DynamicInterfaceCastableImplementation>
Friend Interface IAnotherBadVBInterface
    ' Violation: Friend interface with unsupported attribute
    Property Name As String
    Function Calculate() As Double
End Interface

' BAD: Public DynamicInterfaceCastableImplementation interface
<DynamicInterfaceCastableImplementation>
Public Interface IPublicBadInterface
    ' Violation: Public interface with unsupported attribute in VB.NET
    Sub Initialize()
    Function Process() As Boolean
End Interface

' GOOD: Regular interfaces without DynamicInterfaceCastableImplementation
Public Interface IGoodVBInterface
    ' Good: Regular interface without the unsupported attribute
    Function GetValue() As Integer
    Sub DoSomething()
End Interface

' GOOD: Another regular interface
Friend Interface IAnotherGoodInterface
    ' Good: Friend interface without problematic attribute
    Property Name As String
    Function Calculate() As Double
End Interface
