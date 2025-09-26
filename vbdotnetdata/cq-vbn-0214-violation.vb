' Test file for cq-vbn-0214: Do not call dangerous methods in deserialization
' This rule detects dangerous method calls in deserialization callbacks

Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization
Imports Microsoft.Win32

<Serializable>
Public Class DangerousDeserializationViolations
    Implements IDeserializationCallback
    
    ' Violation: Process.Start in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedMethod(context As StreamingContext)
        Process.Start("notepad.exe") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: File.Delete in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithFileDelete(context As StreamingContext)
        File.Delete("C:\temp\file.txt") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Directory.Delete in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithDirectoryDelete(context As StreamingContext)
        Directory.Delete("C:\temp\folder", True) ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Registry.SetValue in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithRegistrySetValue(context As StreamingContext)
        Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Test", "TestKey", "TestValue") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Assembly.Load in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithAssemblyLoad(context As StreamingContext)
        Assembly.Load("System.Data") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Process.Start in OnDeserializing
    <OnDeserializing>
    Private Sub OnDeserializingMethod(context As StreamingContext)
        Process.Start("cmd.exe", "/c dir") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: File.Delete in OnDeserializing
    <OnDeserializing>
    Private Sub OnDeserializingWithFileDelete(context As StreamingContext)
        File.Delete("C:\important\file.txt") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Directory.Delete in OnDeserializing
    <OnDeserializing>
    Private Sub OnDeserializingWithDirectoryDelete(context As StreamingContext)
        Directory.Delete("C:\important\folder") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Registry.SetValue in OnDeserializing
    <OnDeserializing>
    Private Sub OnDeserializingWithRegistrySetValue(context As StreamingContext)
        Registry.SetValue("HKEY_CURRENT_USER\Software\Test", "MaliciousKey", "MaliciousValue") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Assembly.Load in OnDeserializing
    <OnDeserializing>
    Private Sub OnDeserializingWithAssemblyLoad(context As StreamingContext)
        Assembly.Load("Malicious.dll") ' Violation - dangerous method in deserialization
    End Sub
    
    ' Violation: Multiple dangerous methods in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithMultipleDangerousMethods(context As StreamingContext)
        Process.Start("malware.exe") ' Violation
        File.Delete("C:\system32\important.dll") ' Violation
        Directory.Delete("C:\Windows\System32") ' Violation
    End Sub
    
    ' Violation: Dangerous methods in conditional within OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithConditional(context As StreamingContext)
        If Environment.OSVersion.Platform = PlatformID.Win32NT Then
            Process.Start("windows_malware.exe") ' Violation
        Else
            Process.Start("linux_malware") ' Violation
        End If
    End Sub
    
    ' Violation: Dangerous methods in loop within OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithLoop(context As StreamingContext)
        For i As Integer = 0 To 10
            File.Delete($"C:\temp\file{i}.txt") ' Violation
        Next
    End Sub
    
    ' Violation: Dangerous methods in Try-Catch within OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithTryCatch(context As StreamingContext)
        Try
            Assembly.Load("dangerous.dll") ' Violation
        Catch ex As Exception
            Process.Start("fallback_malware.exe") ' Violation
        End Try
    End Sub
    
    ' Violation: Dangerous methods with variables in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithVariables(context As StreamingContext)
        Dim processName As String = "malicious.exe"
        Dim fileName As String = "C:\victim\file.txt"
        
        Process.Start(processName) ' Violation
        File.Delete(fileName) ' Violation
    End Sub
    
    ' Violation: Dangerous methods in Select Case within OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithSelectCase(context As StreamingContext)
        Dim action As Integer = 1
        Select Case action
            Case 1
                Process.Start("action1.exe") ' Violation
            Case 2
                File.Delete("action2.txt") ' Violation
            Case 3
                Directory.Delete("action3_folder") ' Violation
        End Select
    End Sub
    
    ' Violation: Dangerous methods in While loop within OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithWhileLoop(context As StreamingContext)
        Dim counter As Integer = 0
        While counter < 5
            Process.Start($"process{counter}.exe") ' Violation
            counter += 1
        End While
    End Sub
    
    ' Non-violation: Safe operations in OnDeserialized (should not be detected)
    <OnDeserialized>
    Private Sub OnDeserializedSafeOperations(context As StreamingContext)
        Console.WriteLine("Deserialization completed") ' No violation - safe operation
        Dim data As String = "Safe data"
        Me.SafeProperty = data
    End Sub
    
    ' Non-violation: Safe operations in OnDeserializing (should not be detected)
    <OnDeserializing>
    Private Sub OnDeserializingSafeOperations(context As StreamingContext)
        Console.WriteLine("Deserialization starting") ' No violation - safe operation
        Me.InitializeData()
    End Sub
    
    ' Non-violation: Comments and strings mentioning dangerous methods
    Public Sub CommentsAndStrings()
        ' This is about Process.Start and File.Delete in deserialization
        Dim message As String = "Do not call Process.Start in OnDeserialized"
        Console.WriteLine("Dangerous methods like Assembly.Load should not be called during deserialization")
    End Sub
    
    ' Non-violation: Dangerous methods outside deserialization callbacks (should not be detected)
    Public Sub RegularMethodWithDangerousOperations()
        Process.Start("notepad.exe") ' No violation - not in deserialization callback
        File.Delete("temp.txt") ' No violation - not in deserialization callback
    End Sub
    
    ' Violation: IDeserializationCallback implementation with dangerous methods
    Public Sub OnDeserialization(sender As Object) Implements IDeserializationCallback.OnDeserialization
        Process.Start("callback_malware.exe") ' Violation - dangerous method in deserialization callback
        Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Malware", "Installed", "True") ' Violation
    End Sub
    
    Private Property SafeProperty As String
    
    Private Sub InitializeData()
        ' Safe initialization code
    End Sub
    
    ' Violation: Dangerous methods with method chaining in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithMethodChaining(context As StreamingContext)
        Dim processInfo = New ProcessStartInfo("malware.exe").
                         With {.Arguments = "/silent", .CreateNoWindow = True}
        Process.Start(processInfo) ' Violation
    End Sub
    
    ' Violation: Dangerous methods with static calls in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithStaticCalls(context As StreamingContext)
        System.Diagnostics.Process.Start("static_malware.exe") ' Violation
        System.IO.File.Delete("static_file.txt") ' Violation
    End Sub
    
    ' Violation: Dangerous methods with different overloads in OnDeserialized
    <OnDeserialized>
    Private Sub OnDeserializedWithOverloads(context As StreamingContext)
        Process.Start("malware.exe", "arguments") ' Violation
        Assembly.LoadFrom("C:\malicious.dll") ' Violation - different overload
        Assembly.LoadFile("C:\evil.dll") ' Violation - different overload
    End Sub

End Class
