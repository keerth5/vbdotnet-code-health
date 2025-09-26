' Test file for cq-vbn-0096: Use Literals Where Appropriate
' Rule should detect Shared ReadOnly fields that could be constants

Imports System

Public Class ConstantCandidates
    
    ' Violation 1: Shared ReadOnly field with compile-time constant value (integer)
    Public Shared ReadOnly MaxRetries As Integer = 3
    
    ' Violation 2: Shared ReadOnly field with compile-time constant value (string)
    Private Shared ReadOnly DefaultMessage As String = "Default message"
    
    ' Violation 3: Shared ReadOnly field with compile-time constant value (boolean)
    Protected Shared ReadOnly IsEnabled As Boolean = True
    
    ' Violation 4: Shared ReadOnly field with compile-time constant value (boolean)
    Friend Shared ReadOnly IsDebugMode As Boolean = False
    
    ' Violation 5: Shared ReadOnly field with compile-time constant value (string)
    Public Shared ReadOnly ApplicationName As String = "MyApplication"
    
    ' Violation 6: Shared ReadOnly field with compile-time constant value (integer)
    Private Shared ReadOnly BufferSize As Integer = 1024
    
    ' Violation 7: Shared ReadOnly field with compile-time constant value (double)
    Protected Shared ReadOnly Pi As Double = 3.14159
    
    ' Violation 8: Shared ReadOnly field with compile-time constant value (decimal)
    Friend Shared ReadOnly TaxRate As Decimal = 0.075
    
    ' Violation 9: Shared ReadOnly field with compile-time constant value (Nothing)
    Public Shared ReadOnly DefaultValue As Object = Nothing
    
    ' Violation 10: ReadOnly field without Shared (still a candidate)
    Public ReadOnly TimeoutSeconds As Integer = 30
    
    ' Violation 11: ReadOnly field with string literal
    Private ReadOnly ConfigSection As String = "AppSettings"
    
    ' Violation 12: ReadOnly field with boolean literal
    Protected ReadOnly AllowOverride As Boolean = True
    
    ' This should NOT be detected - field is not ReadOnly
    Public Shared MaxConnections As Integer = 10
    
    ' This should NOT be detected - field value is not compile-time constant
    Public Shared ReadOnly CurrentTime As DateTime = DateTime.Now
    
    ' This should NOT be detected - field value is computed
    Private Shared ReadOnly RandomValue As Integer = New Random().Next()
    
    ' This should NOT be detected - field is already a constant
    Public Const MaxLength As Integer = 255
    
    ' This should NOT be detected - field value is not a literal
    Protected Shared ReadOnly DatabaseConnection As String = GetConnectionString()
    
    Private Shared Function GetConnectionString() As String
        Return "Server=localhost;Database=Test"
    End Function
    
End Class

Public Class MoreConstantCandidates
    
    ' Violation 13: Shared ReadOnly with integer literal
    Public Shared ReadOnly PageSize As Integer = 25
    
    ' Violation 14: Shared ReadOnly with string literal
    Friend Shared ReadOnly ErrorPrefix As String = "ERROR: "
    
    ' Violation 15: Shared ReadOnly with boolean literal
    Private Shared ReadOnly UseCache As Boolean = False
    
    ' Violation 16: ReadOnly field with decimal literal
    Public ReadOnly DiscountRate As Decimal = 0.1
    
    ' Violation 17: ReadOnly field with string literal
    Protected ReadOnly LogFileName As String = "application.log"
    
    ' This should NOT be detected - field is not initialized with literal
    Public Shared ReadOnly Items As New List(Of String)
    
    ' This should NOT be detected - field value involves method call
    Friend Shared ReadOnly Version As String = GetVersion()
    
    Private Shared Function GetVersion() As String
        Return "1.0.0"
    End Function
    
End Class

Friend Class ThirdConstantExample
    
    ' Violation 18: Shared ReadOnly with integer literal
    Public Shared ReadOnly DefaultPort As Integer = 8080
    
    ' Violation 19: Shared ReadOnly with string literal
    Private Shared ReadOnly ServiceName As String = "MyService"
    
    ' Violation 20: Shared ReadOnly with boolean literal
    Protected Shared ReadOnly IsProduction As Boolean = False
    
    ' This should NOT be detected - field is mutable
    Public Shared Counter As Integer = 0
    
    ' This should NOT be detected - field is instance-level and computed
    Public ReadOnly InstanceId As Guid = Guid.NewGuid()
    
End Class

Public Class EdgeCases
    
    ' Violation 21: ReadOnly field with Nothing literal
    Public ReadOnly DefaultObject As Object = Nothing
    
    ' Violation 22: ReadOnly field with empty string literal
    Private ReadOnly EmptyString As String = ""
    
    ' Violation 23: ReadOnly field with zero literal
    Protected ReadOnly ZeroValue As Integer = 0
    
    ' This should NOT be detected - field uses constructor
    Public Shared ReadOnly EmptyArray As String() = New String() {}
    
    ' This should NOT be detected - field uses property
    Friend Shared ReadOnly CurrentDirectory As String = Environment.CurrentDirectory
    
End Class
