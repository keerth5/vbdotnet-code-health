' Test file for cq-vbn-0049: Avoid using cref tags with a prefix
' Rule should detect XML documentation with prefixed cref tags

Imports System

''' <summary>
''' Example class demonstrating cref tag usage
''' </summary>
Public Class DocumentationExamples
    
    ''' <summary>
    ''' Violation 1: Method with prefixed cref tag
    ''' See <see cref="T:System.String"/> for string operations
    ''' </summary>
    Public Sub ProcessString(input As String)
        Console.WriteLine(input)
    End Sub
    
    ''' <summary>
    ''' Violation 2: Property with prefixed seealso tag
    ''' </summary>
    ''' <seealso cref="M:System.Console.WriteLine"/>
    Public Property Message As String
    
    ''' <summary>
    ''' Violation 3: Method with multiple prefixed cref tags
    ''' Use <see cref="T:System.Collections.Generic.List`1"/> or <see cref="T:System.Array"/>
    ''' </summary>
    Public Function GetItems() As String()
        Return New String() {}
    End Function
    
    ''' <summary>
    ''' Violation 4: Event with prefixed cref
    ''' Raised when data changes. See <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/>
    ''' </summary>
    Public Event DataChanged As EventHandler
    
    ''' <summary>
    ''' Violation 5: Field documentation with prefixed cref
    ''' Similar to <see cref="F:System.String.Empty"/>
    ''' </summary>
    Public Shared ReadOnly EmptyValue As String = ""
    
    ''' <summary>
    ''' This should NOT be detected - proper cref without prefix
    ''' See <see cref="String"/> for string operations
    ''' </summary>
    Public Sub ProcessStringCorrect(input As String)
        Console.WriteLine(input)
    End Sub
    
    ''' <summary>
    ''' This should NOT be detected - seealso without prefix
    ''' </summary>
    ''' <seealso cref="Console.WriteLine"/>
    Public Property MessageCorrect As String
    
    ''' <summary>
    ''' This should NOT be detected - multiple proper cref tags
    ''' Use <see cref="List(Of String)"/> or <see cref="Array"/>
    ''' </summary>
    Public Function GetItemsCorrect() As String()
        Return New String() {}
    End Function
    
End Class

''' <summary>
''' Another class with documentation examples
''' </summary>
Public Class MoreDocumentationExamples
    
    ''' <summary>
    ''' Violation 6: Property with prefixed cref in different format
    ''' Related to <see cref="P:System.DateTime.Now"/>
    ''' </summary>
    Public Property CurrentTime As DateTime
        Get
            Return DateTime.Now
        End Get
    End Property
    
    ''' <summary>
    ''' Violation 7: Method with seealso prefixed cref
    ''' Processes the input data
    ''' </summary>
    ''' <param name="data">The data to process</param>
    ''' <seealso cref="M:System.String.Format"/>
    Public Sub ProcessData(data As String)
        Console.WriteLine($"Processing: {data}")
    End Sub
    
    ''' <summary>
    ''' This should NOT be detected - no cref tags
    ''' Simple method without cross-references
    ''' </summary>
    Public Sub SimpleMethod()
        Console.WriteLine("Simple operation")
    End Sub
    
    ''' <summary>
    ''' This should NOT be detected - proper cref usage
    ''' Uses <see cref="DateTime.Now"/> for current time
    ''' </summary>
    Public Function GetCurrentTime() As DateTime
        Return DateTime.Now
    End Function
    
End Class

''' <summary>
''' Interface with documentation
''' </summary>
Public Interface IDataProcessor
    
    ''' <summary>
    ''' Violation 8: Interface method with prefixed cref
    ''' Similar to <see cref="T:System.IDisposable"/>
    ''' </summary>
    Sub ProcessData(data As String)
    
    ''' <summary>
    ''' This should NOT be detected - proper interface documentation
    ''' Processes data similar to <see cref="IDisposable"/>
    ''' </summary>
    Function GetResult() As String
    
End Interface
