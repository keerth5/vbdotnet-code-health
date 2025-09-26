' Test file for cq-vbn-0068: Avoid excessive complexity
' Rule should detect methods with high cyclomatic complexity (multiple control flow statements)

Imports System

Public Class ComplexityExamples
    
    ' Violation 1: Method with multiple nested If statements
    Public Sub ComplexMethod1(value As Integer, flag As Boolean, status As String)
        If value > 0 Then
            If flag Then
                If status = "active" Then
                    Console.WriteLine("All conditions met")
                End If
            End If
        End If
    End Sub
    
    ' Violation 2: Method with multiple For loops and If statements
    Public Sub ComplexMethod2(data() As Integer)
        For i As Integer = 0 To data.Length - 1
            If data(i) > 0 Then
                For j As Integer = i + 1 To data.Length - 1
                    If data(j) < data(i) Then
                        Console.WriteLine("Found smaller element")
                    End If
                Next
            End If
        Next
    End Sub
    
    ' Violation 3: Method with While loops and Case statements
    Public Sub ComplexMethod3(input As String)
        Dim index As Integer = 0
        While index < input.Length
            Select Case input(index)
                Case "a"c
                    If index > 0 Then
                        Console.WriteLine("Found 'a'")
                    End If
                Case "b"c
                    If index < input.Length - 1 Then
                        Console.WriteLine("Found 'b'")
                    End If
            End Select
            index += 1
        End While
    End Sub
    
    ' This should NOT be detected - simple method with single control flow
    Public Sub SimpleMethod(value As Integer)
        If value > 0 Then
            Console.WriteLine("Positive value")
        End If
    End Sub
    
    ' Violation 4: Method with Do loops and ElseIf statements
    Public Sub ComplexMethod4(numbers() As Integer)
        Dim i As Integer = 0
        Do While i < numbers.Length
            If numbers(i) = 0 Then
                Console.WriteLine("Zero found")
            ElseIf numbers(i) > 0 Then
                If numbers(i) Mod 2 = 0 Then
                    Console.WriteLine("Even positive")
                End If
            End If
            i += 1
        Loop
    End Sub
    
    ' Violation 5: Function with multiple return paths and conditions
    Public Function ComplexFunction1(x As Integer, y As Integer, z As Integer) As String
        If x > 0 Then
            If y > 0 Then
                If z > 0 Then
                    Return "All positive"
                End If
            End If
        End If
        Return "Not all positive"
    End Function
    
    ' Violation 6: Private method with high complexity
    Private Sub ComplexPrivateMethod(items() As String)
        For Each item In items
            If Not String.IsNullOrEmpty(item) Then
                Select Case item.ToLower()
                    Case "start"
                        If items.Length > 1 Then
                            Console.WriteLine("Starting process")
                        End If
                    Case "stop"
                        If items.Length > 1 Then
                            Console.WriteLine("Stopping process")
                        End If
                End Select
            End If
        Next
    End Sub
    
    ' This should NOT be detected - method with only two control flow statements
    Public Sub ModerateMethod(flag As Boolean)
        If flag Then
            For i As Integer = 1 To 10
                Console.WriteLine(i)
            Next
        End If
    End Sub
    
    ' Violation 7: Protected method with nested loops and conditions
    Protected Sub ComplexProtectedMethod(matrix(,) As Integer)
        For i As Integer = 0 To matrix.GetLength(0) - 1
            For j As Integer = 0 To matrix.GetLength(1) - 1
                If matrix(i, j) > 0 Then
                    If i = j Then
                        Console.WriteLine("Diagonal positive element")
                    End If
                End If
            Next
        Next
    End Sub
    
    ' Violation 8: Friend method with complex logic
    Friend Sub ComplexFriendMethod(data As Dictionary(Of String, Integer))
        For Each kvp In data
            If kvp.Value > 0 Then
                Select Case kvp.Key
                    Case "priority"
                        If kvp.Value > 5 Then
                            Console.WriteLine("High priority")
                        End If
                    Case "status"
                        If kvp.Value = 1 Then
                            Console.WriteLine("Active status")
                        End If
                End Select
            End If
        Next
    End Sub
    
End Class

Public Class MoreComplexityExamples
    
    ' Violation 9: Method with multiple exception handling and conditions
    Public Sub ComplexExceptionHandling(filePath As String)
        Try
            If System.IO.File.Exists(filePath) Then
                Dim content = System.IO.File.ReadAllText(filePath)
                If Not String.IsNullOrEmpty(content) Then
                    If content.Contains("error") Then
                        Console.WriteLine("Error found in file")
                    End If
                End If
            End If
        Catch ex As Exception
            If TypeOf ex Is System.IO.FileNotFoundException Then
                Console.WriteLine("File not found")
            ElseIf TypeOf ex Is UnauthorizedAccessException Then
                Console.WriteLine("Access denied")
            End If
        End Try
    End Sub
    
    ' Violation 10: Method with complex validation logic
    Public Function ComplexValidation(user As Object) As Boolean
        If user IsNot Nothing Then
            If user.GetType().GetProperty("Name") IsNot Nothing Then
                Dim name = user.GetType().GetProperty("Name").GetValue(user)
                If name IsNot Nothing Then
                    If name.ToString().Length > 0 Then
                        Return True
                    End If
                End If
            End If
        End If
        Return False
    End Function
    
    ' This should NOT be detected - simple validation
    Public Function SimpleValidation(value As String) As Boolean
        If String.IsNullOrEmpty(value) Then
            Return False
        End If
        Return True
    End Function
    
End Class
