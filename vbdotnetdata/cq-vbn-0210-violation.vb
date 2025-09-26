' Test file for cq-vbn-0210: Do Not Use Weak Cryptographic Algorithms
' This rule detects usage of weak cryptographic algorithms like TripleDES, SHA1, and RIPEMD160

Imports System
Imports System.Security.Cryptography

Public Class WeakCryptographicAlgorithmViolations
    
    ' Violation: TripleDES usage
    Public Sub UseTripleDES()
        Dim algorithm As TripleDES = TripleDES.Create() ' Violation
    End Sub
    
    ' Violation: 3DES usage
    Public Sub Use3DES()
        Dim algorithm As TripleDES = TripleDES.Create("3DES") ' Violation
    End Sub
    
    ' Violation: SHA1 usage
    Public Sub UseSHA1()
        Dim hash As SHA1 = SHA1.Create() ' Violation
    End Sub
    
    ' Violation: RIPEMD160 usage
    Public Sub UseRIPEMD160()
        Dim hash As RIPEMD160 = RIPEMD160.Create() ' Violation
    End Sub
    
    ' Violation: New TripleDESCryptoServiceProvider
    Public Sub NewTripleDESCryptoServiceProvider()
        Dim provider As New TripleDESCryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: New SHA1Managed
    Public Sub NewSHA1Managed()
        Dim sha1 As New SHA1Managed() ' Violation
    End Sub
    
    ' Violation: New SHA1CryptoServiceProvider
    Public Sub NewSHA1CryptoServiceProvider()
        Dim sha1 As New SHA1CryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: New RIPEMD160Managed
    Public Sub NewRIPEMD160Managed()
        Dim ripemd As New RIPEMD160Managed() ' Violation
    End Sub
    
    ' Violation: HashAlgorithm.Create with TripleDES
    Public Sub HashAlgorithmCreateWithTripleDES()
        Dim hash = HashAlgorithm.Create("TripleDES") ' Violation
    End Sub
    
    ' Violation: HashAlgorithm.Create with 3DES
    Public Sub HashAlgorithmCreateWith3DES()
        Dim hash = HashAlgorithm.Create("3DES") ' Violation
    End Sub
    
    ' Violation: HashAlgorithm.Create with SHA1
    Public Sub HashAlgorithmCreateWithSHA1()
        Dim hash = HashAlgorithm.Create("SHA1") ' Violation
    End Sub
    
    ' Violation: HashAlgorithm.Create with RIPEMD160
    Public Sub HashAlgorithmCreateWithRIPEMD160()
        Dim hash = HashAlgorithm.Create("RIPEMD160") ' Violation
    End Sub
    
    ' Violation: Multiple weak algorithms in one method
    Public Sub MultipleWeakAlgorithms()
        Dim tripleDES As New TripleDESCryptoServiceProvider() ' Violation
        Dim sha1 As New SHA1Managed() ' Violation
        Dim ripemd As New RIPEMD160Managed() ' Violation
    End Sub
    
    ' Violation: Weak algorithms in loop
    Public Sub WeakAlgorithmsInLoop()
        For i As Integer = 0 To 5
            Dim sha1 As New SHA1Managed() ' Violation
        Next
    End Sub
    
    ' Violation: Weak algorithms in conditional
    Public Sub ConditionalWeakAlgorithms(useWeak As Boolean)
        If useWeak Then
            Dim tripleDES As New TripleDESCryptoServiceProvider() ' Violation
        End If
    End Sub
    
    ' Violation: Weak algorithms in Try-Catch
    Public Sub WeakAlgorithmsInTryCatch()
        Try
            Dim sha1 As New SHA1CryptoServiceProvider() ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Weak algorithm field
    Private tripleDESField As New TripleDESCryptoServiceProvider() ' Violation
    
    Public Sub UseWeakAlgorithmField()
        Dim data As Byte() = {1, 2, 3, 4, 5}
        Dim encrypted = tripleDESField.CreateEncryptor()
    End Sub
    
    ' Violation: Weak algorithm in method parameter
    Public Sub ProcessWithWeakAlgorithm(algorithm As TripleDES) ' Violation in parameter type
        Console.WriteLine("Processing with weak algorithm")
    End Sub
    
    ' Violation: Weak algorithm in method return type
    Public Function CreateWeakAlgorithm() As SHA1 ' Violation in return type
        Return New SHA1Managed() ' Violation
    End Function
    
    ' Non-violation: Strong algorithms (should not be detected)
    Public Sub UseStrongAlgorithms()
        Dim aes As Aes = Aes.Create() ' No violation - strong algorithm
        Dim sha256 As SHA256 = SHA256.Create() ' No violation - strong algorithm
        Dim sha512 As SHA512 = SHA512.Create() ' No violation - strong algorithm
    End Sub
    
    ' Non-violation: Comments and strings mentioning weak algorithms
    Public Sub CommentsAndStrings()
        ' This is about TripleDES and SHA1
        Dim message As String = "Do not use TripleDES, SHA1, or RIPEMD160"
        Console.WriteLine("Weak algorithms like 3DES should be avoided")
    End Sub
    
    ' Violation: Weak algorithms in Select Case
    Public Sub WeakAlgorithmsInSelectCase(algorithmType As Integer)
        Select Case algorithmType
            Case 1
                Dim tripleDES As New TripleDESCryptoServiceProvider() ' Violation
            Case 2
                Dim sha1 As New SHA1Managed() ' Violation
            Case 3
                Dim ripemd As New RIPEMD160Managed() ' Violation
        End Select
    End Sub
    
    ' Violation: Weak algorithms in Using statement
    Public Sub WeakAlgorithmsInUsing()
        Using sha1 As New SHA1CryptoServiceProvider() ' Violation
            Dim data As Byte() = {1, 2, 3, 4, 5}
            Dim hash = sha1.ComputeHash(data)
        End Using
    End Sub
    
    ' Violation: Weak algorithms in While loop
    Public Sub WeakAlgorithmsInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim tripleDES As New TripleDESCryptoServiceProvider() ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: Weak algorithm with variable assignment
    Public Sub WeakAlgorithmWithVariableAssignment()
        Dim algorithm As SymmetricAlgorithm
        algorithm = New TripleDESCryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: Weak algorithm in array initialization
    Public Sub WeakAlgorithmsInArrayInitialization()
        Dim algorithms() As HashAlgorithm = {
            New SHA1Managed(), ' Violation
            New RIPEMD160Managed() ' Violation
        }
    End Sub
    
    ' Violation: Weak algorithm in collection initialization
    Public Sub WeakAlgorithmsInCollectionInitialization()
        Dim hashList As New List(Of HashAlgorithm) From {
            New SHA1CryptoServiceProvider(), ' Violation
            New RIPEMD160Managed() ' Violation
        }
    End Sub
    
    ' Violation: Weak algorithm with conditional operator
    Public Sub WeakAlgorithmWithConditionalOperator(useSHA1 As Boolean)
        Dim hash = If(useSHA1, New SHA1Managed(), SHA256.Create()) ' Violation
    End Sub
    
    ' Violation: Weak algorithm in lambda expression
    Public Sub WeakAlgorithmInLambda()
        Dim createHash = Function() New SHA1CryptoServiceProvider() ' Violation
    End Sub
    
    ' Violation: Weak algorithm in property
    Public ReadOnly Property WeakHashAlgorithm As SHA1 ' Violation in property type
        Get
            Return New SHA1Managed() ' Violation
        End Get
    End Property
    
    ' Violation: Weak algorithm in delegate
    Public Delegate Function WeakAlgorithmDelegate() As TripleDES ' Violation in delegate return type
    
    ' Violation: Weak algorithm in generic constraint
    Public Sub GenericMethodWithWeakConstraint(Of T As {SHA1, New})() ' Violation in constraint
        Dim instance As T = New T()
    End Sub
    
    ' Violation: Weak algorithm in cast operation
    Public Sub WeakAlgorithmInCast()
        Dim obj As Object = New SHA1Managed() ' Violation
        Dim sha1 = CType(obj, SHA1)
    End Sub

End Class
