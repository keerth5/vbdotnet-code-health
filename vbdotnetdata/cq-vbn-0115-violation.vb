' VB.NET test file for cq-vbn-0115: Prefer strongly-typed Append and Insert method overloads on StringBuilder
' Rule: Append and Insert provide overloads for multiple types beyond System.String. When possible, 
' prefer the strongly-typed overloads over using ToString() and the string-based overload.

Imports System
Imports System.Text

Public Class StringBuilderExamples
    
    Public Sub TestStringBuilderAppend()
        Dim sb As New StringBuilder()
        Dim number As Integer = 42
        Dim price As Double = 19.99
        Dim isActive As Boolean = True
        Dim character As Char = "A"c
        Dim longValue As Long = 1234567890L
        Dim floatValue As Single = 3.14F
        Dim decimalValue As Decimal = 123.45D
        
        ' Violation: Using ToString() with Append instead of strongly-typed overload
        sb.Append(number.ToString())
        
        ' Violation: Using ToString() with Append for Double
        sb.Append(price.ToString())
        
        ' Violation: Using ToString() with Append for Boolean
        sb.Append(isActive.ToString())
        
        ' Violation: Using ToString() with Append for Char
        sb.Append(character.ToString())
        
        ' Violation: Using ToString() with Append for Long
        sb.Append(longValue.ToString())
        
        ' Violation: Using ToString() with Append for Single
        sb.Append(floatValue.ToString())
        
        ' Violation: Using ToString() with Append for Decimal
        sb.Append(decimalValue.ToString())
        
        ' Violation: Using ToString() with Append in method call
        BuildString(sb, number.ToString())
        
        ' Violation: Using ToString() with Append in complex expression
        sb.Append("Value: ").Append(number.ToString()).Append(" units")
        
        ' Violation: Using ToString() with Append for object property
        Dim customer As New Customer With {.Id = 123}
        sb.Append(customer.Id.ToString())
        
        ' Violation: Using ToString() with Append for method result
        sb.Append(GetNumber().ToString())
        
        ' Violation: Using ToString() with Append for calculated value
        sb.Append((number * 2).ToString())
        
    End Sub
    
    Public Sub TestStringBuilderInsert()
        Dim sb As New StringBuilder("Hello World")
        Dim number As Integer = 42
        Dim price As Double = 19.99
        Dim isActive As Boolean = True
        Dim character As Char = "X"c
        
        ' Violation: Using ToString() with Insert instead of strongly-typed overload
        sb.Insert(0, number.ToString())
        
        ' Violation: Using ToString() with Insert for Double
        sb.Insert(5, price.ToString())
        
        ' Violation: Using ToString() with Insert for Boolean
        sb.Insert(10, isActive.ToString())
        
        ' Violation: Using ToString() with Insert for Char
        sb.Insert(3, character.ToString())
        
        ' Violation: Using ToString() with Insert in complex scenario
        sb.Insert(0, "Start: ").Insert(sb.Length, " End: ").Insert(sb.Length, number.ToString())
        
        ' Violation: Using ToString() with Insert for object property
        Dim customer As New Customer With {.Id = 456}
        sb.Insert(0, customer.Id.ToString())
        
        ' Violation: Using ToString() with Insert for method result
        sb.Insert(sb.Length, GetNumber().ToString())
        
        ' Violation: Using ToString() with Insert for calculated value
        sb.Insert(5, (number + price).ToString())
        
    End Sub
    
    Public Sub TestStringBuilderInLoop()
        Dim sb As New StringBuilder()
        Dim numbers As Integer() = {1, 2, 3, 4, 5}
        
        For Each num In numbers
            ' Violation: Using ToString() with Append in loop
            sb.Append(num.ToString()).Append(", ")
        Next
        
        For i As Integer = 0 To numbers.Length - 1
            ' Violation: Using ToString() with Insert in loop
            sb.Insert(0, numbers(i).ToString())
        Next
    End Sub
    
    Public Function BuildReport() As String
        Dim sb As New StringBuilder()
        Dim totalItems As Integer = 100
        Dim totalPrice As Decimal = 1599.99D
        Dim isComplete As Boolean = True
        
        ' Violation: Multiple ToString() calls with Append
        sb.Append("Report Summary:").AppendLine()
        sb.Append("Total Items: ").Append(totalItems.ToString()).AppendLine()
        sb.Append("Total Price: $").Append(totalPrice.ToString()).AppendLine()
        sb.Append("Complete: ").Append(isComplete.ToString())
        
        Return sb.ToString()
    End Function
    
    Private Sub BuildString(sb As StringBuilder, value As String)
        sb.Append(value)
    End Sub
    
    Private Function GetNumber() As Integer
        Return 999
    End Function
    
