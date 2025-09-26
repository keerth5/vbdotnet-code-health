' VB.NET test file for cq-vbn-0247: Set HttpOnly to true for HttpCookie
' This rule detects HttpCookie instances that don't have HttpOnly set to true or have it explicitly set to false

Imports System
Imports System.Web

' Violation: HttpCookie without HttpOnly set to true
Public Class InsecureCookies1
    Public Sub CreateCookieWithoutHttpOnly()
        ' Violation: Cookie created without setting HttpOnly to true
        Dim cookie As New HttpCookie("sessionId", "12345")
        cookie.Expires = DateTime.Now.AddHours(1)
        
        ' HttpOnly is not set, making it vulnerable to XSS
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: HttpCookie with HttpOnly explicitly set to False
Public Class InsecureCookies2
    Public Sub CreateCookieWithHttpOnlyFalse()
        Dim cookie As New HttpCookie("userId", "user123")
        
        ' Violation: HttpOnly explicitly set to False
        cookie.HttpOnly = False
        cookie.Secure = True
        
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Multiple cookies without HttpOnly
Public Class InsecureCookies3
    Public Sub CreateMultipleCookiesWithoutHttpOnly()
        ' Violation: First cookie without HttpOnly
        Dim sessionCookie As New HttpCookie("PHPSESSID", "sess123")
        sessionCookie.Path = "/"
        
        ' Violation: Second cookie without HttpOnly
        Dim userCookie As New HttpCookie("username", "john")
        userCookie.Domain = "example.com"
        
        ' Violation: Third cookie with HttpOnly = False
        Dim prefCookie As New HttpCookie("preferences", "theme=dark")
        prefCookie.HttpOnly = False
        
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(sessionCookie)
        response.Cookies.Add(userCookie)
        response.Cookies.Add(prefCookie)
    End Sub
End Class

' Violation: Cookie with other security settings but missing HttpOnly
Public Class InsecureCookies4
    Public Sub CreateCookieWithPartialSecurity()
        Dim cookie As New HttpCookie("authToken", "token123")
        
        ' Has Secure but missing HttpOnly
        cookie.Secure = True
        cookie.SameSite = SameSiteMode.Strict
        cookie.Expires = DateTime.Now.AddMinutes(30)
        
        ' Violation: HttpOnly not set to true
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Cookie in conditional logic without HttpOnly
Public Class InsecureCookies5
    Public Sub ConditionalCookieCreation(rememberUser As Boolean)
        If rememberUser Then
            ' Violation: Cookie created conditionally without HttpOnly
            Dim rememberCookie As New HttpCookie("rememberMe", "true")
            rememberCookie.Expires = DateTime.Now.AddDays(30)
            
            Dim response As HttpResponse = Nothing
            response.Cookies.Add(rememberCookie)
        End If
    End Sub
End Class

' Violation: Cookie created in loop without HttpOnly
Public Class InsecureCookies6
    Public Sub CreateCookiesInLoop()
        Dim cookieData As String() = {"data1", "data2", "data3"}
        Dim response As HttpResponse = Nothing
        
        For i As Integer = 0 To cookieData.Length - 1
            ' Violation: Cookies created in loop without HttpOnly
            Dim cookie As New HttpCookie($"cookie{i}", cookieData(i))
            cookie.Secure = True
            response.Cookies.Add(cookie)
        Next
    End Sub
End Class

' Violation: Cookie with HttpOnly set to False in object initializer
Public Class InsecureCookies7
    Public Sub CreateCookieWithInitializer()
        ' Violation: Object initializer with HttpOnly = False
        Dim cookie As New HttpCookie("tracking", "track123") With {
            .HttpOnly = False,
            .Secure = True,
            .Expires = DateTime.Now.AddHours(2)
        }
        
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Method that returns cookie without HttpOnly
Public Class InsecureCookies8
    Public Function CreateSessionCookie(sessionId As String) As HttpCookie
        ' Violation: Returns cookie without HttpOnly set
        Dim cookie As New HttpCookie("sessionId", sessionId)
        cookie.Expires = DateTime.Now.AddMinutes(20)
        Return cookie
    End Function
    
    Public Sub UseSessionCookie()
        Dim cookie As HttpCookie = CreateSessionCookie("abc123")
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Cookie with complex logic but missing HttpOnly
Public Class InsecureCookies9
    Public Sub CreateComplexCookie()
        Dim cookie As New HttpCookie("complexCookie", GenerateCookieValue())
        
        ' Set various properties
        cookie.Path = "/app"
        cookie.Domain = ".example.com"
        cookie.Secure = IsSecureConnection()
        cookie.Expires = CalculateExpirationTime()
        
        ' Violation: HttpOnly not set despite complex security logic
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
    
    Private Function GenerateCookieValue() As String
        Return Guid.NewGuid().ToString()
    End Function
    
    Private Function IsSecureConnection() As Boolean
        Return True
    End Function
    
    Private Function CalculateExpirationTime() As DateTime
        Return DateTime.Now.AddHours(4)
    End Function
End Class

' Violation: Cookie in try-catch block without HttpOnly
Public Class InsecureCookies10
    Public Sub CreateCookieWithErrorHandling()
        Try
            ' Violation: Cookie in try block without HttpOnly
            Dim cookie As New HttpCookie("errorCookie", "value")
            cookie.Secure = True
            
            Dim response As HttpResponse = Nothing
            response.Cookies.Add(cookie)
        Catch ex As Exception
            Console.WriteLine($"Error creating cookie: {ex.Message}")
        End Try
    End Sub
End Class

' Violation: Static method creating cookie without HttpOnly
Public Class InsecureCookies11
    Public Shared Function CreateAuthCookie(token As String) As HttpCookie
        ' Violation: Static method returns cookie without HttpOnly
        Dim cookie As New HttpCookie("authToken", token)
        cookie.Secure = True
        cookie.Path = "/"
        Return cookie
    End Function
End Class

' Good examples (should not be detected as violations)
Public Class SecureCookies
    Public Sub CreateSecureCookie()
        ' Good: Cookie with HttpOnly set to True
        Dim cookie As New HttpCookie("sessionId", "12345")
        cookie.HttpOnly = True
        cookie.Secure = True
        cookie.Expires = DateTime.Now.AddHours(1)
        
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
    
    Public Sub CreateCookieWithInitializerSecure()
        ' Good: Object initializer with HttpOnly = True
        Dim cookie As New HttpCookie("userId", "user123") With {
            .HttpOnly = True,
            .Secure = True,
            .Expires = DateTime.Now.AddHours(2)
        }
        
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(cookie)
    End Sub
    
    Public Sub CreateMultipleSecureCookies()
        ' Good: Multiple cookies with HttpOnly = True
        Dim sessionCookie As New HttpCookie("session", "sess123")
        sessionCookie.HttpOnly = True
        sessionCookie.Secure = True
        
        Dim userCookie As New HttpCookie("user", "john")
        userCookie.HttpOnly = True
        userCookie.Secure = True
        
        Dim response As HttpResponse = Nothing
        response.Cookies.Add(sessionCookie)
        response.Cookies.Add(userCookie)
    End Sub
    
    Public Function CreateSecureAuthCookie(token As String) As HttpCookie
        ' Good: Method returns secure cookie
        Dim cookie As New HttpCookie("authToken", token)
        cookie.HttpOnly = True
        cookie.Secure = True
        cookie.SameSite = SameSiteMode.Strict
        Return cookie
    End Function
    
    Public Sub NonCookieMethod()
        ' Good: Method that doesn't create cookies
        Dim data As String = "Some data processing"
        Console.WriteLine(data)
    End Sub
End Class
