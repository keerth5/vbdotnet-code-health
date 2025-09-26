' Test file for cq-vbn-0050: Do not pass literals as localized parameters
' Rule should detect literal strings passed to localization-sensitive methods

Imports System
Imports System.Diagnostics
Imports System.Windows.Forms

Public Class LocalizationExamples
    
    Public Sub ShowMessages()
        
        ' Violation 1: MessageBox.Show with literal string
        MessageBox.Show("This is a hardcoded message")
        
        ' Violation 2: MessageBox.Show with literal title and message
        MessageBox.Show("Error occurred", "Error")
        
        ' Violation 3: Console.WriteLine with literal string
        Console.WriteLine("Processing started")
        
        ' Violation 4: Debug.WriteLine with literal
        Debug.WriteLine("Debug information here")
        
        ' Violation 5: Trace.WriteLine with literal
        Trace.WriteLine("Trace message")
        
        ' This should NOT be detected - using resource strings
        MessageBox.Show(My.Resources.ErrorMessage)
        
        ' This should NOT be detected - using variables
        Dim message As String = GetLocalizedMessage()
        MessageBox.Show(message)
        
        ' This should NOT be detected - using string interpolation with variables
        Dim userName As String = "John"
        Console.WriteLine($"Welcome {userName}")
        
    End Sub
    
    Public Sub MoreExamples()
        
        ' Violation 6: Another MessageBox.Show with literal
        MessageBox.Show("File not found")
        
        ' Violation 7: Console.WriteLine with literal message
        Console.WriteLine("Operation completed successfully")
        
        ' Violation 8: Debug.WriteLine with literal debug info
        Debug.WriteLine("Entering method ProcessData")
        
        ' Violation 9: Trace.WriteLine with literal trace
        Trace.WriteLine("Performance counter updated")
        
        ' This should NOT be detected - method calls without literals
        Console.WriteLine(DateTime.Now.ToString())
        
        ' This should NOT be detected - using constants (still not ideal but pattern may not catch)
        Const WelcomeMessage As String = "Welcome"
        Console.WriteLine(WelcomeMessage)
        
    End Sub
    
    Public Sub LoggingExamples()
        
        ' Violation 10: Debug.WriteLine with literal error message
        Debug.WriteLine("Invalid configuration detected")
        
        ' Violation 11: Trace.WriteLine with literal warning
        Trace.WriteLine("Memory usage is high")
        
        ' This should NOT be detected - using formatted strings with variables
        Dim errorCode As Integer = 404
        Debug.WriteLine($"Error code: {errorCode}")
        
        ' This should NOT be detected - using resource manager
        Dim resourceManager As New System.Resources.ResourceManager("MyApp.Resources", Me.GetType().Assembly)
        Dim localizedMessage As String = resourceManager.GetString("ErrorMessage")
        MessageBox.Show(localizedMessage)
        
    End Sub
    
    Private Function GetLocalizedMessage() As String
        ' This would typically get a localized string from resources
        Return My.Resources.DefaultMessage
    End Function
    
    Public Sub ProperLocalizationExamples()
        
        ' These should NOT be detected - proper localization practices
        MessageBox.Show(My.Resources.ConfirmationMessage, My.Resources.ConfirmationTitle)
        Console.WriteLine(My.Resources.ProcessingStarted)
        Debug.WriteLine(My.Resources.DebugInfo)
        Trace.WriteLine(My.Resources.TraceMessage)
        
        ' Using resource manager properly
        Dim rm As New System.Resources.ResourceManager("Resources", System.Reflection.Assembly.GetExecutingAssembly())
        MessageBox.Show(rm.GetString("WelcomeMessage"))
        
    End Sub
    
End Class

Public Class UtilityMethods
    
    Public Shared Sub LogError()
        ' Violation 12: Another Console.WriteLine with literal
        Console.WriteLine("An error has occurred")
    End Sub
    
    Public Shared Sub ShowWarning()
        ' Violation 13: MessageBox with literal warning
        MessageBox.Show("Warning: Operation may take a long time")
    End Sub
    
    Public Shared Sub DebugMethod()
        ' Violation 14: Debug.WriteLine with literal
        Debug.WriteLine("Method execution started")
    End Sub
    
    ' This should NOT be detected - methods that don't use the problematic calls
    Public Shared Sub ProcessData(data As String)
        ' Process data without localization issues
        data = data.Trim()
    End Sub
    
End Class
