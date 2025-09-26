' Test file for cq-vbn-0091: Identifiers should not contain type names
' Rule should detect identifiers that contain type names like 'Int32', 'String', etc.

Imports System

' Violation 1: Class with type name
Public Class StringManager
    Public Property Name As String
End Class

' Violation 2: Class with type name
Public Class Int32Helper
    Public Property Value As Integer
End Class

' Violation 3: Class with type name
Friend Class BooleanProcessor
    Public Property IsEnabled As Boolean
End Class

' Violation 4: Class with type name
Public Class DateTimeService
    Public Property CurrentTime As DateTime
End Class

' Violation 5: Class with type name
Protected Class ObjectFactory
    Public Function CreateObject() As Object
        Return New Object()
    End Function
End Class

Public Class TypeNameExamples
    
    ' Violation 6: Method with type name
    Public Sub ProcessStringData()
        Console.WriteLine("Processing string data")
    End Sub
    
    ' Violation 7: Method with type name
    Protected Sub HandleInt32Values()
        Console.WriteLine("Handling integer values")
    End Sub
    
    ' Violation 8: Function with type name
    Public Function GetBooleanResult() As Boolean
        Return True
    End Function
    
    ' Violation 9: Function with type name
    Friend Function CreateDoubleArray() As Double()
        Return New Double(10) {}
    End Function
    
    ' Violation 10: Property with type name
    Public Property StringCollection As String()
    
    ' Violation 11: Property with type name
    Protected Property DecimalValue As Decimal
    
    ' Violation 12: Property with type name
    Friend Property ArrayManager As Array
    
    Public Sub TestMethod(
        ByVal stringParam As String,        ' Violation 13: Parameter with type name
        ByVal int32Value As Integer,        ' Violation 14: Parameter with type name
        ByVal booleanFlag As Boolean        ' Violation 15: Parameter with type name
    )
        Console.WriteLine("Method with type name parameters")
    End Sub
    
    Public Sub AnotherMethod(
        ByVal dateTimeInput As DateTime,    ' Violation 16: Parameter with type name
        ByVal objectRef As Object,          ' Violation 17: Parameter with type name
        ByVal listData As List(Of String)   ' Violation 18: Parameter with type name
    )
        Console.WriteLine("Another method with type names")
    End Sub
    
    ' This should NOT be detected - proper naming without type names
    Public Sub ProcessData()
        Console.WriteLine("Proper method name")
    End Sub
    
    ' This should NOT be detected - proper naming
    Public Property UserName As String
    
    ' This should NOT be detected - proper naming
    Public Function CalculateTotal() As Double
        Return 0.0
    End Function
    
End Class

' Violation 19: Class with type name
Public Class DictionaryCache
    Private _cache As New Dictionary(Of String, Object)
End Class

' Violation 20: Class with type name
Friend Class CollectionUtils
    Public Sub ProcessItems()
        Console.WriteLine("Processing items")
    End Sub
End Class

Public Class MoreTypeNameExamples
    
    ' Violation 21: Method with type name
    Public Sub InitializeListItems()
        Console.WriteLine("Initializing list")
    End Sub
    
    ' Violation 22: Function with type name
    Protected Function ValidateStringInput() As Boolean
        Return True
    End Function
    
    ' Violation 23: Property with type name
    Public Property DictionaryKeys As String()
    
    Public Sub ParameterTest(
        ByVal collectionSize As Integer,    ' Violation 24: Parameter with type name
        ByVal arrayIndex As Integer         ' Violation 25: Parameter with type name
    )
        Console.WriteLine("Parameter test method")
    End Sub
    
End Class

' This should NOT be detected - proper class naming
Public Class DataProcessor
    
    Public Sub ProcessInformation()
        Console.WriteLine("Processing information")
    End Sub
    
    Public Property CustomerName As String
    
End Class

' This should NOT be detected - proper class naming
Friend Class ConfigurationManager
    
    Public Property Settings As String
    
    Public Function LoadConfiguration() As Boolean
        Return True
    End Function
    
End Class

' Violation 26: Class with multiple type names
Public Class StringArrayProcessor
    
    ' Violation 27: Method with type name
    Public Sub ConvertObjectToString()
        Console.WriteLine("Converting object")
    End Sub
    
End Class
