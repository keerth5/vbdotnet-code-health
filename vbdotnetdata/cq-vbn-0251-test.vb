' Test file for cq-vbn-0251: Use CreateEncryptor with the default IV
' Rule should detect: CreateEncryptor calls with hardcoded IV or GenerateIV issues

Imports System.Security.Cryptography

Public Class EncryptionTest
    
    ' VIOLATION: CreateEncryptor with hardcoded IV
    Public Function BadEncryption1() As Byte()
        Dim aes As Aes = Aes.Create()
        aes.Key = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}
        aes.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}
        Dim encryptor = aes.CreateEncryptor(aes.Key, aes.IV)
        Return Nothing
    End Function
    
    ' VIOLATION: CreateEncryptor with hardcoded IV array
    Public Function BadEncryption2() As Byte()
        Dim aes As Aes = Aes.Create()
        aes.Key = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}
        Dim hardcodedIV As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}
        Dim encryptor = aes.CreateEncryptor(aes.Key, hardcodedIV)
        Return Nothing
    End Function
    
    ' VIOLATION: IV assignment with hardcoded array
    Public Function BadEncryption3() As Byte()
        Dim aes As Aes = Aes.Create()
        aes.IV = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
        Return Nothing
    End Function
    
    ' GOOD: Using GenerateIV() - should NOT be flagged
    Public Function GoodEncryption1() As Byte()
        Dim aes As Aes = Aes.Create()
        aes.GenerateKey()
        aes.GenerateIV()
        Dim encryptor = aes.CreateEncryptor()
        Return Nothing
    End Function
    
    ' GOOD: Using random IV - should NOT be flagged
    Public Function GoodEncryption2() As Byte()
        Dim aes As Aes = Aes.Create()
        aes.Key = New Byte(15) {}
        Dim rng As New RNGCryptoServiceProvider()
        rng.GetBytes(aes.IV)
        Dim encryptor = aes.CreateEncryptor(aes.Key, aes.IV)
        Return Nothing
    End Function
    
    ' GOOD: Normal method call - should NOT be flagged
    Public Function NormalMethod() As String
        Return "This is not encryption related"
    End Function
    
End Class
