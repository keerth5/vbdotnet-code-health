' Test file for cq-vbn-0028: Do not overload operator == on reference types
' Rule should detect classes that overload the equality operator

Imports System

' Violation 1: Public class overloading equality operator
Public Class Person1
    
    Public Property Name As String
    Public Property Age As Integer
    
    ' Overloading == operator - violation for reference type
    Public Shared Operator =(left As Person1, right As Person1) As Boolean
        If ReferenceEquals(left, right) Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.Name = right.Name AndAlso left.Age = right.Age
    End Operator
    
    ' Usually comes with != operator too
    Public Shared Operator <>(left As Person1, right As Person1) As Boolean
        Return Not (left = right)
    End Operator
    
End Class

' Violation 2: Protected class overloading equality operator
Protected Class Employee1
    
    Public Property Id As Integer
    Public Property Department As String
    
    Public Shared Operator =(left As Employee1, right As Employee1) As Boolean
        If ReferenceEquals(left, right) Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.Id = right.Id
    End Operator
    
    Public Shared Operator <>(left As Employee1, right As Employee1) As Boolean
        Return Not (left = right)
    End Operator
    
End Class

' Violation 3: Friend class overloading equality operator
Friend Class Product1
    
    Public Property Code As String
    Public Property Price As Decimal
    
    Public Shared Operator =(left As Product1, right As Product1) As Boolean
        If ReferenceEquals(left, right) Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.Code = right.Code
    End Operator
    
    Public Shared Operator <>(left As Product1, right As Product1) As Boolean
        Return Not (left = right)
    End Operator
    
End Class

' Violation 4: Private class overloading equality operator
Private Class Item1
    
    Public Property Value As String
    
    Public Shared Operator =(left As Item1, right As Item1) As Boolean
        If ReferenceEquals(left, right) Then Return True
        If left Is Nothing OrElse right Is Nothing Then Return False
        Return left.Value = right.Value
    End Operator
    
    Public Shared Operator <>(left As Item1, right As Item1) As Boolean
        Return Not (left = right)
    End Operator
    
End Class

' This should NOT be detected - class without operator overloading
Public Class SimpleClass
    Public Property Name As String
    Public Property Value As Integer
End Class

' This should NOT be detected - class with other operators (not equality)
Public Class MathClass
    
    Public Property Value As Integer
    
    Public Shared Operator +(left As MathClass, right As MathClass) As MathClass
        Return New MathClass With {.Value = left.Value + right.Value}
    End Operator
    
    Public Shared Operator -(left As MathClass, right As MathClass) As MathClass
        Return New MathClass With {.Value = left.Value - right.Value}
    End Operator
    
End Class

' This should NOT be detected - structure (value type) with equality operator
Public Structure Point
    
    Public X As Integer
    Public Y As Integer
    
    ' Structures are value types, so equality operator is acceptable
    Public Shared Operator =(left As Point, right As Point) As Boolean
        Return left.X = right.X AndAlso left.Y = right.Y
    End Operator
    
    Public Shared Operator <>(left As Point, right As Point) As Boolean
        Return Not (left = right)
    End Operator
    
End Structure
