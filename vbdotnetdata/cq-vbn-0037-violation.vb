' Test file for cq-vbn-0037: Types should not extend certain base types
' Rule should detect classes inheriting from inappropriate base types

Imports System

' Violation 1: Class inheriting from ApplicationException (deprecated)
Public Class CustomApplicationException
    Inherits ApplicationException
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
End Class

' Violation 2: Class inheriting from SystemException (should inherit from Exception)
Friend Class CustomSystemException
    Inherits SystemException
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
End Class

' Violation 3: Class inheriting directly from Exception (usually okay, but pattern detects it)
Public Class CustomException
    Inherits Exception
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
End Class

' Violation 4: Class trying to inherit from Enum (invalid)
Public Class CustomEnum
    Inherits Enum
    
    ' This would be a compile error
    Public Value As Integer
    
End Class

' Violation 5: Class trying to inherit from ValueType (invalid)
Friend Class CustomValueType
    Inherits ValueType
    
    ' This would be a compile error
    Public Data As String
    
End Class

' Violation 6: Class trying to inherit from Delegate (invalid)
Public Class CustomDelegate
    Inherits Delegate
    
    ' This would be a compile error
    Public Sub New()
    End Sub
    
End Class

' Violation 7: Class trying to inherit from MulticastDelegate (invalid)
Public Class CustomMulticastDelegate
    Inherits MulticastDelegate
    
    ' This would be a compile error
    Public Sub New()
    End Sub
    
End Class

' This should NOT be detected - class inheriting from proper base class
Public Class ValidCustomException
    Inherits ArgumentException
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
End Class

' This should NOT be detected - class inheriting from custom base class
Public Class DerivedClass
    Inherits BaseClass
    
    Public Overrides Sub ProcessData()
        Console.WriteLine("Processing in derived class")
    End Sub
    
End Class

Public MustInherit Class BaseClass
    
    Public MustOverride Sub ProcessData()
    
End Class

' This should NOT be detected - class without inheritance
Public Class StandaloneClass
    
    Public Property Name As String
    
    Public Sub DoWork()
        Console.WriteLine("Doing work")
    End Sub
    
End Class
