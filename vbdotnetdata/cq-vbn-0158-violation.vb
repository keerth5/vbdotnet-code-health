' Test file for cq-vbn-0158: RequiresAssemblyFilesAttribute cannot be placed directly on application entry point
' RequiresAssemblyFilesAttribute cannot be placed directly on application entry point

Imports System
Imports System.Diagnostics.CodeAnalysis

' Violation: RequiresAssemblyFiles on Main method (application entry point)
Public Class Program
    
    ' Violation: Main method with RequiresAssemblyFiles attribute
    <RequiresAssemblyFiles("Main method should not have RequiresAssemblyFiles")>
    Public Shared Sub Main(args() As String)
        Console.WriteLine("Application entry point with RequiresAssemblyFiles")
        Console.WriteLine($"Arguments count: {args.Length}")
    End Sub
    
End Class

' Violation: Another Main method variation
Public Class AlternativeProgram
    
    ' Violation: Main method without parameters with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Parameterless Main with RequiresAssemblyFiles")>
    Public Shared Sub Main()
        Console.WriteLine("Parameterless Main with RequiresAssemblyFiles")
    End Sub
    
End Class

' Violation: Main method with different access modifier
Public Class ThirdProgram
    
    ' Violation: Public Main method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Public Main method with RequiresAssemblyFiles")>
    Public Shared Sub Main(args() As String)
        Console.WriteLine("Public Main method with RequiresAssemblyFiles")
    End Sub
    
End Class

' Violation: Main method with complex attribute
Public Class ComplexProgram
    
    ' Violation: Main with complex RequiresAssemblyFiles attribute
    <RequiresAssemblyFiles("Complex Main method requiring assembly files", Url:="https://docs.microsoft.com")>
    Public Shared Sub Main(args() As String)
        Console.WriteLine("Complex Main method with detailed RequiresAssemblyFiles")
    End Sub
    
End Class

' Good practice: Main method without RequiresAssemblyFiles (should not be detected)
Public Class GoodProgram
    
    ' Good: Main method without RequiresAssemblyFiles
    Public Shared Sub Main(args() As String)
        Console.WriteLine("Good Main method without RequiresAssemblyFiles")
        
        ' Call other methods that may have RequiresAssemblyFiles
        ProcessFiles()
    End Sub
    
    ' Good: Other method can have RequiresAssemblyFiles
    <RequiresAssemblyFiles("Helper method can have RequiresAssemblyFiles")>
    Private Shared Sub ProcessFiles()
        Console.WriteLine("Helper method with RequiresAssemblyFiles")
    End Sub
    
End Class

' Violation: Main method in Module
Public Module MainModule
    
    ' Violation: Main method in module with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Module Main method with RequiresAssemblyFiles")>
    Public Sub Main(args() As String)
        Console.WriteLine("Module Main method with RequiresAssemblyFiles")
    End Sub
    
End Module

' Violation: Different Main method signatures
Public Class VariousMainSignatures
    
    ' Violation: Main returning Integer with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Main returning Integer with RequiresAssemblyFiles")>
    Public Shared Function Main(args() As String) As Integer
        Console.WriteLine("Main returning Integer with RequiresAssemblyFiles")
        Return 0
    End Function
    
End Class

' Another violation example
Public Class AnotherMainExample
    
    ' Violation: Main with different parameter name
    <RequiresAssemblyFiles("Main with different parameter name")>
    Public Shared Sub Main(arguments() As String)
        Console.WriteLine("Main with different parameter name and RequiresAssemblyFiles")
    End Sub
    
End Class

' Good: Non-Main methods can have RequiresAssemblyFiles
Public Class UtilityClass
    
    ' Good: Regular method with RequiresAssemblyFiles (not Main)
    <RequiresAssemblyFiles("Utility method can have RequiresAssemblyFiles")>
    Public Shared Sub UtilityMethod()
        Console.WriteLine("Utility method with RequiresAssemblyFiles")
    End Sub
    
    ' Good: Instance method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Instance method can have RequiresAssemblyFiles")>
    Public Sub InstanceMethod()
        Console.WriteLine("Instance method with RequiresAssemblyFiles")
    End Sub
    
End Class

' Violation: Main method with multiple attributes
Public Class MultiAttributeProgram
    
    ' Violation: Main with multiple attributes including RequiresAssemblyFiles
    <Obsolete("This Main method is obsolete")>
    <RequiresAssemblyFiles("Main method with multiple attributes")>
    Public Shared Sub Main(args() As String)
        Console.WriteLine("Main with multiple attributes including RequiresAssemblyFiles")
    End Sub
    
End Class

' Test with different casing and spacing
Public Class CasingTestProgram
    
    ' Violation: Main method with different casing in attribute
    <RequiresAssemblyFiles("MAIN METHOD WITH UPPERCASE DESCRIPTION")>
    Public Shared Sub Main(args() As String)
        Console.WriteLine("Main method with uppercase RequiresAssemblyFiles description")
    End Sub
    
End Class

' Violation: Friend Main method
Public Class FriendMainProgram
    
    ' Violation: Friend Main method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Friend Main method with RequiresAssemblyFiles")>
    Friend Shared Sub Main(args() As String)
        Console.WriteLine("Friend Main method with RequiresAssemblyFiles")
    End Sub
    
End Class

' Good: Other entry point methods without Main name
Public Class AlternativeEntryPoints
    
    ' Good: Not a Main method, can have RequiresAssemblyFiles
    <RequiresAssemblyFiles("Alternative entry point can have RequiresAssemblyFiles")>
    Public Shared Sub StartApplication(args() As String)
        Console.WriteLine("Alternative entry point with RequiresAssemblyFiles")
    End Sub
    
    ' Good: Not a Main method
    <RequiresAssemblyFiles("Run method can have RequiresAssemblyFiles")>
    Public Shared Sub Run()
        Console.WriteLine("Run method with RequiresAssemblyFiles")
    End Sub
    
End Class
