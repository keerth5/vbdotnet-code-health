' VB.NET test file for cq-vbn-0237: Use RSA algorithm with sufficient key size
' This rule detects RSA usage with insufficient key sizes (less than 2048 bits)

Imports System
Imports System.Security.Cryptography

' Violation: Using RSA with 512-bit key
Public Class WeakRSA1
    Public Sub CreateRSA512()
        ' Violation: 512-bit key is too weak
        Dim rsa As New RSACryptoServiceProvider(512)
        
        Dim data As Byte() = Text.Encoding.UTF8.GetBytes("Test message")
        Dim encrypted As Byte() = rsa.Encrypt(data, False)
        
        rsa.Dispose()
    End Sub
End Class

' Violation: Using RSA with 768-bit key
Public Class WeakRSA2
    Public Sub CreateRSA768()
        ' Violation: 768-bit key is insufficient
        Using rsa As New RSACryptoServiceProvider(768)
            Dim publicKey As String = rsa.ToXmlString(False)
            Console.WriteLine("Generated weak RSA key")
        End Using
    End Sub
End Class

' Violation: Using RSA with 1024-bit key
Public Class WeakRSA3
    Public Sub CreateRSA1024()
        ' Violation: 1024-bit key is considered weak
        Dim rsa As New RSACryptoServiceProvider(1024)
        
        Dim message As Byte() = Text.Encoding.UTF8.GetBytes("Important data")
        Dim signature As Byte() = rsa.SignData(message, "SHA256")
        
        rsa.Clear()
    End Sub
End Class

' Violation: Using RSA.Create with weak key size
Public Class WeakRSA4
    Public Sub CreateRSAWithWeakSize()
        ' Violation: RSA.Create with 512-bit key
        Using rsa As RSA = RSA.Create(512)
            rsa.ImportParameters(New RSAParameters())
        End Using
    End Sub
End Class

' Violation: Using RSA.Create with 768-bit key
Public Class WeakRSA5
    Public Sub CreateRSA768Static()
        ' Violation: RSA.Create with 768-bit key
        Dim rsa As RSA = RSA.Create(768)
        
        Dim keySize As Integer = rsa.KeySize
        Console.WriteLine($"RSA key size: {keySize}")
        
        rsa.Dispose()
    End Sub
End Class

' Violation: Using RSA.Create with 1024-bit key
Public Class WeakRSA6
    Public Sub CreateRSA1024Static()
        ' Violation: RSA.Create with 1024-bit key
        Using rsa As RSA = RSA.Create(1024)
            Dim parameters As RSAParameters = rsa.ExportParameters(False)
        End Using
    End Sub
End Class

' Violation: Setting KeySize property to weak value
Public Class WeakRSA7
    Public Sub SetWeakKeySize()
        Using rsa As New RSACryptoServiceProvider()
            ' Violation: Setting KeySize to weak value
            rsa.KeySize = 512
            
            Dim data As Byte() = {1, 2, 3, 4, 5}
            Dim encrypted As Byte() = rsa.Encrypt(data, True)
        End Using
    End Sub
End Class

' Violation: Multiple weak RSA instances
Public Class WeakRSA8
    Public Sub CreateMultipleWeakRSA()
        ' Violation: First weak RSA instance
        Dim rsa1 As New RSACryptoServiceProvider(768)
        
        ' Violation: Second weak RSA instance
        Dim rsa2 As New RSACryptoServiceProvider(1024)
        
        ' Use both instances
        Dim data As Byte() = Text.Encoding.UTF8.GetBytes("Test")
        Dim encrypted1 As Byte() = rsa1.Encrypt(data, False)
        Dim encrypted2 As Byte() = rsa2.Encrypt(data, False)
        
        rsa1.Dispose()
        rsa2.Dispose()
    End Sub
End Class

' Violation: Weak RSA in conditional logic
Public Class WeakRSA9
    Public Sub ConditionalWeakRSA(useWeak As Boolean)
        If useWeak Then
            ' Violation: Conditional weak RSA creation
            Using rsa As New RSACryptoServiceProvider(512)
                Dim publicKey As String = rsa.ToXmlString(False)
            End Using
        End If
    End Sub
End Class

' Violation: Weak RSA in method parameter
Public Class WeakRSA10
    Public Sub ProcessData()
        ' Violation: Inline weak RSA creation
        ProcessWithRSA(New RSACryptoServiceProvider(1024))
    End Sub
    
    Private Sub ProcessWithRSA(rsa As RSACryptoServiceProvider)
        Dim data As Byte() = {1, 2, 3}
        Dim result As Byte() = rsa.Encrypt(data, True)
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureRSA
    Public Sub CreateSecureRSA2048()
        ' Good: 2048-bit key is secure
        Using rsa As New RSACryptoServiceProvider(2048)
            Dim data As Byte() = Text.Encoding.UTF8.GetBytes("Secure message")
            Dim encrypted As Byte() = rsa.Encrypt(data, True)
        End Using
    End Sub
    
    Public Sub CreateSecureRSA3072()
        ' Good: 3072-bit key is very secure
        Dim rsa As New RSACryptoServiceProvider(3072)
        
        Dim message As Byte() = Text.Encoding.UTF8.GetBytes("Important data")
        Dim signature As Byte() = rsa.SignData(message, "SHA256")
        
        rsa.Dispose()
    End Sub
    
    Public Sub CreateRSA4096()
        ' Good: 4096-bit key is highly secure
        Using rsa As RSA = RSA.Create(4096)
            Dim parameters As RSAParameters = rsa.ExportParameters(False)
        End Using
    End Sub
    
    Public Sub UseDefaultRSASize()
        ' Good: Default RSA size (usually 2048 or higher)
        Using rsa As New RSACryptoServiceProvider()
            Console.WriteLine($"Default key size: {rsa.KeySize}")
        End Using
    End Sub
End Class
