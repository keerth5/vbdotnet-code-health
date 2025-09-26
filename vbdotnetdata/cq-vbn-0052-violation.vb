' Test file for cq-vbn-0052: Specify IFormatProvider
' Rule should detect culture-sensitive parsing and formatting without IFormatProvider

Imports System
Imports System.Globalization

Public Class FormatProviderExamples
    
    Public Sub ParseNumbers()
        Dim numberText As String = "123.45"
        
        ' Violation 1: Integer.Parse without IFormatProvider
        Dim intValue = Integer.Parse("123")
        
        ' Violation 2: Double.Parse without IFormatProvider
        Dim doubleValue = Double.Parse(numberText)
        
        ' Violation 3: Decimal.Parse without IFormatProvider
        Dim decimalValue = Decimal.Parse("456.78")
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim intWithProvider = Integer.Parse("123", CultureInfo.InvariantCulture)
        Dim doubleWithProvider = Double.Parse(numberText, CultureInfo.CurrentCulture)
        
    End Sub
    
    Public Sub ParseDates()
        Dim dateText As String = "2023-12-25"
        
        ' Violation 4: DateTime.Parse without IFormatProvider
        Dim dateValue = DateTime.Parse(dateText)
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim dateWithProvider = DateTime.Parse(dateText, CultureInfo.InvariantCulture)
        
    End Sub
    
    Public Sub FormatStrings()
        Dim number As Integer = 123
        Dim value As Double = 456.78
        
        ' Violation 5: ToString with format string but no IFormatProvider
        Dim formatted1 = number.ToString("D5")
        
        ' Violation 6: ToString with format string but no IFormatProvider
        Dim formatted2 = value.ToString("F2")
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim properFormat1 = number.ToString("D5", CultureInfo.InvariantCulture)
        Dim properFormat2 = value.ToString("F2", CultureInfo.CurrentCulture)
        
        ' This should NOT be detected - ToString without format string (covered by cq-vbn-0051)
        Dim simple = number.ToString()
        
    End Sub
    
    Public Sub MoreParsingExamples()
        
        ' Violation 7: Another Integer.Parse
        Dim result1 = Integer.Parse("789")
        
        ' Violation 8: Another Double.Parse
        Dim result2 = Double.Parse("12.34")
        
        ' Violation 9: Another Decimal.Parse
        Dim result3 = Decimal.Parse("56.78")
        
        ' This should NOT be detected - TryParse methods (different pattern)
        Dim success As Boolean
        Dim tryResult As Integer
        success = Integer.TryParse("123", tryResult)
        
    End Sub
    
    Public Sub DateTimeFormatting()
        Dim now As DateTime = DateTime.Now
        
        ' Violation 10: DateTime ToString with format but no IFormatProvider
        Dim dateFormatted = now.ToString("yyyy-MM-dd")
        
        ' This should NOT be detected - proper usage
        Dim properDateFormat = now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        
    End Sub
    
End Class

Public Class AdditionalFormatExamples
    
    Public Sub ProcessUserInput(input As String)
        
        ' Violation 11: Parse in conditional
        If Integer.Parse(input) > 100 Then
            Console.WriteLine("Large number")
        End If
        
        ' Violation 12: Parse in calculation
        Dim calculated = Double.Parse(input) * 2.5
        
        ' This should NOT be detected - proper error handling with provider
        Try
            Dim safeValue = Integer.Parse(input, CultureInfo.InvariantCulture)
        Catch ex As FormatException
            Console.WriteLine("Invalid format")
        End Try
        
    End Sub
    
End Class
