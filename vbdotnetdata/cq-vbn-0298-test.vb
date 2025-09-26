' VB.NET test file for cq-vbn-0298: Do not use ConfigureAwaitOptions.SuppressThrowing with Task<TResult>
' This rule detects ConfigureAwaitOptions.SuppressThrowing usage with generic Task

Imports System
Imports System.Threading.Tasks

Public Class BadConfigureAwait
    ' BAD: ConfigureAwaitOptions.SuppressThrowing with Task(Of T)
    Public Async Function TestBadConfigureAwait() As Task
        ' Violation: SuppressThrowing with Task(Of String)
        Dim result As String = Await GetStringAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing)
        
        ' Violation: SuppressThrowing with Task(Of Integer)
        Dim number As Integer = Await GetIntegerAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing)
        
        ' Violation: SuppressThrowing with Task(Of Boolean)
        Dim flag As Boolean = Await GetBooleanAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing)
    End Function
    
    Public Async Function TestMoreBadUsage() As Task
        ' Violation: Combined options with SuppressThrowing
        Dim data As String = Await GetStringAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing Or ConfigureAwaitOptions.ContinueOnCapturedContext)
    End Function
    
    Private Async Function GetStringAsync() As Task(Of String)
        Await Task.Delay(100)
        Return "test"
    End Function
    
    Private Async Function GetIntegerAsync() As Task(Of Integer)
        Await Task.Delay(100)
        Return 42
    End Function
    
    Private Async Function GetBooleanAsync() As Task(Of Boolean)
        Await Task.Delay(100)
        Return True
    End Function
    
    ' GOOD: Proper ConfigureAwait usage
    Public Async Function TestGoodConfigureAwait() As Task
        ' Good: ConfigureAwait with boolean
        Dim result As String = Await GetStringAsync().ConfigureAwait(False)
        
        ' Good: ConfigureAwait with non-SuppressThrowing options
        Dim number As Integer = Await GetIntegerAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext)
        
        ' Good: ConfigureAwait with non-generic Task
        Await Task.Delay(100).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing)
    End Function
End Class
