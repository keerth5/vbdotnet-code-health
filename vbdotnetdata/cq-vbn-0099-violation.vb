' Test file for cq-vbn-0099: Initialize reference type static fields inline
' Rule should detect classes with static constructors that initialize static fields

Imports System

' Violation 1: Class with static constructor and static fields
Public Class StaticInitializationExample
    
    Private Shared _instance As StaticInitializationExample
    Private Shared _connectionString As String
    Private Shared _isInitialized As Boolean
    
    Shared Sub New()
        _instance = New StaticInitializationExample()
        _connectionString = "Server=localhost;Database=Test"
        _isInitialized = True
    End Sub
    
    Public Sub New()
        ' Instance constructor
    End Sub
    
    Public Shared Property Instance As StaticInitializationExample
        Get
            Return _instance
        End Get
    End Property
    
End Class

' Violation 2: Another class with static constructor
Friend Class ConfigurationManager
    
    Private Shared _settings As Dictionary(Of String, String)
    Private Shared _defaultTimeout As Integer
    
    Shared Sub New()
        _settings = New Dictionary(Of String, String)()
        _defaultTimeout = 30
    End Sub
    
    Public Shared Function GetSetting(key As String) As String
        Return _settings(key)
    End Function
    
End Class

' Violation 3: Class with static constructor initializing reference types
Public Class CacheManager
    
    Private Shared _cache As Dictionary(Of String, Object)
    Private Shared _lockObject As Object
    
    Shared Sub New()
        _cache = New Dictionary(Of String, Object)()
        _lockObject = New Object()
    End Sub
    
    Public Shared Sub AddToCache(key As String, value As Object)
        SyncLock _lockObject
            _cache(key) = value
        End SyncLock
    End Sub
    
End Class

' Violation 4: Module with static constructor
Public Module UtilityModule
    
    Private _logger As Object
    Private _initialized As Boolean
    
    Sub New()
        _logger = CreateLogger()
        _initialized = True
    End Sub
    
    Private Function CreateLogger() As Object
        Return New Object()
    End Function
    
End Module

' This should NOT be detected - no static constructor
Public Class NoStaticConstructor
    
    Private Shared _instance As NoStaticConstructor = New NoStaticConstructor()
    Private Shared ReadOnly _connectionString As String = "DefaultConnection"
    
    Public Sub New()
        ' Only instance constructor
    End Sub
    
End Class

' This should NOT be detected - static constructor without static fields
Public Class StaticConstructorOnly
    
    Shared Sub New()
        ' Just initialization logic, no static fields
        Console.WriteLine("Static constructor called")
    End Sub
    
    Public Sub New()
        ' Instance constructor
    End Sub
    
End Class

' Violation 5: Class with static constructor and multiple static fields
Protected Class DatabaseConnection
    
    Private Shared _connection As Object
    Private Shared _connectionPool As List(Of Object)
    Private Shared _maxConnections As Integer
    
    Shared Sub New()
        _connection = CreateConnection()
        _connectionPool = New List(Of Object)()
        _maxConnections = 10
    End Sub
    
    Private Shared Function CreateConnection() As Object
        Return New Object()
    End Function
    
    Public Sub New()
        ' Instance constructor
    End Sub
    
End Class

' Violation 6: Class with static constructor in different order
Public Class ServiceLocator
    
    Shared Sub New()
        _services = New Dictionary(Of String, Object)()
        _isConfigured = False
    End Sub
    
    Private Shared _services As Dictionary(Of String, Object)
    Private Shared _isConfigured As Boolean
    
    Public Shared Sub RegisterService(name As String, service As Object)
        _services(name) = service
    End Sub
    
End Class

' This should NOT be detected - fields initialized inline
Public Class InlineInitialization
    
    Private Shared ReadOnly _instance As New InlineInitialization()
    Private Shared _counter As Integer = 0
    Private Shared _name As String = "Default"
    
    Public Sub New()
        ' Instance constructor only
    End Sub
    
End Class

' Violation 7: Generic class with static constructor
Public Class GenericStaticExample(Of T)
    
    Private Shared _defaultValue As T
    Private Shared _comparer As Object
    
    Shared Sub New()
        _defaultValue = Nothing
        _comparer = CreateComparer()
    End Sub
    
    Private Shared Function CreateComparer() As Object
        Return New Object()
    End Function
    
End Class

' Violation 8: Class with static constructor and mixed field types
Friend Class MixedFieldTypes
    
    Private Shared _referenceField As Object
    Private Shared _valueField As Integer
    Private Shared _stringField As String
    
    Shared Sub New()
        _referenceField = New Object()
        _valueField = 42
        _stringField = "Initialized"
    End Sub
    
    Public Sub New()
        ' Instance constructor
    End Sub
    
End Class

' This should NOT be detected - no static fields, only instance fields
Public Class InstanceFieldsOnly
    
    Private _instanceField As String
    Private _anotherField As Integer
    
    Shared Sub New()
        ' Static constructor but no static fields
        Console.WriteLine("Static initialization")
    End Sub
    
    Public Sub New()
        _instanceField = "Instance"
        _anotherField = 1
    End Sub
    
End Class

' Violation 9: Class with nested static initialization
Public Class NestedStaticExample
    
    Private Shared _outerField As Object
    
    Shared Sub New()
        _outerField = New InnerClass()
    End Sub
    
    Private Class InnerClass
        Private Shared _innerField As String
        
        Shared Sub New()
            _innerField = "Inner"
        End Sub
    End Class
    
End Class
