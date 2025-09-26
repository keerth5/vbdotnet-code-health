' Test file for cq-vbn-0098: Do not ignore method results
' Rule should detect method calls whose results are ignored when they should be used

Imports System

Public Class IgnoredMethodResults
    
    Public Sub TestIgnoredResults()
        
        Dim text As String = "Hello World"
        Dim number As Integer = 42
        Dim obj As New Object()
        
        ' Violation 1: ToString result ignored
        text.ToString()
        
        ' Violation 2: Clone result ignored
        text.Clone()
        
        ' Violation 3: GetHashCode result ignored
        obj.GetHashCode()
        
        ' Violation 4: CompareTo result ignored
        text.CompareTo("Other")
        
        ' Violation 5: Equals result ignored
        obj.Equals(Nothing)
        
        ' Violation 6: Object creation ignored
        New StringBuilder()
        
        ' Violation 7: Another object creation ignored
        New List(Of String)()
        
        ' Violation 8: Object creation with parameters ignored
        New Random(123)
        
        ' This should NOT be detected - result is used
        Dim result As String = text.ToString()
        Console.WriteLine(result)
        
        ' This should NOT be detected - result is used in expression
        If obj.Equals(Nothing) Then
            Console.WriteLine("Object is null")
        End If
        
        ' This should NOT be detected - void method call
        Console.WriteLine("This is fine")
        
    End Sub
    
    Public Sub MoreIgnoredResults()
        
        Dim list As New List(Of String)
        Dim dict As New Dictionary(Of String, Integer)
        
        ' Violation 9: ToString on collection ignored
        list.ToString()
        
        ' Violation 10: GetHashCode on dictionary ignored
        dict.GetHashCode()
        
        ' Violation 11: Clone method ignored
        list.Clone()
        
        ' Violation 12: Object creation ignored
        New DateTime(2023, 1, 1)
        
        ' Violation 13: Object creation ignored
        New TimeSpan(1, 0, 0)
        
        ' This should NOT be detected - constructor assignment
        Dim newList As New List(Of String)()
        
        ' This should NOT be detected - method with void return
        list.Clear()
        
    End Sub
    
    Public Sub StringMethodsIgnored()
        
        Dim text As String = "Sample Text"
        
        ' Violation 14: String method result ignored
        text.ToUpper()
        
        ' Violation 15: String method result ignored
        text.ToLower()
        
        ' Violation 16: String method result ignored
        text.Trim()
        
        ' Violation 17: String method result ignored
        text.Substring(0, 5)
        
        ' This should NOT be detected - result is assigned
        Dim upperText As String = text.ToUpper()
        
        ' This should NOT be detected - result used in condition
        If text.Contains("Sample") Then
            Console.WriteLine("Found")
        End If
        
    End Sub
    
    Public Sub ObjectCreationIgnored()
        
        ' Violation 18: StringBuilder creation ignored
        New StringBuilder("Initial")
        
        ' Violation 19: Array creation ignored
        New String(10) {}
        
        ' Violation 20: Generic object creation ignored
        New Dictionary(Of String, Object)()
        
        ' Violation 21: Custom object creation ignored
        New CustomClass()
        
        ' This should NOT be detected - object assigned to variable
        Dim sb As New StringBuilder()
        
        ' This should NOT be detected - object used in method call
        ProcessObject(New CustomClass())
        
    End Sub
    
    Private Sub ProcessObject(obj As Object)
        Console.WriteLine(obj.ToString())
    End Sub
    
    Public Sub ComparisonMethodsIgnored()
        
        Dim str1 As String = "Hello"
        Dim str2 As String = "World"
        Dim num1 As Integer = 10
        Dim num2 As Integer = 20
        
        ' Violation 22: String comparison ignored
        str1.CompareTo(str2)
        
        ' Violation 23: Equals method ignored
        str1.Equals(str2)
        
        ' Violation 24: Object comparison ignored
        num1.CompareTo(num2)
        
        ' This should NOT be detected - comparison result used
        If str1.CompareTo(str2) = 0 Then
            Console.WriteLine("Strings are equal")
        End If
        
    End Sub
    
    Public Sub HashCodeMethodsIgnored()
        
        Dim obj1 As New Object()
        Dim obj2 As New CustomClass()
        Dim str As String = "Test"
        
        ' Violation 25: GetHashCode ignored
        obj1.GetHashCode()
        
        ' Violation 26: GetHashCode on custom object ignored
        obj2.GetHashCode()
        
        ' Violation 27: GetHashCode on string ignored
        str.GetHashCode()
        
        ' This should NOT be detected - hash code used
        Dim hash As Integer = obj1.GetHashCode()
        
    End Sub
    
End Class

Public Class CustomClass
    
    Public Sub TestMethod()
        
        ' Violation 28: Own ToString ignored
        Me.ToString()
        
        ' Violation 29: Own GetHashCode ignored
        Me.GetHashCode()
        
        ' Violation 30: Object creation in method ignored
        New CustomClass()
        
    End Sub
    
    Public Overrides Function ToString() As String
        Return "CustomClass"
    End Function
    
End Class

Public Class StaticMethodIgnored
    
    Public Shared Sub TestStaticIgnored()
        
        ' Violation 31: Static object creation ignored
        New Random()
        
        ' Violation 32: Object creation ignored
        New System.Text.StringBuilder()
        
        ' This should NOT be detected - static method call (void)
        Console.WriteLine("Message")
        
        ' This should NOT be detected - static method result used
        Dim guid As Guid = Guid.NewGuid()
        
    End Sub
    
End Class
