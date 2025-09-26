' VB.NET test file for cq-vbn-0282: Do not assign a property to itself
' This rule detects properties assigned to themselves

Imports System

Public Class BadSelfAssignment
    Public Property Name As String
    Public Property Age As Integer
    
    ' BAD: Property assigned to itself
    Public Sub TestSelfAssignment()
        ' Violation: Property assigned to itself
        Name = Name
        
        ' Violation: Property assigned to itself with Me
        Me.Age = Me.Age
        
        ' Violation: Another self assignment
        Me.Name = Me.Name
    End Sub
    
    ' GOOD: Valid property assignments
    Public Sub TestValidAssignment()
        ' Good: Different property assignment
        Name = "John"
        Age = 25
        
        ' Good: Assignment from parameter
        Dim newName As String = "Jane"
        Name = newName
        
        ' Good: Assignment from another object
        Dim other As New BadSelfAssignment()
        Name = other.Name
    End Sub
End Class
