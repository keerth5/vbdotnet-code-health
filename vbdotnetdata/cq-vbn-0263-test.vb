' Test file for cq-vbn-0263: Do not call overridable methods in constructors
' Rule should detect: Constructor calls to virtual/overridable methods

Public Class BaseClass
    
    ' VIOLATION: Constructor calls overridable method
    Public Sub New()
        InitializeData()
        SetupConfiguration()
    End Sub
    
    ' VIOLATION: Constructor with parameters calls overridable method
    Public Sub New(value As Integer)
        ProcessValue(value)
    End Sub
    
    Protected Overridable Sub InitializeData()
        ' Base implementation
    End Sub
    
    Protected Overridable Sub SetupConfiguration()
        ' Base implementation
    End Sub
    
    Protected Overridable Sub ProcessValue(value As Integer)
        ' Base implementation
    End Sub
    
End Class

Public Class DerivedClass
    Inherits BaseClass
    
    ' VIOLATION: Constructor calls overridable method from base
    Public Sub New()
        MyBase.New()
        ConfigureSettings()
    End Sub
    
    Protected Overridable Sub ConfigureSettings()
        ' Derived implementation
    End Sub
    
    Protected Overrides Sub InitializeData()
        ' Override implementation - could be called before this constructor runs
    End Sub
    
End Class

' GOOD: Constructor calls non-virtual methods - should NOT be flagged
Public Class GoodClass
    
    Public Sub New()
        InitializeNonVirtual()
        SetupPrivateData()
    End Sub
    
    Private Sub InitializeNonVirtual()
        ' Non-virtual method - safe to call
    End Sub
    
    Private Sub SetupPrivateData()
        ' Private method - safe to call
    End Sub
    
End Class
