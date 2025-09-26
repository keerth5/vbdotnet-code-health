' Test file for cq-vbn-0060: Validate platform compatibility
' Rule should detect platform-specific APIs that may not work on all platforms

Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports Microsoft.Win32

Public Class PlatformSpecificExamples
    
    Public Sub UseWindowsForms()
        
        ' Violation 1: Windows Forms usage (Windows-specific)
        Dim form As New Form()
        form.Text = "Windows Form"
        
        ' Violation 2: MessageBox from Windows Forms
        MessageBox.Show("Hello World", "Information")
        
        ' Violation 3: Button control
        Dim button As New Button()
        button.Text = "Click Me"
        
        ' Violation 4: TextBox control
        Dim textBox As New TextBox()
        textBox.Text = "Enter text here"
        
    End Sub
    
    Public Sub UseSystemDrawing()
        
        ' Violation 5: System.Drawing usage (Windows-specific)
        Dim bitmap As New Bitmap(100, 100)
        
        ' Violation 6: Graphics object
        Dim graphics As Graphics = Graphics.FromImage(bitmap)
        
        ' Violation 7: Color usage
        Dim color As Color = Color.Red
        
        ' Violation 8: Font usage
        Dim font As New Font("Arial", 12)
        
    End Sub
    
    Public Sub UseRegistryAccess()
        
        ' Violation 9: Registry access (Windows-specific)
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE")
        
        ' Violation 10: Registry key manipulation
        Dim value = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft", "Version", Nothing)
        
        ' Violation 11: Registry CurrentUser access
        Dim userKey = Registry.CurrentUser.CreateSubKey("MyApplication")
        
    End Sub
    
    Public Sub ProcessWindowsSpecificOperations()
        
        ' Violation 12: Windows Forms Application class
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        
        ' Violation 13: Windows Forms Timer
        Dim timer As New Timer()
        timer.Interval = 1000
        
    End Sub
    
    ' This should NOT be detected - cross-platform .NET APIs
    Public Sub UseCrossPlatformApis()
        
        ' These are cross-platform and should not be detected
        Console.WriteLine("Cross-platform console output")
        
        Dim dateTime As DateTime = DateTime.Now
        Dim text As String = "Cross-platform string"
        
        Dim list As New List(Of String)()
        list.Add("Cross-platform collection")
        
        ' File I/O is cross-platform
        System.IO.File.WriteAllText("test.txt", "Cross-platform file I/O")
        
    End Sub
    
End Class

Public Class MorePlatformSpecificExamples
    
    Public Sub UseAdvancedWindowsForms()
        
        ' Violation 14: DataGridView (Windows Forms)
        Dim dataGrid As New DataGridView()
        dataGrid.DataSource = Nothing
        
        ' Violation 15: TreeView control
        Dim treeView As New TreeView()
        treeView.Nodes.Add("Root Node")
        
        ' Violation 16: MenuStrip control
        Dim menuStrip As New MenuStrip()
        
    End Sub
    
    Public Sub UseDrawingOperations()
        
        ' Violation 17: Pen for drawing
        Dim pen As New Pen(Color.Black, 2)
        
        ' Violation 18: Brush for filling
        Dim brush As New SolidBrush(Color.Blue)
        
        ' Violation 19: Rectangle structure
        Dim rect As New Rectangle(10, 10, 100, 50)
        
    End Sub
    
    Public Sub UseRegistryOperations()
        
        ' Violation 20: Registry ClassesRoot access
        Dim classesRoot = Registry.ClassesRoot.OpenSubKey(".txt")
        
        ' Violation 21: Registry Users access
        Dim users = Registry.Users.GetSubKeyNames()
        
    End Sub
    
    ' This should NOT be detected - System namespace usage (cross-platform)
    Public Sub UseSystemNamespace()
        
        ' These System namespace usages are cross-platform
        Dim guid As Guid = Guid.NewGuid()
        Dim uri As New Uri("https://example.com")
        Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
        
        ' Threading is cross-platform
        Dim thread As New System.Threading.Thread(Sub() Console.WriteLine("Thread"))
        
    End Sub
    
End Class

' WinRT usage examples (Windows 10+ specific)
Public Class WinRTExamples
    
    ' Note: These would require proper WinRT references in a real project
    ' For testing purposes, we'll simulate the usage patterns
    
    Public Sub UseWinRTApis()
        
        ' Violation 22: WinRT namespace usage (simulated)
        ' In real code: Dim picker As New Windows.Storage.Pickers.FileOpenPicker()
        Console.WriteLine("WinRT.FileOpenPicker usage")
        
        ' Violation 23: Another WinRT usage (simulated)
        ' In real code: Dim toast As New Windows.UI.Notifications.ToastNotification()
        Console.WriteLine("WinRT.ToastNotification usage")
        
    End Sub
    
End Class
