' Test file for cq-vbn-0017: Use events where appropriate
' Rule should detect methods that should use events for extensibility

Imports System

Public Class EventPublisher
    
    ' Violation 1: Public method that should use events
    Public Sub OnDataChanged()
        ' Should use events instead of direct method calls
        Console.WriteLine("Data changed")
        ' Missing: RaiseEvent DataChanged()
    End Sub
    
    ' Violation 2: Protected method that should use events
    Protected Sub OnItemAdded()
        Console.WriteLine("Item added")
        ' Missing: RaiseEvent ItemAdded()
    End Sub
    
    ' Violation 3: Public method with event-like name
    Public Sub OnUserLoggedIn()
        ' Should raise an event
        Console.WriteLine("User logged in")
    End Sub
    
    ' Violation 4: Protected method for notifications
    Protected Sub OnStatusChanged()
        ' Should use event mechanism
        UpdateUI()
    End Sub
    
    ' Violation 5: Public notification method
    Public Sub OnProcessCompleted()
        ' Should raise event instead
        NotifySubscribers()
    End Sub
    
    ' This should NOT be detected - regular method without On prefix
    Public Sub ProcessData()
        Console.WriteLine("Processing data")
    End Sub
    
    ' This should NOT be detected - private method
    Private Sub OnInternalEvent()
        Console.WriteLine("Internal event")
    End Sub
    
    ' This should NOT be detected - method with parameters
    Public Sub OnItemChanged(item As Object)
        Console.WriteLine("Item changed: " & item.ToString())
    End Sub
    
    Private Sub UpdateUI()
        ' Helper method
    End Sub
    
    Private Sub NotifySubscribers()
        ' Helper method
    End Sub
End Class
