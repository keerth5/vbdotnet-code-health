' VB.NET test file for cq-vbn-0136: Seal internal types
' Rule: A type that's not accessible outside its assembly and has no subtypes within its containing assembly is not sealed.

Imports System
Imports System.Collections.Generic

' Violation: Internal class that could be sealed (NotInheritable in VB.NET)
Friend Class InternalDataProcessor
    Public Sub ProcessData(data As String)
        Console.WriteLine($"Processing: {data}")
    End Sub
End Class

' Violation: Internal class with no inheritance that should be sealed
Friend Class ConfigurationManager
    Private _settings As Dictionary(Of String, String)
    
    Public Sub New()
        _settings = New Dictionary(Of String, String)()
    End Sub
    
    Public Sub SetSetting(key As String, value As String)
        _settings(key) = value
    End Sub
    
    Public Function GetSetting(key As String) As String
        Return If(_settings.ContainsKey(key), _settings(key), Nothing)
    End Function
End Class

' Violation: Internal utility class that should be sealed
Friend Class StringUtilities
    Public Shared Function IsNullOrWhiteSpace(value As String) As Boolean
        Return String.IsNullOrWhiteSpace(value)
    End Function
    
    Public Shared Function Capitalize(value As String) As String
        If String.IsNullOrEmpty(value) Then Return value
        Return Char.ToUpper(value(0)) + value.Substring(1).ToLower()
    End Function
End Class

' Violation: Internal service class that should be sealed
Friend Class EmailService
    Private ReadOnly _smtpServer As String
    
    Public Sub New(smtpServer As String)
        _smtpServer = smtpServer
    End Sub
    
    Public Sub SendEmail(toAddress As String, subject As String, body As String)
        ' Email sending logic
        Console.WriteLine($"Sending email to {toAddress}")
    End Sub
End Class

' Violation: Internal repository class that should be sealed
Friend Class UserRepository
    Private ReadOnly _connectionString As String
    
    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub
    
    Public Function GetUser(id As Integer) As User
        ' Database logic
        Return New User With {.Id = id, .Name = "Test User"}
    End Function
    
    Public Sub SaveUser(user As User)
        ' Save logic
        Console.WriteLine($"Saving user: {user.Name}")
    End Sub
End Class

' Violation: Internal helper class that should be sealed
Friend Class FileHelper
    Public Shared Function ReadAllText(filePath As String) As String
        Return System.IO.File.ReadAllText(filePath)
    End Function
    
    Public Shared Sub WriteAllText(filePath As String, content As String)
        System.IO.File.WriteAllText(filePath, content)
    End Sub
    
    Public Shared Function FileExists(filePath As String) As Boolean
        Return System.IO.File.Exists(filePath)
    End Function
End Class

' Violation: Internal calculator class that should be sealed
Friend Class Calculator
    Public Function Add(a As Double, b As Double) As Double
        Return a + b
    End Function
    
    Public Function Subtract(a As Double, b As Double) As Double
        Return a - b
    End Function
    
    Public Function Multiply(a As Double, b As Double) As Double
        Return a * b
    End Function
    
    Public Function Divide(a As Double, b As Double) As Double
        If b = 0 Then Throw New DivideByZeroException()
        Return a / b
    End Function
End Class

' Violation: Internal logger class that should be sealed
Friend Class SimpleLogger
    Private ReadOnly _logFilePath As String
    
    Public Sub New(logFilePath As String)
        _logFilePath = logFilePath
    End Sub
    
    Public Sub Log(message As String)
        Dim logEntry As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}"
        System.IO.File.AppendAllText(_logFilePath, logEntry + Environment.NewLine)
    End Sub
    
    Public Sub LogError(message As String, ex As Exception)
        Log($"ERROR: {message} - {ex.Message}")
    End Sub
End Class

' Violation: Internal validation class that should be sealed
Friend Class ValidationHelper
    Public Shared Function IsValidEmail(email As String) As Boolean
        If String.IsNullOrWhiteSpace(email) Then Return False
        Return email.Contains("@") AndAlso email.Contains(".")
    End Function
    
    Public Shared Function IsValidPhoneNumber(phoneNumber As String) As Boolean
        If String.IsNullOrWhiteSpace(phoneNumber) Then Return False
        Return phoneNumber.All(Function(c) Char.IsDigit(c) OrElse c = "-"c OrElse c = "("c OrElse c = ")"c OrElse c = " "c)
    End Function
    
    Public Shared Function IsValidZipCode(zipCode As String) As Boolean
        Return Not String.IsNullOrWhiteSpace(zipCode) AndAlso zipCode.Length = 5 AndAlso zipCode.All(AddressOf Char.IsDigit)
    End Function
End Class

' Violation: Internal cache class that should be sealed
Friend Class MemoryCache
    Private ReadOnly _cache As Dictionary(Of String, Object)
    
    Public Sub New()
        _cache = New Dictionary(Of String, Object)()
    End Sub
    
    Public Sub Set(key As String, value As Object)
        _cache(key) = value
    End Sub
    
    Public Function Get(Of T)(key As String) As T
        If _cache.ContainsKey(key) Then
            Return CType(_cache(key), T)
        End If
        Return Nothing
    End Function
    
    Public Sub Remove(key As String)
        _cache.Remove(key)
    End Sub
    
    Public Sub Clear()
        _cache.Clear()
    End Sub
End Class

' Violation: Internal formatter class that should be sealed
Friend Class DateTimeFormatter
    Public Shared Function FormatAsShortDate(dateTime As DateTime) As String
        Return dateTime.ToString("yyyy-MM-dd")
    End Function
    
    Public Shared Function FormatAsLongDate(dateTime As DateTime) As String
        Return dateTime.ToString("dddd, MMMM dd, yyyy")
    End Function
    
    Public Shared Function FormatAsTime(dateTime As DateTime) As String
        Return dateTime.ToString("HH:mm:ss")
    End Function
End Class

' Supporting classes
Public Class User
    Public Property Id As Integer
    Public Property Name As String
    Public Property Email As String
End Class

' Examples of correct usage (for reference)

' Correct: Sealed internal class
Friend NotInheritable Class SealedInternalClass
    Public Sub DoSomething()
        Console.WriteLine("This class is properly sealed")
    End Sub
End Class

' Correct: Public class that might be inherited
Public Class PublicBaseClass
    Public Overridable Sub DoSomething()
        Console.WriteLine("This can be overridden")
    End Sub
End Class

' Correct: Internal class that has subclasses (should not be sealed)
Friend Class BaseProcessor
    Public Overridable Sub Process()
        Console.WriteLine("Base processing")
    End Sub
End Class

Friend Class SpecialProcessor
    Inherits BaseProcessor
    
    Public Overrides Sub Process()
        Console.WriteLine("Special processing")
    End Sub
End Class

' Violation: Another internal class that should be sealed
Friend Class CryptographyHelper
    Public Shared Function GenerateHash(input As String) As String
        ' Hash generation logic
        Return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
    End Function
    
    Public Shared Function EncryptString(plainText As String, key As String) As String
        ' Encryption logic
        Return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText + key))
    End Function
End Class

' Violation: Internal JSON helper that should be sealed
Friend Class JsonHelper
    Public Shared Function Serialize(Of T)(obj As T) As String
        ' Serialization logic
        Return obj.ToString()
    End Function
    
    Public Shared Function Deserialize(Of T)(json As String) As T
        ' Deserialization logic
        Return Nothing
    End Function
End Class
