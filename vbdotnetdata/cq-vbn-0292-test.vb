' VB.NET test file for cq-vbn-0292: The ModuleInitializer attribute should not be used in libraries
' This rule detects ModuleInitializer attribute usage in library code

Imports System
Imports System.Runtime.CompilerServices

' BAD: ModuleInitializer in library code
Public Class BadModuleInitializer
    ' Violation: ModuleInitializer on public method
    <ModuleInitializer>
    Public Shared Sub InitializeModule()
        Console.WriteLine("Module initialized")
    End Sub
    
    ' Violation: ModuleInitializer on friend method
    <ModuleInitializer>
    Friend Shared Sub InitializeFriend()
        Console.WriteLine("Friend initialization")
    End Sub
    
    ' Violation: ModuleInitializer on private method
    <ModuleInitializer>
    Private Shared Sub InitializePrivate()
        Console.WriteLine("Private initialization")
    End Sub
End Class

' BAD: Another class with ModuleInitializer
Public Class AnotherBadInitializer
    ' Violation: ModuleInitializer without Shared keyword (but implied)
    <ModuleInitializer>
    Public Sub BadInit()
        Console.WriteLine("Bad initialization")
    End Sub
End Class

' GOOD: Regular initialization without ModuleInitializer
Public Class GoodInitializer
    ' Good: Regular static constructor
    Shared Sub New()
        Console.WriteLine("Static constructor")
    End Sub
    
    ' Good: Regular initialization method
    Public Shared Sub Initialize()
        Console.WriteLine("Manual initialization")
    End Sub
    
    ' Good: Instance initialization
    Public Sub New()
        Console.WriteLine("Instance constructor")
    End Sub
End Class
