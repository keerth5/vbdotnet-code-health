' Test file for cq-vbn-0015: Mark enums with FlagsAttribute
' Rule should detect enums with power-of-2 values that should have FlagsAttribute

Imports System

' Violation 1: Enum with power-of-2 values but missing FlagsAttribute
Public Enum FilePermissions
    None = 0
    Read = 1
    Write = 2
    Execute = 4
    ReadWrite = 8
    All = 16
End Enum

' Violation 2: Another enum with bitwise values
Protected Enum UserRoles
    Guest = 1
    User = 2
    Admin = 4
    SuperAdmin = 8
End Enum

' Violation 3: Friend enum with power-of-2 pattern
Friend Enum ProcessingFlags
    Default = 1
    Async = 2
    Cached = 4
    Validated = 8
    Compressed = 16
End Enum

' Violation 4: Private enum with bitwise pattern
Private Enum InternalFlags
    Flag1 = 1
    Flag2 = 2
    Flag3 = 4
End Enum

' This should NOT be detected - enum without power-of-2 values
Public Enum Status
    Pending = 0
    InProgress = 1
    Completed = 2
    Failed = 3
End Enum

' This should NOT be detected - enum with FlagsAttribute (if pattern was correct)
<Flags>
Public Enum CorrectFlags
    None = 0
    Option1 = 1
    Option2 = 2
    Option3 = 4
End Enum
