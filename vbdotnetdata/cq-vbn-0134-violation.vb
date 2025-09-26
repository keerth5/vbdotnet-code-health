' VB.NET test file for cq-vbn-0134: Prefer static HashData method over ComputeHash
' Rule: It's more efficient to use the static HashData method over creating and managing a HashAlgorithm instance to call ComputeHash.

Imports System
Imports System.Security.Cryptography
Imports System.Text

Public Class HashingExamples
    
    Public Sub TestSHA256Hashing()
        Dim data As String = "Hello, World!"
        Dim bytes() As Byte = Encoding.UTF8.GetBytes(data)
        
        ' Violation: Creating SHA256 instance and calling ComputeHash
        Using sha256 As SHA256 = SHA256.Create()
            Dim hash() As Byte = sha256.ComputeHash(bytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
        
        ' Violation: Using SHA256CryptoServiceProvider directly
        Using sha256 As New SHA256CryptoServiceProvider()
            Dim hash() As Byte = sha256.ComputeHash(bytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
        
        ' Violation: Creating instance and calling ComputeHash with stream
        Using sha256 As SHA256 = SHA256.Create()
            Using stream As New MemoryStream(bytes)
                Dim hash() As Byte = sha256.ComputeHash(stream)
                Console.WriteLine(Convert.ToHexString(hash))
            End Using
        End Using
    End Sub
    
    Public Sub TestSHA1Hashing()
        Dim input As String = "Test data for SHA1"
        Dim inputBytes() As Byte = Encoding.UTF8.GetBytes(input)
        
        ' Violation: Creating SHA1 instance and calling ComputeHash
        Using sha1 As SHA1 = SHA1.Create()
            Dim hash() As Byte = sha1.ComputeHash(inputBytes)
            Console.WriteLine(Convert.ToBase64String(hash))
        End Using
        
        ' Violation: Using SHA1CryptoServiceProvider
        Using sha1 As New SHA1CryptoServiceProvider()
            Dim hash() As Byte = sha1.ComputeHash(inputBytes)
            Console.WriteLine(Convert.ToBase64String(hash))
        End Using
    End Sub
    
    Public Sub TestSHA384Hashing()
        Dim message As String = "Message to hash with SHA384"
        Dim messageBytes() As Byte = Encoding.UTF8.GetBytes(message)
        
        ' Violation: Creating SHA384 instance and calling ComputeHash
        Using sha384 As SHA384 = SHA384.Create()
            Dim hash() As Byte = sha384.ComputeHash(messageBytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
        
        ' Violation: Using SHA384CryptoServiceProvider
        Using sha384 As New SHA384CryptoServiceProvider()
            Dim hash() As Byte = sha384.ComputeHash(messageBytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
    End Sub
    
    Public Sub TestSHA512Hashing()
        Dim text As String = "Data for SHA512 hashing"
        Dim textBytes() As Byte = Encoding.UTF8.GetBytes(text)
        
        ' Violation: Creating SHA512 instance and calling ComputeHash
        Using sha512 As SHA512 = SHA512.Create()
            Dim hash() As Byte = sha512.ComputeHash(textBytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
        
        ' Violation: Using SHA512CryptoServiceProvider
        Using sha512 As New SHA512CryptoServiceProvider()
            Dim hash() As Byte = sha512.ComputeHash(textBytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
    End Sub
    
    Public Sub TestMD5Hashing()
        Dim content As String = "Content to hash with MD5"
        Dim contentBytes() As Byte = Encoding.UTF8.GetBytes(content)
        
        ' Violation: Creating MD5 instance and calling ComputeHash
        Using md5 As MD5 = MD5.Create()
            Dim hash() As Byte = md5.ComputeHash(contentBytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
        
        ' Violation: Using MD5CryptoServiceProvider
        Using md5 As New MD5CryptoServiceProvider()
            Dim hash() As Byte = md5.ComputeHash(contentBytes)
            Console.WriteLine(Convert.ToHexString(hash))
        End Using
    End Sub
    
    Public Sub TestHashingInLoop()
        Dim documents() As String = {"Doc1", "Doc2", "Doc3", "Doc4", "Doc5"}
        
        ' Violation: Creating hash algorithm instance in loop
        For Each doc In documents
            Using sha256 As SHA256 = SHA256.Create()
                Dim docBytes() As Byte = Encoding.UTF8.GetBytes(doc)
                Dim hash() As Byte = sha256.ComputeHash(docBytes)
                Console.WriteLine($"{doc}: {Convert.ToHexString(hash)}")
            End Using
        Next
        
        ' Violation: Creating instance outside loop but still using ComputeHash
        Using sha256 As SHA256 = SHA256.Create()
            For Each doc In documents
                Dim docBytes() As Byte = Encoding.UTF8.GetBytes(doc)
                Dim hash() As Byte = sha256.ComputeHash(docBytes)
                Console.WriteLine($"{doc}: {Convert.ToHexString(hash)}")
            Next
        End Using
    End Sub
    
    Public Sub TestHashingWithStreams()
        Dim filePaths() As String = {"file1.txt", "file2.txt", "file3.txt"}
        
        For Each filePath In filePaths
            ' Violation: Creating hash algorithm and using ComputeHash with stream
            Using sha256 As SHA256 = SHA256.Create()
                Using fileStream As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    Dim hash() As Byte = sha256.ComputeHash(fileStream)
                    Console.WriteLine($"{filePath}: {Convert.ToHexString(hash)}")
                End Using
            End Using
        Next
    End Sub
    
    Public Sub TestMultipleHashAlgorithms()
        Dim data As String = "Data to hash with multiple algorithms"
        Dim dataBytes() As Byte = Encoding.UTF8.GetBytes(data)
        
        ' Violation: Using SHA256 with ComputeHash
        Using sha256 As SHA256 = SHA256.Create()
            Dim sha256Hash() As Byte = sha256.ComputeHash(dataBytes)
            Console.WriteLine($"SHA256: {Convert.ToHexString(sha256Hash)}")
        End Using
        
        ' Violation: Using SHA1 with ComputeHash
        Using sha1 As SHA1 = SHA1.Create()
            Dim sha1Hash() As Byte = sha1.ComputeHash(dataBytes)
            Console.WriteLine($"SHA1: {Convert.ToHexString(sha1Hash)}")
        End Using
        
        ' Violation: Using MD5 with ComputeHash
        Using md5 As MD5 = MD5.Create()
            Dim md5Hash() As Byte = md5.ComputeHash(dataBytes)
            Console.WriteLine($"MD5: {Convert.ToHexString(md5Hash)}")
        End Using
    End Sub
    
    Public Function ComputeFileHash(filePath As String) As String
        ' Violation: Creating hash algorithm instance and using ComputeHash
        Using sha256 As SHA256 = SHA256.Create()
            Using fileStream As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                Dim hash() As Byte = sha256.ComputeHash(fileStream)
                Return Convert.ToHexString(hash)
            End Using
        End Using
    End Function
    
    Public Function ComputeStringHash(input As String) As String
        Dim inputBytes() As Byte = Encoding.UTF8.GetBytes(input)
        
        ' Violation: Creating hash algorithm instance and using ComputeHash
        Using sha256 As SHA256 = SHA256.Create()
            Dim hash() As Byte = sha256.ComputeHash(inputBytes)
            Return Convert.ToBase64String(hash)
        End Using
    End Function
    
    ' Examples of correct usage (for reference)
    Public Function ComputeHashCorrectly(data() As Byte) As Byte()
        ' Correct: Using static HashData method
        Return SHA256.HashData(data)
    End Function
    
    Public Function ComputeStringHashCorrectly(input As String) As String
        Dim inputBytes() As Byte = Encoding.UTF8.GetBytes(input)
        ' Correct: Using static HashData method
        Dim hash() As Byte = SHA256.HashData(inputBytes)
        Return Convert.ToHexString(hash)
    End Function
    
    Public Sub TestPasswordHashing()
        Dim password As String = "MySecretPassword123"
        Dim passwordBytes() As Byte = Encoding.UTF8.GetBytes(password)
        
        ' Violation: Using hash algorithm instance for password hashing
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedPassword() As Byte = sha256.ComputeHash(passwordBytes)
            Console.WriteLine($"Hashed Password: {Convert.ToBase64String(hashedPassword)}")
        End Using
    End Sub
    
    Public Sub TestDataIntegrityCheck()
        Dim originalData As String = "Important data that needs integrity check"
        Dim dataBytes() As Byte = Encoding.UTF8.GetBytes(originalData)
        
        ' Violation: Computing hash for data integrity using instance method
        Using sha512 As SHA512 = SHA512.Create()
            Dim integrityHash() As Byte = sha512.ComputeHash(dataBytes)
            
            ' Store the hash for later verification
            Dim storedHash As String = Convert.ToBase64String(integrityHash)
            Console.WriteLine($"Integrity Hash: {storedHash}")
        End Using
    End Sub
End Class
