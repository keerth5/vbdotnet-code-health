' Test file for cq-vbn-0205: Do not add schema by URL
' This rule detects unsafe Add method calls with HTTP URLs

Imports System
Imports System.Xml
Imports System.Xml.Schema

Public Class SchemaAddByUrlViolations
    
    ' Violation: Add method with HTTP URL
    Public Sub AddSchemaWithHttpUrl()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("http://example.com/schema.xsd") ' Violation
    End Sub
    
    ' Violation: Add method with HTTPS URL
    Public Sub AddSchemaWithHttpsUrl()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("https://example.com/secure-schema.xsd") ' Violation
    End Sub
    
    ' Violation: XmlSchemaSet.Add with HTTP URL
    Public Sub XmlSchemaSetAddWithHttpUrl()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add(Nothing, "http://schemas.example.com/myschema.xsd") ' Violation
    End Sub
    
    ' Violation: XmlSchemaSet.Add with HTTPS URL and namespace
    Public Sub XmlSchemaSetAddWithHttpsUrlAndNamespace()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("http://example.com/namespace", "https://example.com/schema.xsd") ' Violation
    End Sub
    
    ' Violation: Multiple Add calls with HTTP URLs
    Public Sub MultipleAddCallsWithHttpUrls()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("http://example.com/schema1.xsd") ' Violation
        schemaSet.Add("http://example.com/schema2.xsd") ' Violation
    End Sub
    
    ' Violation: Add in loop with HTTP URLs
    Public Sub AddInLoopWithHttpUrls()
        Dim schemaSet As New XmlSchemaSet()
        For i As Integer = 1 To 5
            schemaSet.Add($"http://example.com/schema{i}.xsd") ' Violation
        Next
    End Sub
    
    ' Violation: Add in conditional with HTTP URL
    Public Sub ConditionalAddWithHttpUrl(useRemoteSchema As Boolean)
        Dim schemaSet As New XmlSchemaSet()
        If useRemoteSchema Then
            schemaSet.Add("http://remote.example.com/schema.xsd") ' Violation
        End If
    End Sub
    
    ' Violation: Add in Try-Catch with HTTP URL
    Public Sub AddInTryCatchWithHttpUrl()
        Try
            Dim schemaSet As New XmlSchemaSet()
            schemaSet.Add("http://example.com/schema.xsd") ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Add with HTTP URL in variable
    Public Sub AddWithHttpUrlInVariable()
        Dim schemaSet As New XmlSchemaSet()
        Dim schemaUrl As String = "http://example.com/dynamic-schema.xsd"
        schemaSet.Add(schemaUrl) ' Violation
    End Sub
    
    ' Violation: Add with HTTPS URL in field
    Private schemaUrl As String = "https://example.com/field-schema.xsd"
    
    Public Sub AddWithHttpsUrlInField()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add(schemaUrl) ' Violation
    End Sub
    
    ' Violation: Add with HTTP URL in method parameter
    Public Sub AddWithHttpUrlInParameter(url As String)
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("http://example.com/param-schema.xsd") ' Violation
    End Sub
    
    ' Violation: Add with HTTP URL in method return context
    Public Function AddAndReturnSchemaSet() As XmlSchemaSet
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("http://example.com/return-schema.xsd") ' Violation
        Return schemaSet
    End Function
    
    ' Non-violation: Add with local file path (should not be detected)
    Public Sub SafeAddWithLocalFile()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("C:\schemas\local-schema.xsd") ' No violation - local file
    End Sub
    
    ' Non-violation: Add with relative file path (should not be detected)
    Public Sub SafeAddWithRelativePath()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add(".\schemas\relative-schema.xsd") ' No violation - relative path
    End Sub
    
    ' Non-violation: Add with file:// URL (should not be detected)
    Public Sub SafeAddWithFileUrl()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("file:///C:/schemas/file-schema.xsd") ' No violation - file URL
    End Sub
    
    ' Non-violation: Comments and strings mentioning HTTP URLs
    Public Sub CommentsAndStrings()
        ' This is about schemaSet.Add("http://example.com/schema.xsd")
        Dim message As String = "Do not use Add with http://example.com URLs"
        Console.WriteLine("Avoid HTTP URLs in XmlSchemaSet.Add")
    End Sub
    
    ' Violation: Add with HTTP URL in Select Case
    Public Sub AddInSelectCaseWithHttpUrl(option As Integer)
        Dim schemaSet As New XmlSchemaSet()
        Select Case option
            Case 1
                schemaSet.Add("http://example.com/option1-schema.xsd") ' Violation
            Case 2
                schemaSet.Add("https://example.com/option2-schema.xsd") ' Violation
            Case Else
                schemaSet.Add("http://example.com/default-schema.xsd") ' Violation
        End Select
    End Sub
    
    ' Violation: Add with HTTP URL in Using statement
    Public Sub AddWithHttpUrlInUsing()
        Using schemaSet As New XmlSchemaSet()
            schemaSet.Add("http://example.com/using-schema.xsd") ' Violation
        End Using
    End Sub
    
    ' Violation: Add with HTTP URL in While loop
    Public Sub AddInWhileLoopWithHttpUrl()
        Dim schemaSet As New XmlSchemaSet()
        Dim counter As Integer = 0
        While counter < 3
            schemaSet.Add($"http://example.com/while-schema-{counter}.xsd") ' Violation
            counter += 1
        End While
    End Sub
    
    ' Violation: Add with HTTP URL and additional parameters
    Public Sub AddWithHttpUrlAndParameters()
        Dim schemaSet As New XmlSchemaSet()
        schemaSet.Add("http://example.com/namespace", "http://example.com/schema.xsd") ' Violation
    End Sub
    
    ' Violation: Add with concatenated HTTP URL
    Public Sub AddWithConcatenatedHttpUrl()
        Dim schemaSet As New XmlSchemaSet()
        Dim baseUrl As String = "http://example.com/"
        Dim schemaName As String = "concatenated-schema.xsd"
        schemaSet.Add(baseUrl + schemaName) ' Violation
    End Sub
    
    ' Violation: Add with interpolated HTTP URL
    Public Sub AddWithInterpolatedHttpUrl()
        Dim schemaSet As New XmlSchemaSet()
        Dim version As String = "v1"
        schemaSet.Add($"http://example.com/schemas/{version}/interpolated-schema.xsd") ' Violation
    End Sub
    
    ' Violation: Generic object Add method with HTTP URL
    Public Sub GenericAddWithHttpUrl()
        Dim obj As Object = New XmlSchemaSet()
        Dim schemaSet = CType(obj, XmlSchemaSet)
        schemaSet.Add("http://example.com/generic-schema.xsd") ' Violation
    End Sub

End Class
