' Test file for cq-vbn-0085: Do not prefix enum values with type name
' Rule should detect enum values that are prefixed with the enum type name

Imports System

' Violation 1: Enum with values prefixed with enum name
Public Enum Color
    ColorRed = 1
    ColorGreen = 2
    ColorBlue = 3
    ColorYellow = 4
End Enum

' Violation 2: Enum with values prefixed with enum name (different case)
Public Enum Status
    StatusActive = 0
    StatusInactive = 1
    StatusPending = 2
    StatusCanceled = 3
End Enum

' Violation 3: Friend enum with prefixed values
Friend Enum Priority
    PriorityLow = 1
    PriorityMedium = 2
    PriorityHigh = 3
    PriorityCritical = 4
End Enum

' Violation 4: Enum with partial prefixing
Public Enum FileType
    FileTypeText = 1
    FileTypeBinary = 2
    FileTypeImage = 3
    Document = 4  ' This one doesn't have prefix but others do
End Enum

' Violation 5: Enum with abbreviated prefix
Public Enum Direction
    DirNorth = 1
    DirSouth = 2
    DirEast = 3
    DirWest = 4
End Enum

' Violation 6: Enum with different prefix pattern
Friend Enum LogLevel
    LogLevelDebug = 0
    LogLevelInfo = 1
    LogLevelWarning = 2
    LogLevelError = 3
    LogLevelFatal = 4
End Enum

' Violation 7: Enum with values having type name prefix
Public Enum ConnectionState
    ConnectionStateConnected = 1
    ConnectionStateDisconnected = 2
    ConnectionStateConnecting = 3
    ConnectionStateTimeout = 4
End Enum

' Violation 8: Enum with mixed prefixing
Public Enum ProcessState
    ProcessStateRunning = 1
    ProcessStateStopped = 2
    Paused = 3  ' No prefix
    ProcessStateTerminated = 4
End Enum

' Violation 9: Enum with type name in different case
Public Enum UserRole
    userroleAdmin = 1
    userroleUser = 2
    userroleGuest = 3
    userroleModerator = 4
End Enum

' Violation 10: Enum with exact type name prefix
Friend Enum ErrorCode
    ErrorCodeSuccess = 0
    ErrorCodeNotFound = 404
    ErrorCodeUnauthorized = 401
    ErrorCodeInternalError = 500
End Enum

' This should NOT be detected - proper enum without type name prefix
Public Enum Gender
    Male = 1
    Female = 2
    Other = 3
End Enum

' This should NOT be detected - proper enum values
Public Enum DayOfWeek
    Monday = 1
    Tuesday = 2
    Wednesday = 3
    Thursday = 4
    Friday = 5
    Saturday = 6
    Sunday = 7
End Enum

' This should NOT be detected - values don't have type name prefix
Friend Enum HttpMethod
    Get = 1
    Post = 2
    Put = 3
    Delete = 4
    Patch = 5
End Enum

' This should NOT be detected - proper naming without redundant prefix
Public Enum Temperature
    Cold = 1
    Warm = 2
    Hot = 3
End Enum

' This should NOT be detected - values are properly named
Public Enum Size
    Small = 1
    Medium = 2
    Large = 3
    ExtraLarge = 4
End Enum

' Violation 11: Another example with type prefix
Public Enum Animal
    AnimalDog = 1
    AnimalCat = 2
    AnimalBird = 3
    AnimalFish = 4
End Enum

' Violation 12: Enum with abbreviated type prefix
Friend Enum Vehicle
    VehicleCar = 1
    VehicleTruck = 2
    VehicleMotorcycle = 3
    VehicleBus = 4
End Enum
