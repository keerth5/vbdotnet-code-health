' VB.NET test file for cq-vbn-0123: Use Environment.ProcessPath instead of Process.GetCurrentProcess().MainModule.FileName
' Rule: Environment.ProcessPath is simpler and faster than Process.GetCurrentProcess().MainModule.FileName.

Imports System
Imports System.Diagnostics

Public Class ProcessPathExamples
    
    Public Sub TestProcessPath()
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName instead of Environment.ProcessPath
        Dim currentProcessPath = Process.GetCurrentProcess().MainModule.FileName
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName in assignment
        Dim processPath As String = Process.GetCurrentProcess().MainModule.FileName
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName in method parameter
        LogProcessPath(Process.GetCurrentProcess().MainModule.FileName)
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName in string interpolation
        Console.WriteLine($"Current process path: {Process.GetCurrentProcess().MainModule.FileName}")
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName in conditional
        If Process.GetCurrentProcess().MainModule.FileName.Contains("MyApp") Then
            Console.WriteLine("Running MyApp")
        End If
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName in return statement
        Return Process.GetCurrentProcess().MainModule.FileName
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName with Path operations
        Dim directory = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)
        
        Console.WriteLine($"Process path: {currentProcessPath}")
        Console.WriteLine($"Process directory: {directory}")
    End Sub
    
    Public Function GetCurrentProcessPath() As String
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName in function return
        Return Process.GetCurrentProcess().MainModule.FileName
    End Function
    
    Public Sub AnalyzeProcessLocation()
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName for analysis
        Dim processPath = Process.GetCurrentProcess().MainModule.FileName
        Dim fileName = System.IO.Path.GetFileName(processPath)
        Dim directory = System.IO.Path.GetDirectoryName(processPath)
        
        Console.WriteLine($"Process file: {fileName}")
        Console.WriteLine($"Process directory: {directory}")
    End Sub
    
    Public Sub LogProcessInformation()
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName in logging
        Console.WriteLine($"[{DateTime.Now}] Process started from: {Process.GetCurrentProcess().MainModule.FileName}")
        
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName with other information
        Dim processName = Process.GetCurrentProcess().ProcessName
        Console.WriteLine($"Process: {processName} at {Process.GetCurrentProcess().MainModule.FileName}")
    End Sub
    
    Private Sub LogProcessPath(path As String)
        Console.WriteLine($"Logged process path: {path}")
    End Sub
    
End Class

' More violation examples

Public Class ApplicationInfoExample
    
    Public Sub GetApplicationInfo()
        ' Violation: Using Process.GetCurrentProcess().MainModule.FileName for app info
        Dim appPath = Process.GetCurrentProcess().MainModule.FileName
        Dim appName = System.IO.Path.GetFileNameWithoutExtension(appPath)
        
        Console.WriteLine($"Application: {appName}")
        Console.WriteLine($"Location: {appPath}")
    End Sub
    
End Class

' Non-violation examples (these should not be detected):

Public Class ProperProcessUsageExamples
    
    Public Sub TestProperUsage()
        
        ' Correct: Using Environment.ProcessPath - should not be detected
        Dim currentProcessPath = Environment.ProcessPath
        
        ' Correct: Using other Process properties - should not be detected
        Dim processName = Process.GetCurrentProcess().ProcessName
        Dim processId = Process.GetCurrentProcess().Id
        
        ' Correct: Using Process.GetCurrentProcess() for other purposes - should not be detected
        Dim currentProcess = Process.GetCurrentProcess()
        Console.WriteLine($"Process name: {currentProcess.ProcessName}")
        
        Console.WriteLine($"Current process path: {currentProcessPath}")
    End Sub
    
End Class
