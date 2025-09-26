' Test file for cq-vbn-0081: Identifiers should not contain underscores
' Rule should detect identifiers with underscores that violate .NET naming conventions

Imports System

' Violation 1: Class with underscore in name
Public Class My_Bad_Class
    
    ' Violation 2: Method with underscore in name
    Public Sub Process_Data()
        Console.WriteLine("Processing data")
    End Sub
    
    ' Violation 3: Function with underscore in name
    Public Function Get_User_Name() As String
        Return "user"
    End Function
    
    ' Violation 4: Property with underscore in name
    Public Property First_Name As String
    
    ' Violation 5: Private field with underscore (should be detected)
    Private _user_data As String
    
    ' Violation 6: Protected method with underscore
    Protected Sub Handle_Event()
        Console.WriteLine("Handling event")
    End Sub
    
    ' Violation 7: Friend property with underscore
    Friend Property Last_Name As String
    
    Public Sub TestMethod(ByVal param_with_underscore As String)
        ' Violation 8: Local variable with underscore
        Dim local_variable As Integer = 10
        
        ' Violation 9: Another local variable with underscore
        Dim temp_data As String = "test"
        
        Console.WriteLine(param_with_underscore)
        Console.WriteLine(local_variable)
        Console.WriteLine(temp_data)
    End Sub
    
    ' This should NOT be detected - proper naming
    Public Sub ProcessData()
        Console.WriteLine("Proper naming")
    End Sub
    
    ' This should NOT be detected - proper naming
    Public Property FirstName As String
    
    ' This should NOT be detected - proper private field naming
    Private _userData As String
    
End Class

' Violation 10: Another class with underscore
Public Class Data_Handler
    
    ' Violation 11: Method with multiple underscores
    Public Sub Save_User_Data_To_File()
        Console.WriteLine("Saving data")
    End Sub
    
End Class

' This should NOT be detected - proper class naming
Public Class DataHandler
    
    Public Sub SaveUserDataToFile()
        Console.WriteLine("Proper naming")
    End Sub
    
End Class

' Violation 12: Friend class with underscore
Friend Class Internal_Helper
    
    ' Violation 13: Function with underscore
    Public Function Calculate_Total() As Double
        Return 0.0
    End Function
    
End Class

' Violation 14: Protected method in inheritance scenario
Public Class BaseClass
    
    ' Violation 15: Protected method with underscore
    Protected Overridable Sub Initialize_Data()
        Console.WriteLine("Initializing")
    End Sub
    
End Class

Public Class DerivedClass
    Inherits BaseClass
    
    ' Violation 16: Override method with underscore
    Protected Overrides Sub Initialize_Data()
        Console.WriteLine("Derived initialization")
    End Sub
    
End Class

Public Module Utility_Module
    
    ' Violation 17: Module method with underscore
    Public Sub Log_Message(message As String)
        Console.WriteLine(message)
    End Sub
    
End Module
