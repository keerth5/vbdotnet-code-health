' Test file for cq-vbn-0000: Do not declare static members on generic types
' This file should trigger violations for static members in generic types

Imports System

' Violation: Generic class with static member (single line pattern)
Public Class GenericRepository(Of T) : Public Shared Function GetDefaultInstance() As GenericRepository(Of T) : Return New GenericRepository(Of T)() : End Function : End Class

' Violation: Generic class with static property (single line pattern)  
Public Class GenericCache(Of T) : Public Shared Property DefaultTimeout As Integer = 30 : End Class

' Violation: Generic structure with static member (single line pattern)
Public Structure GenericContainer(Of T) : Public Shared Function CreateEmpty() As GenericContainer(Of T) : Return New GenericContainer(Of T)() : End Function : End Structure

' Violation: Generic class with static sub (single line pattern)
Protected Class GenericProcessor(Of T) : Public Shared Sub Initialize() : End Sub : End Class

' Violation: Generic structure with static member
Public Structure GenericContainer(Of T)
    Private value As T
    
    ' This should trigger violation - shared member in generic structure
    Public Shared Function CreateEmpty() As GenericContainer(Of T)
        Return New GenericContainer(Of T)()
    End Function
End Structure

' Non-violation: Non-generic class with static member (should not trigger)
Public Class NonGenericRepository
    Public Shared Function GetInstance() As NonGenericRepository
        Return New NonGenericRepository()
    End Function
End Class
