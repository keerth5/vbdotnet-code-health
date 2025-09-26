' Test file for cq-vbn-0215: Do not disable SChannel use of strong crypto
' This rule detects disabling of strong cryptography in SChannel

Imports System

Public Class DisableStrongCryptoViolations
    
    ' Violation: Switch.System.Net.DontEnableSchUseStrongCrypto set to True
    Public Sub DisableStrongCryptoWithSwitch()
        Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
    End Sub
    
    ' Violation: AppContext.SetSwitch disabling strong crypto
    Public Sub DisableStrongCryptoWithAppContext()
        AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", True) ' Violation
    End Sub
    
    ' Violation: Multiple ways of disabling strong crypto
    Public Sub MultipleWaysToDisableStrongCrypto()
        Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
        AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", True) ' Violation
    End Sub
    
    ' Violation: Disabling strong crypto in loop
    Public Sub DisableStrongCryptoInLoop()
        For i As Integer = 0 To 3
            Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
        Next
    End Sub
    
    ' Violation: Disabling strong crypto in conditional
    Public Sub ConditionalDisableStrongCrypto(disable As Boolean)
        If disable Then
            Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
        End If
    End Sub
    
    ' Violation: Disabling strong crypto in Try-Catch
    Public Sub DisableStrongCryptoInTryCatch()
        Try
            AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", True) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Disabling strong crypto in method return context
    Public Function DisableStrongCryptoAndReturn() As Boolean
        Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
        Return True
    End Function
    
    ' Non-violation: Enabling strong crypto (should not be detected)
    Public Sub EnableStrongCrypto()
        Switch.System.Net.DontEnableSchUseStrongCrypto = False ' No violation - enabling strong crypto
        AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", False) ' No violation - enabling strong crypto
    End Sub
    
    ' Non-violation: Other switch configurations (should not be detected)
    Public Sub OtherSwitchConfigurations()
        Switch.System.Net.DisableIPv6 = True ' No violation - different switch
        AppContext.SetSwitch("Switch.System.Net.Http.UseSocketsHttpHandler", True) ' No violation - different switch
    End Sub
    
    ' Non-violation: Comments and strings mentioning the switch
    Public Sub CommentsAndStrings()
        ' This is about Switch.System.Net.DontEnableSchUseStrongCrypto = True
        Dim message As String = "Do not set DontEnableSchUseStrongCrypto to True"
        Console.WriteLine("Strong crypto should not be disabled")
    End Sub
    
    ' Violation: Disabling strong crypto in Select Case
    Public Sub DisableStrongCryptoInSelectCase(option As Integer)
        Select Case option
            Case 1
                Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
            Case 2
                AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", True) ' Violation
        End Select
    End Sub
    
    ' Violation: Disabling strong crypto in Using statement
    Public Sub DisableStrongCryptoInUsing()
        Using stream As New System.IO.MemoryStream()
            Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
        End Using
    End Sub
    
    ' Violation: Disabling strong crypto in While loop
    Public Sub DisableStrongCryptoInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", True) ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: Disabling strong crypto with variable assignment
    Public Sub DisableStrongCryptoWithVariableAssignment()
        Dim disableStrongCrypto As Boolean = True
        Switch.System.Net.DontEnableSchUseStrongCrypto = disableStrongCrypto ' Violation (assuming pattern matches True literal)
    End Sub
    
    ' Violation: Disabling strong crypto with constant
    Private Const DISABLE_STRONG_CRYPTO As Boolean = True
    
    Public Sub DisableStrongCryptoWithConstant()
        Switch.System.Net.DontEnableSchUseStrongCrypto = DISABLE_STRONG_CRYPTO ' Violation (if pattern matches True)
    End Sub
    
    ' Violation: AppContext.SetSwitch with different parameter formatting
    Public Sub DisableStrongCryptoWithDifferentFormatting()
        AppContext.SetSwitch( "Switch.System.Net.DontEnableSchUseStrongCrypto" , True ) ' Violation
    End Sub
    
    ' Violation: Disabling strong crypto in lambda expression
    Public Sub DisableStrongCryptoInLambda()
        Dim action = Sub() Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
        action()
    End Sub
    
    ' Violation: Disabling strong crypto in property
    Public WriteOnly Property DisableStrongCrypto As Boolean
        Set(value As Boolean)
            If value Then
                Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
            End If
        End Set
    End Property
    
    ' Violation: Disabling strong crypto in constructor
    Public Sub New(disableStrongCrypto As Boolean)
        If disableStrongCrypto Then
            Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
        End If
    End Sub
    
    ' Violation: Disabling strong crypto with method call result
    Public Sub DisableStrongCryptoWithMethodCall()
        Switch.System.Net.DontEnableSchUseStrongCrypto = GetDisableFlag() ' Violation (if GetDisableFlag returns True)
    End Sub
    
    Private Function GetDisableFlag() As Boolean
        Return True
    End Function
    
    ' Violation: Disabling strong crypto in static context
    Shared Sub New()
        Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
    End Sub
    
    ' Violation: Disabling strong crypto with conditional operator
    Public Sub DisableStrongCryptoWithConditionalOperator(condition As Boolean)
        Switch.System.Net.DontEnableSchUseStrongCrypto = If(condition, True, False) ' Violation
    End Sub
    
    ' Violation: AppContext.SetSwitch with string interpolation
    Public Sub DisableStrongCryptoWithStringInterpolation()
        Dim switchName As String = "Switch.System.Net.DontEnableSchUseStrongCrypto"
        AppContext.SetSwitch(switchName, True) ' Violation
    End Sub
    
    ' Violation: Disabling strong crypto in event handler
    Public Sub OnSomeEvent(sender As Object, e As EventArgs)
        Switch.System.Net.DontEnableSchUseStrongCrypto = True ' Violation
    End Sub
    
    ' Violation: Disabling strong crypto in delegate
    Public Sub DisableStrongCryptoWithDelegate()
        Dim del As Action = Sub() AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", True) ' Violation
        del()
    End Sub

End Class