End Class

Public Class Customer
    Public Property Id As Integer
    Public Property Name As String
End Class

' More violation examples in different contexts

Public Class LoggingExample
    
    Public Sub LogMessage(level As Integer, message As String, timestamp As DateTime)
        Dim sb As New StringBuilder()
        
        ' Violation: Using ToString() with Append for logging
        sb.Append("[").Append(level.ToString()).Append("] ")
        sb.Append(timestamp.ToString()).Append(": ")
        sb.Append(message)
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub LogError(errorCode As Integer, exception As Exception)
        Dim sb As New StringBuilder()
        
        ' Violation: Using ToString() with Insert for error logging
        sb.Insert(0, "ERROR ").Insert(6, errorCode.ToString()).Insert(sb.Length, ": ")
        sb.Append(exception.Message)
        
        Console.WriteLine(sb.ToString())
    End Sub
    
End Class

Public Class DataFormattingExample
    
    Public Function FormatData(id As Integer, value As Double, isValid As Boolean) As String
        Dim sb As New StringBuilder()
        
        ' Violation: Using ToString() with Append for data formatting
        sb.Append("ID=").Append(id.ToString())
        sb.Append(", Value=").Append(value.ToString())
        sb.Append(", Valid=").Append(isValid.ToString())
        
        Return sb.ToString()
    End Function
    
    Public Sub ProcessRecords()
        Dim sb As New StringBuilder()
        Dim records As List(Of Record) = GetRecords()
        
        For Each record In records
            ' Violation: Using ToString() with Append in data processing
            sb.Append(record.Id.ToString()).Append("|")
            sb.Append(record.Amount.ToString()).Append("|")
            sb.Append(record.IsActive.ToString()).AppendLine()
        Next
        
        SaveData(sb.ToString())
    End Sub
    
    Private Function GetRecords() As List(Of Record)
        Return New List(Of Record)()
    End Function
    
    Private Sub SaveData(data As String)
        ' Save the formatted data
    End Sub
    
End Class

Public Class Record
    Public Property Id As Integer
    Public Property Amount As Decimal
    Public Property IsActive As Boolean
End Class

' Non-violation examples (these should not be detected):

Public Class ProperStringBuilderUsage
    
    Public Sub TestProperUsage()
        Dim sb As New StringBuilder()
        Dim number As Integer = 42
        Dim price As Double = 19.99
        Dim isActive As Boolean = True
        Dim character As Char = "A"c
        
        ' Correct: Using strongly-typed Append overloads - should not be detected
        sb.Append(number)
        sb.Append(price)
        sb.Append(isActive)
        sb.Append(character)
        
        ' Correct: Using strongly-typed Insert overloads - should not be detected
        sb.Insert(0, number)
        sb.Insert(5, price)
        sb.Insert(10, isActive)
        sb.Insert(3, character)
        
        ' Correct: Using string literals - should not be detected
        sb.Append("Hello World")
        sb.Insert(0, "Prefix: ")
        
        ' Correct: Using ToString() for custom formatting - should not be detected
        sb.Append(number.ToString("N2"))
        sb.Append(price.ToString("C"))
        
        ' Correct: Using ToString() on complex objects - should not be detected
        Dim customer As New Customer()
        sb.Append(customer.ToString())
        
    End Sub
    
End Class
