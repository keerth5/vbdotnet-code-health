' Test file for cq-vbn-0197: Ensure DataSet.ReadXml()'s input is trusted
' This rule detects DataSet.ReadXml calls which can be vulnerable to DoS and RCE attacks

Imports System
Imports System.Data
Imports System.IO
Imports System.Xml

Public Class DataSetReadXmlViolations
    
    ' Violation: DataSet.ReadXml with string parameter
    Public Sub DataSetReadXmlWithString()
        Dim dataSet As New DataSet()
        dataSet.ReadXml("data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml with Stream parameter
    Public Sub DataSetReadXmlWithStream()
        Dim dataSet As New DataSet()
        Using stream As New FileStream("data.xml", FileMode.Open)
            dataSet.ReadXml(stream) ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with TextReader parameter
    Public Sub DataSetReadXmlWithTextReader()
        Dim dataSet As New DataSet()
        Using reader As New StreamReader("data.xml")
            dataSet.ReadXml(reader) ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with XmlReader parameter
    Public Sub DataSetReadXmlWithXmlReader()
        Dim dataSet As New DataSet()
        Using reader As XmlReader = XmlReader.Create("data.xml")
            dataSet.ReadXml(reader) ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with XmlReadMode parameter
    Public Sub DataSetReadXmlWithMode()
        Dim dataSet As New DataSet()
        dataSet.ReadXml("data.xml", XmlReadMode.ReadSchema) ' Violation
    End Sub
    
    ' Violation: Multiple DataSet.ReadXml calls
    Public Sub MultipleDataSetReadXml()
        Dim dataSet1 As New DataSet()
        Dim dataSet2 As New DataSet()
        
        dataSet1.ReadXml("data1.xml") ' Violation
        dataSet2.ReadXml("data2.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml in loop
    Public Sub DataSetReadXmlInLoop()
        For i As Integer = 0 To 10
            Dim dataSet As New DataSet()
            dataSet.ReadXml("data" & i.ToString() & ".xml") ' Violation
        Next
    End Sub
    
    ' Violation: DataSet.ReadXml in conditional
    Public Sub ConditionalDataSetReadXml(condition As Boolean)
        Dim dataSet As New DataSet()
        If condition Then
            dataSet.ReadXml("conditional_data.xml") ' Violation
        End If
    End Sub
    
    ' Violation: DataSet.ReadXml in Try-Catch
    Public Sub DataSetReadXmlInTryCatch()
        Try
            Dim dataSet As New DataSet()
            dataSet.ReadXml("data.xml") ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: DataSet field ReadXml
    Private dataSet As New DataSet()
    
    Public Sub FieldDataSetReadXml()
        dataSet.ReadXml("field_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet parameter ReadXml
    Public Sub ParameterDataSetReadXml(ds As DataSet)
        ds.ReadXml("parameter_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml with variable assignment
    Public Sub DataSetReadXmlWithVariable()
        Dim dataSet As DataSet
        dataSet = New DataSet()
        dataSet.ReadXml("variable_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml in method return context
    Public Function ReadXmlAndReturnDataSet() As DataSet
        Dim dataSet As New DataSet()
        dataSet.ReadXml("return_data.xml") ' Violation
        Return dataSet
    End Function
    
    ' Non-violation: DataSet without ReadXml (should not be detected)
    Public Sub SafeDataSetOperations()
        Dim dataSet As New DataSet()
        dataSet.DataSetName = "SafeDataSet"
        Dim table As New DataTable("SafeTable")
        dataSet.Tables.Add(table)
    End Sub
    
    ' Non-violation: Other ReadXml methods (should not be detected for this rule)
    Public Sub OtherReadXmlMethods()
        Dim table As New DataTable()
        table.ReadXml("table.xml") ' This should be detected by cq-vbn-0196, not this rule
    End Sub
    
    ' Non-violation: Comments and strings mentioning ReadXml
    Public Sub CommentsAndStrings()
        ' This is a comment about dataSet.ReadXml
        Dim message As String = "DataSet.ReadXml can be dangerous with untrusted input"
        Console.WriteLine("Use dataSet.ReadXml carefully")
    End Sub
    
    ' Violation: DataSet.ReadXml in Select Case
    Public Sub DataSetReadXmlInSelectCase(option As Integer)
        Dim dataSet As New DataSet()
        Select Case option
            Case 1
                dataSet.ReadXml("option1_data.xml") ' Violation
            Case 2
                dataSet.ReadXml("option2_data.xml") ' Violation
            Case Else
                dataSet.ReadXml("default_data.xml") ' Violation
        End Select
    End Sub
    
    ' Violation: DataSet.ReadXml with Using statement
    Public Sub DataSetReadXmlWithUsing()
        Using dataSet As New DataSet()
            dataSet.ReadXml("using_data.xml") ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml in While loop
    Public Sub DataSetReadXmlInWhileLoop()
        Dim counter As Integer = 0
        While counter < 5
            Dim dataSet As New DataSet()
            dataSet.ReadXml("while_data_" & counter.ToString() & ".xml") ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: DataSet.ReadXml with MemoryStream
    Public Sub DataSetReadXmlWithMemoryStream()
        Dim dataSet As New DataSet()
        Dim xmlData As Byte() = System.Text.Encoding.UTF8.GetBytes("<root><item>data</item></root>")
        Using stream As New MemoryStream(xmlData)
            dataSet.ReadXml(stream) ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with StringReader
    Public Sub DataSetReadXmlWithStringReader()
        Dim dataSet As New DataSet()
        Dim xmlString As String = "<root><item>data</item></root>"
        Using reader As New StringReader(xmlString)
            dataSet.ReadXml(reader) ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with XmlTextReader
    Public Sub DataSetReadXmlWithXmlTextReader()
        Dim dataSet As New DataSet()
        Using reader As New XmlTextReader("data.xml")
            dataSet.ReadXml(reader) ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with FileStream and XmlReadMode
    Public Sub DataSetReadXmlWithFileStreamAndMode()
        Dim dataSet As New DataSet()
        Using stream As New FileStream("data.xml", FileMode.Open)
            dataSet.ReadXml(stream, XmlReadMode.InferSchema) ' Violation
        End Using
    End Sub

End Class
