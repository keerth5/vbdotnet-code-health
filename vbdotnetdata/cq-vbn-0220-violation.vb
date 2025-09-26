' Test file for cq-vbn-0220: Do Not Serialize Types With Pointer Fields
' This rule detects serializable classes with pointer fields (IntPtr)

Imports System
Imports System.Runtime.Serialization

' Violation: Serializable class with IntPtr property
<Serializable>
Public Class SerializableClassWithIntPtrProperty
    Public Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
End Class

' Violation: Serializable class with IntPtr field
<Serializable>
Public Class SerializableClassWithIntPtrField
    Public PointerField As IntPtr ' Violation - serializable class with IntPtr field
End Class

' Violation: Serializable class with multiple IntPtr members
<Serializable>
Public Class SerializableClassWithMultipleIntPtrs
    Public Property Pointer1 As IntPtr ' Violation
    Public Property Pointer2 As IntPtr ' Violation
    Public PointerField1 As IntPtr ' Violation
    Public PointerField2 As IntPtr ' Violation
End Class

' Violation: Serializable structure with IntPtr property
<Serializable>
Public Structure SerializableStructureWithIntPtrProperty
    Public Property PointerProperty As IntPtr ' Violation - serializable structure with IntPtr property
End Structure

' Violation: Serializable structure with IntPtr field
<Serializable>
Public Structure SerializableStructureWithIntPtrField
    Public PointerField As IntPtr ' Violation - serializable structure with IntPtr field
End Structure

' Violation: Friend serializable class with IntPtr
<Serializable>
Friend Class FriendSerializableClassWithIntPtr
    Public Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
End Class

' Violation: Serializable class with IntPtr and other properties
<Serializable>
Public Class SerializableClassWithMixedMembers
    Public Property Name As String
    Public Property Age As Integer
    Public Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
    Public Property Description As String
End Class

' Violation: Serializable class with Protected IntPtr property
<Serializable>
Public Class SerializableClassWithProtectedIntPtr
    Protected Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
End Class

' Violation: Serializable class with Friend IntPtr property
<Serializable>
Public Class SerializableClassWithFriendIntPtr
    Friend Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
End Class

' Violation: Serializable class with Protected IntPtr field
<Serializable>
Public Class SerializableClassWithProtectedIntPtrField
    Protected PointerField As IntPtr ' Violation - serializable class with IntPtr field
End Class

' Violation: Serializable class with Friend IntPtr field
<Serializable>
Public Class SerializableClassWithFriendIntPtrField
    Friend PointerField As IntPtr ' Violation - serializable class with IntPtr field
End Class

' Non-violation: Serializable class without IntPtr (should not be detected)
<Serializable>
Public Class SafeSerializableClass
    Public Property Name As String
    Public Property Age As Integer
    Public Property Description As String
End Class

' Non-violation: Non-serializable class with IntPtr (should not be detected)
Public Class NonSerializableClassWithIntPtr
    Public Property PointerProperty As IntPtr ' No violation - not serializable
End Class

' Non-violation: Serializable class with private IntPtr (should not be detected by this pattern)
<Serializable>
Public Class SerializableClassWithPrivateIntPtr
    Private pointerField As IntPtr ' No violation - private member (pattern may not detect)
End Class

' Violation: Serializable class with IntPtr in inheritance scenario
<Serializable>
Public Class BaseSerializableClassWithIntPtr
    Public Property BasePointer As IntPtr ' Violation - serializable class with IntPtr property
End Class

Public Class DerivedClass
    Inherits BaseSerializableClassWithIntPtr
    Public Property DerivedProperty As String
End Class

' Violation: Serializable class with IntPtr in nested scenario
<Serializable>
Public Class OuterSerializableClassWithIntPtr
    Public Property OuterPointer As IntPtr ' Violation - serializable class with IntPtr property
    
    <Serializable>
    Public Class NestedSerializableClass
        Public Property NestedPointer As IntPtr ' Violation - serializable class with IntPtr property
    End Class
