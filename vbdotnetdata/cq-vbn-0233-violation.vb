' VB.NET test file for cq-vbn-0233: Ensure certificates are not added to root store
' This rule detects code that potentially adds certificates to the Trusted Root Certification Authorities store

Imports System
Imports System.Security.Cryptography.X509Certificates

' Violation: Creating X509Store and adding certificate
Public Class CertificateStoreViolation1
    Public Sub AddCertificateToStore()
        Dim cert As New X509Certificate2("certificate.cer")
        
        ' Violation: Creating store and adding certificate
        Dim store As New X509Store()
        store.Open(OpenFlags.ReadWrite)
        store.Add(cert)
        store.Close()
    End Sub
End Class

' Violation: Opening store with ReadWrite and adding certificate
Public Class CertificateStoreViolation2
    Public Sub OpenStoreAndAddCert()
        Dim certificate As New X509Certificate2("mycert.pfx", "password")
        
        ' Violation: Opening with ReadWrite flags and adding
        Dim certStore As New X509Store(StoreName.My, StoreLocation.LocalMachine)
        certStore.Open(OpenFlags.ReadWrite)
        certStore.Add(certificate)
        certStore.Close()
    End Sub
End Class

' Violation: Using different certificate constructor
Public Class CertificateStoreViolation3
    Public Sub AddCertificateFromBytes()
        Dim certBytes As Byte() = IO.File.ReadAllBytes("certificate.cer")
        Dim cert As New X509Certificate2(certBytes)
        
        ' Violation: Adding certificate from byte array
        Using store As New X509Store(StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadWrite)
            store.Add(cert)
        End Using
    End Sub
End Class

' Violation: Adding certificate in method with parameters
Public Class CertificateStoreViolation4
    Public Sub AddCertificateToSpecificStore(storeName As StoreName, location As StoreLocation)
        Dim cert As New X509Certificate2("certificate.p12", "password")
        
        ' Violation: Adding certificate to parameterized store
        Dim store As New X509Store(storeName, location)
        store.Open(OpenFlags.ReadWrite)
        store.Add(cert)
        store.Close()
    End Sub
End Class

' Violation: Adding multiple certificates
Public Class CertificateStoreViolation5
    Public Sub AddMultipleCertificates()
        Dim certs As X509Certificate2() = {
            New X509Certificate2("cert1.cer"),
            New X509Certificate2("cert2.cer"),
            New X509Certificate2("cert3.cer")
        }
        
        ' Violation: Adding multiple certificates
        Using store As New X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine)
            store.Open(OpenFlags.ReadWrite)
            For Each cert In certs
                store.Add(cert)
            Next
        End Using
    End Sub
End Class

' Violation: Conditional certificate addition
Public Class CertificateStoreViolation6
    Public Sub ConditionalAdd(shouldAdd As Boolean, cert As X509Certificate2)
        If shouldAdd Then
            ' Violation: Conditional certificate addition
            Dim store As New X509Store()
            store.Open(OpenFlags.ReadWrite)
            store.Add(cert)
            store.Close()
        End If
    End Sub
End Class

' Violation: Adding certificate in try-catch block
Public Class CertificateStoreViolation7
    Public Sub AddCertificateWithErrorHandling()
        Try
            Dim cert As New X509Certificate2("certificate.pfx", "password")
            
            ' Violation: Adding certificate with error handling
            Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadWrite)
            store.Add(cert)
            store.Close()
        Catch ex As Exception
            Console.WriteLine("Error adding certificate: " & ex.Message)
        End Try
    End Sub
End Class

' Violation: Adding certificate through property
Public Class CertificateStoreViolation8
    Private _store As X509Store
    
    Public Property CertificateStore As X509Store
        Get
            Return _store
        End Get
        Set(value As X509Store)
            _store = value
        End Set
    End Property
    
    Public Sub AddCertificateViaProperty()
        Dim cert As New X509Certificate2("certificate.cer")
        
        ' Violation: Adding through property
        CertificateStore = New X509Store(StoreName.Root, StoreLocation.LocalMachine)
        CertificateStore.Open(OpenFlags.ReadWrite)
        CertificateStore.Add(cert)
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureCertificateOperations
    Public Sub ReadCertificatesOnly()
        ' Good: Only reading certificates, not adding
        Using store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadOnly)
            Dim certificates = store.Certificates
            For Each cert In certificates
                Console.WriteLine(cert.Subject)
            Next
        End Using
    End Sub
    
    Public Sub RemoveCertificate()
        Dim cert As New X509Certificate2("certificate.cer")
        
        ' Good: Removing certificate, not adding
        Using store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadWrite)
            store.Remove(cert)
        End Using
    End Sub
    
    Public Sub ValidateCertificate()
        Dim cert As New X509Certificate2("certificate.cer")
        
        ' Good: Just validating certificate
        Dim isValid As Boolean = cert.Verify()
        Console.WriteLine("Certificate valid: " & isValid)
    End Sub
End Class
