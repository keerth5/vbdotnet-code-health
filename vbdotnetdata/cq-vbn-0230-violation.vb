' VB.NET test file for cq-vbn-0230: Do not disable ServicePointManagerSecurityProtocols
' This rule detects disabling of ServicePointManager security protocols

Imports System

Public Class ServicePointManagerSecurityProtocolsViolations
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols set to True
    Public Sub DisableServicePointManagerSecurityProtocols()
        ' Violation: DisableUsingServicePointManagerSecurityProtocols = True
        DisableUsingServicePointManagerSecurityProtocols = True
        
        ' This limits TLS connections to TLS 1.0
    End Sub
    
    ' Violation: Multiple DisableUsingServicePointManagerSecurityProtocols assignments
    Public Sub MultipleDisableAssignments()
        Dim disableSecurity As Boolean = True
        
        ' Violation: DisableUsingServicePointManagerSecurityProtocols = True
        DisableUsingServicePointManagerSecurityProtocols = True
        
        ' Additional security-related code...
        If disableSecurity Then
            ' Violation: Another DisableUsingServicePointManagerSecurityProtocols = True
            DisableUsingServicePointManagerSecurityProtocols = True
        End If
    End Sub
    
    ' Violation: AppContext.SetSwitch for ServicePointManager security protocols
    Public Sub DisableUsingAppContextSetSwitch()
        ' Violation: AppContext.SetSwitch with DisableUsingServicePointManagerSecurityProtocols
        AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", True)
    End Sub
    
    ' Violation: Multiple AppContext.SetSwitch calls
    Public Sub MultipleAppContextSetSwitchCalls()
        ' Violation: AppContext.SetSwitch with security protocol switch
        AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", True)
        
        ' Other AppContext switches...
        AppContext.SetSwitch("Switch.System.Net.Http.UseSocketsHttpHandler", False)
        
        ' Violation: Another AppContext.SetSwitch with security protocol switch
        AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", True)
    End Sub
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols in try-catch
    Public Sub DisableSecurityProtocolsWithErrorHandling()
        Try
            ' Violation: DisableUsingServicePointManagerSecurityProtocols = True in try block
            DisableUsingServicePointManagerSecurityProtocols = True
            
        Catch ex As Exception
            Console.WriteLine("Error disabling security protocols: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols in conditional
    Public Sub ConditionalDisableSecurityProtocols(disableSecurity As Boolean)
        If disableSecurity Then
            ' Violation: DisableUsingServicePointManagerSecurityProtocols = True in conditional
            DisableUsingServicePointManagerSecurityProtocols = True
        End If
    End Sub
    
    ' Violation: AppContext.SetSwitch in conditional
    Public Sub ConditionalAppContextSetSwitch(useOldProtocols As Boolean)
        If useOldProtocols Then
            ' Violation: AppContext.SetSwitch in conditional
            AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", True)
        End If
    End Sub
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols in loop
    Public Sub DisableSecurityProtocolsInLoop()
        Dim conditions() As Boolean = {True, False, True}
        
        For Each condition As Boolean In conditions
            If condition Then
                ' Violation: DisableUsingServicePointManagerSecurityProtocols = True in loop
                DisableUsingServicePointManagerSecurityProtocols = True
            End If
        Next
    End Sub
    
    ' Violation: AppContext.SetSwitch in loop
    Public Sub AppContextSetSwitchInLoop()
        Dim switches() As String = {
            "Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols",
            "Switch.System.Net.Http.UseSocketsHttpHandler"
        }
        
        For Each switchName As String In switches
            If switchName.Contains("DisableUsingServicePointManagerSecurityProtocols") Then
                ' Violation: AppContext.SetSwitch in loop
                AppContext.SetSwitch(switchName, True)
            End If
        Next
    End Sub
    
    ' Good example (should not be detected - setting to False)
    Public Sub EnableServicePointManagerSecurityProtocols()
        ' Good: Setting DisableUsingServicePointManagerSecurityProtocols to False
        DisableUsingServicePointManagerSecurityProtocols = False
        
        ' This enables modern TLS protocols
    End Sub
    
    ' Good example (should not be detected - AppContext.SetSwitch with False)
    Public Sub EnableUsingAppContextSetSwitch()
        ' Good: AppContext.SetSwitch with False value
        AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", False)
    End Sub
    
    ' Good example (should not be detected - different switch)
    Public Sub SetOtherAppContextSwitches()
        ' Good: Using other AppContext switches
        AppContext.SetSwitch("Switch.System.Net.Http.UseSocketsHttpHandler", True)
        AppContext.SetSwitch("Switch.System.Globalization.NoAsyncCurrentCulture", False)
    End Sub
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols with variable
    Public Sub DisableSecurityProtocolsWithVariable()
        Dim disableProtocols As Boolean = True
        
        ' Violation: DisableUsingServicePointManagerSecurityProtocols = True with variable
        DisableUsingServicePointManagerSecurityProtocols = disableProtocols
    End Sub
    
    ' Violation: AppContext.SetSwitch with variable
    Public Sub AppContextSetSwitchWithVariable()
        Dim switchName As String = "Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols"
        Dim switchValue As Boolean = True
        
        ' Violation: AppContext.SetSwitch with variables
        AppContext.SetSwitch(switchName, switchValue)
    End Sub
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols in method
    Public Sub ConfigureSecurityProtocols()
        Console.WriteLine("Configuring security protocols...")
        
        ' Violation: DisableUsingServicePointManagerSecurityProtocols = True
        DisableUsingServicePointManagerSecurityProtocols = True
        
        Console.WriteLine("Security protocols configured")
    End Sub
    
    ' Violation: AppContext.SetSwitch in initialization method
    Public Sub InitializeApplication()
        Console.WriteLine("Initializing application...")
        
        ' Set various application switches
        AppContext.SetSwitch("Switch.System.Net.Http.UseSocketsHttpHandler", False)
        
        ' Violation: AppContext.SetSwitch for security protocols
        AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", True)
        
        Console.WriteLine("Application initialized")
    End Sub
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols in property
    Public WriteOnly Property SecurityProtocolsDisabled() As Boolean
        Set(value As Boolean)
            If value Then
                ' Violation: DisableUsingServicePointManagerSecurityProtocols = True in property
                DisableUsingServicePointManagerSecurityProtocols = True
            End If
        End Set
    End Property
    
    ' Violation: Complex security configuration
    Public Sub ComplexSecurityConfiguration()
        ' Configuration logic
        Dim useLegacyProtocols As Boolean = CheckLegacyRequirement()
        
        If useLegacyProtocols Then
            Console.WriteLine("Using legacy TLS protocols")
            
            ' Violation: DisableUsingServicePointManagerSecurityProtocols = True
            DisableUsingServicePointManagerSecurityProtocols = True
            
            ' Additional legacy configuration
            ConfigureLegacySettings()
        End If
    End Sub
    
    Private Function CheckLegacyRequirement() As Boolean
        ' Logic to check if legacy protocols are required
        Return True
    End Function
    
    Private Sub ConfigureLegacySettings()
        ' Additional legacy configuration
    End Sub
    
    ' Violation: AppContext.SetSwitch in static constructor equivalent
    Shared Sub New()
        ' Violation: AppContext.SetSwitch in shared constructor
        AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", True)
    End Sub
    
    ' Violation: DisableUsingServicePointManagerSecurityProtocols in field initialization
    Private Shared ReadOnly securityDisabled As Boolean = SetSecurityProtocols()
    
    Private Shared Function SetSecurityProtocols() As Boolean
        ' Violation: DisableUsingServicePointManagerSecurityProtocols = True in initialization
        DisableUsingServicePointManagerSecurityProtocols = True
        Return True
    End Function
    
    ' Violation: AppContext.SetSwitch with string concatenation
    Public Sub AppContextSetSwitchWithConcatenation()
        Dim baseSwitchName As String = "Switch.System.ServiceModel."
        Dim switchSuffix As String = "DisableUsingServicePointManagerSecurityProtocols"
        
        ' Violation: AppContext.SetSwitch with concatenated switch name
        AppContext.SetSwitch(baseSwitchName & switchSuffix, True)
    End Sub
    
    ' Violation: Multiple security protocol configurations
    Public Sub ConfigureMultipleSecuritySettings()
        ' Violation: DisableUsingServicePointManagerSecurityProtocols = True
        DisableUsingServicePointManagerSecurityProtocols = True
        
        ' Violation: AppContext.SetSwitch for same setting
        AppContext.SetSwitch("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", True)
        
        ' Additional security configurations...
        Console.WriteLine("Multiple security settings configured")
    End Sub
End Class
