' VB.NET test file for cq-vbn-0243: Use DefaultDllImportSearchPaths attribute for P/Invokes
' This rule detects P/Invoke methods without DefaultDllImportSearchPaths attribute

Imports System
Imports System.Runtime.InteropServices

' Violation: DllImport without DefaultDllImportSearchPaths
Public Class UnsafePInvoke1
    ' Violation: DllImport without DefaultDllImportSearchPaths
    <DllImport("kernel32.dll")>
    Public Shared Function CreateFile(filename As String, access As UInteger, share As UInteger, 
                                    securityAttributes As IntPtr, creationDisposition As UInteger, 
                                    flagsAndAttributes As UInteger, templateFile As IntPtr) As IntPtr
    End Function
End Class

' Violation: Multiple P/Invoke methods without DefaultDllImportSearchPaths
Public Class UnsafePInvoke2
    ' Violation: First P/Invoke without DefaultDllImportSearchPaths
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function
    
    ' Violation: Second P/Invoke without DefaultDllImportSearchPaths
    <DllImport("user32.dll")>
    Public Shared Function GetWindowText(hWnd As IntPtr, lpString As System.Text.StringBuilder, 
                                       nMaxCount As Integer) As Integer
    End Function
    
    ' Violation: Third P/Invoke without DefaultDllImportSearchPaths
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function GetCurrentProcessId() As UInteger
    End Function
End Class

' Violation: P/Invoke with other attributes but missing DefaultDllImportSearchPaths
Public Class UnsafePInvoke3
    ' Violation: Has CharSet but missing DefaultDllImportSearchPaths
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function OpenProcessToken(ProcessHandle As IntPtr, DesiredAccess As UInteger, 
                                          ByRef TokenHandle As IntPtr) As Boolean
    End Function
    
    ' Violation: Has CallingConvention but missing DefaultDllImportSearchPaths
    <DllImport("kernel32.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function GetTickCount() As UInteger
    End Function
End Class

' Violation: P/Invoke with ExactSpelling but missing DefaultDllImportSearchPaths
Public Class UnsafePInvoke4
    ' Violation: Has ExactSpelling but missing DefaultDllImportSearchPaths
    <DllImport("kernel32.dll", ExactSpelling:=True)>
    Public Shared Function GetCurrentThreadId() As UInteger
    End Function
    
    ' Violation: Has EntryPoint but missing DefaultDllImportSearchPaths
    <DllImport("user32.dll", EntryPoint:="MessageBoxA")>
    Public Shared Function MessageBox(hWnd As IntPtr, text As String, caption As String, 
                                    uType As UInteger) As Integer
    End Function
End Class

' Violation: Private P/Invoke methods without DefaultDllImportSearchPaths
Public Class UnsafePInvoke5
    ' Violation: Private P/Invoke without DefaultDllImportSearchPaths
    <DllImport("shell32.dll")>
    Private Shared Function SHGetFolderPath(hwndOwner As IntPtr, nFolder As Integer, 
                                          hToken As IntPtr, dwFlags As UInteger, 
                                          pszPath As System.Text.StringBuilder) As Integer
    End Function
    
    ' Violation: Friend P/Invoke without DefaultDllImportSearchPaths
    <DllImport("ole32.dll")>
    Friend Shared Function CoInitialize(pvReserved As IntPtr) As Integer
    End Function
End Class

' Violation: P/Invoke functions with different return types
Public Class UnsafePInvoke6
    ' Violation: Function returning Boolean
    <DllImport("kernel32.dll")>
    Public Shared Function CloseHandle(hObject As IntPtr) As Boolean
    End Function
    
    ' Violation: Sub (no return value)
    <DllImport("kernel32.dll")>
    Public Shared Sub ExitProcess(uExitCode As UInteger)
    End Sub
    
    ' Violation: Function returning String
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetComputerName(lpBuffer As System.Text.StringBuilder, 
                                         ByRef nSize As Integer) As Boolean
    End Function
End Class

' Violation: P/Invoke with complex parameter types
Public Class UnsafePInvoke7
    ' Violation: P/Invoke with structure parameter
    <DllImport("user32.dll")>
    Public Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function
    
    ' Violation: P/Invoke with array parameter
    <DllImport("kernel32.dll")>
    Public Shared Function ReadProcessMemory(hProcess As IntPtr, lpBaseAddress As IntPtr, 
                                           lpBuffer As Byte(), nSize As IntPtr, 
                                           ByRef lpNumberOfBytesRead As IntPtr) As Boolean
    End Function
End Class

' Structure for P/Invoke example
<StructLayout(LayoutKind.Sequential)>
Public Structure RECT
    Public Left As Integer
    Public Top As Integer
    Public Right As Integer
    Public Bottom As Integer
End Structure

' Good examples (should not be detected as violations)
Public Class SecurePInvoke
    ' Good: P/Invoke with DefaultDllImportSearchPaths
    <DllImport("kernel32.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.System32)>
    Public Shared Function GetCurrentProcess() As IntPtr
    End Function
    
    ' Good: P/Invoke with multiple search paths
    <DllImport("user32.dll", SetLastError:=True)>
    <DefaultDllImportSearchPaths(DllImportSearchPath.System32 Or DllImportSearchPath.UserDirectories)>
    Public Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, 
                                       x As Integer, y As Integer, cx As Integer, cy As Integer, 
                                       uFlags As UInteger) As Boolean
    End Function
    
    ' Good: P/Invoke with SafeDirectories
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)>
    <DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)>
    Public Shared Function RegOpenKeyEx(hKey As IntPtr, lpSubKey As String, ulOptions As UInteger, 
                                       samDesired As Integer, ByRef phkResult As IntPtr) As Integer
    End Function
End Class

' Good: Class with managed methods (no P/Invoke)
Public Class ManagedMethods
    Public Shared Function CalculateSum(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    Public Shared Sub ProcessData(data As String)
        Console.WriteLine(data)
    End Sub
End Class
