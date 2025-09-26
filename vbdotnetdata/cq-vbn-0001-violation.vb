' Test file for cq-vbn-0001: Types that own disposable fields should be disposable
' This file should trigger violations for types with disposable fields that don't implement IDisposable

Imports System
Imports System.IO

' Violation: Class with disposable field (single line pattern)
Public Class FileProcessor : Private fileStream As FileStream : End Class

' Violation: Class with StreamReader field (single line pattern)  
Protected Class DataReader : Private reader As StreamReader : End Class

' Violation: Structure with disposable field (single line pattern)
Public Structure DataProcessor : Private memoryStream As MemoryStream : End Structure

' Violation: Class with multiple disposable fields (single line pattern)
Friend Class ConnectionManager : Private connection As SqlConnection : Private command As SqlCommand : End Class

' Non-violation: Class without disposable fields (should not trigger)
Public Class SimpleCalculator
    Private value As Integer
    Private name As String
    
    Public Function Add(x As Integer, y As Integer) As Integer
        Return x + y
    End Function
End Class

' Non-violation: Class with disposable field that implements IDisposable (should not trigger)
Public Class ProperFileProcessor
    Implements IDisposable
    
    Private fileStream As FileStream
    
    Public Sub Dispose() Implements IDisposable.Dispose
        fileStream?.Dispose()
    End Sub
End Class
