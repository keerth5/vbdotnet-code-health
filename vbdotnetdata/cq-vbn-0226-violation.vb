' VB.NET test file for cq-vbn-0226: Do not use obsolete key derivation function
' This rule detects usage of weak key derivation methods

Imports System
Imports System.Security.Cryptography

Public Class ObsoleteKeyDerivationViolations
    
    ' Violation: PasswordDeriveBytes usage
    Public Sub UsePasswordDeriveBytes()
        Dim password As String = "mypassword"
        Dim salt() As Byte = {1, 2, 3, 4, 5, 6, 7, 8}
        
        ' Violation: New PasswordDeriveBytes
        Dim pdb As New PasswordDeriveBytes(password, salt)
        Dim key() As Byte = pdb.GetBytes(16)
        
        pdb.Dispose()
    End Sub
    
    ' Violation: Multiple PasswordDeriveBytes instances
    Public Sub UseMultiplePasswordDeriveBytes()
        Dim password1 As String = "password1"
        Dim password2 As String = "password2"
        Dim salt1() As Byte = {1, 2, 3, 4}
        Dim salt2() As Byte = {5, 6, 7, 8}
        
        ' Violation: Multiple New PasswordDeriveBytes
        Dim pdb1 As New PasswordDeriveBytes(password1, salt1)
        Dim pdb2 As New PasswordDeriveBytes(password2, salt2)
        
        Dim key1() As Byte = pdb1.GetBytes(16)
        Dim key2() As Byte = pdb2.GetBytes(32)
        
        pdb1.Dispose()
        pdb2.Dispose()
    End Sub
    
    ' Violation: PasswordDeriveBytes with iteration count
    Public Sub UsePasswordDeriveBytesWithIterations()
        Dim password As String = "complexpassword"
        Dim salt() As Byte = {10, 20, 30, 40, 50, 60, 70, 80}
        Dim iterations As Integer = 1000
        
        ' Violation: New PasswordDeriveBytes with iterations
        Dim pdb As New PasswordDeriveBytes(password, salt, iterations)
        Dim derivedKey() As Byte = pdb.GetBytes(24)
        
        pdb.Dispose()
    End Sub
    
    ' Violation: PasswordDeriveBytes with hash algorithm
    Public Sub UsePasswordDeriveBytesWithHash()
        Dim password As String = "securepassword"
        Dim salt() As Byte = {11, 22, 33, 44, 55, 66, 77, 88}
        
        ' Violation: New PasswordDeriveBytes with hash algorithm
        Dim pdb As New PasswordDeriveBytes(password, salt, "SHA1")
        Dim key() As Byte = pdb.GetBytes(20)
        
        pdb.Dispose()
    End Sub
    
    ' Violation: Rfc2898DeriveBytes.CryptDeriveKey usage
    Public Sub UseRfc2898DeriveBytesWithCryptDeriveKey()
        Dim password As String = "mypassword"
        Dim salt() As Byte = {1, 2, 3, 4, 5, 6, 7, 8}
        
        Dim rfc2898 As New Rfc2898DeriveBytes(password, salt)
        
        ' Violation: Rfc2898DeriveBytes.CryptDeriveKey
        Dim key() As Byte = rfc2898.CryptDeriveKey("RC2", "SHA1", 128, New Byte(7) {})
        
        rfc2898.Dispose()
    End Sub
    
    ' Violation: Multiple Rfc2898DeriveBytes.CryptDeriveKey calls
    Public Sub UseMultipleCryptDeriveKey()
        Dim password As String = "password123"
        Dim salt() As Byte = {9, 8, 7, 6, 5, 4, 3, 2}
        
        Dim rfc2898 As New Rfc2898DeriveBytes(password, salt, 2000)
        
        ' Violation: Multiple Rfc2898DeriveBytes.CryptDeriveKey calls
        Dim key1() As Byte = rfc2898.CryptDeriveKey("DES", "SHA1", 64, New Byte(7) {})
        Dim key2() As Byte = rfc2898.CryptDeriveKey("RC4", "MD5", 128, New Byte(15) {})
        
        rfc2898.Dispose()
    End Sub
    
    ' Violation: PasswordDeriveBytes in try-catch
    Public Sub UsePasswordDeriveBytesWithErrorHandling()
        Try
            Dim password As String = "errorhandling"
            Dim salt() As Byte = {1, 3, 5, 7, 9, 11, 13, 15}
            
            ' Violation: New PasswordDeriveBytes in try block
            Dim pdb As New PasswordDeriveBytes(password, salt)
            Dim key() As Byte = pdb.GetBytes(32)
            
            pdb.Dispose()
        Catch ex As Exception
            Console.WriteLine("Key derivation error: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: PasswordDeriveBytes with variable
    Public Sub UsePasswordDeriveBytesWithVariable()
        Dim password As String = "variablepassword"
        Dim salt() As Byte = {2, 4, 6, 8, 10, 12, 14, 16}
        
        Dim keyDeriver As PasswordDeriveBytes
        
        ' Violation: PasswordDeriveBytes assignment
        keyDeriver = New PasswordDeriveBytes(password, salt)
        
        Dim derivedKey() As Byte = keyDeriver.GetBytes(16)
        keyDeriver.Dispose()
    End Sub
    
    ' Violation: PasswordDeriveBytes in conditional
    Public Sub ConditionalPasswordDeriveBytes(useWeakDerivation As Boolean)
        If useWeakDerivation Then
            Dim password As String = "conditionalpassword"
            Dim salt() As Byte = {17, 19, 23, 29, 31, 37, 41, 43}
            
            ' Violation: New PasswordDeriveBytes in conditional
            Dim pdb As New PasswordDeriveBytes(password, salt)
            Dim key() As Byte = pdb.GetBytes(24)
            
            pdb.Dispose()
        End If
    End Sub
    
    ' Violation: PasswordDeriveBytes in loop
    Public Sub UsePasswordDeriveBytesInLoop()
        Dim passwords() As String = {"pass1", "pass2", "pass3"}
        Dim baseSalt() As Byte = {1, 2, 3, 4, 5, 6, 7, 8}
        
        For i As Integer = 0 To passwords.Length - 1
            ' Violation: New PasswordDeriveBytes in loop
            Dim pdb As New PasswordDeriveBytes(passwords(i), baseSalt)
            Dim key() As Byte = pdb.GetBytes(16)
            
            pdb.Dispose()
        Next
    End Sub
    
    ' Good example (should not be detected - uses Rfc2898DeriveBytes properly)
    Public Sub UseSecureKeyDerivation()
        Dim password As String = "securepassword"
        Dim salt() As Byte = {1, 2, 3, 4, 5, 6, 7, 8}
        
        ' Good: Using Rfc2898DeriveBytes without CryptDeriveKey
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 10000)
        Dim key() As Byte = pbkdf2.GetBytes(32)
        
        pbkdf2.Dispose()
    End Sub
    
    ' Good example (should not be detected - different class)
    Public Sub UseOtherCryptography()
        ' Good: Using other cryptographic methods
        Dim aes As Aes = Aes.Create()
        aes.GenerateKey()
        aes.GenerateIV()
        
        aes.Dispose()
    End Sub
    
    ' Violation: PasswordDeriveBytes with return
    Public Function CreatePasswordDeriveBytes() As PasswordDeriveBytes
        Dim password As String = "returnpassword"
        Dim salt() As Byte = {47, 53, 59, 61, 67, 71, 73, 79}
        
        ' Violation: New PasswordDeriveBytes for return
        Return New PasswordDeriveBytes(password, salt)
    End Function
    
    ' Violation: Rfc2898DeriveBytes.CryptDeriveKey in method
    Public Sub DeriveKeyFromRfc2898(rfc As Rfc2898DeriveBytes)
        ' Violation: CryptDeriveKey method call
        Dim key() As Byte = rfc.CryptDeriveKey("AES", "SHA256", 256, New Byte(15) {})
    End Sub
    
    ' Violation: PasswordDeriveBytes with encoding
    Public Sub UsePasswordDeriveBytesWithEncoding()
        Dim password As String = "encodedpassword"
        Dim salt() As Byte = {83, 89, 97, 101, 103, 107, 109, 113}
        
        ' Violation: New PasswordDeriveBytes with encoding
        Dim pdb As New PasswordDeriveBytes(password, salt, "SHA256", 5000)
        Dim key() As Byte = pdb.GetBytes(32)
        
        pdb.Dispose()
    End Sub
    
    ' Violation: PasswordDeriveBytes in property
    Public ReadOnly Property WeakKeyDeriver() As PasswordDeriveBytes
        Get
            Dim password As String = "propertypassword"
            Dim salt() As Byte = {127, 131, 137, 139, 149, 151, 157, 163}
            
            ' Violation: New PasswordDeriveBytes in property
            Return New PasswordDeriveBytes(password, salt)
        End Get
    End Property
    
    ' Violation: Complex Rfc2898DeriveBytes.CryptDeriveKey usage
    Public Sub ComplexCryptDeriveKey()
        Dim password As String = "complexpassword"
        Dim salt() As Byte = {167, 173, 179, 181, 191, 193, 197, 199}
        
        Dim rfc2898 As New Rfc2898DeriveBytes(password, salt, 1500)
        
        ' Violation: Complex Rfc2898DeriveBytes.CryptDeriveKey call
        Dim symmetricKey() As Byte = rfc2898.CryptDeriveKey("TripleDES", "SHA1", 192, New Byte(23) {})
        
        rfc2898.Dispose()
    End Sub
End Class