End Class

' Violation: Serializable class with IntPtr property with custom getter/setter
<Serializable>
Public Class SerializableClassWithCustomIntPtrProperty
    Private _pointer As IntPtr
    
    Public Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
        Get
            Return _pointer
        End Get
        Set(value As IntPtr)
            _pointer = value
        End Set
    End Property
End Class

' Violation: Serializable class with readonly IntPtr field
<Serializable>
Public Class SerializableClassWithReadOnlyIntPtr
    Public ReadOnly PointerField As IntPtr ' Violation - serializable class with IntPtr field
    
    Public Sub New(pointer As IntPtr)
        PointerField = pointer
    End Sub
End Class

' Violation: Serializable class with shared IntPtr field
<Serializable>
Public Class SerializableClassWithSharedIntPtr
    Public Shared PointerField As IntPtr ' Violation - serializable class with IntPtr field
End Class

' Violation: Serializable class with IntPtr array property
<Serializable>
Public Class SerializableClassWithIntPtrArray
    Public Property Pointers As IntPtr() ' Violation - serializable class with IntPtr property
End Class

' Violation: Serializable class with IntPtr list property
<Serializable>
Public Class SerializableClassWithIntPtrList
    Public Property Pointers As List(Of IntPtr) ' Violation - serializable class with IntPtr property
End Class

' Non-violation: Comments and strings mentioning IntPtr
Public Class CommentsAndStrings
    Public Sub TestMethod()
        ' This is about serializable classes with IntPtr properties
        Dim message As String = "Serializable classes should not have IntPtr fields"
        Console.WriteLine("IntPtr fields in serializable types can cause security issues")
    End Sub
End Class

' Violation: Serializable class with IntPtr in conditional compilation
<Serializable>
Public Class SerializableClassWithConditionalIntPtr
#If DEBUG Then
    Public Property DebugPointer As IntPtr ' Violation
#End If
#If RELEASE Then
    Public Property ReleasePointer As IntPtr ' Violation
#End If
End Class

' Violation: Serializable class with IntPtr using different syntax
<Serializable>
Public Class SerializableClassWithDifferentIntPtrSyntax
    Public Property Pointer1 As System.IntPtr ' Violation - fully qualified IntPtr
    Public Pointer2 As System.IntPtr ' Violation - fully qualified IntPtr field
End Class

' Violation: Serializable class with IntPtr in generic context
<Serializable>
Public Class SerializableGenericClassWithIntPtr(Of T)
    Public Property GenericProperty As T
    Public Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
End Class

' Violation: Serializable class implementing interface with IntPtr
<Serializable>
Public Class SerializableClassImplementingInterface
    Implements IDisposable
    
    Public Property PointerProperty As IntPtr ' Violation - serializable class with IntPtr property
    
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Disposal logic
    End Sub
End Class

' Violation: Serializable partial class with IntPtr
<Serializable>
Partial Public Class SerializablePartialClassWithIntPtr
    Public Property Pointer1 As IntPtr ' Violation - serializable class with IntPtr property
End Class

Partial Public Class SerializablePartialClassWithIntPtr
    Public Property Pointer2 As IntPtr ' Violation - serializable class with IntPtr property
End Class

' Violation: Serializable class with IntPtr in different access level combinations
<Serializable>
Public Class SerializableClassWithVariousAccessLevels
    Public Property PublicPointer As IntPtr ' Violation
    Protected Property ProtectedPointer As IntPtr ' Violation
    Friend Property FriendPointer As IntPtr ' Violation
    
    Public PublicPointerField As IntPtr ' Violation
    Protected ProtectedPointerField As IntPtr ' Violation
    Friend FriendPointerField As IntPtr ' Violation
End Class

' Violation: Serializable class with IntPtr using Dim keyword
<Serializable>
Public Class SerializableClassWithDimIntPtr
    Public Property PointerProperty As IntPtr ' Violation
    Dim PointerField As IntPtr ' Violation - using Dim keyword
End Class
