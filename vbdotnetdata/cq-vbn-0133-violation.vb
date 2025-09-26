' VB.NET test file for cq-vbn-0133: Call async methods when in an async method
' Rule: In a method which is already asynchronous, calls to other methods should be to their async versions, where they exist.

Imports System
Imports System.IO
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Data.SqlClient

Public Class AsyncMethodExamples
    Private ReadOnly _httpClient As HttpClient
    Private ReadOnly _connectionString As String
    
    Public Sub New()
        _httpClient = New HttpClient()
        _connectionString = "Server=localhost;Database=TestDB;Integrated Security=true;"
    End Sub
    
    ' Violation: Async method calling sync File.ReadAllText instead of File.ReadAllTextAsync
    Public Async Function ReadFileContentAsync(filePath As String) As Task(Of String)
        Return File.ReadAllText(filePath)  ' Should use File.ReadAllTextAsync
    End Function
    
    ' Violation: Async method calling sync File.WriteAllText instead of File.WriteAllTextAsync
    Public Async Function WriteFileContentAsync(filePath As String, content As String) As Task
        File.WriteAllText(filePath, content)  ' Should use File.WriteAllTextAsync
    End Function
    
    ' Violation: Async method calling sync File.ReadAllLines instead of File.ReadAllLinesAsync
    Public Async Function ReadLinesAsync(filePath As String) As Task(Of String())
        Return File.ReadAllLines(filePath)  ' Should use File.ReadAllLinesAsync
    End Function
    
    ' Violation: Async method calling sync HttpClient.Send instead of SendAsync
    Public Async Function SendHttpRequestAsync(request As HttpRequestMessage) As Task(Of HttpResponseMessage)
        Return _httpClient.Send(request)  ' Should use _httpClient.SendAsync
    End Function
    
    ' Violation: Async method calling sync Stream.Read instead of ReadAsync
    Public Async Function ReadStreamAsync(stream As Stream, buffer() As Byte) As Task(Of Integer)
        Return stream.Read(buffer, 0, buffer.Length)  ' Should use stream.ReadAsync
    End Function
    
    ' Violation: Async method calling sync Stream.Write instead of WriteAsync
    Public Async Function WriteStreamAsync(stream As Stream, data() As Byte) As Task
        stream.Write(data, 0, data.Length)  ' Should use stream.WriteAsync
    End Function
    
    ' Violation: Async method calling sync SqlCommand.ExecuteReader instead of ExecuteReaderAsync
    Public Async Function ExecuteQueryAsync(query As String) As Task(Of SqlDataReader)
        Using connection As New SqlConnection(_connectionString)
            connection.Open()  ' Should use connection.OpenAsync
            Using command As New SqlCommand(query, connection)
                Return command.ExecuteReader()  ' Should use command.ExecuteReaderAsync
            End Using
        End Using
    End Function
    
    ' Violation: Async method calling sync SqlCommand.ExecuteNonQuery instead of ExecuteNonQueryAsync
    Public Async Function ExecuteCommandAsync(commandText As String) As Task(Of Integer)
        Using connection As New SqlConnection(_connectionString)
            connection.Open()  ' Should use connection.OpenAsync
            Using command As New SqlCommand(commandText, connection)
                Return command.ExecuteNonQuery()  ' Should use command.ExecuteNonQueryAsync
            End Using
        End Using
    End Function
    
    ' Violation: Async method calling sync SqlCommand.ExecuteScalar instead of ExecuteScalarAsync
    Public Async Function ExecuteScalarAsync(query As String) As Task(Of Object)
        Using connection As New SqlConnection(_connectionString)
            connection.Open()  ' Should use connection.OpenAsync
            Using command As New SqlCommand(query, connection)
                Return command.ExecuteScalar()  ' Should use command.ExecuteScalarAsync
            End Using
        End Using
    End Function
    
    ' Violation: Async method calling sync File operations
    Public Async Function ProcessFilesAsync(sourceDir As String, targetDir As String) As Task
        Dim files() As String = Directory.GetFiles(sourceDir)  ' Should use Directory.GetFilesAsync if available
        
        For Each filePath In files
            Dim content As String = File.ReadAllText(filePath)  ' Should use File.ReadAllTextAsync
            Dim newPath As String = Path.Combine(targetDir, Path.GetFileName(filePath))
            File.WriteAllText(newPath, content)  ' Should use File.WriteAllTextAsync
        Next
    End Function
    
    ' Violation: Async method with multiple sync calls
    Public Async Function ProcessDataAsync(inputFile As String, outputFile As String) As Task
        ' Should use File.ReadAllTextAsync
        Dim data As String = File.ReadAllText(inputFile)
        
        ' Process data
        Dim processedData As String = data.ToUpper()
        
        ' Should use File.WriteAllTextAsync
        File.WriteAllText(outputFile, processedData)
    End Function
    
    ' Violation: Async method calling sync network operations
    Public Async Function DownloadContentAsync(url As String) As Task(Of String)
        Dim request As New HttpRequestMessage(HttpMethod.Get, url)
        Dim response As HttpResponseMessage = _httpClient.Send(request)  ' Should use SendAsync
        Return response.Content.ReadAsString()  ' Should use ReadAsStringAsync
    End Function
    
    ' Violation: Async method with sync database operations in loop
    Public Async Function ProcessRecordsAsync(records As List(Of String)) As Task
        Using connection As New SqlConnection(_connectionString)
            connection.Open()  ' Should use OpenAsync
            
            For Each record In records
                Using command As New SqlCommand($"INSERT INTO Records (Data) VALUES ('{record}')", connection)
                    command.ExecuteNonQuery()  ' Should use ExecuteNonQueryAsync
                End Using
            Next
        End Using
    End Function
    
    ' Violation: Async method calling sync file copy
    Public Async Function BackupFileAsync(sourcePath As String, backupPath As String) As Task
        File.Copy(sourcePath, backupPath)  ' Should use async file operations
    End Function
    
    ' Violation: Async method with sync stream operations
    Public Async Function CopyStreamAsync(source As Stream, destination As Stream) As Task
        Dim buffer(4096) As Byte
        Dim bytesRead As Integer
        
        Do
            bytesRead = source.Read(buffer, 0, buffer.Length)  ' Should use ReadAsync
            If bytesRead > 0 Then
                destination.Write(buffer, 0, bytesRead)  ' Should use WriteAsync
            End If
        Loop While bytesRead > 0
    End Function
    
    ' Violation: Async method calling sync HttpClient operations
    Public Async Function PostDataAsync(url As String, data As String) As Task(Of String)
        Dim content As New StringContent(data)
        Dim response As HttpResponseMessage = _httpClient.Post(url, content)  ' Should use PostAsync
        Return response.Content.ReadAsString()  ' Should use ReadAsStringAsync
    End Function
    
    ' Valid examples (for reference)
    Public Async Function ReadFileContentCorrectAsync(filePath As String) As Task(Of String)
        Return Await File.ReadAllTextAsync(filePath)
    End Function
    
    Public Async Function WriteFileContentCorrectAsync(filePath As String, content As String) As Task
        Await File.WriteAllTextAsync(filePath, content)
    End Function
    
    Public Async Function SendHttpRequestCorrectAsync(request As HttpRequestMessage) As Task(Of HttpResponseMessage)
        Return Await _httpClient.SendAsync(request)
    End Function
End Class
