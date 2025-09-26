' Test file for cq-vbn-0066: Validate marshalling of delegate parameters
' Rule should detect delegate parameters in P/Invoke methods

Imports System
Imports System.Runtime.InteropServices

' Delegate definitions for testing
Public Delegate Function CallbackDelegate(value As Integer) As Boolean
Public Delegate Sub NotificationDelegate(message As String)
Public Delegate Function ProcessDelegate(data As IntPtr, size As Integer) As Integer

Public Class DelegateMarshallingExamples
    
    ' Violation 1: P/Invoke with delegate parameter
    <DllImport("user32.dll")>
    Public Shared Function EnumWindows(lpEnumFunc As CallbackDelegate, lParam As IntPtr) As Boolean
    End Function
    
    ' Violation 2: P/Invoke with different delegate parameter
    <DllImport("kernel32.dll")>
    Public Shared Function SetConsoleCtrlHandler(HandlerRoutine As NotificationDelegate, Add As Boolean) As Boolean
    End Function
    
    ' Violation 3: P/Invoke with delegate parameter and different signature
    <DllImport("advapi32.dll", SetLastError:=True)>
    Public Shared Function RegEnumKeyEx(hKey As IntPtr, dwIndex As UInteger, lpName As System.Text.StringBuilder, lpcchName As UInteger, lpReserved As IntPtr, lpClass As System.Text.StringBuilder, lpcchClass As UInteger, lpftLastWriteTime As IntPtr, callback As ProcessDelegate) As Integer
    End Function
    
    ' This should NOT be detected - P/Invoke without delegate parameters
    <DllImport("user32.dll")>
    Public Shared Function MessageBox(hWnd As IntPtr, text As String, caption As String, type As UInteger) As Integer
    End Function
    
    ' Violation 4: P/Invoke with delegate parameter using generic delegate
    <DllImport("shell32.dll")>
    Public Shared Function SHCreateThread(pfnThreadProc As System.Threading.ThreadStart, pData As IntPtr, flags As UInteger, ByRef pdwThreadId As UInteger) As IntPtr
    End Function
    
    ' This should NOT be detected - regular method with delegate parameter (not P/Invoke)
    Public Shared Sub RegularMethodWithDelegate(callback As CallbackDelegate)
        callback(42)
    End Sub
    
    ' Violation 5: P/Invoke with Action delegate
    <DllImport("ntdll.dll")>
    Public Shared Function RtlQueueWorkItem(callback As Action, context As IntPtr, flags As UInteger) As Integer
    End Function
    
    ' Violation 6: P/Invoke with Func delegate
    <DllImport("ole32.dll")>
    Public Shared Function CoInitializeEx(pvReserved As IntPtr, dwCoInit As UInteger, callback As Func(Of Integer, Boolean)) As Integer
    End Function
    
    ' This should NOT be detected - P/Invoke with non-delegate parameters
    <DllImport("gdi32.dll")>
    Public Shared Function CreateSolidBrush(crColor As UInteger) As IntPtr
    End Function
    
    ' Violation 7: P/Invoke with custom delegate
    <DllImport("winmm.dll")>
    Public Shared Function timeSetEvent(uDelay As UInteger, uResolution As UInteger, lpTimeProc As TimerDelegate, dwUser As IntPtr, fuEvent As UInteger) As UInteger
    End Function
    
    ' Violation 8: P/Invoke with delegate parameter in Friend method
    <DllImport("psapi.dll")>
    Friend Shared Function EnumProcesses(lpidProcess As IntPtr, cb As UInteger, lpcbNeeded As UInteger, enumCallback As ProcessEnumDelegate) As Boolean
    End Function
    
    ' This should NOT be detected - structure definition
    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure
    
End Class

' Additional delegate definitions
Public Delegate Sub TimerDelegate(uTimerID As UInteger, uMsg As UInteger, dwUser As IntPtr, dw1 As IntPtr, dw2 As IntPtr)
Public Delegate Function ProcessEnumDelegate(processId As UInteger) As Boolean

Public Class MoreDelegateExamples
    
    ' Violation 9: P/Invoke with EventHandler delegate
    <DllImport("user32.dll")>
    Public Shared Function SetWinEventHook(eventMin As UInteger, eventMax As UInteger, hmodWinEventProc As IntPtr, lpfnWinEventProc As EventHandler, idProcess As UInteger, idThread As UInteger, dwFlags As UInteger) As IntPtr
    End Function
    
    ' Violation 10: P/Invoke with delegate parameter using different calling convention
    <DllImport("kernel32.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function CreateThread(lpThreadAttributes As IntPtr, dwStackSize As UInteger, lpStartAddress As ThreadDelegate, lpParameter As IntPtr, dwCreationFlags As UInteger, ByRef lpThreadId As UInteger) As IntPtr
    End Function
    
    ' This should NOT be detected - regular class method
    Public Sub ProcessData(processor As ProcessDelegate)
        processor(IntPtr.Zero, 0)
    End Sub
    
End Class

' Thread delegate for testing
Public Delegate Function ThreadDelegate(lpParameter As IntPtr) As UInteger
