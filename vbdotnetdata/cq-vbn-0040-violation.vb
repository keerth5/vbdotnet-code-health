' Test file for cq-vbn-0040: Validate arguments of public methods
' Rule should detect public methods with reference type parameters that should validate for null

Imports System

Public Class ArgumentValidationExamples
    
    ' Violation 1: Public method with String parameter (should validate for null)
    Public Sub ProcessText(text As String)
        ' Missing null check - should have: If text Is Nothing Then Throw New ArgumentNullException(NameOf(text))
        Console.WriteLine(text.ToUpper())
    End Sub
    
    ' Violation 2: Public method with Object parameter
    Public Function SerializeObject(obj As Object) As String
        ' Missing null check
        Return obj.ToString()
    End Function
    
    ' Violation 3: Public method with array parameter
    Public Sub ProcessArray(items As String())
        ' Missing null check
        For Each item In items
            Console.WriteLine(item)
        Next
    End Sub
    
    ' Violation 4: Public method with multiple reference parameters
    Public Sub CombineData(first As String, second As Object)
        ' Missing null checks for both parameters
        Console.WriteLine(first & second.ToString())
    End Sub
    
    ' Violation 5: Public method with generic array
    Public Function ProcessItems(Of T)(items As T()) As Integer
        ' Missing null check
        Return items.Length
    End Function
    
    ' Violation 6: Public method with object array
    Public Sub DisplayObjects(objects As Object())
        ' Missing null check
        For Each obj In objects
            Console.WriteLine(obj.ToString())
        Next
    End Sub
    
    ' This should NOT be detected - method with proper null validation
    Public Sub ProcessTextWithValidation(text As String)
        If text Is Nothing Then
            Throw New ArgumentNullException(NameOf(text))
        End If
        Console.WriteLine(text.ToUpper())
    End Sub
    
    ' This should NOT be detected - method with value type parameters
    Public Function Add(a As Integer, b As Integer) As Integer
        Return a + b
    End Function
    
    ' This should NOT be detected - private method (less critical)
    Private Sub InternalProcess(data As String)
        Console.WriteLine(data.Length)
    End Sub
    
    ' This should NOT be detected - protected method (less critical for this rule)
    Protected Sub ProcessInternal(data As String)
        Console.WriteLine(data)
    End Sub
    
    ' This should NOT be detected - method without parameters
    Public Sub DoWork()
        Console.WriteLine("Doing work")
    End Sub
    
End Class

Public Class DataProcessor
    
    ' Violation 7: Another class with validation issues
    Public Sub SaveData(data As String, filename As String)
        ' Missing null checks for both parameters
        System.IO.File.WriteAllText(filename, data)
    End Sub
    
    ' Violation 8: Method with collection parameter
    Public Function CountItems(items As Object()) As Integer
        ' Missing null check
        Return items.Count()
    End Function
    
End Class

Public Class FileManager
    
    ' Violation 9: Method with string parameter for file operations
    Public Sub DeleteFile(filePath As String)
        ' Missing null check
        System.IO.File.Delete(filePath)
    End Sub
    
    ' Violation 10: Method with object parameter
    Public Sub LogObject(obj As Object)
        ' Missing null check
        Console.WriteLine($"Logging: {obj.GetType().Name}")
    End Sub
    
End Class
