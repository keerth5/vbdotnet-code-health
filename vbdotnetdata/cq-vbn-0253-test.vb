' Test file for cq-vbn-0253: Do not disable token validation checks
' Rule should detect: TokenValidationParameters properties set to False

Imports Microsoft.IdentityModel.Tokens

Public Class TokenValidationTest
    
    ' VIOLATION: ValidateIssuer set to False
    Public Function BadTokenValidation1() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.ValidateIssuer = False
        Return parameters
    End Function
    
    ' VIOLATION: ValidateAudience set to False
    Public Function BadTokenValidation2() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.ValidateAudience = False
        Return parameters
    End Function
    
    ' VIOLATION: ValidateLifetime set to False
    Public Function BadTokenValidation3() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.ValidateLifetime = False
        Return parameters
    End Function
    
    ' VIOLATION: ValidateIssuerSigningKey set to False
    Public Function BadTokenValidation4() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.ValidateIssuerSigningKey = False
        Return parameters
    End Function
    
    ' VIOLATION: Multiple validation properties set to False in object initializer
    Public Function BadTokenValidation5() As TokenValidationParameters
        Return New TokenValidationParameters With {
            .ValidateIssuer = False,
            .ValidateAudience = False
        }
    End Function
    
    ' GOOD: Validation properties set to True - should NOT be flagged
    Public Function GoodTokenValidation1() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.ValidateIssuer = True
        parameters.ValidateAudience = True
        parameters.ValidateLifetime = True
        parameters.ValidateIssuerSigningKey = True
        Return parameters
    End Function
    
    ' GOOD: Default validation (no explicit setting) - should NOT be flagged
    Public Function GoodTokenValidation2() As TokenValidationParameters
        Dim parameters As New TokenValidationParameters()
        parameters.IssuerSigningKey = New SymmetricSecurityKey(New Byte() {})
        Return parameters
    End Function
    
    ' GOOD: Normal boolean assignment - should NOT be flagged
    Public Function NormalMethod() As Boolean
        Dim result As Boolean = False
        Return result
    End Function
    
End Class
