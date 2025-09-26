' Test file for cq-vbn-0259: Initialize value type static fields inline
' Rule should detect: Value type static fields initialized in static constructor

Public Structure BadValueType1
    
    ' VIOLATION: Static constructor with static field initialization
    Public Shared Counter As Integer
    Public Shared IsActive As Boolean
    Public Shared Rate As Double
    
    Shared Sub New()
        Counter = 0
        IsActive = False
        Rate = 0.0
    End Sub
    
End Structure

Public Class BadClass1
    
    ' VIOLATION: Static constructor with value type static fields
    Public Shared Count As Integer
    Public Shared Flag As Boolean
    Public Shared Value As Decimal
    
    Shared Sub New()
        Count = 100
        Flag = True
        Value = 99.99D
    End Sub
    
End Class

Public Structure BadValueType2
    
    ' VIOLATION: Static constructor initializing Date field
    Public Shared CreatedDate As Date
    
    Shared Sub New()
        CreatedDate = Date.Now
    End Sub
    
End Structure

' GOOD: Value type with inline field initialization - should NOT be flagged
Public Structure GoodValueType1
    
    Public Shared Counter As Integer = 0
    Public Shared IsActive As Boolean = False
    Public Shared Rate As Double = 0.0
    
End Structure

' GOOD: Class without static constructor - should NOT be flagged
Public Class GoodClass1
    
    Public Shared Count As Integer = 100
    Public Shared Flag As Boolean = True
    Public Shared Value As Decimal = 99.99D
    
End Class

' GOOD: Static constructor with reference types - should NOT be flagged
Public Class GoodClass2
    
    Public Shared Name As String
    Public Shared Items As List(Of String)
    
    Shared Sub New()
        Name = "Default"
        Items = New List(Of String)()
    End Sub
    
End Class

' GOOD: Instance constructor - should NOT be flagged
Public Structure GoodValueType2
    
    Public Value As Integer
    
    Public Sub New(initialValue As Integer)
        Value = initialValue
    End Sub
    
End Structure
