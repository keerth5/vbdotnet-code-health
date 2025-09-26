' Test file for cq-vbn-0039: Do not hide base class methods
' Rule should detect methods using Shadows keyword that hide base class methods

Imports System

Public Class BaseClass
    
    Public Overridable Sub ProcessData()
        Console.WriteLine("Base ProcessData")
    End Sub
    
    Public Overridable Function Calculate(x As Integer) As Integer
        Return x * 2
    End Function
    
    Public Sub DisplayInfo()
        Console.WriteLine("Base DisplayInfo")
    End Sub
    
End Class

Public Class DerivedClass
    Inherits BaseClass
    
    ' Violation 1: Public method using Shadows to hide base method
    Public Shadows Sub ProcessData()
        Console.WriteLine("Derived ProcessData - hiding base method")
    End Sub
    
    ' Violation 2: Protected method using Shadows
    Protected Shadows Function Calculate(x As Integer) As Integer
        Return x * 3
    End Function
    
    ' Violation 3: Friend method using Shadows
    Friend Shadows Sub DisplayInfo()
        Console.WriteLine("Derived DisplayInfo - hiding base method")
    End Sub
    
    ' This should NOT be detected - proper override (not hiding)
    Public Overrides Function Calculate(x As Integer) As Integer
        Return x * 4
    End Function
    
    ' This should NOT be detected - new method without Shadows
    Public Sub NewMethod()
        Console.WriteLine("New method in derived class")
    End Sub
    
End Class

Public Class AnotherDerivedClass
    Inherits BaseClass
    
    ' Violation 4: Another Shadows method
    Public Shadows Sub ProcessData(additionalParam As String)
        Console.WriteLine($"Derived ProcessData with param: {additionalParam}")
    End Sub
    
    ' Violation 5: Protected Shadows function
    Protected Shadows Function GetValue() As String
        Return "Derived value"
    End Function
    
End Class

Friend Class FriendDerivedClass
    Inherits BaseClass
    
    ' Violation 6: Friend class with Shadows method
    Friend Shadows Sub ProcessData()
        Console.WriteLine("Friend derived ProcessData")
    End Sub
    
End Class

' This should NOT be detected - class without inheritance
Public Class StandaloneClass
    
    Public Sub ProcessData()
        Console.WriteLine("Standalone ProcessData")
    End Sub
    
    Public Function Calculate(x As Integer) As Integer
        Return x * 5
    End Function
    
End Class

Public Class ProperDerivedClass
    Inherits BaseClass
    
    ' This should NOT be detected - proper overrides
    Public Overrides Sub ProcessData()
        Console.WriteLine("Proper override of ProcessData")
    End Sub
    
    ' This should NOT be detected - overloaded method (different signature)
    Public Sub ProcessData(data As String)
        Console.WriteLine($"Processing: {data}")
    End Sub
    
End Class
