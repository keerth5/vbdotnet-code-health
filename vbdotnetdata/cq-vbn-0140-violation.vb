' VB.NET test file for cq-vbn-0140: Incorrect usage of ConstantExpected attribute
' Rule: The ConstantExpectedAttribute attribute is not applied correctly on a parameter.

Imports System
Imports System.Diagnostics.CodeAnalysis

Public Class ConstantExpectedExamples
    
    ' Violation: ConstantExpected on parameter that should be constant but isn't used as such
    Public Sub ProcessMessage(<ConstantExpected> messageType As String, content As String)
        ' The messageType parameter should be a constant value but method doesn't enforce it
        Console.WriteLine($"{messageType}: {content}")
    End Sub
    
    ' Violation: ConstantExpected on parameter where constant is not actually expected
    Public Sub LogLevel(<ConstantExpected> level As Integer)
        ' Integer level might not need to be constant - could be dynamic
        Console.WriteLine($"Logging at level: {level}")
    End Sub
    
    ' Violation: ConstantExpected on parameter that changes within method
    Public Sub ConfigureOption(<ConstantExpected> optionName As String, value As String)
        ' The optionName is expected to be constant but method might modify it
        Dim processedName As String = optionName.ToLower()
        Console.WriteLine($"Setting {processedName} to {value}")
    End Sub
    
    ' Violation: ConstantExpected on numeric parameter without proper validation
    Public Sub SetTimeout(<ConstantExpected> timeoutSeconds As Integer)
        ' Should validate that this is a reasonable constant value
        If timeoutSeconds <= 0 Then
            timeoutSeconds = 30  ' Modifying the parameter defeats the purpose
        End If
        Console.WriteLine($"Timeout set to {timeoutSeconds} seconds")
    End Sub
    
    ' Violation: ConstantExpected on parameter that's used in dynamic context
    Public Sub ExecuteCommand(<ConstantExpected> commandName As String, args() As String)
        ' Command name is expected to be constant but used dynamically
        Dim fullCommand As String = commandName + " " + String.Join(" ", args)
        Console.WriteLine($"Executing: {fullCommand}")
    End Sub
    
    ' Violation: ConstantExpected on optional parameter with non-constant default
    Public Sub InitializeService(<ConstantExpected> Optional serviceName As String = GetDefaultServiceName())
        Console.WriteLine($"Initializing service: {serviceName}")
    End Sub
    
    Private Function GetDefaultServiceName() As String
        Return "DefaultService"
    End Function
    
    ' Violation: ConstantExpected on parameter that's passed to non-constant expecting method
    Public Sub CallExternalMethod(<ConstantExpected> methodName As String)
        ' The parameter is expected to be constant but passed to dynamic method
        Dim result As String = InvokeMethod(methodName)
        Console.WriteLine(result)
    End Sub
    
    Private Function InvokeMethod(name As String) As String
        ' This method doesn't expect a constant
        Return $"Invoked {name}"
    End Function
    
    ' Violation: ConstantExpected on parameter used in string interpolation without validation
    Public Sub FormatMessage(<ConstantExpected> template As String, ParamArray values() As Object)
        ' Template should be constant but no validation of format
        Dim message As String = String.Format(template, values)
        Console.WriteLine(message)
    End Sub
    
    ' Violation: ConstantExpected on parameter that's stored in non-constant field
    Private _currentMode As String
    
    Public Sub SetMode(<ConstantExpected> mode As String)
        ' Mode is expected constant but stored in mutable field
        _currentMode = mode
        Console.WriteLine($"Mode set to: {mode}")
    End Sub
    
    ' Violation: ConstantExpected on parameter used in conditional logic
    Public Sub ProcessByType(<ConstantExpected> objectType As String, data As Object)
        ' Type parameter expected constant but used in dynamic conditional
        Select Case objectType.ToLower()  ' ToLower() suggests it's not truly constant
            Case "string"
                ProcessString(data)
            Case "number"
                ProcessNumber(data)
            Case Else
                ProcessGeneric(data)
        End Select
    End Sub
    
    Private Sub ProcessString(data As Object)
        Console.WriteLine($"Processing string: {data}")
    End Sub
    
    Private Sub ProcessNumber(data As Object)
        Console.WriteLine($"Processing number: {data}")
    End Sub
    
    Private Sub ProcessGeneric(data As Object)
        Console.WriteLine($"Processing generic: {data}")
    End Sub
    
    ' Violation: ConstantExpected on parameter that's modified before use
    Public Sub ValidateAndProcess(<ConstantExpected> ruleName As String, input As String)
        ' Rule name expected constant but gets modified
        ruleName = ruleName.Trim().ToUpper()  ' This modification violates constant expectation
        ApplyRule(ruleName, input)
    End Sub
    
    Private Sub ApplyRule(rule As String, input As String)
        Console.WriteLine($"Applying rule {rule} to {input}")
    End Sub
    
    ' Violation: ConstantExpected on parameter in method that doesn't actually need constants
    Public Sub PrintValue(<ConstantExpected> label As String, value As Object)
        ' Simple printing doesn't require constant label
        Console.WriteLine($"{label}: {value}")
    End Sub
    
    ' Violation: ConstantExpected on parameter passed to collection
    Private _registeredHandlers As New List(Of String)()
    
    Public Sub RegisterHandler(<ConstantExpected> handlerType As String)
        ' Handler type expected constant but added to dynamic collection
        _registeredHandlers.Add(handlerType)
        Console.WriteLine($"Registered handler: {handlerType}")
    End Sub
    
    ' Violation: ConstantExpected on parameter used in calculation
    Public Function CalculateScore(<ConstantExpected> category As String, baseScore As Integer) As Integer
        ' Category expected constant but used in hash calculation (dynamic behavior)
        Dim categoryHash As Integer = category.GetHashCode()
        Return baseScore + (categoryHash Mod 100)
    End Function
    
    ' Violation: ConstantExpected on parameter that gets concatenated dynamically
    Public Sub BuildConnectionString(<ConstantExpected> provider As String, server As String, database As String)
        ' Provider expected constant but used in dynamic string building
        Dim connectionString As String = $"Provider={provider};Server={server};Database={database}"
        Console.WriteLine(connectionString)
    End Sub
    
    ' Violation: ConstantExpected on parameter used with reflection
    Public Sub InvokeByName(<ConstantExpected> methodName As String, target As Object)
        ' Method name expected constant but used with reflection (inherently dynamic)
        Dim methodInfo = target.GetType().GetMethod(methodName)
        If methodInfo IsNot Nothing Then
            methodInfo.Invoke(target, Nothing)
        End If
    End Sub
    
    ' Violation: ConstantExpected on parameter in generic method
    Public Sub ProcessItem(Of T)(<ConstantExpected> itemType As String, item As T)
        ' Item type expected constant but generic context is inherently dynamic
        Console.WriteLine($"Processing {itemType}: {item}")
    End Sub
    
    ' Violation: ConstantExpected on parameter that's used as dictionary key
    Private _cache As New Dictionary(Of String, Object)()
    
    Public Sub CacheValue(<ConstantExpected> key As String, value As Object)
        ' Key expected constant but dictionary operations are dynamic
        _cache(key) = value
        Console.WriteLine($"Cached value for key: {key}")
    End Sub
    
    ' Examples of potentially correct usage (for reference)
    Public Sub LogMessage(<ConstantExpected> logLevel As String, message As String)
        ' This might be correct if logLevel should always be a constant like "INFO", "ERROR", etc.
        ' But the implementation should validate this expectation
        If logLevel <> "INFO" AndAlso logLevel <> "WARN" AndAlso logLevel <> "ERROR" Then
            Throw New ArgumentException("Invalid log level", NameOf(logLevel))
        End If
        Console.WriteLine($"[{logLevel}] {message}")
    End Sub
    
    Public Sub SetFeatureFlag(<ConstantExpected> featureName As String, enabled As Boolean)
        ' This could be correct if feature names should be compile-time constants
        ' Implementation should validate known feature names
        Const FEATURE_A As String = "FeatureA"
        Const FEATURE_B As String = "FeatureB"
        
        If featureName <> FEATURE_A AndAlso featureName <> FEATURE_B Then
            Throw New ArgumentException("Unknown feature name", NameOf(featureName))
        End If
        
        Console.WriteLine($"Feature {featureName} set to {enabled}")
    End Sub
    
    ' Violation: ConstantExpected on parameter in async method
    Public Async Function ProcessAsync(<ConstantExpected> operationType As String, data As String) As Task
        ' Operation type expected constant but async context might be dynamic
        Await Task.Delay(100)  ' Simulate async work
        Console.WriteLine($"Async processing {operationType}: {data}")
    End Function
    
    ' Violation: ConstantExpected on parameter that's used in LINQ
    Public Sub FilterItems(<ConstantExpected> filterType As String, items As List(Of String))
        ' Filter type expected constant but LINQ operations are dynamic
        Dim filtered = items.Where(Function(item) item.Contains(filterType)).ToList()
        Console.WriteLine($"Filtered {filtered.Count} items by {filterType}")
    End Sub
End Class
