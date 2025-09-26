' VB.NET test file for cq-vbn-0223: Use XmlReader for validating reader
' This rule detects XmlValidatingReader usage without secure XmlReader

Imports System
Imports System.IO
Imports System.Xml

Public Class XmlValidatingReaderViolations
    
    ' Violation: XmlValidatingReader with Stream
    Public Sub CreateValidatingReaderFromStream()
        Dim stream As New FileStream("schema.xml", FileMode.Open)
        
        ' Violation: XmlValidatingReader with Stream parameter
        Dim validatingReader As New XmlValidatingReader(stream)
        
        validatingReader.Close()
        stream.Close()
    End Sub
    
    ' Violation: XmlValidatingReader with String
    Public Sub CreateValidatingReaderFromString()
        Dim xmlString As String = "<root><element>value</element></root>"
        
        ' Violation: XmlValidatingReader with String parameter
        Dim validatingReader As New XmlValidatingReader(xmlString)
        
        validatingReader.Close()
    End Sub
    
    ' Violation: Multiple XmlValidatingReader instances with Stream
    Public Sub CreateMultipleValidatingReaders()
        Dim stream1 As New FileStream("file1.xml", FileMode.Open)
        Dim stream2 As New MemoryStream()
        
        ' Violation: Multiple XmlValidatingReader with Stream
        Dim reader1 As New XmlValidatingReader(stream1)
        Dim reader2 As New XmlValidatingReader(stream2)
        
        reader1.Close()
        reader2.Close()
        stream1.Close()
        stream2.Close()
    End Sub
    
    ' Violation: XmlValidatingReader with TextReader
    Public Sub CreateValidatingReaderFromTextReader()
        Dim textReader As New StringReader("<data>test</data>")
        
        ' Violation: XmlValidatingReader with TextReader
        Dim validatingReader As New XmlValidatingReader(textReader)
        
        validatingReader.Close()
        textReader.Close()
    End Sub
    
    ' Violation: XmlValidatingReader in try-catch
    Public Sub CreateValidatingReaderWithErrorHandling()
        Try
            Dim stream As New FileStream("config.xml", FileMode.Open)
            
            ' Violation: XmlValidatingReader with Stream in try block
            Dim reader As New XmlValidatingReader(stream)
            
            reader.Close()
            stream.Close()
        Catch ex As Exception
            Console.WriteLine("Error creating validating reader: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: XmlValidatingReader with variable
    Public Sub CreateValidatingReaderWithVariable()
        Dim fileStream As Stream = New FileStream("data.xml", FileMode.Open)
        
        ' Violation: XmlValidatingReader with Stream variable
        Dim validatingReader As New XmlValidatingReader(fileStream)
        
        validatingReader.Close()
        fileStream.Dispose()
    End Sub
    
    ' Violation: XmlValidatingReader with method parameter
    Public Sub CreateValidatingReaderFromParameter(inputStream As Stream)
        ' Violation: XmlValidatingReader with Stream parameter
        Dim reader As New XmlValidatingReader(inputStream)
        
        reader.Close()
    End Sub
    
    ' Violation: XmlValidatingReader in conditional
    Public Sub ConditionalValidatingReader(useStream As Boolean)
        If useStream Then
            Dim stream As New FileStream("conditional.xml", FileMode.Open)
            
            ' Violation: XmlValidatingReader with Stream in conditional
            Dim reader As New XmlValidatingReader(stream)
            
            reader.Close()
            stream.Close()
        End If
    End Sub
    
    ' Violation: XmlValidatingReader in loop
    Public Sub CreateValidatingReadersInLoop()
        Dim files() As String = {"file1.xml", "file2.xml", "file3.xml"}
        
        For Each fileName As String In files
            Dim stream As New FileStream(fileName, FileMode.Open)
            
            ' Violation: XmlValidatingReader with Stream in loop
            Dim reader As New XmlValidatingReader(stream)
            
            reader.Close()
            stream.Close()
        Next
    End Sub
    
    ' Violation: XmlValidatingReader with different stream types
    Public Sub CreateValidatingReadersDifferentTypes()
        ' Violation: XmlValidatingReader with MemoryStream
        Dim memStream As New MemoryStream()
        Dim reader1 As New XmlValidatingReader(memStream)
        
        ' Violation: XmlValidatingReader with FileStream
        Dim fileStream As New FileStream("test.xml", FileMode.Open)
        Dim reader2 As New XmlValidatingReader(fileStream)
        
        reader1.Close()
        reader2.Close()
        memStream.Close()
        fileStream.Close()
    End Sub
    
    ' Good example (should not be detected - uses XmlReader)
    Public Sub SecureValidatingReader()
        Dim stream As New FileStream("data.xml", FileMode.Open)
        Dim xmlReader As XmlReader = XmlReader.Create(stream)
        
        ' Good: Using XmlReader for secure validation
        Dim validatingReader As New XmlValidatingReader(xmlReader)
        
        validatingReader.Close()
        xmlReader.Close()
        stream.Close()
    End Sub
    
    ' Good example (should not be detected - not XmlValidatingReader)
    Public Sub RegularXmlReader()
        Dim stream As New FileStream("data.xml", FileMode.Open)
        
        ' Good: Using regular XmlReader
        Dim reader As XmlReader = XmlReader.Create(stream)
        
        reader.Close()
        stream.Close()
    End Sub
    
    ' Violation: XmlValidatingReader with return
    Public Function CreateAndReturnValidatingReader() As XmlValidatingReader
        Dim stream As New FileStream("return.xml", FileMode.Open)
        
        ' Violation: XmlValidatingReader with Stream for return
        Return New XmlValidatingReader(stream)
    End Function
    
    ' Violation: XmlValidatingReader assignment
    Public Sub AssignValidatingReader()
        Dim stream As New FileStream("assign.xml", FileMode.Open)
        Dim reader As XmlValidatingReader
        
        ' Violation: XmlValidatingReader assignment with Stream
        reader = New XmlValidatingReader(stream)
        
        reader.Close()
        stream.Close()
    End Sub
    
    ' Violation: XmlValidatingReader with string literal
    Public Sub CreateValidatingReaderFromLiteral()
        ' Violation: XmlValidatingReader with string literal
        Dim reader As New XmlValidatingReader("<root><data>value</data></root>")
        
        reader.Close()
    End Sub
    
    ' Violation: XmlValidatingReader with complex expression
    Public Sub CreateValidatingReaderFromExpression()
        Dim xmlData As String = GetXmlData()
        
        ' Violation: XmlValidatingReader with string expression
        Dim reader As New XmlValidatingReader(xmlData)
        
        reader.Close()
    End Sub
    
    Private Function GetXmlData() As String
        Return "<sample>data</sample>"
    End Function
    
    ' Violation: XmlValidatingReader in using statement
    Public Sub CreateValidatingReaderInUsing()
        Dim stream As New FileStream("using.xml", FileMode.Open)
        
        ' Violation: XmlValidatingReader with Stream
        Using reader As New XmlValidatingReader(stream)
            ' Process XML data
        End Using
        
        stream.Close()
    End Sub
End Class
