' Test file for cq-vbn-0002: Do not expose generic lists
' This file should trigger violations for exposing List(Of T) in public APIs

Imports System
Imports System.Collections.Generic

Public Class CustomerService
    
    ' Violation: Public function returning List(Of T)
    Public Function GetAllCustomers() As List(Of Customer)
        Return New List(Of Customer)()
    End Function
    
    ' Violation: Protected function returning List(Of T)
    Protected Function GetInternalCustomers() As List(Of Customer)
        Return New List(Of Customer)()
    End Function
    
    ' Violation: Public property of type List(Of T)
    Public Property CustomerList As List(Of Customer)
    
    ' Violation: Protected property of type List(Of T)
    Protected Property InternalList As List(Of Customer)
    
    ' Violation: Public ReadOnly property returning List(Of T)
    Public ReadOnly Property ActiveCustomers() As List(Of Customer)
        Get
            Return New List(Of Customer)()
        End Get
    End Property
    
    ' Non-violation: Private function returning List(Of T) (should not trigger)
    Private Function GetPrivateList() As List(Of Customer)
        Return New List(Of Customer)()
    End Function
    
    ' Non-violation: Friend function returning List(Of T) (should not trigger)
    Friend Function GetFriendList() As List(Of Customer)
        Return New List(Of Customer)()
    End Function
    
    ' Non-violation: Public function returning IList(Of T) (should not trigger)
    Public Function GetCustomersAsInterface() As IList(Of Customer)
        Return New List(Of Customer)()
    End Function
    
    ' Non-violation: Public function returning ReadOnlyCollection (should not trigger)
    Public Function GetReadOnlyCustomers() As ReadOnlyCollection(Of Customer)
        Return New ReadOnlyCollection(Of Customer)(New List(Of Customer)())
    End Function
End Class

Public Class Customer
    Public Property Id As Integer
    Public Property Name As String
End Class
