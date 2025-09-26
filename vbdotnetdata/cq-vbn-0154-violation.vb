' Test file for cq-vbn-0154: Prefer 'Convert.ToHexString' and 'Convert.ToHexStringLower' over call chains based on 'BitConverter.ToString'
' Use Convert.ToHexString or Convert.ToHexStringLower when encoding bytes to a hexadecimal string representation

Imports System

Public Class HexConversionTests
    
    ' Violation: BitConverter.ToString with Replace to remove dashes
    Public Sub ConvertBytesToHexWithReplace()
        Dim bytes() As Byte = {255, 128, 64, 32}
        
        ' Violation: BitConverter.ToString followed by Replace
        Dim hex As String = BitConverter.ToString(bytes).Replace("-", "")
        
        Console.WriteLine($"Hex: {hex}")
    End Sub
    
    ' Violation: BitConverter.ToString with Replace and ToLower
    Public Sub ConvertBytesToLowerHexWithReplace()
        Dim data() As Byte = {10, 20, 30, 40, 50}
        
        ' Violation: BitConverter.ToString with Replace and ToLower chain
        Dim lowerHex As String = BitConverter.ToString(data).Replace("-", "").ToLower()
        
        Console.WriteLine($"Lower hex: {lowerHex}")
    End Sub
    
    ' Violation: BitConverter.ToString with different spacing in Replace
    Public Sub ConvertWithDifferentSpacing()
        Dim bytes() As Byte = {170, 187, 204, 221}
        
        ' Violation: Replace with different whitespace
        Dim hex As String = BitConverter.ToString(bytes).Replace("-", "")
        
        Console.WriteLine($"Hex without dashes: {hex}")
    End Sub
    
    ' Violation: BitConverter in loop
    Public Sub ConvertMultipleByteArrays()
        Dim arrays()() As Byte = {
            New Byte() {1, 2, 3},
            New Byte() {4, 5, 6},
            New Byte() {7, 8, 9}
        }
        
        For Each arr In arrays
            ' Violation: BitConverter.ToString with Replace in loop
            Dim hex As String = BitConverter.ToString(arr).Replace("-", "")
            Console.WriteLine($"Array hex: {hex}")
        Next
    End Sub
    
    ' Violation: BitConverter with method chaining
    Public Function GetHexString(data() As Byte) As String
        ' Violation: Method chaining with BitConverter
        Return BitConverter.ToString(data).Replace("-", "").ToLower()
    End Function
    
    ' Violation: BitConverter with partial array
    Public Sub ConvertPartialArray()
        Dim fullArray() As Byte = {1, 2, 3, 4, 5, 6, 7, 8}
        
        ' Violation: BitConverter with startIndex and length, then Replace
        Dim partialHex As String = BitConverter.ToString(fullArray, 2, 4).Replace("-", "")
        
        Console.WriteLine($"Partial hex: {partialHex}")
    End Sub
    
    ' Good practice: Using Convert.ToHexString (should not be detected)
    Public Sub ConvertWithModernMethod()
        Dim bytes() As Byte = {255, 128, 64, 32}
        
        ' Good: Using Convert.ToHexString
        Dim hex As String = Convert.ToHexString(bytes)
        
        Console.WriteLine($"Modern hex: {hex}")
    End Sub
    
    ' Good: Using Convert.ToHexStringLower
    Public Sub ConvertWithModernLowerMethod()
        Dim bytes() As Byte = {255, 128, 64, 32}
        
        ' Good: Using Convert.ToHexStringLower
        Dim lowerHex As String = Convert.ToHexStringLower(bytes)
        
        Console.WriteLine($"Modern lower hex: {lowerHex}")
    End Sub
    
    ' Good: BitConverter.ToString without Replace (should not be detected)
    Public Sub ConvertWithoutReplace()
        Dim bytes() As Byte = {255, 128, 64, 32}
        
        ' Good: BitConverter.ToString without Replace (keeps dashes)
        Dim hexWithDashes As String = BitConverter.ToString(bytes)
        
        Console.WriteLine($"Hex with dashes: {hexWithDashes}")
    End Sub
    
    ' Violation: Complex BitConverter usage
    Public Sub ComplexConversion()
        Dim guid As Guid = Guid.NewGuid()
        Dim guidBytes() As Byte = guid.ToByteArray()
        
        ' Violation: Complex BitConverter chain
        Dim hexGuid As String = BitConverter.ToString(guidBytes).Replace("-", "").ToLower()
        
        Console.WriteLine($"GUID as hex: {hexGuid}")
    End Sub
    
    ' Violation: BitConverter in conditional
    Public Sub ConditionalConversion(useUpperCase As Boolean)
        Dim data() As Byte = {100, 150, 200, 250}
        
        Dim hex As String
        If useUpperCase Then
            ' Violation: BitConverter with Replace
            hex = BitConverter.ToString(data).Replace("-", "")
        Else
            ' Violation: BitConverter with Replace and ToLower
            hex = BitConverter.ToString(data).Replace("-", "").ToLower()
        End If
        
        Console.WriteLine($"Conditional hex: {hex}")
    End Sub
    
End Class

' Additional test cases
Public Module HexUtilities
    
    ' Violation: Utility method with BitConverter
    Public Function BytesToHex(bytes() As Byte) As String
        ' Violation: BitConverter.ToString with Replace
        Return BitConverter.ToString(bytes).Replace("-", "")
    End Function
    
    ' Violation: Utility method with lower case conversion
    Public Function BytesToLowerHex(bytes() As Byte) As String
        ' Violation: BitConverter.ToString with Replace and ToLower
        Return BitConverter.ToString(bytes).Replace("-", "").ToLower()
    End Function
    
    ' Violation: Hash conversion utility
    Public Function HashToHex(hashBytes() As Byte) As String
        ' Violation: Converting hash bytes using BitConverter
        Return BitConverter.ToString(hashBytes).Replace("-", "")
    End Function
    
    ' Good: Modern utility methods
    Public Function ModernBytesToHex(bytes() As Byte) As String
        Return Convert.ToHexString(bytes)
    End Function
    
    Public Function ModernBytesToLowerHex(bytes() As Byte) As String
        Return Convert.ToHexStringLower(bytes)
    End Function
    
    ' Violation: Processing file hash
    Public Sub DisplayFileHash()
        ' Simulate getting file hash
        Dim hashBytes() As Byte = {&H12, &H34, &H56, &H78, &H9A, &HBC, &HDE, &HF0}
        
        ' Violation: BitConverter for file hash display
        Dim hashString As String = BitConverter.ToString(hashBytes).Replace("-", "").ToLower()
        
        Console.WriteLine($"File hash: {hashString}")
    End Sub
    
End Module

' Test with different data types
Public Class DataConversionTests
    
    ' Violation: Converting integer to hex
    Public Sub ConvertIntegerToHex(value As Integer)
        Dim bytes() As Byte = BitConverter.GetBytes(value)
        
        ' Violation: BitConverter.ToString with Replace
        Dim hex As String = BitConverter.ToString(bytes).Replace("-", "")
        
        Console.WriteLine($"Integer {value} as hex: {hex}")
    End Sub
    
    ' Violation: Converting long to hex
    Public Sub ConvertLongToHex(value As Long)
        Dim bytes() As Byte = BitConverter.GetBytes(value)
        
        ' Violation: BitConverter.ToString with Replace and ToLower
        Dim hex As String = BitConverter.ToString(bytes).Replace("-", "").ToLower()
        
        Console.WriteLine($"Long {value} as hex: {hex}")
    End Sub
    
End Class
