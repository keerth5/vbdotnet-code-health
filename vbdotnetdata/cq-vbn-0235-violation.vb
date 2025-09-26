' VB.NET test file for cq-vbn-0235: Ensure use secure cookies in ASP.NET Core
' This rule detects cookies that are missing Secure = True setting

Imports System
Imports System.Web
Imports Microsoft.AspNetCore.Http

' Violation: Adding cookie without setting Secure = True
Public Class MissingSecureCookies1
    Public Sub AddCookieWithoutSecure()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Cookie added without Secure = True
        Dim cookie As New HttpCookie("sessionId", "12345")
        cookie.HttpOnly = True
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: HttpCookie without Secure = True in initializer
Public Class MissingSecureCookies2
    Public Sub CreateCookieWithoutSecureFlag()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Object initializer missing Secure = True
        Dim cookie As New HttpCookie("userId", "user123") With {
            .HttpOnly = True,
            .Expires = DateTime.Now.AddHours(1)
        }
        
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Multiple cookies without secure setting
Public Class MissingSecureCookies3
    Public Sub CreateMultipleCookiesWithoutSecure()
        Dim response As HttpResponse = Nothing
        
        ' Violation: First cookie without Secure = True
        Dim sessionCookie As New HttpCookie("PHPSESSID", "sess123")
        sessionCookie.HttpOnly = True
        
        ' Violation: Second cookie without Secure = True
        Dim userCookie As New HttpCookie("username", "john")
        userCookie.Path = "/"
        
        response.Cookies.Add(sessionCookie)
        response.Cookies.Add(userCookie)
    End Sub
End Class

' Violation: Cookie with only some properties set
Public Class MissingSecureCookies4
    Public Sub SetCookiePropertiesWithoutSecure()
        Dim context As HttpContext = Nothing
        
        ' Violation: Cookie missing Secure = True
        Dim cookie As New HttpCookie("authToken", "token123")
        cookie.HttpOnly = True
        cookie.Expires = DateTime.Now.AddMinutes(30)
        cookie.Path = "/"
        cookie.Domain = "example.com"
        
        context.Response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Using Response.Cookies.Add without secure cookie
Public Class MissingSecureCookies5
    Public Sub AddCookieDirectly()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Adding cookie directly without Secure = True
        response.Cookies.Add(New HttpCookie("tracking", "track123") With {.HttpOnly = True})
    End Sub
End Class

' Violation: Cookie created in method parameter
Public Class MissingSecureCookies6
    Public Sub ProcessCookie()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Cookie created inline without Secure = True
        response.Cookies.Add(New HttpCookie("preferences", "dark-theme"))
    End Sub
End Class

' Violation: ASP.NET Core cookie options without Secure = True
Public Class MissingSecureCookies7
    Public Sub SetCookieOptionsWithoutSecure()
        Dim context As HttpContext = Nothing
        
        ' Violation: Cookie options missing Secure = True
        Dim options As New CookieOptions() With {
            .HttpOnly = True,
            .SameSite = SameSiteMode.Strict,
            .Expires = DateTimeOffset.Now.AddHours(1)
        }
        
        context.Response.Cookies.Append("settings", "value123", options)
    End Sub
End Class

' Violation: Cookie with conditional logic missing secure
Public Class MissingSecureCookies8
    Public Sub ConditionalCookieCreation(addCookie As Boolean)
        Dim response As HttpResponse = Nothing
        
        If addCookie Then
            ' Violation: Cookie created conditionally without Secure = True
            Dim cookie As New HttpCookie("conditional", "value")
            cookie.HttpOnly = True
            response.Cookies.Add(cookie)
        End If
    End Sub
End Class

' Violation: Cookie created in loop without secure setting
Public Class MissingSecureCookies9
    Public Sub CreateCookiesInLoop()
        Dim response As HttpResponse = Nothing
        Dim cookieData As String() = {"data1", "data2", "data3"}
        
        For i As Integer = 0 To cookieData.Length - 1
            ' Violation: Cookies created in loop without Secure = True
            Dim cookie As New HttpCookie($"cookie{i}", cookieData(i))
            cookie.HttpOnly = True
            response.Cookies.Add(cookie)
        Next
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureCookieExamples
    Public Sub CreateSecureCookie()
        Dim response As HttpResponse = Nothing
        
        ' Good: Cookie with Secure = True
        Dim cookie As New HttpCookie("sessionId", "12345")
        cookie.Secure = True
        cookie.HttpOnly = True
        
        response.Cookies.Add(cookie)
    End Sub
    
    Public Sub CreateCookieWithSecureInitializer()
        Dim response As HttpResponse = Nothing
        
        ' Good: Object initializer with Secure = True
        Dim cookie As New HttpCookie("userId", "user123") With {
            .Secure = True,
            .HttpOnly = True,
            .Expires = DateTime.Now.AddHours(1)
        }
        
        response.Cookies.Add(cookie)
    End Sub
    
    Public Sub CreateSecureCookieOptions()
        Dim context As HttpContext = Nothing
        
        ' Good: Cookie options with Secure = True
        Dim options As New CookieOptions() With {
            .Secure = True,
            .HttpOnly = True,
            .SameSite = SameSiteMode.Strict
        }
        
        context.Response.Cookies.Append("preferences", "dark-theme", options)
    End Sub
End Class
