' Test file for cq-vbn-0200: Unsafe DataSet or DataTable in deserialized object graph can be vulnerable to remote code execution attack
' This rule detects IFormatter.Deserialize calls followed by casting to types containing DataSet or DataTable

Imports System
Imports System.Data
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class UnsafeDataSetDataTableDeserializationViolations
    
    ' Violation: IFormatter.Deserialize with CType to DataSet
    Public Sub FormatterDeserializeWithCTypeToDataSet()
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Dim dataSet = CType(result, DataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: IFormatter.Deserialize with CType to DataTable
    Public Sub FormatterDeserializeWithCTypeToDataTable()
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Dim dataTable = CType(result, DataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: BinaryFormatter.Deserialize with CType to DataSet
    Public Sub BinaryFormatterDeserializeWithCTypeToDataSet()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Dim dataSet = CType(result, DataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: BinaryFormatter.UnsafeDeserialize with CType to DataTable
    Public Sub BinaryFormatterUnsafeDeserializeWithCTypeToDataTable()
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.UnsafeDeserialize(stream, Nothing)
            Dim dataTable = CType(result, DataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: IFormatter.Deserialize with CType to custom DataSet type
    Public Sub FormatterDeserializeWithCTypeToCustomDataSet()
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Dim customDataSet = CType(result, MyCustomDataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: IFormatter.Deserialize with CType to custom DataTable type
    Public Sub FormatterDeserializeWithCTypeToCustomDataTable()
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Dim customDataTable = CType(result, MyCustomDataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: Multiple IFormatter.Deserialize with DataSet/DataTable casting
    Public Sub MultipleFormatterDeserializeWithCasting()
        Dim formatter As IFormatter = New BinaryFormatter()
        
        Using stream1 As New MemoryStream()
            Dim result1 = formatter.Deserialize(stream1)
            Dim dataSet = CType(result1, DataSet) ' Violation
        End Using
        
        Using stream2 As New MemoryStream()
            Dim result2 = formatter.Deserialize(stream2)
            Dim dataTable = CType(result2, DataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: IFormatter.Deserialize in loop with DataSet casting
    Public Sub FormatterDeserializeInLoopWithDataSetCasting()
        Dim formatter As IFormatter = New BinaryFormatter()
        For i As Integer = 0 To 5
            Using stream As New MemoryStream()
                Dim result = formatter.Deserialize(stream)
                Dim dataSet = CType(result, DataSet) ' Violation
            End Using
        Next
    End Sub
    
    ' Violation: IFormatter.Deserialize in conditional with DataTable casting
    Public Sub ConditionalFormatterDeserializeWithDataTableCasting(condition As Boolean)
        Dim formatter As IFormatter = New BinaryFormatter()
        If condition Then
            Using stream As New MemoryStream()
                Dim result = formatter.Deserialize(stream)
                Dim dataTable = CType(result, DataTable) ' Violation
            End Using
        End If
    End Sub
    
    ' Violation: IFormatter.Deserialize in Try-Catch with DataSet casting
    Public Sub FormatterDeserializeInTryCatchWithDataSetCasting()
        Try
            Dim formatter As IFormatter = New BinaryFormatter()
            Using stream As New MemoryStream()
                Dim result = formatter.Deserialize(stream)
                Dim dataSet = CType(result, DataSet) ' Violation
            End Using
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: IFormatter field deserialize with DataTable casting
    Private formatter As IFormatter = New BinaryFormatter()
    
    Public Sub FieldFormatterDeserializeWithDataTableCasting()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Dim dataTable = CType(result, DataTable) ' Violation
        End Using
    End Sub
    
    ' Violation: IFormatter parameter deserialize with DataSet casting
    Public Sub ParameterFormatterDeserializeWithDataSetCasting(fmt As IFormatter)
        Using stream As New MemoryStream()
            Dim result = fmt.Deserialize(stream)
            Dim dataSet = CType(result, DataSet) ' Violation
        End Using
    End Sub
    
    ' Violation: IFormatter.Deserialize with method return and DataTable casting
    Public Function FormatterDeserializeAndReturnDataTable() As DataTable
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Return CType(result, DataTable) ' Violation
        End Using
    End Function
    
    ' Violation: IFormatter with DataSet variable in object graph context
    Public Sub FormatterWithDataSetInObjectGraph()
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            ' Some operations that might involve DataSet in object graph
            If TypeOf result Is DataSet Then ' Violation context
                Dim dataSet = CType(result, DataSet) ' Violation
            End If
        End Using
    End Sub
    
    ' Non-violation: IFormatter.Deserialize without DataSet/DataTable casting (should not be detected)
    Public Sub SafeFormatterDeserialize()
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream)
            Dim safeObject = CType(result, String) ' No violation - not DataSet/DataTable
        End Using
    End Sub
    
    ' Non-violation: Serialize operations (should not be detected)
    Public Sub SafeFormatterSerialize()
        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As New MemoryStream()
            Dim dataSet As New DataSet()
            formatter.Serialize(stream, dataSet) ' No violation - serialize operation
        End Using
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about formatter.Deserialize and CType to DataSet
        Dim message As String = "IFormatter.Deserialize with DataSet casting can be vulnerable"
        Console.WriteLine("Avoid CType(result, DataTable) after Deserialize")
    End Sub
    
    ' Violation: IFormatter.Deserialize in Select Case with DataSet/DataTable casting
    Public Sub FormatterDeserializeInSelectCaseWithCasting(option As Integer)
        Dim formatter As IFormatter = New BinaryFormatter()
        Select Case option
            Case 1
                Using stream As New MemoryStream()
                    Dim result = formatter.Deserialize(stream)
                    Dim dataSet = CType(result, DataSet) ' Violation
                End Using
            Case 2
                Using stream As New MemoryStream()
                    Dim result = formatter.Deserialize(stream)
                    Dim dataTable = CType(result, DataTable) ' Violation
                End Using
        End Select
    End Sub
    
    ' Violation: IFormatter.Deserialize in While loop with DataTable casting
    Public Sub FormatterDeserializeInWhileLoopWithDataTableCasting()
        Dim formatter As IFormatter = New BinaryFormatter()
        Dim counter As Integer = 0
        While counter < 3
            Using stream As New MemoryStream()
                Dim result = formatter.Deserialize(stream)
                Dim dataTable = CType(result, DataTable) ' Violation
            End Using
            counter += 1
        End While
    End Sub

End Class

' Helper classes for testing
Public Class MyCustomDataSet
    Inherits DataSet
End Class

Public Class MyCustomDataTable
    Inherits DataTable
End Class
