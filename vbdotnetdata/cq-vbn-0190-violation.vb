' Test file for cq-vbn-0190: Ensure JavaScriptSerializer is not initialized with SimpleTypeResolver before deserializing
' This rule detects when JavaScriptSerializer is created with SimpleTypeResolver and then used for deserialization

Imports System
Imports System.Web.Script.Serialization

Public Class JavaScriptSerializerSimpleTypeResolverEnsureViolations
    
    ' Violation: Create JavaScriptSerializer with SimpleTypeResolver and Deserialize
    Public Sub CreateWithSimpleTypeResolverAndDeserialize()
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.Deserialize(jsonData, GetType(Object)) ' Violation
    End Sub
    
    ' Violation: Create JavaScriptSerializer with SimpleTypeResolver and DeserializeObject
    Public Sub CreateWithSimpleTypeResolverAndDeserializeObject()
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.DeserializeObject(jsonData) ' Violation
    End Sub
    
    ' Violation: Create JavaScriptSerializer, set TypeResolver, and Deserialize
    Public Sub CreateSetTypeResolverAndDeserialize()
        Dim serializer As New JavaScriptSerializer()
        serializer.TypeResolver = New SimpleTypeResolver()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.Deserialize(jsonData, GetType(Object)) ' Violation
    End Sub
    
    ' Violation: Multiple operations with SimpleTypeResolver
    Public Sub MultipleOperationsWithSimpleTypeResolver()
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim jsonData1 As String = "{""data1"":""value1""}"
        Dim jsonData2 As String = "{""data2"":""value2""}"
        
        ' Some other operations
        serializer.MaxJsonLength = 1000
        
        Dim result1 = serializer.Deserialize(jsonData1, GetType(Object)) ' Violation
        Dim result2 = serializer.DeserializeObject(jsonData2) ' Violation
    End Sub
    
    ' Violation: Create in loop with SimpleTypeResolver and deserialize
    Public Sub CreateInLoopWithSimpleTypeResolver()
        For i As Integer = 0 To 5
            Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
            Dim jsonData As String = "{""index"":" & i.ToString() & "}"
            Dim result = serializer.DeserializeObject(jsonData) ' Violation
        Next
    End Sub
    
    ' Violation: Create conditionally with SimpleTypeResolver and deserialize
    Public Sub CreateConditionallyWithSimpleTypeResolver(condition As Boolean)
        If condition Then
            Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
            Dim jsonData As String = "{""conditional"":true}"
            Dim result = serializer.Deserialize(jsonData, GetType(Object)) ' Violation
        End If
    End Sub
    
    ' Violation: Try-catch with create with SimpleTypeResolver and deserialize
    Public Sub TryCatchCreateWithSimpleTypeResolver()
        Try
            Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
            Dim jsonData As String = "{""key"":""value""}"
            Dim result = serializer.DeserializeObject(jsonData) ' Violation
        Catch ex As Exception
            Console.WriteLine("Error occurred")
        End Try
    End Sub
    
    ' Violation: Using statement with create with SimpleTypeResolver and deserialize
    Public Sub UsingStatementWithSimpleTypeResolver()
        Using reader As New System.IO.StringReader("{""key"":""value""}")
            Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
            Dim jsonData As String = reader.ReadToEnd()
            Dim result = serializer.Deserialize(jsonData, GetType(Object)) ' Violation
        End Using
    End Sub
    
    ' Violation: Method with return and SimpleTypeResolver
    Public Function DeserializeDataWithSimpleTypeResolver(jsonData As String) As Object
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Return serializer.DeserializeObject(jsonData) ' Violation
    End Function
    
    ' Violation: TypeResolver set after creation and then deserialize
    Public Sub SetTypeResolverAfterCreationAndDeserialize()
        Dim serializer As New JavaScriptSerializer()
        ' Some other operations
        serializer.MaxJsonLength = 2000
        serializer.TypeResolver = New SimpleTypeResolver()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.Deserialize(jsonData, GetType(Object)) ' Violation
    End Sub
    
    ' Non-violation: Create JavaScriptSerializer with SimpleTypeResolver and only Serialize
    Public Sub CreateWithSimpleTypeResolverAndSerializeOnly()
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim data As Object = New With {.key = "value"}
        Dim jsonString = serializer.Serialize(data) ' No violation - only serializing
    End Sub
    
    ' Non-violation: Create JavaScriptSerializer without SimpleTypeResolver
    Public Sub CreateWithoutSimpleTypeResolver()
        Dim serializer As New JavaScriptSerializer()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.DeserializeObject(jsonData) ' No violation
    End Sub
    
    ' Non-violation: TypeResolver set to non-SimpleTypeResolver
    Public Sub SetTypeResolverToCustomResolver()
        Dim serializer As New JavaScriptSerializer()
        serializer.TypeResolver = New CustomTypeResolver()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.Deserialize(jsonData, GetType(Object)) ' No violation
    End Sub
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This creates a New JavaScriptSerializer(New SimpleTypeResolver()) and calls Deserialize
        Dim message As String = "Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver()) then serializer.Deserialize(jsonData, GetType(Object))"
        Console.WriteLine("Do not use SimpleTypeResolver before deserializing")
    End Sub
    
    ' Violation: Multiple serializers with SimpleTypeResolver
    Public Sub MultipleSerializersWithSimpleTypeResolver()
        Dim serializer1 As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim serializer2 As New JavaScriptSerializer()
        serializer2.TypeResolver = New SimpleTypeResolver()
        
        Dim jsonData1 As String = "{""data1"":""value1""}"
        Dim jsonData2 As String = "{""data2"":""value2""}"
        
        Dim result1 = serializer1.DeserializeObject(jsonData1) ' Violation
        Dim result2 = serializer2.Deserialize(jsonData2, GetType(Object)) ' Violation
    End Sub
    
    ' Violation: Select Case with creation with SimpleTypeResolver and deserialization
    Public Sub SelectCaseWithSimpleTypeResolver(option As Integer)
        Select Case option
            Case 1
                Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
                Dim jsonData As String = "{""option"":1}"
                Dim result = serializer.DeserializeObject(jsonData) ' Violation
            Case 2
                Dim serializer2 As New JavaScriptSerializer()
                serializer2.TypeResolver = New SimpleTypeResolver()
                Dim jsonData2 As String = "{""option"":2}"
                Dim result2 = serializer2.Deserialize(jsonData2, GetType(Object)) ' Violation
        End Select
    End Sub
    
    ' Violation: While loop with SimpleTypeResolver
    Public Sub WhileLoopWithSimpleTypeResolver()
        Dim counter As Integer = 0
        While counter < 3
            Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
            Dim jsonData As String = "{""counter"":" & counter.ToString() & "}"
            Dim result = serializer.DeserializeObject(jsonData) ' Violation
            counter += 1
        End While
    End Sub

End Class

' Helper class for testing
Public Class CustomTypeResolver
    Inherits JavaScriptTypeResolver
    
    Public Overrides Function ResolveType(id As String) As Type
        Return Nothing
    End Function
    
    Public Overrides Function ResolveTypeId(type As Type) As String
        Return Nothing
    End Function
End Class
