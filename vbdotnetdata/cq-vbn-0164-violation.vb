' Test file for cq-vbn-0164: Do not assign property within its setter
' A property was accidentally assigned a value within its own set accessor

Imports System

Public Class PropertySetterTests
    
    Private _name As String
    Private _age As Integer
    Private _isActive As Boolean
    
    ' Violation: Property assigns to itself in setter
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            ' Violation: Assigning Name property within its own setter
            Name = value.Trim()
        End Set
    End Property
    
    ' Violation: Integer property assigns to itself
    Public Property Age As Integer
        Get
            Return _age
        End Get
        Set(value As Integer)
            If value >= 0 Then
                ' Violation: Assigning Age property within its own setter
                Age = value
            End If
        End Set
    End Property
    
    ' Violation: Boolean property assigns to itself
    Public Property IsActive As Boolean
        Get
            Return _isActive
        End Get
        Set(value As Boolean)
            ' Violation: Assigning IsActive property within its own setter
            IsActive = value
            Console.WriteLine($"IsActive set to: {value}")
        End Set
    End Property
    
    ' Good practice: Proper property setter (should not be detected)
    Private _email As String
    Public Property Email As String
        Get
            Return _email
        End Get
        Set(value As String)
            ' Good: Assigning to backing field
            _email = value?.Trim().ToLower()
        End Set
    End Property
    
    ' Violation: Property with validation that assigns to itself
    Private _score As Double
    Public Property Score As Double
        Get
            Return _score
        End Get
        Set(value As Double)
            If value >= 0.0 AndAlso value <= 100.0 Then
                ' Violation: Assigning Score to itself instead of backing field
                Score = value
            Else
                Throw New ArgumentOutOfRangeException("Score must be between 0 and 100")
            End If
        End Set
    End Property
    
    ' Violation: String property with complex logic
    Private _description As String
    Public Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            If Not String.IsNullOrEmpty(value) Then
                ' Violation: Assigning Description to itself
                Description = value.Substring(0, Math.Min(value.Length, 100))
            End If
        End Set
    End Property
    
    ' Violation: Property with multiple assignments to itself
    Private _status As String
    Public Property Status As String
        Get
            Return _status
        End Get
        Set(value As String)
            Select Case value?.ToLower()
                Case "active"
                    ' Violation: First assignment to itself
                    Status = "ACTIVE"
                Case "inactive"
                    ' Violation: Second assignment to itself
                    Status = "INACTIVE"
                Case Else
                    ' Violation: Third assignment to itself
                    Status = "UNKNOWN"
            End Select
        End Set
    End Property
    
    ' Good: Auto-implemented property (should not be detected)
    Public Property AutoProperty As String
    
    ' Good: Read-only property (should not be detected)
    Public ReadOnly Property ReadOnlyProperty As String
        Get
            Return "ReadOnly"
        End Get
    End Property
    
    ' Violation: Property in conditional assignment
    Private _priority As Integer
    Public Property Priority As Integer
        Get
            Return _priority
        End Get
        Set(value As Integer)
            If value > 0 Then
                ' Violation: Conditional assignment to itself
                Priority = Math.Min(value, 10)
            End If
        End Set
    End Property
    
End Class

