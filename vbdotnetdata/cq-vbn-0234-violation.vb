' VB.NET test file for cq-vbn-0234: Use secure cookies in ASP.NET Core
' This rule detects cookies that are not secure (Secure = False)

Imports System
Imports System.Web
Imports Microsoft.AspNetCore.Http

' Violation: Setting cookie with Secure = False
Public Class InsecureCookies1
    Public Sub CreateInsecureCookie()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Creating cookie with Secure = False
        Dim cookie As New HttpCookie("sessionId", "12345")
        cookie.Secure = False
        cookie.HttpOnly = True
        
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Using object initializer with Secure = False
Public Class InsecureCookies2
    Public Sub CreateCookieWithInitializer()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Object initializer with Secure = False
        Dim cookie As New HttpCookie("userId", "user123") With {
            .Secure = False,
            .HttpOnly = True,
            .Expires = DateTime.Now.AddHours(1)
        }
        
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Setting Secure property after creation
Public Class InsecureCookies3
    Public Sub SetCookiePropertiesAfterCreation()
        Dim context As HttpContext = Nothing
        
        Dim cookie As New HttpCookie("authToken", "token123")
        cookie.HttpOnly = True
        cookie.Expires = DateTime.Now.AddMinutes(30)
        
        ' Violation: Setting Secure to False after creation
        cookie.Secure = False
        
        context.Response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Multiple cookies with insecure settings
Public Class InsecureCookies4
    Public Sub CreateMultipleInsecureCookies()
        Dim response As HttpResponse = Nothing
        
        ' Violation: First insecure cookie
        Dim sessionCookie As New HttpCookie("PHPSESSID", "sess123")
        sessionCookie.Secure = False
        
        ' Violation: Second insecure cookie
        Dim userCookie As New HttpCookie("username", "john")
        userCookie.Secure = False
        
        response.Cookies.Add(sessionCookie)
        response.Cookies.Add(userCookie)
    End Sub
End Class

' Violation: Conditional cookie security
Public Class InsecureCookies5
    Public Sub ConditionalCookieSecurity(isHttps As Boolean)
        Dim response As HttpResponse = Nothing
        Dim cookie As New HttpCookie("data", "sensitive")
        
        If Not isHttps Then
            ' Violation: Setting Secure to False conditionally
            cookie.Secure = False
        Else
            cookie.Secure = True
        End If
        
        response.Cookies.Add(cookie)
    End Sub
End Class

' Violation: Using HttpCookies collection with insecure settings
Public Class InsecureCookies6
    Public Sub AddCookieToCollection()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Adding cookie with Secure = False to collection
        response.Cookies.Add(New HttpCookie("tracking", "track123") With {.Secure = False})
    End Sub
End Class

' Violation: ASP.NET Core cookie options with insecure settings
Public Class InsecureCookies7
    Public Sub SetCookieOptions()
        Dim context As HttpContext = Nothing
        
        ' Violation: Cookie options with Secure = False
        Dim options As New CookieOptions() With {
            .Secure = False,
            .HttpOnly = True,
            .SameSite = SameSiteMode.Strict
        }
        
        context.Response.Cookies.Append("preferences", "dark-theme", options)
    End Sub
End Class

' Violation: Setting cookie properties in different ways
Public Class InsecureCookies8
    Public Sub VariousCookieSettings()
        Dim response As HttpResponse = Nothing
        
        ' Violation: Direct property assignment
        Dim cookie1 As New HttpCookie("cart", "item123")
        cookie1.Secure = False
        
        ' Violation: Using With statement
        Dim cookie2 As HttpCookie = New HttpCookie("wishlist", "wish456") With {.Secure = False}
        
        response.Cookies.Add(cookie1)
        response.Cookies.Add(cookie2)
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecureCookies
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
    
    Public Sub CreateCookieWithoutSecureProperty()
        Dim response As HttpResponse = Nothing
        
        ' Good: Cookie without explicitly setting Secure (may default to True)
        Dim cookie As New HttpCookie("data", "value")
        cookie.HttpOnly = True
        
        response.Cookies.Add(cookie)
    End Sub
End Class
