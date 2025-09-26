' Test file for cq-vbn-0183: Ensure BinaryFormatter.Binder is set before calling BinaryFormatter.Deserialize
' This rule detects when BinaryFormatter is created and Deserialize is called without setting Binder

Imports System
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization

Public Class BinaryFormatterBinderEnsureViolations
    
    ' Violation: Create BinaryFormatter and Deserialize without setting Binder
    Public Sub CreateAndDeserializeWithoutBinder()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' Violation: No Binder set
    End Sub
    
    ' Violation: Create BinaryFormatter and UnsafeDeserialize without setting Binder
    Public Sub CreateAndUnsafeDeserializeWithoutBinder()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.UnsafeDeserialize(stream, Nothing) ' Violation: No Binder set
    End Sub
    
    ' Violation: Multiple operations without Binder
    Public Sub MultipleOperationsWithoutBinder()
        Dim formatter As New BinaryFormatter()
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        
        ' Some other operations
        formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
        
        Dim result1 = formatter.Deserialize(stream1) ' Violation
        Dim result2 = formatter.Deserialize(stream2) ' Violation
    End Sub
    
    ' Violation: Create in loop and deserialize without Binder
    Public Sub CreateInLoopWithoutBinder()
        For i As Integer = 0 To 5
            Dim formatter As New BinaryFormatter()
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream) ' Violation
        Next
    End Sub
    
    ' Violation: Create conditionally and deserialize without Binder
    Public Sub CreateConditionallyWithoutBinder(condition As Boolean)
        If condition Then
            Dim formatter As New BinaryFormatter()
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream) ' Violation
        End If
    End Sub
    
    ' Violation: Try-catch with create and deserialize without Binder
    Public Sub TryCatchCreateWithoutBinder()
        Try
            Dim formatter As New BinaryFormatter()
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Using statement with create and deserialize without Binder
    Public Sub UsingStatementWithoutBinder()
        Using stream As New MemoryStream()
            Dim formatter As New BinaryFormatter()
            Dim result = formatter.Deserialize(stream) ' Violation
        End Using
    End Sub
    
    ' Violation: Method with return and no Binder
    Public Function DeserializeDataWithoutBinder(data As Byte()) As Object
        Dim formatter As New BinaryFormatter()
        Using stream As New MemoryStream(data)
            Return formatter.Deserialize(stream) ' Violation
        End Using
    End Function
    
    ' Non-violation: Create BinaryFormatter and set Binder before Deserialize
    Public Sub CreateAndDeserializeWithBinder()
        Dim formatter As New BinaryFormatter()
        formatter.Binder = New CustomSerializationBinder()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' No violation
    End Sub
    
    ' Non-violation: Create BinaryFormatter and only Serialize
    Public Sub CreateAndSerializeOnly()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        formatter.Serialize(stream, "data") ' No violation - only serializing
    End Sub
    
    ' Non-violation: Binder set immediately after creation
    Public Sub BinderSetImmediately()
        Dim formatter As New BinaryFormatter()
        formatter.Binder = New CustomSerializationBinder()
        ' Some other operations
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' No violation
    End Sub
    
    ' Violation: Binder set after Deserialize call
    Public Sub BinderSetTooLate()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' Violation - Binder not set yet
        formatter.Binder = New CustomSerializationBinder() ' Too late
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This creates a New BinaryFormatter() and calls Deserialize
        Dim message As String = "Dim formatter As New BinaryFormatter() then formatter.Deserialize(stream)"
        Console.WriteLine("Always set Binder before Deserialize")
    End Sub
    
    ' Violation: Multiple formatters without Binder
    Public Sub MultipleFormattersWithoutBinder()
        Dim formatter1 As New BinaryFormatter()
        Dim formatter2 As New BinaryFormatter()
        
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        
        Dim result1 = formatter1.Deserialize(stream1) ' Violation
        Dim result2 = formatter2.Deserialize(stream2) ' Violation
    End Sub

End Class

' Helper class for testing
Public Class CustomSerializationBinder
    Inherits SerializationBinder
    
    Public Overrides Function BindToType(assemblyName As String, typeName As String) As Type
        Return Nothing
    End Function
End Class
