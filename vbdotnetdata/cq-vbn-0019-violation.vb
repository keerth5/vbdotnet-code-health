' Test file for cq-vbn-0019: Implement standard exception constructors
' Rule should detect custom exceptions missing standard constructors

Imports System

' Violation 1: Custom exception with only parameterless constructor
Public Class CustomValidationException
    Inherits Exception
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    ' Missing: New(message As String)
    ' Missing: New(message As String, innerException As Exception)
End Class

' Violation 2: Custom exception with only message constructor
Protected Class BusinessLogicException
    Inherits Exception
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
    ' Missing: New()
    ' Missing: New(message As String, innerException As Exception)
End Class

' Violation 3: Custom exception with only inner exception constructor
Friend Class DataAccessException
    Inherits Exception
    
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
    
    ' Missing: New()
    ' Missing: New(message As String)
End Class

' Violation 4: Custom exception with partial constructors
Private Class ConfigurationException
    Inherits Exception
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
    ' Missing: New(message As String, innerException As Exception)
End Class

' This should NOT be detected - complete exception with all standard constructors
Public Class CompleteCustomException
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

' This should NOT be detected - not an exception class
Public Class RegularClass
    Public Sub New()
    End Sub
End Class
