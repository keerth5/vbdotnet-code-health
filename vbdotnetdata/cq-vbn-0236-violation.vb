' VB.NET test file for cq-vbn-0236: Do not use digital signature algorithm (DSA)
' This rule detects usage of DSA cryptographic algorithm which is considered weak

Imports System
Imports System.Security.Cryptography

' Violation: Using DSACryptoServiceProvider
Public Class DSAUsage1
    Public Sub CreateDSAProvider()
        ' Violation: Creating DSA crypto service provider
        Dim dsa As New DSACryptoServiceProvider()
        
        ' Generate key pair
        Dim publicKey As String = dsa.ToXmlString(False)
        Dim privateKey As String = dsa.ToXmlString(True)
        
        dsa.Dispose()
    End Sub
End Class

' Violation: Using DSACryptoServiceProvider with key size
Public Class DSAUsage2
    Public Sub CreateDSAWithKeySize()
        ' Violation: Creating DSA with specific key size
        Dim dsa As New DSACryptoServiceProvider(1024)
        
        Dim data As Byte() = Text.Encoding.UTF8.GetBytes("Hello World")
        Dim signature As Byte() = dsa.SignData(data)
        
        dsa.Clear()
    End Sub
End Class

' Violation: Using DSA.Create method
Public Class DSAUsage3
    Public Sub CreateDSAInstance()
        ' Violation: Using DSA.Create static method
        Using dsa As DSA = DSA.Create()
            Dim parameters As DSAParameters = dsa.ExportParameters(True)
            Console.WriteLine("DSA key generated")
        End Using
    End Sub
End Class

' Violation: Using DSA.Create with algorithm name
Public Class DSAUsage4
    Public Sub CreateDSAByName()
        ' Violation: Creating DSA by algorithm name
        Using dsa As DSA = DSA.Create("DSA")
            dsa.KeySize = 1024
            Dim publicKey As String = dsa.ToXmlString(False)
        End Using
    End Sub
End Class

' Violation: Using DSA in signing operation
Public Class DSAUsage5
    Public Sub SignDataWithDSA()
        ' Violation: Using DSA for signing
        Dim dsa As New DSACryptoServiceProvider(2048)
        
        Dim message As Byte() = Text.Encoding.UTF8.GetBytes("Important message")
        Dim hash As Byte() = SHA1.Create().ComputeHash(message)
        
        Dim signature As Byte() = dsa.SignHash(hash, "SHA1")
        
        dsa.Dispose()
    End Sub
End Class

' Violation: Using DSA for verification
Public Class DSAUsage6
    Public Sub VerifySignatureWithDSA()
        Dim publicKeyXml As String = "<DSAKeyValue>...</DSAKeyValue>"
        
        ' Violation: Creating DSA for verification
        Dim dsa As New DSACryptoServiceProvider()
        dsa.FromXmlString(publicKeyXml)
        
        Dim data As Byte() = Text.Encoding.UTF8.GetBytes("Message to verify")
        Dim signature As Byte() = {1, 2, 3, 4, 5}
        
        Dim isValid As Boolean = dsa.VerifyData(data, signature)
        
        dsa.Clear()
    End Sub
End Class

' Violation: Using DSA in key exchange
Public Class DSAUsage7
    Public Sub KeyExchangeWithDSA()
        ' Violation: Creating DSA instances for key exchange
        Dim dsa1 As New DSACryptoServiceProvider()
        Dim dsa2 As New DSACryptoServiceProvider()
        
        ' Export public keys
        Dim publicKey1 As String = dsa1.ToXmlString(False)
        Dim publicKey2 As String = dsa2.ToXmlString(False)
        
        ' Simulate key exchange
        Console.WriteLine("Key exchange using DSA")
        
        dsa1.Dispose()
        dsa2.Dispose()
    End Sub
End Class

' Violation: Using DSA in a generic method
Public Class DSAUsage8
    Public Function CreateAsymmetricAlgorithm(Of T As AsymmetricAlgorithm)() As T
        ' This would be called with DSA type
        Return CType(Activator.CreateInstance(GetType(T)), T)
    End Function
    
    Public Sub UseDSAGeneric()
        ' Violation: Using DSA through generic method
        Dim dsa As DSACryptoServiceProvider = CreateAsymmetricAlgorithm(Of DSACryptoServiceProvider)()
        dsa.KeySize = 1024
        dsa.Dispose()
    End Sub
End Class

' Violation: Using DSA with different constructors
Public Class DSAUsage9
    Public Sub CreateDSAWithParameters()
        Dim cspParams As New CspParameters(13) ' DSA provider type
        
        ' Violation: Creating DSA with CSP parameters
        Dim dsa As New DSACryptoServiceProvider(cspParams)
        
        Dim keyInfo As String = dsa.KeyExchangeAlgorithm
        Console.WriteLine($"Key exchange algorithm: {keyInfo}")
        
        dsa.Dispose()
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureAlgorithms
    Public Sub UseRSAInstead()
        ' Good: Using RSA instead of DSA
        Using rsa As New RSACryptoServiceProvider(2048)
            Dim data As Byte() = Text.Encoding.UTF8.GetBytes("Secure message")
            Dim signature As Byte() = rsa.SignData(data, "SHA256")
            Console.WriteLine("RSA signature created")
        End Using
    End Sub
    
    Public Sub UseECDSAInstead()
        ' Good: Using ECDSA instead of DSA
        Using ecdsa As ECDsa = ECDsa.Create()
            Dim data As Byte() = Text.Encoding.UTF8.GetBytes("Secure message")
            Dim signature As Byte() = ecdsa.SignData(data)
            Console.WriteLine("ECDSA signature created")
        End Using
    End Sub
    
    Public Sub UseAESForSymmetric()
        ' Good: Using AES for symmetric encryption
        Using aes As Aes = Aes.Create()
            aes.KeySize = 256
            Console.WriteLine("AES algorithm initialized")
        End Using
    End Sub
End Class
