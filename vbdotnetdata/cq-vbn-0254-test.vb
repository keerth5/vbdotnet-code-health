' Test file for cq-vbn-0254: Do not always skip token validation in delegates
' Rule should detect: AudienceValidator or LifetimeValidator always returning true

Imports Microsoft.IdentityModel.Tokens

Public Class TokenValidatorTest
    
    ' VIOLATION: AudienceValidator always returns True
    Public Function BadTokenValidator1() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.AudienceValidator = Function(audiences, securityToken, validationParameters) True
        Return parameters
    End Function
    
    ' VIOLATION: LifetimeValidator always returns True
    Public Function BadTokenValidator2() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.LifetimeValidator = Function(notBefore, expires, securityToken, validationParameters) True
        Return parameters
    End Function
    
    ' VIOLATION: AudienceValidator using AddressOf to method that returns True
    Public Function BadTokenValidator3() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.AudienceValidator = AddressOf AlwaysValidateAudience
        Return parameters
    End Function
    
    ' VIOLATION: LifetimeValidator using AddressOf to method that returns True  
    Public Function BadTokenValidator4() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.LifetimeValidator = AddressOf AlwaysValidateLifetime
        Return parameters
    End Function
    
    ' Method that always returns True (should be detected when used with AddressOf)
    Private Function AlwaysValidateAudience(audiences As IEnumerable(Of String), securityToken As SecurityToken, validationParameters As TokenValidationParameters) As Boolean
        Return True
    End Function
    
    Private Function AlwaysValidateLifetime(notBefore As DateTime?, expires As DateTime?, securityToken As SecurityToken, validationParameters As TokenValidationParameters) As Boolean
        Return True
    End Function
    
    ' GOOD: AudienceValidator with proper validation logic - should NOT be flagged
    Public Function GoodTokenValidator1() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.AudienceValidator = Function(audiences, securityToken, validationParameters)
                                           Return audiences.Contains("valid-audience")
                                       End Function
        Return parameters
    End Function
    
    ' GOOD: LifetimeValidator with proper validation logic - should NOT be flagged
    Public Function GoodTokenValidator2() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.LifetimeValidator = Function(notBefore, expires, securityToken, validationParameters)
                                           Return DateTime.Now >= notBefore AndAlso DateTime.Now <= expires
                                       End Function
        Return parameters
    End Function
    
    ' GOOD: Normal assignment - should NOT be flagged
    Public Function NormalMethod() As Boolean
        Dim result As Boolean = True
        Return result
    End Function
    
End Class
