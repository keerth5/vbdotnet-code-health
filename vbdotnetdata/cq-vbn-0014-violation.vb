' Test file for cq-vbn-0014: Use properties where appropriate
' Rule should detect getter/setter methods that should be properties instead

Imports System

Public Class Person
    
    Private _name As String
    Private _age As Integer
    Private _email As String
    
    ' Violation 1: Simple getter method - should be a property
    Public Function GetName() As String
        Return _name
    End Function
    
    ' Violation 2: Simple setter method - should be a property
    Public Sub SetName(name As String)
        _name = name
    End Sub
    
    ' Violation 3: Protected getter method
    Protected Function GetAge() As Integer
        Return _age
    End Function
    
    ' Violation 4: Protected setter method
    Protected Sub SetAge(age As Integer)
        _age = age
    End Sub
    
    ' Violation 5: Another getter method
    Public Function GetEmail() As String
        Return _email
    End Function
    
    ' Violation 6: Another setter method
    Public Sub SetEmail(email As String)
        _email = email
    End Sub
    
    ' This should NOT be detected - complex method with parameters
    Public Function CalculateAge(birthDate As Date) As Integer
        Return Date.Now.Year - birthDate.Year
    End Function
    
    ' This should NOT be detected - method with business logic
    Public Sub ProcessData(data As String)
        ' Complex processing logic
        Console.WriteLine("Processing: " & data)
    End Sub
    
    ' This should NOT be detected - proper property
    Public Property Address As String
        Get
            Return _address
        End Get
        Set(value As String)
            _address = value
        End Set
    End Property
    
    Private _address As String
End Class
