' Test file for cq-vbn-0201: Unsafe DataSet or DataTable in deserialized object graph
' This rule detects deserialization followed by casting to DataSet or DataTable types

Imports System
Imports System.Data
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class UnsafeDataSetDataTableDeserializationViolations
    
    ' Violation: CType with Deserialize and DataSet
    Public Sub CTypeWithDeserializeDataSet()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = CType(formatter.Deserialize(stream), DataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: CType with Deserialize and DataTable
    Public Sub CTypeWithDeserializeDataTable()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = CType(formatter.Deserialize(stream), DataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: DirectCast with Deserialize and DataSet
    Public Sub DirectCastWithDeserializeDataSet()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = DirectCast(formatter.Deserialize(stream), DataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: DirectCast with Deserialize and DataTable
    Public Sub DirectCastWithDeserializeDataTable()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = DirectCast(formatter.Deserialize(stream), DataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: CType with UnsafeDeserialize and DataSet
    Public Sub CTypeWithUnsafeDeserializeDataSet()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = CType(formatter.UnsafeDeserialize(stream, Nothing), DataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: DirectCast with UnsafeDeserialize and DataTable
    Public Sub DirectCastWithUnsafeDeserializeDataTable()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = DirectCast(formatter.UnsafeDeserialize(stream, Nothing), DataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: Multiple operations with CType and DataSet
    Public Sub MultipleOperationsWithCTypeDataSet()
        Dim formatter As New BinaryFormatter()
        Using stream1 As New MemoryStream()
            Using stream2 As New MemoryStream()
                Dim result1 = CType(formatter.Deserialize(stream1), DataSet) ' Violation
                Dim result2 = CType(formatter.Deserialize(stream2), DataTable) ' Violation
            End Using
        End Using
    End Sub
    
    ' Violation: CType in loop with DataSet
    Public Sub CTypeInLoopWithDataSet()
        Dim formatter As New BinaryFormatter()
        For i As Integer = 0 To 5
            Using stream As New MemoryStream()
                Dim result = CType(formatter.Deserialize(stream), DataSet) ' Violation
            End Using
        Next
    End Sub
    
    ' Violation: DirectCast in conditional with DataTable
    Public Sub DirectCastInConditionalWithDataTable(condition As Boolean)
        Dim formatter As New BinaryFormatter()
        If condition Then
            Using stream As New MemoryStream()
                Dim result = DirectCast(formatter.Deserialize(stream), DataTable) ' Violation
            End Using
        End If
    End Sub
    
    ' Violation: CType in Try-Catch with DataSet
    Public Sub CTypeInTryCatchWithDataSet()
        Try
            Dim formatter As New BinaryFormatter()
            Using stream As New MemoryStream()
                Dim result = CType(formatter.Deserialize(stream), DataSet) ' Violation
            End Using
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Custom DataSet type with CType
    Public Sub CTypeWithCustomDataSet()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = CType(formatter.Deserialize(stream), MyCustomDataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: Custom DataTable type with DirectCast
    Public Sub DirectCastWithCustomDataTable()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = DirectCast(formatter.Deserialize(stream), MyCustomDataTable) ' Violation
        End Using
    End Sub
    
    ' Non-violation: CType without DataSet/DataTable (should not be detected)
    Public Sub SafeCType()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = CType(formatter.Deserialize(stream), String) ' No violation - not DataSet/DataTable
        End Using
    End Sub
    
    ' Non-violation: DirectCast without DataSet/DataTable (should not be detected)
    Public Sub SafeDirectCast()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = DirectCast(formatter.Deserialize(stream), Object) ' No violation - not DataSet/DataTable
        End Using
    End Sub
    
    ' Non-violation: Serialize operations (should not be detected)
    Public Sub SafeSerialize()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim dataSet As New DataSet()
            formatter.Serialize(stream, dataSet) ' No violation - serialize operation
        End Using
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about CType(formatter.Deserialize(stream), DataSet)
        Dim message As String = "CType with Deserialize and DataSet can be vulnerable"
        Console.WriteLine("Avoid DirectCast(result, DataTable) after Deserialize")
    End Sub
    
    ' Violation: CType in Select Case with DataSet/DataTable
    Public Sub CTypeInSelectCaseWithDataSetDataTable(option As Integer)
        Dim formatter As New BinaryFormatter()
        Select Case option
            Case 1
                Using stream As New MemoryStream()
                    Dim result = CType(formatter.Deserialize(stream), DataSet) ' Violation
                End Using
            Case 2
                Using stream As New MemoryStream()
                    Dim result = DirectCast(formatter.Deserialize(stream), DataTable) ' Violation
                End Using
        End Select
    End Sub
    
    ' Violation: CType in While loop with DataTable
    Public Sub CTypeInWhileLoopWithDataTable()
        Dim formatter As New BinaryFormatter()
        Dim counter As Integer = 0
        While counter < 3
            Using stream As New MemoryStream()
                Dim result = CType(formatter.Deserialize(stream), DataTable) ' Violation
            End Using
            counter += 1
        End While
    End Sub
    
    ' Violation: Different formatter types with CType/DirectCast
    Public Sub DifferentFormatterTypesWithCasting()
        Dim soapFormatter As New System.Runtime.Serialization.Formatters.Soap.SoapFormatter()
        Using stream As New MemoryStream()
            Dim result1 = CType(soapFormatter.Deserialize(stream), DataSet) ' Violation
            Dim result2 = DirectCast(soapFormatter.Deserialize(stream), DataTable) ' Violation
        End Using
    End Sub

End Class

' Helper classes for testing
Public Class MyCustomDataSet
    Inherits DataSet
End Class

Public Class MyCustomDataTable
    Inherits DataTable
End Class
