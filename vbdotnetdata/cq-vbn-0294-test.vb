' VB.NET test file for cq-vbn-0294: Members defined on an interface with 'DynamicInterfaceCastableImplementationAttribute' should be 'static'
' This rule detects non-static members in DynamicInterfaceCastableImplementation interfaces

Imports System
Imports System.Runtime.InteropServices

' BAD: Non-static members in DynamicInterfaceCastableImplementation interface
<DynamicInterfaceCastableImplementation>
Public Interface IBadDynamicInterface
    ' Violation: Non-static function
    Function GetValue() As Integer
    
    ' Violation: Non-static sub
    Sub DoSomething()
    
    ' Violation: Non-static property
    Property Name As String
End Interface

' BAD: Mixed static and non-static members
<DynamicInterfaceCastableImplementation>
Public Interface IMixedDynamicInterface
    ' Good: Static member
    Shared Function GetStaticValue() As Integer
    
    ' Violation: Non-static function should be Shared
    Function GetInstanceValue() As Integer
    
    ' Violation: Non-static property should be Shared
    Property InstanceProperty As String
End Interface

' GOOD: All static members in DynamicInterfaceCastableImplementation interface
<DynamicInterfaceCastableImplementation>
Public Interface IGoodDynamicInterface
    ' Good: Static function
    Shared Function GetValue() As Integer
    
    ' Good: Static sub
    Shared Sub DoSomething()
    
    ' Good: Static property
    Shared Property Name As String
End Interface

' GOOD: Regular interface without attribute
Public Interface IRegularInterface
    ' Good: Non-static members are fine in regular interfaces
    Function GetValue() As Integer
    Sub DoSomething()
    Property Name As String
End Interface
