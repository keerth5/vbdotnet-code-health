' Test file for cq-vbn-0005: Enums should have zero value
' This file should trigger violations for enums without explicit zero values

Imports System

' Violation: Enum without explicit zero value
Public Enum Status
    Active = 1
    Inactive = 2
    Pending = 3
End Enum

' Violation: Enum starting from non-zero value
Protected Enum Priority
    Low = 10
    Medium = 20
    High = 30
End Enum

' Violation: Enum with gaps but no zero
Private Enum ErrorCode
    NotFound = 404
    ServerError = 500
    BadRequest = 400
End Enum

' Violation: Enum without any explicit values (but should have explicit zero)
Friend Enum Direction
    North
    South
    East
    West
End Enum

' Non-violation: Enum with explicit zero value (should not trigger)
Public Enum State
    None = 0
    Running = 1
    Stopped = 2
End Enum

' Non-violation: Enum with implicit zero as first value (should not trigger)
Public Enum Color
    Red     ' This is implicitly 0
    Green   ' This is implicitly 1
    Blue    ' This is implicitly 2
End Enum
