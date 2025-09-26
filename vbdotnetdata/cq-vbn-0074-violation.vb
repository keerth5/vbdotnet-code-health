' Test file for cq-vbn-0074: Use ArgumentNullException throw helper
' Rule should detect manual null checks that could use ArgumentNullException.ThrowIfNull

Imports System

Public Class ArgumentNullHelperExamples
    
    Public Sub ProcessString(input As String)
        ' Violation 1: Manual null check with ArgumentNullException
        If input Is Nothing Then
            Throw New ArgumentNullException("input")
        End If
        
        Console.WriteLine(input.Length)
    End Sub
    
    Public Sub ProcessObject(data As Object)
        ' Violation 2: Manual null check with ArgumentNullException
        If data Is Nothing Then
            Throw New ArgumentNullException("data")
        End If
        
        Console.WriteLine(data.ToString())
    End Sub
    
    Public Sub ProcessArray(items As Integer())
        ' Violation 3: Manual null check with ArgumentNullException
        If items Is Nothing Then
            Throw New ArgumentNullException("items")
        End If
        
        Console.WriteLine("Array length: " & items.Length)
    End Sub
    
    ' This should NOT be detected - using modern throw helper (if available)
    Public Sub ModernNullCheck(value As String)
        ' ArgumentNullException.ThrowIfNull(value) ' Modern approach
        If value IsNot Nothing Then
            Console.WriteLine(value)
        End If
    End Sub
    
    Public Function CalculateLength(text As String) As Integer
        ' Violation 4: Manual null check in function
        If text Is Nothing Then
            Throw New ArgumentNullException("text")
        End If
        
        Return text.Length
    End Function
    
    Public Sub ProcessMultipleParameters(first As String, second As Object)
        ' Violation 5: First parameter null check
        If first Is Nothing Then
            Throw New ArgumentNullException("first")
        End If
        
        ' Violation 6: Second parameter null check
        If second Is Nothing Then
            Throw New ArgumentNullException("second")
        End If
        
        Console.WriteLine(first & second.ToString())
    End Sub
    
    ' This should NOT be detected - different exception type
    Public Sub DifferentException(input As String)
        If input Is Nothing Then
            Throw New InvalidOperationException("Input is required")
        End If
    End Sub
    
    ' This should NOT be detected - different condition
    Public Sub DifferentCondition(input As String)
        If String.IsNullOrEmpty(input) Then
            Throw New ArgumentException("Input cannot be empty")
        End If
    End Sub
    
    Public Sub ProcessCollection(collection As IEnumerable(Of String))
        ' Violation 7: Collection null check
        If collection Is Nothing Then
            Throw New ArgumentNullException("collection")
        End If
        
        For Each item In collection
            Console.WriteLine(item)
        Next
    End Sub
    
    Public Sub ProcessCallback(callback As Action)
        ' Violation 8: Callback null check
        If callback Is Nothing Then
            Throw New ArgumentNullException("callback")
        End If
        
        callback()
    End Sub
    
End Class

Public Class MoreNullCheckExamples
    
    Public Sub ValidateInput(parameter As String)
        ' Violation 9: Another null check pattern
        If parameter Is Nothing Then
            Throw New ArgumentNullException("parameter")
        End If
        
        parameter = parameter.Trim()
    End Sub
    
    Public Sub ProcessData(info As Object)
        ' Violation 10: Object null check
        If info Is Nothing Then
            Throw New ArgumentNullException("info")
        End If
        
        ' Additional processing
        Dim result = info.GetHashCode()
    End Sub
    
    ' This should NOT be detected - complex condition
    Public Sub ComplexCondition(value As String)
        If value Is Nothing OrElse value.Length = 0 Then
            Throw New ArgumentException("Value cannot be null or empty")
        End If
    End Sub
    
    Public Function GetStringLength(str As String) As Integer
        ' Violation 11: String null check in function
        If str Is Nothing Then
            Throw New ArgumentNullException("str")
        End If
        
        Return str.Length
    End Function
    
End Class
