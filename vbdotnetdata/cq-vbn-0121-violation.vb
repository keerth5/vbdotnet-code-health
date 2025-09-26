' VB.NET test file for cq-vbn-0121: Use Environment.ProcessId instead of Process.GetCurrentProcess().Id
' Rule: Environment.ProcessId is simpler and faster than Process.GetCurrentProcess().Id.

Imports System
Imports System.Diagnostics

Public Class ProcessIdExamples
    
    Public Sub TestProcessId()
        
        ' Violation: Using Process.GetCurrentProcess().Id instead of Environment.ProcessId
        Dim currentProcessId = Process.GetCurrentProcess().Id
        
        ' Violation: Using Process.GetCurrentProcess().Id in assignment
        Dim processId As Integer = Process.GetCurrentProcess().Id
        
        ' Violation: Using Process.GetCurrentProcess().Id in method parameter
        LogProcessId(Process.GetCurrentProcess().Id)
        
        ' Violation: Using Process.GetCurrentProcess().Id in string interpolation
        Console.WriteLine($"Current process ID: {Process.GetCurrentProcess().Id}")
        
        ' Violation: Using Process.GetCurrentProcess().Id in string concatenation
        Dim message = "Process ID: " & Process.GetCurrentProcess().Id.ToString()
        
        ' Violation: Using Process.GetCurrentProcess().Id in conditional
        If Process.GetCurrentProcess().Id > 1000 Then
            Console.WriteLine("Process ID is greater than 1000")
        End If
        
        ' Violation: Using Process.GetCurrentProcess().Id in return statement
        Return Process.GetCurrentProcess().Id
        
        ' Violation: Using Process.GetCurrentProcess().Id in calculation
        Dim calculatedValue = Process.GetCurrentProcess().Id * 2
        
        ' Violation: Using Process.GetCurrentProcess().Id in comparison
        Dim otherProcessId As Integer = 1234
        If Process.GetCurrentProcess().Id = otherProcessId Then
            Console.WriteLine("Process IDs match")
        End If
        
        ' Violation: Using Process.GetCurrentProcess().Id in array/collection
        Dim processIds As Integer() = {Process.GetCurrentProcess().Id, 1001, 1002}
        
        Console.WriteLine($"Current process ID: {currentProcessId}")
        Console.WriteLine(message)
    End Sub
    
    Public Function GetCurrentProcessId() As Integer
        ' Violation: Using Process.GetCurrentProcess().Id in function return
        Return Process.GetCurrentProcess().Id
    End Function
    
    Public Sub CompareProcessIds()
        Dim targetProcessId As Integer = 5678
        
        ' Violation: Using Process.GetCurrentProcess().Id in comparison
        If Process.GetCurrentProcess().Id <> targetProcessId Then
            Console.WriteLine("Different process IDs")
        End If
        
        ' Violation: Using Process.GetCurrentProcess().Id in complex condition
        If Process.GetCurrentProcess().Id > 1000 AndAlso Process.GetCurrentProcess().Id < 10000 Then
            Console.WriteLine("Process ID is in expected range")
        End If
    End Sub
    
    Public Sub LogProcessInformation()
        ' Violation: Using Process.GetCurrentProcess().Id in logging
        Console.WriteLine($"[{DateTime.Now}] Process {Process.GetCurrentProcess().Id} started")
        
        ' Violation: Using Process.GetCurrentProcess().Id with other process information
        Dim processName = Process.GetCurrentProcess().ProcessName
        Console.WriteLine($"Process: {processName} (ID: {Process.GetCurrentProcess().Id})")
    End Sub
    
    Public Sub CreateProcessReport()
        ' Violation: Using Process.GetCurrentProcess().Id in report generation
        Dim report As String = $"Process Report" & vbCrLf &
                              $"Process ID: {Process.GetCurrentProcess().Id}" & vbCrLf &
                              $"Generated at: {DateTime.Now}"
        
        Console.WriteLine(report)
    End Sub
    
    Public Sub StoreProcessId()
        ' Violation: Using Process.GetCurrentProcess().Id for storage
        Dim processInfo As New Dictionary(Of String, Object) From {
            {"ProcessId", Process.GetCurrentProcess().Id},
            {"StartTime", DateTime.Now}
        }
        
        SaveProcessInfo(processInfo)
    End Sub
    
    Public Sub MonitorProcess()
        Dim startTime As DateTime = DateTime.Now
        
        ' Violation: Using Process.GetCurrentProcess().Id in monitoring
        Console.WriteLine($"Monitoring process {Process.GetCurrentProcess().Id}")
        
        ' Simulate some work
        Threading.Thread.Sleep(1000)
        
        Dim endTime As DateTime = DateTime.Now
        Console.WriteLine($"Process {Process.GetCurrentProcess().Id} completed in {(endTime - startTime).TotalMilliseconds} ms")
    End Sub
    
    Public Sub ValidateProcessId()
        ' Violation: Using Process.GetCurrentProcess().Id in validation
        Dim currentId = Process.GetCurrentProcess().Id
        
        If currentId <= 0 Then
            Throw New InvalidOperationException($"Invalid process ID: {currentId}")
        End If
        
        Console.WriteLine($"Process ID {currentId} is valid")
    End Sub
    
    Public Sub ProcessIdInLoop()
        ' Violation: Using Process.GetCurrentProcess().Id in loop (inefficient)
        For i As Integer = 1 To 5
            Console.WriteLine($"Iteration {i}: Process ID {Process.GetCurrentProcess().Id}")
            Threading.Thread.Sleep(100)
        Next
    End Sub
    
    Public Sub ProcessIdInException()
        Try
            ' Some operation that might fail
            Throw New Exception("Sample exception")
        Catch ex As Exception
            ' Violation: Using Process.GetCurrentProcess().Id in exception handling
            Console.WriteLine($"Exception in process {Process.GetCurrentProcess().Id}: {ex.Message}")
        End Try
    End Sub
    
    Private Sub LogProcessId(processId As Integer)
        Console.WriteLine($"Logged process ID: {processId}")
    End Sub
    
    Private Sub SaveProcessInfo(info As Dictionary(Of String, Object))
        Console.WriteLine("Process information saved")
        For Each kvp In info
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}")
        Next
    End Sub
    
