' VB.NET test file for cq-vbn-0239: Do not use weak key derivation function with insufficient iteration count
' This rule detects Rfc2898DeriveBytes usage with iteration count less than 100,000

Imports System
Imports System.Security.Cryptography
Imports System.Text

' Violation: Using very low iteration count (1000)
Public Class WeakIterationCount1
    Public Sub DeriveKeyWithLowIterations()
        Dim password As String = "password123"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("salt123")
        
        ' Violation: Only 1000 iterations (too low)
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 1000)
        Dim key As Byte() = pbkdf2.GetBytes(32)
        
        pbkdf2.Dispose()
    End Sub
End Class

' Violation: Using 5000 iterations (still too low)
Public Class WeakIterationCount2
    Public Sub DeriveKeyWith5000Iterations()
        Dim passwordBytes As Byte() = Encoding.UTF8.GetBytes("mypassword")
        Dim salt As Byte() = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}
        
        ' Violation: 5000 iterations is insufficient
        Dim pbkdf2 As New Rfc2898DeriveBytes(passwordBytes, salt, 5000)
        Dim derivedKey As Byte() = pbkdf2.GetBytes(16)
        
        pbkdf2.Dispose()
    End Sub
End Class

' Violation: Using 10000 iterations (below recommended 100000)
Public Class WeakIterationCount3
    Public Sub DeriveKeyWith10000Iterations()
        Dim password As String = "secret"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("randomsalt")
        
        ' Violation: 10000 iterations is below recommended minimum
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 10000)
        Dim key As Byte() = pbkdf2.GetBytes(24)
        
        pbkdf2.Clear()
    End Sub
End Class

' Violation: Using 50000 iterations (still insufficient)
Public Class WeakIterationCount4
    Public Sub DeriveKeyWith50000Iterations()
        Dim password As String = "testpassword"
        Dim salt As Byte() = New Byte(15) {}
        
        ' Violation: 50000 iterations is below 100000 threshold
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 50000)
        Dim result As Byte() = pbkdf2.GetBytes(32)
        
        pbkdf2.Dispose()
    End Sub
End Class

' Violation: Using object initializer with low iteration count
Public Class WeakIterationCount5
    Public Sub CreateWithLowIterationProperty()
        Dim password As String = "password"
        Dim salt As Byte() = {1, 2, 3, 4, 5, 6, 7, 8}
        
        ' Violation: Using With statement to set low IterationCount
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt) With {
            .IterationCount = 25000
        }
        
        Dim key As Byte() = pbkdf2.GetBytes(16)
        pbkdf2.Dispose()
    End Sub
End Class

' Violation: Setting IterationCount property after creation
Public Class WeakIterationCount6
    Public Sub SetIterationCountAfterCreation()
        Dim password As String = "mypassword"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("salt")
        
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt)
        
        ' Violation: Setting IterationCount to insufficient value
        pbkdf2.IterationCount = 75000
        
        Dim derivedKey As Byte() = pbkdf2.GetBytes(32)
        pbkdf2.Dispose()
    End Sub
End Class

' Violation: Using very low iteration count (100)
Public Class WeakIterationCount7
    Public Sub DeriveKeyWithVeryLowIterations()
        Dim password As String = "weak"
        Dim salt As Byte() = {9, 8, 7, 6, 5, 4, 3, 2}
        
        ' Violation: Only 100 iterations (extremely low)
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 100)
        Dim key As Byte() = pbkdf2.GetBytes(16)
        
        pbkdf2.Dispose()
    End Sub
End Class

' Violation: Using 99999 iterations (just below threshold)
Public Class WeakIterationCount8
    Public Sub DeriveKeyJustBelowThreshold()
        Dim password As String = "almostsecure"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("saltyness")
        
        ' Violation: 99999 is just below the 100000 threshold
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 99999)
        Dim key As Byte() = pbkdf2.GetBytes(32)
        
        pbkdf2.Dispose()
    End Sub
End Class

' Violation: Multiple instances with weak iteration counts
Public Class WeakIterationCount9
    Public Sub CreateMultipleWeakInstances()
        Dim pwd1 As String = "password1"
        Dim pwd2 As String = "password2"
        Dim salt1 As Byte() = {1, 2, 3, 4}
        Dim salt2 As Byte() = {5, 6, 7, 8}
        
        ' Violation: First instance with low iterations
        Dim kdf1 As New Rfc2898DeriveBytes(pwd1, salt1, 15000)
        
        ' Violation: Second instance with low iterations
        Dim kdf2 As New Rfc2898DeriveBytes(pwd2, salt2, 30000)
        
        Dim key1 As Byte() = kdf1.GetBytes(16)
        Dim key2 As Byte() = kdf2.GetBytes(32)
        
        kdf1.Dispose()
        kdf2.Dispose()
    End Sub
End Class

' Violation: Using With statement and low iteration count
Public Class WeakIterationCount10
    Public Sub CreateWithObjectInitializer()
        Dim password As String = "testpass"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("testsalt")
        
        ' Violation: Object initializer with insufficient iterations
        Dim pbkdf2 As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(password, salt) With {
            .IterationCount = 45000
        }
        
        Dim result As Byte() = pbkdf2.GetBytes(24)
        pbkdf2.Dispose()
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureIterationCounts
    Public Sub DeriveKeyWithSecureIterations()
        Dim password As String = "securepassword"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("securesalt")
        
        ' Good: Using 100000 iterations (meets minimum requirement)
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 100000)
        Dim key As Byte() = pbkdf2.GetBytes(32)
        
        pbkdf2.Dispose()
    End Sub
    
    Public Sub DeriveKeyWithHighIterations()
        Dim password As String = "verysecure"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("verysecuresalt")
        
        ' Good: Using 200000 iterations (well above minimum)
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 200000)
        Dim key As Byte() = pbkdf2.GetBytes(48)
        
        pbkdf2.Dispose()
    End Sub
    
    Public Sub DeriveKeyWithVeryHighIterations()
        Dim password As String = "ultrasecure"
        Dim salt As Byte() = Encoding.UTF8.GetBytes("ultrasecuresalt")
        
        ' Good: Using 500000 iterations (very secure)
        Dim pbkdf2 As New Rfc2898DeriveBytes(password, salt, 500000)
        Dim key As Byte() = pbkdf2.GetBytes(64)
        
        pbkdf2.Dispose()
    End Sub
End Class