' Additional test cases with different property patterns
Public Class AdvancedPropertyTests
    
    ' Violation: Protected property assigns to itself
    Private _protectedValue As String
    Protected Property ProtectedValue As String
        Get
            Return _protectedValue
        End Get
        Set(value As String)
            ' Violation: Protected property assigning to itself
            ProtectedValue = value?.ToUpper()
        End Set
    End Property
    
    ' Violation: Friend property assigns to itself
    Private _friendValue As Integer
    Friend Property FriendValue As Integer
        Get
            Return _friendValue
        End Get
        Set(value As Integer)
            ' Violation: Friend property assigning to itself
            FriendValue = Math.Abs(value)
        End Set
    End Property
    
    ' Violation: Private property assigns to itself
    Private _privateValue As Boolean
    Private Property PrivateValue As Boolean
        Get
            Return _privateValue
        End Get
        Set(value As Boolean)
            ' Violation: Private property assigning to itself
            PrivateValue = value
        End Set
    End Property
    
    ' Violation: Generic property assigns to itself
    Private _genericValue As List(Of String)
    Public Property GenericValue As List(Of String)
        Get
            Return _genericValue
        End Get
        Set(value As List(Of String))
            ' Violation: Generic property assigning to itself
            GenericValue = value?.Where(Function(s) Not String.IsNullOrEmpty(s)).ToList()
        End Set
    End Property
    
    ' Violation: Nullable property assigns to itself
    Private _nullableValue As Integer?
    Public Property NullableValue As Integer?
        Get
            Return _nullableValue
        End Get
        Set(value As Integer?)
            ' Violation: Nullable property assigning to itself
            NullableValue = If(value.HasValue AndAlso value.Value >= 0, value, Nothing)
        End Set
    End Property
    
    ' Violation: Property with exception handling
    Private _exceptionValue As String
    Public Property ExceptionValue As String
        Get
            Return _exceptionValue
        End Get
        Set(value As String)
            Try
                ' Violation: Assignment to itself in try block
                ExceptionValue = value.Trim()
            Catch ex As Exception
                Console.WriteLine($"Error setting ExceptionValue: {ex.Message}")
            End Try
        End Set
    End Property
    
    ' Good: Property with proper backing field usage
    Private _properValue As String
    Public Property ProperValue As String
        Get
            Return _properValue
        End Get
        Set(value As String)
            ' Good: Assigning to backing field
            _properValue = value?.Trim()
            OnPropertyChanged("ProperValue")
        End Set
    End Property
    
    Private Sub OnPropertyChanged(propertyName As String)
        Console.WriteLine($"Property {propertyName} changed")
    End Sub
    
End Class

' Test with indexed properties
Public Class IndexedPropertyTests
    
    Private _items As New Dictionary(Of String, String)
    
    ' Violation: Indexed property assigns to itself
    Default Public Property Item(key As String) As String
        Get
            Return If(_items.ContainsKey(key), _items(key), Nothing)
        End Get
        Set(value As String)
            ' Violation: Indexed property assigning to itself
            Item(key) = value?.Trim()
        End Set
    End Property
    
    ' Good: Indexed property with proper implementation
    Private _values As New Dictionary(Of Integer, String)
    Public Property Values(index As Integer) As String
        Get
            Return If(_values.ContainsKey(index), _values(index), Nothing)
        End Get
        Set(value As String)
            ' Good: Assigning to backing dictionary
            _values(index) = value
        End Set
    End Property
    
End Class

' Test with inheritance
Public Class BasePropertyClass
    
    Private _baseValue As String
    
    ' Violation: Base class property assigns to itself
    Public Overridable Property BaseValue As String
        Get
            Return _baseValue
        End Get
        Set(value As String)
            ' Violation: Base property assigning to itself
            BaseValue = value?.ToLower()
        End Set
    End Property
    
End Class

Public Class DerivedPropertyClass
    Inherits BasePropertyClass
    
    ' Violation: Overridden property assigns to itself
    Public Overrides Property BaseValue As String
        Get
            Return MyBase.BaseValue
        End Get
        Set(value As String)
            ' Violation: Overridden property assigning to itself
            BaseValue = value?.ToUpper()
        End Set
    End Property
    
End Class

' Test with interface implementation
Public Interface IPropertyInterface
    Property InterfaceValue As String
End Interface

Public Class InterfaceImplementation
    Implements IPropertyInterface
    
    Private _interfaceValue As String
    
    ' Violation: Interface property implementation assigns to itself
    Public Property InterfaceValue As String Implements IPropertyInterface.InterfaceValue
        Get
            Return _interfaceValue
        End Get
        Set(value As String)
            ' Violation: Interface property assigning to itself
            InterfaceValue = value?.Trim()
        End Set
    End Property
    
End Class
