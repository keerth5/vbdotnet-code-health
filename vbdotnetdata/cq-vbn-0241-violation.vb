' VB.NET test file for cq-vbn-0241: Do not hard-code encryption key
' This rule detects hard-coded encryption keys in cryptographic operations

Imports System
Imports System.Security.Cryptography
Imports System.Text

' Violation: Hard-coded key using Encoding.GetBytes
Public Class HardCodedKeys1
    Public Sub EncryptWithHardCodedKey()
        Using aes As Aes = Aes.Create()
            ' Violation: Hard-coded key using Encoding.UTF8.GetBytes
            aes.Key = Encoding.UTF8.GetBytes("MySecretKey12345")
            aes.IV = New Byte(15) {}
            
            Dim plaintext As String = "Hello World"
            ' Encryption logic here...
        End Using
    End Sub
End Class

' Violation: Hard-coded key using byte array literal
Public Class HardCodedKeys2
    Public Sub SetKeyWithByteArray()
        Using des As DES = DES.Create()
            ' Violation: Hard-coded key using byte array literal
            des.Key = {&H1, &H2, &H3, &H4, &H5, &H6, &H7, &H8}
            des.IV = New Byte(7) {}
            
            ' Encryption operations...
        End Using
    End Sub
End Class

' Violation: Hard-coded key using ASCII encoding
Public Class HardCodedKeys3
    Public Sub UseASCIIEncodedKey()
        Using aes As New AesCryptoServiceProvider()
            ' Violation: Hard-coded key using Encoding.ASCII.GetBytes
            aes.Key = Encoding.ASCII.GetBytes("MyPassword123456")
            aes.Mode = CipherMode.CBC
            
            ' Encrypt data...
        End Using
    End Sub
End Class

' Violation: Hard-coded key with variable assignment
Public Class HardCodedKeys4
    Public Sub AssignHardCodedKey()
        ' Violation: Hard-coded key in byte array variable
        Dim secretKey As Byte() = {&H10, &H20, &H30, &H40, &H50, &H60, &H70, &H80, &H90, &HA0, &HB0, &HC0, &HD0, &HE0, &HF0, &H11}
        
        Using aes As Aes = Aes.Create()
            aes.Key = secretKey
            ' Encryption logic...
        End Using
    End Sub
End Class

' Violation: Hard-coded key using Unicode encoding
Public Class HardCodedKeys5
    Public Sub UseUnicodeEncodedKey()
        Using rijndael As New RijndaelManaged()
            ' Violation: Hard-coded key using Encoding.Unicode.GetBytes
            rijndael.Key = Encoding.Unicode.GetBytes("HardCodedSecret!")
            rijndael.BlockSize = 128
            
            ' Encryption operations...
        End Using
    End Sub
End Class

' Violation: Hard-coded key in method parameter
Public Class HardCodedKeys6
    Public Sub EncryptData(data As String)
        ' Violation: Hard-coded key using Encoding.UTF8.GetBytes
        Dim key As Byte() = Encoding.UTF8.GetBytes("StaticEncryptionKey123")
        
        Using aes As Aes = Aes.Create()
            aes.Key = key
            ' Process data...
        End Using
    End Sub
End Class

' Violation: Hard-coded key with different encoding
Public Class HardCodedKeys7
    Public Sub UseBase64EncodedKey()
        Using aes As Aes = Aes.Create()
            ' Violation: Hard-coded key (even though it's base64, it's still hard-coded)
            aes.Key = Encoding.UTF8.GetBytes("SGFyZENvZGVkS2V5")
            aes.GenerateIV()
            
            ' Encryption logic...
        End Using
    End Sub
End Class

' Violation: Multiple hard-coded keys
Public Class HardCodedKeys8
    Public Sub UseMultipleHardCodedKeys()
        ' Violation: First hard-coded key
        Dim key1 As Byte() = Encoding.UTF8.GetBytes("FirstHardCodedKey")
        
        ' Violation: Second hard-coded key
        Dim key2 As Byte() = {&H01, &H02, &H03, &H04, &H05, &H06, &H07, &H08, &H09, &H0A, &H0B, &H0C, &H0D, &H0E, &H0F, &H10}
        
        Using aes1 As Aes = Aes.Create()
            aes1.Key = key1
        End Using
        
        Using aes2 As Aes = Aes.Create()
            aes2.Key = key2
        End Using
    End Sub
End Class

' Violation: Hard-coded key in property assignment
Public Class HardCodedKeys9
    Private _algorithm As SymmetricAlgorithm
    
    Public Property Algorithm As SymmetricAlgorithm
        Get
            Return _algorithm
        End Get
        Set(value As SymmetricAlgorithm)
            _algorithm = value
            ' Violation: Hard-coded key assignment in property
            _algorithm.Key = Encoding.UTF8.GetBytes("PropertyHardCodedKey")
        End Set
    End Property
End Class

' Good examples (should not be detected as violations)
Public Class SecureKeyHandling
    Public Sub GenerateRandomKey()
        Using aes As Aes = Aes.Create()
            ' Good: Generate random key
            aes.GenerateKey()
            aes.GenerateIV()
            
            ' Encryption logic...
        End Using
    End Sub
    
    Public Sub UseKeyFromConfiguration()
        ' Good: Key from configuration or external source
        Dim keyFromConfig As String = System.Configuration.ConfigurationManager.AppSettings("EncryptionKey")
        
        Using aes As Aes = Aes.Create()
            If Not String.IsNullOrEmpty(keyFromConfig) Then
                aes.Key = Convert.FromBase64String(keyFromConfig)
            End If
        End Using
    End Sub
    
    Public Sub UseKeyDerivation()
        ' Good: Key derived from password
        Dim password As String = GetPasswordFromUser()
        Dim salt As Byte() = GetSaltFromStorage()
        
        Using pbkdf2 As New Rfc2898DeriveBytes(password, salt, 10000)
            Dim key As Byte() = pbkdf2.GetBytes(32)
            
            Using aes As Aes = Aes.Create()
                aes.Key = key
            End Using
        End Using
    End Sub
    
    Private Function GetPasswordFromUser() As String
        ' Implementation to get password from user
        Return ""
    End Function
    
    Private Function GetSaltFromStorage() As Byte()
        ' Implementation to get salt from storage
        Return New Byte(15) {}
    End Function
End Class
