' Test file for cq-vbn-0212: Do Not Use Unsafe Cipher Modes
' This rule detects usage of unsafe cipher modes like ECB and OFB

Imports System
Imports System.Security.Cryptography

Public Class UnsafeCipherModeViolations
    
    ' Violation: Mode set to ECB
    Public Sub SetModeToECB()
        Dim aes As Aes = Aes.Create()
        aes.Mode = CipherMode.ECB ' Violation
    End Sub
    
    ' Violation: Mode set to OFB
    Public Sub SetModeToOFB()
        Dim aes As Aes = Aes.Create()
        aes.Mode = CipherMode.OFB ' Violation
    End Sub
    
    ' Violation: CipherMode.ECB usage
    Public Sub UseCipherModeECB()
        Dim mode As CipherMode = CipherMode.ECB ' Violation
    End Sub
    
    ' Violation: CipherMode.OFB usage
    Public Sub UseCipherModeOFB()
        Dim mode As CipherMode = CipherMode.OFB ' Violation
    End Sub
    
    ' Violation: Multiple unsafe cipher modes in one method
    Public Sub MultipleUnsafeCipherModes()
        Dim aes1 As Aes = Aes.Create()
        Dim aes2 As Aes = Aes.Create()
        
        aes1.Mode = CipherMode.ECB ' Violation
        aes2.Mode = CipherMode.OFB ' Violation
    End Sub
    
    ' Violation: Unsafe cipher modes in loop
    Public Sub UnsafeCipherModesInLoop()
        For i As Integer = 0 To 5
            Dim aes As Aes = Aes.Create()
            aes.Mode = CipherMode.ECB ' Violation
        Next
    End Sub
    
    ' Violation: Unsafe cipher modes in conditional
    Public Sub ConditionalUnsafeCipherModes(useECB As Boolean)
        Dim aes As Aes = Aes.Create()
        If useECB Then
            aes.Mode = CipherMode.ECB ' Violation
        Else
            aes.Mode = CipherMode.OFB ' Violation
        End If
    End Sub
    
    ' Violation: Unsafe cipher modes in Try-Catch
    Public Sub UnsafeCipherModesInTryCatch()
        Try
            Dim aes As Aes = Aes.Create()
            aes.Mode = CipherMode.ECB ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Unsafe cipher mode with field
    Private aesField As Aes = Aes.Create()
    
    Public Sub FieldUnsafeCipherMode()
        aesField.Mode = CipherMode.OFB ' Violation
    End Sub
    
    ' Violation: Unsafe cipher mode with parameter
    Public Sub ParameterUnsafeCipherMode(algorithm As Aes)
        algorithm.Mode = CipherMode.ECB ' Violation
    End Sub
    
    ' Violation: Unsafe cipher mode in method return context
    Public Function CreateInsecureAes() As Aes
        Dim aes As Aes = Aes.Create()
        aes.Mode = CipherMode.OFB ' Violation
        Return aes
    End Function
    
    ' Non-violation: Safe cipher modes (should not be detected)
    Public Sub SafeCipherModes()
        Dim aes As Aes = Aes.Create()
        aes.Mode = CipherMode.CBC ' No violation - safe mode
        aes.Mode = CipherMode.CFB ' No violation - safe mode
        aes.Mode = CipherMode.CTS ' No violation - safe mode
    End Sub
    
    ' Non-violation: Comments and strings mentioning unsafe cipher modes
    Public Sub CommentsAndStrings()
        ' This is about CipherMode.ECB and CipherMode.OFB
        Dim message As String = "Do not use ECB or OFB cipher modes"
        Console.WriteLine("Unsafe cipher modes like ECB should be avoided")
    End Sub
    
    ' Violation: Unsafe cipher modes in Select Case
    Public Sub UnsafeCipherModesInSelectCase(option As Integer)
        Dim aes As Aes = Aes.Create()
        Select Case option
            Case 1
                aes.Mode = CipherMode.ECB ' Violation
            Case 2
                aes.Mode = CipherMode.OFB ' Violation
        End Select
    End Sub
    
    ' Violation: Unsafe cipher modes in Using statement
    Public Sub UnsafeCipherModesInUsing()
        Using aes As Aes = Aes.Create()
            aes.Mode = CipherMode.ECB ' Violation
        End Using
    End Sub
    
    ' Violation: Unsafe cipher modes in While loop
    Public Sub UnsafeCipherModesInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim aes As Aes = Aes.Create()
            aes.Mode = CipherMode.OFB ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: Unsafe cipher mode with variable assignment
    Public Sub UnsafeCipherModeWithVariableAssignment()
        Dim mode As CipherMode
        mode = CipherMode.ECB ' Violation
    End Sub
    
    ' Violation: Unsafe cipher mode in array initialization
    Public Sub UnsafeCipherModesInArrayInitialization()
        Dim modes() As CipherMode = {
            CipherMode.ECB, ' Violation
            CipherMode.OFB ' Violation
        }
    End Sub
    
    ' Violation: Unsafe cipher mode in collection initialization
    Public Sub UnsafeCipherModesInCollectionInitialization()
        Dim modeList As New List(Of CipherMode) From {
            CipherMode.ECB, ' Violation
            CipherMode.OFB ' Violation
        }
    End Sub
    
    ' Violation: Unsafe cipher mode with conditional operator
    Public Sub UnsafeCipherModeWithConditionalOperator(useECB As Boolean)
        Dim mode = If(useECB, CipherMode.ECB, CipherMode.OFB) ' Violation
    End Sub
    
    ' Violation: Unsafe cipher mode in lambda expression
    Public Sub UnsafeCipherModeInLambda()
        Dim getMode = Function() CipherMode.ECB ' Violation
    End Sub
    
    ' Violation: Unsafe cipher mode in property
    Public ReadOnly Property UnsafeCipherMode As CipherMode
        Get
            Return CipherMode.OFB ' Violation
        End Get
    End Property
    
    ' Violation: Different encryption algorithms with unsafe modes
    Public Sub DifferentAlgorithmsWithUnsafeModes()
        Dim des As DES = DES.Create()
        Dim rc2 As RC2 = RC2.Create()
        
        des.Mode = CipherMode.ECB ' Violation
        rc2.Mode = CipherMode.OFB ' Violation
    End Sub
    
    ' Violation: Unsafe cipher mode with object initializer
    Public Sub UnsafeCipherModeWithObjectInitializer()
        Dim aes As New AesCryptoServiceProvider() With {
            .Mode = CipherMode.ECB ' Violation
        }
    End Sub
    
    ' Violation: Mixed case cipher modes
    Public Sub MixedCaseUnsafeCipherModes()
        Dim aes As Aes = Aes.Create()
        ' Note: VB.NET is case-insensitive, but testing various patterns
        aes.Mode = CipherMode.ECB ' Violation
        aes.Mode = CipherMode.OFB ' Violation
    End Sub

End Class
