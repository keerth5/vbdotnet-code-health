' VB.NET test file for cq-vbn-0137: Unnecessary call to 'Dictionary.ContainsKey(key)'
' Rule: There's no need to guard Dictionary.Remove(key) with Dictionary.ContainsKey(key). Dictionary<TKey,TValue>.Remove(TKey) already checks whether the key exists and doesn't throw if it doesn't exist.

Imports System
Imports System.Collections.Generic

Public Class UnnecessaryContainsKeyExamples
    Private _userCache As Dictionary(Of Integer, String)
    Private _settings As Dictionary(Of String, String)
    Private _sessionData As Dictionary(Of String, Object)
    
    Public Sub New()
        _userCache = New Dictionary(Of Integer, String)()
        _settings = New Dictionary(Of String, String)()
        _sessionData = New Dictionary(Of String, Object)()
        
        ' Initialize with some data
        _userCache.Add(1, "Alice")
        _userCache.Add(2, "Bob")
        _settings.Add("theme", "dark")
        _settings.Add("language", "en")
    End Sub
    
    Public Sub TestUnnecessaryContainsKeyBeforeRemove()
        Dim userId As Integer = 1
        
        ' Violation: Unnecessary ContainsKey check before Remove
        If _userCache.ContainsKey(userId) Then
            _userCache.Remove(userId)
        End If
        
        Dim settingKey As String = "theme"
        
        ' Violation: Unnecessary ContainsKey check before Remove
        If _settings.ContainsKey(settingKey) Then
            _settings.Remove(settingKey)
        End If
    End Sub
    
    Public Sub RemoveUserData(userId As Integer)
        ' Violation: Checking ContainsKey before Remove
        If _userCache.ContainsKey(userId) Then
            _userCache.Remove(userId)
            Console.WriteLine($"Removed user {userId}")
        Else
            Console.WriteLine($"User {userId} not found")
        End If
    End Sub
    
    Public Sub RemoveSetting(key As String)
        ' Violation: Unnecessary check before removal
        If _settings.ContainsKey(key) Then
            _settings.Remove(key)
            Console.WriteLine($"Setting '{key}' removed")
        End If
    End Sub
    
    Public Sub ClearExpiredSessions(expiredKeys As List(Of String))
        For Each key In expiredKeys
            ' Violation: ContainsKey check before Remove in loop
            If _sessionData.ContainsKey(key) Then
                _sessionData.Remove(key)
            End If
        Next
    End Sub
    
    Public Sub ProcessUserRemovals(userIds As Integer())
        For Each id In userIds
            ' Violation: Unnecessary ContainsKey check
            If _userCache.ContainsKey(id) Then
                _userCache.Remove(id)
                Console.WriteLine($"User {id} removed from cache")
            End If
        Next
    End Sub
    
    Public Sub UpdateOrRemoveSettings(settingsToProcess As Dictionary(Of String, String))
        For Each kvp In settingsToProcess
            If String.IsNullOrEmpty(kvp.Value) Then
                ' Violation: ContainsKey before Remove
                If _settings.ContainsKey(kvp.Key) Then
                    _settings.Remove(kvp.Key)
                End If
            Else
                _settings(kvp.Key) = kvp.Value
            End If
        Next
    End Sub
    
    Public Sub CleanupCache(keysToRemove As List(Of Integer))
        Console.WriteLine("Starting cache cleanup...")
        
        For Each key In keysToRemove
            ' Violation: Unnecessary ContainsKey check
            If _userCache.ContainsKey(key) Then
                _userCache.Remove(key)
                Console.WriteLine($"Removed key: {key}")
            Else
                Console.WriteLine($"Key {key} not found in cache")
            End If
        Next
        
        Console.WriteLine("Cache cleanup completed")
    End Sub
    
    Public Sub ConditionalRemoval(condition As Boolean, keyToRemove As String)
        If condition Then
            ' Violation: ContainsKey check before Remove
            If _settings.ContainsKey(keyToRemove) Then
                _settings.Remove(keyToRemove)
            End If
        End If
    End Sub
    
    Public Function TryRemoveUser(userId As Integer) As Boolean
        ' Violation: Using ContainsKey before Remove when we could use Remove's return value
        If _userCache.ContainsKey(userId) Then
            _userCache.Remove(userId)
            Return True
        Else
            Return False
        End If
    End Function
    
    Public Sub RemoveMultipleKeys(keys As String())
        For Each key In keys
            ' Violation: Checking existence before removal
            If _sessionData.ContainsKey(key) Then
                _sessionData.Remove(key)
                Console.WriteLine($"Removed session: {key}")
            End If
        Next
    End Sub
    
    Public Sub ProcessDynamicRemovals()
        Dim keysToProcess As String() = {"key1", "key2", "key3", "key4", "key5"}
        
        For Each key In keysToProcess
            ' Violation: Unnecessary ContainsKey call
            If _settings.ContainsKey(key) Then
                Dim removed As Boolean = _settings.Remove(key)
                Console.WriteLine($"Key {key} removal result: {removed}")
            End If
        Next
    End Sub
    
    Public Sub RemoveWithLogging(dictionary As Dictionary(Of String, Object), keyToRemove As String)
        ' Violation: ContainsKey check before Remove with logging
        If dictionary.ContainsKey(keyToRemove) Then
            dictionary.Remove(keyToRemove)
            Console.WriteLine($"Successfully removed key: {keyToRemove}")
        Else
            Console.WriteLine($"Key not found: {keyToRemove}")
        End If
    End Sub
    
    Public Sub BatchRemoveOperations(itemsToRemove As List(Of String))
        Dim removedCount As Integer = 0
        
        For Each item In itemsToRemove
            ' Violation: ContainsKey before Remove
            If _sessionData.ContainsKey(item) Then
                _sessionData.Remove(item)
                removedCount += 1
            End If
        Next
        
        Console.WriteLine($"Removed {removedCount} items from session data")
    End Sub
    
    Public Sub ComplexRemovalLogic(userId As Integer, settingKey As String)
        ' Violation: Multiple unnecessary ContainsKey calls
        If _userCache.ContainsKey(userId) Then
            _userCache.Remove(userId)
            
            If _settings.ContainsKey(settingKey) Then
                _settings.Remove(settingKey)
            End If
        End If
    End Sub
    
    Public Sub RemoveIfExists(dictionary As Dictionary(Of Integer, String), key As Integer)
        ' Violation: The method name suggests checking existence, but Remove already does this
        If dictionary.ContainsKey(key) Then
            dictionary.Remove(key)
        End If
    End Sub
    
    ' Examples of correct usage (for reference)
    Public Sub CorrectRemovalPattern(userId As Integer)
        ' Correct: Direct removal, Remove returns boolean indicating success
        Dim wasRemoved As Boolean = _userCache.Remove(userId)
        If wasRemoved Then
            Console.WriteLine($"User {userId} was removed")
        Else
            Console.WriteLine($"User {userId} was not found")
        End If
    End Sub
    
    Public Sub CorrectBatchRemoval(keysToRemove As List(Of String))
        Dim removedCount As Integer = 0
        
        For Each key In keysToRemove
            ' Correct: Use Remove's return value
            If _settings.Remove(key) Then
                removedCount += 1
            End If
        Next
        
        Console.WriteLine($"Removed {removedCount} settings")
    End Sub
    
    Public Sub CorrectConditionalAccess(key As String)
        ' Correct: ContainsKey is appropriate when we need to read the value
        If _settings.ContainsKey(key) Then
            Dim value As String = _settings(key)
            Console.WriteLine($"Setting {key} has value: {value}")
            ' Only remove after using the value
            _settings.Remove(key)
        End If
    End Sub
    
    Public Sub TestEdgeCases()
        Dim tempDict As New Dictionary(Of String, Integer)()
        tempDict.Add("a", 1)
        tempDict.Add("b", 2)
        
        ' Violation: Unnecessary ContainsKey in local dictionary
        If tempDict.ContainsKey("a") Then
            tempDict.Remove("a")
        End If
        
        ' Violation: ContainsKey with variable key
        Dim keyToRemove As String = "b"
        If tempDict.ContainsKey(keyToRemove) Then
            tempDict.Remove(keyToRemove)
        End If
    End Sub
End Class
