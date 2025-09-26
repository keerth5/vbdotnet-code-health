' VB.NET test file for cq-vbn-0244: Do not use unsafe DllImportSearchPath value
' This rule detects usage of unsafe DllImportSearchPath values

Imports System
Imports System.Runtime.InteropServices

' Violation: Using ApplicationDirectory
Public Class UnsafeSearchPath1
    ' Violation: DllImportSearchPath.ApplicationDirectory is unsafe
    <DllImport("mylib.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.ApplicationDirectory)>
    Public Shared Function UnsafeFunction1() As Integer
    End Function
End Class

' Violation: Using LegacyBehavior
Public Class UnsafeSearchPath2
    ' Violation: DllImportSearchPath.LegacyBehavior is unsafe
    <DllImport("legacy.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)>
    Public Shared Function UnsafeFunction2() As Boolean
    End Function
End Class

' Violation: Using AssemblyDirectory
Public Class UnsafeSearchPath3
    ' Violation: DllImportSearchPath.AssemblyDirectory is unsafe
    <DllImport("assembly.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)>
    Public Shared Sub UnsafeFunction3()
    End Sub
End Class

' Violation: Using UseDllDirectoryForDependencies
Public Class UnsafeSearchPath4
    ' Violation: DllImportSearchPath.UseDllDirectoryForDependencies is unsafe
    <DllImport("dependency.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.UseDllDirectoryForDependencies)>
    Public Shared Function UnsafeFunction4(param As String) As IntPtr
    End Function
End Class

' Violation: Combining unsafe paths with safe ones
Public Class UnsafeSearchPath5
    ' Violation: ApplicationDirectory combined with System32 (still unsafe)
    <DllImport("combined.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.ApplicationDirectory Or DllImportSearchPath.System32)>
    Public Shared Function UnsafeFunction5() As UInteger
    End Function
    
    ' Violation: LegacyBehavior combined with UserDirectories (still unsafe)
    <DllImport("legacy2.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior Or DllImportSearchPath.UserDirectories)>
    Public Shared Function UnsafeFunction6() As Integer
    End Function
End Class

' Violation: Multiple methods with different unsafe paths
Public Class UnsafeSearchPath6
    ' Violation: AssemblyDirectory
    <DllImport("lib1.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)>
    Public Shared Function Method1() As Boolean
    End Function
    
    ' Violation: UseDllDirectoryForDependencies
    <DllImport("lib2.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.UseDllDirectoryForDependencies)>
    Public Shared Function Method2() As Integer
    End Function
    
    ' Violation: ApplicationDirectory
    <DllImport("lib3.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.ApplicationDirectory)>
    Public Shared Sub Method3()
    End Sub
End Class

' Violation: Using unsafe paths in different contexts
Public Class UnsafeSearchPath7
    ' Violation: Private method with unsafe path
    <DllImport("private.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)>
    Private Shared Function PrivateUnsafeMethod() As IntPtr
    End Function
    
    ' Violation: Friend method with unsafe path
    <DllImport("friend.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.ApplicationDirectory)>
    Friend Shared Function FriendUnsafeMethod() As String
    End Function
    
    ' Violation: Public method with unsafe path
    <DllImport("public.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)>
    Public Shared Function PublicUnsafeMethod() As Boolean
    End Function
End Class

' Violation: Complex combinations of unsafe paths
Public Class UnsafeSearchPath8
    ' Violation: Multiple unsafe paths combined
    <DllImport("multi1.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.ApplicationDirectory Or 
                               DllImportSearchPath.AssemblyDirectory)>
    Public Shared Function MultiUnsafe1() As Integer
    End Function
    
    ' Violation: Unsafe path with safe paths
    <DllImport("multi2.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.System32 Or 
                               DllImportSearchPath.UseDllDirectoryForDependencies Or 
                               DllImportSearchPath.UserDirectories)>
    Public Shared Function MultiUnsafe2() As Boolean
    End Function
End Class

' Violation: Using unsafe paths in static constructor context
Public Class UnsafeSearchPath9
    ' Violation: LegacyBehavior in static context
    <DllImport("static.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.LegacyBehavior)>
    Public Shared Function StaticUnsafeMethod() As Integer
    End Function
    
    Shared Sub New()
        ' Static constructor that might call unsafe P/Invoke
        Dim result As Integer = StaticUnsafeMethod()
    End Sub
End Class

' Violation: Conditional compilation with unsafe paths
Public Class UnsafeSearchPath10
#If DEBUG Then
    ' Violation: ApplicationDirectory in debug build
    <DllImport("debug.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.ApplicationDirectory)>
    Public Shared Function DebugUnsafeMethod() As Boolean
    End Function
#Else
    ' Violation: AssemblyDirectory in release build
    <DllImport("release.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)>
    Public Shared Function ReleaseUnsafeMethod() As Boolean
    End Function
#End If
End Class

' Good examples (should not be detected as violations)
Public Class SafeSearchPaths
    ' Good: Using System32 (safe)
    <DllImport("kernel32.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.System32)>
    Public Shared Function SafeMethod1() As Integer
    End Function
    
    ' Good: Using UserDirectories (safe)
    <DllImport("user.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)>
    Public Shared Function SafeMethod2() As Boolean
    End Function
    
    ' Good: Using SafeDirectories (safe)
    <DllImport("safe.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)>
    Public Shared Function SafeMethod3() As IntPtr
    End Function
    
    ' Good: Combining safe paths
    <DllImport("combined.dll")>
    <DefaultDllImportSearchPaths(DllImportSearchPath.System32 Or DllImportSearchPath.UserDirectories)>
    Public Shared Function SafeMethod4() As UInteger
    End Function
    
    ' Good: Method without P/Invoke
    Public Shared Function ManagedMethod() As String
        Return "This is a managed method"
    End Function
End Class
