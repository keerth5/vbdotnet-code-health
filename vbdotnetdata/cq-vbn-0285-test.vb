' VB.NET test file for cq-vbn-0285: Provide correct 'enum' argument to 'Enum.HasFlag'
' This rule detects HasFlag calls with wrong enum type

Imports System

<Flags>
Public Enum FilePermissions
    Read = 1
    Write = 2
    Execute = 4
End Enum

<Flags>
Public Enum NetworkPermissions
    Connect = 1
    Listen = 2
    Accept = 4
End Enum

Public Class BadEnumHasFlag
    ' BAD: HasFlag with wrong enum type
    Public Sub TestWrongEnumType()
        Dim filePerms As FilePermissions = FilePermissions.Read Or FilePermissions.Write
        
        ' Violation: Using NetworkPermissions enum with FilePermissions variable
        If filePerms.HasFlag(NetworkPermissions.Connect) Then
            Console.WriteLine("Wrong enum type")
        End If
        
        ' Violation: Another wrong enum type usage
        Dim netPerms As NetworkPermissions = NetworkPermissions.Listen
        If netPerms.HasFlag(FilePermissions.Read) Then
            Console.WriteLine("Wrong enum type")
        End If
    End Sub
    
    ' GOOD: HasFlag with correct enum type
    Public Sub TestCorrectEnumType()
        Dim filePerms As FilePermissions = FilePermissions.Read Or FilePermissions.Write
        
        ' Good: Using same enum type
        If filePerms.HasFlag(FilePermissions.Read) Then
            Console.WriteLine("Correct enum type")
        End If
        
        ' Good: Another correct usage
        Dim netPerms As NetworkPermissions = NetworkPermissions.Listen
        If netPerms.HasFlag(NetworkPermissions.Connect) Then
            Console.WriteLine("Correct enum type")
        End If
    End Sub
End Class
