' Test file for cq-vbn-0036: URI properties should not be strings
' Rule should detect properties that represent URIs but use String instead of Uri type

Imports System

Public Class UriPropertyExamples
    
    ' Violation 1: Public property with URI as String
    Public Property BaseUri As String
    
    ' Violation 2: Protected property with URL as String
    Protected Property ServiceUrl As String
    
    ' Violation 3: Friend property with URI as String
    Friend Property ApiUri As String
    
    ' Violation 4: Property with downloadUrl name
    Public Property DownloadUrl As String
    
    ' Violation 5: Property with redirectUri name
    Public Property RedirectUri As String
    
    ' Violation 6: Property with endpointUrl name
    Protected Property EndpointUrl As String
    
    ' This should NOT be detected - property with Uri type (correct)
    Public Property BaseUriCorrect As Uri
    
    ' This should NOT be detected - private property (less critical)
    Private Property InternalUri As String
    
    ' This should NOT be detected - property without URI/URL in name
    Public Property Address As String
    
    ' This should NOT be detected - property with URI in name but not representing URI
    Public Property UriProcessor As String
    
End Class

Public Class WebConfiguration
    
    ' Violation 7: Another class with URI property violations
    Public Property ServerUrl As String
    
    ' Violation 8: Property for API endpoint
    Friend Property ApiEndpointUri As String
    
    ' Violation 9: Property for callback URL
    Public Property CallbackUrl As String
    
End Class

Public Class ConnectionSettings
    
    ' Violation 10: Connection URI property
    Public Property ConnectionUri As String
    
    ' Violation 11: Database URL property
    Protected Property DatabaseUrl As String
    
    ' This should NOT be detected - timeout property (not URI related)
    Public Property Timeout As Integer
    
    ' This should NOT be detected - proper Uri property
    Public Property ServerUri As Uri
    
End Class

Public Class ApiClient
    
    ' Violation 12: Base URL for API
    Public Property BaseUrl As String
        Get
            Return _baseUrl
        End Get
        Set(value As String)
            _baseUrl = value
        End Set
    End Property
    
    ' Violation 13: Authentication URI
    Friend Property AuthUri As String
    
    Private _baseUrl As String
    
End Class
