' Test file for cq-vbn-0093: Type names should not match namespaces
' Rule should detect type names that conflict with common namespaces

Imports System

' Violation 1: Class name conflicts with System namespace
Public Class System
    Public Property Name As String
End Class

' Violation 2: Class name conflicts with Microsoft namespace
Public Class Microsoft
    Public Property Product As String
End Class

' Violation 3: Class name conflicts with Collections namespace
Friend Class Collections
    Public Property Items As String()
End Class

' Violation 4: Class name conflicts with Generic namespace
Public Class Generic
    Public Property TypeName As String
End Class

' Violation 5: Class name conflicts with Text namespace
Public Class Text
    Public Property Content As String
End Class

' Violation 6: Class name conflicts with IO namespace
Friend Class IO
    Public Property FilePath As String
End Class

' Violation 7: Class name conflicts with Net namespace
Public Class Net
    Public Property Url As String
End Class

' Violation 8: Class name conflicts with Threading namespace
Public Class Threading
    Public Property ThreadCount As Integer
End Class

' Violation 9: Class name conflicts with Xml namespace
Friend Class Xml
    Public Property Document As String
End Class

' Violation 10: Class name conflicts with Data namespace
Public Class Data
    Public Property ConnectionString As String
End Class

' Violation 11: Class name conflicts with Web namespace
Public Class Web
    Public Property ServerName As String
End Class

' Violation 12: Class name conflicts with Windows namespace
Friend Class Windows
    Public Property Version As String
End Class

' Violation 13: Class name conflicts with Forms namespace
Public Class Forms
    Public Property FormCount As Integer
End Class

' Violation 14: Class name conflicts with Drawing namespace
Public Class Drawing
    Public Property Canvas As String
End Class

' Violation 15: Module name conflicts with System namespace
Public Module System
    Public Sub ProcessData()
        Console.WriteLine("Processing data")
    End Sub
End Module

' Violation 16: Module name conflicts with Collections namespace
Friend Module Collections
    Public Sub ManageItems()
        Console.WriteLine("Managing items")
    End Sub
End Module

' This should NOT be detected - proper class naming
Public Class UserManager
    Public Property Users As String()
End Class

' This should NOT be detected - proper class naming
Friend Class DatabaseHelper
    Public Property ConnectionString As String
End Class

' This should NOT be detected - proper class naming
Public Class FileProcessor
    Public Property FileName As String
End Class

' This should NOT be detected - proper module naming
Public Module UtilityFunctions
    Public Sub LogMessage(message As String)
        Console.WriteLine(message)
    End Sub
End Module

' This should NOT be detected - proper module naming
Friend Module StringHelpers
    Public Function FormatString(input As String) As String
        Return input.ToUpper()
    End Function
End Module

' Violation 17: Class name conflicts with Generic namespace (case variation)
Public Class generic
    Public Property Items As Object()
End Class

' Violation 18: Class name conflicts with Threading namespace (case variation)
Friend Class threading
    Public Property IsRunning As Boolean
End Class

' This should NOT be detected - doesn't match common namespace names
Public Class CustomProcessor
    Public Property ProcessId As Integer
End Class

' This should NOT be detected - doesn't match common namespace names
Friend Class BusinessLogic
    Public Property Rules As String()
End Class

' Violation 19: Module conflicts with Text namespace
Public Module Text
    Public Function ProcessText(input As String) As String
        Return input.Trim()
    End Function
End Module

' Violation 20: Module conflicts with Data namespace
Friend Module Data
    Public Sub SaveData()
        Console.WriteLine("Saving data")
    End Sub
End Module
