' Test file for cq-vbn-0170: Parameter count mismatch
' Number of parameters supplied in the logging message template do not match the number of named placeholders

Imports System
Imports Microsoft.Extensions.Logging

Public Class LoggingParameterMismatchTests
    
    Private ReadOnly _logger As ILogger
    
    Public Sub New(logger As ILogger)
        _logger = logger
    End Sub
    
    ' Violation: More placeholders than parameters
    Public Sub LogWithMissingParameters()
        Dim userId As Integer = 123
        Dim userName As String = "John"
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogInformation("User {UserId} with name {UserName} has status {Status}", userId, userName)
    End Sub
    
    ' Violation: More parameters than placeholders
    Public Sub LogWithExtraParameters()
        Dim orderId As Integer = 456
        Dim amount As Decimal = 99.99D
        Dim currency As String = "USD"
        
        ' Violation: 2 placeholders but 3 parameters
        _logger.LogWarning("Order {OrderId} has amount {Amount}", orderId, amount, currency)
    End Sub
    
    ' Violation: LogDebug with parameter mismatch
    Public Sub LogDebugWithMismatch()
        Dim filename As String = "data.txt"
        Dim size As Long = 1024
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogDebug("Processing file {FileName} with size {Size} and type {FileType}", filename, size)
    End Sub
    
    ' Violation: LogError with parameter mismatch
    Public Sub LogErrorWithMismatch()
        Dim errorCode As Integer = 500
        Dim message As String = "Internal Server Error"
        Dim timestamp As DateTime = DateTime.Now
        
        ' Violation: 2 placeholders but 3 parameters
        _logger.LogError("Error {ErrorCode}: {Message}", errorCode, message, timestamp)
    End Sub
    
    ' Violation: LogCritical with parameter mismatch
    Public Sub LogCriticalWithMismatch()
        Dim systemId As String = "SYS001"
        
        ' Violation: 2 placeholders but only 1 parameter
        _logger.LogCritical("System {SystemId} is down at {Timestamp}", systemId)
    End Sub
    
    ' Good practice: Matching placeholders and parameters (should not be detected)
    Public Sub LogWithCorrectParameters()
        Dim userId As Integer = 789
        Dim action As String = "login"
        
        ' Good: 2 placeholders and 2 parameters
        _logger.LogInformation("User {UserId} performed action {Action}", userId, action)
    End Sub
    
    ' Good: No placeholders, no parameters
    Public Sub LogWithoutParameters()
        ' Good: No placeholders, no parameters
        _logger.LogInformation("System startup completed")
    End Sub
    
    ' Violation: Complex logging with multiple mismatches
    Public Sub ComplexLoggingMismatches()
        Dim requestId As String = "REQ123"
        Dim method As String = "POST"
        Dim url As String = "/api/users"
        Dim statusCode As Integer = 201
        
        ' Violation: 3 placeholders but 4 parameters
        _logger.LogInformation("Request {RequestId} {Method} {Url}", requestId, method, url, statusCode)
        
        ' Violation: 4 placeholders but 3 parameters  
        _logger.LogWarning("Request {RequestId} {Method} {Url} returned {StatusCode}", requestId, method, url)
    End Sub
    
    ' Violation: LogTrace with parameter mismatch
    Public Sub LogTraceWithMismatch()
        Dim threadId As Integer = Thread.CurrentThread.ManagedThreadId
        Dim operation As String = "DataProcessing"
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogTrace("Thread {ThreadId} starting operation {Operation} at {StartTime}", threadId, operation)
    End Sub
    
    ' Violation: Logging in loop with parameter mismatch
    Public Sub LogInLoopWithMismatch()
        Dim items() As String = {"item1", "item2", "item3"}
        
        For i As Integer = 0 To items.Length - 1
            ' Violation: 3 placeholders but only 2 parameters
            _logger.LogDebug("Processing item {Index} with value {Value} and status {Status}", i, items(i))
        Next
    End Sub
    
    ' Violation: Conditional logging with parameter mismatch
    Public Sub ConditionalLoggingWithMismatch(isError As Boolean)
        Dim code As Integer = 404
        Dim message As String = "Not Found"
        
        If isError Then
            ' Violation: 3 placeholders but only 2 parameters
            _logger.LogError("Error occurred: {Code} - {Message} at {Time}", code, message)
        Else
            ' Violation: 1 placeholder but 2 parameters
            _logger.LogInformation("Success: {Message}", code, message)
        End If
    End Sub
    
    ' Violation: Exception logging with parameter mismatch
    Public Sub LogExceptionWithMismatch()
        Try
            Throw New InvalidOperationException("Test exception")
        Catch ex As Exception
            ' Violation: 2 placeholders but 3 parameters (exception is not counted as a parameter for placeholders)
            _logger.LogError(ex, "Exception in method {MethodName} with parameter {Parameter}", "LogExceptionWithMismatch", "testParam", "extraParam")
        End Try
    End Sub
    
End Class

