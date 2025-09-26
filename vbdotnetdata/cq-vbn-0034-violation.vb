' Test file for cq-vbn-0034: URI parameters should not be strings
' Rule should detect methods with URI/URL parameters that use String instead of Uri type

Imports System

Public Class UriParameterExamples
    
    ' Violation 1: Public method with uri parameter as String
    Public Sub ProcessUri(uri As String)
        Console.WriteLine("Processing URI: " & uri)
    End Sub
    
    ' Violation 2: Protected method with url parameter as String
    Protected Function ValidateUrl(url As String) As Boolean
        Return url.StartsWith("http")
    End Function
    
    ' Violation 3: Friend method with URI parameter as String
    Friend Sub DownloadFromUri(downloadUri As String)
        Console.WriteLine("Downloading from: " & downloadUri)
    End Sub
    
    ' Violation 4: Public method with multiple parameters including URI
    Public Function ConnectToUrl(serverUrl As String, timeout As Integer) As Boolean
        Console.WriteLine($"Connecting to {serverUrl} with timeout {timeout}")
        Return True
    End Function
    
    ' Violation 5: Method with baseUri parameter
    Public Sub SetBaseUri(baseUri As String)
        Console.WriteLine("Setting base URI: " & baseUri)
    End Sub
    
    ' Violation 6: Method with requestUrl parameter
    Protected Function MakeRequest(requestUrl As String, data As String) As String
        Console.WriteLine($"Making request to {requestUrl}")
        Return "Response"
    End Function
    
    ' This should NOT be detected - method with Uri type parameter (correct)
    Public Sub ProcessUriCorrect(uri As Uri)
        Console.WriteLine("Processing URI: " & uri.ToString())
    End Sub
    
    ' This should NOT be detected - method with URL in name but not parameter
    Public Sub ProcessUrlData(data As String)
        Console.WriteLine("Processing data: " & data)
    End Sub
    
    ' This should NOT be detected - private method (less critical)
    Private Sub InternalProcessUri(uri As String)
        Console.WriteLine("Internal processing: " & uri)
    End Sub
    
    ' This should NOT be detected - parameter name doesn't contain uri/url
    Public Sub ProcessAddress(address As String)
        Console.WriteLine("Processing address: " & address)
    End Sub
    
    ' This should NOT be detected - method with Uri return type and parameter
    Public Function CreateUri(baseUri As Uri, relativePath As String) As Uri
        Return New Uri(baseUri, relativePath)
    End Function
    
End Class

Public Class WebServiceClient
    
    ' Violation 7: Another class with URI parameter violations
    Public Sub CallWebService(serviceUrl As String, apiKey As String)
        Console.WriteLine($"Calling service at {serviceUrl} with key {apiKey}")
    End Sub
    
    ' Violation 8: Method with endpoint URI
    Friend Function GetEndpointUri(endpointUri As String) As String
        Return endpointUri.ToLower()
    End Function
    
End Class
