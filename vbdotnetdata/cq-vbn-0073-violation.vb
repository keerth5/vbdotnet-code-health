' Test file for cq-vbn-0073: Invalid entry in code metrics configuration file
' Rule should detect CodeMetrics configuration entries

Imports System

' Violation 1: CodeMetrics configuration block
<CodeMetrics>
    <Rule Id="CC001" Threshold="10" />
    <Rule Id="MI002" Threshold="20" />
</CodeMetrics>

Public Class CodeMetricsExample
    
    Public Sub ExampleMethod()
        Console.WriteLine("This is a regular method")
    End Sub
    
End Class

' Violation 2: Another CodeMetrics block with different content
<CodeMetrics>
    <Maintainability Index="85" />
    <Complexity Threshold="15" />
    <LinesOfCode Max="500" />
</CodeMetrics>

Public Module CodeMetricsModule
    
    Public Sub ModuleMethod()
        Console.WriteLine("Module method")
    End Sub
    
End Module

' This should NOT be detected - regular XML documentation
''' <summary>
''' This is a regular XML documentation comment
''' </summary>
Public Class RegularClass
    
    ''' <param name="value">The input value</param>
    ''' <returns>Processed value</returns>
    Public Function ProcessValue(value As String) As String
        Return value.ToUpper()
    End Function
    
End Class

' Violation 3: CodeMetrics with single rule
<CodeMetrics>
    <CyclomaticComplexity>12</CyclomaticComplexity>
</CodeMetrics>

Public Structure MetricsStructure
    
    Public Value As Integer
    
    Public Sub Initialize()
        Value = 0
    End Sub
    
End Structure

' This should NOT be detected - regular attribute
<Serializable>
Public Class SerializableClass
    
    Public Property Data As String
    
End Class

' Violation 4: CodeMetrics with nested elements
<CodeMetrics>
    <Rules>
        <Rule Name="Complexity" Value="8" />
        <Rule Name="Maintainability" Value="75" />
    </Rules>
</CodeMetrics>

Public Interface IMetricsInterface
    
    Sub ProcessMetrics()
    
End Interface

' This should NOT be detected - regular code without metrics
Public Class RegularCodeClass
    
    Private _data As String
    
    Public Property Data As String
        Get
            Return _data
        End Get
        Set(value As String)
            _data = value
        End Set
    End Property
    
    Public Sub ProcessData()
        If Not String.IsNullOrEmpty(_data) Then
            Console.WriteLine(_data)
        End If
    End Sub
    
End Class