' Additional test cases with different logger types
Public Class ExtendedLoggingTests
    
    Private ReadOnly _logger As ILogger(Of ExtendedLoggingTests)
    
    Public Sub New(logger As ILogger(Of ExtendedLoggingTests))
        _logger = logger
    End Sub
    
    ' Violation: Generic logger with parameter mismatch
    Public Sub GenericLoggerMismatch()
        Dim sessionId As String = "SESSION123"
        Dim userId As Integer = 456
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogInformation("Session {SessionId} for user {UserId} expired at {ExpiryTime}", sessionId, userId)
    End Sub
    
    ' Violation: Structured logging with mismatch
    Public Sub StructuredLoggingMismatch()
        Dim productId As Integer = 789
        Dim categoryId As Integer = 10
        Dim price As Decimal = 29.99D
        Dim discount As Decimal = 5.0D
        
        ' Violation: 3 placeholders but 4 parameters
        _logger.LogDebug("Product {ProductId} in category {CategoryId} has price {Price}", productId, categoryId, price, discount)
    End Sub
    
    ' Violation: Performance logging with mismatch
    Public Sub PerformanceLoggingMismatch()
        Dim operationName As String = "DatabaseQuery"
        Dim duration As TimeSpan = TimeSpan.FromMilliseconds(250)
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogInformation("Operation {OperationName} completed in {Duration} with result {Result}", operationName, duration)
    End Sub
    
    ' Violation: Security logging with mismatch
    Public Sub SecurityLoggingMismatch()
        Dim ipAddress As String = "192.168.1.100"
        Dim attemptCount As Integer = 3
        Dim lockoutTime As DateTime = DateTime.Now.AddMinutes(15)
        
        ' Violation: 2 placeholders but 3 parameters
        _logger.LogWarning("Failed login attempts from {IpAddress} count: {AttemptCount}", ipAddress, attemptCount, lockoutTime)
    End Sub
    
End Class

' Test with module-level logging
Public Module LoggingUtilities
    
    Private ReadOnly _logger As ILogger = LoggerFactory.Create(Sub(builder) builder.AddConsole()).CreateLogger("Utilities")
    
    ' Violation: Module logging with parameter mismatch
    Public Sub UtilityLoggingMismatch()
        Dim processId As Integer = Process.GetCurrentProcess().Id
        Dim memoryUsage As Long = GC.GetTotalMemory(False)
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogInformation("Process {ProcessId} using {MemoryUsage} bytes with status {Status}", processId, memoryUsage)
    End Sub
    
    ' Violation: Batch processing logging with mismatch
    Public Sub BatchProcessingMismatch()
        Dim batchId As String = "BATCH001"
        Dim itemCount As Integer = 100
        Dim successCount As Integer = 95
        Dim errorCount As Integer = 5
        
        ' Violation: 3 placeholders but 4 parameters
        _logger.LogInformation("Batch {BatchId} processed {ItemCount} items with {SuccessCount}", batchId, itemCount, successCount, errorCount)
    End Sub
    
End Module

' Test with different log levels and scenarios
Public Class ComprehensiveLoggingTests
    
    Private ReadOnly _logger As ILogger
    
    Public Sub New(logger As ILogger)
        _logger = logger
    End Sub
    
    ' Violation: Multiple log levels with mismatches
    Public Sub MultipleLogLevelMismatches()
        Dim requestId As String = "REQ456"
        Dim userId As Integer = 123
        Dim endpoint As String = "/api/data"
        
        ' Violation: LogTrace with mismatch
        _logger.LogTrace("Trace: Request {RequestId} from user {UserId} to endpoint {Endpoint} with method {Method}", requestId, userId, endpoint)
        
        ' Violation: LogDebug with mismatch  
        _logger.LogDebug("Debug: Processing request {RequestId} for user {UserId}", requestId, userId, endpoint)
        
        ' Violation: LogInformation with mismatch
        _logger.LogInformation("Info: Request {RequestId} completed", requestId, userId)
        
        ' Violation: LogWarning with mismatch
        _logger.LogWarning("Warning: Slow request {RequestId} from {UserId} took {Duration} ms", requestId, userId)
        
        ' Violation: LogError with mismatch
        _logger.LogError("Error: Request {RequestId} failed with code {ErrorCode} and message {ErrorMessage}", requestId)
        
        ' Violation: LogCritical with mismatch
        _logger.LogCritical("Critical: System failure for request {RequestId} user {UserId}", requestId, userId, endpoint)
    End Sub
    
    ' Violation: Async method logging with mismatch
    Public Async Function AsyncLoggingMismatch() As Task
        Dim taskId As String = "TASK789"
        Dim startTime As DateTime = DateTime.Now
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogInformation("Starting async task {TaskId} at {StartTime} with priority {Priority}", taskId, startTime)
        
        Await Task.Delay(1000)
        
        ' Violation: 2 placeholders but 3 parameters
        _logger.LogInformation("Completed task {TaskId}", taskId, startTime, "High")
    End Function
    
    ' Violation: Property logging with mismatch
    Public ReadOnly Property LoggingProperty As String
        Get
            Dim propertyName As String = "LoggingProperty"
            Dim accessTime As DateTime = DateTime.Now
            
            ' Violation: 3 placeholders but only 2 parameters
            _logger.LogDebug("Accessing property {PropertyName} at {AccessTime} with value {Value}", propertyName, accessTime)
            
            Return "PropertyValue"
        End Get
    End Property
    
    ' Violation: Event handler logging with mismatch
    Public Sub OnDataReceived(sender As Object, e As EventArgs)
        Dim senderType As String = sender?.GetType().Name
        Dim eventTime As DateTime = DateTime.Now
        
        ' Violation: 3 placeholders but only 2 parameters
        _logger.LogInformation("Event received from {SenderType} at {EventTime} with data {Data}", senderType, eventTime)
    End Sub
    
End Class
