' Test file for cq-vbn-0055: Use ordinal StringComparison
' Rule should detect non-ordinal StringComparison usage for non-linguistic comparisons

Imports System

Public Class OrdinalComparisonExamples
    
    Public Sub CompareIdentifiers()
        Dim id1 As String = "USER123"
        Dim id2 As String = "user123"
        
        ' Violation 1: Equals with CurrentCulture (should use Ordinal for identifiers)
        Dim isEqual = id1.Equals(id2, StringComparison.CurrentCulture)
        
        ' Violation 2: CompareTo with InvariantCulture (should use Ordinal)
        Dim comparison = id1.CompareTo(id2, StringComparison.InvariantCulture)
        
        ' Violation 3: StartsWith with CurrentCulture
        Dim startsWith = id1.StartsWith("USER", StringComparison.CurrentCulture)
        
        ' This should NOT be detected - proper Ordinal usage
        Dim properEquals = id1.Equals(id2, StringComparison.Ordinal)
        Dim properStartsWith = id1.StartsWith("USER", StringComparison.OrdinalIgnoreCase)
        
    End Sub
    
    Public Sub ProcessFilePaths()
        Dim filePath As String = "C:\Temp\file.txt"
        Dim extension As String = ".txt"
        
        ' Violation 4: EndsWith with CurrentCulture (should use Ordinal for file paths)
        Dim hasExtension = filePath.EndsWith(extension, StringComparison.CurrentCulture)
        
        ' Violation 5: IndexOf with InvariantCulture
        Dim index = filePath.IndexOf("\", StringComparison.InvariantCulture)
        
        ' This should NOT be detected - proper Ordinal usage
        Dim properEndsWith = filePath.EndsWith(extension, StringComparison.OrdinalIgnoreCase)
        Dim properIndex = filePath.IndexOf("\", StringComparison.Ordinal)
        
    End Sub
    
    Public Sub ValidateTokens()
        Dim token As String = "AUTH_TOKEN_12345"
        Dim prefix As String = "AUTH_"
        
        ' Violation 6: StartsWith with CurrentCulture for token validation
        If token.StartsWith(prefix, StringComparison.CurrentCulture) Then
            Console.WriteLine("Valid token")
        End If
        
        ' Violation 7: Equals with InvariantCulture for exact token match
        If token.Equals("AUTH_TOKEN_12345", StringComparison.InvariantCulture) Then
            Console.WriteLine("Exact match")
        End If
        
        ' This should NOT be detected - proper Ordinal usage
        If token.StartsWith(prefix, StringComparison.Ordinal) Then
            Console.WriteLine("Valid token (ordinal)")
        End If
        
    End Sub
    
    Public Sub ProcessConfigurationKeys()
        Dim configKey As String = "database.connection.timeout"
        Dim searchKey As String = "database"
        
        ' Violation 8: Contains-like behavior with IndexOf and CurrentCulture
        Dim keyIndex = configKey.IndexOf(searchKey, StringComparison.CurrentCulture)
        
        ' Violation 9: EndsWith with InvariantCulture for config keys
        Dim isTimeoutKey = configKey.EndsWith("timeout", StringComparison.InvariantCulture)
        
        ' This should NOT be detected - proper Ordinal usage
        Dim properIndex = configKey.IndexOf(searchKey, StringComparison.Ordinal)
        Dim properEndsWith = configKey.EndsWith("timeout", StringComparison.OrdinalIgnoreCase)
        
    End Sub
    
    Public Sub ProcessApiEndpoints()
        Dim endpoint As String = "/api/v1/users"
        Dim baseApi As String = "/api/"
        
        ' Violation 10: StartsWith with CurrentCulture for API endpoints
        If endpoint.StartsWith(baseApi, StringComparison.CurrentCulture) Then
            Console.WriteLine("API endpoint")
        End If
        
        ' This should NOT be detected - proper Ordinal usage
        If endpoint.StartsWith(baseApi, StringComparison.Ordinal) Then
            Console.WriteLine("API endpoint (ordinal)")
        End If
        
    End Sub
    
End Class

Public Class DatabaseIdentifierExamples
    
    Public Sub CompareTableNames()
        Dim tableName As String = "USER_ACCOUNTS"
        Dim searchTable As String = "user_accounts"
        
        ' Violation 11: Equals with CurrentCulture for database identifiers
        Dim isMatch = tableName.Equals(searchTable, StringComparison.CurrentCulture)
        
        ' Violation 12: CompareTo with InvariantCulture for sorting table names
        Dim sortOrder = tableName.CompareTo("SYSTEM_CONFIG", StringComparison.InvariantCulture)
        
        ' This should NOT be detected - proper Ordinal usage
        Dim properMatch = tableName.Equals(searchTable, StringComparison.OrdinalIgnoreCase)
        
    End Sub
    
End Class
