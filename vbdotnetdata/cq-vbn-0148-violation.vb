' VB.NET test file for cq-vbn-0148: Prefer the 'IDictionary.TryAdd(TKey, TValue)' method
' Rule: Both Dictionary<TKey,TValue>.ContainsKey(TKey) and Dictionary<TKey,TValue>.Add perform a lookup, which is redundant. It's is more efficient to call Dictionary<TKey,TValue>.TryAdd, which returns a bool indicating if the value was added or not. TryAdd doesn't overwrite the key's value if the key is already present.

Imports System
Imports System.Collections.Generic

Public Class TryAddDictionaryExamples
    Private _userCache As Dictionary(Of Integer, String)
    Private _settings As Dictionary(Of String, String)
    Private _sessionData As Dictionary(Of String, Object)
    Private _counters As Dictionary(Of String, Integer)
    
    Public Sub New()
        _userCache = New Dictionary(Of Integer, String)()
        _settings = New Dictionary(Of String, String)()
        _sessionData = New Dictionary(Of String, Object)()
        _counters = New Dictionary(Of String, Integer)()
    End Sub
    
    Public Sub TestBasicContainsKeyAdd()
        Dim userId As Integer = 1
        Dim userName As String = "Alice"
        
        ' Violation: ContainsKey check before Add
        If Not _userCache.ContainsKey(userId) Then
            _userCache.Add(userId, userName)
        End If
        
        Dim settingKey As String = "theme"
        Dim settingValue As String = "dark"
        
        ' Violation: ContainsKey check before Add
        If Not _settings.ContainsKey(settingKey) Then
            _settings.Add(settingKey, settingValue)
        End If
    End Sub
    
    Public Sub AddUserIfNotExists(userId As Integer, userName As String)
        ' Violation: ContainsKey before Add pattern
        If Not _userCache.ContainsKey(userId) Then
            _userCache.Add(userId, userName)
            Console.WriteLine($"Added user {userId}: {userName}")
        Else
            Console.WriteLine($"User {userId} already exists")
        End If
    End Sub
    
    Public Sub AddSettingIfNotExists(key As String, value As String)
        ' Violation: ContainsKey check before Add
        If Not _settings.ContainsKey(key) Then
            _settings.Add(key, value)
            Console.WriteLine($"Setting '{key}' added with value '{value}'")
        End If
    End Sub
    
    Public Sub ProcessMultipleUsers(users As Dictionary(Of Integer, String))
        For Each kvp In users
            ' Violation: ContainsKey before Add in loop
            If Not _userCache.ContainsKey(kvp.Key) Then
                _userCache.Add(kvp.Key, kvp.Value)
            End If
        Next
    End Sub
    
    Public Sub InitializeDefaultSettings()
        Dim defaultSettings As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"language", "en"},
            {"theme", "light"},
            {"notifications", "true"},
            {"autoSave", "false"}
        }
        
        ' Violation: ContainsKey before Add for each setting
        For Each setting In defaultSettings
            If Not _settings.ContainsKey(setting.Key) Then
                _settings.Add(setting.Key, setting.Value)
            End If
        Next
    End Sub
    
    Public Sub AddSessionData(sessionId As String, data As Object)
        ' Violation: ContainsKey check before Add
        If Not _sessionData.ContainsKey(sessionId) Then
            _sessionData.Add(sessionId, data)
            Console.WriteLine($"Session data added for {sessionId}")
        Else
            Console.WriteLine($"Session {sessionId} already has data")
        End If
    End Sub
    
    Public Sub IncrementCounter(counterName As String)
        ' Violation: ContainsKey before Add for counter initialization
        If Not _counters.ContainsKey(counterName) Then
            _counters.Add(counterName, 0)
        End If
        
        _counters(counterName) += 1
    End Sub
    
    Public Sub ProcessCounterUpdates(counterUpdates As List(Of String))
        For Each counterName In counterUpdates
            ' Violation: ContainsKey before Add in loop
            If Not _counters.ContainsKey(counterName) Then
                _counters.Add(counterName, 1)
            Else
                _counters(counterName) += 1
            End If
        Next
    End Sub
    
    Public Sub TestComplexContainsKeyAddPatterns()
        Dim tempDict As New Dictionary(Of String, Integer)()
        Dim items() As String = {"apple", "banana", "cherry", "date", "elderberry"}
        
        ' Violation: ContainsKey before Add with complex logic
        For Each item In items
            If Not tempDict.ContainsKey(item) Then
                tempDict.Add(item, item.Length)
                Console.WriteLine($"Added {item} with length {item.Length}")
            End If
        Next
        
        ' Violation: ContainsKey before Add with conditional value
        Dim key As String = "special"
        Dim value As Integer = If(tempDict.Count > 3, 100, 50)
        
        If Not tempDict.ContainsKey(key) Then
            tempDict.Add(key, value)
        End If
    End Sub
    
    Public Sub AddConfigurationValues(config As Dictionary(Of String, String))
        ' Violation: ContainsKey before Add for configuration merging
        For Each configItem In config
            If Not _settings.ContainsKey(configItem.Key) Then
                _settings.Add(configItem.Key, configItem.Value)
                Console.WriteLine($"Added config: {configItem.Key} = {configItem.Value}")
            Else
                Console.WriteLine($"Config {configItem.Key} already exists, keeping existing value")
            End If
        Next
    End Sub
    
    Public Sub TestContainsKeyAddWithExceptionHandling()
        Dim testDict As New Dictionary(Of String, String)()
        
        Try
            Dim key As String = "testKey"
            Dim value As String = "testValue"
            
            ' Violation: ContainsKey before Add even with exception handling
            If Not testDict.ContainsKey(key) Then
                testDict.Add(key, value)
                Console.WriteLine("Key added successfully")
            End If
            
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Sub
    
    Public Function TryAddUser(userId As Integer, userName As String) As Boolean
        ' Violation: ContainsKey check before Add with return value
        If Not _userCache.ContainsKey(userId) Then
            _userCache.Add(userId, userName)
            Return True
        Else
            Return False
        End If
    End Function
    
    Public Sub TestContainsKeyAddWithLogging()
        Dim logData As New Dictionary(Of String, String)()
        Dim entries() As String = {"entry1", "entry2", "entry3", "entry1"} ' Duplicate entry1
        
        For Each entry In entries
            ' Violation: ContainsKey before Add with logging
            If Not logData.ContainsKey(entry) Then
                logData.Add(entry, DateTime.Now.ToString())
                Console.WriteLine($"Logged entry: {entry}")
            Else
                Console.WriteLine($"Entry {entry} already logged")
            End If
        Next
    End Sub
    
    Public Sub ProcessBatchAdditions(batchData As List(Of KeyValuePair(Of String, Object)))
        ' Violation: ContainsKey before Add in batch processing
        For Each item In batchData
            If Not _sessionData.ContainsKey(item.Key) Then
                _sessionData.Add(item.Key, item.Value)
            End If
        Next
        
        Console.WriteLine($"Processed {batchData.Count} items, session data now has {_sessionData.Count} entries")
    End Sub
    
    Public Sub TestNestedContainsKeyAdd()
        Dim outerDict As New Dictionary(Of String, Dictionary(Of String, Integer))()
        Dim categories() As String = {"fruits", "vegetables", "grains"}
        
        For Each category In categories
            ' Violation: ContainsKey before Add for outer dictionary
            If Not outerDict.ContainsKey(category) Then
                outerDict.Add(category, New Dictionary(Of String, Integer)())
            End If
            
            ' Violation: ContainsKey before Add for inner dictionary
            Dim innerDict As Dictionary(Of String, Integer) = outerDict(category)
            Dim itemName As String = category & "_item"
            
            If Not innerDict.ContainsKey(itemName) Then
                innerDict.Add(itemName, 1)
            End If
        Next
    End Sub
    
    Public Sub AddUserPermissions(userId As Integer, permissions As List(Of String))
        Dim userPermissions As New Dictionary(Of Integer, List(Of String))()
        
        ' Violation: ContainsKey before Add for user permissions
        If Not userPermissions.ContainsKey(userId) Then
            userPermissions.Add(userId, New List(Of String)())
        End If
        
        userPermissions(userId).AddRange(permissions)
        Console.WriteLine($"Added {permissions.Count} permissions for user {userId}")
    End Sub
    
    Public Sub TestContainsKeyAddWithMethodChaining()
        Dim chainDict As New Dictionary(Of String, String)()
        Dim keys() As String = {"key1", "key2", "key3"}
        
        For Each key In keys
            ' Violation: ContainsKey before Add with method chaining
            If Not chainDict.ContainsKey(key.ToLower()) Then
                chainDict.Add(key.ToLower(), key.ToUpper())
            End If
        Next
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub TestCorrectUsage()
        ' Correct: Using TryAdd
        Dim wasAdded1 As Boolean = _userCache.TryAdd(100, "TestUser")
        If wasAdded1 Then
            Console.WriteLine("User added successfully")
        Else
            Console.WriteLine("User already exists")
        End If
        
        ' Correct: Using TryAdd in loop
        Dim newUsers As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String) From {
            {101, "User101"},
            {102, "User102"},
            {103, "User103"}
        }
        
        For Each kvp In newUsers
            If _userCache.TryAdd(kvp.Key, kvp.Value) Then
                Console.WriteLine($"Added user {kvp.Key}: {kvp.Value}")
            End If
        Next
        
        ' Correct: Using indexer when overwriting is intended
        _settings("existingKey") = "newValue"
        
        ' Correct: Using ContainsKey when not adding
        If _settings.ContainsKey("theme") Then
            Console.WriteLine($"Theme is set to: {_settings("theme")}")
        End If
    End Sub
    
    Public Sub TestMoreContainsKeyAddViolations()
        Dim cacheDict As New Dictionary(Of String, Object)()
        Dim cacheKeys() As String = {"cache1", "cache2", "cache3", "cache4"}
        
        ' Violation: ContainsKey before Add with complex objects
        For Each cacheKey In cacheKeys
            If Not cacheDict.ContainsKey(cacheKey) Then
                cacheDict.Add(cacheKey, New With {.Key = cacheKey, .Value = DateTime.Now, .Count = 1})
            End If
        Next
        
        ' Violation: ContainsKey before Add in property setter
        Dim obj As New TestClass()
        obj.AddToInternalDict("testKey", "testValue")
    End Sub
    
    Public Class TestClass
        Private _internalDict As New Dictionary(Of String, String)()
        
        Public Sub AddToInternalDict(key As String, value As String)
            ' Violation: ContainsKey before Add in class method
            If Not _internalDict.ContainsKey(key) Then
                _internalDict.Add(key, value)
            End If
        End Sub
    End Class
    
    Public Sub TestContainsKeyAddWithGenericDictionaries()
        Dim genericDict As New Dictionary(Of String, List(Of Integer))()
        Dim keys() As String = {"numbers1", "numbers2", "numbers3"}
        
        For Each key In keys
            ' Violation: ContainsKey before Add with generic value type
            If Not genericDict.ContainsKey(key) Then
                genericDict.Add(key, New List(Of Integer)())
            End If
            
            ' Add some numbers to the list
            genericDict(key).Add(Random.Shared.Next(1, 100))
        Next
    End Sub
    
    Public Sub TestContainsKeyAddInAsyncContext()
        ' Violation: ContainsKey before Add (even in async context)
        Dim asyncDict As New Dictionary(Of String, Task(Of String))()
        Dim taskKey As String = "asyncTask"
        
        If Not asyncDict.ContainsKey(taskKey) Then
            asyncDict.Add(taskKey, Task.FromResult("completed"))
        End If
    End Sub
End Class
