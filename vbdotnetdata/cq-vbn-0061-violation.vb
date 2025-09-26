' Test file for cq-vbn-0061: Do not use OutAttribute on string parameters for P/Invokes
' Rule should detect OutAttribute usage on string parameters in P/Invoke methods

Imports System
Imports System.Runtime.InteropServices

Public Class PInvokeOutAttributeExamples
    
    ' Violation 1: OutAttribute on string parameter in P/Invoke
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetWindowText(hWnd As IntPtr, <Out> lpString As String, nMaxCount As Integer) As Integer
    End Function
    
    ' Violation 2: OutAttribute on string parameter with different formatting
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function GetComputerName(<Out> lpBuffer As String, ByRef nSize As UInteger) As Boolean
    End Function
    
    ' Violation 3: OutAttribute on string parameter in another P/Invoke
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function GetUserName(<Out> lpBuffer As String, ByRef pcbBuffer As UInteger) As Boolean
    End Function
    
    ' This should NOT be detected - OutAttribute on StringBuilder (acceptable)
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetWindowText(hWnd As IntPtr, <Out> lpString As System.Text.StringBuilder, nMaxCount As Integer) As Integer
    End Function
    
    ' This should NOT be detected - OutAttribute on non-string parameter
    <DllImport("kernel32.dll")>
    Public Shared Function GetSystemInfo(<Out> lpSystemInfo As SYSTEM_INFO) As Boolean
    End Function
    
    ' Violation 4: OutAttribute on string parameter with complex signature
    <DllImport("shell32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function SHGetFolderPath(hwndOwner As IntPtr, nFolder As Integer, hToken As IntPtr, dwFlags As UInteger, <Out> pszPath As String) As Integer
    End Function
    
    ' This should NOT be detected - regular public method (not P/Invoke)
    Public Shared Function RegularMethod(<Out> output As String) As Boolean
        output = "test"
        Return True
    End Function
    
    ' Violation 5: OutAttribute on string parameter with different DLL
    <DllImport("ntdll.dll")>
    Public Shared Function NtQuerySystemInformation(SystemInformationClass As Integer, <Out> SystemInformation As String, SystemInformationLength As UInteger, ByRef ReturnLength As UInteger) As Integer
    End Function
    
    ' This should NOT be detected - no OutAttribute
    <DllImport("user32.dll")>
    Public Shared Function MessageBox(hWnd As IntPtr, text As String, caption As String, type As UInteger) As Integer
    End Function
    
    ' Violation 6: OutAttribute on string parameter in Friend method
    <DllImport("gdi32.dll")>
    Friend Shared Function GetObject(hgdiobj As IntPtr, cbBuffer As Integer, <Out> lpvObject As String) As Integer
    End Function
    
    ' Violation 7: OutAttribute on string parameter with EntryPoint
    <DllImport("kernel32.dll", EntryPoint:="GetModuleFileNameA")>
    Public Shared Function GetModuleFileName(hModule As IntPtr, <Out> lpFilename As String, nSize As UInteger) As UInteger
    End Function
    
    ' This should NOT be detected - OutAttribute on IntPtr
    <DllImport("kernel32.dll")>
    Public Shared Function GetProcAddress(hModule As IntPtr, <Out> lpProcName As IntPtr) As IntPtr
    End Function
    
    ' Violation 8: OutAttribute on string parameter with PreserveSig
    <DllImport("ole32.dll", PreserveSig:=False)>
    Public Shared Function CoCreateGuid(<Out> pguid As String) As Integer
    End Function
    
End Class

' Structure for testing non-string OutAttribute (should not be detected)
<StructLayout(LayoutKind.Sequential)>
Public Structure SYSTEM_INFO
    Public dwOemId As UInteger
    Public dwPageSize As UInteger
    Public lpMinimumApplicationAddress As IntPtr
    Public lpMaximumApplicationAddress As IntPtr
End Structure
