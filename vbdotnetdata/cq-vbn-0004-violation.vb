' Test file for cq-vbn-0004: Avoid excessive parameters on generic types
' This file should trigger violations for generic types with too many parameters (>2)

Imports System

' Violation: Generic class with 3 type parameters
Public Class TripleGeneric(Of T, U, V)
    Private value1 As T
    Private value2 As U
    Private value3 As V
    
    Public Sub New(v1 As T, v2 As U, v3 As V)
        value1 = v1
        value2 = v2
        value3 = v3
    End Sub
End Class

' Violation: Generic class with 4 type parameters
Public Class QuadrupleGeneric(Of T, U, V, W)
    Private value1 As T
    Private value2 As U
    Private value3 As V
    Private value4 As W
End Class

' Violation: Generic structure with 5 type parameters
Public Structure MultiGenericStruct(Of T, U, V, W, X)
    Public Value1 As T
    Public Value2 As U
    Public Value3 As V
    Public Value4 As W
    Public Value5 As X
End Structure

' Violation: Generic class with many type parameters
Protected Class ManyGenerics(Of T, U, V, W, X, Y, Z)
    ' Implementation here
End Class

' Non-violation: Generic class with 1 type parameter (should not trigger)
Public Class SingleGeneric(Of T)
    Private value As T
End Class

' Non-violation: Generic class with 2 type parameters (should not trigger)
Public Class DoubleGeneric(Of T, U)
    Private value1 As T
    Private value2 As U
End Class

' Non-violation: Non-generic class (should not trigger)
Public Class NonGeneric
    Private value As String
End Class
