' VB.NET test file for cq-vbn-0227: Do Not Use XslTransform
' This rule detects usage of obsolete XslTransform class

Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl

Public Class XslTransformViolations
    
    ' Violation: XslTransform instantiation
    Public Sub UseXslTransform()
        ' Violation: New XslTransform
        Dim transform As New XslTransform()
        
        ' Load and transform would follow...
    End Sub
    
    ' Violation: Multiple XslTransform instances
    Public Sub UseMultipleXslTransforms()
        ' Violation: Multiple New XslTransform
        Dim transform1 As New XslTransform()
        Dim transform2 As New XslTransform()
        
        ' Processing would follow...
    End Sub
    
    ' Violation: XslTransform with full namespace
    Public Sub UseXslTransformWithNamespace()
        ' Violation: System.Xml.Xsl.XslTransform reference
        Dim transform As System.Xml.Xsl.XslTransform = New System.Xml.Xsl.XslTransform()
    End Sub
    
    ' Violation: XslTransform in try-catch
    Public Sub UseXslTransformWithErrorHandling()
        Try
            ' Violation: New XslTransform in try block
            Dim transform As New XslTransform()
            
            ' Transform operations...
        Catch ex As Exception
            Console.WriteLine("Transform error: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: XslTransform with variable
    Public Sub UseXslTransformWithVariable()
        Dim xslProcessor As XslTransform
        
        ' Violation: XslTransform assignment
        xslProcessor = New XslTransform()
    End Sub
    
    ' Violation: XslTransform in conditional
    Public Sub ConditionalXslTransform(useTransform As Boolean)
        If useTransform Then
            ' Violation: New XslTransform in conditional
            Dim transform As New XslTransform()
            
            ' Transform logic...
        End If
    End Sub
    
    ' Violation: XslTransform in loop
    Public Sub UseXslTransformInLoop()
        Dim stylesheets() As String = {"style1.xsl", "style2.xsl", "style3.xsl"}
        
        For Each stylesheet As String In stylesheets
            ' Violation: New XslTransform in loop
            Dim transform As New XslTransform()
            
            ' Load stylesheet and transform...
        Next
    End Sub
    
    ' Violation: XslTransform with load and transform
    Public Sub LoadAndTransform()
        ' Violation: New XslTransform
        Dim transform As New XslTransform()
        
        ' Load stylesheet
        transform.Load("stylesheet.xsl")
        
        ' Transform document
        Dim input As New XmlDocument()
        input.Load("input.xml")
        
        Dim output As New StringWriter()
        transform.Transform(input, Nothing, output)
    End Sub
    
    ' Violation: XslTransform with XmlReader
    Public Sub TransformWithXmlReader()
        ' Violation: New XslTransform
        Dim transform As New XslTransform()
        
        Dim stylesheetReader As XmlReader = XmlReader.Create("stylesheet.xsl")
        transform.Load(stylesheetReader)
        
        Dim inputReader As XmlReader = XmlReader.Create("input.xml")
        Dim output As New StringWriter()
        
        transform.Transform(inputReader, Nothing, output)
        
        stylesheetReader.Close()
        inputReader.Close()
    End Sub
    
    ' Good example (should not be detected - uses XslCompiledTransform)
    Public Sub UseSecureTransform()
        ' Good: Using XslCompiledTransform instead of XslTransform
        Dim transform As New XslCompiledTransform()
        
        transform.Load("stylesheet.xsl")
        
        Dim input As New XmlDocument()
        input.Load("input.xml")
        
        Dim output As New StringWriter()
        transform.Transform(input, output)
    End Sub
    
    ' Good example (should not be detected - different class)
    Public Sub UseOtherXmlClasses()
        ' Good: Using other XML classes
        Dim document As New XmlDocument()
        document.Load("data.xml")
        
        Dim reader As XmlReader = XmlReader.Create("data.xml")
        reader.Close()
    End Sub
    
    ' Violation: XslTransform with return
    Public Function CreateXslTransform() As XslTransform
        ' Violation: New XslTransform for return
        Return New XslTransform()
    End Function
    
    ' Violation: XslTransform assignment in method
    Public Sub AssignXslTransform()
        Dim processor As XslTransform
        
        ' Violation: XslTransform assignment
        processor = New XslTransform()
        
        ' Use processor...
    End Sub
    
    ' Violation: XslTransform with arguments
    Public Sub TransformWithArguments()
        ' Violation: New XslTransform
        Dim transform As New XslTransform()
        
        transform.Load("stylesheet.xsl")
        
        Dim args As New XsltArgumentList()
        args.AddParam("param1", "", "value1")
        
        Dim input As New XmlDocument()
        input.Load("input.xml")
        
        Dim output As New StringWriter()
        transform.Transform(input, args, output)
    End Sub
    
    ' Violation: XslTransform in property
    Public ReadOnly Property TransformProcessor() As XslTransform
        Get
            ' Violation: New XslTransform in property
            Return New XslTransform()
        End Get
    End Property
    
    ' Violation: XslTransform with resolver
    Public Sub TransformWithResolver()
        ' Violation: New XslTransform
        Dim transform As New XslTransform()
        
        Dim resolver As New XmlUrlResolver()
        transform.Load("stylesheet.xsl", resolver)
        
        Dim input As New XmlDocument()
        input.Load("input.xml")
        
        Dim output As New StringWriter()
        transform.Transform(input, Nothing, output, resolver)
    End Sub
    
    ' Violation: XslTransform with evidence (if supported)
    Public Sub TransformWithEvidence()
        ' Violation: New XslTransform
        Dim transform As New XslTransform()
        
        ' Load and transform operations...
    End Sub
    
    ' Violation: XslTransform in using statement (if IDisposable)
    Public Sub TransformInUsing()
        ' Violation: New XslTransform
        Dim transform As New XslTransform()
        
        ' Transform operations...
    End Sub
    
    ' Violation: System.Xml.Xsl.XslTransform type reference
    Public Sub ReferenceXslTransformType()
        Dim transformType As Type = GetType(System.Xml.Xsl.XslTransform)
        
        ' Violation: System.Xml.Xsl.XslTransform type usage
        Console.WriteLine("Transform type: " & transformType.Name)
    End Sub
    
    ' Violation: XslTransform in field declaration
    Private xslTransformField As XslTransform = New XslTransform()
    
    ' Violation: XslTransform parameter
    Public Sub ProcessWithTransform(transform As XslTransform)
        ' Method using XslTransform parameter
        If transform IsNot Nothing Then
            ' Process with transform...
        End If
    End Sub
    
    ' Violation: XslTransform array
    Public Sub CreateTransformArray()
        ' Violation: Array of XslTransform with New XslTransform
        Dim transforms() As XslTransform = {New XslTransform(), New XslTransform()}
        
        For Each transform As XslTransform In transforms
            ' Process each transform...
        Next
    End Sub
End Class
