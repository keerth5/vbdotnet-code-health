' VB.NET test file for cq-vbn-0299: Set MaxResponseHeadersLength properly
' This rule detects incorrect MaxResponseHeadersLength values (should be in kilobytes)

Imports System
Imports System.Net.Http

Public Class BadMaxResponseHeadersLength
    ' BAD: MaxResponseHeadersLength set to byte values instead of kilobytes
    Public Sub TestBadHeaderLength()
        Dim client As New HttpClient()
        
        ' Violation: Value too large (likely in bytes, not kilobytes)
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 1048576 ' 1MB in bytes
        
        ' Violation: Another large value
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 2097152 ' 2MB in bytes
        
        ' Violation: Very large value
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 5000000 ' 5MB in bytes
    End Sub
    
    Public Sub TestMoreBadValues()
        Dim handler As New HttpClientHandler()
        
        ' Violation: Large byte value
        Dim client As New HttpClient(handler)
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 10000000
        
        ' Violation: Another problematic value
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 8388608
    End Sub
    
    ' GOOD: MaxResponseHeadersLength set to appropriate kilobyte values
    Public Sub TestGoodHeaderLength()
        Dim client As New HttpClient()
        
        ' Good: 1024 KB (1MB)
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 1024
        
        ' Good: 2048 KB (2MB)
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 2048
        
        ' Good: 512 KB
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 512
        
        ' Good: 256 KB
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 256
        
        ' Good: Default reasonable value
        client.DefaultRequestHeaders.MaxResponseHeadersLength = 64
    End Sub
End Class
