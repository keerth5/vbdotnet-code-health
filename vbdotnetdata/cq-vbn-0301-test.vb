' VB.NET test file for cq-vbn-0301: Do not pass a non-nullable value to 'ArgumentNullException.ThrowIfNull'
' This rule detects ArgumentNullException.ThrowIfNull with non-nullable values

Imports System

Public Class BadThrowIfNull
    ' BAD: ArgumentNullException.ThrowIfNull with non-nullable values
    Public Sub TestBadThrowIfNull()
        ' Violation: ThrowIfNull with New expression (never null)
        ArgumentNullException.ThrowIfNull(New String("test"))
        
        ' Violation: ThrowIfNull with another New expression
        ArgumentNullException.ThrowIfNull(New Integer())
        
        ' Violation: ThrowIfNull with NameOf expression (never null)
        ArgumentNullException.ThrowIfNull(NameOf(TestBadThrowIfNull))
        
        ' Violation: ThrowIfNull with NameOf parameter
        ArgumentNullException.ThrowIfNull(NameOf(String))
    End Sub
    
    Public Sub TestMoreBadUsage(value As String)
        ' Violation: ThrowIfNull with New object
        ArgumentNullException.ThrowIfNull(New List(Of String)())
        
        ' Violation: ThrowIfNull with NameOf
        ArgumentNullException.ThrowIfNull(NameOf(value))
        
        ' Violation: ThrowIfNull with New struct
        ArgumentNullException.ThrowIfNull(New DateTime())
    End Sub
    
    ' GOOD: ArgumentNullException.ThrowIfNull with potentially nullable values
    Public Sub TestGoodThrowIfNull(nullableParam As String, nullableObject As Object)
        ' Good: ThrowIfNull with nullable parameter
        ArgumentNullException.ThrowIfNull(nullableParam)
        
        ' Good: ThrowIfNull with nullable object
        ArgumentNullException.ThrowIfNull(nullableObject)
        
        ' Good: ThrowIfNull with method result that could be null
        ArgumentNullException.ThrowIfNull(GetPotentiallyNullValue())
        
        ' Good: ThrowIfNull with property that could be null
        ArgumentNullException.ThrowIfNull(Me.PotentiallyNullProperty)
    End Sub
    
    Private Function GetPotentiallyNullValue() As String
        Return If(DateTime.Now.Millisecond > 500, "value", Nothing)
    End Function
    
    Public Property PotentiallyNullProperty As String
    
    ' GOOD: Regular null checks without ThrowIfNull
    Public Sub TestRegularNullChecks()
        ' Good: Regular null check
        Dim obj As New String("test")
        If obj Is Nothing Then
            Throw New ArgumentNullException(NameOf(obj))
        End If
        
        ' Good: Using NameOf for parameter name, not value check
        If String.IsNullOrEmpty(NameOf(TestRegularNullChecks)) Then
            ' This is fine - NameOf used for string comparison, not null check
        End If
    End Sub
End Class
