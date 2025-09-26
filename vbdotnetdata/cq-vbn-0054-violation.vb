' Test file for cq-vbn-0054: Normalize strings to uppercase
' Rule should detect ToLower usage with CultureInfo (should use ToUpper instead)

Imports System
Imports System.Globalization

Public Class StringNormalizationExamples
    
    Public Sub ProcessStrings()
        Dim text As String = "Hello World"
        
        ' Violation 1: ToLower with CultureInfo.InvariantCulture (should use ToUpper)
        Dim normalized1 = text.ToLower(CultureInfo.InvariantCulture)
        
        ' Violation 2: ToLower with CultureInfo.CurrentCulture (should use ToUpper)
        Dim normalized2 = text.ToLower(CultureInfo.CurrentCulture)
        
        ' This should NOT be detected - proper ToUpper usage
        Dim properNormalized1 = text.ToUpper(CultureInfo.InvariantCulture)
        Dim properNormalized2 = text.ToUpper(CultureInfo.CurrentCulture)
        
        ' This should NOT be detected - ToLower without CultureInfo (covered by cq-vbn-0051)
        Dim simple = text.ToLower()
        
    End Sub
    
    Public Function NormalizeForComparison(input As String) As String
        
        ' Violation 3: ToLower with specific CultureInfo
        Return input.ToLower(CultureInfo.InvariantCulture)
        
    End Function
    
    Public Sub ProcessUserInput(userInput As String)
        
        ' Violation 4: ToLower with CurrentCulture in conditional
        If userInput.ToLower(CultureInfo.CurrentCulture) = "yes" Then
            Console.WriteLine("User confirmed")
        End If
        
        ' This should NOT be detected - proper ToUpper usage
        If userInput.ToUpper(CultureInfo.InvariantCulture) = "YES" Then
            Console.WriteLine("User confirmed (uppercase)")
        End If
        
    End Sub
    
    Public Sub StringArrayProcessing()
        Dim items() As String = {"Apple", "Banana", "Cherry"}
        
        For Each item In items
            ' Violation 5: ToLower with CultureInfo in loop
            Dim normalized = item.ToLower(CultureInfo.InvariantCulture)
            Console.WriteLine(normalized)
        Next
        
        ' This should NOT be detected - proper ToUpper usage
        For Each item In items
            Dim properNormalized = item.ToUpper(CultureInfo.InvariantCulture)
            Console.WriteLine(properNormalized)
        Next
        
    End Sub
    
    Public Sub DatabaseComparison()
        Dim searchTerm As String = "SearchValue"
        
        ' Violation 6: ToLower with CultureInfo for database comparison
        Dim dbQuery = searchTerm.ToLower(CultureInfo.InvariantCulture)
        
        ' This should NOT be detected - ToUpper is recommended
        Dim properDbQuery = searchTerm.ToUpper(CultureInfo.InvariantCulture)
        
    End Sub
    
End Class

Public Class CultureSpecificExamples
    
    Public Sub ProcessInternationalText()
        Dim turkishText As String = "İstanbul"
        Dim germanText As String = "Straße"
        
        ' Violation 7: ToLower with Turkish culture
        Dim turkishLower = turkishText.ToLower(New CultureInfo("tr-TR"))
        
        ' Violation 8: ToLower with German culture
        Dim germanLower = germanText.ToLower(New CultureInfo("de-DE"))
        
        ' This should NOT be detected - proper ToUpper usage
        Dim turkishUpper = turkishText.ToUpper(New CultureInfo("tr-TR"))
        Dim germanUpper = germanText.ToUpper(New CultureInfo("de-DE"))
        
    End Sub
    
    Public Sub CacheKeyGeneration(input As String)
        
        ' Violation 9: ToLower for cache key generation (should use ToUpper)
        Dim cacheKey = "cache_" & input.ToLower(CultureInfo.InvariantCulture)
        
        ' This should NOT be detected - proper ToUpper for cache keys
        Dim properCacheKey = "cache_" & input.ToUpper(CultureInfo.InvariantCulture)
        
    End Sub
    
End Class
