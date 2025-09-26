' Test file for cq-vbn-0082: Identifiers should differ by more than case
' Rule should detect identifiers that differ only by case which can cause confusion

Imports System

Namespace TestNamespace
    
    ' Violation 1: Classes differing only by case
    Public Class UserData
        Public Property Name As String
    End Class
    
    ' Violation 2: Class differing only by case from above
    Public Class userData
        Public Property Value As String
    End Class
    
    ' Violation 3: Another case variation
    Public Class USERDATA
        Public Property Id As Integer
    End Class
    
    ' This should NOT be detected - clearly different names
    Public Class CustomerData
        Public Property CustomerName As String
    End Class
    
    Public Class OrderData
        Public Property OrderId As Integer
    End Class
    
End Namespace

Namespace AnotherNamespace
    
    ' Violation 4: Module with case-only difference
    Public Module DataProcessor
        Public Sub ProcessData()
            Console.WriteLine("Processing")
        End Sub
    End Module
    
    ' Violation 5: Module differing only by case
    Public Module dataProcessor
        Public Sub HandleData()
            Console.WriteLine("Handling")
        End Sub
    End Module
    
    ' This should NOT be detected - different names
    Public Module FileProcessor
        Public Sub ProcessFiles()
            Console.WriteLine("Processing files")
        End Sub
    End Module
    
End Namespace

Namespace CaseTestNamespace
    
    ' Violation 6: Friend class with case difference
    Friend Class ConfigManager
        Public Property Settings As String
    End Class
    
    ' Violation 7: Friend class differing only by case
    Friend Class configManager
        Public Property Options As String
    End Class
    
    ' Violation 8: Public class with case difference
    Public Class ServiceHandler
        Public Sub StartService()
            Console.WriteLine("Starting service")
        End Sub
    End Class
    
    ' Violation 9: Public class differing only by case
    Public Class serviceHandler
        Public Sub StopService()
            Console.WriteLine("Stopping service")
        End Sub
    End Class
    
    ' This should NOT be detected - different functionality implied by name
    Public Class ServiceManager
        Public Sub ManageServices()
            Console.WriteLine("Managing services")
        End Sub
    End Class
    
End Namespace

' Violation 10: Top-level classes with case differences
Public Class EventHandler
    Public Sub HandleEvent()
        Console.WriteLine("Handling event")
    End Sub
End Class

' Violation 11: Class differing only by case
Public Class eventHandler
    Public Sub ProcessEvent()
        Console.WriteLine("Processing event")
    End Sub
End Class

' Violation 12: Module with case difference
Public Module StringHelper
    Public Function FormatString(input As String) As String
        Return input.ToUpper()
    End Function
End Module

' Violation 13: Module differing only by case
Public Module stringHelper
    Public Function ParseString(input As String) As String
        Return input.ToLower()
    End Function
End Module

' This should NOT be detected - clearly different purpose
Public Module MathHelper
    Public Function Add(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
End Module

' Violation 14: Friend module with case difference
Friend Module LogManager
    Public Sub WriteLog(message As String)
        Console.WriteLine("Log: " & message)
    End Sub
End Module

' Violation 15: Friend module differing only by case
Friend Module logManager
    Public Sub ReadLog() As String
        Return "Log content"
    End Sub
End Module
