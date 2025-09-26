' Test file for cq-vbn-0076: Use ArgumentOutOfRangeException throw helper
' Rule should detect manual range checks that could use ArgumentOutOfRangeException.ThrowIf

Imports System

Public Class ArgumentOutOfRangeHelperExamples
    
    Public Sub ValidateAge(age As Integer)
        ' Violation 1: Manual range check with ArgumentOutOfRangeException
        If age < 0 Then
            Throw New ArgumentOutOfRangeException("age", "Age cannot be negative")
        End If
        
        Console.WriteLine("Age: " & age)
    End Sub
    
    Public Sub ValidateCount(count As Integer)
        ' Violation 2: Manual range check with ArgumentOutOfRangeException
        If count <= 0 Then
            Throw New ArgumentOutOfRangeException("count", "Count must be positive")
        End If
        
        Console.WriteLine("Count: " & count)
    End Sub
    
    Public Sub ValidateIndex(index As Integer, maxIndex As Integer)
        ' Violation 3: Manual index validation
        If index >= maxIndex Then
            Throw New ArgumentOutOfRangeException("index", "Index out of range")
        End If
        
        Console.WriteLine("Valid index: " & index)
    End Sub
    
    ' This should NOT be detected - using modern throw helper (if available)
    Public Sub ModernRangeCheck(value As Integer)
        ' ArgumentOutOfRangeException.ThrowIfNegative(value) ' Modern approach
        If value >= 0 Then
            Console.WriteLine("Valid value: " & value)
        End If
    End Sub
    
    Public Sub ValidatePercentage(percentage As Double)
        ' Violation 4: Percentage range validation
        If percentage > 100 Then
            Throw New ArgumentOutOfRangeException("percentage", "Percentage cannot exceed 100")
        End If
        
        Console.WriteLine("Percentage: " & percentage)
    End Sub
    
    Public Sub ValidateArrayIndex(index As Integer)
        ' Violation 5: Array index validation
        If index < 0 Then
            Throw New ArgumentOutOfRangeException("index", "Array index cannot be negative")
        End If
        
        Console.WriteLine("Array index: " & index)
    End Sub
    
    ' This should NOT be detected - different exception type
    Public Sub DifferentException(value As Integer)
        If value < 0 Then
            Throw New ArgumentException("Value must be positive")
        End If
    End Sub
    
    Public Function ValidateCapacity(capacity As Integer) As Integer
        ' Violation 6: Capacity validation in function
        If capacity <= 0 Then
            Throw New ArgumentOutOfRangeException("capacity", "Capacity must be positive")
        End If
        
        Return capacity * 2
    End Function
    
    Public Sub ValidateTimeout(timeout As Integer)
        ' Violation 7: Timeout validation
        If timeout < 1000 Then
            Throw New ArgumentOutOfRangeException("timeout", "Timeout must be at least 1000ms")
        End If
        
        Console.WriteLine("Timeout: " & timeout)
    End Sub
    
    ' This should NOT be detected - complex condition
    Public Sub ComplexCondition(value As Integer)
        If value < 0 OrElse value > 100 Then
            Throw New ArgumentException("Value must be between 0 and 100")
        End If
    End Sub
    
    Public Sub ValidatePage(pageNumber As Integer)
        ' Violation 8: Page number validation
        If pageNumber < 1 Then
            Throw New ArgumentOutOfRangeException("pageNumber", "Page number must be at least 1")
        End If
        
        Console.WriteLine("Page: " & pageNumber)
    End Sub
    
End Class

Public Class MoreRangeValidationExamples
    
    Public Sub ValidateSize(size As Integer)
        ' Violation 9: Size validation
        If size <= 0 Then
            Throw New ArgumentOutOfRangeException("size", "Size must be positive")
        End If
        
        Console.WriteLine("Size: " & size)
    End Sub
    
    Public Sub ValidateLength(length As Double)
        ' Violation 10: Length validation
        If length < 0.0 Then
            Throw New ArgumentOutOfRangeException("length", "Length cannot be negative")
        End If
        
        Console.WriteLine("Length: " & length)
    End Sub
    
    Public Sub ValidateQuantity(quantity As Integer)
        ' Violation 11: Quantity validation
        If quantity < 1 Then
            Throw New ArgumentOutOfRangeException("quantity", "Quantity must be at least 1")
        End If
        
        Console.WriteLine("Quantity: " & quantity)
    End Sub
    
    ' This should NOT be detected - different comparison operator pattern
    Public Sub DifferentPattern(value As Integer)
        If Not (value >= 0) Then
            Throw New ArgumentOutOfRangeException("value")
        End If
    End Sub
    
    Public Function ValidateScore(score As Integer) As Boolean
        ' Violation 12: Score validation
        If score > 100 Then
            Throw New ArgumentOutOfRangeException("score", "Score cannot exceed 100")
        End If
        
        Return score >= 70
    End Function
    
    Public Sub ValidateBufferSize(bufferSize As Long)
        ' Violation 13: Buffer size validation
        If bufferSize <= 0 Then
            Throw New ArgumentOutOfRangeException("bufferSize", "Buffer size must be positive")
        End If
        
        Console.WriteLine("Buffer size: " & bufferSize)
    End Sub
    
End Class
