' Test file for cq-vbn-0078: Use InvalidOperationException throw helper
' Rule should detect manual state checks that could use InvalidOperationException.ThrowIf

Imports System

Public Class InvalidOperationHelperExamples
    
    Private _isInitialized As Boolean = False
    Private _isRunning As Boolean = False
    Private _isConnected As Boolean = False
    
    Public Sub Start()
        ' Violation 1: Manual state check with InvalidOperationException
        If _isRunning Then
            Throw New InvalidOperationException("Service is already running")
        End If
        
        _isRunning = True
        Console.WriteLine("Service started")
    End Sub
    
    Public Sub Stop()
        ' Violation 2: Manual state check with InvalidOperationException
        If Not _isRunning Then
            Throw New InvalidOperationException("Service is not running")
        End If
        
        _isRunning = False
        Console.WriteLine("Service stopped")
    End Sub
    
    Public Sub Initialize()
        ' Violation 3: Initialization state check
        If _isInitialized Then
            Throw New InvalidOperationException("Already initialized")
        End If
        
        _isInitialized = True
        Console.WriteLine("Initialized")
    End Sub
    
    ' This should NOT be detected - using modern throw helper (if available)
    Public Sub ModernStateCheck()
        ' InvalidOperationException.ThrowIf(_isRunning, "Already running") ' Modern approach
        If Not _isRunning Then
            Console.WriteLine("Ready to start")
        End If
    End Sub
    
    Public Sub Connect()
        ' Violation 4: Connection state check
        If _isConnected Then
            Throw New InvalidOperationException("Already connected")
        End If
        
        _isConnected = True
        Console.WriteLine("Connected")
    End Sub
    
    Public Sub SendData(data As String)
        ' Violation 5: Connection required check
        If Not _isConnected Then
            Throw New InvalidOperationException("Not connected")
        End If
        
        Console.WriteLine("Sending: " & data)
    End Sub
    
    ' This should NOT be detected - different exception type
    Public Sub DifferentException()
        If _isRunning Then
            Throw New ArgumentException("Service is running")
        End If
    End Sub
    
    Public Sub ProcessRequest()
        ' Violation 6: Initialization required check
        If Not _isInitialized Then
            Throw New InvalidOperationException("Not initialized")
        End If
        
        Console.WriteLine("Processing request")
    End Sub
    
    ' This should NOT be detected - complex condition
    Public Sub ComplexCondition()
        If _isRunning AndAlso _isConnected Then
            Console.WriteLine("Running and connected")
        End If
    End Sub
    
End Class

Public Class StateMachineExample
    
    Private _state As String = "Idle"
    Private _canExecute As Boolean = False
    Private _isReady As Boolean = False
    
    Public Sub Execute()
        ' Violation 7: State validation
        If Not _canExecute Then
            Throw New InvalidOperationException("Cannot execute in current state")
        End If
        
        Console.WriteLine("Executing")
    End Sub
    
    Public Sub Prepare()
        ' Violation 8: Ready state check
        If _isReady Then
            Throw New InvalidOperationException("Already prepared")
        End If
        
        _isReady = True
        Console.WriteLine("Prepared")
    End Sub
    
    Public Sub Reset()
        ' Violation 9: State reset check
        If _state = "Idle" Then
            Throw New InvalidOperationException("Already in idle state")
        End If
        
        _state = "Idle"
        Console.WriteLine("Reset to idle")
    End Sub
    
    Public Function GetResult() As String
        ' Violation 10: Ready state validation in function
        If Not _isReady Then
            Throw New InvalidOperationException("Not ready to get result")
        End If
        
        Return "Result"
    End Function
    
    ' This should NOT be detected - string comparison
    Public Sub StringComparison()
        If _state.Equals("Running") Then
            Console.WriteLine("Currently running")
        End If
    End Sub
    
End Class

Public Class WorkflowExample
    
    Private _workflowStarted As Boolean = False
    Private _workflowCompleted As Boolean = False
    
    Public Sub StartWorkflow()
        ' Violation 11: Workflow state check
        If _workflowStarted Then
            Throw New InvalidOperationException("Workflow already started")
        End If
        
        _workflowStarted = True
        Console.WriteLine("Workflow started")
    End Sub
    
    Public Sub CompleteWorkflow()
        ' Violation 12: Workflow completion check
        If _workflowCompleted Then
            Throw New InvalidOperationException("Workflow already completed")
        End If
        
        _workflowCompleted = True
        Console.WriteLine("Workflow completed")
    End Sub
    
    Public Sub ProcessStep()
        ' Violation 13: Workflow started validation
        If Not _workflowStarted Then
            Throw New InvalidOperationException("Workflow not started")
        End If
        
        Console.WriteLine("Processing step")
    End Sub
    
End Class
