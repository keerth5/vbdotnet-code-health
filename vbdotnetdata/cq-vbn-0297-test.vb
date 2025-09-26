' VB.NET test file for cq-vbn-0297: Implement generic math interfaces correctly
' This rule detects incorrect generic math interface implementations

Imports System
Imports System.Numerics

' BAD: Incorrect generic math interface implementation
Public Class BadMathImplementation
    Implements IAdditionOperators(Of BadMathImplementation, Integer, String)
    
    ' Violation: Wrong type parameter - should be BadMathImplementation, not Integer/String
    Public Shared Operator +(left As BadMathImplementation, right As Integer) As String Implements IAdditionOperators(Of BadMathImplementation, Integer, String).op_Addition
        Return left.ToString() & right.ToString()
    End Operator
End Class

' BAD: Another incorrect implementation
Public Structure BadMathStruct
    Implements IMultiplyOperators(Of BadMathStruct, Double, BadMathStruct)
    
    Public Value As Double
    
    ' Violation: Inconsistent type parameters
    Public Shared Operator *(left As BadMathStruct, right As Double) As BadMathStruct Implements IMultiplyOperators(Of BadMathStruct, Double, BadMathStruct).op_Multiply
        Return New BadMathStruct With {.Value = left.Value * right}
    End Operator
End Class

' BAD: Comparison operators with wrong types
Public Class BadComparisonClass
    Implements IComparisonOperators(Of BadComparisonClass, Integer, Boolean)
    
    ' Violation: Should use self-recurring type parameter
    Public Shared Operator >(left As BadComparisonClass, right As Integer) As Boolean Implements IComparisonOperators(Of BadComparisonClass, Integer, Boolean).op_GreaterThan
        Return True
    End Operator
    
    Public Shared Operator <(left As BadComparisonClass, right As Integer) As Boolean Implements IComparisonOperators(Of BadComparisonClass, Integer, Boolean).op_LessThan
        Return False
    End Operator
End Class

' GOOD: Correct generic math interface implementation
Public Class GoodMathImplementation
    Implements IAdditionOperators(Of GoodMathImplementation, GoodMathImplementation, GoodMathImplementation)
    
    Public Value As Integer
    
    ' Good: Self-recurring type parameter
    Public Shared Operator +(left As GoodMathImplementation, right As GoodMathImplementation) As GoodMathImplementation Implements IAdditionOperators(Of GoodMathImplementation, GoodMathImplementation, GoodMathImplementation).op_Addition
        Return New GoodMathImplementation With {.Value = left.Value + right.Value}
    End Operator
End Class
