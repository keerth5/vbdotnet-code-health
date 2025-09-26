' Test file for cq-vbn-0159: Dispose objects before losing scope
' Because an exceptional event might occur that will prevent the finalizer of an object from running, the object should be explicitly disposed before all references to it are out of scope

Imports System
Imports System.IO
Imports System.Data.SqlClient

Public Class DisposableObjectTests
    
    ' Violation: FileStream not properly disposed
    Public Sub ReadFileWithoutDispose()
        ' Violation: FileStream created but not disposed
        Dim stream As New FileStream("test.txt", FileMode.Open)
        
        Dim buffer(1024) As Byte
        stream.Read(buffer, 0, buffer.Length)
        
        ' Stream goes out of scope without explicit disposal
    End Sub
    
    ' Violation: StreamReader not disposed
    Public Sub ReadTextWithoutDispose()
        ' Violation: StreamReader created but not disposed
        Dim reader As New StreamReader("data.txt")
        
        Dim content As String = reader.ReadToEnd()
        Console.WriteLine(content)
        
        ' Reader goes out of scope without disposal
    End Sub
    
    ' Violation: Multiple disposable objects not disposed
    Public Sub ProcessDataWithoutDispose()
        ' Violation: FileStream not disposed
        Dim fileStream As New FileStream("input.dat", FileMode.Open)
        
        ' Violation: BinaryReader not disposed
        Dim binaryReader As New BinaryReader(fileStream)
        
        Dim data As Integer = binaryReader.ReadInt32()
        Console.WriteLine($"Data: {data}")
        
        ' Both objects go out of scope without disposal
    End Sub
    
    ' Violation: SqlConnection not disposed
    Public Sub DatabaseOperationWithoutDispose()
        ' Violation: SqlConnection created but not disposed
        Dim connection As New SqlConnection("Server=localhost;Database=TestDB;")
        
        connection.Open()
        
        ' Violation: SqlCommand not disposed
        Dim command As New SqlCommand("SELECT * FROM Users", connection)
        
        ' Violation: SqlDataReader not disposed
        Dim reader As SqlDataReader = command.ExecuteReader()
        
        While reader.Read()
            Console.WriteLine(reader("Name").ToString())
        End While
        
        ' All objects go out of scope without disposal
    End Sub
    
    ' Violation: MemoryStream not disposed
    Public Sub WriteToMemoryWithoutDispose()
        ' Violation: MemoryStream created but not disposed
        Dim memoryStream As New MemoryStream()
        
        ' Violation: BinaryWriter not disposed
        Dim writer As New BinaryWriter(memoryStream)
        
        writer.Write("Hello World")
        writer.Write(42)
        
        Dim data() As Byte = memoryStream.ToArray()
        Console.WriteLine($"Data length: {data.Length}")
        
        ' Objects go out of scope without disposal
    End Sub
    
    ' Violation: TextWriter not disposed
    Public Sub WriteTextWithoutDispose()
        ' Violation: StreamWriter created but not disposed
        Dim writer As New StreamWriter("output.txt")
        
        writer.WriteLine("First line")
        writer.WriteLine("Second line")
        writer.Flush()
        
        ' Writer goes out of scope without disposal
    End Sub
    
    ' Good practice: Using Using statement (should not be detected)
    Public Sub ReadFileWithUsing()
        ' Good: Using statement ensures disposal
        Using stream As New FileStream("test.txt", FileMode.Open)
            Dim buffer(1024) As Byte
            stream.Read(buffer, 0, buffer.Length)
        End Using ' Automatic disposal
    End Sub
    
    ' Good: Manual disposal
    Public Sub ReadFileWithManualDispose()
        Dim stream As FileStream = Nothing
        
        Try
            stream = New FileStream("test.txt", FileMode.Open)
            Dim buffer(1024) As Byte
            stream.Read(buffer, 0, buffer.Length)
            
        Finally
            ' Good: Manual disposal in Finally block
            If stream IsNot Nothing Then
                stream.Dispose()
            End If
        End Try
    End Sub
    
    ' Violation: Connection in loop without disposal
    Public Sub ProcessMultipleFilesWithoutDispose()
        Dim fileNames() As String = {"file1.txt", "file2.txt", "file3.txt"}
        
        For Each fileName In fileNames
            ' Violation: FileStream created in loop without disposal
            Dim stream As New FileStream(fileName, FileMode.Open)
            
            Dim buffer(512) As Byte
            stream.Read(buffer, 0, buffer.Length)
            
            Console.WriteLine($"Processed {fileName}")
            
            ' Stream goes out of scope without disposal (memory leak in loop)
        Next
    End Sub
    
    ' Violation: Nested disposable objects
    Public Sub NestedDisposableObjects()
        ' Violation: Outer FileStream not disposed
        Dim outerStream As New FileStream("outer.dat", FileMode.Create)
        
        ' Violation: Inner BinaryWriter not disposed
        Dim writer As New BinaryWriter(outerStream)
        
        writer.Write("Nested data")
        
        ' Both objects go out of scope without disposal
    End Sub
    
    ' Violation: Disposable object in exception handling
    Public Sub ProcessWithExceptionHandling()
        Try
            ' Violation: FileStream created but not disposed if exception occurs
            Dim stream As New FileStream("risky.txt", FileMode.Open)
            
            ' Some risky operation that might throw
            Dim data(1000000) As Byte
            stream.Read(data, 0, data.Length)
            
            ' If exception occurs here, stream won't be disposed
            Throw New InvalidOperationException("Something went wrong")
            
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
            ' Stream not disposed in catch block
        End Try
    End Sub
    
