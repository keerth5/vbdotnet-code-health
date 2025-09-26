' VB.NET test file for cq-vbn-0291: Template should be a static expression
' This rule detects logging templates that vary between calls

Imports System
Imports Microsoft.Extensions.Logging

Public Class BadLoggingTemplate
    Private ReadOnly logger As ILogger
    Private ReadOnly name As String = "TestName"
    
    Public Sub New(logger As ILogger)
        Me.logger = logger
    End Sub
    
    ' BAD: Non-static logging templates
    Public Sub TestDynamicTemplate()
        ' Violation: Using variable concatenation in template
        logger.LogInformation("User " & name & " logged in")
        
        ' Violation: Using String.Format in logging
        logger.LogError(String.Format("Error {0} occurred", "Database"))
        
        ' Violation: Dynamic template construction
        LoggerExtensions.LogWarning(logger, "Process " & GetProcessName() & " completed")
    End Sub
    
    Private Function GetProcessName() As String
        Return "backup"
    End Function
    
    ' GOOD: Static logging templates
    Public Sub TestStaticTemplate()
        ' Good: Static template with parameters
        logger.LogInformation("User {name} logged in", name)
        
        ' Good: Static template with multiple parameters
        logger.LogError("Error {errorType} occurred at {timestamp}", "Database", DateTime.Now)
        
        ' Good: Simple static template
        logger.LogWarning("Process {processName} completed", GetProcessName())
        
        ' Good: No parameters
        logger.LogDebug("System initialized successfully")
    End Sub
End Class
