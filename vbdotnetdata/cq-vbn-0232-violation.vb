' VB.NET test file for cq-vbn-0232: Do not add certificates to root store
' This rule detects code that adds certificates to the Trusted Root Certification Authorities store

Imports System
Imports System.Security.Cryptography.X509Certificates

' Violation: Adding certificate to root store using StoreName.Root
Public Class RootStoreViolation1
    Public Sub AddCertificateToRootStore()
        Dim cert As New X509Certificate2("certificate.cer")
        
        ' Violation: Opening root store and adding certificate
        Dim store As New X509Store(StoreName.Root, StoreLocation.LocalMachine)
        store.Open(OpenFlags.ReadWrite)
        store.Add(cert)
        store.Close()
    End Sub
End Class

' Violation: Adding certificate to root store using string "Root"
Public Class RootStoreViolation2
    Public Sub AddCertToRootStoreByName()
        Dim certificate As New X509Certificate2()
        
        ' Violation: Using "Root" string to specify root store
        Dim rootStore As New X509Store("Root", StoreLocation.CurrentUser)
        rootStore.Open(OpenFlags.ReadWrite)
        rootStore.Add(certificate)
        rootStore.Close()
    End Sub
End Class

' Violation: Multiple certificates added to root store
Public Class RootStoreViolation3
    Public Sub AddMultipleCertificates()
        Dim cert1 As New X509Certificate2("cert1.pfx", "password")
        Dim cert2 As New X509Certificate2("cert2.pfx", "password")
        
        ' Violation: Adding multiple certificates to root store
        Using store As New X509Store(StoreName.Root, StoreLocation.LocalMachine)
            store.Open(OpenFlags.ReadWrite)
            store.Add(cert1)
            store.Add(cert2)
        End Using
    End Sub
End Class

' Violation: Adding certificate in different method structure
Public Class RootStoreViolation4
    Private rootCertStore As X509Store
    
    Public Sub InitializeStore()
        ' Violation: Initialize root store
        rootCertStore = New X509Store(StoreName.Root, StoreLocation.CurrentUser)
        rootCertStore.Open(OpenFlags.ReadWrite)
    End Sub
    
    Public Sub AddTrustedCertificate(cert As X509Certificate2)
        ' Violation: Add certificate to root store
        rootCertStore.Add(cert)
    End Sub
End Class

' Violation: Using different constructor overloads
Public Class RootStoreViolation5
    Public Sub AddCertificateWithDifferentConstructor()
        Dim certData As Byte() = IO.File.ReadAllBytes("certificate.cer")
        Dim cert As New X509Certificate2(certData)
        
        ' Violation: Another way to create root store
        Dim store As New X509Store("Root")
        store.Open(OpenFlags.ReadWrite)
        store.Add(cert)
        store.Close()
    End Sub
End Class

' Violation: Adding certificate in a loop
Public Class RootStoreViolation6
    Public Sub AddCertificatesInLoop()
        Dim certFiles As String() = {"cert1.cer", "cert2.cer", "cert3.cer"}
        
        ' Violation: Adding multiple certificates in loop
        Using store As New X509Store(StoreName.Root, StoreLocation.LocalMachine)
            store.Open(OpenFlags.ReadWrite)
            For Each certFile In certFiles
                Dim cert As New X509Certificate2(certFile)
                store.Add(cert)
            Next
        End Using
    End Sub
End Class

' Violation: Conditional certificate addition
Public Class RootStoreViolation7
    Public Sub ConditionalCertificateAdd(addToRoot As Boolean)
        Dim cert As New X509Certificate2("certificate.pfx", "password")
        
        If addToRoot Then
            ' Violation: Conditionally adding to root store
            Dim store As New X509Store(StoreName.Root, StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadWrite)
            store.Add(cert)
            store.Close()
        End If
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureCertificateHandling
    Public Sub AddCertificateToPersonalStore()
        Dim cert As New X509Certificate2("certificate.cer")
        
        ' Good: Adding to personal store instead of root
        Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
        store.Open(OpenFlags.ReadWrite)
        store.Add(cert)
        store.Close()
    End Sub
    
    Public Sub AddCertificateToTrustedPeopleStore()
        Dim cert As New X509Certificate2("certificate.cer")
        
        ' Good: Adding to trusted people store
        Dim store As New X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine)
        store.Open(OpenFlags.ReadWrite)
        store.Add(cert)
        store.Close()
    End Sub
    
    Public Sub ReadFromRootStore()
        ' Good: Only reading from root store, not adding
        Using store As New X509Store(StoreName.Root, StoreLocation.LocalMachine)
            store.Open(OpenFlags.ReadOnly)
            Dim certificates = store.Certificates
            ' Process certificates...
        End Using
    End Sub
End Class
