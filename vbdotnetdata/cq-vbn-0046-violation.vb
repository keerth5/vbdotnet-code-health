' Test file for cq-vbn-0046: CancellationToken parameters must come last
' Rule should detect methods where CancellationToken is not the last parameter

Imports System
Imports System.Threading

Public Class AsyncOperationExamples
    
    ' Violation 1: Public method with CancellationToken not as last parameter
    Public Sub ProcessDataAsync(cancellationToken As CancellationToken, data As String)
        Console.WriteLine($"Processing: {data}")
        cancellationToken.ThrowIfCancellationRequested()
    End Sub
    
    ' Violation 2: Protected method with CancellationToken in middle
    Protected Function DownloadFileAsync(cancellationToken As CancellationToken, url As String, timeout As Integer) As Task(Of String)
        cancellationToken.ThrowIfCancellationRequested()
        Return Task.FromResult($"Downloaded from {url}")
    End Function
    
    ' Violation 3: Friend method with CancellationToken not last
    Friend Sub SaveToDatabase(cancellationToken As CancellationToken, connectionString As String)
        Console.WriteLine($"Saving to: {connectionString}")
        cancellationToken.ThrowIfCancellationRequested()
    End Sub
    
    ' Violation 4: Public function with multiple parameters after CancellationToken
    Public Function CalculateAsync(cancellationToken As CancellationToken, x As Integer, y As Integer) As Task(Of Integer)
        cancellationToken.ThrowIfCancellationRequested()
        Return Task.FromResult(x + y)
    End Function
    
    ' Violation 5: Method with CancellationToken followed by optional parameter
    Public Sub ProcessWithOptions(cancellationToken As CancellationToken, Optional verbose As Boolean = False)
        If verbose Then
            Console.WriteLine("Verbose processing enabled")
        End If
        cancellationToken.ThrowIfCancellationRequested()
    End Sub
    
    ' This should NOT be detected - CancellationToken as last parameter (correct)
    Public Sub ProcessDataCorrectly(data As String, cancellationToken As CancellationToken)
        Console.WriteLine($"Processing: {data}")
        cancellationToken.ThrowIfCancellationRequested()
    End Sub
    
    ' This should NOT be detected - CancellationToken as only parameter
    Public Function GetStatusAsync(cancellationToken As CancellationToken) As Task(Of String)
        cancellationToken.ThrowIfCancellationRequested()
        Return Task.FromResult("Ready")
    End Function
    
    ' This should NOT be detected - method without CancellationToken
    Public Sub ProcessSynchronously(data As String, options As Integer)
        Console.WriteLine($"Processing synchronously: {data} with options {options}")
    End Sub
    
    ' This should NOT be detected - private method (less critical)
    Private Sub InternalProcess(cancellationToken As CancellationToken, data As String)
        Console.WriteLine($"Internal processing: {data}")
    End Sub
    
    ' This should NOT be detected - CancellationToken as last parameter with multiple params
    Public Function ComplexOperationAsync(url As String, timeout As Integer, retryCount As Integer, cancellationToken As CancellationToken) As Task(Of Boolean)
        cancellationToken.ThrowIfCancellationRequested()
        Return Task.FromResult(True)
    End Function
    
End Class

Public Class WebServiceClient
    
    ' Violation 6: Another class with CancellationToken parameter order issue
    Public Function CallApiAsync(cancellationToken As CancellationToken, endpoint As String, headers As Dictionary(Of String, String)) As Task(Of String)
        cancellationToken.ThrowIfCancellationRequested()
        Return Task.FromResult($"Called {endpoint}")
    End Function
    
    ' This should NOT be detected - proper parameter order
    Public Function CallApiCorrectly(endpoint As String, headers As Dictionary(Of String, String), cancellationToken As CancellationToken) As Task(Of String)
        cancellationToken.ThrowIfCancellationRequested()
        Return Task.FromResult($"Called {endpoint}")
    End Function
    
End Class
