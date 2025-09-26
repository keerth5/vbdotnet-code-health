' Test file for cq-vbn-0086: Events should not have before or after prefix
' Rule should detect events with 'Before' or 'After' prefixes in their names

Imports System

Public Class EventExamples
    
    ' Violation 1: Event with Before prefix
    Public Event BeforeLoad As EventHandler
    
    ' Violation 2: Event with After prefix
    Public Event AfterSave As EventHandler
    
    ' Violation 3: Protected event with Before prefix
    Protected Event BeforeProcess As EventHandler(Of EventArgs)
    
    ' Violation 4: Friend event with After prefix
    Friend Event AfterUpdate As EventHandler(Of EventArgs)
    
    ' Violation 5: Event with Before prefix (different case)
    Public Event beforeDelete As EventHandler
    
    ' Violation 6: Event with After prefix (different case)
    Public Event afterCreate As EventHandler
    
    Public Sub TriggerEvents()
        RaiseEvent BeforeLoad(Me, EventArgs.Empty)
        RaiseEvent AfterSave(Me, EventArgs.Empty)
        RaiseEvent BeforeProcess(Me, EventArgs.Empty)
        RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        RaiseEvent beforeDelete(Me, EventArgs.Empty)
        RaiseEvent afterCreate(Me, EventArgs.Empty)
    End Sub
    
    ' This should NOT be detected - proper event naming
    Public Event DataLoaded As EventHandler
    Public Event FileSaved As EventHandler
    Public Event ProcessCompleted As EventHandler(Of EventArgs)
    Public Event UpdateFinished As EventHandler(Of EventArgs)
    
End Class

Public Class DocumentProcessor
    
    ' Violation 7: Event with Before prefix in different context
    Public Event BeforeValidation As EventHandler(Of EventArgs)
    
    ' Violation 8: Event with After prefix in different context
    Public Event AfterValidation As EventHandler(Of EventArgs)
    
    ' Violation 9: Protected event with Before prefix
    Protected Event BeforeTransform As EventHandler
    
    ' Violation 10: Friend event with After prefix
    Friend Event AfterTransform As EventHandler
    
    Public Sub ProcessDocument()
        RaiseEvent BeforeValidation(Me, EventArgs.Empty)
        ' Processing logic here
        RaiseEvent AfterValidation(Me, EventArgs.Empty)
        
        RaiseEvent BeforeTransform(Me, EventArgs.Empty)
        ' Transform logic here
        RaiseEvent AfterTransform(Me, EventArgs.Empty)
    End Sub
    
    ' This should NOT be detected - proper event naming
    Public Event ValidationCompleted As EventHandler(Of EventArgs)
    Public Event TransformationFinished As EventHandler
    Public Event DocumentReady As EventHandler
    
End Class

Friend Class DataManager
    
    ' Violation 11: Event with Before prefix
    Public Event BeforeDataLoad As EventHandler(Of EventArgs)
    
    ' Violation 12: Event with After prefix
    Public Event AfterDataSave As EventHandler(Of EventArgs)
    
    ' Violation 13: Event with Before prefix (compound name)
    Protected Event BeforeDataValidation As EventHandler
    
    ' Violation 14: Event with After prefix (compound name)
    Friend Event AfterDataProcessing As EventHandler
    
    Public Sub ManageData()
        RaiseEvent BeforeDataLoad(Me, EventArgs.Empty)
        ' Data loading logic
        
        RaiseEvent AfterDataSave(Me, EventArgs.Empty)
        ' Data saving logic
        
        RaiseEvent BeforeDataValidation(Me, EventArgs.Empty)
        ' Validation logic
        
        RaiseEvent AfterDataProcessing(Me, EventArgs.Empty)
        ' Processing completion
    End Sub
    
    ' This should NOT be detected - events without Before/After prefix
    Public Event DataChanged As EventHandler(Of EventArgs)
    Public Event ValidationFailed As EventHandler
    Public Event ProcessingError As EventHandler(Of EventArgs)
    
End Class

Public Class UserInterface
    
    ' Violation 15: Event with Before prefix
    Public Event BeforeRender As EventHandler
    
    ' Violation 16: Event with After prefix
    Public Event AfterRender As EventHandler
    
    ' This should NOT be detected - proper event naming
    Public Event ButtonClicked As EventHandler
    Public Event TextChanged As EventHandler(Of EventArgs)
    Public Event FormClosing As EventHandler
    Public Event RenderCompleted As EventHandler
    
    Public Sub RenderUI()
        RaiseEvent BeforeRender(Me, EventArgs.Empty)
        ' Rendering logic
        RaiseEvent AfterRender(Me, EventArgs.Empty)
    End Sub
    
End Class

' Custom event args for testing
Public Class CustomEventArgs
    Inherits EventArgs
    
    Public Property Message As String
    
    Public Sub New(message As String)
        Me.Message = message
    End Sub
End Class

Public Class ServiceManager
    
    ' Violation 17: Event with Before prefix using custom event args
    Public Event BeforeServiceStart As EventHandler(Of CustomEventArgs)
    
    ' Violation 18: Event with After prefix using custom event args
    Public Event AfterServiceStop As EventHandler(Of CustomEventArgs)
    
    Public Sub StartService()
        RaiseEvent BeforeServiceStart(Me, New CustomEventArgs("Starting service"))
        ' Service start logic
    End Sub
    
    Public Sub StopService()
        ' Service stop logic
        RaiseEvent AfterServiceStop(Me, New CustomEventArgs("Service stopped"))
    End Sub
    
    ' This should NOT be detected - proper event naming
    Public Event ServiceStarted As EventHandler(Of CustomEventArgs)
    Public Event ServiceStopped As EventHandler(Of CustomEventArgs)
    Public Event ServiceError As EventHandler(Of CustomEventArgs)
    
End Class
