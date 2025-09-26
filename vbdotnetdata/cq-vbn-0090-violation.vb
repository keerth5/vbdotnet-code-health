' Test file for cq-vbn-0090: Only FlagsAttribute enums should have plural names
' Rule should detect non-Flags enums that have plural names (which should be singular)

Imports System

' Violation 1: Regular enum with plural name (should be singular)
Public Enum Colors
    Red = 1
    Green = 2
    Blue = 3
    Yellow = 4
End Enum

' Violation 2: Regular enum with plural name (should be singular)
Friend Enum Statuses
    Pending = 0
    InProgress = 1
    Completed = 2
    Failed = 3
End Enum

' Violation 3: Regular enum with plural name (should be singular)
Public Enum Priorities
    Low = 1
    Medium = 2
    High = 3
    Critical = 4
End Enum

' Violation 4: Regular enum with plural name (should be singular)
Friend Enum Types
    Text = 1
    Number = 2
    Date = 3
    Boolean = 4
End Enum

' Violation 5: Regular enum with plural name (should be singular)
Public Enum Levels
    Beginner = 1
    Intermediate = 2
    Advanced = 3
    Expert = 4
End Enum

' Violation 6: Regular enum with plural name (should be singular)
Public Enum Sizes
    Small = 1
    Medium = 2
    Large = 3
    ExtraLarge = 4
End Enum

' Violation 7: Regular enum with plural name (should be singular)
Friend Enum Directions
    North = 1
    South = 2
    East = 3
    West = 4
End Enum

' Violation 8: Regular enum with plural name (should be singular)
Public Enum Categories
    Electronics = 1
    Clothing = 2
    Books = 3
    Sports = 4
End Enum

' This should NOT be detected - regular enum with proper singular name
Public Enum Color
    Red = 1
    Green = 2
    Blue = 3
    Yellow = 4
End Enum

' This should NOT be detected - regular enum with proper singular name
Friend Enum Status
    Pending = 0
    InProgress = 1
    Completed = 2
    Failed = 3
End Enum

' This should NOT be detected - regular enum with proper singular name
Public Enum Priority
    Low = 1
    Medium = 2
    High = 3
    Critical = 4
End Enum

' This should NOT be detected - Flags enum with proper plural name
<Flags>
Public Enum Permissions
    None = 0
    Read = 1
    Write = 2
    Execute = 4
    All = Read Or Write Or Execute
End Enum

' This should NOT be detected - Flags enum with proper plural name
<Flags>
Friend Enum FileAttributes
    Normal = 0
    ReadOnly = 1
    Hidden = 2
    System = 4
    Archive = 8
End Enum

' This should NOT be detected - Flags enum with proper plural name
<Flags>
Public Enum Options
    Default = 0
    CaseSensitive = 1
    IgnoreWhitespace = 2
    Multiline = 4
    GlobalMatch = 8
End Enum

' Violation 9: Regular enum with plural name (should be singular)
Public Enum Genders
    Male = 1
    Female = 2
    Other = 3
End Enum

' Violation 10: Regular enum with plural name (should be singular)
Friend Enum Formats
    Json = 1
    Xml = 2
    Csv = 3
    Binary = 4
End Enum

' Violation 11: Regular enum with plural name (should be singular)
Public Enum Roles
    Admin = 1
    User = 2
    Guest = 3
    Moderator = 4
End Enum

' Violation 12: Regular enum with plural name (should be singular)
Public Enum States
    Active = 1
    Inactive = 2
    Suspended = 3
    Deleted = 4
End Enum

' This should NOT be detected - regular enum with proper singular name
Friend Enum Gender
    Male = 1
    Female = 2
    Other = 3
End Enum

' This should NOT be detected - regular enum with proper singular name
Public Enum Format
    Json = 1
    Xml = 2
    Csv = 3
    Binary = 4
End Enum

' This should NOT be detected - regular enum with proper singular name
Public Enum Role
    Admin = 1
    User = 2
    Guest = 3
    Moderator = 4
End Enum

' Violation 13: Regular enum with plural name (should be singular)
Friend Enum Protocols
    Http = 1
    Https = 2
    Ftp = 3
    Smtp = 4
End Enum

' Violation 14: Regular enum with plural name (should be singular)
Public Enum Methods
    Get = 1
    Post = 2
    Put = 3
    Delete = 4
End Enum

' This should NOT be detected - regular enum with proper singular name
Friend Enum Protocol
    Http = 1
    Https = 2
    Ftp = 3
    Smtp = 4
End Enum

' This should NOT be detected - regular enum with proper singular name
Public Enum Method
    Get = 1
    Post = 2
    Put = 3
    Delete = 4
End Method

' Violation 15: Regular enum with plural name (should be singular)
Public Enum Animals
    Dog = 1
    Cat = 2
    Bird = 3
    Fish = 4
End Enum

' This should NOT be detected - regular enum with proper singular name
Public Enum Animal
    Dog = 1
    Cat = 2
    Bird = 3
    Fish = 4
End Enum

' This should NOT be detected - Flags enum with proper plural name
<Flags>
Public Enum Features
    None = 0
    Logging = 1
    Caching = 2
    Compression = 4
    Encryption = 8
    All = Logging Or Caching Or Compression Or Encryption
End Enum
