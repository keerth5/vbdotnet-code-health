' VB.NET test file for cq-vbn-0238: Avoid hardcoding SecurityProtocolType value
' This rule detects hardcoded TLS protocol versions which should be avoided

Imports System
Imports System.Net

' Violation: Hardcoding SSL3 protocol
Public Class HardcodedProtocols1
    Public Sub SetSSL3Protocol()
        ' Violation: Hardcoding SSL3 which is deprecated
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
        
        ' Make web request...
        Console.WriteLine("Using SSL3 protocol")
    End Sub
End Class

' Violation: Hardcoding TLS 1.0 protocol
Public Class HardcodedProtocols2
    Public Sub SetTLS10Protocol()
        ' Violation: Hardcoding TLS 1.0 which is deprecated
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        
        ' Perform network operation
        Console.WriteLine("Using TLS 1.0 protocol")
    End Sub
End Class

' Violation: Hardcoding TLS 1.1 protocol
Public Class HardcodedProtocols3
    Public Sub SetTLS11Protocol()
        ' Violation: Hardcoding TLS 1.1 which is deprecated
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11
        
        ' Network communication
        Console.WriteLine("Using TLS 1.1 protocol")
    End Sub
End Class

' Violation: Hardcoding TLS 1.2 protocol
Public Class HardcodedProtocols4
    Public Sub SetTLS12Protocol()
        ' Violation: Hardcoding TLS 1.2 (should use SystemDefault)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        
        ' Web service call
        Console.WriteLine("Using TLS 1.2 protocol")
    End Sub
End Class

' Violation: Hardcoding TLS 1.3 protocol
Public Class HardcodedProtocols5
    Public Sub SetTLS13Protocol()
        ' Violation: Hardcoding TLS 1.3 (should use SystemDefault)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13
        
        ' HTTPS request
        Console.WriteLine("Using TLS 1.3 protocol")
    End Sub
End Class

' Violation: Using CType to cast numeric values
Public Class HardcodedProtocols6
    Public Sub SetProtocolWithCast()
        ' Violation: Using CType to cast hardcoded numeric value
        ServicePointManager.SecurityProtocol = CType(192, SecurityProtocolType)
        
        Console.WriteLine("Using protocol with numeric cast")
    End Sub
End Class

' Violation: Using CType with different numeric values
Public Class HardcodedProtocols7
    Public Sub SetProtocolWithDifferentCast()
        ' Violation: Using CType with hardcoded value
        ServicePointManager.SecurityProtocol = CType(768, SecurityProtocolType)
        
        Console.WriteLine("Using another hardcoded protocol")
    End Sub
End Class

' Violation: Multiple hardcoded protocol assignments
Public Class HardcodedProtocols8
    Public Sub SetMultipleProtocols()
        ' Violation: First hardcoded assignment
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        
        ' Some logic...
        If DateTime.Now.Hour > 12 Then
            ' Violation: Second hardcoded assignment
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        End If
    End Sub
End Class

' Violation: Combining protocols with hardcoded values
Public Class HardcodedProtocols9
    Public Sub CombineProtocols()
        ' Violation: Combining hardcoded protocol types
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        
        Console.WriteLine("Using combined protocols")
    End Sub
End Class

' Violation: Using in property assignment
Public Class HardcodedProtocols10
    Private _securityProtocol As SecurityProtocolType
    
    Public Property SecurityProtocol As SecurityProtocolType
        Get
            Return _securityProtocol
        End Get
        Set(value As SecurityProtocolType)
            _securityProtocol = value
        End Set
    End Property
    
    Public Sub SetProtocolProperty()
        ' Violation: Hardcoding in property assignment
        SecurityProtocol = SecurityProtocolType.Tls11
        ServicePointManager.SecurityProtocol = SecurityProtocol
    End Sub
End Class

' Violation: Using in conditional assignment
Public Class HardcodedProtocols11
    Public Sub ConditionalProtocolAssignment(useTLS12 As Boolean)
        If useTLS12 Then
            ' Violation: Conditional hardcoded assignment
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Else
            ' Violation: Another conditional hardcoded assignment
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11
        End If
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureProtocolSettings
    Public Sub UseSystemDefault()
        ' Good: Using SystemDefault lets the OS choose the best protocol
        ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault
        
        Console.WriteLine("Using system default protocol")
    End Sub
    
    Public Sub UseConfigurationValue()
        ' Good: Reading from configuration instead of hardcoding
        Dim configProtocol As String = System.Configuration.ConfigurationManager.AppSettings("SecurityProtocol")
        
        ' Parse and set based on configuration
        If Not String.IsNullOrEmpty(configProtocol) Then
            Console.WriteLine($"Using configured protocol: {configProtocol}")
        End If
    End Sub
    
    Public Sub UseEnvironmentVariable()
        ' Good: Using environment variable instead of hardcoding
        Dim envProtocol As String = Environment.GetEnvironmentVariable("TLS_PROTOCOL")
        
        If Not String.IsNullOrEmpty(envProtocol) Then
            Console.WriteLine($"Using environment protocol: {envProtocol}")
        End If
    End Sub
    
    Public Sub DynamicProtocolSelection()
        ' Good: Dynamic protocol selection based on runtime conditions
        If Environment.Version.Major >= 4 Then
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault
        End If
        
        Console.WriteLine("Using dynamically selected protocol")
    End Sub
End Class
