' VB.NET test file for cq-vbn-0296: Ensure ThreadStatic is only used with static fields
' This rule detects ThreadStatic attribute on non-static fields

Imports System

Public Class BadThreadStatic
    ' BAD: ThreadStatic on non-static fields
    <ThreadStatic>
    Public instanceField As Integer
    
    ' BAD: ThreadStatic on private instance field
    <ThreadStatic>
    Private instanceValue As String
    
    ' BAD: ThreadStatic on protected instance field
    <ThreadStatic>
    Protected instanceData As Double
    
    ' BAD: ThreadStatic on friend instance field
    <ThreadStatic>
    Friend instanceCounter As Long
    
    ' GOOD: ThreadStatic on static fields
    <ThreadStatic>
    Public Shared staticField As Integer
    
    <ThreadStatic>
    Private Shared staticValue As String
    
    <ThreadStatic>
    Protected Shared staticData As Double
    
    <ThreadStatic>
    Friend Shared staticCounter As Long
End Class

' Another test class
Public Class AnotherThreadStaticTest
    ' BAD: ThreadStatic on instance field
    <ThreadStatic>
    Public badField As Boolean
    
    ' GOOD: ThreadStatic on static field
    <ThreadStatic>
    Public Shared goodField As Boolean
    
    ' GOOD: Regular instance field without ThreadStatic
    Public regularField As Integer
    
    ' GOOD: Regular static field without ThreadStatic
    Public Shared regularStaticField As String
End Class
