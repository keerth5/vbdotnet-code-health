' Test file for cq-vbn-0166: Do not use ReferenceEquals with value types
' When comparing values using System.Object.ReferenceEquals, if objA and objB are value types, they are boxed before they are passed to the ReferenceEquals method. This means that even if both objA and objB represent the same instance of a value type, the ReferenceEquals method nevertheless returns false.

Imports System

Public Class ReferenceEqualsTests
    
    ' Violation: ReferenceEquals with Integer
    Public Sub CompareIntegers()
        Dim a As Integer = 42
        Dim b As Integer = 42
        
        ' Violation: Using ReferenceEquals with Integer values
        Dim areEqual As Boolean = Object.ReferenceEquals(a, b)
        
        Console.WriteLine($"Integer ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Double
    Public Sub CompareDoubles()
        Dim x As Double = 3.14
        Dim y As Double = 3.14
        
        ' Violation: Using ReferenceEquals with Double values
        Dim areEqual As Boolean = Object.ReferenceEquals(x, y)
        
        Console.WriteLine($"Double ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Boolean
    Public Sub CompareBooleans()
        Dim flag1 As Boolean = True
        Dim flag2 As Boolean = True
        
        ' Violation: Using ReferenceEquals with Boolean values
        Dim areEqual As Boolean = Object.ReferenceEquals(flag1, flag2)
        
        Console.WriteLine($"Boolean ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Decimal
    Public Sub CompareDecimals()
        Dim amount1 As Decimal = 100.50D
        Dim amount2 As Decimal = 100.50D
        
        ' Violation: Using ReferenceEquals with Decimal values
        Dim areEqual As Boolean = Object.ReferenceEquals(amount1, amount2)
        
        Console.WriteLine($"Decimal ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with DateTime
    Public Sub CompareDateTimes()
        Dim date1 As DateTime = New DateTime(2023, 1, 1)
        Dim date2 As DateTime = New DateTime(2023, 1, 1)
        
        ' Violation: Using ReferenceEquals with DateTime values
        Dim areEqual As Boolean = Object.ReferenceEquals(date1, date2)
        
        Console.WriteLine($"DateTime ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Single (Float)
    Public Sub CompareSingles()
        Dim value1 As Single = 2.5F
        Dim value2 As Single = 2.5F
        
        ' Violation: Using ReferenceEquals with Single values
        Dim areEqual As Boolean = Object.ReferenceEquals(value1, value2)
        
        Console.WriteLine($"Single ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Long
    Public Sub CompareLongs()
        Dim big1 As Long = 9223372036854775807L
        Dim big2 As Long = 9223372036854775807L
        
        ' Violation: Using ReferenceEquals with Long values
        Dim areEqual As Boolean = Object.ReferenceEquals(big1, big2)
        
        Console.WriteLine($"Long ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Short
    Public Sub CompareShorts()
        Dim small1 As Short = 32767S
        Dim small2 As Short = 32767S
        
        ' Violation: Using ReferenceEquals with Short values
        Dim areEqual As Boolean = Object.ReferenceEquals(small1, small2)
        
        Console.WriteLine($"Short ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Byte
    Public Sub CompareBytes()
        Dim byte1 As Byte = 255
        Dim byte2 As Byte = 255
        
        ' Violation: Using ReferenceEquals with Byte values
        Dim areEqual As Boolean = Object.ReferenceEquals(byte1, byte2)
        
        Console.WriteLine($"Byte ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with SByte
    Public Sub CompareSBytes()
        Dim sbyte1 As SByte = -128
        Dim sbyte2 As SByte = -128
        
        ' Violation: Using ReferenceEquals with SByte values
        Dim areEqual As Boolean = Object.ReferenceEquals(sbyte1, sbyte2)
        
        Console.WriteLine($"SByte ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with UInteger
    Public Sub CompareUIntegers()
        Dim uint1 As UInteger = 4294967295UI
        Dim uint2 As UInteger = 4294967295UI
        
        ' Violation: Using ReferenceEquals with UInteger values
        Dim areEqual As Boolean = Object.ReferenceEquals(uint1, uint2)
        
        Console.WriteLine($"UInteger ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with ULong
    Public Sub CompareULongs()
        Dim ulong1 As ULong = 18446744073709551615UL
        Dim ulong2 As ULong = 18446744073709551615UL
        
        ' Violation: Using ReferenceEquals with ULong values
        Dim areEqual As Boolean = Object.ReferenceEquals(ulong1, ulong2)
        
        Console.WriteLine($"ULong ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with UShort
    Public Sub CompareUShorts()
        Dim ushort1 As UShort = 65535US
        Dim ushort2 As UShort = 65535US
        
        ' Violation: Using ReferenceEquals with UShort values
        Dim areEqual As Boolean = Object.ReferenceEquals(ushort1, ushort2)
        
        Console.WriteLine($"UShort ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with Char
    Public Sub CompareChars()
        Dim char1 As Char = "A"c
        Dim char2 As Char = "A"c
        
        ' Violation: Using ReferenceEquals with Char values
        Dim areEqual As Boolean = Object.ReferenceEquals(char1, char2)
        
        Console.WriteLine($"Char ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Good practice: ReferenceEquals with reference types (should not be detected)
    Public Sub CompareStrings()
        Dim str1 As String = "Hello"
        Dim str2 As String = "Hello"
        
        ' Good: Using ReferenceEquals with reference types
        Dim areEqual As Boolean = Object.ReferenceEquals(str1, str2)
        
        Console.WriteLine($"String ReferenceEquals: {areEqual}")
    End Sub
    
    ' Good: ReferenceEquals with objects
    Public Sub CompareObjects()
        Dim obj1 As New Object()
        Dim obj2 As Object = obj1
        
        ' Good: Using ReferenceEquals with reference types
        Dim areEqual As Boolean = Object.ReferenceEquals(obj1, obj2)
        
        Console.WriteLine($"Object ReferenceEquals: {areEqual}")
    End Sub
    
    ' Violation: ReferenceEquals in conditional
    Public Sub ConditionalReferenceEquals()
        Dim value1 As Integer = 10
        Dim value2 As Integer = 20
        
        ' Violation: ReferenceEquals with Integer in conditional
        If Object.ReferenceEquals(value1, value2) Then
            Console.WriteLine("Values are reference equal")
        Else
            Console.WriteLine("Values are not reference equal")
        End If
    End Sub
    
    ' Violation: ReferenceEquals in loop
    Public Sub ReferenceEqualsInLoop()
        Dim values() As Double = {1.0, 2.0, 3.0, 1.0}
        
        For i As Integer = 0 To values.Length - 2
            For j As Integer = i + 1 To values.Length - 1
                ' Violation: ReferenceEquals with Double in loop
                If Object.ReferenceEquals(values(i), values(j)) Then
                    Console.WriteLine($"Found reference equal values at {i} and {j}")
                End If
            Next
        Next
    End Sub
    
    ' Violation: ReferenceEquals with method return values
    Public Sub CompareMethodReturnValues()
        ' Violation: ReferenceEquals with Integer return values
        Dim areEqual As Boolean = Object.ReferenceEquals(GetInteger(), GetInteger())
        
        Console.WriteLine($"Method return ReferenceEquals: {areEqual}")
    End Sub
    
    Private Function GetInteger() As Integer
        Return 42
    End Function
    
End Class

' Additional test cases
Public Module ReferenceEqualsUtilities
    
    ' Violation: Utility method using ReferenceEquals with value types
    Public Function AreReferenceEqual(Of T)(value1 As T, value2 As T) As Boolean
        ' This could be a violation if T is a value type
        Return Object.ReferenceEquals(value1, value2)
    End Function
    
    ' Violation: Specific utility methods for value types
    Public Function AreIntegersReferenceEqual(a As Integer, b As Integer) As Boolean
        ' Violation: ReferenceEquals with Integer parameters
        Return Object.ReferenceEquals(a, b)
    End Function
    
    Public Function AreBooleansReferenceEqual(flag1 As Boolean, flag2 As Boolean) As Boolean
        ' Violation: ReferenceEquals with Boolean parameters
        Return Object.ReferenceEquals(flag1, flag2)
    End Function
    
    ' Good: Utility method for reference types
    Public Function AreStringsReferenceEqual(str1 As String, str2 As String) As Boolean
        ' Good: ReferenceEquals with String (reference type)
        Return Object.ReferenceEquals(str1, str2)
    End Function
    
End Module

' Test with custom value types
Public Structure CustomStruct
    Public Value As Integer
    Public Name As String
    
    Public Sub New(value As Integer, name As String)
        Me.Value = value
        Me.Name = name
    End Sub
End Structure

Public Class CustomValueTypeTests
    
    ' Violation: ReferenceEquals with custom struct
    Public Sub CompareCustomStructs()
        Dim struct1 As New CustomStruct(42, "Test")
        Dim struct2 As New CustomStruct(42, "Test")
        
        ' Violation: ReferenceEquals with custom struct (value type)
        Dim areEqual As Boolean = Object.ReferenceEquals(struct1, struct2)
        
        Console.WriteLine($"Custom struct ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with nullable value types
    Public Sub CompareNullableIntegers()
        Dim nullable1 As Integer? = 42
        Dim nullable2 As Integer? = 42
        
        ' Violation: ReferenceEquals with nullable Integer (still value type)
        Dim areEqual As Boolean = Object.ReferenceEquals(nullable1, nullable2)
        
        Console.WriteLine($"Nullable Integer ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
    ' Violation: ReferenceEquals with enum
    Public Sub CompareEnums()
        Dim status1 As ConsoleColor = ConsoleColor.Red
        Dim status2 As ConsoleColor = ConsoleColor.Red
        
        ' Violation: ReferenceEquals with enum (value type)
        Dim areEqual As Boolean = Object.ReferenceEquals(status1, status2)
        
        Console.WriteLine($"Enum ReferenceEquals: {areEqual}") ' Will be False
    End Sub
    
End Class

' Test with advanced scenarios
Public Class AdvancedReferenceEqualsScenarios
    
    ' Violation: ReferenceEquals in exception handling
    Public Sub ReferenceEqualsInExceptionHandling()
        Try
            Dim value1 As Integer = 10
            Dim value2 As Integer = 10
            
            ' Violation: ReferenceEquals with Integer in try block
            If Object.ReferenceEquals(value1, value2) Then
                Throw New InvalidOperationException("Should not be reference equal")
            End If
        Catch ex As Exception
            Console.WriteLine($"Exception: {ex.Message}")
        End Try
    End Sub
    
    ' Violation: ReferenceEquals with property values
    Public Sub ComparePropertyValues()
        Dim obj As New TestClass()
        
        ' Violation: ReferenceEquals with Integer properties
        Dim areEqual As Boolean = Object.ReferenceEquals(obj.IntegerProperty, obj.IntegerProperty)
        
        Console.WriteLine($"Property ReferenceEquals: {areEqual}")
    End Sub
    
    ' Violation: ReferenceEquals with array elements
    Public Sub CompareArrayElements()
        Dim numbers() As Integer = {1, 2, 3, 1}
        
        ' Violation: ReferenceEquals with Integer array elements
        Dim areEqual As Boolean = Object.ReferenceEquals(numbers(0), numbers(3))
        
        Console.WriteLine($"Array element ReferenceEquals: {areEqual}")
    End Sub
    
End Class

Public Class TestClass
    Public Property IntegerProperty As Integer = 42
End Class
