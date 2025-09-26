' VB.NET test file for cq-vbn-0122: Avoid StringBuilder parameters for P/Invokes
' Rule: Marshalling of StringBuilder always creates a native buffer copy, resulting in multiple allocations for one marshalling operation.

Imports System
Imports System.Runtime.InteropServices
Imports System.Text

Public Class PInvokeStringBuilderExamples
    
    ' Violation: P/Invoke method with StringBuilder parameter
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetCurrentDirectory(nBufferLength As Integer, lpBuffer As StringBuilder) As Integer
    End Function
    
    ' Violation: P/Invoke method with StringBuilder parameter and other parameters
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetWindowText(hWnd As IntPtr, lpString As StringBuilder, nMaxCount As Integer) As Integer
    End Function
    
    ' Violation: P/Invoke method with multiple StringBuilder parameters
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetUserName(lpBuffer As StringBuilder, lpnSize As Integer) As Boolean
    End Function
    
    ' Violation: P/Invoke method with StringBuilder as ByRef parameter
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetComputerName(ByRef lpBuffer As StringBuilder, ByRef nSize As Integer) As Boolean
    End Function
    
    ' Violation: P/Invoke method with StringBuilder and additional attributes
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function GetModuleFileName(hModule As IntPtr, lpFilename As StringBuilder, nSize As Integer) As Integer
    End Function
    
    ' Violation: P/Invoke method with StringBuilder parameter in different position
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetClassName(hWnd As IntPtr, lpClassName As StringBuilder, nMaxCount As Integer) As Integer
    End Function
    
    ' Violation: P/Invoke method with StringBuilder and calling convention
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function GetSystemDirectory(lpBuffer As StringBuilder, uSize As Integer) As Integer
    End Function
    
    ' Violation: P/Invoke method with StringBuilder and entry point
    <DllImport("kernel32.dll", EntryPoint:="GetTempPathA", CharSet:=CharSet.Ansi)>
    Public Shared Function GetTempPath(nBufferLength As Integer, lpBuffer As StringBuilder) As Integer
    End Function
    
    ' Violation: P/Invoke method with StringBuilder in structure
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function RegQueryValueEx(hKey As IntPtr, lpValueName As String, lpReserved As IntPtr, lpType As IntPtr, lpData As StringBuilder, lpcbData As IntPtr) As Integer
    End Function
    
    Public Sub UsePInvokeMethods()
        ' Using the P/Invoke methods with StringBuilder
        Dim buffer As New StringBuilder(260)
        Dim result As Integer
        
        ' Call methods that use StringBuilder parameters
        result = GetCurrentDirectory(buffer.Capacity, buffer)
        Console.WriteLine($"Current directory: {buffer}")
        
        buffer.Clear()
        Dim success As Boolean = GetUserName(buffer, buffer.Capacity)
        Console.WriteLine($"User name: {buffer}")
        
        buffer.Clear()
        result = GetSystemDirectory(buffer, buffer.Capacity)
        Console.WriteLine($"System directory: {buffer}")
    End Sub
    
End Class

' More violation examples with different P/Invoke scenarios

