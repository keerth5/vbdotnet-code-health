' Test file for cq-vbn-0153: Do not pass a nullable struct to 'ArgumentNullException.ThrowIfNull'
' ArgumentNullException.ThrowIfNull accepts an 'object', so passing a nullable struct might cause the value to be boxed

Imports System

Public Class NullableStructTests
    
    ' Violation: Passing nullable Integer to ThrowIfNull
    Public Sub ValidateNullableInteger(value As Integer?)
        ' Violation: Boxing nullable struct
        ArgumentNullException.ThrowIfNull(value)
        
        Console.WriteLine($"Value is: {value.Value}")
    End Sub
    
    ' Violation: Passing nullable DateTime to ThrowIfNull
    Public Sub ValidateNullableDateTime(timestamp As DateTime?)
        ' Violation: Boxing nullable struct
        ArgumentNullException.ThrowIfNull(timestamp)
        
        Console.WriteLine($"Timestamp: {timestamp.Value}")
    End Sub
    
    ' Violation: Passing nullable Boolean to ThrowIfNull
    Public Sub ValidateNullableBoolean(flag As Boolean?)
        ' Violation: Boxing nullable struct
        ArgumentNullException.ThrowIfNull(flag)
        
        Console.WriteLine($"Flag is: {flag.Value}")
    End Sub
    
    ' Violation: Passing nullable Decimal to ThrowIfNull
    Public Sub ValidateNullableDecimal(amount As Decimal?)
        ' Violation: Boxing nullable struct
        ArgumentNullException.ThrowIfNull(amount)
        
        Console.WriteLine($"Amount: {amount.Value}")
    End Sub
    
    ' Violation: Passing nullable Double to ThrowIfNull
    Public Sub ValidateNullableDouble(rate As Double?)
        ' Violation: Boxing nullable struct
        ArgumentNullException.ThrowIfNull(rate)
        
        Console.WriteLine($"Rate: {rate.Value}")
    End Sub
    
    ' Violation: Passing nullable Guid to ThrowIfNull
    Public Sub ValidateNullableGuid(id As Guid?)
        ' Violation: Boxing nullable struct
        ArgumentNullException.ThrowIfNull(id)
        
        Console.WriteLine($"ID: {id.Value}")
    End Sub
    
    ' Violation: Passing nullable TimeSpan to ThrowIfNull
    Public Sub ValidateNullableTimeSpan(duration As TimeSpan?)
        ' Violation: Boxing nullable struct
        ArgumentNullException.ThrowIfNull(duration)
        
        Console.WriteLine($"Duration: {duration.Value}")
    End Sub
    
    ' Violation: Passing nullable custom struct to ThrowIfNull
    Public Structure Point
        Public X As Integer
        Public Y As Integer
    End Structure
    
    Public Sub ValidateNullablePoint(point As Point?)
        ' Violation: Boxing nullable custom struct
        ArgumentNullException.ThrowIfNull(point)
        
        Console.WriteLine($"Point: ({point.Value.X}, {point.Value.Y})")
    End Sub
    
    ' Good practice: Proper null checks for nullable structs (should not be detected)
    Public Sub ProperNullableIntegerValidation(value As Integer?)
        If value Is Nothing Then
            Throw New ArgumentNullException(NameOf(value))
        End If
        
        Console.WriteLine($"Value is: {value.Value}")
    End Sub
    
    ' Good: Using ThrowIfNull with reference types
    Public Sub ValidateString(text As String)
        ArgumentNullException.ThrowIfNull(text)
        Console.WriteLine($"Text: {text}")
    End Sub
    
    ' Good: Using ThrowIfNull with object
    Public Sub ValidateObject(obj As Object)
        ArgumentNullException.ThrowIfNull(obj)
        Console.WriteLine($"Object: {obj}")
    End Sub
    
    ' Violation: Multiple nullable struct validations
    Public Sub ValidateMultipleNullableValues(id As Integer?, name As String, active As Boolean?)
        ' Violation: Boxing nullable Integer
        ArgumentNullException.ThrowIfNull(id)
        
        ' Good: Reference type validation
        ArgumentNullException.ThrowIfNull(name)
        
        ' Violation: Boxing nullable Boolean
        ArgumentNullException.ThrowIfNull(active)
        
        Console.WriteLine($"ID: {id}, Name: {name}, Active: {active}")
    End Sub
    
    ' Violation: Nullable struct in conditional validation
    Public Sub ConditionalValidation(value As Long?)
        If value.HasValue Then
            ' Violation: Still boxing even in conditional
            ArgumentNullException.ThrowIfNull(value)
        End If
    End Sub
    
End Class

' Additional test cases
Public Module NullableValidationUtilities
    
    ' Violation: Generic method with nullable struct
    Public Sub ValidateNullable(Of T As Structure)(value As T?)
        ' Violation: Boxing nullable generic struct
        ArgumentNullException.ThrowIfNull(value)
        
        Console.WriteLine($"Value: {value}")
    End Sub
    
    ' Violation: Extension method simulation
    Public Sub ValidateAndProcess(value As Single?)
        ' Violation: Boxing nullable Single
        ArgumentNullException.ThrowIfNull(value)
        
        Dim processed As Single = value.Value * 2
        Console.WriteLine($"Processed: {processed}")
    End Sub
    
    ' Violation: Nullable struct in array processing
    Public Sub ProcessNullableArray(values() As Integer?)
        For Each value In values
            If value.HasValue Then
                ' Violation: Boxing in loop
                ArgumentNullException.ThrowIfNull(value)
                Console.WriteLine($"Processing: {value.Value}")
            End If
        Next
    End Sub
    
    ' Good: Proper validation without boxing
    Public Sub ProperValidation(value As Integer?)
        If Not value.HasValue Then
            Throw New ArgumentException("Value cannot be null", NameOf(value))
        End If
        
        Console.WriteLine($"Valid value: {value.Value}")
    End Sub
    
End Module

' Test with enum (which is also a value type)
Public Enum Status
    Active
    Inactive
    Pending
End Enum

Public Class EnumTests
    
    ' Violation: Nullable enum to ThrowIfNull
    Public Sub ValidateStatus(status As Status?)
        ' Violation: Boxing nullable enum
        ArgumentNullException.ThrowIfNull(status)
        
        Console.WriteLine($"Status: {status.Value}")
    End Sub
    
End Class
