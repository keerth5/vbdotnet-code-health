' Test file for cq-vbn-0035: URI return values should not be strings
' Rule should detect functions that return URI/URL values as String instead of Uri type

Imports System

Public Class UriReturnExamples
    
    ' Violation 1: Public function returning URI as String
    Public Function GetBaseUri() As String
        Return "https://api.example.com"
    End Function
    
    ' Violation 2: Protected function returning URL as String
    Protected Function BuildUrl(path As String) As String
        Return "https://example.com/" & path
    End Function
    
    ' Violation 3: Friend function returning URI as String
    Friend Function GetServiceUri(service As String) As String
        Return $"https://services.example.com/{service}"
    End Function
    
    ' Violation 4: Function with Uri in name returning String
    Public Function CreateRequestUri(endpoint As String) As String
        Return "https://api.example.com/v1/" & endpoint
    End Function
    
    ' Violation 5: Function returning download URL
    Public Function GetDownloadUrl(fileId As String) As String
        Return $"https://downloads.example.com/files/{fileId}"
    End Function
    
    ' Violation 6: Function returning redirect URI
    Protected Function GetRedirectUri(returnPath As String) As String
        Return "https://example.com/auth/callback?return=" & returnPath
    End Function
    
    ' This should NOT be detected - function returning Uri type (correct)
    Public Function GetBaseUriCorrect() As Uri
        Return New Uri("https://api.example.com")
    End Function
    
    ' This should NOT be detected - function with URI in name but not returning URI
    Public Function ProcessUriData(uri As String) As Boolean
        Return uri.Length > 0
    End Function
    
    ' This should NOT be detected - private function (less critical)
    Private Function InternalGetUri() As String
        Return "internal://uri"
    End Function
    
    ' This should NOT be detected - function returning non-URI string
    Public Function GetName() As String
        Return "Example Name"
    End Function
    
    ' This should NOT be detected - function with URL processing but returning other type
    Public Function ValidateUrl(url As String) As Boolean
        Return url.StartsWith("http")
    End Function
    
End Class

Public Class WebApiClient
    
    ' Violation 7: Another class with URI return violations
    Public Function GetApiUrl(version As String) As String
        Return $"https://api.example.com/{version}"
    End Function
    
    ' Violation 8: Function returning endpoint URI
    Friend Function BuildEndpointUri(resource As String, id As Integer) As String
        Return $"https://api.example.com/{resource}/{id}"
    End Function
    
End Class

Public Class UrlBuilder
    
    ' Violation 9: Function with URL in class and method name
    Public Function BuildCompleteUrl(baseUrl As String, parameters As String) As String
        Return baseUrl & "?" & parameters
    End Function
    
End Class