End Class

' More violation examples in different contexts

Public Class ProcessTrackingExample
    
    Public Sub TrackProcessLifecycle()
        ' Violation: Using Process.GetCurrentProcess().Id for tracking
        Dim processId = Process.GetCurrentProcess().Id
        
        Console.WriteLine($"Process {processId} lifecycle started")
        
        ' Simulate process work
        DoWork()
        
        Console.WriteLine($"Process {processId} lifecycle completed")
    End Sub
    
    Public Function CreateProcessIdentifier() As String
        ' Violation: Using Process.GetCurrentProcess().Id for identifier creation
        Return $"PROC_{Process.GetCurrentProcess().Id}_{DateTime.Now:yyyyMMdd_HHmmss}"
    End Function
    
    Public Sub RegisterProcess()
        ' Violation: Using Process.GetCurrentProcess().Id for registration
        Dim registration As New ProcessRegistration With {
            .ProcessId = Process.GetCurrentProcess().Id,
            .StartTime = DateTime.Now,
            .MachineName = Environment.MachineName
        }
        
        RegisterProcessInSystem(registration)
    End Sub
    
    Private Sub DoWork()
        Threading.Thread.Sleep(500)
    End Sub
    
    Private Sub RegisterProcessInSystem(registration As ProcessRegistration)
        Console.WriteLine($"Registered process: {registration.ProcessId}")
    End Sub
    
End Class

Public Class ProcessRegistration
    Public Property ProcessId As Integer
    Public Property StartTime As DateTime
    Public Property MachineName As String
End Class

Public Class DiagnosticsExample
    
    Public Sub CollectDiagnostics()
        ' Violation: Using Process.GetCurrentProcess().Id in diagnostics
        Dim diagnostics As New Dictionary(Of String, Object) From {
            {"ProcessId", Process.GetCurrentProcess().Id},
            {"WorkingSet", Process.GetCurrentProcess().WorkingSet64},
            {"ThreadCount", Process.GetCurrentProcess().Threads.Count}
        }
        
        OutputDiagnostics(diagnostics)
    End Sub
    
    Public Sub CreatePerformanceCounter()
        ' Violation: Using Process.GetCurrentProcess().Id for performance counter
        Dim processIdString = Process.GetCurrentProcess().Id.ToString()
        Console.WriteLine($"Creating performance counter for process {processIdString}")
    End Sub
    
    Private Sub OutputDiagnostics(diagnostics As Dictionary(Of String, Object))
        Console.WriteLine("System Diagnostics:")
        For Each kvp In diagnostics
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}")
        Next
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperProcessUsageExamples
    
    Public Sub TestProperUsage()
        
        ' Correct: Using Environment.ProcessId - should not be detected
        Dim currentProcessId = Environment.ProcessId
        
        ' Correct: Using other Process properties - should not be detected
        Dim processName = Process.GetCurrentProcess().ProcessName
        Dim startTime = Process.GetCurrentProcess().StartTime
        Dim workingSet = Process.GetCurrentProcess().WorkingSet64
        
        ' Correct: Using Process.GetCurrentProcess() for other purposes - should not be detected
        Dim currentProcess = Process.GetCurrentProcess()
        Console.WriteLine($"Process name: {currentProcess.ProcessName}")
        
        ' Correct: Using other process methods - should not be detected
        Dim processes = Process.GetProcesses()
        For Each proc In processes
            Console.WriteLine($"Process: {proc.ProcessName} (ID: {proc.Id})")
        Next
        
        Console.WriteLine($"Current process ID: {currentProcessId}")
        Console.WriteLine($"Process name: {processName}")
    End Sub
    
End Class