Public Class WindowsPInvokeExamples
    
    ' Violation: Windows API with StringBuilder
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetWindowsDirectory(lpBuffer As StringBuilder, uSize As Integer) As Integer
    End Function
    
    ' Violation: Registry API with StringBuilder
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function RegEnumKeyEx(hKey As IntPtr, dwIndex As Integer, lpName As StringBuilder, lpcchName As Integer, lpReserved As IntPtr, lpClass As StringBuilder, lpcchClass As IntPtr, lpftLastWriteTime As IntPtr) As Integer
    End Function
    
    ' Violation: File system API with StringBuilder
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function GetShortPathName(lpszLongPath As String, lpszShortPath As StringBuilder, cchBuffer As Integer) As Integer
    End Function
    
    ' Violation: Network API with StringBuilder
    <DllImport("netapi32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function NetGetJoinInformation(lpServer As String, lpNameBuffer As StringBuilder, bufferType As IntPtr) As Integer
    End Function
    
    Public Sub UseWindowsAPIs()
        Dim buffer As New StringBuilder(1024)
        Dim result As Integer
        
        result = GetWindowsDirectory(buffer, buffer.Capacity)
        Console.WriteLine($"Windows directory: {buffer}")
        
        buffer.Clear()
        result = GetShortPathName("C:\Program Files\", buffer, buffer.Capacity)
        Console.WriteLine($"Short path: {buffer}")
    End Sub
    
End Class

Public Class CustomPInvokeExamples
    
    ' Violation: Custom library with StringBuilder
    <DllImport("customlib.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetCustomData(dataBuffer As StringBuilder, bufferSize As Integer) As Integer
    End Function
    
    ' Violation: Another custom library with StringBuilder
    <DllImport("mylib.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)>
    Public Shared Function ProcessText(inputText As String, outputBuffer As StringBuilder, maxLength As Integer) As Boolean
    End Function
    
    ' Violation: Third-party library with StringBuilder
    <DllImport("thirdparty.dll", CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function FormatData(format As String, data As IntPtr, result As StringBuilder) As Integer
    End Function
    
    Public Sub UseCustomAPIs()
        Dim buffer As New StringBuilder(512)
        Dim success As Boolean
        
        success = ProcessText("input data", buffer, buffer.Capacity)
        Console.WriteLine($"Processed text: {buffer}")
        
        buffer.Clear()
        Dim result As Integer = GetCustomData(buffer, buffer.Capacity)
        Console.WriteLine($"Custom data: {buffer}")
    End Sub
    
End Class

Public Class LegacyPInvokeExamples
    
    ' Violation: Legacy API with StringBuilder
    <DllImport("legacy.dll", CharSet:=CharSet.Auto, PreserveSig:=True)>
    Public Shared Function GetLegacyInfo(infoBuffer As StringBuilder, bufferLength As Integer) As IntPtr
    End Function
    
    ' Violation: Old system API with StringBuilder
    <DllImport("oldsys.dll", CharSet:=CharSet.Ansi, ThrowOnUnmappableChar:=True)>
    Public Shared Function RetrieveSystemInfo(systemInfo As StringBuilder, infoSize As Integer) As Integer
    End Function
    
    Public Sub UseLegacyAPIs()
        Dim infoBuffer As New StringBuilder(256)
        Dim result As IntPtr
        
        result = GetLegacyInfo(infoBuffer, infoBuffer.Capacity)
        Console.WriteLine($"Legacy info: {infoBuffer}")
        
        infoBuffer.Clear()
        Dim intResult As Integer = RetrieveSystemInfo(infoBuffer, infoBuffer.Capacity)
        Console.WriteLine($"System info: {infoBuffer}")
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperPInvokeExamples
    
    ' Correct: P/Invoke without StringBuilder - should not be detected
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetCurrentProcessId() As Integer
    End Function
    
    ' Correct: P/Invoke with string parameters - should not be detected
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function MessageBox(hWnd As IntPtr, text As String, caption As String, type As Integer) As Integer
    End Function
    
    ' Correct: P/Invoke with IntPtr and other types - should not be detected
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function CloseHandle(hObject As IntPtr) As Boolean
    End Function
    
    ' Correct: P/Invoke with byte arrays - should not be detected
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function ReadFile(hFile As IntPtr, lpBuffer As Byte(), nNumberOfBytesToRead As Integer, lpNumberOfBytesRead As IntPtr, lpOverlapped As IntPtr) As Boolean
    End Function
    
    ' Correct: Regular method with StringBuilder - should not be detected
    Public Shared Function ProcessStringBuilder(sb As StringBuilder) As String
        sb.Append(" processed")
        Return sb.ToString()
    End Function
    
    Public Sub UseProperAPIs()
        Dim processId As Integer = GetCurrentProcessId()
        Console.WriteLine($"Process ID: {processId}")
        
        Dim result As Integer = MessageBox(IntPtr.Zero, "Hello", "Title", 0)
        Console.WriteLine($"MessageBox result: {result}")
        
        Dim sb As New StringBuilder("test")
        Dim processed = ProcessStringBuilder(sb)
        Console.WriteLine($"Processed: {processed}")
    End Sub
    
End Class
