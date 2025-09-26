' Test file for cq-vbn-0199: Unsafe DataSet or DataTable in serializable type
' This rule detects classes marked with XML serialization or data contract attributes that contain DataSet or DataTable

Imports System
Imports System.Data
Imports System.Runtime.Serialization
Imports System.Xml.Serialization

' Violation: XmlRoot class with DataSet property
<XmlRoot("RootElement")>
Public Class XmlRootClassWithDataSet
    Public Property MyDataSet As DataSet ' Violation
End Class

' Violation: XmlType class with DataTable property
<XmlType("TypeElement")>
Public Class XmlTypeClassWithDataTable
    Public Property MyDataTable As DataTable ' Violation
End Class

' Violation: DataContract class with DataSet field
<DataContract>
Public Class DataContractClassWithDataSet
    Public MyDataSet As DataSet ' Violation
End Class

' Violation: DataContract class with DataTable property
<DataContract>
Public Class DataContractClassWithDataTable
    Public Property MyDataTable As DataTable ' Violation
End Class

' Violation: DataMember class with DataSet property
<DataMember>
Public Class DataMemberClassWithDataSet
    Public Property MyDataSet As DataSet ' Violation
End Class

' Violation: DataMember class with DataTable field
<DataMember>
Public Class DataMemberClassWithDataTable
    Public MyDataTable As DataTable ' Violation
End Class

' Violation: XmlRoot class with Protected DataSet property
<XmlRoot("ProtectedRoot")>
Public Class XmlRootClassWithProtectedDataSet
    Protected Property MyDataSet As DataSet ' Violation
End Class

' Violation: XmlType class with Protected DataTable field
<XmlType("ProtectedType")>
Public Class XmlTypeClassWithProtectedDataTable
    Protected MyDataTable As DataTable ' Violation
End Class

' Violation: DataContract class with Friend DataSet property
<DataContract>
Public Class DataContractClassWithFriendDataSet
    Friend Property MyDataSet As DataSet ' Violation
End Class

' Violation: DataContract class with Friend DataTable field
<DataContract>
Public Class DataContractClassWithFriendDataTable
    Friend MyDataTable As DataTable ' Violation
End Class

' Violation: XmlRoot class with multiple DataSet and DataTable members
<XmlRoot("MultipleRoot")>
Public Class XmlRootClassWithMultipleDataMembers
    Public Property DataSet1 As DataSet ' Violation
    Public Property DataTable1 As DataTable ' Violation
    Public DataSet2 As DataSet ' Violation
    Public DataTable2 As DataTable ' Violation
End Class

' Violation: DataContract structure with DataSet property
<DataContract>
Public Structure DataContractStructWithDataSet
    Public Property MyDataSet As DataSet ' Violation
End Structure

' Violation: XmlType structure with DataTable field
<XmlType("StructType")>
Public Structure XmlTypeStructWithDataTable
    Public MyDataTable As DataTable ' Violation
End Structure

' Violation: Friend XmlRoot class with DataSet
<XmlRoot("FriendRoot")>
Friend Class FriendXmlRootClassWithDataSet
    Public Property MyDataSet As DataSet ' Violation
End Class

' Violation: DataContract class with DataSet and other properties
<DataContract>
Public Class DataContractClassWithMixedProperties
    Public Property Name As String
    Public Property Age As Integer
    Public Property MyDataSet As DataSet ' Violation
    Public Property Description As String
End Class

' Violation: XmlRoot class with namespace and DataTable
<XmlRoot("RootWithNamespace", Namespace:="http://example.com")>
Public Class XmlRootClassWithNamespaceAndDataTable
    Public Property MyDataTable As DataTable ' Violation
End Class

' Violation: DataContract class with namespace and DataSet
<DataContract(Namespace:="http://example.com")>
Public Class DataContractClassWithNamespaceAndDataSet
    Public Property MyDataSet As DataSet ' Violation
End Class

' Non-violation: XmlRoot class without DataSet or DataTable (should not be detected)
<XmlRoot("SafeRoot")>
Public Class SafeXmlRootClass
    Public Property Name As String
    Public Property Age As Integer
    Public Property Description As String
End Class

' Non-violation: DataContract class without DataSet or DataTable (should not be detected)
<DataContract>
Public Class SafeDataContractClass
    Public Property Name As String
    Public Property Age As Integer
    Public Property Description As String
End Class

' Non-violation: Non-attributed class with DataSet (should not be detected)
Public Class NonAttributedClassWithDataSet
    Public Property MyDataSet As DataSet ' No violation - no serialization attributes
End Class

' Non-violation: XmlRoot class with private DataSet (should not be detected by this pattern)
<XmlRoot("PrivateRoot")>
Public Class XmlRootClassWithPrivateDataSet
    Private myDataSet As DataSet ' No violation - private member
End Class

' Violation: XmlRoot class with DataSet in inheritance scenario
<XmlRoot("BaseRoot")>
Public Class BaseXmlRootClassWithDataSet
    Public Property BaseDataSet As DataSet ' Violation
End Class

Public Class DerivedXmlClass
    Inherits BaseXmlRootClassWithDataSet
    Public Property DerivedProperty As String
End Class

' Violation: DataContract class with DataTable in nested scenario
<DataContract>
Public Class OuterDataContractClass
    Public Property OuterDataTable As DataTable ' Violation
    
    <DataContract>
    Public Class NestedDataContractClass
        Public Property NestedDataSet As DataSet ' Violation
    End Class
End Class

' Violation: XmlType class with DataSet property with custom getter/setter
<XmlType("CustomType")>
Public Class XmlTypeClassWithCustomDataSetProperty
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

' Violation: DataContract class with readonly DataTable field
<DataContract>
Public Class DataContractClassWithReadOnlyDataTable
    Public ReadOnly MyDataTable As DataTable ' Violation
    
    Public Sub New()
        MyDataTable = New DataTable()
    End Sub
End Class

' Violation: XmlRoot class with shared DataSet field
<XmlRoot("SharedRoot")>
Public Class XmlRootClassWithSharedDataSet
    Public Shared MyDataSet As DataSet ' Violation
End Class

' Violation: Multiple attributes on class with DataSet
<XmlRoot("MultiAttrRoot")>
<DataContract>
Public Class MultiAttributeClassWithDataSet
    Public Property MyDataSet As DataSet ' Violation
End Class

Public Class NonAttributedViolations
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about <XmlRoot> Public Class with DataSet property
        Dim message As String = "XML serializable classes with DataSet can be vulnerable"
        Console.WriteLine("Avoid DataTable in DataContract types")
    End Sub

End Class
