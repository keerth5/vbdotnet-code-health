' Test file for cq-vbn-0006: Collections should implement generic interface
' This file should trigger violations for collection classes implementing non-generic interfaces

Imports System
Imports System.Collections

' Violation: Collection class implementing non-generic ICollection (single line pattern)
Public Class CustomCollection : Implements ICollection : End Class

' Violation: Another collection class implementing ICollection (single line pattern)
Protected Class MyCollection : Implements ICollection : End Class

' Violation: DataCollection implementing ICollection
Friend Class DataCollection : Implements ICollection : End Class

' Violation: ItemCollection implementing ICollection  
Public Class ItemCollection : Implements ICollection : End Class

' Non-violation: Collection class implementing generic interface (should not trigger)
Public Class GenericCustomCollection(Of T)
    Implements ICollection(Of T)
    
    Private items As New List(Of T)
    
    ' Generic interface implementation
    Public ReadOnly Property Count As Integer Implements ICollection(Of T).Count
        Get
            Return items.Count
        End Get
    End Property
    
    ' Other interface members...
End Class

' Non-violation: Regular class not implementing collection interfaces (should not trigger)
Public Class RegularClass
    Private data As String
    
    Public Sub ProcessData()
        ' Regular processing
    End Sub
End Class
