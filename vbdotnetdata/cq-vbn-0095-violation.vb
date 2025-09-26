' Test file for cq-vbn-0095: Use PascalCase for named placeholders
' Rule should detect string literals with lowercase named placeholders that should use PascalCase

Imports System

Public Class StringFormattingExamples
    
    Public Sub DisplayMessages()
        
        ' Violation 1: String with lowercase named placeholder
        Dim message1 As String = "Hello {name}, welcome to our system!"
        Console.WriteLine(message1)
        
        ' Violation 2: String with lowercase named placeholder
        Dim message2 As String = "User {userId} has {count} items in cart"
        Console.WriteLine(message2)
        
        ' Violation 3: String with lowercase named placeholder
        Dim errorMessage As String = "Error occurred in {methodName} at line {lineNumber}"
        Console.WriteLine(errorMessage)
        
        ' Violation 4: String with multiple lowercase placeholders
        Dim logEntry As String = "Processing {fileName} with {fileSize} bytes at {timestamp}"
        Console.WriteLine(logEntry)
        
        ' Violation 5: String with lowercase placeholder
        Dim statusMessage As String = "Operation {operationId} completed successfully"
        Console.WriteLine(statusMessage)
        
        ' This should NOT be detected - proper PascalCase placeholder
        Dim correctMessage1 As String = "Hello {Name}, welcome to our system!"
        Console.WriteLine(correctMessage1)
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctMessage2 As String = "User {UserId} has {Count} items in cart"
        Console.WriteLine(correctMessage2)
        
        ' This should NOT be detected - no placeholders
        Dim simpleMessage As String = "This is a simple message without placeholders"
        Console.WriteLine(simpleMessage)
        
    End Sub
    
    Public Sub FormatTemplates()
        
        ' Violation 6: Template with lowercase placeholder
        Dim template1 As String = "Dear {customerName}, your order {orderId} is ready"
        
        ' Violation 7: Template with lowercase placeholder
        Dim template2 As String = "Invoice {invoiceNumber} for amount ${totalAmount}"
        
        ' Violation 8: Template with lowercase placeholder
        Dim emailTemplate As String = "Thank you {firstName} for your purchase on {purchaseDate}"
        
        ' Violation 9: Query template with lowercase placeholder
        Dim queryTemplate As String = "SELECT * FROM Users WHERE UserId = {userId} AND Status = '{status}'"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctTemplate1 As String = "Dear {CustomerName}, your order {OrderId} is ready"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctTemplate2 As String = "Invoice {InvoiceNumber} for amount ${TotalAmount}"
        
    End Sub
    
    Public Sub ValidationMessages()
        
        ' Violation 10: Validation message with lowercase placeholder
        Dim validation1 As String = "Field {fieldName} is required"
        
        ' Violation 11: Validation message with lowercase placeholder
        Dim validation2 As String = "Value {inputValue} is not valid for {propertyName}"
        
        ' Violation 12: Validation message with lowercase placeholder
        Dim validation3 As String = "Maximum length for {fieldName} is {maxLength} characters"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctValidation1 As String = "Field {FieldName} is required"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctValidation2 As String = "Value {InputValue} is not valid for {PropertyName}"
        
    End Sub
    
    Public Sub ConfigurationMessages()
        
        ' Violation 13: Configuration message with lowercase placeholder
        Dim config1 As String = "Loading configuration from {configPath}"
        
        ' Violation 14: Configuration message with lowercase placeholder
        Dim config2 As String = "Setting {settingName} has value {settingValue}"
        
        ' Violation 15: Configuration message with lowercase placeholder
        Dim config3 As String = "Database connection string: {connectionString}"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctConfig1 As String = "Loading configuration from {ConfigPath}"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctConfig2 As String = "Setting {SettingName} has value {SettingValue}"
        
    End Sub
    
    Public Sub ReportMessages()
        
        ' Violation 16: Report message with lowercase placeholder
        Dim report1 As String = "Report {reportName} generated on {generationDate}"
        
        ' Violation 17: Report message with lowercase placeholder
        Dim report2 As String = "Total records: {recordCount}, Processed: {processedCount}"
        
        ' Violation 18: Report message with lowercase placeholder
        Dim report3 As String = "Export completed in {executionTime} seconds"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctReport1 As String = "Report {ReportName} generated on {GenerationDate}"
        
        ' This should NOT be detected - proper PascalCase placeholders
        Dim correctReport2 As String = "Total records: {RecordCount}, Processed: {ProcessedCount}"
        
    End Sub
    
    Public Sub EdgeCases()
        
        ' Violation 19: Edge case with underscore in placeholder
        Dim edge1 As String = "Processing {file_name} with {file_size}"
        
        ' Violation 20: Edge case with number in placeholder
        Dim edge2 As String = "Item {item1} and {item2} are available"
        
        ' This should NOT be detected - single letter placeholder (might be acceptable)
        Dim edge3 As String = "Value is {x}"
        
        ' This should NOT be detected - all uppercase placeholder
        Dim edge4 As String = "Constant {MAX_VALUE} exceeded"
        
        ' This should NOT be detected - no placeholders, just braces
        Dim edge5 As String = "This is a JSON object: { key: value }"
        
    End Sub
    
End Class
