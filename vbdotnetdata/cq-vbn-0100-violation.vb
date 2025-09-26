' Test file for cq-vbn-0100: Avoid uninstantiated internal classes
' Rule should detect Friend (internal) classes that are never instantiated

Imports System

' Violation 1: Friend class that might not be instantiated
Friend Class UnusedInternalClass
    
    Public Property Name As String
    
    Public Sub New()
        Name = "Unused"
    End Sub
    
    Public Sub ProcessData()
        Console.WriteLine("Processing data in unused class")
    End Sub
    
End Class

' Violation 2: Another Friend class that might not be instantiated
Friend Class InternalHelper
    
    Private _value As Integer
    
    Public Sub New(value As Integer)
        _value = value
    End Sub
    
    Public Function Calculate() As Integer
        Return _value * 2
    End Function
    
End Class

' Violation 3: Friend class with static members only (might still be uninstantiated)
Friend Class StaticUtilityClass
    
    Public Shared Function FormatString(input As String) As String
        Return input.ToUpper()
    End Function
    
    Public Shared Sub LogMessage(message As String)
        Console.WriteLine($"Log: {message}")
    End Sub
    
    ' Private constructor to prevent instantiation
    Private Sub New()
    End Sub
    
End Class

' Violation 4: Friend class that appears unused
Friend Class ConfigurationReader
    
    Private _configPath As String
    
    Public Sub New(configPath As String)
        _configPath = configPath
    End Sub
    
    Public Function ReadConfiguration() As Dictionary(Of String, String)
        Return New Dictionary(Of String, String)()
    End Function
    
End Class

' Violation 5: Friend class with complex functionality but potentially unused
Friend Class DataProcessor
    
    Private _data As List(Of String)
    
    Public Sub New()
        _data = New List(Of String)()
    End Sub
    
    Public Sub AddData(item As String)
        _data.Add(item)
    End Sub
    
    Public Function ProcessData() As String()
        Return _data.ToArray()
    End Function
    
    Public Sub ClearData()
        _data.Clear()
    End Sub
    
End Class

' This should NOT be detected - Public class
Public Class PublicClass
    
    Public Property Value As String
    
    Public Sub New()
        Value = "Public"
    End Sub
    
End Class

' This should NOT be detected - Private class (different scope)
Private Class PrivateClass
    
    Public Property Id As Integer
    
    Public Sub New()
        Id = 1
    End Sub
    
End Class

' Violation 6: Friend class that might be unused
Friend Class CacheManager
    
    Private Shared _cache As New Dictionary(Of String, Object)()
    
    Public Shared Sub AddToCache(key As String, value As Object)
        _cache(key) = value
    End Sub
    
    Public Shared Function GetFromCache(key As String) As Object
        Return If(_cache.ContainsKey(key), _cache(key), Nothing)
    End Function
    
End Class

' Violation 7: Friend class with inheritance
Friend Class BaseInternalClass
    
    Protected Property BaseValue As String
    
    Public Sub New()
        BaseValue = "Base"
    End Sub
    
    Public Overridable Sub DoWork()
        Console.WriteLine("Base work")
    End Sub
    
End Class

' Violation 8: Friend class that inherits from base
Friend Class DerivedInternalClass
    Inherits BaseInternalClass
    
    Public Sub New()
        MyBase.New()
    End Sub
    
    Public Overrides Sub DoWork()
        Console.WriteLine("Derived work")
    End Sub
    
End Class

' Violation 9: Friend class implementing interface
Friend Class InternalImplementation
    Implements IDisposable
    
    Private _disposed As Boolean = False
    
    Public Sub Dispose() Implements IDisposable.Dispose
        If Not _disposed Then
            ' Cleanup code
            _disposed = True
        End If
    End Sub
    
    Public Sub DoSomething()
        If _disposed Then
            Throw New ObjectDisposedException("InternalImplementation")
        End If
        Console.WriteLine("Doing something")
    End Sub
    
End Class

' Violation 10: Friend class with generic parameters
Friend Class GenericInternalClass(Of T)
    
    Private _items As List(Of T)
    
    Public Sub New()
        _items = New List(Of T)()
    End Sub
    
    Public Sub Add(item As T)
        _items.Add(item)
    End Sub
    
    Public Function GetAll() As T()
        Return _items.ToArray()
    End Function
    
End Class

' This should NOT be detected - Protected class (different access level)
Protected Class ProtectedClass
    
    Public Property Name As String
    
    Public Sub New()
        Name = "Protected"
    End Sub
    
End Class

' Violation 11: Friend class with events
Friend Class EventPublisher
    
    Public Event DataReceived As EventHandler(Of String)
    
    Public Sub PublishData(data As String)
        RaiseEvent DataReceived(Me, data)
    End Sub
    
End Class

' Violation 12: Friend class with nested types
Friend Class ContainerClass
    
    Public Property Name As String
    
    Public Class NestedPublicClass
        Public Property Value As Integer
    End Class
    
    Friend Class NestedFriendClass
        Public Property Description As String
    End Class
    
End Class

' Violation 13: Friend class with delegates
Friend Class DelegateExample
    
    Public Delegate Function ProcessDelegate(input As String) As String
    
    Public Function ProcessData(processor As ProcessDelegate, data As String) As String
        Return processor(data)
    End Function
    
End Class

' Violation 14: Friend class with operators
Friend Class OperatorExample
    
    Private _value As Integer
    
    Public Sub New(value As Integer)
        _value = value
    End Sub
    
    Public Shared Operator +(left As OperatorExample, right As OperatorExample) As OperatorExample
        Return New OperatorExample(left._value + right._value)
    End Operator
    
    Public ReadOnly Property Value As Integer
        Get
            Return _value
        End Get
    End Property
    
End Class

' Violation 15: Friend class that might be a utility class
Friend Class FileHelper
    
    Public Shared Function ReadAllText(filePath As String) As String
        Return System.IO.File.ReadAllText(filePath)
    End Function
    
    Public Shared Sub WriteAllText(filePath As String, content As String)
        System.IO.File.WriteAllText(filePath, content)
    End Sub
    
End Class
