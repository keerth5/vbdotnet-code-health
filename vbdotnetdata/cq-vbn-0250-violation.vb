' VB.NET test file for cq-vbn-0250: Ensure HttpClient certificate revocation list check is not disabled
' This rule detects when certificate revocation list checking is disabled

Imports System
Imports System.Net.Http

' Violation: CheckCertificateRevocationList set to False
Public Class DisabledRevocationCheck1
    Public Sub CreateClientWithDisabledRevocationCheck()
        ' Violation: Explicitly disabling certificate revocation list check
        Dim handler As New HttpClientHandler()
        handler.CheckCertificateRevocationList = False
        
        Dim client As New HttpClient(handler)
        ' Use client for HTTPS requests...
    End Sub
End Class

' Violation: HttpClientHandler with object initializer setting CheckCertificateRevocationList to False
Public Class DisabledRevocationCheck2
    Public Sub CreateClientWithInitializer()
        ' Violation: Object initializer with CheckCertificateRevocationList = False
        Dim handler As New HttpClientHandler() With {
            .CheckCertificateRevocationList = False,
            .UseCookies = True
        }
        
        Dim client As New HttpClient(handler)
    End Sub
End Class

' Violation: Multiple handlers with disabled revocation check
Public Class DisabledRevocationCheck3
    Public Sub CreateMultipleClientsWithDisabledCheck()
        ' Violation: First handler with disabled check
        Dim handler1 As New HttpClientHandler()
        handler1.CheckCertificateRevocationList = False
        handler1.UseProxy = False
        
        ' Violation: Second handler with disabled check
        Dim handler2 As New HttpClientHandler()
        handler2.CheckCertificateRevocationList = False
        handler2.AllowAutoRedirect = True
        
        Dim client1 As New HttpClient(handler1)
        Dim client2 As New HttpClient(handler2)
    End Sub
End Class

' Violation: Conditional disabling of revocation check
Public Class DisabledRevocationCheck4
    Public Sub ConditionallyDisableRevocationCheck(disableCheck As Boolean)
        Dim handler As New HttpClientHandler()
        
        If disableCheck Then
            ' Violation: Conditionally disabling revocation check
            handler.CheckCertificateRevocationList = False
        Else
            handler.CheckCertificateRevocationList = True
        End If
        
        Dim client As New HttpClient(handler)
    End Sub
End Class

' Violation: Disabling revocation check in method parameter
Public Class DisabledRevocationCheck5
    Public Sub ConfigureHandler(handler As HttpClientHandler)
        ' Violation: Disabling revocation check in method
        handler.CheckCertificateRevocationList = False
        handler.ServerCertificateCustomValidationCallback = Nothing
    End Sub
    
    Public Sub UseConfiguredHandler()
        Dim handler As New HttpClientHandler()
        ConfigureHandler(handler)
        
        Dim client As New HttpClient(handler)
    End Sub
End Class

' Violation: Disabling revocation check in property setter
Public Class DisabledRevocationCheck6
    Private _handler As HttpClientHandler
    
    Public Property ClientHandler As HttpClientHandler
        Get
            Return _handler
        End Get
        Set(value As HttpClientHandler)
            _handler = value
            ' Violation: Disabling revocation check in property setter
            _handler.CheckCertificateRevocationList = False
        End Set
    End Property
End Class

' Violation: Disabling revocation check in static context
Public Class DisabledRevocationCheck7
    Public Shared Function CreateGlobalClient() As HttpClient
        ' Violation: Static method disabling revocation check
        Dim handler As New HttpClientHandler()
        handler.CheckCertificateRevocationList = False
        
        Return New HttpClient(handler)
    End Function
End Class

' Violation: Disabling revocation check in exception handling
Public Class DisabledRevocationCheck8
    Public Sub HandleCertificateErrors()
        Try
            Dim handler As New HttpClientHandler()
            handler.CheckCertificateRevocationList = True
            
            Dim client As New HttpClient(handler)
            ' Some operation that might fail due to certificate issues
        Catch ex As Exception
            ' Violation: Disabling revocation check on error
            Dim fallbackHandler As New HttpClientHandler()
            fallbackHandler.CheckCertificateRevocationList = False
            
            Dim fallbackClient As New HttpClient(fallbackHandler)
        End Try
    End Sub
