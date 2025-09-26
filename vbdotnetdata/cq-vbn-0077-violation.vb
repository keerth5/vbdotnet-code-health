' Test file for cq-vbn-0077: Use ObjectDisposedException throw helper
' Rule should detect manual disposed checks that could use ObjectDisposedException.ThrowIfDisposed

Imports System

Public Class ObjectDisposedHelperExamples
    Implements IDisposable
    
    Private _disposed As Boolean = False
    Private _isDisposed As Boolean = False
    Private _hasBeenDisposed As Boolean = False
    
    Public Sub DoWork()
        ' Violation 1: Manual disposed check with ObjectDisposedException
        If _disposed Then
            Throw New ObjectDisposedException(Me.GetType().Name)
        End If
        
        Console.WriteLine("Doing work")
    End Sub
    
    Public Sub ProcessData()
        ' Violation 2: Manual disposed check with different variable name
        If _isDisposed Then
            Throw New ObjectDisposedException("ProcessData")
        End If
        
        Console.WriteLine("Processing data")
    End Sub
    
    Public Function GetValue() As Integer
        ' Violation 3: Manual disposed check in function
        If _hasBeenDisposed Then
            Throw New ObjectDisposedException("GetValue")
        End If
        
        Return 42
    End Function
    
    ' This should NOT be detected - using modern throw helper (if available)
    Public Sub ModernDisposedCheck()
        ' ObjectDisposedException.ThrowIfDisposed(_disposed, Me) ' Modern approach
        If Not _disposed Then
            Console.WriteLine("Not disposed yet")
        End If
    End Sub
    
    Public Sub ExecuteOperation()
        ' Violation 4: Another disposed check pattern
        If _disposed Then
            Throw New ObjectDisposedException("ExecuteOperation")
        End If
        
        ' Operation logic
        Console.WriteLine("Executing operation")
    End Sub
    
    ' This should NOT be detected - different exception type
    Public Sub DifferentException()
        If _disposed Then
            Throw New InvalidOperationException("Object is disposed")
        End If
    End Sub
    
    Public Sub ReadData()
        ' Violation 5: Disposed check for read operation
        If _isDisposed Then
            Throw New ObjectDisposedException("ReadData")
        End If
        
        Console.WriteLine("Reading data")
    End Sub
    
    Public Sub WriteData(data As String)
        ' Violation 6: Disposed check for write operation
        If _disposed Then
            Throw New ObjectDisposedException("WriteData")
        End If
        
        Console.WriteLine("Writing: " & data)
    End Sub
    
    ' This should NOT be detected - different condition
    Public Sub DifferentCondition()
        If Not _disposed Then
            Console.WriteLine("Still active")
        End If
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Dispose managed resources
            End If
            _disposed = True
        End If
    End Sub
    
End Class

Public Class AnotherDisposableClass
    Implements IDisposable
    
    Private _objectDisposed As Boolean = False
    Private _disposed As Boolean = False
    
    Public Sub PerformTask()
        ' Violation 7: Object disposed check
        If _objectDisposed Then
            Throw New ObjectDisposedException("PerformTask")
        End If
        
        Console.WriteLine("Task performed")
    End Sub
    
    Public Sub HandleRequest()
        ' Violation 8: Another disposed check
        If _disposed Then
            Throw New ObjectDisposedException("HandleRequest")
        End If
        
        Console.WriteLine("Request handled")
    End Sub
    
    Public Function CalculateResult() As Double
        ' Violation 9: Disposed check in calculation function
        If _objectDisposed Then
            Throw New ObjectDisposedException("CalculateResult")
        End If
        
        Return Math.PI * 2
    End Function
    
    ' This should NOT be detected - complex condition
    Public Sub ComplexCondition()
        If _disposed AndAlso DateTime.Now.Hour > 12 Then
            Throw New InvalidOperationException("Complex condition")
        End If
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        _disposed = True
        _objectDisposed = True
    End Sub
    
End Class

Public Class ThirdDisposableExample
    Implements IDisposable
    
    Private _wasDisposed As Boolean = False
    
    Public Sub StartProcess()
        ' Violation 10: Was disposed check
        If _wasDisposed Then
            Throw New ObjectDisposedException("StartProcess")
        End If
        
        Console.WriteLine("Process started")
    End Sub
    
    Public Sub StopProcess()
        ' Violation 11: Another disposed check
        If _wasDisposed Then
            Throw New ObjectDisposedException("StopProcess")
        End If
        
        Console.WriteLine("Process stopped")
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        _wasDisposed = True
    End Sub
    
End Class
