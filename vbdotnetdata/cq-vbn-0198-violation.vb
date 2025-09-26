' Test file for cq-vbn-0198: Unsafe DataSet or DataTable in serializable type can be vulnerable to remote code execution attacks
' This rule detects classes marked with SerializableAttribute that contain DataSet or DataTable fields/properties

Imports System
Imports System.Data
Imports System.Runtime.Serialization

' Violation: Serializable class with DataSet property
<Serializable>
Public Class SerializableClassWithDataSetProperty
    Public Property MyDataSet As DataSet ' Violation
End Class

' Violation: Serializable class with DataTable property
<Serializable>
Public Class SerializableClassWithDataTableProperty
    Public Property MyDataTable As DataTable ' Violation
End Class

' Violation: Serializable class with DataSet field
<Serializable>
Public Class SerializableClassWithDataSetField
    Public MyDataSet As DataSet ' Violation
End Class

' Violation: Serializable class with DataTable field
<Serializable>
Public Class SerializableClassWithDataTableField
    Public MyDataTable As DataTable ' Violation
End Class

' Violation: Serializable class with Protected DataSet property
<Serializable>
Public Class SerializableClassWithProtectedDataSet
    Protected Property MyDataSet As DataSet ' Violation
End Class

' Violation: Serializable class with Protected DataTable field
<Serializable>
Public Class SerializableClassWithProtectedDataTable
    Protected MyDataTable As DataTable ' Violation
End Class

' Violation: Serializable class with Friend DataSet property
<Serializable>
Public Class SerializableClassWithFriendDataSet
    Friend Property MyDataSet As DataSet ' Violation
End Class

' Violation: Serializable class with Friend DataTable field
<Serializable>
Public Class SerializableClassWithFriendDataTable
    Friend MyDataTable As DataTable ' Violation
End Class

' Violation: Serializable class with multiple DataSet and DataTable members
<Serializable>
Public Class SerializableClassWithMultipleDataMembers
    Public Property DataSet1 As DataSet ' Violation
    Public Property DataTable1 As DataTable ' Violation
    Public DataSet2 As DataSet ' Violation
    Public DataTable2 As DataTable ' Violation
End Class

' Violation: Serializable structure with DataSet property
<Serializable>
Public Structure SerializableStructWithDataSet
    Public Property MyDataSet As DataSet ' Violation
End Structure

' Violation: Serializable structure with DataTable field
<Serializable>
Public Structure SerializableStructWithDataTable
    Public MyDataTable As DataTable ' Violation
End Structure

' Violation: Friend serializable class with DataSet
<Serializable>
Friend Class FriendSerializableClassWithDataSet
    Public Property MyDataSet As DataSet ' Violation
End Class

' Violation: Serializable class with DataSet and other properties
<Serializable>
Public Class SerializableClassWithMixedProperties
    Public Property Name As String
    Public Property Age As Integer
    Public Property MyDataSet As DataSet ' Violation
    Public Property Description As String
End Class

' Non-violation: Serializable class without DataSet or DataTable (should not be detected)
<Serializable>
Public Class SafeSerializableClass
    Public Property Name As String
    Public Property Age As Integer
    Public Property Description As String
End Class

' Non-violation: Non-serializable class with DataSet (should not be detected)
Public Class NonSerializableClassWithDataSet
    Public Property MyDataSet As DataSet ' No violation - not serializable
End Class

' Non-violation: Serializable class with private DataSet (should not be detected by this pattern)
<Serializable>
Public Class SerializableClassWithPrivateDataSet
    Private myDataSet As DataSet ' No violation - private member
End Class

' Violation: Serializable class with DataSet in inheritance scenario
<Serializable>
Public Class BaseSerializableClassWithDataSet
    Public Property BaseDataSet As DataSet ' Violation
End Class

Public Class DerivedClass
    Inherits BaseSerializableClassWithDataSet
    Public Property DerivedProperty As String
End Class

' Violation: Serializable class with DataTable in nested scenario
<Serializable>
Public Class OuterSerializableClass
    Public Property OuterDataTable As DataTable ' Violation
    
    <Serializable>
    Public Class NestedSerializableClass
        Public Property NestedDataSet As DataSet ' Violation
    End Class
End Class

' Violation: Serializable class with DataSet property with custom getter/setter
<Serializable>
Public Class SerializableClassWithCustomDataSetProperty
    Private _dataSet As DataSet
    
    Public Property MyDataSet As DataSet ' Violation
        Get
            Return _dataSet
        End Get
        Set(value As DataSet)
            _dataSet = value
        End Set
    End Property
End Class

' Violation: Serializable class with readonly DataTable field
<Serializable>
Public Class SerializableClassWithReadOnlyDataTable
    Public ReadOnly MyDataTable As DataTable ' Violation
    
    Public Sub New()
        MyDataTable = New DataTable()
    End Sub
End Class

' Violation: Serializable class with shared DataSet field
<Serializable>
Public Class SerializableClassWithSharedDataSet
    Public Shared MyDataSet As DataSet ' Violation
End Class

Public Class NonSerializableViolations
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about <Serializable> Public Class with DataSet property
        Dim message As String = "Serializable classes with DataSet can be vulnerable"
        Console.WriteLine("Avoid DataTable in serializable types")
    End Sub

End Class
