' Test file for cq-vbn-0265: Disposable types should declare finalizer
' Rule should detect: IDisposable classes with unmanaged resources but no finalizer

Imports System
Imports System.Runtime.InteropServices

' VIOLATION: IDisposable class with IntPtr field but no finalizer
Public Class BadUnmanagedResource1
    Implements IDisposable
    
    Private handle As IntPtr
    
    Public Sub New()
        handle = Marshal.AllocHGlobal(1024)
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        If handle <> IntPtr.Zero Then
            Marshal.FreeHGlobal(handle)
            handle = IntPtr.Zero
        End If
    End Sub
    
    ' Missing: Protected Overrides Sub Finalize()
    
End Class

' VIOLATION: IDisposable class with unmanaged resource but no finalizer
Friend Class BadUnmanagedResource2
    Implements IDisposable
    
    Private unmanagedHandle As IntPtr
    Private disposed As Boolean = False
    
    Public Sub New()
        unmanagedHandle = Marshal.AllocHGlobal(2048)
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Sub Dispose(disposing As Boolean)
        If Not disposed Then
            If unmanagedHandle <> IntPtr.Zero Then
                Marshal.FreeHGlobal(unmanagedHandle)
                unmanagedHandle = IntPtr.Zero
            End If
            disposed = True
        End If
    End Sub
    
    ' Missing: Protected Overrides Sub Finalize()
    
End Class

' GOOD: IDisposable class with unmanaged resource and proper finalizer - should NOT be flagged
Public Class GoodUnmanagedResource1
    Implements IDisposable
    
    Private handle As IntPtr
    Private disposed As Boolean = False
    
    Public Sub New()
        handle = Marshal.AllocHGlobal(1024)
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Sub Dispose(disposing As Boolean)
        If Not disposed Then
            If handle <> IntPtr.Zero Then
                Marshal.FreeHGlobal(handle)
                handle = IntPtr.Zero
            End If
            disposed = True
        End If
    End Sub
    
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
    
End Class

' GOOD: IDisposable class without unmanaged resources - should NOT be flagged
Public Class ManagedOnlyResource
    Implements IDisposable
    
    Private managedResource As String = "test"
    
    Public Sub Dispose() Implements IDisposable.Dispose
        managedResource = Nothing
    End Sub
    
End Class
