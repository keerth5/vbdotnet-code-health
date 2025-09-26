' VB.NET test file for cq-vbn-0110: Avoid zero-length array allocations
' Rule: Initializing a zero-length array leads to unnecessary memory allocation. Instead, use the 
' statically allocated empty array instance by calling Array.Empty. The memory allocation is shared 
' across all invocations of this method.

Imports System
Imports System.Collections.Generic

Public Class ArrayAllocationExamples
    
    Public Sub TestZeroLengthArrays()
        
        ' Violation: Creating zero-length array with New and size 0
        Dim emptyIntArray As Integer() = New Integer(0) {}
        
        ' Violation: Creating zero-length array with different syntax
        Dim emptyStringArray As String() = New String(-1) {}
        
        ' Violation: Creating zero-length array in method call
        ProcessArray(New Integer(0) {})
        
        ' Violation: Creating zero-length array in return statement
        Dim result = GetEmptyArray()
        
        ' Violation: Creating zero-length array with explicit size
        Dim emptyDoubleArray As Double() = New Double(0) {}
        
        ' Violation: Creating zero-length array with negative size
        Dim emptyByteArray As Byte() = New Byte(-1) {}
        
        ' Violation: Creating zero-length array of custom type
        Dim emptyCustomArray As CustomType() = New CustomType(0) {}
        
        ' Violation: Creating zero-length array in assignment
        Dim data As Integer() = New Integer(0) {}
        
        ' Violation: Creating zero-length array in field assignment
        _emptyField = New String(0) {}
        
        ' Violation: Creating zero-length array in property assignment
        EmptyProperty = New Integer(0) {}
        
    End Sub
    
    Public Sub MoreZeroLengthExamples()
        
        ' Violation: Zero-length array in variable declaration
        Dim chars As Char() = New Char(-1) {}
        
        ' Violation: Zero-length array in loop
        For i As Integer = 0 To 5
            Dim temp As Integer() = New Integer(0) {}
            ProcessArray(temp)
        Next
        
        ' Violation: Zero-length array in conditional
        If SomeCondition() Then
            Dim conditional As String() = New String(-1) {}
            ProcessStringArray(conditional)
        End If
        
        ' Violation: Zero-length array in try-catch
        Try
            Dim errorArray As Exception() = New Exception(0) {}
        Catch ex As Exception
            ' Handle exception
        End Try
        
    End Sub
    
    Private Function GetEmptyArray() As Integer()
        ' Violation: Returning zero-length array
        Return New Integer(0) {}
    End Function
    
    Private Function GetEmptyStringArray() As String()
        ' Violation: Returning zero-length array with negative size
        Return New String(-1) {}
    End Function
    
    Private Sub ProcessArray(arr As Integer())
        ' Process array
    End Sub
    
    Private Sub ProcessStringArray(arr As String())
        ' Process string array
    End Sub
    
    Private Function SomeCondition() As Boolean
        Return True
    End Function
    
    ' Field for testing
    Private _emptyField As String()
    
    ' Property for testing
    Public Property EmptyProperty As Integer()
    
End Class

Public Class GenericArrayExamples
    
    Public Sub TestGenericZeroLengthArrays()
        
        ' Violation: Zero-length array of List type
        Dim emptyListArray As List(Of String)() = New List(Of String)(0) {}
        
        ' Violation: Zero-length array of Dictionary type
        Dim emptyDictArray As Dictionary(Of String, Integer)() = New Dictionary(Of String, Integer)(-1) {}
        
        ' Violation: Zero-length array of generic type
        Dim emptyGenericArray As T() = New T(0) {}
        
    End Sub
    
End Class

Public Class CustomType
    Public Property Name As String
    Public Property Value As Integer
End Class

Public Class StaticMethodExamples
    
    Public Shared Function CreateEmptyIntArray() As Integer()
        ' Violation: Static method returning zero-length array
        Return New Integer(0) {}
    End Function
    
    Public Shared Function CreateEmptyStringArray() As String()
        ' Violation: Static method returning zero-length array with negative size
        Return New String(-1) {}
    End Function
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperArrayExamples
    
    Public Sub TestProperArrayCreation()
        
        ' Correct: Using Array.Empty - should not be detected
        Dim emptyArray As Integer() = Array.Empty(Of Integer)()
        
        ' Correct: Creating array with actual elements - should not be detected
        Dim dataArray As Integer() = New Integer() {1, 2, 3}
        
        ' Correct: Creating array with specific size > 0 - should not be detected
        Dim sizedArray As String() = New String(4) {}
        
        ' Correct: Creating array with size 1 - should not be detected
        Dim singleArray As Integer() = New Integer(1) {}
        
        ' Correct: Using collection instead of array - should not be detected
        Dim list As New List(Of Integer)()
        
        ' Correct: Using Nothing/null instead of empty array - should not be detected
        Dim nullArray As Integer() = Nothing
        
    End Sub
    
    Public Function GetProperEmptyArray() As Integer()
        ' Correct: Using Array.Empty - should not be detected
        Return Array.Empty(Of Integer)()
    End Function
    
    Public Sub ProcessData()
        ' Correct: Creating non-empty arrays - should not be detected
        Dim numbers As Integer() = New Integer() {1, 2, 3, 4, 5}
        Dim names As String() = New String() {"Alice", "Bob", "Charlie"}
        
        ProcessArray(numbers)
        ProcessStringArray(names)
    End Sub
    
    Private Sub ProcessArray(arr As Integer())
        ' Process array
    End Sub
    
    Private Sub ProcessStringArray(arr As String())
        ' Process string array
    End Sub
    
End Class
