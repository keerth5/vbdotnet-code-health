' Test file for cq-vbn-0059: P/Invoke method should not be visible
' Rule should detect public P/Invoke methods that should be internal or private

Imports System
Imports System.Runtime.InteropServices

Public Class PInvokeVisibilityExamples
    
    ' Violation 1: Public P/Invoke method
    <DllImport("user32.dll")>
    Public Shared Function MessageBox(hWnd As IntPtr, text As String, caption As String, type As UInteger) As Integer
    End Function
    
    ' Violation 2: Public P/Invoke function
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function GetCurrentThreadId() As UInteger
    End Function
    
    ' Violation 3: Public P/Invoke sub
    <DllImport("kernel32.dll")>
    Public Shared Sub Sleep(milliseconds As UInteger)
    End Sub
    
    ' This should NOT be detected - Friend (internal) P/Invoke method
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Friend Shared Function OpenProcessToken(ProcessHandle As IntPtr, DesiredAccess As UInteger, ByRef TokenHandle As IntPtr) As Boolean
    End Function
    
    ' This should NOT be detected - Private P/Invoke method
    <DllImport("kernel32.dll")>
    Private Shared Function GetTickCount() As UInteger
    End Function
    
    ' This should NOT be detected - regular public method (not P/Invoke)
    Public Shared Sub RegularMethod()
        Console.WriteLine("Regular method")
    End Sub
    
End Class

Public Class WindowsApiWrapper
    
    ' Violation 4: Another public P/Invoke method
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function
    
    ' Violation 5: Public P/Invoke with complex signature
    <DllImport("user32.dll")>
    Public Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As UInteger) As Boolean
    End Function
    
    ' This should NOT be detected - Friend P/Invoke method
    <DllImport("kernel32.dll", SetLastError:=True)>
    Friend Shared Function CloseHandle(hObject As IntPtr) As Boolean
    End Function
    
    ' Public wrapper method that uses private P/Invoke (this is acceptable)
    Public Shared Sub ShowMessage(message As String)
        MessageBoxInternal(IntPtr.Zero, message, "Information", 0)
    End Sub
    
    ' This should NOT be detected - Private P/Invoke used by public wrapper
    <DllImport("user32.dll", EntryPoint:="MessageBox")>
    Private Shared Function MessageBoxInternal(hWnd As IntPtr, text As String, caption As String, type As UInteger) As Integer
    End Function
    
End Class

Public Class SystemInteropExamples
    
    ' Violation 6: Public P/Invoke for system calls
    <DllImport("kernel32.dll")>
    Public Shared Function GetCurrentProcess() As IntPtr
    End Function
    
    ' Violation 7: Public P/Invoke with string marshaling
    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Public Shared Function GetWindowText(hWnd As IntPtr, lpString As System.Text.StringBuilder, nMaxCount As Integer) As Integer
    End Function
    
    ' This should NOT be detected - Protected method (not public)
    <DllImport("kernel32.dll")>
    Protected Shared Function GetProcessId(hProcess As IntPtr) As UInteger
    End Function
    
End Class

' This should NOT be detected - NativeMethods class with proper encapsulation
Friend Class NativeMethods
    
    <DllImport("user32.dll")>
    Public Shared Function GetForegroundWindow() As IntPtr
    End Function
    
    <DllImport("kernel32.dll")>
    Public Shared Function GetCurrentProcessId() As UInteger
    End Function
    
End Class

Public Class FileSystemInterop
    
    ' Violation 8: Public P/Invoke for file operations
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function CreateFile(lpFileName As String, dwDesiredAccess As UInteger, dwShareMode As UInteger, lpSecurityAttributes As IntPtr, dwCreationDisposition As UInteger, dwFlagsAndAttributes As UInteger, hTemplateFile As IntPtr) As IntPtr
    End Function
    
    ' This should NOT be detected - Friend P/Invoke method
    <DllImport("kernel32.dll", SetLastError:=True)>
    Friend Shared Function ReadFile(hFile As IntPtr, lpBuffer As Byte(), nNumberOfBytesToRead As UInteger, ByRef lpNumberOfBytesRead As UInteger, lpOverlapped As IntPtr) As Boolean
    End Function
    
End Class

Public Class RegistryInterop
    
    ' Violation 9: Public P/Invoke for registry operations
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function RegOpenKeyEx(hKey As IntPtr, lpSubKey As String, ulOptions As UInteger, samDesired As UInteger, ByRef phkResult As IntPtr) As Integer
    End Function
    
    ' This should NOT be detected - Private P/Invoke method
    <DllImport("advapi32.dll")>
    Private Shared Function RegCloseKey(hKey As IntPtr) As Integer
    End Function
    
End Class
