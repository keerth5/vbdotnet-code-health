' Test file for cq-vbn-0083: Identifiers should have correct suffix
' Rule should detect collection types that should have appropriate suffixes like 'Collection' or 'Dictionary'

Imports System
Imports System.Collections.Generic
Imports System.Collections

' Violation 1: Class implementing ICollection without Collection suffix
Public Class UserData
    Implements ICollection(Of String)
    
    Private _items As New List(Of String)
    
    Public ReadOnly Property Count As Integer Implements ICollection(Of String).Count
        Get
            Return _items.Count
        End Get
    End Property
    
    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of String).IsReadOnly
        Get
            Return False
        End Get
    End Property
    
    Public Sub Add(item As String) Implements ICollection(Of String).Add
        _items.Add(item)
    End Sub
    
    Public Sub Clear() Implements ICollection(Of String).Clear
        _items.Clear()
    End Sub
    
    Public Function Contains(item As String) As Boolean Implements ICollection(Of String).Contains
        Return _items.Contains(item)
    End Function
    
    Public Sub CopyTo(array() As String, arrayIndex As Integer) Implements ICollection(Of String).CopyTo
        _items.CopyTo(array, arrayIndex)
    End Sub
    
    Public Function Remove(item As String) As Boolean Implements ICollection(Of String).Remove
        Return _items.Remove(item)
    End Function
    
    Public Function GetEnumerator() As IEnumerator(Of String) Implements IEnumerable(Of String).GetEnumerator
        Return _items.GetEnumerator()
    End Function
    
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class

' Violation 2: Class implementing IDictionary without Dictionary suffix
Public Class KeyValueStore
    Implements IDictionary(Of String, Integer)
    
    Private _dict As New Dictionary(Of String, Integer)
    
    Public Property Item(key As String) As Integer Implements IDictionary(Of String, Integer).Item
        Get
            Return _dict(key)
        End Get
        Set(value As Integer)
            _dict(key) = value
        End Set
    End Property
    
    Public ReadOnly Property Keys As ICollection(Of String) Implements IDictionary(Of String, Integer).Keys
        Get
            Return _dict.Keys
        End Get
    End Property
    
    Public ReadOnly Property Values As ICollection(Of Integer) Implements IDictionary(Of String, Integer).Values
        Get
            Return _dict.Values
        End Get
    End Property
    
    Public ReadOnly Property Count As Integer Implements ICollection(Of KeyValuePair(Of String, Integer)).Count
        Get
            Return _dict.Count
        End Get
    End Property
    
    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of KeyValuePair(Of String, Integer)).IsReadOnly
        Get
            Return False
        End Get
    End Property
    
    Public Sub Add(key As String, value As Integer) Implements IDictionary(Of String, Integer).Add
        _dict.Add(key, value)
    End Sub
    
    Public Sub Add(item As KeyValuePair(Of String, Integer)) Implements ICollection(Of KeyValuePair(Of String, Integer)).Add
        _dict.Add(item.Key, item.Value)
    End Sub
    
    Public Sub Clear() Implements ICollection(Of KeyValuePair(Of String, Integer)).Clear
        _dict.Clear()
    End Sub
    
    Public Function Contains(item As KeyValuePair(Of String, Integer)) As Boolean Implements ICollection(Of KeyValuePair(Of String, Integer)).Contains
        Return _dict.Contains(item)
    End Function
    
    Public Function ContainsKey(key As String) As Boolean Implements IDictionary(Of String, Integer).ContainsKey
        Return _dict.ContainsKey(key)
    End Function
    
    Public Sub CopyTo(array() As KeyValuePair(Of String, Integer), arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of String, Integer)).CopyTo
        CType(_dict, ICollection(Of KeyValuePair(Of String, Integer))).CopyTo(array, arrayIndex)
    End Sub
    
    Public Function Remove(key As String) As Boolean Implements IDictionary(Of String, Integer).Remove
        Return _dict.Remove(key)
    End Function
    
    Public Function Remove(item As KeyValuePair(Of String, Integer)) As Boolean Implements ICollection(Of KeyValuePair(Of String, Integer)).Remove
        Return _dict.Remove(item.Key)
    End Function
    
    Public Function TryGetValue(key As String, ByRef value As Integer) As Boolean Implements IDictionary(Of String, Integer).TryGetValue
        Return _dict.TryGetValue(key, value)
    End Function
    
    Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of String, Integer)) Implements IEnumerable(Of KeyValuePair(Of String, Integer)).GetEnumerator
        Return _dict.GetEnumerator()
    End Function
    
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class

' Violation 3: Class implementing IList without List suffix
Public Class ItemContainer
    Implements IList(Of String)
    
    Private _items As New List(Of String)
    
    Default Public Property Item(index As Integer) As String Implements IList(Of String).Item
        Get
            Return _items(index)
        End Get
        Set(value As String)
            _items(index) = value
        End Set
    End Property
    
    Public ReadOnly Property Count As Integer Implements ICollection(Of String).Count
        Get
            Return _items.Count
        End Get
    End Property
    
    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of String).IsReadOnly
        Get
            Return False
        End Get
    End Property
    
    Public Sub Add(item As String) Implements ICollection(Of String).Add
        _items.Add(item)
    End Sub
    
    Public Sub Clear() Implements ICollection(Of String).Clear
        _items.Clear()
    End Sub
    
    Public Function Contains(item As String) As Boolean Implements ICollection(Of String).Contains
        Return _items.Contains(item)
    End Function
    
    Public Sub CopyTo(array() As String, arrayIndex As Integer) Implements ICollection(Of String).CopyTo
        _items.CopyTo(array, arrayIndex)
    End Sub
    
    Public Function IndexOf(item As String) As Integer Implements IList(Of String).IndexOf
        Return _items.IndexOf(item)
    End Function
    
    Public Sub Insert(index As Integer, item As String) Implements IList(Of String).Insert
        _items.Insert(index, item)
    End Sub
    
    Public Function Remove(item As String) As Boolean Implements ICollection(Of String).Remove
        Return _items.Remove(item)
    End Function
    
    Public Sub RemoveAt(index As Integer) Implements IList(Of String).RemoveAt
        _items.RemoveAt(index)
    End Sub
    
    Public Function GetEnumerator() As IEnumerator(Of String) Implements IEnumerable(Of String).GetEnumerator
        Return _items.GetEnumerator()
    End Function
    
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class

