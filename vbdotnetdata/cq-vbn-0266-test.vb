' Test file for cq-vbn-0266: Do not mark enums with FlagsAttribute
' Rule should detect: Enums with FlagsAttribute that have non-power-of-two values

Imports System

' VIOLATION: FlagsAttribute enum with non-power-of-two values
<Flags>
Public Enum BadFlags1
    None = 0
    First = 1
    Second = 2
    Third = 3      ' Violation: 3 is not a power of 2
    Fourth = 4
End Enum

' VIOLATION: FlagsAttribute enum with odd values
<Flags>
Public Enum BadFlags2
    Option1 = 1
    Option2 = 3    ' Violation: 3 is not a power of 2
    Option3 = 5    ' Violation: 5 is not a power of 2
    Option4 = 8
End Enum

' VIOLATION: FlagsAttribute enum with mixed invalid values
<Flags>
Friend Enum BadFlags3
    None = 0
    Alpha = 1
    Beta = 2
    Gamma = 7      ' Violation: 7 is not a power of 2 or combination
    Delta = 16
End Enum

' GOOD: FlagsAttribute enum with proper power-of-two values - should NOT be flagged
<Flags>
Public Enum GoodFlags1
    None = 0
    Read = 1
    Write = 2
    Execute = 4
    ReadWrite = 3  ' 1 + 2 = valid combination
    All = 7        ' 1 + 2 + 4 = valid combination
End Enum

' GOOD: Regular enum without FlagsAttribute - should NOT be flagged
Public Enum RegularEnum
    First = 1
    Second = 3
    Third = 5
    Fourth = 7
End Enum

' GOOD: FlagsAttribute enum with all power-of-two values - should NOT be flagged
<Flags>
Public Enum GoodFlags2
    None = 0
    Bit0 = 1
    Bit1 = 2
    Bit2 = 4
    Bit3 = 8
    Bit4 = 16
    Bit5 = 32
End Enum
