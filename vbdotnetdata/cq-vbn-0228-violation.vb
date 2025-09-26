' VB.NET test file for cq-vbn-0228: Do not use account shared access signature
' This rule detects usage of account-level shared access signatures

Imports System
Imports Microsoft.WindowsAzure.Storage
Imports Microsoft.WindowsAzure.Storage.Auth
Imports Microsoft.WindowsAzure.Storage.Blob

Public Class AccountSharedAccessSignatureViolations
    
    ' Violation: GetSharedAccessSignature with SharedAccessAccountPolicy
    Public Sub UseAccountSharedAccessSignature()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        Dim policy As New SharedAccessAccountPolicy()
        policy.Permissions = SharedAccessAccountPermissions.Read
        policy.Services = SharedAccessAccountServices.Blob
        policy.ResourceTypes = SharedAccessAccountResourceTypes.Object
        policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1)
        
        ' Violation: GetSharedAccessSignature with SharedAccessAccountPolicy
        Dim sasToken As String = storageAccount.GetSharedAccessSignature(policy)
    End Sub
    
    ' Violation: Multiple account SAS generations
    Public Sub GenerateMultipleAccountSAS()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        ' First policy
        Dim policy1 As New SharedAccessAccountPolicy()
        policy1.Permissions = SharedAccessAccountPermissions.Read Or SharedAccessAccountPermissions.Write
        policy1.Services = SharedAccessAccountServices.Blob
        policy1.ResourceTypes = SharedAccessAccountResourceTypes.Container
        policy1.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(2)
        
        ' Second policy
        Dim policy2 As New SharedAccessAccountPolicy()
        policy2.Permissions = SharedAccessAccountPermissions.Delete
        policy2.Services = SharedAccessAccountServices.Table
        policy2.ResourceTypes = SharedAccessAccountResourceTypes.Service
        policy2.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30)
        
        ' Violation: Multiple GetSharedAccessSignature with SharedAccessAccountPolicy
        Dim sasToken1 As String = storageAccount.GetSharedAccessSignature(policy1)
        Dim sasToken2 As String = storageAccount.GetSharedAccessSignature(policy2)
    End Sub
    
    ' Violation: SharedAccessAccountPolicy instantiation
    Public Sub CreateSharedAccessAccountPolicy()
        ' Violation: SharedAccessAccountPolicy usage
        Dim accountPolicy As New SharedAccessAccountPolicy()
        
        accountPolicy.Permissions = SharedAccessAccountPermissions.List
        accountPolicy.Services = SharedAccessAccountServices.File
        accountPolicy.ResourceTypes = SharedAccessAccountResourceTypes.Object
        accountPolicy.SharedAccessStartTime = DateTimeOffset.UtcNow
        accountPolicy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddDays(1)
    End Sub
    
    ' Violation: Account SAS with all permissions
    Public Sub CreateFullPermissionAccountSAS()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        Dim policy As New SharedAccessAccountPolicy()
        
        ' Violation: SharedAccessAccountPolicy with full permissions
        policy.Permissions = SharedAccessAccountPermissions.Read Or 
                           SharedAccessAccountPermissions.Write Or 
                           SharedAccessAccountPermissions.Delete Or 
                           SharedAccessAccountPermissions.List
        policy.Services = SharedAccessAccountServices.All
        policy.ResourceTypes = SharedAccessAccountResourceTypes.All
        policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddDays(7)
        
        ' Violation: GetSharedAccessSignature with full permission policy
        Dim sasToken As String = storageAccount.GetSharedAccessSignature(policy)
    End Sub
    
    ' Violation: Account SAS in try-catch
    Public Sub CreateAccountSASWithErrorHandling()
        Try
            Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
            
            Dim policy As New SharedAccessAccountPolicy()
            policy.Permissions = SharedAccessAccountPermissions.Read
            policy.Services = SharedAccessAccountServices.Blob
            policy.ResourceTypes = SharedAccessAccountResourceTypes.Container
            policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(4)
            
            ' Violation: GetSharedAccessSignature in try block
            Dim sasToken As String = storageAccount.GetSharedAccessSignature(policy)
            
        Catch ex As Exception
            Console.WriteLine("Account SAS creation error: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: Account SAS with conditional logic
    Public Sub ConditionalAccountSAS(createSAS As Boolean)
        If createSAS Then
            Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
            
            Dim policy As New SharedAccessAccountPolicy()
            policy.Permissions = SharedAccessAccountPermissions.Write
            policy.Services = SharedAccessAccountServices.Queue
            policy.ResourceTypes = SharedAccessAccountResourceTypes.Service
            policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(15)
            
            ' Violation: GetSharedAccessSignature in conditional
            Dim sasToken As String = storageAccount.GetSharedAccessSignature(policy)
        End If
    End Sub
    
    ' Violation: Account SAS in loop
    Public Sub CreateAccountSASInLoop()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim permissions() As SharedAccessAccountPermissions = {
            SharedAccessAccountPermissions.Read,
            SharedAccessAccountPermissions.Write,
            SharedAccessAccountPermissions.Delete
        }
        
        For Each permission As SharedAccessAccountPermissions In permissions
            Dim policy As New SharedAccessAccountPolicy()
            policy.Permissions = permission
            policy.Services = SharedAccessAccountServices.Blob
            policy.ResourceTypes = SharedAccessAccountResourceTypes.Object
            policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1)
            
            ' Violation: GetSharedAccessSignature in loop
            Dim sasToken As String = storageAccount.GetSharedAccessSignature(policy)
        Next
    End Sub
    
    ' Good example (should not be detected - uses service SAS)
    Public Sub UseServiceSAS()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("mycontainer")
        
        Dim policy As New SharedAccessBlobPolicy()
        policy.Permissions = SharedAccessBlobPermissions.Read
        policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1)
        
        ' Good: Using service-level SAS instead of account SAS
        Dim sasToken As String = container.GetSharedAccessSignature(policy)
    End Sub
    
    ' Good example (should not be detected - different method)
    Public Sub UseOtherStorageMethods()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        ' Good: Using other storage operations
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim tableClient As CloudTableClient = storageAccount.CreateCloudTableClient()
    End Sub
    
    ' Violation: Account SAS with return
    Public Function CreateAndReturnAccountSAS() As String
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        Dim policy As New SharedAccessAccountPolicy()
        policy.Permissions = SharedAccessAccountPermissions.List
        policy.Services = SharedAccessAccountServices.File
        policy.ResourceTypes = SharedAccessAccountResourceTypes.Container
        policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(3)
        
        ' Violation: GetSharedAccessSignature for return
        Return storageAccount.GetSharedAccessSignature(policy)
    End Function
    
    ' Violation: SharedAccessAccountPolicy in property
    Public ReadOnly Property AccountPolicy() As SharedAccessAccountPolicy
        Get
            ' Violation: SharedAccessAccountPolicy in property
            Dim policy As New SharedAccessAccountPolicy()
            policy.Permissions = SharedAccessAccountPermissions.Read
            policy.Services = SharedAccessAccountServices.Blob
            policy.ResourceTypes = SharedAccessAccountResourceTypes.Object
            policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1)
            
            Return policy
        End Get
    End Property
    
    ' Violation: Account SAS with variable assignment
    Public Sub AssignAccountSAS()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        Dim policy As New SharedAccessAccountPolicy()
        policy.Permissions = SharedAccessAccountPermissions.Read Or SharedAccessAccountPermissions.Write
        policy.Services = SharedAccessAccountServices.All
        policy.ResourceTypes = SharedAccessAccountResourceTypes.All
        policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddDays(1)
        
        Dim sasToken As String
        
        ' Violation: GetSharedAccessSignature assignment
        sasToken = storageAccount.GetSharedAccessSignature(policy)
    End Sub
    
    ' Violation: Account SAS with method parameter
    Public Sub ProcessAccountSAS(policy As SharedAccessAccountPolicy)
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        ' Violation: GetSharedAccessSignature with parameter policy
        Dim sasToken As String = storageAccount.GetSharedAccessSignature(policy)
    End Sub
    
    ' Violation: Complex account SAS scenario
    Public Sub ComplexAccountSASScenario()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        ' Violation: SharedAccessAccountPolicy creation
        Dim readPolicy As New SharedAccessAccountPolicy() With {
            .Permissions = SharedAccessAccountPermissions.Read,
            .Services = SharedAccessAccountServices.Blob Or SharedAccessAccountServices.File,
            .ResourceTypes = SharedAccessAccountResourceTypes.Object,
            .SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5),
            .SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(2)
        }
        
        ' Violation: GetSharedAccessSignature with complex policy
        Dim readSAS As String = storageAccount.GetSharedAccessSignature(readPolicy)
        
        Console.WriteLine("Account SAS created: " & readSAS.Substring(0, 20) & "...")
    End Sub
    
    ' Violation: Account SAS for different services
    Public Sub CreateServiceSpecificAccountSAS()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        
        ' Blob service account SAS
        Dim blobPolicy As New SharedAccessAccountPolicy()
        blobPolicy.Permissions = SharedAccessAccountPermissions.Read
        blobPolicy.Services = SharedAccessAccountServices.Blob
        blobPolicy.ResourceTypes = SharedAccessAccountResourceTypes.Container Or SharedAccessAccountResourceTypes.Object
        blobPolicy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1)
        
        ' Table service account SAS
        Dim tablePolicy As New SharedAccessAccountPolicy()
        tablePolicy.Permissions = SharedAccessAccountPermissions.Read Or SharedAccessAccountPermissions.Write
        tablePolicy.Services = SharedAccessAccountServices.Table
        tablePolicy.ResourceTypes = SharedAccessAccountResourceTypes.Service
        tablePolicy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(2)
        
        ' Violation: Multiple service-specific account SAS
        Dim blobSAS As String = storageAccount.GetSharedAccessSignature(blobPolicy)
        Dim tableSAS As String = storageAccount.GetSharedAccessSignature(tablePolicy)
    End Sub
End Class
