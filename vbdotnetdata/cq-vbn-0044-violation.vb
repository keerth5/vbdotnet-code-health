' Test file for cq-vbn-0044: Type should implement IEquatable<T> when it implements IComparable<T>
' Rule should detect classes implementing IComparable<T> without IEquatable<T>

Imports System

' Violation 1: Public class implementing IComparable(Of T) without IEquatable(Of T)
Public Class Person
    Implements IComparable(Of Person)
    
    Public Property Name As String
    Public Property Age As Integer
    
    Public Sub New(name As String, age As Integer)
        Me.Name = name
        Me.Age = age
    End Sub
    
    Public Function CompareTo(other As Person) As Integer Implements IComparable(Of Person).CompareTo
        If other Is Nothing Then Return 1
        Return Me.Age.CompareTo(other.Age)
    End Function
    
End Class

' Violation 2: Friend class implementing IComparable(Of T)
Friend Class Product
    Implements IComparable(Of Product)
    
    Public Property Id As Integer
    Public Property Price As Decimal
    
    Public Function CompareTo(other As Product) As Integer Implements IComparable(Of Product).CompareTo
        If other Is Nothing Then Return 1
        Return Me.Price.CompareTo(other.Price)
    End Function
    
End Class

' Violation 3: Another public class with IComparable(Of T)
Public Class Employee
    Implements IComparable(Of Employee)
    
    Public Property EmployeeId As Integer
    Public Property Salary As Decimal
    Public Property Department As String
    
    Public Function CompareTo(other As Employee) As Integer Implements IComparable(Of Employee).CompareTo
        If other Is Nothing Then Return 1
        Dim result = Me.Department.CompareTo(other.Department)
        If result = 0 Then
            result = Me.Salary.CompareTo(other.Salary)
        End If
        Return result
    End Function
    
End Class

' This should NOT be detected - class implementing both IComparable(Of T) and IEquatable(Of T)
Public Class Customer
    Implements IComparable(Of Customer), IEquatable(Of Customer)
    
    Public Property CustomerId As Integer
    Public Property Name As String
    
    Public Function CompareTo(other As Customer) As Integer Implements IComparable(Of Customer).CompareTo
        If other Is Nothing Then Return 1
        Return Me.CustomerId.CompareTo(other.CustomerId)
    End Function
    
    Public Function Equals(other As Customer) As Boolean Implements IEquatable(Of Customer).Equals
        If other Is Nothing Then Return False
        Return Me.CustomerId = other.CustomerId
    End Function
    
    Public Overrides Function Equals(obj As Object) As Boolean
        Return Equals(TryCast(obj, Customer))
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return CustomerId.GetHashCode()
    End Function
    
End Class

' This should NOT be detected - class not implementing IComparable(Of T)
Public Class Address
    
    Public Property Street As String
    Public Property City As String
    Public Property ZipCode As String
    
    Public Overrides Function ToString() As String
        Return $"{Street}, {City} {ZipCode}"
    End Function
    
End Class

' This should NOT be detected - class implementing only IEquatable(Of T)
Public Class Category
    Implements IEquatable(Of Category)
    
    Public Property CategoryId As Integer
    Public Property Name As String
    
    Public Function Equals(other As Category) As Boolean Implements IEquatable(Of Category).Equals
        If other Is Nothing Then Return False
        Return Me.CategoryId = other.CategoryId
    End Function
    
    Public Overrides Function Equals(obj As Object) As Boolean
        Return Equals(TryCast(obj, Category))
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return CategoryId.GetHashCode()
    End Function
    
End Class