End Class

' Violation: Disabling revocation check in loop
Public Class DisabledRevocationCheck9
    Public Sub CreateMultipleClientsInLoop()
        Dim clients As New List(Of HttpClient)()
        
        For i As Integer = 0 To 4
            ' Violation: Disabling revocation check in loop
            Dim handler As New HttpClientHandler()
            handler.CheckCertificateRevocationList = False
            
            clients.Add(New HttpClient(handler))
        Next
    End Sub
End Class

' Violation: Complex object initializer with disabled revocation check
Public Class DisabledRevocationCheck10
    Public Sub CreateComplexHandler()
        ' Violation: Complex initializer with CheckCertificateRevocationList = False
        Dim handler As New HttpClientHandler() With {
            .AllowAutoRedirect = True,
            .CheckCertificateRevocationList = False,
            .CookieContainer = New System.Net.CookieContainer(),
            .UseCookies = True,
            .UseProxy = False,
            .MaxAutomaticRedirections = 10
        }
        
        Dim client As New HttpClient(handler)
    End Sub
End Class

' Violation: Disabling revocation check with other security settings
Public Class DisabledRevocationCheck11
    Public Sub ConfigureSecuritySettings()
        Dim handler As New HttpClientHandler()
        
        ' Set other security settings
        handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12
        handler.ServerCertificateCustomValidationCallback = AddressOf ValidateCertificate
        
        ' Violation: Disable revocation check despite other security measures
        handler.CheckCertificateRevocationList = False
        
        Dim client As New HttpClient(handler)
    End Sub
    
    Private Function ValidateCertificate(sender As Object, certificate As System.Security.Cryptography.X509Certificates.X509Certificate, chain As System.Security.Cryptography.X509Certificates.X509Chain, sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function
End Class

' Violation: Disabling revocation check in async context
Public Class DisabledRevocationCheck12
    Public Async Function CreateAsyncClient() As Task(Of HttpClient)
        ' Violation: Disabling revocation check in async method
        Dim handler As New HttpClientHandler()
        handler.CheckCertificateRevocationList = False
        
        Await Task.Delay(100) ' Simulate async operation
        
        Return New HttpClient(handler)
    End Function
End Class

' Good examples (should not be detected as violations)
Public Class SecureRevocationCheck
    Public Sub CreateClientWithRevocationCheckEnabled()
        ' Good: Enabling certificate revocation list check
        Dim handler As New HttpClientHandler()
        handler.CheckCertificateRevocationList = True
        
        Dim client As New HttpClient(handler)
    End Sub
    
    Public Sub CreateClientWithSecureInitializer()
        ' Good: Object initializer with CheckCertificateRevocationList = True
        Dim handler As New HttpClientHandler() With {
            .CheckCertificateRevocationList = True,
            .UseCookies = True,
            .SslProtocols = System.Security.Authentication.SslProtocols.Tls12
        }
        
        Dim client As New HttpClient(handler)
    End Sub
    
    Public Sub CreateDefaultClient()
        ' Good: Using default HttpClient (default behavior may vary)
        Dim client As New HttpClient()
        ' Default configuration is used
    End Sub
    
    Public Sub CreateClientWithoutExplicitSetting()
        ' Good: Creating handler without explicitly setting CheckCertificateRevocationList
        Dim handler As New HttpClientHandler()
        handler.UseCookies = False
        handler.AllowAutoRedirect = True
        
        Dim client As New HttpClient(handler)
    End Sub
    
    Public Sub ConfigureSecureHandler()
        ' Good: Configuring handler with security in mind
        Dim handler As New HttpClientHandler()
        handler.CheckCertificateRevocationList = True
        handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12
        
        Dim client As New HttpClient(handler)
    End Sub
    
    Public Sub NonHttpClientMethod()
        ' Good: Method that doesn't use HttpClient
        Dim data As String = "Processing non-HTTP data"
        Console.WriteLine(data)
    End Sub
    
    Public Function CreateSecureClientFactory() As Func(Of HttpClient)
        ' Good: Factory method that creates secure clients
        Return Function()
                   Dim handler As New HttpClientHandler()
                   handler.CheckCertificateRevocationList = True
                   Return New HttpClient(handler)
               End Function
    End Function
End Class
