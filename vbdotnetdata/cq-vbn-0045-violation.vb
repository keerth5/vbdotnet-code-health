' Test file for cq-vbn-0045: Override Object.Equals when implementing IEquatable<T>
' Rule should detect classes implementing IEquatable<T> without overriding Object.Equals

Imports System

' Violation 1: Public class implementing IEquatable(Of T) without overriding Object.Equals
Public Class Book
    Implements IEquatable(Of Book)
    
    Public Property ISBN As String
    Public Property Title As String
    Public Property Author As String
    
    Public Sub New(isbn As String, title As String, author As String)
        Me.ISBN = isbn
        Me.Title = title
        Me.Author = author
    End Sub
    
    Public Function Equals(other As Book) As Boolean Implements IEquatable(Of Book).Equals
        If other Is Nothing Then Return False
        Return Me.ISBN = other.ISBN
    End Function
    
    ' Missing: Public Overrides Function Equals(obj As Object) As Boolean
    
    Public Overrides Function GetHashCode() As Integer
        Return ISBN.GetHashCode()
    End Function
    
End Class

' Violation 2: Friend class implementing IEquatable(Of T)
Friend Class Order
    Implements IEquatable(Of Order)
    
    Public Property OrderId As Integer
    Public Property CustomerId As Integer
    
    Public Function Equals(other As Order) As Boolean Implements IEquatable(Of Order).Equals
        If other Is Nothing Then Return False
        Return Me.OrderId = other.OrderId
    End Function
    
    ' Missing Object.Equals override
    
End Class

' Violation 3: Another public class with IEquatable(Of T)
Public Class Vehicle
    Implements IEquatable(Of Vehicle)
    
    Public Property VIN As String
    Public Property Make As String
    Public Property Model As String
    Public Property Year As Integer
    
    Public Function Equals(other As Vehicle) As Boolean Implements IEquatable(Of Vehicle).Equals
        If other Is Nothing Then Return False
        Return Me.VIN = other.VIN
    End Function
    
    ' Missing Object.Equals override
    
    Public Overrides Function GetHashCode() As Integer
        Return VIN.GetHashCode()
    End Function
    
End Class

' This should NOT be detected - class implementing IEquatable(Of T) with proper Object.Equals override
Public Class Student
    Implements IEquatable(Of Student)
    
    Public Property StudentId As Integer
    Public Property Name As String
    Public Property Email As String
    
    Public Function Equals(other As Student) As Boolean Implements IEquatable(Of Student).Equals
        If other Is Nothing Then Return False
        Return Me.StudentId = other.StudentId
    End Function
    
    Public Overrides Function Equals(obj As Object) As Boolean
        Return Equals(TryCast(obj, Student))
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return StudentId.GetHashCode()
    End Function
    
End Class

' This should NOT be detected - class not implementing IEquatable(Of T)
Public Class Department
    
    Public Property DepartmentId As Integer
    Public Property Name As String
    Public Property Manager As String
    
    Public Overrides Function ToString() As String
        Return $"{Name} (Manager: {Manager})"
    End Function
    
End Class

' This should NOT be detected - class with both interfaces and proper overrides
Public Class Account
    Implements IEquatable(Of Account), IComparable(Of Account)
    
    Public Property AccountNumber As String
    Public Property Balance As Decimal
    
    Public Function Equals(other As Account) As Boolean Implements IEquatable(Of Account).Equals
        If other Is Nothing Then Return False
        Return Me.AccountNumber = other.AccountNumber
    End Function
    
    Public Overrides Function Equals(obj As Object) As Boolean
        Return Equals(TryCast(obj, Account))
    End Function
    
    Public Function CompareTo(other As Account) As Integer Implements IComparable(Of Account).CompareTo
        If other Is Nothing Then Return 1
        Return Me.Balance.CompareTo(other.Balance)
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return AccountNumber.GetHashCode()
    End Function
    
End Class
