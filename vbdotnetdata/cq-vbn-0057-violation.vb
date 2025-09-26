' Test file for cq-vbn-0057: Specify CultureInfo for overloads without StringComparison
' Rule should detect String.Compare calls with boolean parameter that need CultureInfo

Imports System
Imports System.Globalization

Public Class StringCompareOverloadExamples
    
    Public Sub CompareWithBooleanParameter()
        Dim text1 As String = "Hello"
        Dim text2 As String = "HELLO"
        
        ' Violation 1: String.Compare with ignoreCase True but no CultureInfo
        Dim result1 = String.Compare(text1, text2, True)
        
        ' Violation 2: String.Compare with ignoreCase False but no CultureInfo
        Dim result2 = String.Compare(text1, text2, False)
        
        ' This should NOT be detected - proper CultureInfo usage
        Dim properResult1 = String.Compare(text1, text2, True, CultureInfo.CurrentCulture)
        Dim properResult2 = String.Compare(text1, text2, False, CultureInfo.InvariantCulture)
        
        ' This should NOT be detected - StringComparison enum usage
        Dim enumResult = String.Compare(text1, text2, StringComparison.OrdinalIgnoreCase)
        
    End Sub
    
    Public Sub CaseInsensitiveComparison()
        Dim userInput As String = "yes"
        Dim expectedValue As String = "YES"
        
        ' Violation 3: Case-insensitive comparison without CultureInfo
        If String.Compare(userInput, expectedValue, True) = 0 Then
            Console.WriteLine("User confirmed")
        End If
        
        ' This should NOT be detected - proper CultureInfo specification
        If String.Compare(userInput, expectedValue, True, CultureInfo.InvariantCulture) = 0 Then
            Console.WriteLine("User confirmed (invariant)")
        End If
        
    End Sub
    
    Public Sub CaseSensitiveComparison()
        Dim password As String = "SecretPassword"
        Dim inputPassword As String = "secretpassword"
        
        ' Violation 4: Case-sensitive comparison without CultureInfo
        If String.Compare(password, inputPassword, False) <> 0 Then
            Console.WriteLine("Password mismatch")
        End If
        
        ' This should NOT be detected - proper CultureInfo specification
        If String.Compare(password, inputPassword, False, CultureInfo.InvariantCulture) <> 0 Then
            Console.WriteLine("Password mismatch (invariant)")
        End If
        
    End Sub
    
    Public Function ValidateInput(input As String, validValue As String) As Boolean
        
        ' Violation 5: String.Compare in return statement with boolean
        Return String.Compare(input, validValue, True) = 0
        
    End Function
    
    Public Sub ProcessConfigurationValues()
        Dim configValue As String = "EnableFeature"
        Dim userSetting As String = "enablefeature"
        
        ' Violation 6: Configuration comparison without CultureInfo
        Dim isEnabled = String.Compare(configValue, userSetting, True) = 0
        
        If isEnabled Then
            Console.WriteLine("Feature is enabled")
        End If
        
        ' This should NOT be detected - proper CultureInfo usage
        Dim properIsEnabled = String.Compare(configValue, userSetting, True, CultureInfo.InvariantCulture) = 0
        
    End Sub
    
    Public Sub SortingWithCaseHandling()
        Dim items() As String = {"Apple", "banana", "Cherry"}
        
        ' Violation 7: Sorting with case-insensitive comparison without CultureInfo
        Array.Sort(items, Function(x, y) String.Compare(x, y, True))
        
        ' This should NOT be detected - proper CultureInfo usage
        Array.Sort(items, Function(x, y) String.Compare(x, y, True, CultureInfo.CurrentCulture))
        
    End Sub
    
    Public Sub DatabaseComparison()
        Dim dbColumn As String = "UserName"
        Dim searchValue As String = "username"
        
        ' Violation 8: Database field comparison without CultureInfo
        If String.Compare(dbColumn, searchValue, True) = 0 Then
            Console.WriteLine("Column found")
        End If
        
        ' This should NOT be detected - proper CultureInfo specification
        If String.Compare(dbColumn, searchValue, True, CultureInfo.InvariantCulture) = 0 Then
            Console.WriteLine("Column found (invariant)")
        End If
        
    End Sub
    
End Class

Public Class ValidationExamples
    
    Public Sub ValidateUserChoices()
        Dim userChoice As String = "Yes"
        Dim validChoices() As String = {"yes", "no", "maybe"}
        
        For Each choice In validChoices
            ' Violation 9: Validation with case-insensitive comparison
            If String.Compare(userChoice, choice, True) = 0 Then
                Console.WriteLine("Valid choice: " & userChoice)
                Exit For
            End If
        Next
        
        ' This should NOT be detected - proper CultureInfo usage
        For Each choice In validChoices
            If String.Compare(userChoice, choice, True, CultureInfo.CurrentCulture) = 0 Then
                Console.WriteLine("Valid choice (culture-aware): " & userChoice)
                Exit For
            End If
        Next
        
    End Sub
    
    Public Sub ProcessFileExtensions()
        Dim fileName As String = "document.PDF"
        Dim allowedExtension As String = ".pdf"
        
        ' Violation 10: File extension comparison without CultureInfo
        If String.Compare(fileName.Substring(fileName.LastIndexOf(".")), allowedExtension, True) = 0 Then
            Console.WriteLine("Valid file type")
        End If
        
    End Sub
    
End Class
