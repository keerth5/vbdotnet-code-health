' Test file for cq-vbn-0042: Exceptions should be public
' Rule should detect custom exceptions that are not public

Imports System

' Violation 1: Private exception class
Private Class PrivateCustomException
    Inherits Exception
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
    
End Class

' Violation 2: Friend exception class
Friend Class FriendCustomException
    Inherits Exception
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
End Class

' Violation 3: Another private exception
Private Class ValidationException
    Inherits Exception
    
    Public Sub New(fieldName As String)
        MyBase.New($"Validation failed for field: {fieldName}")
    End Sub
    
End Class

' Violation 4: Friend exception with specific naming
Friend Class DataProcessingException
    Inherits Exception
    
    Public Property ErrorCode As Integer
    
    Public Sub New(errorCode As Integer, message As String)
        MyBase.New(message)
        Me.ErrorCode = errorCode
    End Sub
    
End Class

' This should NOT be detected - public exception (correct)
Public Class PublicCustomException
    Inherits Exception
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
    
End Class

' This should NOT be detected - public exception with custom properties
Public Class BusinessRuleException
    Inherits Exception
    
    Public Property RuleId As String
    Public Property Severity As Integer
    
    Public Sub New(ruleId As String, message As String)
        MyBase.New(message)
        Me.RuleId = ruleId
        Me.Severity = 1
    End Sub
    
End Class

' This should NOT be detected - regular class not inheriting from Exception
Private Class PrivateUtilityClass
    
    Public Shared Function FormatMessage(message As String) As String
        Return $"[{Date.Now}] {message}"
    End Function
    
End Class

' This should NOT be detected - friend class not inheriting from Exception
Friend Class ConfigurationManager
    
    Private _settings As Dictionary(Of String, String)
    
    Public Sub New()
        _settings = New Dictionary(Of String, String)()
    End Sub
    
    Public Sub SetSetting(key As String, value As String)
        _settings(key) = value
    End Sub
    
End Class
