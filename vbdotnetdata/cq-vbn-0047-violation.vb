' Test file for cq-vbn-0047: Enums should not have duplicate values
' Rule should detect enums with duplicate values that can cause ambiguity

Imports System

' Violation 1: Public enum with duplicate values
Public Enum Status
    Unknown = 0
    Pending = 1
    InProgress = 2
    Completed = 3
    Failed = 2  ' Duplicate value - same as InProgress
End Enum

' Violation 2: Friend enum with duplicate values
Friend Enum Priority
    Low = 1
    Normal = 2
    High = 3
    Critical = 3  ' Duplicate value - same as High
    Urgent = 4
End Enum

' Violation 3: Enum with multiple duplicate values
Public Enum ErrorCode
    None = 0
    InvalidInput = 100
    NetworkError = 200
    DatabaseError = 300
    ValidationError = 100  ' Duplicate of InvalidInput
    ConnectionError = 200  ' Duplicate of NetworkError
End Enum

' Violation 4: Enum with explicit duplicate assignment
Friend Enum LogLevel
    Trace = 1
    Debug = 2
    Info = 3
    Warning = 4
    Error = 5
    Fatal = 5  ' Duplicate of Error
End Enum

' This should NOT be detected - enum without duplicate values
Public Enum OrderStatus
    Created = 1
    Confirmed = 2
    Shipped = 3
    Delivered = 4
    Cancelled = 5
End Enum

' This should NOT be detected - enum with flags (duplicates may be intentional)
<Flags>
Public Enum FileAccess
    None = 0
    Read = 1
    Write = 2
    Execute = 4
    ReadWrite = Read Or Write  ' This is 3, which is intentional combination
    All = Read Or Write Or Execute  ' This is 7, intentional combination
End Enum

' This should NOT be detected - enum with sequential values
Public Enum DayOfWeek
    Sunday = 0
    Monday = 1
    Tuesday = 2
    Wednesday = 3
    Thursday = 4
    Friday = 5
    Saturday = 6
End Enum

' This should NOT be detected - enum without explicit values (auto-incremented)
Public Enum Color
    Red
    Green
    Blue
    Yellow
    Orange
End Enum

' This should NOT be detected - enum with unique explicit values
Friend Enum HttpStatusCode
    OK = 200
    NotFound = 404
    InternalServerError = 500
    BadRequest = 400
    Unauthorized = 401
End Enum

' Violation 5: Another enum with duplicate values in different pattern
Public Enum ProcessingState
    Idle = 0
    Starting = 1
    Running = 2
    Stopping = 3
    Stopped = 0  ' Duplicate of Idle
    Error = 4
End Enum
