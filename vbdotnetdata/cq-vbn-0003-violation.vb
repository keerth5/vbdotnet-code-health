' Test file for cq-vbn-0003: Use generic event handler instances
' This file should trigger violations for non-generic event delegates

Imports System

' Custom delegate types (violations)
Public Delegate Sub CustomDelegate(sender As Object, args As EventArgs)
Public Delegate Sub DataChangedDelegate(data As String)
Public Delegate Function ValidationDelegate(value As String) As Boolean

Public Class EventPublisher
    
    ' Violation: Event using custom delegate
    Public Event DataChanged As CustomDelegate
    
    ' Violation: Event using another custom delegate
    Public Event ValidationRequired As ValidationDelegate
    
    ' Violation: Event using simple delegate
    Protected Event InternalChanged As DataChangedDelegate
    
    ' Violation: Private event with custom delegate
    Private Event PrivateChanged As CustomDelegate
    
    ' Violation: Friend event with custom delegate
    Friend Event FriendChanged As CustomDelegate
    
    ' Non-violation: Event using generic EventHandler(Of T) (should not trigger)
    Public Event PropertyChanged As EventHandler(Of PropertyChangedEventArgs)
    
    ' Non-violation: Event using standard EventHandler (should not trigger)
    Public Event StandardEvent As EventHandler
    
    Public Sub TriggerEvents()
        RaiseEvent DataChanged(Me, EventArgs.Empty)
        RaiseEvent ValidationRequired("test")
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("TestProperty"))
    End Sub
End Class

Public Class PropertyChangedEventArgs
    Inherits EventArgs
    
    Public Property PropertyName As String
    
    Public Sub New(propertyName As String)
        Me.PropertyName = propertyName
    End Sub
End Class
