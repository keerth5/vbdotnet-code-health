' Test file for cq-vbn-0264: Dispose methods should call base class dispose
' Rule should detect: Dispose methods that don't call MyBase.Dispose()

Imports System

Public Class BaseDisposable
    Implements IDisposable
    
    Protected disposed As Boolean = False
    
    Public Overridable Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposed Then
            If disposing Then
                ' Dispose managed resources
            End If
            disposed = True
        End If
    End Sub
    
End Class

' VIOLATION: Dispose method doesn't call MyBase.Dispose()
Public Class BadDerivedDisposable1
    Inherits BaseDisposable
    
    Private resource As Object
    
    Public Overrides Sub Dispose()
        ' Missing: MyBase.Dispose()
        resource = Nothing
    End Sub
    
End Class

' VIOLATION: Dispose(Boolean) method doesn't call base dispose
Public Class BadDerivedDisposable2
    Inherits BaseDisposable
    
    Private resource As Object
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            resource = Nothing
        End If
        ' Missing: MyBase.Dispose(disposing)
    End Sub
    
End Class

' GOOD: Dispose method properly calls MyBase.Dispose() - should NOT be flagged
Public Class GoodDerivedDisposable1
    Inherits BaseDisposable
    
    Private resource As Object
    
    Public Overrides Sub Dispose()
        resource = Nothing
        MyBase.Dispose()
    End Sub
    
End Class

' GOOD: Dispose(Boolean) method properly calls base dispose - should NOT be flagged
Public Class GoodDerivedDisposable2
    Inherits BaseDisposable
    
    Private resource As Object
    
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            resource = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub
    
End Class
