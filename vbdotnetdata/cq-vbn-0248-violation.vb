' VB.NET test file for cq-vbn-0248: Do not use deprecated SslProtocols values
' This rule detects usage of deprecated SSL/TLS protocol versions

Imports System
Imports System.Net.Security
Imports System.Security.Authentication

' Violation: Using SslProtocols.Ssl2
Public Class DeprecatedSslProtocols1
    Public Sub UseSSL2()
        ' Violation: SSL 2.0 is deprecated and insecure
        Dim sslStream As New SslStream(Nothing)
        sslStream.AuthenticateAsClient("example.com", Nothing, SslProtocols.Ssl2, False)
    End Sub
End Class

' Violation: Using SslProtocols.Ssl3
Public Class DeprecatedSslProtocols2
    Public Sub UseSSL3()
        ' Violation: SSL 3.0 is deprecated and insecure
        Dim client As New System.Net.Http.HttpClient()
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.Ssl3
    End Sub
End Class

' Violation: Using SslProtocols.Tls (TLS 1.0)
Public Class DeprecatedSslProtocols3
    Public Sub UseTLS10()
        ' Violation: TLS 1.0 is deprecated
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.Tls
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
End Class

' Violation: Using SslProtocols.Tls11 (TLS 1.1)
Public Class DeprecatedSslProtocols4
    Public Sub UseTLS11()
        ' Violation: TLS 1.1 is deprecated
        Dim request As System.Net.HttpWebRequest = CType(System.Net.WebRequest.Create("https://example.com"), System.Net.HttpWebRequest)
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls11
    End Sub
End Class

' Violation: Setting SslProtocols property to deprecated values
Public Class DeprecatedSslProtocols5
    Public Sub SetDeprecatedProtocols()
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        ' Violation: Setting SslProtocols to SSL2
        handler.SslProtocols = SslProtocols.Ssl2
        
        ' Some other configuration
        handler.UseCookies = False
    End Sub
End Class

' Violation: Multiple deprecated protocols
Public Class DeprecatedSslProtocols6
    Public Sub UseMultipleDeprecatedProtocols()
        Dim handler1 As New System.Net.Http.HttpClientHandler()
        Dim handler2 As New System.Net.Http.HttpClientHandler()
        Dim handler3 As New System.Net.Http.HttpClientHandler()
        
        ' Violation: SSL3
        handler1.SslProtocols = SslProtocols.Ssl3
        
        ' Violation: TLS 1.0
        handler2.SslProtocols = SslProtocols.Tls
        
        ' Violation: TLS 1.1
        handler3.SslProtocols = SslProtocols.Tls11
    End Sub
End Class

' Violation: Deprecated protocols in conditional logic
Public Class DeprecatedSslProtocols7
    Public Sub ConditionalDeprecatedProtocol(useLegacy As Boolean)
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        If useLegacy Then
            ' Violation: Conditional use of SSL3
            handler.SslProtocols = SslProtocols.Ssl3
        Else
            handler.SslProtocols = SslProtocols.Tls12
        End If
    End Sub
End Class

' Violation: Deprecated protocols in method parameters
Public Class DeprecatedSslProtocols8
    Public Sub ConfigureClient(protocol As SslProtocols)
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = protocol
    End Sub
    
    Public Sub UseDeprecatedInMethod()
        ' Violation: Passing deprecated protocol to method
        ConfigureClient(SslProtocols.Ssl2)
        ConfigureClient(SslProtocols.Tls)
    End Sub
End Class

' Violation: Deprecated protocols with bitwise operations
Public Class DeprecatedSslProtocols9
    Public Sub UseBitwiseDeprecatedProtocols()
        Dim handler As New System.Net.Http.HttpClientHandler()
        
        ' Violation: Combining deprecated protocols
        handler.SslProtocols = SslProtocols.Ssl3 Or SslProtocols.Tls
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
End Class

' Violation: Deprecated protocols in static context
Public Class DeprecatedSslProtocols10
    Shared Sub New()
        ' Violation: Setting deprecated protocol in static constructor
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
    End Sub
    
    Public Shared Sub SetGlobalDeprecatedProtocol()
        ' Violation: Setting deprecated protocol globally
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls
    End Sub
End Class

' Violation: Deprecated protocols in property assignment
Public Class DeprecatedSslProtocols11
    Private _handler As System.Net.Http.HttpClientHandler
    
    Public Property ClientHandler As System.Net.Http.HttpClientHandler
        Get
            Return _handler
        End Get
        Set(value As System.Net.Http.HttpClientHandler)
            _handler = value
            ' Violation: Setting deprecated protocol in property setter
            _handler.SslProtocols = SslProtocols.Tls11
        End Set
    End Property
End Class

' Violation: Deprecated protocols in exception handling
Public Class DeprecatedSslProtocols12
    Public Sub HandleDeprecatedProtocolErrors()
        Try
            Dim handler As New System.Net.Http.HttpClientHandler()
            handler.SslProtocols = SslProtocols.Tls12
            
            ' Some operation that might fail
            Dim client As New System.Net.Http.HttpClient(handler)
        Catch ex As Exception
            ' Violation: Fallback to deprecated protocol on error
            Dim fallbackHandler As New System.Net.Http.HttpClientHandler()
            fallbackHandler.SslProtocols = SslProtocols.Ssl3
        End Try
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureSslProtocols
    Public Sub UseTLS12()
        ' Good: Using TLS 1.2 (secure)
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.Tls12
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Sub UseTLS13()
        ' Good: Using TLS 1.3 (secure)
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.Tls13
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Sub UseSecureProtocolsCombined()
        ' Good: Using secure protocols combined
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.Tls12 Or SslProtocols.Tls13
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Sub UseSystemDefault()
        ' Good: Using system default (lets OS choose secure protocol)
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.None ' Uses system default
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
    
    Public Sub NonSslMethod()
        ' Good: Method that doesn't deal with SSL protocols
        Dim data As String = "Some processing"
        Console.WriteLine(data)
    End Sub
    
    Public Sub ConfigureSecureClient()
        ' Good: Configuring client with secure settings
        Dim handler As New System.Net.Http.HttpClientHandler()
        handler.SslProtocols = SslProtocols.Tls12
        handler.CheckCertificateRevocationList = True
        
        Dim client As New System.Net.Http.HttpClient(handler)
    End Sub
End Class
