' Test file for cq-vbn-0196: Ensure DataTable.ReadXml()'s input is trusted
' This rule detects DataTable.ReadXml calls which can be vulnerable to DoS and RCE attacks

Imports System
Imports System.Data
Imports System.IO
Imports System.Xml

Public Class DataTableReadXmlViolations
    
    ' Violation: DataTable.ReadXml with string parameter
    Public Sub DataTableReadXmlWithString()
        Dim table As New DataTable()
        table.ReadXml("data.xml") ' Violation
    End Sub
    
    ' Violation: DataTable.ReadXml with Stream parameter
    Public Sub DataTableReadXmlWithStream()
        Dim table As New DataTable()
        Using stream As New FileStream("data.xml", FileMode.Open)
            table.ReadXml(stream) ' Violation
        End Using
    End Sub
    
    ' Violation: DataTable.ReadXml with TextReader parameter
    Public Sub DataTableReadXmlWithTextReader()
        Dim table As New DataTable()
        Using reader As New StreamReader("data.xml")
            table.ReadXml(reader) ' Violation
        End Using
    End Sub
    
    ' Violation: DataTable.ReadXml with XmlReader parameter
    Public Sub DataTableReadXmlWithXmlReader()
        Dim table As New DataTable()
        Using reader As XmlReader = XmlReader.Create("data.xml")
            table.ReadXml(reader) ' Violation
        End Using
    End Sub
    
    ' Violation: DataTable.ReadXml with XmlReadMode parameter
    Public Sub DataTableReadXmlWithMode()
        Dim table As New DataTable()
        table.ReadXml("data.xml", XmlReadMode.ReadSchema) ' Violation
    End Sub
    
    ' Violation: Multiple DataTable.ReadXml calls
    Public Sub MultipleDataTableReadXml()
        Dim table1 As New DataTable()
        Dim table2 As New DataTable()
        
        table1.ReadXml("data1.xml") ' Violation
        table2.ReadXml("data2.xml") ' Violation
    End Sub
    
    ' Violation: DataTable.ReadXml in loop
    Public Sub DataTableReadXmlInLoop()
        For i As Integer = 0 To 10
            Dim table As New DataTable()
            table.ReadXml("data" & i.ToString() & ".xml") ' Violation
        Next
    End Sub
    
    ' Violation: DataTable.ReadXml in conditional
    Public Sub ConditionalDataTableReadXml(condition As Boolean)
        Dim table As New DataTable()
        If condition Then
            table.ReadXml("conditional_data.xml") ' Violation
        End If
    End Sub
    
    ' Violation: DataTable.ReadXml in Try-Catch
    Public Sub DataTableReadXmlInTryCatch()
        Try
            Dim table As New DataTable()
            table.ReadXml("data.xml") ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: DataTable field ReadXml
    Private dataTable As New DataTable()
    
    Public Sub FieldDataTableReadXml()
        dataTable.ReadXml("field_data.xml") ' Violation
    End Sub
    
    ' Violation: DataTable parameter ReadXml
    Public Sub ParameterDataTableReadXml(table As DataTable)
        table.ReadXml("parameter_data.xml") ' Violation
    End Sub
    
    ' Violation: DataTable.ReadXml with variable assignment
    Public Sub DataTableReadXmlWithVariable()
        Dim table As DataTable
        table = New DataTable()
        table.ReadXml("variable_data.xml") ' Violation
    End Sub
    
    ' Violation: DataTable.ReadXml in method return context
    Public Function ReadXmlAndReturnTable() As DataTable
        Dim table As New DataTable()
        table.ReadXml("return_data.xml") ' Violation
        Return table
    End Function
    
    ' Non-violation: DataTable without ReadXml (should not be detected)
    Public Sub SafeDataTableOperations()
        Dim table As New DataTable()
        table.TableName = "SafeTable"
        table.Columns.Add("Column1", GetType(String))
        Dim row = table.NewRow()
        table.Rows.Add(row)
    End Sub
    
    ' Non-violation: Other ReadXml methods (should not be detected for this rule)
    Public Sub OtherReadXmlMethods()
        Dim dataSet As New DataSet()
        dataSet.ReadXml("dataset.xml") ' This should be detected by cq-vbn-0197, not this rule
    End Sub
    
    ' Non-violation: Comments and strings mentioning ReadXml
    Public Sub CommentsAndStrings()
        ' This is a comment about table.ReadXml
        Dim message As String = "DataTable.ReadXml can be dangerous with untrusted input"
        Console.WriteLine("Use table.ReadXml carefully")
    End Sub
    
    ' Violation: DataTable.ReadXml in Select Case
    Public Sub DataTableReadXmlInSelectCase(option As Integer)
        Dim table As New DataTable()
        Select Case option
            Case 1
                table.ReadXml("option1_data.xml") ' Violation
            Case 2
                table.ReadXml("option2_data.xml") ' Violation
            Case Else
                table.ReadXml("default_data.xml") ' Violation
        End Select
    End Sub
    
    ' Violation: DataTable.ReadXml with Using statement
    Public Sub DataTableReadXmlWithUsing()
        Using table As New DataTable()
            table.ReadXml("using_data.xml") ' Violation
        End Using
    End Sub
    
    ' Violation: DataTable.ReadXml in While loop
    Public Sub DataTableReadXmlInWhileLoop()
        Dim counter As Integer = 0
        While counter < 5
            Dim table As New DataTable()
            table.ReadXml("while_data_" & counter.ToString() & ".xml") ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: DataTable.ReadXml with MemoryStream
    Public Sub DataTableReadXmlWithMemoryStream()
        Dim table As New DataTable()
        Dim xmlData As Byte() = System.Text.Encoding.UTF8.GetBytes("<root><item>data</item></root>")
        Using stream As New MemoryStream(xmlData)
            table.ReadXml(stream) ' Violation
        End Using
    End Sub
    
    ' Violation: DataTable.ReadXml with StringReader
    Public Sub DataTableReadXmlWithStringReader()
        Dim table As New DataTable()
        Dim xmlString As String = "<root><item>data</item></root>"
        Using reader As New StringReader(xmlString)
            table.ReadXml(reader) ' Violation
        End Using
    End Sub

End Class
