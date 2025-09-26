' Test file for cq-vbn-0038: Move P/Invokes to NativeMethods class
' Rule should detect DllImport methods that should be organized in a NativeMethods class

Imports System
Imports System.Runtime.InteropServices

Public Class RegularClass
    
    ' Violation 1: Public DllImport method in regular class
    <DllImport("user32.dll")>
    Public Shared Function MessageBox(hWnd As IntPtr, text As String, caption As String, type As UInteger) As Integer
    End Function
    
    ' Violation 2: Friend DllImport method
    <DllImport("kernel32.dll", SetLastError:=True)>
    Friend Shared Function GetCurrentThreadId() As UInteger
    End Function
    
    ' Regular method - should not be detected
    Public Sub RegularMethod()
        Console.WriteLine("Regular method")
    End Sub
    
End Class

Public Class AnotherClass
    
    ' Violation 3: Another DllImport method in wrong class
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function OpenProcessToken(ProcessHandle As IntPtr, DesiredAccess As UInteger, ByRef TokenHandle As IntPtr) As Boolean
    End Function
    
    ' Violation 4: DllImport function (not sub)
    <DllImport("kernel32.dll")>
    Friend Shared Function GetTickCount() As UInteger
    End Function
    
End Class

Friend Class UtilityClass
    
    ' Violation 5: DllImport in friend class
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function
    
End Class

' This should NOT be detected - DllImport methods in NativeMethods class (proper organization)
Friend Class NativeMethods
    
    <DllImport("user32.dll")>
    Public Shared Function GetWindowText(hWnd As IntPtr, lpString As System.Text.StringBuilder, nMaxCount As Integer) As Integer
    End Function
    
    <DllImport("kernel32.dll")>
    Public Shared Function GetCurrentProcess() As IntPtr
    End Function
    
End Class

' This should NOT be detected - regular methods without DllImport
Public Class RegularMethodsClass
    
    Public Shared Function CalculateSum(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    Friend Sub ProcessData()
        Console.WriteLine("Processing data")
    End Sub
    
End Class

Public Class WindowsApiWrapper
    
    ' Violation 6: DllImport method in wrapper class (should be in NativeMethods)
    <DllImport("user32.dll")>
    Public Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As UInteger) As Boolean
    End Function
    
    ' Violation 7: Another DllImport method
    <DllImport("kernel32.dll", SetLastError:=True)>
    Friend Shared Function CloseHandle(hObject As IntPtr) As Boolean
    End Function
    
    ' Regular wrapper method - should not be detected
    Public Shared Sub ShowMessage(message As String)
        MessageBox(IntPtr.Zero, message, "Information", 0)
    End Sub
    
End Class
