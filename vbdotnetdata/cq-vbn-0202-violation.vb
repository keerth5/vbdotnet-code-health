' Test file for cq-vbn-0202: Unsafe DataSet or DataTable in web deserialized object graph
' This rule detects web methods with DataSet or DataTable parameters

Imports System
Imports System.Data
Imports System.Web.Services
Imports System.ServiceModel

Public Class WebMethodDataSetDataTableViolations
    
    ' Violation: WebMethod with DataSet parameter
    <WebMethod>
    Public Function ProcessDataSet(data As DataSet) As String ' Violation
        Return "Processing DataSet"
    End Function
    
    ' Violation: WebMethod with DataTable parameter
    <WebMethod>
    Public Sub ProcessDataTable(table As DataTable) ' Violation
        Console.WriteLine("Processing DataTable")
    End Sub
    
    ' Violation: OperationContract with DataSet parameter
    <OperationContract>
    Public Function ProcessContractDataSet(data As DataSet) As Boolean ' Violation
        Return True
    End Function
    
    ' Violation: OperationContract with DataTable parameter
    <OperationContract>
    Public Sub ProcessContractDataTable(table As DataTable) ' Violation
        Console.WriteLine("Processing contract DataTable")
    End Sub
    
    ' Violation: WebMethod with multiple parameters including DataSet
    <WebMethod>
    Public Function ProcessMultipleParams(id As Integer, data As DataSet, name As String) As String ' Violation
        Return $"Processing {name} with ID {id}"
    End Function
    
    ' Violation: OperationContract with multiple parameters including DataTable
    <OperationContract>
    Public Sub ProcessMultipleContractParams(userId As String, table As DataTable, timestamp As DateTime) ' Violation
        Console.WriteLine($"Processing for user {userId}")
    End Sub
    
    ' Violation: WebMethod with custom DataSet type
    <WebMethod>
    Public Function ProcessCustomDataSet(customData As MyCustomDataSet) As Integer ' Violation
        Return 42
    End Function
    
    ' Violation: OperationContract with custom DataTable type
    <OperationContract>
    Public Sub ProcessCustomDataTable(customTable As MyCustomDataTable) ' Violation
        Console.WriteLine("Processing custom DataTable")
    End Sub
    
    ' Violation: WebMethod with DataSet array parameter
    <WebMethod>
    Public Function ProcessDataSetArray(datasets As DataSet()) As String ' Violation
        Return "Processing DataSet array"
    End Function
    
    ' Violation: OperationContract with DataTable list parameter
    <OperationContract>
    Public Sub ProcessDataTableList(tables As List(Of DataTable)) ' Violation
        Console.WriteLine("Processing DataTable list")
    End Sub
    
    ' Violation: Friend WebMethod with DataSet parameter
    <WebMethod>
    Friend Function ProcessFriendDataSet(data As DataSet) As Object ' Violation
        Return New Object()
    End Function
    
    ' Violation: Friend OperationContract with DataTable parameter
    <OperationContract>
    Friend Sub ProcessFriendDataTable(table As DataTable) ' Violation
        Console.WriteLine("Processing friend DataTable")
    End Sub
    
    ' Non-violation: WebMethod without DataSet/DataTable parameters (should not be detected)
    <WebMethod>
    Public Function SafeWebMethod(id As Integer, name As String) As String ' No violation
        Return $"Safe processing for {name}"
    End Function
    
    ' Non-violation: OperationContract without DataSet/DataTable parameters (should not be detected)
    <OperationContract>
    Public Sub SafeOperationContract(userId As String, data As String) ' No violation
        Console.WriteLine($"Safe processing for {userId}")
    End Sub
    
    ' Non-violation: Regular method with DataSet parameter (should not be detected)
    Public Function RegularMethodWithDataSet(data As DataSet) As String ' No violation - not a web method
        Return "Regular processing"
    End Function
    
    ' Non-violation: Comments and strings
    Public Sub CommentsAndStrings()
        ' This is about <WebMethod> Public Function with DataSet parameter
        Dim message As String = "WebMethod with DataSet parameter can be vulnerable"
        Console.WriteLine("OperationContract with DataTable should be avoided")
    End Sub
    
    ' Violation: WebMethod with DataSet parameter in conditional compilation
    #If DEBUG Then
    <WebMethod>
    Public Function DebugProcessDataSet(data As DataSet) As String ' Violation
        Return "Debug processing DataSet"
    End Function
    #End If
    
    ' Violation: OperationContract with DataTable parameter in conditional compilation
    #If RELEASE Then
    <OperationContract>
    Public Sub ReleaseProcessDataTable(table As DataTable) ' Violation
        Console.WriteLine("Release processing DataTable")
    End Sub
    #End If
    
    ' Violation: WebMethod with Optional DataSet parameter
    <WebMethod>
    Public Function ProcessOptionalDataSet(Optional data As DataSet = Nothing) As String ' Violation
        Return "Processing optional DataSet"
    End Function
    
    ' Violation: OperationContract with ByRef DataTable parameter
    <OperationContract>
    Public Sub ProcessByRefDataTable(ByRef table As DataTable) ' Violation
        Console.WriteLine("Processing ByRef DataTable")
    End Sub
    
    ' Violation: WebMethod with ParamArray DataSet parameters
    <WebMethod>
    Public Function ProcessParamArrayDataSet(ParamArray datasets As DataSet()) As Integer ' Violation
        Return datasets.Length
    End Function
    
    ' Violation: Multiple WebMethod attributes with DataSet parameter
    <WebMethod(Description:="Processes DataSet")>
    <WebMethod(EnableSession:=True)>
    Public Function ProcessDescribedDataSet(data As DataSet) As String ' Violation
        Return "Processing described DataSet"
    End Function
    
    ' Violation: OperationContract with action and DataTable parameter
    <OperationContract(Action:="ProcessTable")>
    Public Sub ProcessActionDataTable(table As DataTable) ' Violation
        Console.WriteLine("Processing action DataTable")
    End Sub

End Class

' Helper classes for testing
Public Class MyCustomDataSet
    Inherits DataSet
End Class

Public Class MyCustomDataTable
    Inherits DataTable
End Class
