' VB.NET test file for cq-vbn-0147: Use 'CompositeFormat'
' Rule: To reduce the formatting cost, cache and use a CompositeFormat instance as the argument to String.Format or StringBuilder.AppendFormat.

Imports System
Imports System.Text

Public Class CompositeFormatExamples
    
    Public Sub TestBasicStringFormat()
        Dim name As String = "Alice"
        Dim age As Integer = 30
        Dim city As String = "New York"
        
        ' Violation: String.Format with format string that could be cached
        Dim message1 As String = String.Format("Hello {0}, you are {1} years old", name, age)
        
        ' Violation: String.Format with multiple parameters
        Dim message2 As String = String.Format("Name: {0}, Age: {1}, City: {2}", name, age, city)
        
        ' Violation: String.Format with format string in loop (very inefficient)
        For i As Integer = 1 To 5
            Dim loopMessage As String = String.Format("Iteration {0}: Processing item {1}", i, name)
            Console.WriteLine(loopMessage)
        Next
        
        Console.WriteLine(message1)
        Console.WriteLine(message2)
    End Sub
    
    Public Sub TestStringBuilderAppendFormat()
        Dim sb As New StringBuilder()
        Dim items() As String = {"apple", "banana", "cherry", "date", "elderberry"}
        
        ' Violation: StringBuilder.AppendFormat with repeated format string
        For Each item In items
            sb.AppendFormat("Item: {0}, Length: {1}" & Environment.NewLine, item, item.Length)
        Next
        
        ' Violation: StringBuilder.AppendFormat with complex format
        Dim user As String = "Bob"
        Dim score As Integer = 95
        Dim level As String = "Advanced"
        
        sb.AppendFormat("User {0} achieved score {1} at {2} level" & Environment.NewLine, user, score, level)
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub TestStringFormatInMethods()
        Dim users() As String = {"John", "Jane", "Bob", "Alice"}
        Dim scores() As Integer = {85, 92, 78, 96}
        
        ' Violation: String.Format in method calls
        For i As Integer = 0 To users.Length - 1
            LogUserScore(users(i), scores(i))
        Next
        
        ' Violation: String.Format in return statements
        Dim report As String = GenerateReport("Sales", 2023, 125000)
        Console.WriteLine(report)
    End Sub
    
    Private Sub LogUserScore(userName As String, score As Integer)
        ' Violation: String.Format that could use CompositeFormat
        Dim logMessage As String = String.Format("[{0}] User: {1}, Score: {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userName, score)
        Console.WriteLine(logMessage)
    End Sub
    
    Private Function GenerateReport(category As String, year As Integer, amount As Decimal) As String
        ' Violation: String.Format in frequently called method
        Return String.Format("Report for {0} in {1}: Total amount ${2:N2}", category, year, amount)
    End Function
    
    Public Sub TestComplexFormatStrings()
        Dim product As String = "Laptop"
        Dim price As Decimal = 999.99D
        Dim discount As Double = 0.15
        Dim availability As Boolean = True
        
        ' Violation: Complex format string that would benefit from caching
        Dim productInfo As String = String.Format("Product: {0}" & Environment.NewLine & 
                                                 "Price: ${1:C}" & Environment.NewLine & 
                                                 "Discount: {2:P}" & Environment.NewLine & 
                                                 "Available: {3}", 
                                                 product, price, discount, availability)
        
        Console.WriteLine(productInfo)
        
        ' Violation: Multi-line format string
        Dim detailedInfo As String = String.Format("====== Product Details ======" & Environment.NewLine & 
                                                   "Name: {0}" & Environment.NewLine & 
                                                   "Price: {1:C}" & Environment.NewLine & 
                                                   "Discount: {2:P}" & Environment.NewLine & 
                                                   "Status: {3}" & Environment.NewLine & 
                                                   "=============================", 
                                                   product, price, discount, If(availability, "In Stock", "Out of Stock"))
        
        Console.WriteLine(detailedInfo)
    End Sub
    
    Public Sub TestStringFormatWithCulture()
        Dim amount As Decimal = 1234.56D
        Dim date As DateTime = DateTime.Now
        Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
        
        ' Violation: String.Format with culture that could use CompositeFormat
        Dim formattedMessage As String = String.Format(culture, "Amount: {0:C}, Date: {1:D}", amount, date)
        Console.WriteLine(formattedMessage)
        
        ' Violation: String.Format with custom format provider
        Dim customMessage As String = String.Format(System.Globalization.CultureInfo.CurrentCulture, 
                                                    "Transaction: {0}, Amount: {1:N2}, Date: {2:yyyy-MM-dd}", 
                                                    "TXN001", amount, date)
        Console.WriteLine(customMessage)
    End Sub
    
    Public Sub TestStringBuilderAppendFormatInLoops()
        Dim sb As New StringBuilder()
        Dim data As New Dictionary(Of String, Integer) From {
            {"January", 1000},
            {"February", 1200},
            {"March", 1100},
            {"April", 1300},
            {"May", 1400}
        }
        
        ' Violation: AppendFormat in loop with same format string
        For Each kvp In data
            sb.AppendFormat("Month: {0}, Sales: ${1:N0}" & Environment.NewLine, kvp.Key, kvp.Value)
        Next
        
        ' Violation: AppendFormat with table-like formatting
        sb.AppendLine("Sales Report")
        sb.AppendLine("============")
        For Each kvp In data
            sb.AppendFormat("{0,-10} | ${1,8:N0}" & Environment.NewLine, kvp.Key, kvp.Value)
        Next
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub TestNestedStringFormat()
        Dim orders() As String = {"ORD001", "ORD002", "ORD003"}
        Dim customers() As String = {"Alice", "Bob", "Charlie"}
        Dim amounts() As Decimal = {150.0D, 275.5D, 89.99D}
        
        ' Violation: Nested String.Format calls
        For i As Integer = 0 To orders.Length - 1
            Dim orderDetails As String = String.Format("Order: {0}, Customer: {1}", orders(i), customers(i))
            Dim fullMessage As String = String.Format("Processing {0} with amount ${1:F2}", orderDetails, amounts(i))
            Console.WriteLine(fullMessage)
        Next
    End Sub
    
    Public Sub TestStringFormatWithConditionals()
        Dim users() As String = {"Admin", "User", "Guest", "Moderator"}
        Dim isActive() As Boolean = {True, True, False, True}
        
        ' Violation: String.Format in conditional expressions
        For i As Integer = 0 To users.Length - 1
            Dim status As String = If(isActive(i), "Active", "Inactive")
            Dim userInfo As String = String.Format("User: {0} ({1}) - Status: {2}", 
                                                  users(i), i + 1, status)
            Console.WriteLine(userInfo)
        Next
        
        ' Violation: String.Format in Select Case
        Dim userType As String = "Admin"
        Dim permissions As String
        Select Case userType
            Case "Admin"
                permissions = String.Format("User {0} has {1} permissions", userType, "Full")
            Case "User"
                permissions = String.Format("User {0} has {1} permissions", userType, "Limited")
            Case Else
                permissions = String.Format("User {0} has {1} permissions", userType, "None")
        End Select
        Console.WriteLine(permissions)
    End Sub
    
    Public Sub TestStringBuilderAppendFormatVariations()
        Dim sb As New StringBuilder()
        Dim logLevel As String = "INFO"
        Dim component As String = "Database"
        Dim message As String = "Connection established"
        
        ' Violation: Different AppendFormat patterns that could use CompositeFormat
        sb.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss}] {1}: {2}" & Environment.NewLine, DateTime.Now, logLevel, message)
        sb.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss}] {1}: {2}" & Environment.NewLine, DateTime.Now, "ERROR", "Connection failed")
        sb.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss}] {1}: {2}" & Environment.NewLine, DateTime.Now, "DEBUG", "Query executed")
        
        ' Violation: AppendFormat with alignment and format specifiers
        sb.AppendFormat("{0,-20} | {1,10:N2} | {2,8:P1}" & Environment.NewLine, "Product A", 123.45D, 0.15)
        sb.AppendFormat("{0,-20} | {1,10:N2} | {2,8:P1}" & Environment.NewLine, "Product B", 678.90D, 0.20)
        sb.AppendFormat("{0,-20} | {1,10:N2} | {2,8:P1}" & Environment.NewLine, "Product C", 456.78D, 0.10)
        
        Console.WriteLine(sb.ToString())
    End Sub
    
    Public Sub TestStringFormatInExceptionMessages()
        Dim userId As Integer = 123
        Dim operation As String = "DeleteUser"
        
        Try
            ' Simulate some operation
            If userId <= 0 Then
                ' Violation: String.Format in exception message
                Throw New ArgumentException(String.Format("Invalid user ID: {0}. User ID must be positive.", userId))
            End If
            
            ' Violation: String.Format in different exception types
            Throw New InvalidOperationException(String.Format("Operation '{0}' failed for user {1}", operation, userId))
            
        Catch ex As Exception
            ' Violation: String.Format in logging exception
            Console.WriteLine(String.Format("Exception occurred: {0} - {1}", ex.GetType().Name, ex.Message))
        End Try
    End Sub
    
    ' Examples of correct usage (for reference)
    Private Shared ReadOnly UserLogFormat As CompositeFormat = CompositeFormat.Parse("User: {0}, Score: {1}, Level: {2}")
    Private Shared ReadOnly ReportFormat As CompositeFormat = CompositeFormat.Parse("Report for {0} in {1}: Total amount ${2:N2}")
    
    Public Sub TestCorrectUsage()
        ' Correct: Using CompositeFormat for repeated formatting
        Dim users() As String = {"Alice", "Bob", "Charlie"}
        Dim scores() As Integer = {85, 92, 78}
        Dim levels() As String = {"Beginner", "Intermediate", "Advanced"}
        
        For i As Integer = 0 To users.Length - 1
            Dim message As String = String.Format(UserLogFormat, users(i), scores(i), levels(i))
            Console.WriteLine(message)
        Next
        
        ' Correct: Using CompositeFormat for method
        Dim report As String = String.Format(ReportFormat, "Sales", 2023, 125000D)
        Console.WriteLine(report)
        
        ' Correct: Direct string formatting for one-time use
        Dim simpleMessage As String = $"Hello {users(0)}, welcome!"
        Console.WriteLine(simpleMessage)
    End Sub
    
    Public Sub TestMoreStringFormatViolations()
        ' Violation: String.Format in property getters
        Dim obj As New TestClass()
        Console.WriteLine(obj.FormattedProperty)
        
        ' Violation: String.Format in event handlers
        ' AddHandler SomeEvent, Sub(sender, e) Console.WriteLine(String.Format("Event fired: {0}", e.ToString()))
        
        ' Violation: String.Format in LINQ projections
        Dim numbers() As Integer = {1, 2, 3, 4, 5}
        Dim formattedNumbers = numbers.Select(Function(n) String.Format("Number: {0:D3}", n)).ToArray()
        
        For Each formatted In formattedNumbers
            Console.WriteLine(formatted)
        Next
    End Sub
    
    Public Class TestClass
        Public ReadOnly Property FormattedProperty As String
            Get
                ' Violation: String.Format in property
                Return String.Format("Current time: {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
            End Get
        End Property
    End Class
    
    Public Sub TestStringFormatWithInterpolation()
        Dim name As String = "John"
        Dim age As Integer = 25
        
        ' Violation: Using String.Format when string interpolation might be simpler
        Dim message1 As String = String.Format("Hello {0}, you are {1} years old", name, age)
        
        ' Note: This is still a violation for CompositeFormat rule if used repeatedly
        ' The correct approach would be to use CompositeFormat for repeated patterns
        
        ' Violation: String.Format in string building
        Dim builder As New StringBuilder()
        builder.AppendFormat("User: {0}" & Environment.NewLine, name)
        builder.AppendFormat("Age: {0}" & Environment.NewLine, age)
        builder.AppendFormat("Status: {0}" & Environment.NewLine, "Active")
        
        Console.WriteLine(builder.ToString())
    End Sub
End Class
