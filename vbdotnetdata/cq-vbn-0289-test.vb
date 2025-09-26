' VB.NET test file for cq-vbn-0289: Opt in to preview features
' This rule detects usage of preview features without proper opt-in

Imports System
Imports System.Diagnostics.CodeAnalysis

' BAD: Using preview features without RequiresPreviewFeatures attribute
Public Class BadPreviewFeatureUsage
    ' Violation: Using preview API without attribute
    Public Sub TestPreviewApiUsage()
        ' Using a hypothetical preview feature
        Dim result = PreviewFeatureClass.PreviewMethod()
        Console.WriteLine(result)
    End Sub
    
    ' Violation: Another preview feature usage
    Public Function UsePreviewFeature() As String
        Return PreviewUtility.GetPreviewData()
    End Function
End Class

' BAD: Class using preview features without attribute
Public Class BadPreviewClass
    ' Violation: Preview feature in class without attribute
    Public Sub UsePreviewApi()
        PreviewFeatureClass.DoPreviewWork()
    End Sub
End Class

' GOOD: Proper opt-in to preview features
<RequiresPreviewFeatures>
Public Class GoodPreviewFeatureUsage
    ' Good: Using preview API with proper attribute
    Public Sub TestPreviewApiUsage()
        Dim result = PreviewFeatureClass.PreviewMethod()
        Console.WriteLine(result)
    End Sub
    
    ' Good: Another preview feature usage with attribute
    Public Function UsePreviewFeature() As String
        Return PreviewUtility.GetPreviewData()
    End Function
End Class

' Mock preview feature classes for testing
Public Class PreviewFeatureClass
    Public Shared Function PreviewMethod() As String
        Return "Preview result"
    End Function
    
    Public Shared Sub DoPreviewWork()
        ' Preview work
    End Sub
End Class

Public Class PreviewUtility
    Public Shared Function GetPreviewData() As String
        Return "Preview data"
    End Function
End Class
