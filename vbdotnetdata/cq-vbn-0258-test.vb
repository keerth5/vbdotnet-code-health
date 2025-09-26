' Test file for cq-vbn-0258: Do not raise reserved exception types
' Rule should detect: Throwing reserved system exception types

Public Class ReservedException Test
    
    ' VIOLATION: Throwing base Exception
    Public Sub BadException1()
        Throw New Exception("Generic exception")
    End Sub
    
    ' VIOLATION: Throwing SystemException
    Public Sub BadException2()
        Throw New SystemException("System exception")
    End Sub
    
    ' VIOLATION: Throwing ApplicationException
    Public Sub BadException3()
        Throw New ApplicationException("Application exception")
    End Sub
    
    ' VIOLATION: Throwing ExecutionEngineException
    Public Sub BadException4()
        Throw New ExecutionEngineException("Execution engine exception")
    End Sub
    
    ' VIOLATION: Throwing StackOverflowException
    Public Sub BadException5()
        Throw New StackOverflowException("Stack overflow")
    End Sub
    
    ' VIOLATION: Throwing OutOfMemoryException
    Public Sub BadException6()
        Throw New OutOfMemoryException("Out of memory")
    End Sub
    
    ' VIOLATION: Throwing ComException
    Public Sub BadException7()
        Throw New Runtime.InteropServices.ComException("COM exception")
    End Sub
    
    ' VIOLATION: Throwing SEHException
    Public Sub BadException8()
        Throw New Runtime.InteropServices.SEHException("SEH exception")
    End Sub
    
    ' GOOD: Throwing specific exception types - should NOT be flagged
    Public Sub GoodException1()
        Throw New ArgumentNullException("paramName", "Parameter cannot be null")
    End Sub
    
    Public Sub GoodException2()
        Throw New InvalidOperationException("Invalid operation")
    End Sub
    
    Public Sub GoodException3()
        Throw New NotSupportedException("Operation not supported")
    End Sub
    
    Public Sub GoodException4()
        Throw New FileNotFoundException("File not found")
    End Sub
    
    ' GOOD: Custom exception - should NOT be flagged
    Public Sub GoodException5()
        Throw New CustomBusinessException("Business rule violation")
    End Sub
    
End Class

Public Class CustomBusinessException
    Inherits Exception
    
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    
End Class
