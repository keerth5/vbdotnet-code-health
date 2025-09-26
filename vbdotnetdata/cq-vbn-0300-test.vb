' VB.NET test file for cq-vbn-0300: Prefer generic overload when type is known
' This rule detects usage of GetType() when generic overloads are available

Imports System
Imports System.Reflection

Public Class BadGenericUsage
    ' BAD: Using GetType() when generic overload is available
    Public Sub TestBadGenericUsage()
        ' Violation: Using GetType(String) instead of generic overload
        Dim method1 = GetMethod(GetType(String))
        
        ' Violation: Using instance.GetType() instead of generic
        Dim obj As New TestClass()
        Dim method2 = GetMethod(obj.GetType())
        
        ' Violation: Another GetType usage
        Dim type1 = ProcessType(GetType(Integer))
        
        ' Violation: GetType with complex type
        Dim type2 = ProcessType(GetType(List(Of String)))
    End Sub
    
    Public Sub TestMoreBadUsage()
        Dim instance As New TestClass()
        
        ' Violation: GetType on instance
        Dim result1 = AnalyzeType(instance.GetType())
        
        ' Violation: GetType with generic type
        Dim result2 = AnalyzeType(GetType(Dictionary(Of String, Integer)))
    End Sub
    
    Private Function GetMethod(type As Type) As MethodInfo
        Return type.GetMethod("ToString")
    End Function
    
    Private Function ProcessType(type As Type) As String
        Return type.Name
    End Function
    
    Private Function AnalyzeType(type As Type) As Boolean
        Return type.IsClass
    End Function
    
    ' GOOD: Using generic overloads when available
    Public Sub TestGoodGenericUsage()
        ' Good: Using generic overload instead of GetType
        Dim method1 = GetMethod(Of String)()
        
        ' Good: Generic method call
        Dim type1 = ProcessType(Of Integer)()
        
        ' Good: Generic analysis
        Dim result1 = AnalyzeType(Of List(Of String))()
        
        ' Good: Direct type usage without GetType
        Dim typeName = GetType(String).Name
    End Sub
    
    Private Function GetMethod(Of T)() As MethodInfo
        Return GetType(T).GetMethod("ToString")
    End Function
    
    Private Function ProcessType(Of T)() As String
        Return GetType(T).Name
    End Function
    
    Private Function AnalyzeType(Of T)() As Boolean
        Return GetType(T).IsClass
    End Function
End Class

Public Class TestClass
    Public Property Name As String
End Class