End Class

' Additional test cases
Public Module DisposableUtilities
    
    ' Violation: Utility method with undisposed objects
    Public Sub ProcessFileData(fileName As String)
        ' Violation: FileStream not disposed in utility method
        Dim stream As New FileStream(fileName, FileMode.Open)
        
        ' Violation: StreamReader not disposed
        Dim reader As New StreamReader(stream)
        
        Dim line As String
        While (InlineAssignHelper(line, reader.ReadLine())) IsNot Nothing
            Console.WriteLine(line)
        End While
        
        ' Objects go out of scope without disposal
    End Sub
    
    ' Helper function for inline assignment
    Private Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
    
    ' Violation: Database utility without disposal
    Public Sub ExecuteQuery(connectionString As String, query As String)
        ' Violation: SqlConnection not disposed
        Dim connection As New SqlConnection(connectionString)
        
        connection.Open()
        
        ' Violation: SqlCommand not disposed
        Dim command As New SqlCommand(query, connection)
        
        Dim result As Object = command.ExecuteScalar()
        Console.WriteLine($"Result: {result}")
        
        ' Objects go out of scope without disposal
    End Sub
    
    ' Violation: Complex disposable object creation
    Public Sub CreateComplexStream()
        ' Violation: Multiple nested disposable objects
        Dim fileStream As New FileStream("complex.dat", FileMode.Create)
        Dim bufferedStream As New BufferedStream(fileStream)
        Dim binaryWriter As New BinaryWriter(bufferedStream)
        
        binaryWriter.Write("Complex data structure")
        binaryWriter.Write(DateTime.Now.Ticks)
        
        ' All objects go out of scope without disposal
    End Sub
    
End Module

' Test with custom disposable class
Public Class CustomDisposableResource
    Implements IDisposable
    
    Private disposed As Boolean = False
    
    Public Sub DoWork()
        If disposed Then
            Throw New ObjectDisposedException("CustomDisposableResource")
        End If
        
        Console.WriteLine("Doing work...")
    End Sub
    
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposed Then
            If disposing Then
                ' Dispose managed resources
            End If
            disposed = True
        End If
    End Sub
    
End Class

Public Class CustomDisposableTests
    
    ' Violation: Custom disposable object not disposed
    Public Sub UseCustomResourceWithoutDispose()
        ' Violation: CustomDisposableResource not disposed
        Dim resource As New CustomDisposableResource()
        
        resource.DoWork()
        
        ' Resource goes out of scope without disposal
    End Sub
    
    ' Good: Custom disposable with Using
    Public Sub UseCustomResourceWithUsing()
        ' Good: Using statement ensures disposal
        Using resource As New CustomDisposableResource()
            resource.DoWork()
        End Using
    End Sub
    
End Class
