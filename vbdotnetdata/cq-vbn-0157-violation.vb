' Test file for cq-vbn-0157: 'RequiresAssemblyFilesAttribute' annotations must match across all interface implementations or overrides
' RequiresAssemblyFilesAttribute annotations must match across all interface implementations or overrides

Imports System
Imports System.Diagnostics.CodeAnalysis

' Interface defining methods that may require assembly files
Public Interface IAssemblyFileOperations
    
    Sub ProcessFiles()
    Function GetFileData() As String
    Sub ProcessWithRequirement()
    
End Interface

' Violation: Implementation with mismatched RequiresAssemblyFiles on interface method
Public Class MismatchedImplementation
    Implements IAssemblyFileOperations
    
    ' Good: No annotation mismatch
    Public Sub ProcessFiles() Implements IAssemblyFileOperations.ProcessFiles
        Console.WriteLine("Processing files without requirement")
    End Sub
    
    ' Good: No annotation mismatch
    Public Function GetFileData() As String Implements IAssemblyFileOperations.GetFileData
        Return "File data"
    End Function
    
    ' Violation: Implementation has RequiresAssemblyFiles but interface doesn't
    <RequiresAssemblyFiles("Implementation requires files but interface doesn't")>
    Public Sub ProcessWithRequirement() Implements IAssemblyFileOperations.ProcessWithRequirement
        Console.WriteLine("Implementation with file requirement")
    End Sub
    
End Class

' Base class with virtual methods
Public Class BaseFileOperations
    
    ' Virtual method without RequiresAssemblyFiles
    Public Overridable Sub VirtualMethod()
        Console.WriteLine("Base virtual method")
    End Sub
    
    ' Virtual method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Base method requires assembly files")>
    Public Overridable Sub VirtualMethodWithRequirement()
        Console.WriteLine("Base virtual method with requirement")
    End Sub
    
    ' Abstract-like method (MustOverride simulation)
    Public Overridable Sub OverridableMethod()
        Console.WriteLine("Base overridable method")
    End Sub
    
End Class

' Violation: Derived class with mismatched RequiresAssemblyFiles annotations
Public Class MismatchedDerived
    Inherits BaseFileOperations
    
    ' Violation: Override adds RequiresAssemblyFiles when base doesn't have it
    <RequiresAssemblyFiles("Override adds requirement not in base")>
    Public Overrides Sub VirtualMethod()
        Console.WriteLine("Override with added requirement")
    End Sub
    
    ' Violation: Override removes RequiresAssemblyFiles when base has it
    Public Overrides Sub VirtualMethodWithRequirement()
        Console.WriteLine("Override without requirement")
    End Sub
    
    ' Good: Consistent annotation
    Public Overrides Sub OverridableMethod()
        Console.WriteLine("Override without mismatch")
    End Sub
    
End Class

' Abstract base class
Public MustInherit Class AbstractBaseOperations
    
    ' Abstract method without RequiresAssemblyFiles
    Public MustOverride Sub AbstractMethod()
    
    ' Abstract method with RequiresAssemblyFiles
    <RequiresAssemblyFiles("Abstract method requires assembly files")>
    Public MustOverride Sub AbstractMethodWithRequirement()
    
End Class

' Violation: Implementation with mismatched annotations
Public Class MismatchedAbstractImplementation
    Inherits AbstractBaseOperations
    
    ' Violation: Implementation adds RequiresAssemblyFiles when abstract doesn't have it
    <RequiresAssemblyFiles("Implementation adds requirement to abstract method")>
    Public Overrides Sub AbstractMethod()
        Console.WriteLine("Abstract implementation with added requirement")
    End Sub
    
    ' Violation: Implementation removes RequiresAssemblyFiles when abstract has it
    Public Overrides Sub AbstractMethodWithRequirement()
        Console.WriteLine("Abstract implementation without requirement")
    End Sub
    
End Class

