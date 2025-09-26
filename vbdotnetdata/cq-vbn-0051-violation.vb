' Test file for cq-vbn-0051: Specify CultureInfo
' Rule should detect culture-sensitive string operations without CultureInfo

Imports System
Imports System.Globalization

Public Class CultureInfoExamples
    
    Public Sub ProcessStrings()
        Dim text As String = "Hello World"
        
        ' Violation 1: ToLower without CultureInfo
        Dim lowerText = text.ToLower()
        
        ' Violation 2: ToUpper without CultureInfo
        Dim upperText = text.ToUpper()
        
        ' Violation 3: ToString without CultureInfo
        Dim number As Integer = 123
        Dim numberString = number.ToString()
        
        ' This should NOT be detected - proper CultureInfo usage
        Dim lowerWithCulture = text.ToLower(CultureInfo.InvariantCulture)
        Dim upperWithCulture = text.ToUpper(CultureInfo.CurrentCulture)
        Dim numberWithCulture = number.ToString(CultureInfo.InvariantCulture)
        
    End Sub
    
    Public Function FormatData(data As String) As String
        
        ' Violation 4: Another ToLower without CultureInfo
        Dim processed = data.ToLower()
        
        ' Violation 5: ToUpper in different context
        Return processed.ToUpper()
        
    End Function
    
    Public Sub ProcessNumbers()
        Dim value As Double = 123.45
        
        ' Violation 6: ToString without CultureInfo
        Dim formatted = value.ToString()
        
        ' This should NOT be detected - with format string and CultureInfo
        Dim properFormat = value.ToString("F2", CultureInfo.InvariantCulture)
        
    End Sub
    
    Public Sub StringManipulation()
        Dim input As String = "Test String"
        
        ' Violation 7: ToLower in assignment
        Dim result = input.ToLower()
        
        ' Violation 8: ToUpper in method call
        Console.WriteLine(input.ToUpper())
        
        ' This should NOT be detected - proper usage
        Console.WriteLine(input.ToLower(CultureInfo.CurrentCulture))
        
    End Sub
    
    Public Sub DateTimeFormatting()
        Dim now As DateTime = DateTime.Now
        
        ' Violation 9: DateTime ToString without CultureInfo
        Dim dateString = now.ToString()
        
        ' This should NOT be detected - with format and CultureInfo
        Dim properDateString = now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        
    End Sub
    
End Class

Public Class MoreExamples
    
    Public Sub AdditionalCases()
        Dim text As String = "Sample"
        
        ' Violation 10: ToLower in conditional
        If text.ToLower() = "sample" Then
            Console.WriteLine("Match found")
        End If
        
        ' Violation 11: ToUpper in string concatenation
        Dim result = "Prefix: " & text.ToUpper()
        
        ' This should NOT be detected - method without culture-sensitive operations
        Dim length = text.Length
        Dim substring = text.Substring(0, 3)
        
    End Sub
    
End Class
