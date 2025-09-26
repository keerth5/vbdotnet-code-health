' VB.NET test file for cq-vbn-0132: Use the LoggerMessage delegates
' Rule: For improved performance, use the LoggerMessage delegates.

Imports System
Imports Microsoft.Extensions.Logging

Public Class LoggerMessageExamples
    Private ReadOnly _logger As ILogger
    
    Public Sub New(logger As ILogger)
        _logger = logger
    End Sub
    
    Public Sub TestBasicLogging()
        Dim userId As Integer = 123
        Dim userName As String = "Alice"
        Dim operationTime As TimeSpan = TimeSpan.FromMilliseconds(150)
        
        ' Violation: Using basic logging methods instead of LoggerMessage delegates
        _logger.LogInformation("User {UserId} logged in successfully", userId)
        
        ' Violation: Using LogError with parameters
        _logger.LogError("Failed to process user {UserId} with name {UserName}", userId, userName)
        
        ' Violation: Using LogWarning with parameters
        _logger.LogWarning("Operation took {OperationTime}ms to complete", operationTime.TotalMilliseconds)
        
        ' Violation: Using LogDebug with parameters
        _logger.LogDebug("Processing user {UserId} started", userId)
        
        ' Violation: Using LogTrace with parameters
        _logger.LogTrace("Entering method with user {UserId}", userId)
        
        ' Violation: Using LogCritical with parameters
        _logger.LogCritical("Critical error occurred for user {UserId}", userId)
    End Sub
    
    Public Sub TestComplexLogging()
        Dim orderId As String = "ORD-12345"
        Dim customerId As Integer = 456
        Dim amount As Decimal = 99.99D
        Dim timestamp As DateTime = DateTime.Now
        
        ' Violation: Complex logging with multiple parameters
        _logger.LogInformation("Order {OrderId} created for customer {CustomerId} with amount {Amount} at {Timestamp}", 
                              orderId, customerId, amount, timestamp)
        
        ' Violation: Logging with exception
        Try
            ' Some operation
        Catch ex As Exception
            _logger.LogError(ex, "Error processing order {OrderId} for customer {CustomerId}", orderId, customerId)
        End Try
        
        ' Violation: Conditional logging
        If _logger.IsEnabled(LogLevel.Debug) Then
            _logger.LogDebug("Debug info for order {OrderId}", orderId)
        End If
        
        ' Violation: Logging in loop (performance critical)
        For i As Integer = 1 To 10
            _logger.LogTrace("Processing item {ItemIndex} for order {OrderId}", i, orderId)
        Next
    End Sub
    
    Public Sub TestVariousLogLevels()
        Dim sessionId As String = "sess_abc123"
        Dim requestId As String = "req_xyz789"
        
        ' Violation: Information level logging
        _logger.LogInformation("Session {SessionId} started for request {RequestId}", sessionId, requestId)
        
        ' Violation: Warning level logging
        _logger.LogWarning("Session {SessionId} is about to expire", sessionId)
        
        ' Violation: Error level logging
        _logger.LogError("Session {SessionId} validation failed", sessionId)
        
        ' Violation: Debug level logging
        _logger.LogDebug("Request {RequestId} details processed", requestId)
        
        ' Violation: Trace level logging
        _logger.LogTrace("Trace data for session {SessionId}", sessionId)
    End Sub
    
    Public Sub TestPerformanceCriticalLogging()
        Dim transactionId As String = "txn_001"
        Dim processingTime As Double = 45.6
        Dim recordCount As Integer = 1000
        
        ' Violation: High-frequency logging that would benefit from LoggerMessage
        For Each record In GetRecords()
            _logger.LogDebug("Processing record {RecordId} in transaction {TransactionId}", record.Id, transactionId)
        Next
        
        ' Violation: Performance metrics logging
        _logger.LogInformation("Transaction {TransactionId} processed {RecordCount} records in {ProcessingTime}ms", 
                              transactionId, recordCount, processingTime)
        
        ' Violation: Frequent status updates
        For i As Integer = 1 To 100
            If i Mod 10 = 0 Then
                _logger.LogInformation("Progress: {ProgressPercent}% complete for transaction {TransactionId}", 
                                      i, transactionId)
            End If
        Next
    End Sub
    
    Public Sub TestEventLogging()
        Dim eventId As Integer = 1001
        Dim eventName As String = "UserRegistration"
        Dim userId As String = "user_123"
        
        ' Violation: Event logging with structured data
        _logger.LogInformation("Event {EventId} '{EventName}' triggered by user {UserId}", 
                              eventId, eventName, userId)
        
        ' Violation: Audit logging
        _logger.LogWarning("Security event {EventId} detected for user {UserId}", eventId, userId)
        
        ' Violation: Business event logging
        _logger.LogInformation("Business process '{EventName}' completed for user {UserId} with result {Result}", 
                              eventName, userId, "Success")
    End Sub
    
    Public Sub TestExceptionLogging()
        Dim operationId As String = "op_456"
        Dim resourceId As String = "res_789"
        
        Try
            ' Some risky operation
            ProcessResource(resourceId)
        Catch ex As ArgumentException
            ' Violation: Exception logging with parameters
            _logger.LogError(ex, "Invalid argument in operation {OperationId} for resource {ResourceId}", 
                            operationId, resourceId)
        Catch ex As InvalidOperationException
            ' Violation: Exception logging with parameters
            _logger.LogError(ex, "Invalid operation {OperationId} attempted on resource {ResourceId}", 
                            operationId, resourceId)
        Catch ex As Exception
            ' Violation: General exception logging
            _logger.LogCritical(ex, "Unexpected error in operation {OperationId}", operationId)
        End Try
    End Sub
    
    Private Function GetRecords() As List(Of Record)
        Return New List(Of Record) From {
            New Record With {.Id = 1},
            New Record With {.Id = 2},
            New Record With {.Id = 3}
        }
    End Function
    
    Private Sub ProcessResource(resourceId As String)
        ' Simulate some processing
        If String.IsNullOrEmpty(resourceId) Then
            Throw New ArgumentException("Resource ID cannot be null or empty")
        End If
    End Sub
    
    Public Class Record
        Public Property Id As Integer
    End Class
    
    ' Example of how LoggerMessage delegates should be used (for reference)
    ' Private Shared ReadOnly _userLoggedIn As Action(Of ILogger, Integer, Exception) = 
    '     LoggerMessage.Define(Of Integer)(LogLevel.Information, New EventId(1), "User {UserId} logged in successfully")
    
    ' Public Sub LogUserLogin(userId As Integer)
    '     _userLoggedIn(_logger, userId, Nothing)
    ' End Sub
End Class
