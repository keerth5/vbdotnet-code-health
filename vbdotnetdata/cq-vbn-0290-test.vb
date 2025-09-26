' VB.NET test file for cq-vbn-0290: Named placeholders should not be numeric values
' This rule detects logging templates with numeric-only named placeholders

Imports System
Imports Microsoft.Extensions.Logging

Public Class BadLoggingPlaceholders
    Private ReadOnly logger As ILogger
    
    Public Sub New(logger As ILogger)
        Me.logger = logger
    End Sub
    
    ' BAD: Numeric-only named placeholders in logging
    Public Sub TestBadLoggingPlaceholders()
        ' Violation: Numeric placeholder {0}
        logger.LogInformation("User {0} logged in", "john")
        
        ' Violation: Multiple numeric placeholders {1}, {2}
        logger.LogError("Error {1} occurred at {2}", "Database", DateTime.Now)
        
        ' Violation: LogWarning with numeric placeholder
        logger.LogWarning("Warning {123} detected", "security")
        
        ' Violation: LogDebug with numeric placeholder  
        logger.LogDebug("Debug info {456}", "details")
    End Sub
    
    Public Sub TestMoreBadPlaceholders()
        ' Violation: Log extension with numeric placeholder
        LoggerExtensions.LogInformation(logger, "Process {789} completed", "backup")
        
        ' Violation: Different log level
        LoggerExtensions.LogCritical(logger, "Critical error {999}", "system failure")
    End Sub
    
    ' GOOD: Named placeholders with descriptive names
    Public Sub TestGoodLoggingPlaceholders()
        ' Good: Named placeholder {userId}
        logger.LogInformation("User {userId} logged in", "john")
        
        ' Good: Multiple named placeholders
        logger.LogError("Error {errorType} occurred at {timestamp}", "Database", DateTime.Now)
        
        ' Good: Descriptive placeholder names
        logger.LogWarning("Warning {warningType} detected", "security")
        
        ' Good: Clear placeholder naming
        logger.LogDebug("Debug info {details}", "connection details")
        
        ' Good: No placeholders
        logger.LogInformation("System started successfully")
    End Sub
End Class
