' Test file for cq-vbn-0276: Mark all non-serializable fields
' Rule should detect: Serializable classes with non-serializable fields not marked with NonSerialized

Imports System
Imports System.Threading
Imports System.IO

' VIOLATION: Serializable class with Thread field not marked NonSerialized
<Serializable>
Public Class BadSerializableClass1
    
    Private data As String
    Private workerThread As Thread  ' Should be marked <NonSerialized>
    
    Public Sub New()
        data = "test"
        workerThread = New Thread(AddressOf DoWork)
    End Sub
    
    Private Sub DoWork()
        ' Background work
    End Sub
    
End Class

' VIOLATION: Serializable class with Timer field not marked NonSerialized
<Serializable>
Friend Class BadSerializableClass2
    
    Private name As String
    Protected timer As Timer  ' Should be marked <NonSerialized>
    
    Public Sub New(n As String)
        name = n
        timer = New Timer(AddressOf TimerCallback, Nothing, 1000, 1000)
    End Sub
    
    Private Sub TimerCallback(state As Object)
        ' Timer callback
    End Sub
    
End Class

' VIOLATION: Serializable class with FileStream field not marked NonSerialized
<Serializable>
Public Class BadSerializableClass3
    
    Private id As Integer
    Private fileStream As FileStream  ' Should be marked <NonSerialized>
    
    Public Sub New(identifier As Integer)
        id = identifier
        fileStream = New FileStream("temp.txt", FileMode.Create)
    End Sub
    
End Class

' GOOD: Serializable class with non-serializable fields properly marked - should NOT be flagged
<Serializable>
Public Class GoodSerializableClass1
    
    Private data As String
    <NonSerialized>
    Private workerThread As Thread
    
    Public Sub New()
        data = "test"
        workerThread = New Thread(AddressOf DoWork)
    End Sub
    
    Private Sub DoWork()
        ' Background work
    End Sub
    
End Class

' GOOD: Serializable class with only serializable fields - should NOT be flagged
<Serializable>
Public Class GoodSerializableClass2
    
    Private name As String
    Private value As Integer
    Private flag As Boolean
    
    Public Sub New(n As String, v As Integer)
        name = n
        value = v
        flag = True
    End Sub
    
End Class

' GOOD: Non-serializable class - should NOT be flagged
Public Class NonSerializableClass
    
    Private thread As Thread
    Private timer As Timer
    
    Public Sub New()
        thread = New Thread(AddressOf DoWork)
        timer = New Timer(AddressOf TimerCallback, Nothing, 1000, 1000)
    End Sub
    
    Private Sub DoWork()
        ' Background work
    End Sub
    
    Private Sub TimerCallback(state As Object)
        ' Timer callback
    End Sub
    
End Class
