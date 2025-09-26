' Test file for cq-vbn-0022: Override methods on comparable types
' Rule should detect IComparable types that don't properly implement Equals and GetHashCode

Imports System

' Violation 1: IComparable class without proper overrides
Public Class Person1
    Implements IComparable
    
    Public Property Name As String
    Public Property Age As Integer
    
    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If TypeOf obj Is Person1 Then
            Dim other As Person1 = DirectCast(obj, Person1)
            Return Me.Age.CompareTo(other.Age)
        End If
        Return 1
    End Function
    
    ' Missing: Public Overrides Function Equals(obj As Object) As Boolean
    ' Missing: Public Overrides Function GetHashCode() As Integer
End Class

' Violation 2: Another IComparable class without overrides
Protected Class Employee1
    Implements IComparable
    
    Public Property Id As Integer
    Public Property Salary As Decimal
    
    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If TypeOf obj Is Employee1 Then
            Dim other As Employee1 = DirectCast(obj, Employee1)
            Return Me.Salary.CompareTo(other.Salary)
        End If
        Return 1
    End Function
    
    ' Missing overrides for Equals and GetHashCode
End Class

' Violation 3: Friend IComparable class
Friend Class Product1
    Implements IComparable
    
    Public Property Name As String
    Public Property Price As Decimal
    
    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If TypeOf obj Is Product1 Then
            Dim other As Product1 = DirectCast(obj, Product1)
            Return Me.Price.CompareTo(other.Price)
        End If
        Return 1
    End Function
End Class

' Violation 4: Private IComparable class
Private Class Item1
    Implements IComparable
    
    Public Property Value As Integer
    
    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If TypeOf obj Is Item1 Then
            Dim other As Item1 = DirectCast(obj, Item1)
            Return Me.Value.CompareTo(other.Value)
        End If
        Return 1
    End Function
End Class

' This should NOT be detected - proper implementation with overrides
Public Class Person2
    Implements IComparable
    
    Public Property Name As String
    Public Property Age As Integer
    
    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If TypeOf obj Is Person2 Then
            Dim other As Person2 = DirectCast(obj, Person2)
            Return Me.Age.CompareTo(other.Age)
        End If
        Return 1
    End Function
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is Person2 Then
            Dim other As Person2 = DirectCast(obj, Person2)
            Return Me.Name = other.Name AndAlso Me.Age = other.Age
        End If
        Return False
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return Name.GetHashCode() Xor Age.GetHashCode()
    End Function
End Class

' This should NOT be detected - class not implementing IComparable
Public Class SimpleClass
    Public Property Name As String
End Class
