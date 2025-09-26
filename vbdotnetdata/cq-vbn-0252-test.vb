' Test file for cq-vbn-0252: Do not hard-code certificate
' Rule should detect: X509Certificate constructors with hardcoded data

Imports System.Security.Cryptography.X509Certificates

Public Class CertificateTest
    
    ' VIOLATION: X509Certificate2 with hardcoded string
    Public Function BadCertificate1() As X509Certificate2
        Return New X509Certificate2("MIIBkTCB+wIJANIxQII6xSDqMA0GCSqGSIb3DQEBCwUAMBYxFDASBgNVBAoMC1Rlc3QgQ29tcGFueTAeFw0yMTA2MTYxMzE2MzRaFw0yMjA2MTYxMzE2MzRaMBYxFDASBgNVBAoMC1Rlc3QgQ29tcGFueTCBnzANBgkqhkiG9w0BAQEFAAOBjQAwgYkCgYEAuV")
    End Function
    
    ' VIOLATION: X509Certificate with hardcoded string
    Public Function BadCertificate2() As X509Certificate
        Return New X509Certificate("MIIC2jCCAcKgAwIBAgIJALFKnBuEd6AkMA0GCSqGSIb3DQEBCwUAMC4xCzAJBgNVBAYTAlVTMQswCQYDVQQIDAJDQTELMAkGA1UEBwwCU0YxCzAJBgNVBAoMAlNGMB4XDTE5MDMxMzE3NDYzM1oXDTE5MDQxMjE3NDYzM1owLjELMAkGA1UEBhMCVVMxCzAJBgNVBAgMAkNBMQswCQYDVQQHDAJTRjELMAkGA1UECgwCU0YwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCuV")
    End Function
    
    ' VIOLATION: X509Certificate2 with hardcoded byte array
    Public Function BadCertificate3() As X509Certificate2
        Dim certData As Byte() = {48, 130, 1, 145, 48, 130, 1, 59, 2, 9, 0, 210, 49, 64, 130, 58, 197, 32, 234, 48, 13}
        Return New X509Certificate2(certData)
    End Function
    
    ' VIOLATION: X509Certificate with hardcoded inline byte array
    Public Function BadCertificate4() As X509Certificate
        Return New X509Certificate({48, 130, 2, 218, 48, 130, 1, 194, 160, 3, 2, 1, 2, 2, 9, 0, 177, 74, 156, 27, 132, 119, 160, 36, 48, 13})
    End Function
    
    ' GOOD: Certificate from file - should NOT be flagged
    Public Function GoodCertificate1() As X509Certificate2
        Return New X509Certificate2("certificate.pfx", "password")
    End Function
    
    ' GOOD: Certificate from variable - should NOT be flagged
    Public Function GoodCertificate2(certPath As String) As X509Certificate2
        Return New X509Certificate2(certPath)
    End Function
    
    ' GOOD: Certificate from stream - should NOT be flagged
    Public Function GoodCertificate3(certData As Byte()) As X509Certificate2
        Return New X509Certificate2(certData)
    End Function
    
    ' GOOD: Normal class usage - should NOT be flagged
    Public Function NormalMethod() As String
        Return "This is not certificate related"
    End Function
    
End Class
