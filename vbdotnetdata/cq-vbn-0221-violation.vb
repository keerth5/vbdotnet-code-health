' VB.NET test file for cq-vbn-0221: Set ViewStateUserKey For Classes Derived From Page
' This rule detects classes that inherit from Page but don't set ViewStateUserKey

Imports System
Imports System.Web.UI

' Violation: Page class without ViewStateUserKey
Public Class LoginPage
    Inherits Page
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Missing ViewStateUserKey assignment
        Response.Write("Login page loaded")
    End Sub
End Class

' Violation: Partial page class without ViewStateUserKey
Public Partial Class HomePage
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Missing ViewStateUserKey assignment
        LoadUserData()
    End Sub
    
    Private Sub LoadUserData()
        ' Some functionality
    End Sub
End Class

' Violation: Another page class without ViewStateUserKey
Friend Class AdminPage
    Inherits Page
    
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        ' Missing ViewStateUserKey assignment
        InitializeAdminControls()
    End Sub
    
    Private Sub InitializeAdminControls()
        ' Admin specific controls
    End Sub
End Class

' Violation: Page with System.Web.UI.Page fully qualified
Public Class ReportPage
    Inherits System.Web.UI.Page
    
    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ' Missing ViewStateUserKey assignment
        GenerateReport()
    End Sub
    
    Private Sub GenerateReport()
        ' Report generation logic
    End Sub
End Class

' Violation: Partial page class with full namespace
Public Partial Class DataEntryPage
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Missing ViewStateUserKey assignment
        If Not IsPostBack Then
            LoadInitialData()
        End If
    End Sub
    
    Private Sub LoadInitialData()
        ' Load initial form data
    End Sub
End Class

' Good example (should not be detected as violation - has ViewStateUserKey)
Public Class SecurePage
    Inherits Page
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Correctly sets ViewStateUserKey
        Page.ViewStateUserKey = Session.SessionID
        LoadSecureData()
    End Sub
    
    Private Sub LoadSecureData()
        ' Secure data loading
    End Sub
End Class

' Good example (should not be detected - not inheriting from Page)
Public Class UtilityClass
    
    Public Sub ProcessData()
        ' This class doesn't inherit from Page, so ViewStateUserKey is not required
    End Sub
End Class

' Violation: Friend class inheriting from Page without ViewStateUserKey
Friend Class InternalPage
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Missing ViewStateUserKey assignment
        ProcessInternalRequest()
    End Sub
    
    Private Sub ProcessInternalRequest()
        ' Internal processing
    End Sub
End Class

' Violation: Public partial class inheriting from Page
Public Partial Class CustomerPage
    Inherits Page
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Missing ViewStateUserKey assignment
        LoadCustomerInfo()
    End Sub
    
    Private Sub LoadCustomerInfo()
        ' Customer information loading
    End Sub
End Class

' Violation: Page class with additional interfaces
Public Class MultiFunctionPage
    Inherits System.Web.UI.Page
    Implements IDisposable
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Missing ViewStateUserKey assignment
        InitializePage()
    End Sub
    
    Private Sub InitializePage()
        ' Page initialization
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Cleanup resources
    End Sub
End Class
