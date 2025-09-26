' Test file for cq-vbn-0065: Avoid deprecated runtime marshalling APIs
' Rule should detect usage of deprecated Marshal methods

Imports System
Imports System.Runtime.InteropServices

Public Class DeprecatedMarshallingExamples
    
    Public Sub UseDeprecatedMethods()
        Dim text As String = "Hello World"
        
        ' Violation 1: StringToHGlobalAnsi (deprecated)
        Dim ptr1 As IntPtr = Marshal.StringToHGlobalAnsi(text)
        
        ' Violation 2: StringToHGlobalUni (deprecated)
        Dim ptr2 As IntPtr = Marshal.StringToHGlobalUni(text)
        
        ' Violation 3: StringToCoTaskMemAnsi (deprecated)
        Dim ptr3 As IntPtr = Marshal.StringToCoTaskMemAnsi(text)
        
        ' Violation 4: StringToCoTaskMemUni (deprecated)
        Dim ptr4 As IntPtr = Marshal.StringToCoTaskMemUni(text)
        
        ' This should NOT be detected - modern marshalling methods
        Dim ptr5 As IntPtr = Marshal.StringToHGlobalAuto(text)
        Dim ptr6 As IntPtr = Marshal.StringToCoTaskMemAuto(text)
        
        ' Clean up
        If ptr1 <> IntPtr.Zero Then Marshal.FreeHGlobal(ptr1)
        If ptr2 <> IntPtr.Zero Then Marshal.FreeHGlobal(ptr2)
        If ptr3 <> IntPtr.Zero Then Marshal.FreeCoTaskMem(ptr3)
        If ptr4 <> IntPtr.Zero Then Marshal.FreeCoTaskMem(ptr4)
        If ptr5 <> IntPtr.Zero Then Marshal.FreeHGlobal(ptr5)
        If ptr6 <> IntPtr.Zero Then Marshal.FreeCoTaskMem(ptr6)
        
    End Sub
    
    Public Sub MoreDeprecatedUsage()
        Dim message As String = "Test Message"
        
        ' Violation 5: Another StringToHGlobalAnsi usage
        Dim ansiPtr = Marshal.StringToHGlobalAnsi(message)
        
        ' Violation 6: Another StringToHGlobalUni usage
        Dim uniPtr = Marshal.StringToHGlobalUni(message)
        
        ' This should NOT be detected - other Marshal methods
        Dim size = Marshal.SizeOf(GetType(Integer))
        Dim allocated = Marshal.AllocHGlobal(size)
        
        ' Clean up
        Marshal.FreeHGlobal(ansiPtr)
        Marshal.FreeHGlobal(uniPtr)
        Marshal.FreeHGlobal(allocated)
        
    End Sub
    
    Public Sub ProcessStrings()
        Dim strings() As String = {"First", "Second", "Third"}
        
        For Each str In strings
            ' Violation 7: StringToCoTaskMemAnsi in loop
            Dim taskPtr = Marshal.StringToCoTaskMemAnsi(str)
            
            ' Do something with the pointer
            ' ...
            
            ' Clean up
            Marshal.FreeCoTaskMem(taskPtr)
        Next
        
    End Sub
    
    Public Function ConvertString(input As String) As IntPtr
        ' Violation 8: StringToCoTaskMemUni in return
        Return Marshal.StringToCoTaskMemUni(input)
    End Function
    
    Public Sub HandleNativeStrings()
        Dim nativeString As String = "Native String"
        
        ' Violation 9: StringToHGlobalAnsi with assignment
        Dim globalAnsi As IntPtr = Marshal.StringToHGlobalAnsi(nativeString)
        
        ' Violation 10: StringToHGlobalUni with assignment
        Dim globalUni As IntPtr = Marshal.StringToHGlobalUni(nativeString)
        
        ' This should NOT be detected - PtrToStringAnsi (not deprecated)
        Dim backToString = Marshal.PtrToStringAnsi(globalAnsi)
        
        ' Clean up
        Marshal.FreeHGlobal(globalAnsi)
        Marshal.FreeHGlobal(globalUni)
        
    End Sub
    
    ' This should NOT be detected - method without deprecated calls
    Public Sub ModernMarshallingMethod()
        Dim text As String = "Modern approach"
        Dim ptr As IntPtr = Marshal.StringToHGlobalAuto(text)
        
        Try
            ' Use the pointer
            Console.WriteLine("Using modern marshalling")
        Finally
            Marshal.FreeHGlobal(ptr)
        End Try
    End Sub
    
End Class

Public Class AnotherMarshallingClass
    
    Public Sub AdditionalDeprecatedUsage()
        Dim data As String = "Additional data"
        
        ' Violation 11: StringToCoTaskMemAnsi in different class
        Dim taskMem = Marshal.StringToCoTaskMemAnsi(data)
        
        ' Violation 12: StringToCoTaskMemUni in different class
        Dim taskMemUni = Marshal.StringToCoTaskMemUni(data)
        
        ' Clean up
        Marshal.FreeCoTaskMem(taskMem)
        Marshal.FreeCoTaskMem(taskMemUni)
        
    End Sub
    
End Class
