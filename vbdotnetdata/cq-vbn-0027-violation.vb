' Test file for cq-vbn-0027: Do not pass types by reference
' Rule should detect methods using ByRef parameters

Imports System

Public Class ReferenceParameterExamples
    
    ' Violation 1: Public method with ByRef parameter
    Public Sub SwapValues(ByRef a As Integer, ByRef b As Integer)
        Dim temp As Integer = a
        a = b
        b = temp
    End Sub
    
    ' Violation 2: Protected method with ByRef parameter
    Protected Function ProcessData(ByRef data As String) As Boolean
        data = data.ToUpper()
        Return True
    End Function
    
    ' Violation 3: Private method with ByRef parameter
    Private Sub UpdateValue(ByRef value As Double)
        value = value * 2
    End Sub
    
    ' Violation 4: Friend method with ByRef parameter
    Friend Sub ModifyArray(ByRef arr As Integer())
        For i As Integer = 0 To arr.Length - 1
            arr(i) = arr(i) * 2
        Next
    End Sub
    
    ' Violation 5: Method with multiple ByRef parameters
    Public Sub CalculateStats(data As Integer(), ByRef min As Integer, ByRef max As Integer, ByRef avg As Double)
        min = data.Min()
        max = data.Max()
        avg = data.Average()
    End Sub
    
    ' Violation 6: Function with ByRef parameter
    Public Function TryParse(input As String, ByRef result As Integer) As Boolean
        Return Integer.TryParse(input, result)
    End Function
    
    ' This should NOT be detected - method without ByRef parameters
    Public Sub ProcessNormally(value As Integer)
        Console.WriteLine("Processing: " & value)
    End Sub
    
    ' This should NOT be detected - method with ByVal parameters (default)
    Public Function Calculate(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    ' This should NOT be detected - method with explicit ByVal
    Public Sub DisplayMessage(ByVal message As String)
        Console.WriteLine(message)
    End Sub
    
End Class

Public Class AnotherClass
    
    ' Violation 7: Another method with ByRef
    Public Sub UpdateCounter(ByRef counter As Long)
        counter += 1
    End Sub
    
End Class
