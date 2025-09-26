' VB.NET test file for cq-vbn-0284: Argument passed to TaskCompletionSource constructor should be TaskCreationOptions enum instead of TaskContinuationOptions enum
' This rule detects TaskCompletionSource constructed with wrong enum type

Imports System
Imports System.Threading.Tasks

Public Class BadTaskCompletionSource
    ' BAD: TaskCompletionSource with TaskContinuationOptions
    Public Sub TestWrongEnumType()
        ' Violation: Using TaskContinuationOptions instead of TaskCreationOptions
        Dim tcs1 = New TaskCompletionSource(Of String)(TaskContinuationOptions.ExecuteSynchronously)
        
        ' Violation: Another wrong enum usage
        Dim tcs2 = New TaskCompletionSource(Of Integer)(TaskContinuationOptions.LongRunning)
        
        ' Violation: With state and wrong enum
        Dim tcs3 = New TaskCompletionSource(Of Boolean)("state", TaskContinuationOptions.AttachedToParent)
    End Sub
    
    ' GOOD: TaskCompletionSource with correct enum type
    Public Sub TestCorrectEnumType()
        ' Good: Using TaskCreationOptions
        Dim tcs1 = New TaskCompletionSource(Of String)(TaskCreationOptions.LongRunning)
        
        ' Good: Using TaskCreationOptions with state
        Dim tcs2 = New TaskCompletionSource(Of Integer)("state", TaskCreationOptions.AttachedToParent)
        
        ' Good: Default constructor
        Dim tcs3 = New TaskCompletionSource(Of Boolean)()
        
        ' Good: Constructor with just state
        Dim tcs4 = New TaskCompletionSource(Of String)("state")
    End Sub
End Class
