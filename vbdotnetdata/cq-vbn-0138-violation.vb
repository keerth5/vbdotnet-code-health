' VB.NET test file for cq-vbn-0138: Prefer the 'IDictionary.TryGetValue(TKey, out TValue)' method
' Rule: Prefer 'TryGetValue' over a Dictionary indexer access guarded by a 'ContainsKey' check. 'ContainsKey' and the indexer both look up the key, so using 'TryGetValue' avoids the extra lookup.

Imports System
Imports System.Collections.Generic

Public Class TryGetValueExamples
    Private _userCache As Dictionary(Of Integer, String)
    Private _settings As Dictionary(Of String, String)
    Private _sessionData As Dictionary(Of String, Object)
    Private _configuration As Dictionary(Of String, Integer)
    
    Public Sub New()
        _userCache = New Dictionary(Of Integer, String)()
        _settings = New Dictionary(Of String, String)()
        _sessionData = New Dictionary(Of String, Object)()
        _configuration = New Dictionary(Of String, Integer)()
        
        ' Initialize with sample data
        _userCache.Add(1, "Alice")
        _userCache.Add(2, "Bob")
        _settings.Add("theme", "dark")
        _settings.Add("language", "en")
        _configuration.Add("timeout", 30)
        _configuration.Add("maxRetries", 3)
    End Sub
    
    Public Function GetUserName(userId As Integer) As String
        ' Violation: ContainsKey followed by indexer access
        If _userCache.ContainsKey(userId) Then
            Return _userCache(userId)
        Else
            Return "Unknown User"
        End If
    End Function
    
    Public Function GetSetting(key As String) As String
        ' Violation: ContainsKey check followed by dictionary access
        If _settings.ContainsKey(key) Then
            Return _settings(key)
        End If
        Return Nothing
    End Function
    
    Public Function GetConfigurationValue(key As String) As Integer
        ' Violation: ContainsKey with default value return
        If _configuration.ContainsKey(key) Then
            Return _configuration(key)
        Else
            Return -1
        End If
    End Function
    
    Public Sub ProcessUserData(userId As Integer)
        ' Violation: ContainsKey followed by value access
        If _userCache.ContainsKey(userId) Then
            Dim userName As String = _userCache(userId)
            Console.WriteLine($"Processing user: {userName}")
        Else
            Console.WriteLine("User not found")
        End If
    End Sub
    
    Public Sub UpdateSettingIfExists(key As String, newValue As String)
        ' Violation: ContainsKey check before accessing value
        If _settings.ContainsKey(key) Then
            Dim oldValue As String = _settings(key)
            _settings(key) = newValue
            Console.WriteLine($"Updated setting '{key}' from '{oldValue}' to '{newValue}'")
        End If
    End Sub
    
    Public Sub ProcessSessionData(sessionId As String)
        ' Violation: ContainsKey followed by indexer
        If _sessionData.ContainsKey(sessionId) Then
            Dim data As Object = _sessionData(sessionId)
            Console.WriteLine($"Session data: {data}")
        End If
    End Sub
    
    Public Function GetUserWithDefault(userId As Integer, defaultName As String) As String
        ' Violation: ContainsKey pattern with default value
        If _userCache.ContainsKey(userId) Then
            Return _userCache(userId)
        Else
            Return defaultName
        End If
    End Function
    
    Public Sub LogSettingValue(key As String)
        ' Violation: ContainsKey check before value retrieval
        If _settings.ContainsKey(key) Then
            Dim value As String = _settings(key)
            Console.WriteLine($"Setting '{key}' = '{value}'")
        Else
            Console.WriteLine($"Setting '{key}' not found")
        End If
    End Sub
    
    Public Function CalculateTotal(discountKey As String) As Decimal
        Dim baseAmount As Decimal = 100D
        
        ' Violation: ContainsKey followed by dictionary access
        If _configuration.ContainsKey(discountKey) Then
            Dim discountPercent As Integer = _configuration(discountKey)
            Return baseAmount * (1 - discountPercent / 100D)
        End If
        
        Return baseAmount
    End Function
    
    Public Sub ProcessMultipleKeys(keys As String())
        For Each key In keys
            ' Violation: ContainsKey in loop followed by value access
            If _settings.ContainsKey(key) Then
                Dim value As String = _settings(key)
                Console.WriteLine($"Processing {key}: {value}")
            End If
        Next
    End Sub
    
    Public Function GetNestedValue(outerKey As String, innerKey As String) As String
        ' Violation: ContainsKey followed by complex value access
        If _sessionData.ContainsKey(outerKey) Then
            Dim innerDict As Dictionary(Of String, String) = TryCast(_sessionData(outerKey), Dictionary(Of String, String))
            If innerDict IsNot Nothing AndAlso innerDict.ContainsKey(innerKey) Then
                Return innerDict(innerKey)
            End If
        End If
        Return Nothing
    End Function
    
    Public Sub ConditionalProcessing(userId As Integer, processAll As Boolean)
        If processAll Then
            ' Violation: ContainsKey check before value usage
            If _userCache.ContainsKey(userId) Then
                Dim userName As String = _userCache(userId)
                ProcessUserName(userName)
            End If
        End If
    End Sub
    
    Private Sub ProcessUserName(userName As String)
        Console.WriteLine($"Processing: {userName}")
    End Sub
    
    Public Function GetSettingWithValidation(key As String) As String
        ' Violation: ContainsKey followed by validation and access
        If _settings.ContainsKey(key) Then
            Dim value As String = _settings(key)
            If Not String.IsNullOrEmpty(value) Then
                Return value.Trim()
            End If
        End If
        Return String.Empty
    End Function
    
    Public Sub BatchProcessSettings(settingKeys As List(Of String))
        For Each key In settingKeys
            ' Violation: ContainsKey pattern in batch processing
            If _settings.ContainsKey(key) Then
                Dim value As String = _settings(key)
                ProcessSettingValue(key, value)
            Else
                Console.WriteLine($"Setting '{key}' not found")
            End If
        Next
    End Sub
    
    Private Sub ProcessSettingValue(key As String, value As String)
        Console.WriteLine($"Processing setting: {key} = {value}")
    End Sub
    
    Public Function GetConfigValueOrDefault(key As String, defaultValue As Integer) As Integer
        ' Violation: ContainsKey with default value logic
        If _configuration.ContainsKey(key) Then
            Return _configuration(key)
        Else
            Return defaultValue
        End If
    End Function
    
    Public Sub DisplayUserInfo(userId As Integer)
        ' Violation: ContainsKey followed by value display
        If _userCache.ContainsKey(userId) Then
            Dim userName As String = _userCache(userId)
            Console.WriteLine($"User ID: {userId}, Name: {userName}")
        Else
            Console.WriteLine($"User ID: {userId}, Name: Not Found")
        End If
    End Sub
    
    Public Function TryGetAndModify(key As String) As String
        ' Violation: ContainsKey followed by value modification
        If _settings.ContainsKey(key) Then
            Dim value As String = _settings(key)
            Return value.ToUpper()
        End If
        Return Nothing
    End Function
    
    ' Examples of correct usage (for reference)
    Public Function GetUserNameCorrect(userId As Integer) As String
        Dim userName As String = Nothing
        ' Correct: Using TryGetValue
        If _userCache.TryGetValue(userId, userName) Then
            Return userName
        Else
            Return "Unknown User"
        End If
    End Function
    
    Public Function GetSettingCorrect(key As String) As String
        Dim value As String = Nothing
        ' Correct: Using TryGetValue
        Return If(_settings.TryGetValue(key, value), value, Nothing)
    End Function
    
    Public Sub ProcessUserDataCorrect(userId As Integer)
        Dim userName As String = Nothing
        ' Correct: Using TryGetValue
        If _userCache.TryGetValue(userId, userName) Then
            Console.WriteLine($"Processing user: {userName}")
        Else
            Console.WriteLine("User not found")
        End If
    End Sub
    
    Public Sub TestComplexScenarios()
        Dim complexDict As New Dictionary(Of String, Dictionary(Of String, String))()
        complexDict.Add("section1", New Dictionary(Of String, String) From {{"key1", "value1"}})
        
        Dim sectionKey As String = "section1"
        Dim itemKey As String = "key1"
        
        ' Violation: Nested ContainsKey patterns
        If complexDict.ContainsKey(sectionKey) Then
            Dim section As Dictionary(Of String, String) = complexDict(sectionKey)
            If section.ContainsKey(itemKey) Then
                Dim value As String = section(itemKey)
                Console.WriteLine($"Found value: {value}")
            End If
        End If
    End Sub
    
    Public Function GetValueWithTransformation(key As String) As String
        ' Violation: ContainsKey followed by value transformation
        If _settings.ContainsKey(key) Then
            Dim rawValue As String = _settings(key)
            Return rawValue.ToLower().Trim()
        End If
        Return String.Empty
    End Function
End Class
