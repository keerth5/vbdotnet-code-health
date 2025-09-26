' Test file for cq-vbn-0182: Do not call BinaryFormatter.Deserialize without first setting BinaryFormatter.Binder
' This rule detects BinaryFormatter.Deserialize calls without setting the Binder property

Imports System
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization

Public Class BinaryFormatterBinderViolations
    
    ' Violation: Deserialize without setting Binder
    Public Sub DeserializeWithoutBinder()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' Violation: No Binder set
    End Sub
    
    ' Violation: UnsafeDeserialize without setting Binder
    Public Sub UnsafeDeserializeWithoutBinder()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.UnsafeDeserialize(stream, Nothing) ' Violation: No Binder set
    End Sub
    
    ' Violation: Multiple deserialize calls without Binder
    Public Sub MultipleDeserializeWithoutBinder()
        Dim formatter As New BinaryFormatter()
        Dim stream1 As New MemoryStream()
        Dim stream2 As New MemoryStream()
        Dim result1 = formatter.Deserialize(stream1) ' Violation
        Dim result2 = formatter.Deserialize(stream2) ' Violation
    End Sub
    
    ' Violation: Deserialize in loop without Binder
    Public Sub DeserializeInLoopWithoutBinder()
        Dim formatter As New BinaryFormatter()
        For i As Integer = 0 To 10
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream) ' Violation
        Next
    End Sub
    
    ' Violation: Deserialize in conditional without Binder
    Public Sub ConditionalDeserializeWithoutBinder(condition As Boolean)
        Dim formatter As New BinaryFormatter()
        If condition Then
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream) ' Violation
        End If
    End Sub
    
    ' Violation: Field formatter deserialize without Binder
    Private fieldFormatter As New BinaryFormatter()
    
    Public Sub UseFieldFormatterWithoutBinder()
        Dim stream As New MemoryStream()
        Dim result = fieldFormatter.Deserialize(stream) ' Violation
    End Sub
    
    ' Violation: Method parameter formatter without Binder
    Public Sub UseParameterFormatterWithoutBinder(formatter As BinaryFormatter)
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' Violation
    End Sub
    
    ' Violation: Try-catch deserialize without Binder
    Public Sub TryCatchDeserializeWithoutBinder()
        Try
            Dim formatter As New BinaryFormatter()
            Dim stream As New MemoryStream()
            Dim result = formatter.Deserialize(stream) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Non-violation: Deserialize with Binder set (should not be detected)
    Public Sub DeserializeWithBinder()
        Dim formatter As New BinaryFormatter()
        formatter.Binder = New CustomSerializationBinder()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' No violation
    End Sub
    
    ' Non-violation: Serialize without Binder (should not be detected)
    Public Sub SerializeWithoutBinder()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        formatter.Serialize(stream, "data") ' No violation - serialize doesn't need Binder
    End Sub
    
    ' Non-violation: Binder set after creation but before deserialize
    Public Sub BinderSetBeforeDeserialize()
        Dim formatter As New BinaryFormatter()
        ' Some other code
        formatter.Binder = New CustomSerializationBinder()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' No violation
    End Sub
    
    ' Violation: Binder set after deserialize call
    Public Sub BinderSetAfterDeserialize()
        Dim formatter As New BinaryFormatter()
        Dim stream As New MemoryStream()
        Dim result = formatter.Deserialize(stream) ' Violation - Binder set too late
        formatter.Binder = New CustomSerializationBinder()
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about formatter.Deserialize and Binder
        Dim message As String = "formatter.Deserialize requires Binder"
        Console.WriteLine("Set formatter.Binder before calling Deserialize")
    End Sub

End Class

' Helper class for testing
Public Class CustomSerializationBinder
    Inherits SerializationBinder
    
    Public Overrides Function BindToType(assemblyName As String, typeName As String) As Type
        Return Nothing
    End Function
End Class
