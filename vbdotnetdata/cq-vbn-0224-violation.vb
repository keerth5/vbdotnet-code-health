' VB.NET test file for cq-vbn-0224: Use XmlReader for schema read
' This rule detects XmlSchema.Read usage without secure XmlReader

Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.Schema

Public Class XmlSchemaReadViolations
    
    ' Violation: XmlSchema.Read with Stream
    Public Sub ReadSchemaFromStream()
        Dim stream As New FileStream("schema.xsd", FileMode.Open)
        
        ' Violation: XmlSchema.Read with Stream parameter
        Dim schema As XmlSchema = XmlSchema.Read(stream, Nothing)
        
        stream.Close()
    End Sub
    
    ' Violation: XmlSchema.Read with String
    Public Sub ReadSchemaFromString()
        Dim schemaString As String = "<xs:schema>...</xs:schema>"
        
        ' Violation: XmlSchema.Read with String parameter
        Dim schema As XmlSchema = XmlSchema.Read(schemaString, Nothing)
    End Sub
    
    ' Violation: Multiple XmlSchema.Read with Stream
    Public Sub ReadMultipleSchemas()
        Dim stream1 As New FileStream("schema1.xsd", FileMode.Open)
        Dim stream2 As New MemoryStream()
        
        ' Violation: Multiple XmlSchema.Read with Stream
        Dim schema1 As XmlSchema = XmlSchema.Read(stream1, Nothing)
        Dim schema2 As XmlSchema = XmlSchema.Read(stream2, Nothing)
        
        stream1.Close()
        stream2.Close()
    End Sub
    
    ' Violation: XmlSchema.Read with TextReader
    Public Sub ReadSchemaFromTextReader()
        Dim textReader As New StringReader("<xs:schema>content</xs:schema>")
        
        ' Violation: XmlSchema.Read with TextReader
        Dim schema As XmlSchema = XmlSchema.Read(textReader, Nothing)
        
        textReader.Close()
    End Sub
    
    ' Violation: XmlSchema.Read in try-catch
    Public Sub ReadSchemaWithErrorHandling()
        Try
            Dim stream As New FileStream("config.xsd", FileMode.Open)
            
            ' Violation: XmlSchema.Read with Stream in try block
            Dim schema As XmlSchema = XmlSchema.Read(stream, AddressOf ValidationCallback)
            
            stream.Close()
        Catch ex As Exception
            Console.WriteLine("Error reading schema: " & ex.Message)
        End Try
    End Sub
    
    Private Sub ValidationCallback(sender As Object, args As ValidationEventArgs)
        Console.WriteLine("Validation: " & args.Message)
    End Sub
    
    ' Violation: XmlSchema.Read with variable
    Public Sub ReadSchemaWithVariable()
        Dim fileStream As Stream = New FileStream("variable.xsd", FileMode.Open)
        
        ' Violation: XmlSchema.Read with Stream variable
        Dim schema As XmlSchema = XmlSchema.Read(fileStream, Nothing)
        
        fileStream.Dispose()
    End Sub
    
    ' Violation: XmlSchema.Read with method parameter
    Public Sub ReadSchemaFromParameter(inputStream As Stream)
        ' Violation: XmlSchema.Read with Stream parameter
        Dim schema As XmlSchema = XmlSchema.Read(inputStream, Nothing)
    End Sub
    
    ' Violation: XmlSchema.Read in conditional
    Public Sub ConditionalSchemaRead(useStream As Boolean)
        If useStream Then
            Dim stream As New FileStream("conditional.xsd", FileMode.Open)
            
            ' Violation: XmlSchema.Read with Stream in conditional
            Dim schema As XmlSchema = XmlSchema.Read(stream, Nothing)
            
            stream.Close()
        End If
    End Sub
    
    ' Violation: XmlSchema.Read in loop
    Public Sub ReadSchemasInLoop()
        Dim files() As String = {"schema1.xsd", "schema2.xsd", "schema3.xsd"}
        
        For Each fileName As String In files
            Dim stream As New FileStream(fileName, FileMode.Open)
            
            ' Violation: XmlSchema.Read with Stream in loop
            Dim schema As XmlSchema = XmlSchema.Read(stream, Nothing)
            
            stream.Close()
        Next
    End Sub
    
    ' Violation: XmlSchema.Read with different stream types
    Public Sub ReadSchemasDifferentTypes()
        ' Violation: XmlSchema.Read with MemoryStream
        Dim memStream As New MemoryStream()
        Dim schema1 As XmlSchema = XmlSchema.Read(memStream, Nothing)
        
        ' Violation: XmlSchema.Read with FileStream
        Dim fileStream As New FileStream("test.xsd", FileMode.Open)
        Dim schema2 As XmlSchema = XmlSchema.Read(fileStream, Nothing)
        
        memStream.Close()
        fileStream.Close()
    End Sub
    
    ' Good example (should not be detected - uses XmlReader)
    Public Sub SecureSchemaRead()
        Dim stream As New FileStream("data.xsd", FileMode.Open)
        Dim xmlReader As XmlReader = XmlReader.Create(stream)
        
        ' Good: Using XmlReader for secure schema reading
        Dim schema As XmlSchema = XmlSchema.Read(xmlReader, Nothing)
        
        xmlReader.Close()
        stream.Close()
    End Sub
    
    ' Good example (should not be detected - not XmlSchema.Read)
    Public Sub RegularXmlReading()
        Dim stream As New FileStream("data.xml", FileMode.Open)
        
        ' Good: Using regular XmlReader for document reading
        Dim reader As XmlReader = XmlReader.Create(stream)
        
        reader.Close()
        stream.Close()
    End Sub
    
    ' Violation: XmlSchema.Read with return
    Public Function ReadAndReturnSchema() As XmlSchema
        Dim stream As New FileStream("return.xsd", FileMode.Open)
        
        ' Violation: XmlSchema.Read with Stream for return
        Return XmlSchema.Read(stream, Nothing)
    End Function
    
    ' Violation: XmlSchema.Read assignment
    Public Sub AssignSchema()
        Dim stream As New FileStream("assign.xsd", FileMode.Open)
        Dim schema As XmlSchema
        
        ' Violation: XmlSchema.Read assignment with Stream
        schema = XmlSchema.Read(stream, Nothing)
        
        stream.Close()
    End Sub
    
    ' Violation: XmlSchema.Read with string literal
    Public Sub ReadSchemaFromLiteral()
        ' Violation: XmlSchema.Read with string literal
        Dim schema As XmlSchema = XmlSchema.Read("<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'></xs:schema>", Nothing)
    End Sub
    
    ' Violation: XmlSchema.Read with complex expression
    Public Sub ReadSchemaFromExpression()
        Dim schemaData As String = GetSchemaData()
        
        ' Violation: XmlSchema.Read with string expression
        Dim schema As XmlSchema = XmlSchema.Read(schemaData, Nothing)
    End Sub
    
    Private Function GetSchemaData() As String
        Return "<xs:schema>sample schema</xs:schema>"
    End Function
    
    ' Violation: XmlSchema.Read in using statement
    Public Sub ReadSchemaInUsing()
        Dim stream As New FileStream("using.xsd", FileMode.Open)
        
        ' Violation: XmlSchema.Read with Stream
        Dim schema As XmlSchema = XmlSchema.Read(stream, Nothing)
        
        stream.Close()
    End Sub
    
    ' Violation: XmlSchema.Read with validation callback
    Public Sub ReadSchemaWithCallback()
        Dim stream As New FileStream("callback.xsd", FileMode.Open)
        
        ' Violation: XmlSchema.Read with Stream and callback
        Dim schema As XmlSchema = XmlSchema.Read(stream, AddressOf HandleValidation)
        
        stream.Close()
    End Sub
    
    Private Sub HandleValidation(sender As Object, e As ValidationEventArgs)
        Console.WriteLine("Schema validation: " & e.Message)
    End Sub
    
    ' Violation: XmlSchema.Read in property
    Public ReadOnly Property DefaultSchema() As XmlSchema
        Get
            Dim stream As New FileStream("default.xsd", FileMode.Open)
            
            ' Violation: XmlSchema.Read with Stream in property
            Return XmlSchema.Read(stream, Nothing)
        End Get
    End Property
End Class