' Violation 4: Class implementing ISet without Set suffix
Friend Class UniqueItems
    Implements ISet(Of Integer)
    
    Private _set As New HashSet(Of Integer)
    
    Public ReadOnly Property Count As Integer Implements ICollection(Of Integer).Count
        Get
            Return _set.Count
        End Get
    End Property
    
    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Integer).IsReadOnly
        Get
            Return False
        End Get
    End Property
    
    Public Sub Add(item As Integer) Implements ICollection(Of Integer).Add
        _set.Add(item)
    End Sub
    
    Public Function Add(item As Integer) As Boolean Implements ISet(Of Integer).Add
        Return _set.Add(item)
    End Function
    
    Public Sub Clear() Implements ICollection(Of Integer).Clear
        _set.Clear()
    End Sub
    
    Public Function Contains(item As Integer) As Boolean Implements ICollection(Of Integer).Contains
        Return _set.Contains(item)
    End Function
    
    Public Sub CopyTo(array() As Integer, arrayIndex As Integer) Implements ICollection(Of Integer).CopyTo
        _set.CopyTo(array, arrayIndex)
    End Sub
    
    Public Sub ExceptWith(other As IEnumerable(Of Integer)) Implements ISet(Of Integer).ExceptWith
        _set.ExceptWith(other)
    End Sub
    
    Public Sub IntersectWith(other As IEnumerable(Of Integer)) Implements ISet(Of Integer).IntersectWith
        _set.IntersectWith(other)
    End Sub
    
    Public Function IsProperSubsetOf(other As IEnumerable(Of Integer)) As Boolean Implements ISet(Of Integer).IsProperSubsetOf
        Return _set.IsProperSubsetOf(other)
    End Function
    
    Public Function IsProperSupersetOf(other As IEnumerable(Of Integer)) As Boolean Implements ISet(Of Integer).IsProperSupersetOf
        Return _set.IsProperSupersetOf(other)
    End Function
    
    Public Function IsSubsetOf(other As IEnumerable(Of Integer)) As Boolean Implements ISet(Of Integer).IsSubsetOf
        Return _set.IsSubsetOf(other)
    End Function
    
    Public Function IsSupersetOf(other As IEnumerable(Of Integer)) As Boolean Implements ISet(Of Integer).IsSupersetOf
        Return _set.IsSupersetOf(other)
    End Function
    
    Public Function Overlaps(other As IEnumerable(Of Integer)) As Boolean Implements ISet(Of Integer).Overlaps
        Return _set.Overlaps(other)
    End Function
    
    Public Function Remove(item As Integer) As Boolean Implements ICollection(Of Integer).Remove
        Return _set.Remove(item)
    End Function
    
    Public Function SetEquals(other As IEnumerable(Of Integer)) As Boolean Implements ISet(Of Integer).SetEquals
        Return _set.SetEquals(other)
    End Function
    
    Public Sub SymmetricExceptWith(other As IEnumerable(Of Integer)) Implements ISet(Of Integer).SymmetricExceptWith
        _set.SymmetricExceptWith(other)
    End Sub
    
    Public Sub UnionWith(other As IEnumerable(Of Integer)) Implements ISet(Of Integer).UnionWith
        _set.UnionWith(other)
    End Sub
    
    Public Function GetEnumerator() As IEnumerator(Of Integer) Implements IEnumerable(Of Integer).GetEnumerator
        Return _set.GetEnumerator()
    End Function
    
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class

' This should NOT be detected - proper naming with Collection suffix
Public Class UserDataCollection
    Implements ICollection(Of String)
    
    Private _items As New List(Of String)
    
    Public ReadOnly Property Count As Integer Implements ICollection(Of String).Count
        Get
            Return _items.Count
        End Get
    End Property
    
    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of String).IsReadOnly
        Get
            Return False
        End Get
    End Property
    
    Public Sub Add(item As String) Implements ICollection(Of String).Add
        _items.Add(item)
    End Sub
    
    Public Sub Clear() Implements ICollection(Of String).Clear
        _items.Clear()
    End Sub
    
    Public Function Contains(item As String) As Boolean Implements ICollection(Of String).Contains
        Return _items.Contains(item)
    End Function
    
    Public Sub CopyTo(array() As String, arrayIndex As Integer) Implements ICollection(Of String).CopyTo
        _items.CopyTo(array, arrayIndex)
    End Sub
    
    Public Function Remove(item As String) As Boolean Implements ICollection(Of String).Remove
        Return _items.Remove(item)
    End Function
    
    Public Function GetEnumerator() As IEnumerator(Of String) Implements IEnumerable(Of String).GetEnumerator
        Return _items.GetEnumerator()
    End Function
    
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class

' This should NOT be detected - class not implementing collection interfaces
Public Class RegularClass
    Public Property Name As String
    Public Property Value As Integer
End Class
