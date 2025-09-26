' VB.NET test file for cq-vbn-0109: Mark assemblies with NeutralResourcesLanguageAttribute
' Rule: The NeutralResourcesLanguage attribute informs the Resource Manager of the language that 
' was used to display the resources of a neutral culture for an assembly. This improves lookup 
' performance for the first resource that you load and can reduce your working set.

Imports System
Imports System.Reflection
Imports System.Resources

' Violation: Assembly declaration without NeutralResourcesLanguageAttribute
<Assembly: AssemblyTitle("Sample Application")>
<Assembly: AssemblyDescription("A sample VB.NET application")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("Sample Company")>
<Assembly: AssemblyProduct("Sample Product")>
<Assembly: AssemblyCopyright("Copyright © 2023")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>

' Violation: Another assembly attribute without NeutralResourcesLanguageAttribute
<Assembly: AssemblyMetadata("Author", "John Doe")>

' Violation: Assembly with CLSCompliant but no NeutralResourcesLanguageAttribute
<Assembly: CLSCompliant(True)>

' Violation: Assembly with ComVisible but no NeutralResourcesLanguageAttribute
<Assembly: ComVisible(False)>

Public Class ResourceExample
    
    Public Sub LoadResources()
        ' This class uses resources but the assembly lacks NeutralResourcesLanguageAttribute
        Dim resourceManager As New ResourceManager("MyApp.Resources", Me.GetType().Assembly)
        
        Try
            Dim message As String = resourceManager.GetString("WelcomeMessage")
            Console.WriteLine(message)
        Catch ex As Exception
            Console.WriteLine("Error loading resources: " & ex.Message)
        End Try
    End Sub
    
End Class

Public Class LocalizationExample
    
    Private _resourceManager As ResourceManager
    
    Public Sub New()
        _resourceManager = New ResourceManager("MyApp.Strings", Me.GetType().Assembly)
    End Sub
    
    Public Function GetLocalizedString(key As String) As String
        Try
            Return _resourceManager.GetString(key)
        Catch
            Return $"[{key}]"
        End Try
    End Function
    
    Public Sub DisplayMessages()
        Console.WriteLine(GetLocalizedString("Title"))
        Console.WriteLine(GetLocalizedString("Description"))
        Console.WriteLine(GetLocalizedString("Instructions"))
    End Sub
    
End Class

Public Class CultureExample
    
    Public Sub SetCulture()
        ' Working with cultures but assembly lacks NeutralResourcesLanguageAttribute
        Dim currentCulture = Threading.Thread.CurrentThread.CurrentCulture
        Console.WriteLine($"Current culture: {currentCulture.Name}")
        
        ' Load culture-specific resources
        LoadCultureResources(currentCulture.Name)
    End Sub
    
    Private Sub LoadCultureResources(cultureName As String)
        Try
            Dim rm As New ResourceManager("MyApp.CultureResources", Me.GetType().Assembly)
            Dim cultureInfo As New Globalization.CultureInfo(cultureName)
            
            Dim localizedText = rm.GetString("LocalizedText", cultureInfo)
            Console.WriteLine(localizedText)
        Catch ex As Exception
            Console.WriteLine($"Failed to load resources for culture {cultureName}: {ex.Message}")
        End Try
    End Sub
    
End Class

' More assembly attributes that should trigger the violation
<Assembly: AssemblyInformationalVersion("1.0.0-beta")>
<Assembly: AssemblyDelaySign(False)>

Public Module ResourceUtilities
    
    Public Function GetResourceString(resourceName As String, key As String) As String
        Try
            Dim assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim rm As New ResourceManager(resourceName, assembly)
            Return rm.GetString(key)
        Catch
            Return String.Empty
        End Try
    End Function
    
    Public Sub LoadAllResources()
        ' Load various resource files
        LoadStringResources()
        LoadImageResources()
        LoadConfigResources()
    End Sub
    
    Private Sub LoadStringResources()
        Dim strings = GetResourceString("MyApp.Strings", "AppName")
        Console.WriteLine($"App Name: {strings}")
    End Sub
    
    Private Sub LoadImageResources()
        ' Load image resources
        Console.WriteLine("Loading image resources...")
    End Sub
    
    Private Sub LoadConfigResources()
        ' Load configuration resources
        Console.WriteLine("Loading configuration resources...")
    End Sub
    
End Module

' Non-violation examples (these should not be detected):

' Note: The following would be correct usage with NeutralResourcesLanguageAttribute,
' but since this file is specifically for testing violations, we don't include them here.
' In a real scenario, the assembly would have:
' <Assembly: NeutralResourcesLanguage("en-US")>

Public Class NonResourceClass
    
    ' This class doesn't use resources, but the assembly still should have 
    ' NeutralResourcesLanguageAttribute for performance reasons
    
    Public Sub DoWork()
        Console.WriteLine("Performing work without resources")
    End Sub
    
    Public Function Calculate(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
End Class