' Interface with RequiresAssemblyFiles methods
Public Interface IFileRequiredInterface
    
    <RequiresAssemblyFiles("Interface method requires assembly files")>
    Sub RequiredMethod()
    
    Function RegularMethod() As String
    
End Interface

' Violation: Implementation with annotation mismatch
Public Class MismatchedInterfaceImplementation
    Implements IFileRequiredInterface
    
    ' Violation: Implementation removes RequiresAssemblyFiles when interface has it
    Public Sub RequiredMethod() Implements IFileRequiredInterface.RequiredMethod
        Console.WriteLine("Implementation without required annotation")
    End Sub
    
    ' Violation: Implementation adds RequiresAssemblyFiles when interface doesn't have it
    <RequiresAssemblyFiles("Implementation adds requirement to regular method")>
    Public Function RegularMethod() As String Implements IFileRequiredInterface.RegularMethod
        Return "Regular method with added requirement"
    End Function
    
End Class

' Multiple interface implementation scenario
Public Interface IFirstInterface
    <RequiresAssemblyFiles("First interface method requires files")>
    Sub FirstMethod()
End Interface

Public Interface ISecondInterface
    Sub SecondMethod()
End Interface

' Violation: Class implementing multiple interfaces with mismatched annotations
Public Class MultipleInterfaceImplementation
    Implements IFirstInterface, ISecondInterface
    
    ' Violation: Missing RequiresAssemblyFiles annotation
    Public Sub FirstMethod() Implements IFirstInterface.FirstMethod
        Console.WriteLine("First method without required annotation")
    End Sub
    
    ' Violation: Adding RequiresAssemblyFiles where not required
    <RequiresAssemblyFiles("Second method adds unnecessary requirement")>
    Public Sub SecondMethod() Implements ISecondInterface.SecondMethod
        Console.WriteLine("Second method with unnecessary annotation")
    End Sub
    
End Class

' Generic interface scenario
Public Interface IGenericFileOperations(Of T)
    <RequiresAssemblyFiles("Generic interface method requires files")>
    Sub ProcessGeneric(item As T)
End Interface

' Violation: Generic implementation with mismatched annotation
Public Class GenericMismatchedImplementation(Of T)
    Implements IGenericFileOperations(Of T)
    
    ' Violation: Missing RequiresAssemblyFiles annotation in generic implementation
    Public Sub ProcessGeneric(item As T) Implements IGenericFileOperations(Of T).ProcessGeneric
        Console.WriteLine($"Processing generic item: {item}")
    End Sub
    
End Class

' Property scenario
Public Interface IPropertyInterface
    <RequiresAssemblyFiles("Property requires assembly files")>
    ReadOnly Property FileBasedProperty As String
    
    Property RegularProperty As String
End Interface

' Violation: Property implementation with mismatched annotations
Public Class PropertyMismatchedImplementation
    Implements IPropertyInterface
    
    ' Violation: Property implementation missing RequiresAssemblyFiles
    Public ReadOnly Property FileBasedProperty As String Implements IPropertyInterface.FileBasedProperty
        Get
            Return "File-based property without annotation"
        End Get
    End Property
    
    ' Violation: Property implementation adding RequiresAssemblyFiles
    <RequiresAssemblyFiles("Regular property with added requirement")>
    Public Property RegularProperty As String Implements IPropertyInterface.RegularProperty
    
End Class

' Good practices: Consistent annotations
Public Interface IConsistentInterface
    <RequiresAssemblyFiles("Consistent method requires files")>
    Sub ConsistentMethod()
End Interface

Public Class ConsistentImplementation
    Implements IConsistentInterface
    
    ' Good: Matching annotation
    <RequiresAssemblyFiles("Consistent method requires files")>
    Public Sub ConsistentMethod() Implements IConsistentInterface.ConsistentMethod
        Console.WriteLine("Consistent implementation")
    End Sub
    
End Class
