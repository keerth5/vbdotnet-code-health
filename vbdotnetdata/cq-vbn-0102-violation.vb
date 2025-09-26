' VB.NET test file for cq-vbn-0102: Prefer jagged arrays over multidimensional
' Rule: A jagged array is an array whose elements are arrays. The arrays that make up the elements 
' can be of different sizes, which can result in less wasted space for some sets of data.

Imports System

Public Class ArrayExamples
    
    ' Violation: Two-dimensional array declaration
    Private data As Integer(,)
    
    ' Violation: Public two-dimensional array
    Public matrix As Double(,)
    
    ' Violation: Protected two-dimensional array
    Protected grid As String(,)
    
    ' Violation: Friend two-dimensional array
    Friend coordinates As Integer(,)
    
    ' Violation: Three-dimensional array declaration
    Private cube As Integer(,,)
    
    ' Violation: Public three-dimensional array
    Public space As Double(,,)
    
    ' Violation: Protected three-dimensional array  
    Protected volume As String(,,)
    
    ' Violation: Friend three-dimensional array
    Friend tensor As Integer(,,)
    
    ' Violation: Dim with two-dimensional array
    Sub TestMethod()
        Dim localMatrix As Integer(,)
        Dim localGrid As String(,)
        Dim localCube As Double(,,)
    End Sub
    
    ' Violation: Method parameter with multidimensional array
    Public Sub ProcessMatrix(matrix As Integer(,))
        ' Method body
    End Sub
    
    ' Violation: Method return type with multidimensional array
    Public Function CreateMatrix() As Double(,)
        Return New Double(9, 9) {}
    End Function
    
    ' Violation: Property with multidimensional array
    Public Property GameBoard As String(,)
        Get
            Return _gameBoard
        End Get
        Set(value As String(,))
            _gameBoard = value
        End Set
    End Property
    Private _gameBoard As String(,)
    
    ' Violation: Field with complex multidimensional array
    Public Shared StaticMatrix As Integer(,)
    
    ' Violation: ReadOnly field with multidimensional array
    Public Shared ReadOnly DefaultGrid As String(,) = New String(4, 4) {}
    
End Class

' Non-violation examples (these should not be detected):

Public Class JaggedArrayExamples
    
    ' Correct: Jagged array (array of arrays) - should not be detected
    Private jaggedArray As Integer()()
    
    ' Correct: Single-dimensional array - should not be detected
    Private singleArray As Integer()
    
    ' Correct: List instead of array - should not be detected
    Private dataList As List(Of Integer)
    
    ' Correct: Jagged array with different sizes - should not be detected
    Public triangularArray As Integer()()
    
    ' Correct: Method with jagged array parameter - should not be detected
    Public Sub ProcessJaggedArray(arr As Integer()())
        ' Method body
    End Sub
    
    ' Correct: Method returning jagged array - should not be detected
    Public Function CreateJaggedArray() As String()()
        Return New String(4)() {}
    End Function
    
End Class
