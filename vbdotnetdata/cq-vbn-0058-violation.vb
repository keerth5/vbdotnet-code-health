' Test file for cq-vbn-0058: Use recommended overloads for culture-aware APIs
' Rule should detect culture-sensitive parsing methods without IFormatProvider

Imports System
Imports System.Globalization

Public Class CultureAwareApiExamples
    
    Public Sub ParseDateTimes()
        Dim dateText As String = "2023-12-25"
        
        ' Violation 1: DateTime.Parse without IFormatProvider
        Dim date1 = DateTime.Parse(dateText)
        
        ' Violation 2: Convert.ToDateTime without IFormatProvider
        Dim date2 = Convert.ToDateTime(dateText)
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim properDate1 = DateTime.Parse(dateText, CultureInfo.InvariantCulture)
        Dim properDate2 = Convert.ToDateTime(dateText, CultureInfo.CurrentCulture)
        
    End Sub
    
    Public Sub ParseNumbers()
        Dim numberText As String = "123.45"
        
        ' Violation 3: Decimal.Parse without IFormatProvider
        Dim decimal1 = Decimal.Parse(numberText)
        
        ' Violation 4: Double.Parse without IFormatProvider
        Dim double1 = Double.Parse(numberText)
        
        ' Violation 5: Single.Parse without IFormatProvider
        Dim single1 = Single.Parse(numberText)
        
        ' Violation 6: Integer.Parse without IFormatProvider
        Dim integer1 = Integer.Parse("123")
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim properDecimal = Decimal.Parse(numberText, CultureInfo.InvariantCulture)
        Dim properDouble = Double.Parse(numberText, CultureInfo.CurrentCulture)
        
    End Sub
    
    Public Sub ProcessUserInput(input As String)
        
        ' Violation 7: DateTime.Parse in conditional
        If DateTime.Parse(input) > DateTime.Now Then
            Console.WriteLine("Future date")
        End If
        
        ' Violation 8: Integer.Parse in calculation
        Dim result = Integer.Parse(input) * 2
        
        ' This should NOT be detected - proper IFormatProvider usage
        If DateTime.Parse(input, CultureInfo.InvariantCulture) > DateTime.Now Then
            Console.WriteLine("Future date (invariant)")
        End If
        
    End Sub
    
    Public Sub ConvertValues()
        Dim textValue As String = "456.78"
        
        ' Violation 9: Convert.ToDateTime in assignment
        Dim convertedDate = Convert.ToDateTime("2023-01-01")
        
        ' Violation 10: Decimal.Parse in method call
        Console.WriteLine(Decimal.Parse(textValue))
        
        ' This should NOT be detected - proper IFormatProvider usage
        Console.WriteLine(Decimal.Parse(textValue, CultureInfo.InvariantCulture))
        
    End Sub
    
    Public Function CalculateTotal(priceText As String, quantityText As String) As Decimal
        
        ' Violation 11: Double.Parse in calculation
        Dim price = Double.Parse(priceText)
        
        ' Violation 12: Integer.Parse in calculation
        Dim quantity = Integer.Parse(quantityText)
        
        Return CDec(price * quantity)
        
    End Function
    
    Public Sub ProcessConfigurationFile()
        Dim timeoutValue As String = "30.5"
        Dim retryCountValue As String = "5"
        
        ' Violation 13: Single.Parse for configuration
        Dim timeout = Single.Parse(timeoutValue)
        
        ' Violation 14: Integer.Parse for configuration
        Dim retryCount = Integer.Parse(retryCountValue)
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim properTimeout = Single.Parse(timeoutValue, CultureInfo.InvariantCulture)
        
    End Sub
    
End Class

Public Class DatabaseExamples
    
    Public Sub ProcessDatabaseValues(row As Object())
        
        ' Violation 15: DateTime.Parse from database
        Dim createdDate = DateTime.Parse(row(0).ToString())
        
        ' Violation 16: Decimal.Parse from database
        Dim amount = Decimal.Parse(row(1).ToString())
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim properDate = DateTime.Parse(row(0).ToString(), CultureInfo.InvariantCulture)
        
    End Sub
    
    Public Sub ValidateNumericInput(input As String)
        
        Try
            ' Violation 17: Double.Parse in validation
            Dim value = Double.Parse(input)
            Console.WriteLine("Valid number: " & value)
        Catch ex As FormatException
            Console.WriteLine("Invalid number format")
        End Try
        
        ' This should NOT be detected - TryParse methods (different pattern)
        Dim result As Double
        If Double.TryParse(input, result) Then
            Console.WriteLine("Parsed successfully: " & result)
        End If
        
    End Sub
    
End Class

Public Class FileProcessingExamples
    
    Public Sub ProcessCsvFile(csvLine As String)
        Dim fields() As String = csvLine.Split(","c)
        
        ' Violation 18: DateTime.Parse from CSV
        Dim date1 = DateTime.Parse(fields(0))
        
        ' Violation 19: Decimal.Parse from CSV
        Dim amount = Decimal.Parse(fields(1))
        
        ' This should NOT be detected - proper IFormatProvider usage
        Dim properDate = DateTime.Parse(fields(0), CultureInfo.InvariantCulture)
        
    End Sub
    
End Class
