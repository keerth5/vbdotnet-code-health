' Test file for cq-vbn-0084: Identifiers should not have incorrect suffix
' Rule should detect classes with misleading suffixes like 'Stream', 'Event', or 'Attribute' on non-standard types

Imports System
Imports System.IO

' Violation 1: Class with Stream suffix but not inheriting from Stream
Public Class DataStream
    Public Property Data As String
    Public Property Length As Integer
    
    Public Sub WriteData(data As String)
        Me.Data = data
    End Sub
    
    Public Function ReadData() As String
        Return Data
    End Function
End Class

' Violation 2: Class with Event suffix but not an event-related class
Public Class UserEvent
    Public Property UserId As Integer
    Public Property UserName As String
    
    Public Sub ProcessUser()
        Console.WriteLine("Processing user: " & UserName)
    End Sub
End Class

' Violation 3: Class with Attribute suffix but not inheriting from Attribute
Public Class ValidationAttribute
    Public Property IsRequired As Boolean
    Public Property MaxLength As Integer
    
    Public Function Validate(value As String) As Boolean
        If IsRequired AndAlso String.IsNullOrEmpty(value) Then
            Return False
        End If
        If MaxLength > 0 AndAlso value.Length > MaxLength Then
            Return False
        End If
        Return True
    End Function
End Class

' Violation 4: Another class with Stream suffix
Friend Class FileStream
    Public Property FileName As String
    Public Property Content As String
    
    Public Sub Save()
        Console.WriteLine("Saving file: " & FileName)
    End Sub
    
    Public Sub Load()
        Console.WriteLine("Loading file: " & FileName)
    End Sub
End Class

' Violation 5: Class with Event suffix in different context
Public Class ClickEvent
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Button As String
    
    Public Sub HandleClick()
        Console.WriteLine($"Click at ({X}, {Y}) with {Button}")
    End Sub
End Class

' Violation 6: Class with Attribute suffix for configuration
Public Class ConfigAttribute
    Public Property Key As String
    Public Property Value As String
    Public Property IsEnabled As Boolean
    
    Public Sub ApplyConfig()
        Console.WriteLine($"Applying config: {Key} = {Value}")
    End Sub
End Class

' Violation 7: Class with Stream suffix for data processing
Public Class ProcessingStream
    Public Property Items As New List(Of String)
    
    Public Sub AddItem(item As String)
        Items.Add(item)
    End Sub
    
    Public Sub ProcessItems()
        For Each item In Items
            Console.WriteLine("Processing: " & item)
        Next
    End Sub
End Class

' Violation 8: Class with Event suffix for logging
Friend Class LogEvent
    Public Property Message As String
    Public Property Timestamp As DateTime
    Public Property Level As String
    
    Public Sub WriteLog()
        Console.WriteLine($"[{Timestamp}] {Level}: {Message}")
    End Sub
End Class

' Violation 9: Class with Attribute suffix for metadata
Public Class MetadataAttribute
    Public Property Name As String
    Public Property Description As String
    Public Property Version As String
    
    Public Function GetMetadata() As String
        Return $"{Name} v{Version}: {Description}"
    End Function
End Class

' Violation 10: Class with Stream suffix for network data
Public Class NetworkStream
    Public Property Host As String
    Public Property Port As Integer
    Public Property IsConnected As Boolean
    
    Public Sub Connect()
        IsConnected = True
        Console.WriteLine($"Connected to {Host}:{Port}")
    End Sub
    
    Public Sub Disconnect()
        IsConnected = False
        Console.WriteLine("Disconnected")
    End Sub
End Class

' This should NOT be detected - proper Stream implementation
Public Class CustomStream
    Inherits Stream
    
    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property
    
    Public Overrides ReadOnly Property Length As Long
        Get
            Return 0
        End Get
    End Property
    
    Public Overrides Property Position As Long
        Get
            Return 0
        End Get
        Set(value As Long)
            ' Implementation
        End Set
    End Property
    
    Public Overrides Sub Flush()
        ' Implementation
    End Sub
    
    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return 0
    End Function
    
    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return 0
    End Function
    
    Public Overrides Sub SetLength(value As Long)
        ' Implementation
    End Sub
    
    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        ' Implementation
    End Sub
End Class

' This should NOT be detected - proper Attribute implementation
Public Class CustomAttribute
    Inherits Attribute
    
    Public Property Name As String
    
    Public Sub New(name As String)
        Me.Name = name
    End Sub
End Class

' This should NOT be detected - regular class without misleading suffix
Public Class DataProcessor
    Public Sub ProcessData()
        Console.WriteLine("Processing data")
    End Sub
End Class

' This should NOT be detected - regular class
Public Class UserManager
    Public Sub ManageUsers()
        Console.WriteLine("Managing users")
    End Sub
End Class
