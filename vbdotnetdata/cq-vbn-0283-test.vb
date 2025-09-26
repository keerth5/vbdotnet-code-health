' VB.NET test file for cq-vbn-0283: Do not assign a symbol and its member in the same statement
' This rule detects assignments where a symbol and its member are assigned in the same statement

Imports System

Public Class BadSymbolMemberAssignment
    Public Property Value As Integer
    
    ' BAD: Symbol and member assigned in same statement
    Public Sub TestSymbolMemberAssignment()
        Dim obj As New BadSymbolMemberAssignment()
        
        ' Violation: obj and obj.Value assigned in same statement (ambiguous)
        obj = New BadSymbolMemberAssignment() : obj.Value = 10
        
        ' Violation: Complex assignment with member access
        obj = GetObject() : obj.Value = 20
    End Sub
    
    Private Function GetObject() As BadSymbolMemberAssignment
        Return New BadSymbolMemberAssignment()
    End Function
    
    ' GOOD: Separate assignments
    Public Sub TestValidAssignments()
        Dim obj As New BadSymbolMemberAssignment()
        
        ' Good: Separate statements
        obj = New BadSymbolMemberAssignment()
        obj.Value = 10
        
        ' Good: Only member assignment
        obj.Value = 20
        
        ' Good: Only object assignment
        obj = New BadSymbolMemberAssignment()
    End Sub
End Class
