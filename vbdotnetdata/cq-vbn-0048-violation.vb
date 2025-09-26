' Test file for cq-vbn-0048: Do not declare non-constant fields in public APIs
' Rule should detect public fields that are not const or readonly

Imports System

Public Class ApiConstants
    
    ' Violation 1: Public shared field that should be const or readonly
    Public Shared MaxRetryCount As Integer = 3
    
    ' Violation 2: Public shared field with string value
    Public Shared DefaultConnectionString As String = "Server=localhost;Database=Test"
    
    ' This should NOT be detected - proper const field
    Public Const ApplicationName As String = "MyApplication"
    
    ' This should NOT be detected - proper readonly field
    Public Shared ReadOnly StartupTime As DateTime = DateTime.Now
    
    ' This should NOT be detected - private field
    Private Shared _internalCounter As Integer = 0
    
End Class

Public Structure ConfigurationSettings
    
    ' Violation 3: Public field in structure
    Public Shared TimeoutSeconds As Integer = 30
    
    ' Violation 4: Public shared field in structure
    Public Shared BufferSize As Integer = 1024
    
    ' This should NOT be detected - const field in structure
    Public Const MaxFileSize As Long = 1048576  ' 1MB
    
    ' This should NOT be detected - readonly field in structure
    Public Shared ReadOnly DefaultEncoding As System.Text.Encoding = System.Text.Encoding.UTF8
    
End Structure

Public Class DatabaseSettings
    
    ' Violation 5: Public shared field for connection timeout
    Public Shared ConnectionTimeout As Integer = 30
    
    ' Violation 6: Public shared field for retry policy
    Public Shared RetryPolicy As String = "Exponential"
    
    ' This should NOT be detected - proper const
    Public Const DefaultDatabase As String = "DefaultDB"
    
    ' This should NOT be detected - proper readonly
    Public Shared ReadOnly CreatedDate As DateTime = DateTime.Now
    
    ' This should NOT be detected - private shared field
    Private Shared _connectionPool As Object = Nothing
    
    ' This should NOT be detected - friend field (internal)
    Friend Shared InternalSetting As String = "Internal"
    
End Class

Public Class WebApiConfiguration
    
    ' Violation 7: Public shared field for API version
    Public Shared ApiVersion As String = "v1.0"
    
    ' Violation 8: Public shared field for base URL
    Public Shared BaseUrl As String = "https://api.example.com"
    
    ' This should NOT be detected - const field
    Public Const MaxRequestSize As Integer = 4194304  ' 4MB
    
    ' This should NOT be detected - readonly field
    Public Shared ReadOnly SupportedFormats As String() = {"json", "xml"}
    
End Class

Public Structure NetworkConfiguration
    
    ' Violation 9: Public shared field in structure
    Public Shared DefaultPort As Integer = 8080
    
    ' Violation 10: Public shared field for protocol
    Public Shared Protocol As String = "HTTP"
    
    ' This should NOT be detected - const in structure
    Public Const MaxConnections As Integer = 100
    
    ' This should NOT be detected - readonly in structure
    Public Shared ReadOnly LocalAddress As String = "127.0.0.1"
    
End Structure

' This should NOT be detected - class with only proper constants and readonly fields
Public Class ProperConstants
    
    Public Const ApplicationVersion As String = "2.0.1"
    Public Const MaxUsers As Integer = 1000
    Public Shared ReadOnly BuildDate As DateTime = DateTime.Parse("2023-01-01")
    Public Shared ReadOnly SupportedLanguages As String() = {"en", "es", "fr"}
    
    Private Shared _instanceCount As Integer = 0
    Friend Shared InternalFlag As Boolean = False
    
End Class
