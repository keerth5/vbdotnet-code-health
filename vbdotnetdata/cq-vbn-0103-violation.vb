' VB.NET test file for cq-vbn-0103: Override equals and operator equals on value types
' Rule: For value types, the inherited implementation of Equals uses the Reflection library and 
' compares the contents of all fields. Reflection is computationally expensive, and comparing 
' every field for equality might be unnecessary.

Imports System

' Violation: Public structure without overridden Equals method
Public Structure Point
    Public X As Integer
    Public Y As Integer
    
    Public Sub New(x As Integer, y As Integer)
        Me.X = x
        Me.Y = y
    End Sub
End Structure

' Violation: Friend structure without overridden Equals method
Friend Structure Rectangle
    Public Width As Double
    Public Height As Double
    
    Public Sub New(width As Double, height As Double)
        Me.Width = width
        Me.Height = height
    End Sub
    
    Public Function Area() As Double
        Return Width * Height
    End Function
End Structure

' Violation: Public structure with multiple fields
Public Structure Color
    Public Red As Byte
    Public Green As Byte  
    Public Blue As Byte
    Public Alpha As Byte
    
    Public Sub New(r As Byte, g As Byte, b As Byte)
        Red = r
        Green = g
        Blue = b
        Alpha = 255
    End Sub
End Structure

' Violation: Friend structure with properties
Friend Structure Vector3D
    Public Property X As Double
    Public Property Y As Double
    Public Property Z As Double
    
    Public Sub New(x As Double, y As Double, z As Double)
        Me.X = x
        Me.Y = y
        Me.Z = z
    End Sub
    
    Public Function Magnitude() As Double
        Return Math.Sqrt(X * X + Y * Y + Z * Z)
    End Function
End Structure

' Violation: Public structure with mixed fields and properties
Public Structure Temperature
    Public ReadOnly Celsius As Double
    Public Property Fahrenheit As Double
        Get
            Return Celsius * 9 / 5 + 32
        End Get
        Set(value As Double)
            ' Cannot set as Celsius is ReadOnly
        End Set
    End Property
    
    Public Sub New(celsius As Double)
        Me.Celsius = celsius
    End Sub
End Structure

' Violation: Simple public structure
Public Structure Coordinate
    Public Latitude As Double
    Public Longitude As Double
End Structure

' Violation: Friend structure with methods
Friend Structure Matrix2x2
    Public A11, A12, A21, A22 As Double
    
    Public Function Determinant() As Double
        Return A11 * A22 - A12 * A21
    End Function
End Structure

' Violation: Public structure with nested types
Public Structure ComplexNumber
    Public Real As Double
    Public Imaginary As Double
    
    Public Enum Format
        Standard
        Polar
    End Enum
End Structure

' Violation: Structure with operators but no Equals override
Public Structure Money
    Public Amount As Decimal
    Public Currency As String
    
    Public Shared Operator +(left As Money, right As Money) As Money
        If left.Currency <> right.Currency Then
            Throw New InvalidOperationException("Cannot add different currencies")
        End If
        Return New Money With {.Amount = left.Amount + right.Amount, .Currency = left.Currency}
    End Operator
End Structure

' Violation: Generic structure
Public Structure Pair(Of T)
    Public First As T
    Public Second As T
    
    Public Sub New(first As T, second As T)
        Me.First = first
        Me.Second = second
    End Sub
End Structure

' Non-violation examples (these should not be detected):

' Class (not structure) - should not be detected
Public Class PointClass
    Public X As Integer
    Public Y As Integer
End Class

' Private structure - should not be detected by this pattern
Private Structure PrivatePoint
    Public X As Integer
    Public Y As Integer
End Structure

' Protected structure - should not be detected by this pattern
Protected Structure ProtectedPoint
    Public X As Integer  
    Public Y As Integer
End Structure

' Structure with proper Equals override - should not be detected as violation
Public Structure ProperPoint
    Public X As Integer
    Public Y As Integer
    
    Public Overrides Function Equals(obj As Object) As Boolean
        If Not TypeOf obj Is ProperPoint Then
            Return False
        End If
        Dim other As ProperPoint = DirectCast(obj, ProperPoint)
        Return X = other.X AndAlso Y = other.Y
    End Function
    
    Public Overrides Function GetHashCode() As Integer
        Return X.GetHashCode() Xor Y.GetHashCode()
    End Function
End Structure
