' Test file for cq-vbn-0025: Use integral or string argument for indexers
' Rule should detect indexers that don't use Integer, String, Long, or Short as parameters

Imports System

Public Class IndexerExamples
    
    Private _data As New Dictionary(Of String, String)
    Private _items As New List(Of String)
    
    ' Violation 1: Indexer with Double parameter (should use Integer or String)
    Default Public Property Item(index As Double) As String
        Get
            Return _items(CInt(index))
        End Get
        Set(value As String)
            _items(CInt(index)) = value
        End Set
    End Property
    
End Class

Public Class AnotherIndexerExample
    
    Private _values As New Dictionary(Of String, Object)
    
    ' Violation 2: Indexer with Decimal parameter
    Default Protected Property Item(key As Decimal) As Object
        Get
            Return _values(key.ToString())
        End Get
        Set(value As Object)
            _values(key.ToString()) = value
        End Set
    End Property
    
End Class

Public Class ThirdIndexerExample
    
    Private _collection As New List(Of String)
    
    ' Violation 3: Indexer with Boolean parameter
    Default Private Property Item(flag As Boolean) As String
        Get
            Return If(flag, _collection(0), _collection(1))
        End Get
        Set(value As String)
            If flag Then
                _collection(0) = value
            Else
                _collection(1) = value
            End If
        End Set
    End Property
    
End Class

Public Class FourthIndexerExample
    
    Private _data As New Dictionary(Of String, String)
    
    ' Violation 4: Indexer with DateTime parameter
    Default Friend Property Item(date As DateTime) As String
        Get
            Return _data(date.ToString())
        End Get
        Set(value As String)
            _data(date.ToString()) = value
        End Set
    End Property
    
End Class

' This should NOT be detected - indexer with Integer parameter
Public Class ValidIndexerInteger
    
    Private _items As New List(Of String)
    
    Default Public Property Item(index As Integer) As String
        Get
            Return _items(index)
        End Get
        Set(value As String)
            _items(index) = value
        End Set
    End Property
    
End Class

' This should NOT be detected - indexer with String parameter
Public Class ValidIndexerString
    
    Private _data As New Dictionary(Of String, String)
    
    Default Public Property Item(key As String) As String
        Get
            Return _data(key)
        End Get
        Set(value As String)
            _data(key) = value
        End Set
    End Property
    
End Class

' This should NOT be detected - indexer with Long parameter
Public Class ValidIndexerLong
    
    Private _items As New List(Of String)
    
    Default Public Property Item(index As Long) As String
        Get
            Return _items(CInt(index))
        End Get
        Set(value As String)
            _items(CInt(index)) = value
        End Set
    End Property
    
End Class

' This should NOT be detected - indexer with Short parameter
Public Class ValidIndexerShort
    
    Private _items As New List(Of String)
    
    Default Public Property Item(index As Short) As String
        Get
            Return _items(index)
        End Get
        Set(value As String)
            _items(index) = value
        End Set
    End Property
    
End Class
