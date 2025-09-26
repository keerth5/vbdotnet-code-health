' VB.NET test file for cq-vbn-0225: Use XmlReader for XPathDocument
' This rule detects XPathDocument usage without secure XmlReader

Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath

Public Class XPathDocumentViolations
    
    ' Violation: XPathDocument with Stream
    Public Sub CreateXPathDocumentFromStream()
        Dim stream As New FileStream("document.xml", FileMode.Open)
        
        ' Violation: XPathDocument with Stream parameter
        Dim xpathDoc As New XPathDocument(stream)
        
        stream.Close()
    End Sub
    
    ' Violation: XPathDocument with String
    Public Sub CreateXPathDocumentFromString()
        Dim xmlString As String = "<root><element>value</element></root>"
        
        ' Violation: XPathDocument with String parameter
        Dim xpathDoc As New XPathDocument(xmlString)
    End Sub
    
    ' Violation: Multiple XPathDocument instances with Stream
    Public Sub CreateMultipleXPathDocuments()
        Dim stream1 As New FileStream("file1.xml", FileMode.Open)
        Dim stream2 As New MemoryStream()
        
        ' Violation: Multiple XPathDocument with Stream
        Dim doc1 As New XPathDocument(stream1)
        Dim doc2 As New XPathDocument(stream2)
        
        stream1.Close()
        stream2.Close()
    End Sub
    
    ' Violation: XPathDocument with TextReader
    Public Sub CreateXPathDocumentFromTextReader()
        Dim textReader As New StringReader("<data>test</data>")
        
        ' Violation: XPathDocument with TextReader
        Dim xpathDoc As New XPathDocument(textReader)
        
        textReader.Close()
    End Sub
    
    ' Violation: XPathDocument in try-catch
    Public Sub CreateXPathDocumentWithErrorHandling()
        Try
            Dim stream As New FileStream("config.xml", FileMode.Open)
            
            ' Violation: XPathDocument with Stream in try block
            Dim doc As New XPathDocument(stream)
            
            stream.Close()
        Catch ex As Exception
            Console.WriteLine("Error creating XPathDocument: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: XPathDocument with variable
    Public Sub CreateXPathDocumentWithVariable()
        Dim fileStream As Stream = New FileStream("data.xml", FileMode.Open)
        
        ' Violation: XPathDocument with Stream variable
        Dim xpathDoc As New XPathDocument(fileStream)
        
        fileStream.Dispose()
    End Sub
    
    ' Violation: XPathDocument with method parameter
    Public Sub CreateXPathDocumentFromParameter(inputStream As Stream)
        ' Violation: XPathDocument with Stream parameter
        Dim doc As New XPathDocument(inputStream)
    End Sub
    
    ' Violation: XPathDocument in conditional
    Public Sub ConditionalXPathDocument(useStream As Boolean)
        If useStream Then
            Dim stream As New FileStream("conditional.xml", FileMode.Open)
            
            ' Violation: XPathDocument with Stream in conditional
            Dim doc As New XPathDocument(stream)
            
            stream.Close()
        End If
    End Sub
    
    ' Violation: XPathDocument in loop
    Public Sub CreateXPathDocumentsInLoop()
        Dim files() As String = {"file1.xml", "file2.xml", "file3.xml"}
        
        For Each fileName As String In files
            Dim stream As New FileStream(fileName, FileMode.Open)
            
            ' Violation: XPathDocument with Stream in loop
            Dim doc As New XPathDocument(stream)
            
            stream.Close()
        Next
    End Sub
    
    ' Violation: XPathDocument with different stream types
    Public Sub CreateXPathDocumentsDifferentTypes()
        ' Violation: XPathDocument with MemoryStream
        Dim memStream As New MemoryStream()
        Dim doc1 As New XPathDocument(memStream)
        
        ' Violation: XPathDocument with FileStream
        Dim fileStream As New FileStream("test.xml", FileMode.Open)
        Dim doc2 As New XPathDocument(fileStream)
        
        memStream.Close()
        fileStream.Close()
    End Sub
    
    ' Good example (should not be detected - uses XmlReader)
    Public Sub SecureXPathDocument()
        Dim stream As New FileStream("data.xml", FileMode.Open)
        Dim xmlReader As XmlReader = XmlReader.Create(stream)
        
        ' Good: Using XmlReader for secure XPathDocument creation
        Dim xpathDoc As New XPathDocument(xmlReader)
        
        xmlReader.Close()
        stream.Close()
    End Sub
    
    ' Good example (should not be detected - not XPathDocument)
    Public Sub RegularXmlDocument()
        Dim stream As New FileStream("data.xml", FileMode.Open)
        
        ' Good: Using XmlDocument instead of XPathDocument
        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(stream)
        
        stream.Close()
    End Sub
    
    ' Violation: XPathDocument with return
    Public Function CreateAndReturnXPathDocument() As XPathDocument
        Dim stream As New FileStream("return.xml", FileMode.Open)
        
        ' Violation: XPathDocument with Stream for return
        Return New XPathDocument(stream)
    End Function
    
    ' Violation: XPathDocument assignment
    Public Sub AssignXPathDocument()
        Dim stream As New FileStream("assign.xml", FileMode.Open)
        Dim doc As XPathDocument
        
        ' Violation: XPathDocument assignment with Stream
        doc = New XPathDocument(stream)
        
        stream.Close()
    End Sub
    
    ' Violation: XPathDocument with string literal
    Public Sub CreateXPathDocumentFromLiteral()
        ' Violation: XPathDocument with string literal
        Dim doc As New XPathDocument("<root><data>value</data></root>")
    End Sub
    
    ' Violation: XPathDocument with complex expression
    Public Sub CreateXPathDocumentFromExpression()
        Dim xmlData As String = GetXmlData()
        
        ' Violation: XPathDocument with string expression
        Dim doc As New XPathDocument(xmlData)
    End Sub
    
    Private Function GetXmlData() As String
        Return "<sample>data</sample>"
    End Function
    
    ' Violation: XPathDocument in using statement
    Public Sub CreateXPathDocumentInUsing()
        Dim stream As New FileStream("using.xml", FileMode.Open)
        
        ' Violation: XPathDocument with Stream
        Dim doc As New XPathDocument(stream)
        
        stream.Close()
    End Sub
    
    ' Violation: XPathDocument with file path
    Public Sub CreateXPathDocumentFromFile()
        ' Violation: XPathDocument with file path string
        Dim doc As New XPathDocument("C:\data\sample.xml")
    End Sub
    
    ' Violation: XPathDocument in property
    Public ReadOnly Property DocumentFromFile() As XPathDocument
        Get
            Dim stream As New FileStream("property.xml", FileMode.Open)
            
            ' Violation: XPathDocument with Stream in property
            Return New XPathDocument(stream)
        End Get
    End Property
    
    ' Violation: XPathDocument with navigation
    Public Sub NavigateXPathDocument()
        Dim stream As New FileStream("navigate.xml", FileMode.Open)
        
        ' Violation: XPathDocument with Stream
        Dim doc As New XPathDocument(stream)
        Dim navigator As XPathNavigator = doc.CreateNavigator()
        
        stream.Close()
    End Sub
    
    ' Violation: XPathDocument with whitespace handling
    Public Sub CreateXPathDocumentWithWhitespace()
        Dim stream As New FileStream("whitespace.xml", FileMode.Open)
        
        ' Violation: XPathDocument with Stream and whitespace option
        Dim doc As New XPathDocument(stream, XmlSpace.Preserve)
        
        stream.Close()
    End Sub
End Class
