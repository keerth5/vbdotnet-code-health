' Test file for cq-vbn-0189: Do not deserialize with JavaScriptSerializer using a SimpleTypeResolver
' This rule detects usage of JavaScriptSerializer with SimpleTypeResolver which is insecure for deserializing untrusted data

Imports System
Imports System.Web.Script.Serialization

Public Class JavaScriptSerializerSimpleTypeResolverViolations
    
    ' Violation: Creating JavaScriptSerializer with SimpleTypeResolver in constructor
    Public Sub CreateJavaScriptSerializerWithSimpleTypeResolver()
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        ' Some code here
    End Sub
    
    ' Violation: Setting TypeResolver to SimpleTypeResolver
    Public Sub SetTypeResolverToSimpleTypeResolver()
        Dim serializer As New JavaScriptSerializer()
        serializer.TypeResolver = New SimpleTypeResolver()
        ' Some code here
    End Sub
    
    ' Violation: JavaScriptSerializer with SimpleTypeResolver and Deserialize
    Public Sub DeserializeWithSimpleTypeResolver()
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.Deserialize(jsonData, GetType(Object))
    End Sub
    
    ' Violation: JavaScriptSerializer TypeResolver assignment and DeserializeObject
    Public Sub DeserializeObjectWithSimpleTypeResolver()
        Dim serializer As New JavaScriptSerializer()
        serializer.TypeResolver = New SimpleTypeResolver()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.DeserializeObject(jsonData)
    End Sub
    
    ' Violation: Multiple JavaScriptSerializer instances with SimpleTypeResolver
    Public Sub MultipleJavaScriptSerializersWithSimpleTypeResolver()
        Dim serializer1 As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim serializer2 As New JavaScriptSerializer()
        serializer2.TypeResolver = New SimpleTypeResolver()
        
        Dim jsonData1 As String = "{""data1"":""value1""}"
        Dim jsonData2 As String = "{""data2"":""value2""}"
        
        Dim result1 = serializer1.Deserialize(jsonData1, GetType(Object))
        Dim result2 = serializer2.DeserializeObject(jsonData2)
    End Sub
    
    ' Violation: JavaScriptSerializer with SimpleTypeResolver in different contexts
    Public Function DeserializeJsonData(jsonData As String) As Object
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Return serializer.Deserialize(jsonData, GetType(Object))
    End Function
    
    ' Violation: JavaScriptSerializer with SimpleTypeResolver as field
    Private javaScriptSerializer As New JavaScriptSerializer(New SimpleTypeResolver())
    
    Public Sub UseFieldJavaScriptSerializer()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = javaScriptSerializer.DeserializeObject(jsonData)
    End Sub
    
    ' Violation: JavaScriptSerializer with SimpleTypeResolver in Try-Catch
    Public Sub DeserializeWithErrorHandling()
        Try
            Dim serializer As New JavaScriptSerializer()
            serializer.TypeResolver = New SimpleTypeResolver()
            Dim jsonData As String = "{""key"":""value""}"
            Dim result = serializer.Deserialize(jsonData, GetType(Object))
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub
    
    ' Violation: JavaScriptSerializer TypeResolver set in conditional
    Public Sub ConditionalSimpleTypeResolver(useSimpleResolver As Boolean)
        Dim serializer As New JavaScriptSerializer()
        If useSimpleResolver Then
            serializer.TypeResolver = New SimpleTypeResolver()
        End If
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.DeserializeObject(jsonData)
    End Sub
    
    ' Non-violation: JavaScriptSerializer without SimpleTypeResolver (should not be detected)
    Public Sub SafeJavaScriptSerializer()
        Dim serializer As New JavaScriptSerializer()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.DeserializeObject(jsonData)
    End Sub
    
    ' Non-violation: JavaScriptSerializer with other TypeResolver (should not be detected)
    Public Sub JavaScriptSerializerWithOtherTypeResolver()
        Dim serializer As New JavaScriptSerializer()
        serializer.TypeResolver = New CustomTypeResolver()
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.Deserialize(jsonData, GetType(Object))
    End Sub
    
    ' Non-violation: Safe serialization (should not be detected)
    Public Sub SafeSerialization()
        Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
        Dim data As Object = New With {.key = "value"}
        Dim jsonString = serializer.Serialize(data)
    End Sub
    
    ' Non-violation: Comments and strings mentioning SimpleTypeResolver
    Public Sub CommentsAndStrings()
        ' This is a comment about SimpleTypeResolver
        Dim message As String = "Do not use SimpleTypeResolver for untrusted data"
        Console.WriteLine("JavaScriptSerializer with SimpleTypeResolver is insecure")
    End Sub
    
    ' Violation: JavaScriptSerializer with SimpleTypeResolver in loop
    Public Sub JavaScriptSerializerInLoop()
        For i As Integer = 0 To 10
            Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
            Dim jsonData As String = "{""index"":" & i.ToString() & "}"
            Dim result = serializer.DeserializeObject(jsonData)
        Next
    End Sub
    
    ' Violation: JavaScriptSerializer with SimpleTypeResolver assignment in loop
    Public Sub TypeResolverAssignmentInLoop()
        For i As Integer = 0 To 5
            Dim serializer As New JavaScriptSerializer()
            serializer.TypeResolver = New SimpleTypeResolver()
            Dim jsonData As String = "{""index"":" & i.ToString() & "}"
            Dim result = serializer.Deserialize(jsonData, GetType(Object))
        Next
    End Sub
    
    ' Violation: JavaScriptSerializer with variable assignment
    Public Sub AssignJavaScriptSerializerWithSimpleTypeResolver()
        Dim serializer As JavaScriptSerializer
        serializer = New JavaScriptSerializer(New SimpleTypeResolver())
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.DeserializeObject(jsonData)
    End Sub
    
    ' Violation: JavaScriptSerializer TypeResolver assignment with variable
    Public Sub AssignTypeResolverVariable()
        Dim serializer As New JavaScriptSerializer()
        Dim resolver As New SimpleTypeResolver()
        serializer.TypeResolver = resolver
        Dim jsonData As String = "{""key"":""value""}"
        Dim result = serializer.Deserialize(jsonData, GetType(Object))
    End Sub
    
    ' Violation: JavaScriptSerializer in Select Case with SimpleTypeResolver
    Public Sub JavaScriptSerializerInSelectCase(option As Integer)
        Select Case option
            Case 1
                Dim serializer As New JavaScriptSerializer(New SimpleTypeResolver())
                Dim jsonData As String = "{""option"":1}"
                Dim result = serializer.DeserializeObject(jsonData)
            Case 2
                Dim serializer2 As New JavaScriptSerializer()
                serializer2.TypeResolver = New SimpleTypeResolver()
                Dim jsonData2 As String = "{""option"":2}"
                Dim result2 = serializer2.Deserialize(jsonData2, GetType(Object))
        End Select
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
