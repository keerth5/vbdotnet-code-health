' VB.NET test file for cq-vbn-0249: Avoid hardcoded SslProtocols values
' This rule detects hardcoded SslProtocols values that should be configurable

Imports System
Imports System.Net.Security
Imports System.Security.Authentication

' Violation: Hardcoded SslProtocols.Tls12
Public Class HardcodedSslProtocols1
    Public Sub SetHardcodedTLS12()
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        ' Violation: Hardcoded TLS 1.2
        handler.SslProtocols = SslProtocols.Tls12
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
End Class

' Violation: Hardcoded SslProtocols.Tls13
Public Class HardcodedSslProtocols2
    Public Sub SetHardcodedTLS13()
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        ' Violation: Hardcoded TLS 1.3
        handler.SslProtocols = SslProtocols.Tls13
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
End Class

' Violation: Using CType to cast hardcoded numeric values
Public Class HardcodedSslProtocols3
    Public Sub SetProtocolWithCast()
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        ' Violation: Using CType to cast hardcoded numeric value
        handler.SslProtocols = CType(3072, SslProtocols)
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
End Class

' Violation: Multiple hardcoded protocol assignments
Public Class HardcodedSslProtocols4
    Public Sub SetMultipleHardcodedProtocols()
        Dim handler1 As New System.Net.Http.HttpClientHandler()
        Dim handler2 As New System.Net.Http.HttpClientHandler()
        
        ' Violation: First hardcoded assignment
        handler1.SslProtocols = SslProtocols.Tls12
        
        ' Violation: Second hardcoded assignment
        handler2.SslProtocols = SslProtocols.Tls13
        
        Dim client1 As New System.Net.Http.HttpClient(handler1)
        Dim client2 As New System.Net.Http.HttpClient(handler2)
    End Sub
End Class

' Violation: Hardcoded protocols in conditional logic
Public Class HardcodedSslProtocols5
    Public Sub ConditionalHardcodedProtocol(useNewProtocol As Boolean)
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        If useNewProtocol Then
            ' Violation: Hardcoded TLS 1.3
            handler.SslProtocols = SslProtocols.Tls13
        Else
            ' Violation: Hardcoded TLS 1.2
            handler.SslProtocols = SslProtocols.Tls12
        End If
    End Sub
End Class

' Violation: Hardcoded protocols with bitwise operations
Public Class HardcodedSslProtocols6
    Public Sub CombineHardcodedProtocols()
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        ' Violation: Combining hardcoded protocol values
        handler.SslProtocols = SslProtocols.Tls12 Or SslProtocols.Tls13
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
End Class

' Violation: Hardcoded protocols in method parameters
Public Class HardcodedSslProtocols7
    Public Sub ConfigureClient(handler As System.Net.Http.HttpClientHandler)
        ' Violation: Hardcoded protocol assignment in method
        handler.SslProtocols = SslProtocols.Tls12
    End Sub
    
    Public Sub UseConfiguredClient()
        Dim handler As New System.Net.Http.HttpClientHandler()
        ConfigureClient(handler)
    End Sub
End Class

' Violation: Hardcoded protocols in property assignment
Public Class HardcodedSslProtocols8
    Private _handler As System.Net.Http.HttpClientHandler
    
    Public Property ClientHandler As System.Net.Http.HttpClientHandler
        Get
            Return _handler
        End Get
        Set(value As System.Net.Http.HttpClientHandler)
            _handler = value
            ' Violation: Hardcoded protocol in property setter
            _handler.SslProtocols = SslProtocols.Tls13
        End Set
    End Property
End Class

' Violation: Hardcoded protocols in static context
Public Class HardcodedSslProtocols9
    Public Shared Sub SetGlobalProtocol()
        ' Violation: Hardcoded protocol in static method
        System.Net.ServicePointManager.SecurityProtocol = CType(3072, System.Net.SecurityProtocolType)
    End Sub
    
    Shared Sub New()
        ' Violation: Hardcoded protocol in static constructor
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
    End Sub
End Class

' Violation: Hardcoded protocols with different casting approaches
Public Class HardcodedSslProtocols10
    Public Sub UseDifferentCastingMethods()
        Dim handler1 As New System.Net.Http.HttpClientHandler()
        Dim handler2 As New System.Net.Http.HttpClientHandler()
        Dim handler3 As New System.Net.Http.HttpClientHandler()
        
        ' Violation: CType with decimal value
        handler1.SslProtocols = CType(12288, SslProtocols)
        
        ' Violation: CType with hexadecimal value
        handler2.SslProtocols = CType(&H3000, SslProtocols)
        
        ' Violation: Direct cast
        handler3.SslProtocols = DirectCast(3072, SslProtocols)
    End Sub
End Class

' Violation: Hardcoded protocols in exception handling
Public Class HardcodedSslProtocols11
    Public Sub HandleProtocolErrors()
        Try
            Dim handler As New System.Net.Http.HttpClientHandler()
            ' Some configuration that might fail
            handler.CheckCertificateRevocationList = True
        Catch ex As Exception
            ' Violation: Hardcoded fallback protocol
            Dim fallbackHandler As New System.Net.Http.HttpClientHandler()
            fallbackHandler.SslProtocols = SslProtocols.Tls12
        End Try
    End Sub
End Class

' Violation: Hardcoded protocols in loop
Public Class HardcodedSslProtocols12
    Public Sub CreateMultipleClientsWithHardcodedProtocols()
        Dim clients As New List(Of System.Net.Http.HttpClient)()
        
        For i As Integer = 0 To 4
            Dim handler As New System.Net.Http.HttpClientHandler()
            
            ' Violation: Hardcoded protocol in loop
            handler.SslProtocols = SslProtocols.Tls13
            
            clients.Add(New System.Net.Http.HttpClient(handler))
        Next
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class ConfigurableSslProtocols
    Public Sub UseConfigurableProtocol()
        ' Good: Protocol from configuration
        Dim protocolConfig As String = System.Configuration.ConfigurationManager.AppSettings("SslProtocol")
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        If Not String.IsNullOrEmpty(protocolConfig) Then
            ' Parse and set protocol based on configuration
            If protocolConfig = "Tls12" Then
                handler.SslProtocols = SslProtocols.Tls12
            ElseIf protocolConfig = "Tls13" Then
                handler.SslProtocols = SslProtocols.Tls13
            End If
        End If
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Sub UseEnvironmentVariable()
        ' Good: Protocol from environment variable
        Dim envProtocol As String = Environment.GetEnvironmentVariable("SSL_PROTOCOL")
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        If Not String.IsNullOrEmpty(envProtocol) Then
            ' Set protocol based on environment
            Console.WriteLine($"Using SSL protocol from environment: {envProtocol}")
        End If
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Sub UseSystemDefault()
        ' Good: Using system default (no hardcoding)
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.None ' Uses system default
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Sub DynamicProtocolSelection()
        ' Good: Dynamic protocol selection based on runtime conditions
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        ' Dynamic selection based on framework version
        If Environment.Version.Major >= 4 Then
            ' Use appropriate protocol for framework version
            Console.WriteLine("Using framework-appropriate protocol")
        End If
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Function CreateClientWithProtocol(protocol As SslProtocols) As System.Net.Http.HttpClient
        ' Good: Method that accepts protocol as parameter
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = protocol
        
        Return New System.Net.Http.HttpClient(handler)
    End Function
    
    Public Sub NonSslMethod()
        ' Good: Method that doesn't deal with SSL protocols
        Dim data As String = "Processing non-SSL related data"
        Console.WriteLine(data)
    End Sub
End Class
