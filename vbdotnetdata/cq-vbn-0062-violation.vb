' Test file for cq-vbn-0062: Use valid platform compatibility attributes
' Rule should detect platform compatibility attributes usage

Imports System
Imports System.Runtime.Versioning

' Violation 1: SupportedOSPlatform attribute on class
<SupportedOSPlatform("windows")>
Public Class WindowsSpecificClass
    
    Public Sub DoWindowsStuff()
        Console.WriteLine("Windows-specific functionality")
    End Sub
    
End Class

' Violation 2: UnsupportedOSPlatform attribute on class
<UnsupportedOSPlatform("linux")>
Public Class NotLinuxClass
    
    Public Sub DoNonLinuxStuff()
        Console.WriteLine("Not supported on Linux")
    End Sub
    
End Class

Public Class PlatformCompatibilityExamples
    
    ' Violation 3: SupportedOSPlatform attribute on method
    <SupportedOSPlatform("windows10.0")>
    Public Sub Windows10OnlyMethod()
        Console.WriteLine("Windows 10 only")
    End Sub
    
    ' Violation 4: UnsupportedOSPlatform attribute on method
    <UnsupportedOSPlatform("macos")>
    Public Sub NotMacMethod()
        Console.WriteLine("Not supported on macOS")
    End Sub
    
    ' Violation 5: SupportedOSPlatform with version
    <SupportedOSPlatform("windows7.0")>
    Public Sub Windows7Method()
        Console.WriteLine("Requires Windows 7 or later")
    End Sub
    
    ' Violation 6: UnsupportedOSPlatform with version
    <UnsupportedOSPlatform("android21.0")>
    Public Sub NotAndroidMethod()
        Console.WriteLine("Not supported on Android 21+")
    End Sub
    
    ' This should NOT be detected - regular method without platform attributes
    Public Sub CrossPlatformMethod()
        Console.WriteLine("Works on all platforms")
    End Sub
    
    ' Violation 7: SupportedOSPlatform with different platform
    <SupportedOSPlatform("ios")>
    Public Sub iOSOnlyMethod()
        Console.WriteLine("iOS only")
    End Sub
    
    ' Violation 8: UnsupportedOSPlatform with browser
    <UnsupportedOSPlatform("browser")>
    Public Sub NotBrowserMethod()
        Console.WriteLine("Not supported in browser")
    End Sub
    
End Class

' Violation 9: SupportedOSPlatform on another class
<SupportedOSPlatform("android")>
Public Class AndroidSpecificClass
    
    Public Sub DoAndroidStuff()
        Console.WriteLine("Android-specific functionality")
    End Sub
    
End Class

' Violation 10: UnsupportedOSPlatform on structure
<UnsupportedOSPlatform("tvos")>
Public Structure NotTvOSStruct
    Public Value As Integer
End Structure

' This should NOT be detected - class without platform attributes
Public Class RegularClass
    
    Public Sub RegularMethod()
        Console.WriteLine("Regular functionality")
    End Sub
    
End Class

' Violation 11: Multiple platform attributes
<SupportedOSPlatform("windows")>
<UnsupportedOSPlatform("windows7.0")>
Public Class ComplexPlatformClass
    
    Public Sub ComplexMethod()
        Console.WriteLine("Supported on Windows but not Windows 7")
    End Sub
    
End Class
