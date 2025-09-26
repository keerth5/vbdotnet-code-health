' Test file for cq-vbn-0211: Do Not Use Broken Cryptographic Algorithms
' This rule detects usage of broken cryptographic algorithms like MD5, DES, and RC2

Imports System
Imports System.Security.Cryptography

Public Class BrokenCryptographicAlgorithmViolations
    
    ' Violation: MD5 usage
    Public Sub UseMD5()
        Dim hash As MD5 = MD5.Create() ' Violation
    End Sub
    
    ' Violation: DES usage
    Public Sub UseDES()
        Dim algorithm As DES = DES.Create() ' Violation
    End Sub
    
    ' Violation: RC2 usage
    Public Sub UseRC2()
        Dim algorithm As RC2 = RC2.Create() ' Violation
    End Sub
    
    ' Violation: New MD5CryptoServiceProvider
    Public Sub NewMD5CryptoServiceProvider()
        Dim md5 As New MD5CryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: New DESCryptoServiceProvider
    Public Sub NewDESCryptoServiceProvider()
        Dim des As New DESCryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: New RC2CryptoServiceProvider
    Public Sub NewRC2CryptoServiceProvider()
        Dim rc2 As New RC2CryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: HashAlgorithm.Create with MD5
    Public Sub HashAlgorithmCreateWithMD5()
        Dim hash = HashAlgorithm.Create("MD5") ' Violation
    End Sub
    
    ' Violation: HashAlgorithm.Create with DES
    Public Sub HashAlgorithmCreateWithDES()
        Dim algorithm = HashAlgorithm.Create("DES") ' Violation
    End Sub
    
    ' Violation: HashAlgorithm.Create with RC2
    Public Sub HashAlgorithmCreateWithRC2()
        Dim algorithm = HashAlgorithm.Create("RC2") ' Violation
    End Sub
    
    ' Violation: Multiple broken algorithms in one method
    Public Sub MultipleBrokenAlgorithms()
        Dim md5 As New MD5CryptoServiceProvider() ' Violation
        Dim des As New DESCryptoServiceProvider() ' Violation
        Dim rc2 As New RC2CryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: Broken algorithms in loop
    Public Sub BrokenAlgorithmsInLoop()
        For i As Integer = 0 To 5
            Dim md5 As New MD5CryptoServiceProvider() ' Violation
        Next
    End Sub
    
    ' Violation: Broken algorithms in conditional
    Public Sub ConditionalBrokenAlgorithms(useBroken As Boolean)
        If useBroken Then
            Dim des As New DESCryptoServiceProvider() ' Violation
        End If
    End Sub
    
    ' Violation: Broken algorithms in Try-Catch
    Public Sub BrokenAlgorithmsInTryCatch()
        Try
            Dim rc2 As New RC2CryptoServiceProvider() ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Broken algorithm field
    Private md5Field As New MD5CryptoServiceProvider() ' Violation
    
    Public Sub UseBrokenAlgorithmField()
        Dim data As Byte() = {1, 2, 3, 4, 5}
        Dim hash = md5Field.ComputeHash(data)
    End Sub
    
    ' Violation: Broken algorithm in method parameter
    Public Sub ProcessWithBrokenAlgorithm(algorithm As MD5) ' Violation in parameter type
        Console.WriteLine("Processing with broken algorithm")
    End Sub
    
    ' Violation: Broken algorithm in method return type
    Public Function CreateBrokenAlgorithm() As DES ' Violation in return type
        Return New DESCryptoServiceProvider() ' Violation
    End Function
    
    ' Non-violation: Strong algorithms (should not be detected)
    Public Sub UseStrongAlgorithms()
        Dim aes As Aes = Aes.Create() ' No violation - strong algorithm
        Dim sha256 As SHA256 = SHA256.Create() ' No violation - strong algorithm
        Dim sha512 As SHA512 = SHA512.Create() ' No violation - strong algorithm
    End Sub
    
    ' Non-violation: Comments and strings mentioning broken algorithms
    Public Sub CommentsAndStrings()
        ' This is about MD5 and DES
        Dim message As String = "Do not use MD5, DES, or RC2"
        Console.WriteLine("Broken algorithms like MD5 should be avoided")
    End Sub
    
    ' Violation: Broken algorithms in Select Case
    Public Sub BrokenAlgorithmsInSelectCase(algorithmType As Integer)
        Select Case algorithmType
            Case 1
                Dim md5 As New MD5CryptoServiceProvider() ' Violation
            Case 2
                Dim des As New DESCryptoServiceProvider() ' Violation
            Case 3
                Dim rc2 As New RC2CryptoServiceProvider() ' Violation
        End Select
    End Sub
    
    ' Violation: Broken algorithms in Using statement
    Public Sub BrokenAlgorithmsInUsing()
        Using md5 As New MD5CryptoServiceProvider() ' Violation
            Dim data As Byte() = {1, 2, 3, 4, 5}
            Dim hash = md5.ComputeHash(data)
        End Using
    End Sub
    
    ' Violation: Broken algorithms in While loop
    Public Sub BrokenAlgorithmsInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim des As New DESCryptoServiceProvider() ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: Broken algorithm with variable assignment
    Public Sub BrokenAlgorithmWithVariableAssignment()
        Dim algorithm As SymmetricAlgorithm
        algorithm = New DESCryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: Broken algorithm in array initialization
    Public Sub BrokenAlgorithmsInArrayInitialization()
        Dim algorithms() As HashAlgorithm = {
            New MD5CryptoServiceProvider() ' Violation
        }
    End Sub
    
    ' Violation: Broken algorithm in collection initialization
    Public Sub BrokenAlgorithmsInCollectionInitialization()
        Dim hashList As New List(Of HashAlgorithm) From {
            New MD5CryptoServiceProvider() ' Violation
        }
    End Sub
    
    ' Violation: Broken algorithm with conditional operator
    Public Sub BrokenAlgorithmWithConditionalOperator(useMD5 As Boolean)
        Dim hash = If(useMD5, New MD5CryptoServiceProvider(), SHA256.Create()) ' Violation
    End Sub
    
    ' Violation: Broken algorithm in lambda expression
    Public Sub BrokenAlgorithmInLambda()
        Dim createHash = Function() New MD5CryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: Broken algorithm in property
    Public ReadOnly Property BrokenHashAlgorithm As MD5 ' Violation in property type
        Get
            Return New MD5CryptoServiceProvider() ' Violation
        End Get
    End Property
    
    ' Violation: Broken algorithm in delegate
    Public Delegate Function BrokenAlgorithmDelegate() As DES ' Violation in delegate return type
    
    ' Violation: Broken algorithm in generic constraint
    Public Sub GenericMethodWithBrokenConstraint(Of T As {MD5, New})() ' Violation in constraint
        Dim instance As T = New T()
    End Sub
    
    ' Violation: Broken algorithm in cast operation
    Public Sub BrokenAlgorithmInCast()
        Dim obj As Object = New MD5CryptoServiceProvider() ' Violation
        Dim md5 = CType(obj, MD5)
    End Sub
    
    ' Violation: Mixed case broken algorithms
    Public Sub MixedCaseBrokenAlgorithms()
        Dim hash1 = HashAlgorithm.Create("md5") ' Violation
        Dim hash2 = HashAlgorithm.Create("des") ' Violation
        Dim hash3 = HashAlgorithm.Create("rc2") ' Violation
    End Sub

End Class
