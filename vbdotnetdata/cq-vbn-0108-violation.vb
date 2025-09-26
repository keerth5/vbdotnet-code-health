' VB.NET test file for cq-vbn-0108: Avoid unused private fields
' Rule: Private fields were detected that do not appear to be accessed in the assembly.

Imports System
Imports System.Collections.Generic

Public Class UnusedFieldsExample
    
    ' Violation: Private field that is never used
    Private _unusedField As String
    
    ' Violation: Private field with Dim that is never used
    Private Dim _unusedData As Integer
    
    ' Violation: Private field of different type that is never used
    Private _unusedList As List(Of String)
    
    ' Violation: Private field with complex type that is never used
    Private _unusedDictionary As Dictionary(Of String, Integer)
    
    ' Violation: Private field with array type that is never used
    Private _unusedArray As Integer()
    
    ' Violation: Private field with custom type that is never used
    Private _unusedCustom As CustomType
    
    ' Violation: Private field with nullable type that is never used
    Private _unusedNullable As Integer?
    
    ' Violation: Private field with generic type that is never used
    Private _unusedGeneric As List(Of CustomType)
    
    ' Used field - should not be detected as violation
    Private _usedField As String
    
    Public Sub New()
        ' Initialize used field
        _usedField = "initialized"
    End Sub
    
    Public Function GetUsedField() As String
        Return _usedField
    End Function
    
End Class

Public Class MoreUnusedFieldsExample
    
    ' Violation: Private field declared with Dim that is unused
    Private Dim _count As Integer
    
    ' Violation: Private field of Boolean type that is unused
    Private _isEnabled As Boolean
    
    ' Violation: Private field of Double type that is unused
    Private _percentage As Double
    
    ' Violation: Private field of Date type that is unused
    Private _timestamp As Date
    
    ' Violation: Private field of Decimal type that is unused
    Private _amount As Decimal
    
    ' Violation: Private field of Byte type that is unused
    Private _byteValue As Byte
    
    ' Violation: Private field of Long type that is unused
    Private _longValue As Long
    
    ' Violation: Private field of Short type that is unused
    Private _shortValue As Short
    
    ' Violation: Private field of Char type that is unused
    Private _charValue As Char
    
    ' Violation: Private field of Object type that is unused
    Private _objectValue As Object
    
    Public Sub DoSomething()
        ' This method doesn't use any of the private fields above
        Console.WriteLine("Doing something")
    End Sub
    
End Class

Public Class DatabaseExample
    
    ' Violation: Private field for connection that is never used
    Private _connectionString As String
    
    ' Violation: Private field for timeout that is never used
    Private Dim _commandTimeout As Integer
    
    ' Violation: Private field for retry count that is never used
    Private _retryCount As Integer
    
    ' Violation: Private field for cache that is never used
    Private _cache As Dictionary(Of String, Object)
    
    ' Used field - should not be detected as violation
    Private _isConnected As Boolean
    
    Public Sub Connect()
        _isConnected = True
        Console.WriteLine("Connected to database")
    End Sub
    
    Public Function IsConnected() As Boolean
        Return _isConnected
    End Function
    
End Class

Public Class ConfigurationExample
    
    ' Violation: Private field for app name that is never used
    Private _applicationName As String
    
    ' Violation: Private field with Dim for version that is never used
    Private Dim _version As String
    
    ' Violation: Private field for settings that is never used
    Private _settings As Dictionary(Of String, String)
    
    ' Violation: Private field for default values that is never used
    Private _defaultValues As String()
    
    ' Violation: Private field for configuration file path that is never used
    Private _configFilePath As String
    
    Public Sub LoadConfiguration()
        Console.WriteLine("Loading configuration")
        ' This method doesn't use any of the private fields
    End Sub
    
End Class

' Custom type for testing
Public Class CustomType
    Public Property Name As String
    Public Property Value As Integer
End Class

' Non-violation examples (these should not be detected):

Public Class ProperFieldUsageExample
    
    ' Correct: Public field - should not be detected by this pattern
    Public PublicField As String
    
    ' Correct: Protected field - should not be detected by this pattern
    Protected ProtectedField As String
    
    ' Correct: Friend field - should not be detected by this pattern
    Friend FriendField As String
    
    ' Correct: Private field that is actually used - should not be detected
    Private _name As String
    
    Public Sub New(name As String)
        _name = name
    End Sub
    
    Public Function GetName() As String
        Return _name
    End Function
    
    Public Sub SetName(value As String)
        _name = value
    End Sub
    
End Class

Public Class StaticFieldExample
    
    ' Correct: Private Shared field - should not be detected by this pattern
    Private Shared _staticField As String = "static"
    
    ' Correct: Public Shared field - should not be detected by this pattern
    Public Shared SharedField As Integer = 42
    
    Public Shared Function GetStaticField() As String
        Return _staticField
    End Function
    
End Class
