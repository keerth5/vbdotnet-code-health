' Test file for cq-vbn-0217: Do not use deprecated security protocols
' This rule detects usage of deprecated security protocols like SSL3, TLS, and TLS11

Imports System
Imports System.Net

Public Class DeprecatedSecurityProtocolViolations
    
    ' Violation: SecurityProtocolType.Ssl3 usage
    Public Sub UseSSL3()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 ' Violation
    End Sub
    
    ' Violation: SecurityProtocolType.Tls usage
    Public Sub UseTLS()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
    End Sub
    
    ' Violation: SecurityProtocolType.Tls11 usage
    Public Sub UseTLS11()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 ' Violation
    End Sub
    
    ' Violation: Multiple deprecated security protocols
    Public Sub MultipleDeprecatedProtocols()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 ' Violation
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 ' Violation
    End Sub
    
    ' Violation: Deprecated security protocols in loop
    Public Sub DeprecatedProtocolsInLoop()
        For i As Integer = 0 To 3
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 ' Violation
        Next
    End Sub
    
    ' Violation: Deprecated security protocols in conditional
    Public Sub ConditionalDeprecatedProtocols(useOldProtocol As Boolean)
        If useOldProtocol Then
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
        Else
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 ' Violation
        End If
    End Sub
    
    ' Violation: Deprecated security protocols in Try-Catch
    Public Sub DeprecatedProtocolsInTryCatch()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 ' Violation
        Catch ex As Exception
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
        End Try
    End Sub
    
    ' Violation: Deprecated security protocol with variable assignment
    Public Sub DeprecatedProtocolWithVariableAssignment()
        Dim protocol As SecurityProtocolType = SecurityProtocolType.Ssl3 ' Violation
        ServicePointManager.SecurityProtocol = protocol
    End Sub
    
    ' Violation: Deprecated security protocol in method return context
    Public Function GetDeprecatedProtocol() As SecurityProtocolType
        Return SecurityProtocolType.Tls11 ' Violation
    End Function
    
    Public Sub UseDeprecatedProtocolFromMethod()
        ServicePointManager.SecurityProtocol = GetDeprecatedProtocol()
    End Sub
    
    ' Non-violation: Modern security protocols (should not be detected)
    Public Sub UseModernProtocols()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 ' No violation - modern protocol
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 ' No violation - modern protocol
    End Sub
    
    ' Non-violation: SystemDefault security protocol (should not be detected)
    Public Sub UseSystemDefault()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault ' No violation - system default
    End Sub
    
    ' Non-violation: Comments and strings mentioning deprecated protocols
    Public Sub CommentsAndStrings()
        ' This is about SecurityProtocolType.Ssl3, Tls, and Tls11
        Dim message As String = "Do not use SSL3, TLS, or TLS11"
        Console.WriteLine("Deprecated protocols like SSL3 should be avoided")
    End Sub
    
    ' Violation: Deprecated security protocols in Select Case
    Public Sub DeprecatedProtocolsInSelectCase(option As Integer)
        Select Case option
            Case 1
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 ' Violation
            Case 2
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
            Case 3
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 ' Violation
        End Select
    End Sub
    
    ' Violation: Deprecated security protocols in Using statement
    Public Sub DeprecatedProtocolsInUsing()
        Using client As New System.Net.Http.HttpClient()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 ' Violation
        End Using
    End Sub
    
    ' Violation: Deprecated security protocols in While loop
    Public Sub DeprecatedProtocolsInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: Deprecated security protocol with constant
    Private Const OLD_PROTOCOL As SecurityProtocolType = SecurityProtocolType.Tls11 ' Violation
    
    Public Sub DeprecatedProtocolWithConstant()
        ServicePointManager.SecurityProtocol = OLD_PROTOCOL
    End Sub
    
    ' Violation: Deprecated security protocol in array initialization
    Public Sub DeprecatedProtocolsInArrayInitialization()
        Dim protocols() As SecurityProtocolType = {
            SecurityProtocolType.Ssl3, ' Violation
            SecurityProtocolType.Tls, ' Violation
            SecurityProtocolType.Tls11 ' Violation
        }
    End Sub
    
    ' Violation: Deprecated security protocol in collection initialization
    Public Sub DeprecatedProtocolsInCollectionInitialization()
        Dim protocolList As New List(Of SecurityProtocolType) From {
            SecurityProtocolType.Ssl3, ' Violation
            SecurityProtocolType.Tls, ' Violation
            SecurityProtocolType.Tls11 ' Violation
        }
    End Sub
    
    ' Violation: Deprecated security protocol with conditional operator
    Public Sub DeprecatedProtocolWithConditionalOperator(useSSL3 As Boolean)
        ServicePointManager.SecurityProtocol = If(useSSL3, SecurityProtocolType.Ssl3, SecurityProtocolType.Tls) ' Violation
    End Sub
    
    ' Violation: Deprecated security protocol in lambda expression
    Public Sub DeprecatedProtocolInLambda()
        Dim getProtocol = Function() SecurityProtocolType.Tls11 ' Violation
        ServicePointManager.SecurityProtocol = getProtocol()
    End Sub
    
    ' Violation: Deprecated security protocol in property
    Public ReadOnly Property DeprecatedProtocol As SecurityProtocolType
        Get
            Return SecurityProtocolType.Ssl3 ' Violation
        End Get
    End Property
    
    Public Sub UseDeprecatedProtocolProperty()
        ServicePointManager.SecurityProtocol = DeprecatedProtocol
    End Sub
    
    ' Violation: Deprecated security protocol in constructor
    Public Sub New(useDeprecatedProtocol As Boolean)
        If useDeprecatedProtocol Then
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
        End If
    End Sub
    
    ' Violation: Deprecated security protocol with bitwise operations
    Public Sub DeprecatedProtocolWithBitwiseOperations()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls ' Violation
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 ' Violation (contains deprecated)
    End Sub
    
    ' Violation: Deprecated security protocol in event handler
    Public Sub OnNetworkEvent(sender As Object, e As EventArgs)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 ' Violation
    End Sub
    
    ' Violation: Deprecated security protocol in delegate
    Public Sub DeprecatedProtocolWithDelegate()
        Dim del As Action = Sub() ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 ' Violation
        del()
    End Sub
    
    ' Violation: Deprecated security protocol in static context
    Shared Sub New()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls ' Violation
    End Sub
    
    ' Violation: Deprecated security protocol with method parameters
    Public Sub SetSecurityProtocol(protocol As SecurityProtocolType)
        ServicePointManager.SecurityProtocol = protocol
    End Sub
    
    Public Sub UseDeprecatedProtocolAsParameter()
        SetSecurityProtocol(SecurityProtocolType.Ssl3) ' Violation
        SetSecurityProtocol(SecurityProtocolType.Tls) ' Violation
        SetSecurityProtocol(SecurityProtocolType.Tls11) ' Violation
    End Sub
    
    ' Violation: Deprecated security protocol in field
    Private deprecatedProtocolField As SecurityProtocolType = SecurityProtocolType.Ssl3 ' Violation
    
    Public Sub UseDeprecatedProtocolField()
        ServicePointManager.SecurityProtocol = deprecatedProtocolField
    End Sub

End Class
