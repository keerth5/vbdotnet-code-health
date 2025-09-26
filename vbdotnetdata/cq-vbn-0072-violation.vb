' Test file for cq-vbn-0072: Avoid dead conditional code
' Rule should detect unreachable or always-true/false code

Imports System

Public Class DeadConditionalExamples
    
    Public Sub DeadIfStatements()
        ' Violation 1: If True Then - always true condition
        If True Then
            Console.WriteLine("This will always execute")
        End If
        
        ' Violation 2: If False Then - always false condition
        If False Then
            Console.WriteLine("This will never execute")
        End If
        
        ' This should NOT be detected - normal conditional
        Dim condition As Boolean = DateTime.Now.Hour > 12
        If condition Then
            Console.WriteLine("Afternoon")
        End If
    End Sub
    
    Public Sub DeadWhileLoops()
        ' Violation 3: While False - will never execute
        While False
            Console.WriteLine("This will never execute")
        End While
        
        ' This should NOT be detected - normal while loop
        Dim counter As Integer = 0
        While counter < 5
            Console.WriteLine(counter)
            counter += 1
        End While
    End Sub
    
    Public Sub MoreDeadCode()
        ' Violation 4: Another If True Then
        If True Then
            Console.WriteLine("Always true")
        Else
            Console.WriteLine("Never reached")
        End If
        
        ' Violation 5: Another If False Then
        If False Then
            Console.WriteLine("Never reached")
        Else
            Console.WriteLine("Always reached")
        End If
        
        ' Violation 6: While False in different context
        Dim items As New List(Of String)
        While False
            items.Add("Never added")
        End While
    End Sub
    
    Public Function DeadCodeInFunction() As String
        ' Violation 7: If True Then in function
        If True Then
            Return "Always returned"
        End If
        
        ' This code is unreachable but not detected by this rule
        Return "Never reached"
    End Function
    
    Public Sub ConditionalWithVariables()
        Dim alwaysTrue As Boolean = True
        Dim alwaysFalse As Boolean = False
        
        ' This should NOT be detected - using variables (not literals)
        If alwaysTrue Then
            Console.WriteLine("Using variable")
        End If
        
        If alwaysFalse Then
            Console.WriteLine("Using variable")
        End If
        
        ' Violation 8: Direct literal comparison
        If True Then
            Console.WriteLine("Direct True literal")
        End If
    End Sub
    
    Public Sub NestedDeadCode()
        ' Violation 9: Nested If True Then
        If True Then
            Console.WriteLine("Outer always true")
            
            ' Violation 10: Nested If False Then
            If False Then
                Console.WriteLine("Inner never executed")
            End If
        End If
    End Sub
    
    Public Sub WhileWithConditions()
        ' Violation 11: While False with complex expression
        While False
            Dim x As Integer = 10
            Console.WriteLine(x)
        End While
        
        ' This should NOT be detected - normal condition
        Dim running As Boolean = True
        While running
            Console.WriteLine("Running...")
            running = False ' Will exit
        End While
    End Sub
    
End Class

Public Class AnotherDeadCodeClass
    
    Public Sub AdditionalDeadCode()
        ' Violation 12: If True Then with ElseIf
        If True Then
            Console.WriteLine("Always true")
        ElseIf DateTime.Now.Hour > 10 Then
            Console.WriteLine("Never reached")
        End If
        
        ' Violation 13: If False Then with Else
        If False Then
            Console.WriteLine("Never executed")
        Else
            Console.WriteLine("Always executed")
        End If
    End Sub
    
    Public Sub LoopDeadCode()
        ' Violation 14: While False in different method
        While False
            For i As Integer = 1 To 10
                Console.WriteLine(i)
            Next
        End While
    End Sub
    
End Class
