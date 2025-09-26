' VB.NET test file for cq-vbn-0107: Mark members as static
' Rule: Members that do not access instance data or call instance methods can be marked as static 
' (Shared in Visual Basic). After you mark the methods as static, the compiler will emit nonvirtual 
' call sites to these members.

Imports System
Imports System.Math

Public Class MathUtilities
    
    ' Violation: Public method that could be static (doesn't use instance data)
    Public Function Add(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    ' Violation: Protected method that could be static
    Protected Function Multiply(x As Double, y As Double) As Double
        Return x * y
    End Function
    
    ' Violation: Friend method that could be static
    Friend Function Subtract(a As Integer, b As Integer) As Integer
        Return a - b
    End Function
    
    ' Violation: Private method that could be static
    Private Function Divide(a As Double, b As Double) As Double
        If b = 0 Then
            Throw New DivideByZeroException()
        End If
        Return a / b
    End Function
    
    ' Violation: Public function that could be static
    Public Function CalculateSquare(value As Double) As Double
        Return value * value
    End Function
    
    ' Violation: Protected function that could be static
    Protected Function CalculateCircumference(radius As Double) As Double
        Return 2 * PI * radius
    End Function
    
    ' Violation: Friend function that could be static
    Friend Function ConvertToRadians(degrees As Double) As Double
        Return degrees * PI / 180
    End Function
    
    ' Violation: Public Sub that could be static
    Public Sub PrintMessage(message As String)
        Console.WriteLine(message)
    End Sub
    
    ' Violation: Protected Sub that could be static
    Protected Sub LogError(errorMessage As String)
        Console.WriteLine($"Error: {errorMessage}")
    End Sub
    
    ' Violation: Friend Sub that could be static
    Friend Sub ValidateInput(input As String)
        If String.IsNullOrEmpty(input) Then
            Throw New ArgumentException("Input cannot be null or empty")
        End If
    End Sub
    
End Class

Public Class StringUtilities
    
    ' Violation: Public property that could be static (doesn't access instance data)
    Public Property DefaultEncoding As String
        Get
            Return "UTF-8"
        End Get
        Set(value As String)
            ' This setter doesn't use instance data
        End Set
    End Property
    
    ' Violation: Protected property that could be static
    Protected Property MaxLength As Integer
        Get
            Return 1000
        End Get
        Set(value As Integer)
            ' This setter doesn't use instance data
        End Set
    End Property
    
    ' Violation: Friend property that could be static
    Friend Property DefaultSeparator As String
        Get
            Return ","
        End Get
        Set(value As String)
            ' This setter doesn't use instance data
        End Set
    End Property
    
    ' Violation: Public function that could be static
    Public Function FormatString(input As String, format As String) As String
        Return String.Format(format, input)
    End Function
    
    ' Violation: Protected function that could be static
    Protected Function IsValidEmail(email As String) As Boolean
        Return email.Contains("@") AndAlso email.Contains(".")
    End Function
    
    ' Violation: Friend function that could be static
    Friend Function TrimWhitespace(input As String) As String
        Return input.Trim()
    End Function
    
End Class

Public Class DataProcessor
    
    ' Violation: Public method that only uses parameters
    Public Function ProcessArray(data As Integer()) As Integer
        Dim sum As Integer = 0
        For Each value As Integer In data
            sum += value
        Next
        Return sum
    End Function
    
    ' Violation: Protected method that only uses local variables
    Protected Function GenerateRandomNumber(min As Integer, max As Integer) As Integer
        Dim random As New Random()
        Return random.Next(min, max)
    End Function
    
    ' Violation: Friend method that could be static
    Friend Sub SortArray(data As Integer())
        Array.Sort(data)
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class InstanceDataExample
    
    Private _instanceValue As Integer = 10
    Private _name As String = "Example"
    
    ' Correct: Method uses instance data - should not be detected
    Public Function GetInstanceValue() As Integer
        Return _instanceValue
    End Function
    
    ' Correct: Method modifies instance data - should not be detected
    Public Sub SetInstanceValue(value As Integer)
        _instanceValue = value
    End Sub
    
    ' Correct: Property accesses instance data - should not be detected
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
    
    ' Correct: Method calls other instance methods - should not be detected
    Public Function ProcessInstanceData() As String
        Return $"{GetInstanceValue()}: {Name}"
    End Function
    
    ' Correct: Already static method - should not be detected
    Public Shared Function StaticMethod(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    ' Correct: Already static property - should not be detected
    Public Shared Property StaticProperty As String
        Get
            Return "Static Value"
        End Get
        Set(value As String)
            ' Static property implementation
        End Set
    End Property
    
End Class

' Correct: Constructor - should not be detected
Public Class ConstructorExample
    
    Public Sub New()
        ' Constructor implementation
    End Sub
    
    Public Sub New(value As Integer)
        ' Parameterized constructor
    End Sub
    
End Class
