' VB.NET test file for cq-vbn-0231: Ensure key derivation function algorithm is sufficiently strong
' This rule detects Rfc2898DeriveBytes usage without SHA256 or higher

Imports System
Imports System.Security.Cryptography
Imports System.Text

' Violation: Using default SHA1 algorithm
Public Class WeakKeyDerivation1
    Public Sub DeriveKeyWithDefaultAlgorithm()
        Dim password As String = "password123"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("salt123")
        
        ' Violation: Using default constructor (SHA1)
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 10000)
        Dim key As Byte() = pbkdf2.GetBytes(32)
    End Sub
End Class

' Violation: Using constructor with iteration count but no hash algorithm
Public Class WeakKeyDerivation2
    Public Sub DeriveKeyWithIterationOnly()
        Dim password As String = "mypassword"
        Dim salt As Byte() = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}
        
        ' Violation: No hash algorithm specified (defaults to SHA1)
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 100000)
        Dim derivedKey As Byte() = pbkdf2.GetBytes(16)
    End Sub
End Class

' Violation: Using byte array password without hash algorithm
Public Class WeakKeyDerivation3
    Public Sub DeriveKeyWithBytePassword()
        Dim passwordBytes As Byte() = Encoding.UTF8.GetBytes("secret")
        Dim salt As Byte() = Encoding.UTF8.GetBytes("randomsalt")
        
        ' Violation: Using byte array constructor without hash algorithm
        Dim pbkdf2 As New Rfc2898DeriveBytes(passwordBytes, salt, 50000)
        Dim key As Byte() = pbkdf2.GetBytes(24)
    End Sub
End Class

' Violation: Creating instance and setting properties without secure hash
Public Class WeakKeyDerivation4
    Public Sub CreateWithProperties()
        Dim password As String = "testpassword"
        Dim salt As Byte() = New Byte(15) {}
        
        ' Violation: Default constructor uses SHA1
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt)
        pbkdf2.IterationCount = 10000
        Dim result As Byte() = pbkdf2.GetBytes(32)
    End Sub
End Class

' Violation: Using in a method with default algorithm
Public Class WeakKeyDerivation5
    Public Function GenerateKey(password As String, salt As Byte()) As Byte()
        ' Violation: No hash algorithm specified
        Using pbkdf2 As New Rfc2898DeriveBytes(password, salt, 25000)
            Return pbkdf2.GetBytes(16)
        End Using
    End Function
End Class

' Violation: Multiple instances with weak algorithms
Public Class WeakKeyDerivation6
    Public Sub MultipleWeakDerivations()
        Dim pwd1 As String = "password1"
        Dim pwd2 As String = "password2"
        Dim salt1 As Byte() = {1, 2, 3, 4}
        Dim salt2 As Byte() = {5, 6, 7, 8}
        
        ' Violation: First instance with default SHA1
        Dim kdf1 As New Rfc2898DeriveBytes(pwd1, salt1, 10000)
        
        ' Violation: Second instance with default SHA1
        Dim kdf2 As New Rfc2898DeriveBytes(pwd2, salt2, 20000)
        
        Dim key1 As Byte() = kdf1.GetBytes(16)
        Dim key2 As Byte() = kdf2.GetBytes(32)
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureKeyDerivation
    Public Sub DeriveKeyWithSHA256()
        Dim password As String = "securepassword"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("securesalt")
        
        ' Good: Using SHA256
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256)
        Dim key As Byte() = pbkdf2.GetBytes(32)
    End Sub
    
    Public Sub DeriveKeyWithSHA384()
        Dim password As String = "securepassword"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("securesalt")
        
        ' Good: Using SHA384
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA384)
        Dim key As Byte() = pbkdf2.GetBytes(48)
    End Sub
    
    Public Sub DeriveKeyWithSHA512()
        Dim password As String = "securepassword"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("securesalt")
        
        ' Good: Using SHA512
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA512)
        Dim key As Byte() = pbkdf2.GetBytes(64)
    End Sub
End Class
