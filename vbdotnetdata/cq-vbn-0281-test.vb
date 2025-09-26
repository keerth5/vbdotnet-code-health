' VB.NET test file for cq-vbn-0281: Do not duplicate indexed element initializations
' This rule detects object initializers with duplicate indexed element initializers

Imports System

Public Class BadIndexedInitialization
    ' BAD: Duplicate indexed element initializations
    Public Sub TestDuplicateIndexedElements()
        ' Violation: Same index {0} used multiple times
        Dim dict1 = New Dictionary(Of Integer, String) From {{0, "first"}, {1, "second"}, {0, "duplicate"}}
        
        ' Violation: Same index {2} used multiple times  
        Dim dict2 = New Dictionary(Of Integer, String) From {{2, "value1"}, {3, "value2"}, {2, "duplicate"}}
        
        ' Violation: Multiple duplicates
        Dim dict3 = New Dictionary(Of Integer, String) From {{1, "a"}, {2, "b"}, {1, "c"}, {3, "d"}, {2, "e"}}
    End Sub

    ' GOOD: No duplicate indexed elements
    Public Sub TestValidIndexedElements()
        ' Good: All unique indices
        Dim dict1 = New Dictionary(Of Integer, String) From {{0, "first"}, {1, "second"}, {2, "third"}}
        
        ' Good: Sequential indices
        Dim dict2 = New Dictionary(Of Integer, String) From {{1, "one"}, {2, "two"}, {3, "three"}}
        
        ' Good: Non-sequential but unique indices
        Dim dict3 = New Dictionary(Of Integer, String) From {{10, "ten"}, {20, "twenty"}, {30, "thirty"}}
    End Sub
End Class
