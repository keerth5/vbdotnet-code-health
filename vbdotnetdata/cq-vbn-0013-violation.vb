' Test file for cq-vbn-0013: Avoid out parameters
' Rule should detect methods using Out parameters instead of ref or return values

Imports System

Public Class Calculator
    
    ' Violation 1: Public method with Out parameter
    Public Function TryDivide(dividend As Double, divisor As Double, Out result As Double) As Boolean
        If divisor <> 0 Then
            result = dividend / divisor
            Return True
        Else
            result = 0
            Return False
        End If
    End Function
    
    ' Violation 2: Protected method with Out parameter
    Protected Sub GetValues(Out x As Integer, Out y As Integer)
        x = 10
        y = 20
    End Sub
    
    ' Violation 3: Friend method with multiple Out parameters
    Friend Function ParseNumbers(input As String, Out first As Integer, Out second As Integer) As Boolean
        ' Implementation here
        first = 1
        second = 2
        Return True
    End Function
    
    ' Violation 4: Private method with Out parameter
    Private Sub CalculateStats(data As Integer(), Out min As Integer, Out max As Integer)
        min = data.Min()
        max = data.Max()
    End Sub
    
    ' This should NOT be detected - method without Out parameters
    Public Function Add(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    ' This should NOT be detected - method with ByRef parameter (acceptable)
    Public Sub Swap(ByRef a As Integer, ByRef b As Integer)
        Dim temp As Integer = a
        a = b
        b = temp
    End Sub
End Class
