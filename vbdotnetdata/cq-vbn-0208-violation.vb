' Test file for cq-vbn-0208: Insecure Processing in API Design, XML Document and XML Text Reader
' This rule detects insecure XML processing with XmlUrlResolver

Imports System
Imports System.Xml

Public Class InsecureXmlProcessingViolations
    
    ' Violation: XmlDocument with XmlUrlResolver
    Public Sub XmlDocumentWithXmlUrlResolver()
        Dim doc As New XmlDocument()
        doc.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver
    Public Sub XmlTextReaderWithXmlUrlResolver()
        Dim reader As New XmlTextReader("data.xml")
        reader.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver using stream
    Public Sub XmlDocumentWithXmlUrlResolverUsingStream()
        Using stream As New System.IO.FileStream("data.xml", System.IO.FileMode.Open)
            Dim reader As New XmlTextReader(stream)
            reader.XmlResolver = New XmlUrlResolver() ' Violation
        End Using
    End Sub
    
    ' Violation: Multiple XmlDocument with XmlUrlResolver
    Public Sub MultipleXmlDocumentWithXmlUrlResolver()
        Dim doc1 As New XmlDocument()
        Dim doc2 As New XmlDocument()
        
        doc1.XmlResolver = New XmlUrlResolver() ' Violation
        doc2.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: Multiple XmlTextReader with XmlUrlResolver
    Public Sub MultipleXmlTextReaderWithXmlUrlResolver()
        Dim reader1 As New XmlTextReader("data1.xml")
        Dim reader2 As New XmlTextReader("data2.xml")
        
        reader1.XmlResolver = New XmlUrlResolver() ' Violation
        reader2.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver in loop
    Public Sub XmlDocumentWithXmlUrlResolverInLoop()
        For i As Integer = 0 To 5
            Dim doc As New XmlDocument()
            doc.XmlResolver = New XmlUrlResolver() ' Violation
        Next
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver in loop
    Public Sub XmlTextReaderWithXmlUrlResolverInLoop()
        For i As Integer = 0 To 3
            Dim reader As New XmlTextReader($"data{i}.xml")
            reader.XmlResolver = New XmlUrlResolver() ' Violation
        Next
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver in conditional
    Public Sub ConditionalXmlDocumentWithXmlUrlResolver(useResolver As Boolean)
        Dim doc As New XmlDocument()
        If useResolver Then
            doc.XmlResolver = New XmlUrlResolver() ' Violation
        End If
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver in conditional
    Public Sub ConditionalXmlTextReaderWithXmlUrlResolver(useResolver As Boolean)
        Dim reader As New XmlTextReader("data.xml")
        If useResolver Then
            reader.XmlResolver = New XmlUrlResolver() ' Violation
        End If
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver in Try-Catch
    Public Sub XmlDocumentWithXmlUrlResolverInTryCatch()
        Try
            Dim doc As New XmlDocument()
            doc.XmlResolver = New XmlUrlResolver() ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver in Try-Catch
    Public Sub XmlTextReaderWithXmlUrlResolverInTryCatch()
        Try
            Dim reader As New XmlTextReader("data.xml")
            reader.XmlResolver = New XmlUrlResolver() ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: XmlDocument field with XmlUrlResolver
    Private xmlDoc As New XmlDocument()
    
    Public Sub FieldXmlDocumentWithXmlUrlResolver()
        xmlDoc.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlTextReader field with XmlUrlResolver
    Private xmlReader As New XmlTextReader("field-data.xml")
    
    Public Sub FieldXmlTextReaderWithXmlUrlResolver()
        xmlReader.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlDocument parameter with XmlUrlResolver
    Public Sub ParameterXmlDocumentWithXmlUrlResolver(doc As XmlDocument)
        doc.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlTextReader parameter with XmlUrlResolver
    Public Sub ParameterXmlTextReaderWithXmlUrlResolver(reader As XmlTextReader)
        reader.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver in method return context
    Public Function CreateInsecureXmlDocument() As XmlDocument
        Dim doc As New XmlDocument()
        doc.XmlResolver = New XmlUrlResolver() ' Violation
        Return doc
    End Function
    
    ' Violation: XmlTextReader with XmlUrlResolver in method return context
    Public Function CreateInsecureXmlTextReader() As XmlTextReader
        Dim reader As New XmlTextReader("return-data.xml")
        reader.XmlResolver = New XmlUrlResolver() ' Violation
        Return reader
    End Function
    
    ' Non-violation: XmlDocument with null resolver (should not be detected)
    Public Sub SafeXmlDocumentWithNullResolver()
        Dim doc As New XmlDocument()
        doc.XmlResolver = Nothing ' No violation - safe setting
    End Sub
    
    ' Non-violation: XmlTextReader with null resolver (should not be detected)
    Public Sub SafeXmlTextReaderWithNullResolver()
        Dim reader As New XmlTextReader("data.xml")
        reader.XmlResolver = Nothing ' No violation - safe setting
    End Sub
    
    ' Non-violation: XmlDocument without resolver assignment (should not be detected)
    Public Sub SafeXmlDocumentWithoutResolver()
        Dim doc As New XmlDocument()
        doc.LoadXml("<root><item>data</item></root>") ' No violation - no resolver assignment
    End Sub
    
    ' Non-violation: XmlTextReader without resolver assignment (should not be detected)
    Public Sub SafeXmlTextReaderWithoutResolver()
        Dim reader As New XmlTextReader("data.xml")
        reader.Read() ' No violation - no resolver assignment
    End Sub
    
    ' Non-violation: Comments and strings mentioning XmlUrlResolver
    Public Sub CommentsAndStrings()
        ' This is about doc.XmlResolver = New XmlUrlResolver()
        Dim message As String = "XmlUrlResolver can be dangerous"
        Console.WriteLine("Avoid XmlUrlResolver for security")
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver in Select Case
    Public Sub XmlDocumentWithXmlUrlResolverInSelectCase(option As Integer)
        Dim doc As New XmlDocument()
        Select Case option
            Case 1
                doc.XmlResolver = New XmlUrlResolver() ' Violation
            Case 2
                doc.XmlResolver = New XmlUrlResolver() ' Violation
        End Select
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver in Select Case
    Public Sub XmlTextReaderWithXmlUrlResolverInSelectCase(option As Integer)
        Dim reader As New XmlTextReader("data.xml")
        Select Case option
            Case 1, 2, 3
                reader.XmlResolver = New XmlUrlResolver() ' Violation
        End Select
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver in Using statement
    Public Sub XmlDocumentWithXmlUrlResolverInUsing()
        Using doc As New XmlDocument()
            doc.XmlResolver = New XmlUrlResolver() ' Violation
        End Using
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver in Using statement
    Public Sub XmlTextReaderWithXmlUrlResolverInUsing()
        Using reader As New XmlTextReader("data.xml")
            reader.XmlResolver = New XmlUrlResolver() ' Violation
        End Using
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver in While loop
    Public Sub XmlDocumentWithXmlUrlResolverInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim doc As New XmlDocument()
            doc.XmlResolver = New XmlUrlResolver() ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver in While loop
    Public Sub XmlTextReaderWithXmlUrlResolverInWhileLoop()
        Dim counter As Integer = 0
        While counter < 3
            Dim reader As New XmlTextReader($"data{counter}.xml")
            reader.XmlResolver = New XmlUrlResolver() ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: XmlDocument with XmlUrlResolver and custom settings
    Public Sub XmlDocumentWithXmlUrlResolverAndCustomSettings()
        Dim doc As New XmlDocument()
        doc.PreserveWhitespace = True
        doc.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub
    
    ' Violation: XmlTextReader with XmlUrlResolver and custom settings
    Public Sub XmlTextReaderWithXmlUrlResolverAndCustomSettings()
        Dim reader As New XmlTextReader("data.xml")
        reader.WhitespaceHandling = WhitespaceHandling.Significant
        reader.XmlResolver = New XmlUrlResolver() ' Violation
    End Sub

End Class
