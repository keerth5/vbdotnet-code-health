' VB.NET test file for cq-vbn-0245: Do not use insecure randomness
' This rule detects usage of System.Random for security-sensitive operations

Imports System
Imports System.Security.Cryptography

' Violation: Using System.Random with Next method
Public Class InsecureRandomness1
    Public Sub GenerateRandomNumber()
        ' Violation: Using System.Random for potentially security-sensitive operations
        Dim random As New Random()
        Dim randomValue As Integer = random.Next(1, 100)
        
        ' Use randomValue for some operation
        Console.WriteLine($"Random value: {randomValue}")
    End Sub
End Class

' Violation: Using System.Random with NextBytes
Public Class InsecureRandomness2
    Public Sub GenerateRandomBytes()
        ' Violation: Using System.Random.NextBytes for byte generation
        Dim random As New Random()
        Dim buffer As Byte() = New Byte(15) {}
        random.NextBytes(buffer)
        
        ' Use buffer for encryption or security purposes
        Console.WriteLine("Generated random bytes")
    End Sub
End Class

' Violation: Using System.Random with NextDouble
Public Class InsecureRandomness3
    Public Sub GenerateRandomDouble()
        ' Violation: Using System.Random.NextDouble
        Dim random As New Random()
        Dim randomDouble As Double = random.NextDouble()
        
        ' Use for security-sensitive calculations
        Console.WriteLine($"Random double: {randomDouble}")
    End Sub
End Class

' Violation: Using System.Random with seed
Public Class InsecureRandomness4
    Public Sub GenerateSeededRandom()
        ' Violation: Using System.Random even with seed
        Dim random As New Random(12345)
        Dim value As Integer = random.Next()
        
        Console.WriteLine($"Seeded random: {value}")
    End Sub
End Class

' Violation: Using System.Random in loop
Public Class InsecureRandomness5
    Public Sub GenerateMultipleRandomValues()
        ' Violation: Using System.Random in loop
        Dim random As New Random()
        
        For i As Integer = 0 To 9
            Dim value As Integer = random.Next(1, 1000)
            Console.WriteLine($"Random {i}: {value}")
        Next
    End Sub
End Class

' Violation: Using System.Random for token generation
Public Class InsecureRandomness6
    Public Function GenerateSessionToken() As String
        ' Violation: Using System.Random for security token
        Dim random As New Random()
        Dim token As New System.Text.StringBuilder()
        
        For i As Integer = 0 To 15
            token.Append(random.Next(0, 16).ToString("X"))
        Next
        
        Return token.ToString()
    End Function
End Class

' Violation: Using System.Random for password generation
Public Class InsecureRandomness7
    Public Function GeneratePassword(length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        ' Violation: Using System.Random for password generation
        Dim random As New Random()
        Dim password As New System.Text.StringBuilder()
        
        For i As Integer = 0 To length - 1
            password.Append(chars(random.Next(chars.Length)))
        Next
        
        Return password.ToString()
    End Function
End Class

' Violation: Using System.Random for cryptographic operations
Public Class InsecureRandomness8
    Public Sub GenerateEncryptionKey()
        ' Violation: Using System.Random for encryption key
        Dim random As New Random()
        Dim key As Byte() = New Byte(31) {}
        random.NextBytes(key)
        
        ' Use key for encryption
        Console.WriteLine("Generated encryption key using insecure random")
    End Sub
End Class

' Violation: Using System.Random in static method
Public Class InsecureRandomness9
    Public Shared Function GetRandomId() As Integer
        ' Violation: Static method using System.Random
        Dim random As New Random()
        Return random.Next(100000, 999999)
    End Function
End Class

' Violation: Using System.Random with DateTime seed
Public Class InsecureRandomness10
    Public Sub GenerateTimeBasedRandom()
        ' Violation: Using System.Random with DateTime seed (still insecure)
        Dim random As New Random(DateTime.Now.Millisecond)
        Dim values As Integer() = New Integer(9) {}
        
        For i As Integer = 0 To values.Length - 1
            values(i) = random.Next()
        Next
    End Sub
End Class

' Violation: Using System.Random in property
Public Class InsecureRandomness11
    Private _random As New Random()
    
    Public ReadOnly Property RandomValue As Integer
        Get
            ' Violation: Property using System.Random
            Return _random.Next(1, 100)
        End Get
    End Property
End Class

' Violation: Multiple Random instances
Public Class InsecureRandomness12
    Public Sub UseMultipleRandomInstances()
        ' Violation: First Random instance
        Dim random1 As New Random()
        Dim value1 As Integer = random1.Next()
        
        ' Violation: Second Random instance
        Dim random2 As New Random(Environment.TickCount)
        Dim value2 As Double = random2.NextDouble()
        
        Console.WriteLine($"Values: {value1}, {value2}")
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureRandomness
    Public Sub GenerateSecureRandomBytes()
        ' Good: Using cryptographically secure random
        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            Dim buffer As Byte() = New Byte(31) {}
            rng.GetBytes(buffer)
            
            Console.WriteLine("Generated secure random bytes")
        End Using
    End Sub
    
    Public Sub GenerateSecureRandomNumber()
        ' Good: Using RNGCryptoServiceProvider
        Using rng As New RNGCryptoServiceProvider()
            Dim bytes As Byte() = New Byte(3) {}
            rng.GetBytes(bytes)
            
            Dim randomInt As Integer = BitConverter.ToInt32(bytes, 0)
            Console.WriteLine($"Secure random: {Math.Abs(randomInt)}")
        End Using
    End Sub
    
    Public Function GenerateSecureToken() As String
        ' Good: Using cryptographically secure random for tokens
        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            Dim bytes As Byte() = New Byte(15) {}
            rng.GetBytes(bytes)
            
            Return Convert.ToBase64String(bytes)
        End Using
    End Function
    
    Public Sub GenerateSecurePassword()
        ' Good: Using secure random for password generation
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*"
        
        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            Dim bytes As Byte() = New Byte(15) {}
            Dim password As New System.Text.StringBuilder()
            
            rng.GetBytes(bytes)
            
            For Each b In bytes
                password.Append(chars(b Mod chars.Length))
            Next
            
            Console.WriteLine("Generated secure password")
        End Using
    End Sub
    
    Public Sub NonSecurityRelatedRandom()
        ' Note: This would still be flagged by the rule because it uses System.Random
        ' The rule is conservative and flags all System.Random usage
        ' In practice, you'd need to evaluate if this is truly non-security related
    End Sub
End Class
