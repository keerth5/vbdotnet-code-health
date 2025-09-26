' Test file for cq-vbn-0071: Use nameof in place of string literal
' Rule should detect string literals that could be replaced with nameof

Imports System
Imports System.ComponentModel

Public Class PropertyChangeExamples
    
    Public Event PropertyChanged As PropertyChangedEventHandler
    
    Private _name As String
    Private _age As Integer
    
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
            ' Violation 1: PropertyChangedEventArgs with string literal
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Name"))
        End Set
    End Property
    
    Public Property Age As Integer
        Get
            Return _age
        End Get
        Set(value As Integer)
            _age = value
            ' Violation 2: PropertyChangedEventArgs with string literal
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Age"))
        End Set
    End Property
    
    ' This should NOT be detected - using nameof
    Public Property Address As String
        Get
            Return _address
        End Get
        Set(value As String)
            _address = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Address)))
        End Set
    End Property
    Private _address As String
    
End Class

Public Class ArgumentExceptionExamples
    
    Public Sub ValidateInput(input As String)
        ' Violation 3: ArgumentException with string literal
        If String.IsNullOrEmpty(input) Then
            Throw New ArgumentException("Input cannot be null or empty", "input")
        End If
        
        ' Violation 4: ArgumentNullException with string literal
        If input Is Nothing Then
            Throw New ArgumentNullException("input")
        End If
    End Sub
    
    Public Sub ValidateNumber(number As Integer)
        ' Violation 5: ArgumentException with string literal
        If number < 0 Then
            Throw New ArgumentException("Number must be positive", "number")
        End If
    End Sub
    
    Public Sub ValidateData(data As Object)
        ' Violation 6: ArgumentNullException with string literal
        If data Is Nothing Then
            Throw New ArgumentNullException("data")
        End If
    End Sub
    
    ' This should NOT be detected - using nameof
    Public Sub ValidateWithNameof(value As String)
        If String.IsNullOrEmpty(value) Then
            Throw New ArgumentException("Value cannot be null or empty", NameOf(value))
        End If
        
        If value Is Nothing Then
            Throw New ArgumentNullException(NameOf(value))
        End If
    End Sub
    
    Public Sub AnotherValidation(parameter As String)
        ' Violation 7: ArgumentNullException with string literal
        If parameter Is Nothing Then
            Throw New ArgumentNullException("parameter")
        End If
        
        ' Violation 8: ArgumentException with string literal
        If parameter.Length = 0 Then
            Throw New ArgumentException("Parameter cannot be empty", "parameter")
        End If
    End Sub
    
End Class

Public Class MoreExamples
    
    Public Event SomeEvent As EventHandler
    
    Public Sub RaisePropertyChanged(propertyName As String)
        ' Violation 9: PropertyChangedEventArgs with string literal
        Dim args As New PropertyChangedEventArgs("PropertyName")
        Console.WriteLine("Property changed: " & propertyName)
    End Sub
    
    Public Sub ThrowException(arg As String)
        ' Violation 10: ArgumentException with string literal
        Throw New ArgumentException("Invalid argument", "arg")
    End Sub
    
    Public Sub ThrowNullException(param As Object)
        ' Violation 11: ArgumentNullException with string literal
        Throw New ArgumentNullException("param")
    End Sub
    
    ' This should NOT be detected - regular string usage
    Public Sub RegularStringUsage()
        Dim message As String = "This is a regular string"
        Console.WriteLine("Regular message: " & message)
    End Sub
    
End Class
