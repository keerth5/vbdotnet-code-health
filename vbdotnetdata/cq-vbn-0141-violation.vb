' VB.NET test file for cq-vbn-0141: The parameter expects a constant for optimal performance
' Rule: An invalid argument is passed to a parameter that's annotated with ConstantExpectedAttribute.

Imports System
Imports System.Diagnostics.CodeAnalysis

Public Class ConstantExpectedPerformanceExamples
    
    ' Method with ConstantExpected parameter
    Public Sub ProcessWithConstant(<ConstantExpected> operation As String, data As String)
        Console.WriteLine($"{operation}: {data}")
    End Sub
    
    Public Sub ProcessWithConstantAndDefault(<ConstantExpected> Optional logLevel As String = "INFO", message As String = "")
        Console.WriteLine($"[{logLevel}] {message}")
    End Sub
    
    Public Sub ConfigureFeature(<ConstantExpected> featureName As String, enabled As Boolean)
        Console.WriteLine($"Feature {featureName} is {If(enabled, "enabled", "disabled")}")
    End Sub
    
    Public Sub TestViolations()
        ' Violation: Passing variable instead of constant
        Dim operationType As String = "CREATE"
        ProcessWithConstant(operationType, "test data")
        
        ' Violation: Passing method result instead of constant
        ProcessWithConstant(GetOperationType(), "test data")
        
        ' Violation: Passing concatenated string instead of constant
        ProcessWithConstant("OP_" & "CREATE", "test data")
        
        ' Violation: Passing variable to constant expected parameter
        Dim level As String = "DEBUG"
        ProcessWithConstantAndDefault(level, "Debug message")
        
        ' Violation: Passing computed value
        Dim feature As String = "Feature" & "A"
        ConfigureFeature(feature, True)
        
        ' Violation: Passing parameter from another method
        ProcessData("PROCESS")
        
        ' Violation: Using variable in loop
        For i As Integer = 1 To 3
            Dim opName As String = "OP" & i.ToString()
            ProcessWithConstant(opName, "data")
        Next
        
        ' Violation: Passing field value
        ProcessWithConstant(_operationName, "field data")
        
        ' Violation: Passing property value
        ProcessWithConstant(CurrentOperation, "property data")
        
        ' Violation: Passing array element
        Dim operations() As String = {"READ", "WRITE", "DELETE"}
        ProcessWithConstant(operations(0), "array data")
        
        ' Violation: Passing result of conditional expression
        Dim isDebug As Boolean = True
        ProcessWithConstantAndDefault(If(isDebug, "DEBUG", "INFO"), "conditional message")
        
        ' Violation: Passing interpolated string
        Dim prefix As String = "LOG"
        ProcessWithConstantAndDefault($"{prefix}_LEVEL", "interpolated message")
    End Sub
    
    Private _operationName As String = "FIELD_OP"
    
    Private ReadOnly Property CurrentOperation As String
        Get
            Return "PROPERTY_OP"
        End Get
    End Property
    
    Private Function GetOperationType() As String
        Return "METHOD_OP"
    End Function
    
    Private Sub ProcessData(operationType As String)
        ' This method calls another method with ConstantExpected parameter
        ProcessWithConstant(operationType, "processed data")
    End Sub
    
    Public Sub TestMoreViolations()
        ' Violation: Using user input
        Dim userInput As String = Console.ReadLine()
        ProcessWithConstant(userInput, "user data")
        
        ' Violation: Using DateTime formatting
        Dim timeStamp As String = DateTime.Now.ToString("yyyy-MM-dd")
        ProcessWithConstant(timeStamp, "time data")
        
        ' Violation: Using GUID
        Dim id As String = Guid.NewGuid().ToString()
        ProcessWithConstant(id, "guid data")
        
        ' Violation: Using environment variable
        Dim envVar As String = Environment.GetEnvironmentVariable("PATH")
        ProcessWithConstant(envVar, "env data")
        
        ' Violation: Using random value
        Dim random As New Random()
        Dim randomOp As String = "OP_" & random.Next(1, 100).ToString()
        ProcessWithConstant(randomOp, "random data")
    End Sub
    
    Public Sub TestConditionalViolations(condition As Boolean)
        ' Violation: Variable assignment based on condition
        Dim operation As String
        If condition Then
            operation = "TRUE_OP"
        Else
            operation = "FALSE_OP"
        End If
        ProcessWithConstant(operation, "conditional data")
        
        ' Violation: Using Select Case result
        Dim mode As Integer = 1
        Dim modeString As String
        Select Case mode
            Case 1
                modeString = "MODE_ONE"
            Case 2
                modeString = "MODE_TWO"
            Case Else
                modeString = "MODE_OTHER"
        End Select
        ProcessWithConstant(modeString, "select case data")
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        ' Correct: Using string literal constant
        ProcessWithConstant("LITERAL_OP", "literal data")
        
        ' Correct: Using const field
        ProcessWithConstant(CONSTANT_OPERATION, "const data")
        
        ' Correct: Using default parameter
        ProcessWithConstantAndDefault(message:="default level message")
    End Sub
    
    Private Const CONSTANT_OPERATION As String = "CONST_OP"
    
    ' Test method with multiple ConstantExpected parameters
    Public Sub ProcessMultipleConstants(<ConstantExpected> category As String, <ConstantExpected> action As String, data As Object)
        Console.WriteLine($"{category}.{action}: {data}")
    End Sub
    
    Public Sub TestMultipleConstantViolations()
        ' Violation: Both parameters are non-constant
        Dim cat As String = "CATEGORY"
        Dim act As String = "ACTION"
        ProcessMultipleConstants(cat, act, "data")
        
        ' Violation: One constant, one variable
        Dim dynamicAction As String = GetDynamicAction()
        ProcessMultipleConstants("STATIC_CAT", dynamicAction, "mixed data")
    End Sub
    
    Private Function GetDynamicAction() As String
        Return "DYNAMIC_ACTION"
    End Function
    
    ' Test with generic method having ConstantExpected
    Public Sub ProcessGeneric(Of T)(<ConstantExpected> typeName As String, value As T)
        Console.WriteLine($"Processing {typeName}: {value}")
    End Sub
    
    Public Sub TestGenericViolations()
        ' Violation: Non-constant in generic method
        Dim typeDesc As String = GetType(String).Name
        ProcessGeneric(typeDesc, "test value")
        
        ' Violation: Using reflection result
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        ProcessGeneric(methodName, 42)
    End Sub
End Class
