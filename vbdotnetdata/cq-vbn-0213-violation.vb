' Test file for cq-vbn-0213: Do not disable certificate validation
' This rule detects disabled certificate validation callbacks

Imports System
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class DisableCertificateValidationViolations
    
    ' Violation: ServerCertificateValidationCallback returning True
    Public Sub DisableCertificateValidationWithLambda()
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
    End Sub
    
    ' Violation: ServerCertificateValidationCallback with AddressOf
    Public Sub DisableCertificateValidationWithAddressOf()
        ServicePointManager.ServerCertificateValidationCallback = AddressOf AlwaysAcceptCertificate ' Violation
    End Sub
    
    Private Function AlwaysAcceptCertificate(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function
    
    ' Violation: Multiple certificate validation disabling
    Public Sub MultipleCertificateValidationDisabling()
        ServicePointManager.ServerCertificateValidationCallback = Function(s, c, ch, e) True ' Violation
        ServicePointManager.ServerCertificateValidationCallback = AddressOf AlwaysAcceptCertificate ' Violation
    End Sub
    
    ' Violation: Certificate validation disabling in loop
    Public Sub CertificateValidationDisablingInLoop()
        For i As Integer = 0 To 3
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
        Next
    End Sub
    
    ' Violation: Certificate validation disabling in conditional
    Public Sub ConditionalCertificateValidationDisabling(disable As Boolean)
        If disable Then
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
        End If
    End Sub
    
    ' Violation: Certificate validation disabling in Try-Catch
    Public Sub CertificateValidationDisablingInTryCatch()
        Try
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Certificate validation disabling with different parameter names
    Public Sub CertificateValidationWithDifferentParameters()
        ServicePointManager.ServerCertificateValidationCallback = Function(obj, cert, chain, errors) True ' Violation
    End Sub
    
    ' Violation: Certificate validation disabling with single parameter
    Public Sub CertificateValidationWithSingleParameter()
        ServicePointManager.ServerCertificateValidationCallback = Function(x) True ' Violation
    End Sub
    
    ' Violation: Certificate validation disabling with no parameters
    Public Sub CertificateValidationWithNoParameters()
        ServicePointManager.ServerCertificateValidationCallback = Function() True ' Violation
    End Sub
    
    ' Violation: Certificate validation disabling in method return context
    Public Function GetInsecureServicePointManager() As ServicePointManager
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
        Return ServicePointManager
    End Function
    
    ' Non-violation: Proper certificate validation (should not be detected)
    Public Sub ProperCertificateValidation()
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors)
                                                                       Return sslPolicyErrors = SslPolicyErrors.None ' No violation - proper validation
                                                                   End Function
    End Sub
    
    ' Non-violation: Certificate validation with actual logic (should not be detected)
    Public Sub CertificateValidationWithLogic()
        ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateCertificate ' No violation - proper validation method
    End Sub
    
    Private Function ValidateCertificate(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        ' Proper validation logic here
        If sslPolicyErrors = SslPolicyErrors.None Then
            Return True
        End If
        Return False
    End Function
    
    ' Non-violation: Comments and strings mentioning certificate validation
    Public Sub CommentsAndStrings()
        ' This is about ServerCertificateValidationCallback = Function() True
        Dim message As String = "Do not set ServerCertificateValidationCallback to always return True"
        Console.WriteLine("Certificate validation should not be disabled")
    End Sub
    
    ' Violation: Certificate validation disabling in Select Case
    Public Sub CertificateValidationDisablingInSelectCase(option As Integer)
        Select Case option
            Case 1
                ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
            Case 2
                ServicePointManager.ServerCertificateValidationCallback = AddressOf AlwaysAcceptCertificate ' Violation
        End Select
    End Sub
    
    ' Violation: Certificate validation disabling in Using statement
    Public Sub CertificateValidationDisablingInUsing()
        Using client As New System.Net.Http.HttpClient()
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
        End Using
    End Sub
    
    ' Violation: Certificate validation disabling in While loop
    Public Sub CertificateValidationDisablingInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: Certificate validation disabling with variable assignment
    Public Sub CertificateValidationWithVariableAssignment()
        Dim callback As RemoteCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
        ServicePointManager.ServerCertificateValidationCallback = callback
    End Sub
    
    ' Violation: Certificate validation disabling with method reference
    Public Sub CertificateValidationWithMethodReference()
        ServicePointManager.ServerCertificateValidationCallback = AddressOf TrustAllCertificates ' Violation
    End Sub
    
    Private Function TrustAllCertificates(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function
    
    ' Violation: Certificate validation disabling with inline function
    Public Sub CertificateValidationWithInlineFunction()
        ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, c As X509Certificate, ch As X509Chain, e As SslPolicyErrors) True ' Violation
    End Sub
    
    ' Violation: Certificate validation disabling in class-level field
    Private Shared certificateCallback As RemoteCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
    
    Public Sub UseCertificateCallback()
        ServicePointManager.ServerCertificateValidationCallback = certificateCallback
    End Sub
    
    ' Violation: Certificate validation disabling with property
    Public Property CertificateValidationCallback As RemoteCertificateValidationCallback
        Get
            Return Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
        End Get
        Set(value As RemoteCertificateValidationCallback)
            ServicePointManager.ServerCertificateValidationCallback = value
        End Set
    End Property
    
    ' Violation: Certificate validation disabling in constructor
    Public Sub New()
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
    End Sub
    
    ' Violation: Certificate validation disabling with different ServicePointManager instances
    Public Sub CertificateValidationWithDifferentInstances()
        Dim sp1 = ServicePointManager.FindServicePoint("https://example1.com")
        Dim sp2 = ServicePointManager.FindServicePoint("https://example2.com")
        
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True ' Violation
    End Sub

End Class
