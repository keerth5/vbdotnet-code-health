' Test file for cq-vbn-0080: Do not name enum values 'Reserved'
' Rule should detect enum values named 'Reserved' which can cause COM interop conflicts

Imports System

' Violation 1: Public enum with Reserved value
Public Enum StatusEnum
    None = 0
    Active = 1
    Inactive = 2
    Reserved = 3  ' Violation: Reserved enum value
    Pending = 4
End Enum

' Violation 2: Friend enum with Reserved value
Friend Enum ProcessStateEnum
    Idle = 0
    Running = 1
    Reserved = 2  ' Violation: Reserved enum value
    Stopped = 3
End Enum

' This should NOT be detected - enum without Reserved value
Public Enum ColorEnum
    Red = 1
    Green = 2
    Blue = 3
    Yellow = 4
End Enum

' Violation 3: Enum with Reserved value and assignment
Public Enum ErrorCodeEnum
    Success = 0
    Warning = 1
    Error = 2
    Reserved = 999  ' Violation: Reserved enum value with explicit assignment
End Enum

' This should NOT be detected - ReservedSpace (not exactly 'Reserved')
Public Enum MemoryEnum
    Used = 0
    Free = 1
    ReservedSpace = 2  ' Should not be detected - different name
End Enum

' Violation 4: Enum with multiple Reserved-like values
Public Enum ComplexEnum
    Value1 = 1
    Value2 = 2
    Reserved = 10  ' Violation: Reserved enum value
    Value3 = 3
    ReservedValue = 11  ' This might be detected depending on pattern
End Enum

' This should NOT be detected - Private enum
Private Enum InternalEnum
    Option1 = 0
    Option2 = 1
    Reserved = 2  ' Private enum, might not be detected
End Enum

' Violation 5: Flags enum with Reserved value
<Flags>
Public Enum PermissionEnum
    None = 0
    Read = 1
    Write = 2
    Execute = 4
    Reserved = 8  ' Violation: Reserved enum value in flags enum
    All = Read Or Write Or Execute
End Enum

' This should NOT be detected - enum with similar but different names
Public Enum SimilarEnum
    Standard = 0
    Custom = 1
    Preserve = 2  ' Should not be detected
    Reserve = 3   ' Should not be detected
End Enum

' Violation 6: Simple enum with Reserved
Public Enum SimpleEnum
    First
    Second
    Reserved  ' Violation: Reserved enum value without explicit assignment
    Third
End Enum

' This should NOT be detected - Interface (not enum)
Public Interface IReservedInterface
    Sub ReservedMethod()
End Interface

' Violation 7: Enum with Reserved in different case
Public Enum CaseTestEnum
    Normal = 0
    Special = 1
    RESERVED = 2  ' Violation: Reserved in uppercase
End Enum

' This should NOT be detected - Class with Reserved property
Public Class ReservedClass
    Public Property Reserved As String
    
    Public Sub New()
        Reserved = "Default"
    End Sub
End Class

' Violation 8: Another enum with Reserved value
Friend Enum NetworkStateEnum
    Disconnected = 0
    Connecting = 1
    Connected = 2
    Reserved = 3  ' Violation: Reserved enum value
    Error = 4
End Enum

' This should NOT be detected - Enum with ReservedFor prefix
Public Enum ExtendedEnum
    Basic = 0
    Advanced = 1
    ReservedForFuture = 2  ' Should not be detected - different name
End Enum

' Violation 9: Enum with Reserved and other values
Public Enum FinalEnum
    Alpha = 1
    Beta = 2
    Gamma = 3
    Reserved = 0  ' Violation: Reserved enum value
    Delta = 4
End Enum
