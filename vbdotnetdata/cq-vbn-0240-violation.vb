' VB.NET test file for cq-vbn-0240: Do not add archive item's path to the target file system path
' This rule detects unsafe path operations that can lead to directory traversal attacks

Imports System
Imports System.IO
Imports System.IO.Compression

' Violation: Using Path.Combine with archive entry FullName
Public Class UnsafePathOperations1
    Public Sub ExtractArchiveUnsafely()
        Dim zipPath As String = "archive.zip"
        Dim extractPath As String = "C:\temp\extract"
        
        Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
            For Each entry As ZipArchiveEntry In archive.Entries
                ' Violation: Using entry.FullName directly in Path.Combine
                Dim destinationPath As String = Path.Combine(extractPath, entry.FullName)
                entry.ExtractToFile(destinationPath)
            Next
        End Using
    End Sub
End Class

' Violation: Using Path.Combine with Name property
Public Class UnsafePathOperations2
    Public Sub ExtractWithEntryName()
        Dim zipFile As String = "data.zip"
        Dim targetDir As String = "C:\extracted"
        
        Using zip As ZipArchive = ZipFile.OpenRead(zipFile)
            For Each item As ZipArchiveEntry In zip.Entries
                ' Violation: Using item.Name in Path.Combine
                Dim outputPath As String = Path.Combine(targetDir, item.Name)
                
                Using stream As Stream = item.Open()
                    Using fileStream As New FileStream(outputPath, FileMode.Create)
                        stream.CopyTo(fileStream)
                    End Using
                End Using
            Next
        End Using
    End Sub
End Class

' Violation: ExtractToDirectory without path validation
Public Class UnsafePathOperations3
    Public Sub ExtractArchiveToDirectory()
        Dim archivePath As String = "malicious.zip"
        Dim extractionPath As String = "C:\program files\app"
        
        ' Violation: ExtractToDirectory without path validation
        ZipFile.ExtractToDirectory(archivePath, extractionPath)
        
        ' Additional processing that might use Path.GetFullPath
        Dim fullPath As String = Path.GetFullPath(extractionPath)
        Console.WriteLine($"Extracted to: {fullPath}")
    End Sub
End Class

' Violation: Using entry path in different Path operations
Public Class UnsafePathOperations4
    Public Sub ProcessArchiveEntries()
        Dim zipPath As String = "archive.zip"
        Dim baseDir As String = "C:\temp"
        
        Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
            For Each entry As ZipArchiveEntry In archive.Entries
                ' Violation: Using entry.FullName in Path.Combine
                Dim targetPath As String = Path.Combine(baseDir, entry.FullName)
                
                ' Create directory if needed
                Dim dirPath As String = Path.GetDirectoryName(targetPath)
                If Not Directory.Exists(dirPath) Then
                    Directory.CreateDirectory(dirPath)
                End If
                
                entry.ExtractToFile(targetPath, True)
            Next
        End Using
    End Sub
End Class

' Violation: Using archive entry path with Path.Join
Public Class UnsafePathOperations5
    Public Sub ExtractWithPathJoin()
        Dim archiveFile As String = "data.zip"
        Dim outputFolder As String = "output"
        
        Using zip As ZipArchive = ZipFile.OpenRead(archiveFile)
            For Each entry As ZipArchiveEntry In zip.Entries
                ' Violation: Using entry.FullName with path operations
                Dim destination As String = Path.Combine(outputFolder, entry.FullName)
                
                If Not entry.FullName.EndsWith("/") Then
                    entry.ExtractToFile(destination, True)
                End If
            Next
        End Using
    End Sub
End Class

' Violation: Complex path manipulation with archive entries
Public Class UnsafePathOperations6
    Public Sub ComplexPathManipulation()
        Dim zipPath As String = "complex.zip"
        Dim extractDir As String = "extracted"
        
        Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
            For Each entry As ZipArchiveEntry In archive.Entries
                ' Violation: Multiple path operations with entry.FullName
                Dim relativePath As String = entry.FullName.Replace("/", "\")
                Dim fullPath As String = Path.Combine(extractDir, relativePath)
                Dim normalizedPath As String = Path.GetFullPath(fullPath)
                
                entry.ExtractToFile(normalizedPath, True)
            Next
        End Using
    End Sub
End Class

' Violation: Using entry path in conditional logic
Public Class UnsafePathOperations7
    Public Sub ConditionalExtraction()
        Dim archivePath As String = "conditional.zip"
        Dim targetPath As String = "target"
        
        Using zip As ZipArchive = ZipFile.OpenRead(archivePath)
            For Each entry As ZipArchiveEntry In zip.Entries
                If Not String.IsNullOrEmpty(entry.Name) Then
                    ' Violation: Using entry.FullName in Path.Combine conditionally
                    Dim extractPath As String = Path.Combine(targetPath, entry.FullName)
                    entry.ExtractToFile(extractPath)
                End If
            Next
        End Using
    End Sub
End Class

' Violation: Using archive entry in method parameter
Public Class UnsafePathOperations8
    Public Sub ExtractSpecificEntry(entry As ZipArchiveEntry, baseDir As String)
        ' Violation: Using entry.FullName directly
        Dim outputPath As String = Path.Combine(baseDir, entry.FullName)
        entry.ExtractToFile(outputPath, True)
    End Sub
    
    Public Sub ProcessArchive()
        Using zip As ZipArchive = ZipFile.OpenRead("test.zip")
            For Each entry As ZipArchiveEntry In zip.Entries
                ExtractSpecificEntry(entry, "C:\output")
            Next
        End Using
    End Sub
End Class

' Violation: ExtractToDirectory with Path.GetFullPath usage
Public Class UnsafePathOperations9
    Public Sub ExtractAndGetFullPath()
        Dim zipFile As String = "archive.zip"
        Dim extractTo As String = "extracted"
        
        ' Violation: ExtractToDirectory followed by Path.GetFullPath
        ZipFile.ExtractToDirectory(zipFile, extractTo)
        Dim fullPath As String = Path.GetFullPath(extractTo)
        
        Console.WriteLine($"Extraction completed to: {fullPath}")
    End Sub
End Class

' Good examples (should not be detected as violations)
Public Class SecurePathOperations
    Public Sub SecureExtraction()
        Dim zipPath As String = "secure.zip"
        Dim extractDir As String = "secure_extract"
        
        Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
            For Each entry As ZipArchiveEntry In archive.Entries
                ' Good: Validate and sanitize the path
                Dim entryPath As String = entry.FullName
                
                ' Remove dangerous path components
                entryPath = entryPath.Replace("..", "")
                entryPath = entryPath.TrimStart("/", "\")
                
                ' Use sanitized path
                Dim safePath As String = Path.Combine(extractDir, entryPath)
                
                ' Ensure the path is within the target directory
                Dim fullSafePath As String = Path.GetFullPath(safePath)
                Dim fullExtractDir As String = Path.GetFullPath(extractDir)
                
                If fullSafePath.StartsWith(fullExtractDir) Then
                    entry.ExtractToFile(safePath, True)
                End If
            Next
        End Using
    End Sub
    
    Public Sub SafeDirectoryExtraction()
        Dim zipFile As String = "safe.zip"
        Dim targetDir As String = "safe_target"
        
        ' Good: Using a secure extraction method
        ExtractToDirectorySafely(zipFile, targetDir)
    End Sub
    
    Private Sub ExtractToDirectorySafely(zipPath As String, extractPath As String)
        ' Implementation of secure extraction logic
        Console.WriteLine($"Safely extracting {zipPath} to {extractPath}")
    End Sub
End Class
