' Test file for cq-vbn-0151: Cache and reuse 'JsonSerializerOptions' instances
' Using a local instance of JsonSerializerOptions for serialization or deserialization can substantially degrade performance

Imports System.Text.Json

Public Class JsonSerializationTests
    
    ' Violation: Creating new JsonSerializerOptions locally in method
    Public Sub SerializeWithLocalOptions()
        Dim data As String = "test data"
        
        ' Violation: Local JsonSerializerOptions instance
        Dim options As New JsonSerializerOptions With {
            .PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            .WriteIndented = True
        }
        
        Dim json As String = JsonSerializer.Serialize(data, options)
        Console.WriteLine(json)
    End Sub
    
    ' Violation: Creating JsonSerializerOptions inline
    Public Sub SerializeWithInlineOptions()
        Dim data As Object = New With {.Name = "Test", .Value = 123}
        
        ' Violation: Inline JsonSerializerOptions creation
        Dim json As String = JsonSerializer.Serialize(data, New JsonSerializerOptions With {
            .PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        })
        
        Console.WriteLine(json)
    End Sub
    
    ' Violation: Multiple local JsonSerializerOptions instances
    Public Sub DeserializeWithLocalOptions()
        Dim json As String = "{""name"": ""test""}"
        
        ' Violation: Another local instance
        Dim deserializeOptions As New JsonSerializerOptions()
        deserializeOptions.PropertyNameCaseInsensitive = True
        
        Dim result As Object = JsonSerializer.Deserialize(Of Object)(json, deserializeOptions)
    End Sub
    
    ' Violation: JsonSerializerOptions in loop (very bad for performance)
    Public Sub ProcessMultipleItems()
        Dim items() As String = {"item1", "item2", "item3"}
        
        For Each item In items
            ' Violation: Creating options in loop
            Dim options As New JsonSerializerOptions With {
                .WriteIndented = True
            }
            
            Dim json As String = JsonSerializer.Serialize(item, options)
            Console.WriteLine(json)
        Next
    End Sub
    
    ' Violation: JsonSerializerOptions in Deserialize call
    Public Sub DeserializeInline()
        Dim json As String = "{""test"": true}"
        
        ' Violation: Inline options in Deserialize
        Dim result As Dictionary(Of String, Object) = JsonSerializer.Deserialize(Of Dictionary(Of String, Object))(
            json, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})
    End Sub
    
    ' Good practice: Static/cached options (should not be detected)
    Private Shared ReadOnly CachedOptions As New JsonSerializerOptions With {
        .PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        .WriteIndented = True
    }
    
    ' Good: Using cached options
    Public Sub SerializeWithCachedOptions()
        Dim data As String = "test data"
        Dim json As String = JsonSerializer.Serialize(data, CachedOptions)
        Console.WriteLine(json)
    End Sub
    
    ' Good: Using default options (no options parameter)
    Public Sub SerializeWithDefaults()
        Dim data As String = "test data"
        Dim json As String = JsonSerializer.Serialize(data)
        Console.WriteLine(json)
    End Sub
    
    ' Violation: Complex JsonSerializerOptions creation
    Public Function CreateComplexJson() As String
        Dim complexData As New With {
            .Items = New String() {"a", "b", "c"},
            .Count = 3,
            .IsActive = True
        }
        
        ' Violation: Complex local options
        Dim complexOptions As New JsonSerializerOptions()
        complexOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        complexOptions.WriteIndented = True
        complexOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        
        Return JsonSerializer.Serialize(complexData, complexOptions)
    End Function
    
    ' Violation: JsonSerializerOptions with custom converter
    Public Sub SerializeWithCustomConverter()
        Dim data As DateTime = DateTime.Now
        
        ' Violation: Local options with converter
        Dim options As New JsonSerializerOptions()
        options.Converters.Add(New JsonStringEnumConverter())
        
        Dim json As String = JsonSerializer.Serialize(data, options)
        Console.WriteLine(json)
    End Sub

End Class

' Additional test cases for different scenarios
Public Module JsonUtilities
    
    ' Violation: Module-level method with local options
    Public Function SerializeToJson(data As Object) As String
        ' Violation: Creating options in utility method
        Dim options As New JsonSerializerOptions With {
            .PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }
        
        Return JsonSerializer.Serialize(data, options)
    End Function
    
    ' Violation: Method with multiple JsonSerializerOptions instances
    Public Sub ProcessJsonData()
        Dim data1 As String = "test1"
        Dim data2 As String = "test2"
        
        ' Violation: First local instance
        Dim options1 As New JsonSerializerOptions()
        options1.WriteIndented = True
        
        ' Violation: Second local instance
        Dim options2 As New JsonSerializerOptions()
        options2.PropertyNameCaseInsensitive = True
        
        Dim json1 As String = JsonSerializer.Serialize(data1, options1)
        Dim result2 As String = JsonSerializer.Deserialize(Of String)(json1, options2)
    End Sub
    
End Module
