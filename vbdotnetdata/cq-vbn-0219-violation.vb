' Test file for cq-vbn-0219: Use XmlReader For DataSet Read XML
' This rule detects unsafe DataSet.ReadXml usage without XmlReader

Imports System
Imports System.Data
Imports System.IO
Imports System.Xml

Public Class UnsafeDataSetReadXmlViolations
    
    ' Violation: DataSet.ReadXml with string parameter
    Public Sub ReadXmlWithString()
        Dim dataSet As New DataSet()
        dataSet.ReadXml("data.xml") ' Violation - using string parameter
    End Sub
    
    ' Violation: DataSet.ReadXml with string and XmlReadMode
    Public Sub ReadXmlWithStringAndMode()
        Dim dataSet As New DataSet()
        dataSet.ReadXml("data.xml", XmlReadMode.ReadSchema) ' Violation - using string parameter
    End Sub
    
    ' Violation: DataSet.ReadXml with Stream parameter
    Public Sub ReadXmlWithStream()
        Dim dataSet As New DataSet()
        Using stream As New FileStream("data.xml", FileMode.Open)
            dataSet.ReadXml(stream) ' Violation - using Stream parameter without XmlReader
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with Stream and XmlReadMode
    Public Sub ReadXmlWithStreamAndMode()
        Dim dataSet As New DataSet()
        Using stream As New FileStream("data.xml", FileMode.Open)
            dataSet.ReadXml(stream, XmlReadMode.InferSchema) ' Violation - using Stream parameter without XmlReader
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with TextReader parameter
    Public Sub ReadXmlWithTextReader()
        Dim dataSet As New DataSet()
        Using reader As New StreamReader("data.xml")
            dataSet.ReadXml(reader) ' Violation - using TextReader parameter without XmlReader
        End Using
    End Sub
    
    ' Violation: Multiple DataSet.ReadXml with unsafe parameters
    Public Sub MultipleUnsafeReadXml()
        Dim dataSet1 As New DataSet()
        Dim dataSet2 As New DataSet()
        
        dataSet1.ReadXml("data1.xml") ' Violation
        dataSet2.ReadXml("data2.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml in loop
    Public Sub ReadXmlInLoop()
        For i As Integer = 0 To 5
            Dim dataSet As New DataSet()
            dataSet.ReadXml($"data{i}.xml") ' Violation
        Next
    End Sub
    
    ' Violation: DataSet.ReadXml in conditional
    Public Sub ConditionalReadXml(useXml As Boolean)
        Dim dataSet As New DataSet()
        If useXml Then
            dataSet.ReadXml("conditional_data.xml") ' Violation
        End If
    End Sub
    
    ' Violation: DataSet.ReadXml in Try-Catch
    Public Sub ReadXmlInTryCatch()
        Try
            Dim dataSet As New DataSet()
            dataSet.ReadXml("data.xml") ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: DataSet.ReadXml with field DataSet
    Private dataSetField As New DataSet()
    
    Public Sub FieldDataSetReadXml()
        dataSetField.ReadXml("field_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml with parameter DataSet
    Public Sub ParameterDataSetReadXml(ds As DataSet)
        ds.ReadXml("parameter_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml in method return context
    Public Function ReadXmlAndReturnDataSet() As DataSet
        Dim dataSet As New DataSet()
        dataSet.ReadXml("return_data.xml") ' Violation
        Return dataSet
    End Function
    
    ' Non-violation: DataSet.ReadXml with XmlReader (should not be detected)
    Public Sub SafeReadXmlWithXmlReader()
        Dim dataSet As New DataSet()
        Using reader As XmlReader = XmlReader.Create("data.xml")
            dataSet.ReadXml(reader) ' No violation - using XmlReader
        End Using
    End Sub
    
    ' Non-violation: DataSet.ReadXml with XmlReader and XmlReadMode (should not be detected)
    Public Sub SafeReadXmlWithXmlReaderAndMode()
        Dim dataSet As New DataSet()
        Using reader As XmlReader = XmlReader.Create("data.xml")
            dataSet.ReadXml(reader, XmlReadMode.ReadSchema) ' No violation - using XmlReader
        End Using
    End Sub
    
    ' Non-violation: DataSet operations without ReadXml (should not be detected)
    Public Sub SafeDataSetOperations()
        Dim dataSet As New DataSet()
        dataSet.DataSetName = "SafeDataSet"
        Dim table As New DataTable("SafeTable")
        dataSet.Tables.Add(table)
    End Sub
    
    ' Non-violation: Comments and strings mentioning ReadXml
    Public Sub CommentsAndStrings()
        ' This is about dataSet.ReadXml with string parameters
        Dim message As String = "DataSet.ReadXml should use XmlReader for security"
        Console.WriteLine("Use XmlReader with DataSet.ReadXml for safer XML processing")
    End Sub
    
    ' Violation: DataSet.ReadXml in Select Case
    Public Sub ReadXmlInSelectCase(option As Integer)
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
    
    ' Violation: DataSet.ReadXml in Using statement
    Public Sub ReadXmlInUsing()
        Using dataSet As New DataSet()
            dataSet.ReadXml("using_data.xml") ' Violation
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml in While loop
    Public Sub ReadXmlInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim dataSet As New DataSet()
            dataSet.ReadXml($"while_data_{counter}.xml") ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: DataSet.ReadXml with variable file path
    Public Sub ReadXmlWithVariableFilePath()
        Dim dataSet As New DataSet()
        Dim filePath As String = "variable_data.xml"
        dataSet.ReadXml(filePath) ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml with MemoryStream
    Public Sub ReadXmlWithMemoryStream()
        Dim dataSet As New DataSet()
        Dim xmlData As Byte() = System.Text.Encoding.UTF8.GetBytes("<root><item>data</item></root>")
        Using stream As New MemoryStream(xmlData)
            dataSet.ReadXml(stream) ' Violation - using Stream without XmlReader
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml with StringReader
    Public Sub ReadXmlWithStringReader()
        Dim dataSet As New DataSet()
        Dim xmlString As String = "<root><item>data</item></root>"
        Using reader As New StringReader(xmlString)
            dataSet.ReadXml(reader) ' Violation - using TextReader without XmlReader
        End Using
    End Sub
    
    ' Violation: DataSet.ReadXml in lambda expression
    Public Sub ReadXmlInLambda()
        Dim action = Sub()
                         Dim dataSet As New DataSet()
                         dataSet.ReadXml("lambda_data.xml") ' Violation
                     End Sub
        action()
    End Sub
    
    ' Violation: DataSet.ReadXml in property
    Public WriteOnly Property XmlFilePath As String
        Set(value As String)
            Dim dataSet As New DataSet()
            dataSet.ReadXml(value) ' Violation
        End Set
    End Property
    
    ' Violation: DataSet.ReadXml in constructor
    Public Sub New(xmlFilePath As String)
        Dim dataSet As New DataSet()
        dataSet.ReadXml(xmlFilePath) ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml with method call result
    Public Sub ReadXmlWithMethodCallResult()
        Dim dataSet As New DataSet()
        dataSet.ReadXml(GetXmlFilePath()) ' Violation
    End Sub
    
    Private Function GetXmlFilePath() As String
        Return "method_result_data.xml"
    End Function
    
    ' Violation: DataSet.ReadXml in event handler
    Public Sub OnDataEvent(sender As Object, e As EventArgs)
        Dim dataSet As New DataSet()
        dataSet.ReadXml("event_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml in delegate
    Public Sub ReadXmlWithDelegate()
        Dim del As Action = Sub()
                                Dim dataSet As New DataSet()
                                dataSet.ReadXml("delegate_data.xml") ' Violation
                            End Sub
        del()
    End Sub
    
    ' Violation: DataSet.ReadXml with different DataSet types
    Public Sub ReadXmlWithDifferentDataSetTypes()
        Dim typedDataSet As New CustomDataSet()
        typedDataSet.ReadXml("typed_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml with network path
    Public Sub ReadXmlWithNetworkPath()
        Dim dataSet As New DataSet()
        dataSet.ReadXml("\\server\share\network_data.xml") ' Violation
    End Sub
    
    ' Violation: DataSet.ReadXml with HTTP URL
    Public Sub ReadXmlWithHttpUrl()
        Dim dataSet As New DataSet()
        dataSet.ReadXml("http://example.com/data.xml") ' Violation
    End Sub

End Class

' Helper class for testing
Public Class CustomDataSet
    Inherits DataSet
End Class
