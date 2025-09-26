' Test file for cq-vbn-0156: Avoid calling members annotated with 'RequiresAssemblyFilesAttribute' when publishing as a single file
' Avoid calling members annotated with 'RequiresAssemblyFilesAttribute' when publishing as a single file

Imports System
Imports System.Diagnostics.CodeAnalysis

Public Class RequiresAssemblyFilesTests
    
    ' Violation: Method annotated with RequiresAssemblyFiles
    <RequiresAssemblyFiles("This method requires assembly files")>
    Public Sub MethodThatRequiresFiles()
        Console.WriteLine("This method requires assembly files to be present")
    End Sub
    
    ' Violation: Function annotated with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Function needs file system access to assemblies")>
    Public Function GetAssemblyInfo() As String
        Return "Assembly information that requires file access"
    End Function
    
    ' Violation: Protected method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Protected method requiring assembly files")>
    Protected Sub ProtectedMethodWithFileRequirement()
        Console.WriteLine("Protected method that needs assembly files")
    End Sub
    
    ' Violation: Friend method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Friend method requiring file access")>
    Friend Sub FriendMethodWithFileRequirement()
        Console.WriteLine("Friend method that requires assembly files")
    End Sub
    
    ' Violation: Shared method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Static method requiring assembly files")>
    Public Shared Sub SharedMethodWithFileRequirement()
        Console.WriteLine("Shared method that requires assembly files")
    End Sub
    
    ' Violation: Method with complex RequiresAssemblyFiles attribute
    <RequiresAssemblyFiles("This method performs file I/O operations on assembly files", Url:="https://docs.microsoft.com/assembly-files")>
    Public Sub ComplexAnnotatedMethod()
        Console.WriteLine("Method with complex RequiresAssemblyFiles annotation")
    End Sub
    
    ' Good practice: Methods without RequiresAssemblyFiles (should not be detected)
    Public Sub RegularMethod()
        Console.WriteLine("Regular method without file requirements")
    End Sub
    
    Public Function RegularFunction() As String
        Return "Regular function"
    End Function
    
    ' Violation: Multiple methods with RequiresAssemblyFiles
    <RequiresAssemblyFiles("First method requiring files")>
    Public Sub FirstFileRequiredMethod()
        Console.WriteLine("First method requiring assembly files")
    End Sub
    
    <RequiresAssemblyFiles("Second method requiring files")>
    Public Sub SecondFileRequiredMethod()
        Console.WriteLine("Second method requiring assembly files")
    End Sub
    
    ' Violation: Property with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Property requiring assembly file access")>
    Public ReadOnly Property FileBasedProperty As String
        Get
            Return "Property that requires file access"
        End Get
    End Property
    
End Class

' Interface with RequiresAssemblyFiles methods
Public Interface IFileRequiredOperations
    
    ' Violation: Interface method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Interface method requiring assembly files")>
    Sub ProcessAssemblyFiles()
    
    ' Violation: Interface function with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Interface function requiring file access")>
    Function GetFileBasedData() As String
    
End Interface

' Implementation of interface
Public Class FileRequiredOperationsImpl
    Implements IFileRequiredOperations
    
    ' Violation: Implementation with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Implementation requiring assembly files")>
    Public Sub ProcessAssemblyFiles() Implements IFileRequiredOperations.ProcessAssemblyFiles
        Console.WriteLine("Processing assembly files")
    End Sub
    
    ' Violation: Implementation function with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Implementation function requiring files")>
    Public Function GetFileBasedData() As String Implements IFileRequiredOperations.GetFileBasedData
        Return "File-based data"
    End Function
    
End Class

' Abstract class with RequiresAssemblyFiles methods
Public MustInherit Class AbstractFileOperations
    
    ' Violation: Abstract method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Abstract method requiring assembly files")>
    Public MustOverride Sub AbstractFileMethod()
    
    ' Violation: Virtual method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Virtual method requiring files")>
    Public Overridable Sub VirtualFileMethod()
        Console.WriteLine("Virtual method requiring assembly files")
    End Sub
    
End Class

' Concrete implementation of abstract class
Public Class ConcreteFileOperations
    Inherits AbstractFileOperations
    
    ' Violation: Override with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Override requiring assembly files")>
    Public Overrides Sub AbstractFileMethod()
        Console.WriteLine("Concrete implementation requiring files")
    End Sub
    
    ' Violation: Override of virtual method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Override of virtual method requiring files")>
    Public Overrides Sub VirtualFileMethod()
        Console.WriteLine("Override requiring assembly files")
    End Sub
    
End Class

' Module with RequiresAssemblyFiles methods
Public Module FileUtilities
    
    ' Violation: Module method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Module method requiring assembly files")>
    Public Sub ModuleMethodWithFileRequirement()
        Console.WriteLine("Module method that requires assembly files")
    End Sub
    
    ' Violation: Module function with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Module function requiring file access")>
    Public Function GetModuleFileData() As String
        Return "Module data requiring file access"
    End Function
    
End Module

' Generic class with RequiresAssemblyFiles methods
Public Class GenericFileOperations(Of T)
    
    ' Violation: Generic method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Generic method requiring assembly files")>
    Public Sub ProcessGenericFileData(data As T)
        Console.WriteLine($"Processing generic data requiring files: {data}")
    End Sub
    
    ' Violation: Generic function with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Generic function requiring file access")>
    Public Function GetGenericFileResult() As T
        Return Nothing ' Placeholder implementation
    End Function
    
End Class

' Extension methods simulation (in module)
Public Module ExtensionMethods
    
    ' Violation: Extension method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Extension method requiring assembly files")>
    <System.Runtime.CompilerServices.Extension>
    Public Sub ProcessWithFileRequirement(text As String)
        Console.WriteLine($"Processing text with file requirement: {text}")
    End Sub
    
End Module

' Nested class with RequiresAssemblyFiles
Public Class OuterClass
    
    Public Class NestedClass
        
        ' Violation: Nested class method with RequiresAssemblyFiles
        <RequiresAssemblyFiles("Nested method requiring assembly files")>
        Public Sub NestedMethodWithFileRequirement()
            Console.WriteLine("Nested method requiring assembly files")
        End Sub
        
    End Class
    
End Class
