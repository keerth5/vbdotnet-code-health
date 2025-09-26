' Test file for cq-vbn-0016: Enum storage should be Int32
' Rule should detect enums using non-Int32 underlying types

Imports System

' Violation 1: Enum using Byte as underlying type
Public Enum Priority As Byte
    Low = 1
    Medium = 2
    High = 3
End Enum

' Violation 2: Enum using Short as underlying type
Protected Enum StatusCode As Short
    Success = 200
    NotFound = 404
    ServerError = 500
End Enum

' Violation 3: Enum using Long as underlying type
Friend Enum LargeValues As Long
    Value1 = 1000000000L
    Value2 = 2000000000L
End Enum

' Violation 4: Enum using UInteger as underlying type
Private Enum UnsignedValues As UInteger
    Min = 0UI
    Max = 4294967295UI
End Enum

' Violation 5: Enum using SByte as underlying type
Public Enum SmallRange As SByte
    Negative = -128
    Zero = 0
    Positive = 127
End Enum

' Violation 6: Enum using UShort as underlying type
Public Enum MediumRange As UShort
    Start = 0US
    Middle = 32767US
    End = 65535US
End Enum

' Violation 7: Enum using ULong as underlying type
Public Enum VeryLargeValues As ULong
    Huge1 = 18446744073709551615UL
    Huge2 = 9223372036854775808UL
End Enum

' This should NOT be detected - enum using default Int32 (no explicit type)
Public Enum DefaultEnum
    Value1 = 1
    Value2 = 2
    Value3 = 3
End Enum

' This should NOT be detected - enum explicitly using Integer (Int32)
Public Enum ExplicitInteger As Integer
    First = 1
    Second = 2
    Third = 3
End Enum
