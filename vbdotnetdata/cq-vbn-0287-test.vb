' VB.NET test file for cq-vbn-0287: Use ThrowIfCancellationRequested
' This rule detects manual cancellation checking that can use ThrowIfCancellationRequested

Imports System
Imports System.Threading

Public Class BadCancellationCheck
    ' BAD: Manual cancellation checking
    Public Sub TestManualCancellationCheck(token As CancellationToken)
        ' Violation: Manual check and throw
        If token.IsCancellationRequested Then
            Throw New OperationCanceledException()
        End If
        
        ' Some work here
        Console.WriteLine("Doing work...")
        
        ' Violation: Another manual check
        If token.IsCancellationRequested Then
            Throw New OperationCanceledException("Operation was cancelled")
        End If
    End Sub
    
    Public Sub TestManualCheckWithMessage(cancellationToken As CancellationToken)
        ' Violation: Manual check with custom message
        If cancellationToken.IsCancellationRequested Then
            Throw New OperationCanceledException("Custom cancellation message")
        End If
    End Sub
    
    ' GOOD: Using ThrowIfCancellationRequested
    Public Sub TestCorrectCancellationCheck(token As CancellationToken)
        ' Good: Using ThrowIfCancellationRequested
        token.ThrowIfCancellationRequested()
        
        ' Some work here
        Console.WriteLine("Doing work...")
        
        ' Good: Another correct usage
        token.ThrowIfCancellationRequested()
    End Sub
    
    ' GOOD: Other valid cancellation patterns
    Public Sub TestValidCancellationPatterns(token As CancellationToken)
        ' Good: Using IsCancellationRequested for logic (not throwing)
        If token.IsCancellationRequested Then
            Return
        End If
        
        ' Good: Using IsCancellationRequested in loop condition
        While Not token.IsCancellationRequested
            ' Do work
            Thread.Sleep(100)
        End While
    End Sub
End Class
