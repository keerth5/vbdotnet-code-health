' Test file for cq-vbn-0155: Avoid accessing Assembly file path when publishing as a single file
' Avoid accessing Assembly file path when publishing as a single file

Imports System
Imports System.Reflection

Public Class AssemblyFilePathTests
    
    ' Violation: Accessing Assembly.Location
    Public Sub GetAssemblyLocation()
        Dim currentAssembly As Assembly = Assembly.GetExecutingAssembly()
        
        ' Violation: Accessing Location property
        Dim location As String = currentAssembly.Location
        
        Console.WriteLine($"Assembly location: {location}")
    End Sub
    
    ' Violation: Accessing Assembly.CodeBase
    Public Sub GetAssemblyCodeBase()
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        
        ' Violation: Accessing CodeBase property
        Dim codeBase As String = assembly.CodeBase
        
        Console.WriteLine($"Assembly code base: {codeBase}")
    End Sub
    
    ' Violation: Direct Assembly.GetExecutingAssembly().Location
    Public Sub GetCurrentAssemblyLocation()
        ' Violation: Direct access to Location
        Dim path As String = Assembly.GetExecutingAssembly().Location
        
        If Not String.IsNullOrEmpty(path) Then
            Console.WriteLine($"Current assembly path: {path}")
        End If
    End Sub
    
    ' Violation: Getting entry assembly location
    Public Sub GetEntryAssemblyLocation()
        Dim entryAssembly As Assembly = Assembly.GetEntryAssembly()
        
        ' Violation: Accessing Location of entry assembly
        Dim location As String = entryAssembly.Location
        
        Console.WriteLine($"Entry assembly location: {location}")
    End Sub
    
    ' Violation: Assembly location in file operations
    Public Sub GetAssemblyDirectory()
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        
        ' Violation: Using Location for directory operations
        Dim location As String = assembly.Location
        Dim directory As String = System.IO.Path.GetDirectoryName(location)
        
        Console.WriteLine($"Assembly directory: {directory}")
    End Sub
    
    ' Violation: Multiple assembly location accesses
    Public Sub CompareAssemblyLocations()
        Dim currentAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim entryAssembly As Assembly = Assembly.GetEntryAssembly()
        
        ' Violation: First Location access
        Dim currentLocation As String = currentAssembly.Location
        
        ' Violation: Second Location access
        Dim entryLocation As String = entryAssembly.Location
        
        Console.WriteLine($"Current: {currentLocation}")
        Console.WriteLine($"Entry: {entryLocation}")
    End Sub
    
    ' Violation: Assembly CodeBase for URI operations
    Public Sub GetAssemblyUri()
        Dim assembly As Assembly = Assembly.GetCallingAssembly()
        
        ' Violation: Using CodeBase property
        Dim codeBaseUri As String = assembly.CodeBase
        
        If codeBaseUri.StartsWith("file:///") Then
            Console.WriteLine($"Assembly URI: {codeBaseUri}")
        End If
    End Sub
    
    ' Good practice: Alternative approaches (should not be detected)
    Public Sub GetAssemblyName()
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        
        ' Good: Using AssemblyName instead of file path
        Dim assemblyName As AssemblyName = assembly.GetName()
        
        Console.WriteLine($"Assembly name: {assemblyName.Name}")
        Console.WriteLine($"Version: {assemblyName.Version}")
    End Sub
    
    ' Good: Using AppDomain.CurrentDomain.BaseDirectory
    Public Sub GetApplicationBaseDirectory()
        ' Good: Using BaseDirectory instead of assembly location
        Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
        
        Console.WriteLine($"Application base directory: {baseDir}")
    End Sub
    
    ' Violation: Assembly location in exception handling
    Public Sub HandleAssemblyErrors()
        Try
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            
            ' Violation: Location access in try block
            Dim location As String = assembly.Location
            
            ' Some operation that might fail
            Dim info As New System.IO.FileInfo(location)
            Console.WriteLine($"Assembly size: {info.Length}")
            
        Catch ex As Exception
            Console.WriteLine($"Error accessing assembly: {ex.Message}")
        End Try
    End Sub
    
    ' Violation: Assembly location for loading related assemblies
    Public Sub LoadRelatedAssembly()
        Dim currentAssembly As Assembly = Assembly.GetExecutingAssembly()
        
        ' Violation: Using Location to find related assemblies
        Dim currentLocation As String = currentAssembly.Location
        Dim assemblyDir As String = System.IO.Path.GetDirectoryName(currentLocation)
        Dim relatedPath As String = System.IO.Path.Combine(assemblyDir, "RelatedAssembly.dll")
        
        Console.WriteLine($"Looking for related assembly at: {relatedPath}")
    End Sub
    
End Class

' Additional test cases
Public Module AssemblyUtilities
    
    ' Violation: Utility method accessing Location
    Public Function GetCurrentAssemblyPath() As String
        ' Violation: Returning assembly Location
        Return Assembly.GetExecutingAssembly().Location
    End Function
    
    ' Violation: Utility method accessing CodeBase
    Public Function GetAssemblyCodeBaseUri() As String
        Dim assembly As Assembly = Assembly.GetCallingAssembly()
        
        ' Violation: Returning CodeBase
        Return assembly.CodeBase
    End Function
    
    ' Violation: Check if assembly is in specific location
    Public Function IsAssemblyInTempFolder() As Boolean
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        
        ' Violation: Using Location for path comparison
        Dim location As String = assembly.Location
        
        Return location.Contains("Temp") OrElse location.Contains("tmp")
    End Function
    
    ' Good: Alternative utility methods
    Public Function GetAssemblyFullName() As String
        Return Assembly.GetExecutingAssembly().FullName
    End Function
    
    Public Function GetAssemblyVersion() As Version
        Return Assembly.GetExecutingAssembly().GetName().Version
    End Function
    
    ' Violation: Assembly location for logging
    Public Sub LogAssemblyInfo()
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        
        ' Violation: Location in logging
        Dim location As String = assembly.Location
        Dim name As String = assembly.GetName().Name
        
        Console.WriteLine($"Assembly '{name}' loaded from: {location}")
    End Sub
    
End Module

' Test with different assembly loading scenarios
Public Class AssemblyLoadingTests
    
    ' Violation: Location access after loading assembly
    Public Sub LoadAndGetLocation()
        Try
            Dim assembly As Assembly = Assembly.LoadFrom("SomeAssembly.dll")
            
            ' Violation: Accessing Location of loaded assembly
            Dim loadedLocation As String = assembly.Location
            
            Console.WriteLine($"Loaded assembly location: {loadedLocation}")
            
        Catch ex As Exception
            Console.WriteLine($"Failed to load assembly: {ex.Message}")
        End Try
    End Sub
    
    ' Violation: CodeBase access for loaded assembly
    Public Sub LoadAndGetCodeBase()
        Try
            Dim assembly As Assembly = Assembly.LoadFile("C:\Path\To\Assembly.dll")
            
            ' Violation: Accessing CodeBase of loaded assembly
            Dim codeBase As String = assembly.CodeBase
            
            Console.WriteLine($"Loaded assembly code base: {codeBase}")
            
        Catch ex As Exception
            Console.WriteLine($"Failed to load assembly: {ex.Message}")
        End Try
    End Sub
    
End Class
