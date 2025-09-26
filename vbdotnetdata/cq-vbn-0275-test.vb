' Test file for cq-vbn-0275: Pass System.Uri objects instead of strings
' Rule should detect: Methods with URI-related string parameters when Uri overloads should be used

Imports System

' VIOLATION: Method with URI string parameter
Public Class BadUriClass1
    
    Public Function ProcessUrl(url As String) As String
        Return url.ToLower()
    End Function
    
    Public Sub ConnectToUri(uri As String)
        Console.WriteLine("Connecting to: " & uri)
    End Sub
    
End Class

' VIOLATION: Method with URN string parameter
Friend Class BadUriClass2
    
    Protected Function ValidateUrn(urn As String) As Boolean
        Return urn.StartsWith("urn:")
    End Function
    
    Public Sub HandleURL(webUrl As String)
        Console.WriteLine("Handling: " & webUrl)
    End Sub
    
End Class

' VIOLATION: Method with multiple URI-related parameters
Public Class BadUriClass3
    
    Public Function CompareUris(sourceUri As String, targetURI As String) As Boolean
        Return sourceUri.Equals(targetURI, StringComparison.OrdinalIgnoreCase)
    End Function
    
    Friend Sub ProcessUrls(baseUrl As String, relativeUrl As String)
        Dim combined As String = baseUrl & "/" & relativeUrl
        Console.WriteLine(combined)
    End Sub
    
End Class

' GOOD: Method with Uri parameter - should NOT be flagged
Public Class GoodUriClass1
    
    Public Function ProcessUrl(url As Uri) As String
        Return url.ToString().ToLower()
    End Function
    
    Public Sub ConnectToUri(uri As Uri)
        Console.WriteLine("Connecting to: " & uri.ToString())
    End Sub
    
End Class

' GOOD: Method with non-URI string parameters - should NOT be flagged
Public Class NonUriClass
    
    Public Function ProcessName(name As String) As String
        Return name.ToUpper()
    End Function
    
    Public Sub DisplayMessage(message As String)
        Console.WriteLine(message)
    End Sub
    
End Class

' GOOD: Method overloads with both string and Uri versions - should NOT be flagged
Public Class OverloadedUriClass
    
    Public Function ProcessUrl(url As String) As String
        Return ProcessUrl(New Uri(url))
    End Function
    
    Public Function ProcessUrl(url As Uri) As String
        Return url.ToString().ToLower()
    End Function
    
End Class
