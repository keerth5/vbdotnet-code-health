' VB.NET test file for cq-vbn-0229: Use container level access policy
' This rule detects GetSharedAccessSignature without container-level policy

Imports System
Imports Microsoft.WindowsAzure.Storage
Imports Microsoft.WindowsAzure.Storage.Blob

Public Class ContainerLevelAccessPolicyViolations
    
    ' Violation: GetSharedAccessSignature without SharedAccessBlobPolicy
    Public Sub UseGetSharedAccessSignatureWithoutPolicy()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("mycontainer")
        
        ' Violation: GetSharedAccessSignature without container policy
        Dim sasToken As String = container.GetSharedAccessSignature()
    End Sub
    
    ' Violation: Multiple GetSharedAccessSignature calls without policy
    Public Sub MultipleGetSharedAccessSignatureWithoutPolicy()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        
        Dim container1 As CloudBlobContainer = blobClient.GetContainerReference("container1")
        Dim container2 As CloudBlobContainer = blobClient.GetContainerReference("container2")
        
        ' Violation: Multiple GetSharedAccessSignature without policy
        Dim sasToken1 As String = container1.GetSharedAccessSignature()
        Dim sasToken2 As String = container2.GetSharedAccessSignature()
    End Sub
    
    ' Violation: GetSharedAccessSignature in try-catch
    Public Sub GetSharedAccessSignatureWithErrorHandling()
        Try
            Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
            Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
            Dim container As CloudBlobContainer = blobClient.GetContainerReference("errorcontainer")
            
            ' Violation: GetSharedAccessSignature without policy in try block
            Dim sasToken As String = container.GetSharedAccessSignature()
            
        Catch ex As Exception
            Console.WriteLine("SAS generation error: " & ex.Message)
        End Try
    End Sub
    
    ' Violation: GetSharedAccessSignature with conditional logic
    Public Sub ConditionalGetSharedAccessSignature(generateSAS As Boolean)
        If generateSAS Then
            Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
            Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
            Dim container As CloudBlobContainer = blobClient.GetContainerReference("conditionalcontainer")
            
            ' Violation: GetSharedAccessSignature without policy in conditional
            Dim sasToken As String = container.GetSharedAccessSignature()
        End If
    End Sub
    
    ' Violation: GetSharedAccessSignature in loop
    Public Sub GetSharedAccessSignatureInLoop()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        
        Dim containerNames() As String = {"container1", "container2", "container3"}
        
        For Each containerName As String In containerNames
            Dim container As CloudBlobContainer = blobClient.GetContainerReference(containerName)
            
            ' Violation: GetSharedAccessSignature without policy in loop
            Dim sasToken As String = container.GetSharedAccessSignature()
        Next
    End Sub
    
    ' Violation: GetSharedAccessSignature with variable assignment
    Public Sub AssignGetSharedAccessSignature()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("assigncontainer")
        
        Dim sasToken As String
        
        ' Violation: GetSharedAccessSignature assignment without policy
        sasToken = container.GetSharedAccessSignature()
    End Sub
    
    ' Violation: GetSharedAccessSignature with return
    Public Function CreateAndReturnSAS() As String
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("returncontainer")
        
        ' Violation: GetSharedAccessSignature without policy for return
        Return container.GetSharedAccessSignature()
    End Function
    
    ' Good example (should not be detected - uses SharedAccessBlobPolicy)
    Public Sub UseGetSharedAccessSignatureWithPolicy()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("policycontainer")
        
        Dim policy As New SharedAccessBlobPolicy()
        policy.Permissions = SharedAccessBlobPermissions.Read
        policy.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1)
        
        ' Good: Using SharedAccessBlobPolicy with GetSharedAccessSignature
        Dim sasToken As String = container.GetSharedAccessSignature(policy)
    End Sub
    
    ' Good example (should not be detected - different method)
    Public Sub UseOtherContainerMethods()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("othercontainer")
        
        ' Good: Using other container operations
        container.CreateIfNotExists()
        Dim exists As Boolean = container.Exists()
    End Sub
    
    ' Violation: GetSharedAccessSignature with method parameter
    Public Sub ProcessContainerSAS(container As CloudBlobContainer)
        ' Violation: GetSharedAccessSignature without policy on parameter
        Dim sasToken As String = container.GetSharedAccessSignature()
    End Sub
    
    ' Violation: GetSharedAccessSignature in property
    Public ReadOnly Property ContainerSAS() As String
        Get
            Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
            Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
            Dim container As CloudBlobContainer = blobClient.GetContainerReference("propertycontainer")
            
            ' Violation: GetSharedAccessSignature without policy in property
            Return container.GetSharedAccessSignature()
        End Get
    End Property
    
    ' Violation: GetSharedAccessSignature with null policy
    Public Sub GetSharedAccessSignatureWithNullPolicy()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("nullpolicycontainer")
        
        ' Violation: GetSharedAccessSignature with Nothing/null policy
        Dim sasToken As String = container.GetSharedAccessSignature(Nothing)
    End Sub
    
    ' Violation: GetSharedAccessSignature in complex scenario
    Public Sub ComplexGetSharedAccessSignatureScenario()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        
        ' Create multiple containers
        Dim containers As New List(Of CloudBlobContainer)()
        For i As Integer = 1 To 5
            Dim container As CloudBlobContainer = blobClient.GetContainerReference("container" & i.ToString())
            containers.Add(container)
        Next
        
        ' Generate SAS for each container
        For Each container As CloudBlobContainer In containers
            ' Violation: GetSharedAccessSignature without policy in complex scenario
            Dim sasToken As String = container.GetSharedAccessSignature()
            Console.WriteLine("SAS for " & container.Name & ": " & sasToken.Substring(0, 20) & "...")
        Next
    End Sub
    
    ' Violation: GetSharedAccessSignature with blob reference
    Public Sub GetSharedAccessSignatureFromBlob()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("blobcontainer")
        Dim blob As CloudBlockBlob = container.GetBlockBlobReference("myblob.txt")
        
        ' Violation: GetSharedAccessSignature without policy from blob's container
        Dim sasToken As String = blob.Container.GetSharedAccessSignature()
    End Sub
    
    ' Violation: GetSharedAccessSignature with permissions but no policy
    Public Sub GetSharedAccessSignatureWithPermissions()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("permissionscontainer")
        
        ' Set container permissions but still use GetSharedAccessSignature without policy
        Dim permissions As New BlobContainerPermissions()
        permissions.PublicAccess = BlobContainerPublicAccessType.Blob
        container.SetPermissions(permissions)
        
        ' Violation: GetSharedAccessSignature without policy even with set permissions
        Dim sasToken As String = container.GetSharedAccessSignature()
    End Sub
    
    ' Violation: GetSharedAccessSignature in nested method
    Public Sub NestedGetSharedAccessSignature()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        
        Dim generateSAS As Func(Of String, String) = Function(containerName As String)
                                                         Dim container As CloudBlobContainer = blobClient.GetContainerReference(containerName)
                                                         
                                                         ' Violation: GetSharedAccessSignature without policy in lambda
                                                         Return container.GetSharedAccessSignature()
                                                     End Function
        
        Dim sasToken As String = generateSAS("nestedcontainer")
    End Sub
    
    ' Violation: GetSharedAccessSignature with using statement
    Public Sub GetSharedAccessSignatureInUsing()
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse("connection_string")
        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
        Dim container As CloudBlobContainer = blobClient.GetContainerReference("usingcontainer")
        
        ' Violation: GetSharedAccessSignature without policy
        Dim sasToken As String = container.GetSharedAccessSignature()
        
        Console.WriteLine("Generated SAS: " & sasToken)
    End Sub
End Class
