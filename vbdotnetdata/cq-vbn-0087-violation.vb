' Test file for cq-vbn-0087: Flags enums should have plural names
' Rule should detect enums with FlagsAttribute that have singular names instead of plural

Imports System

' Violation 1: Flags enum with singular name
<Flags>
Public Enum Permission
    None = 0
    Read = 1
    Write = 2
    Execute = 4
    All = Read Or Write Or Execute
End Enum

' Violation 2: Flags enum with singular name (Friend)
<Flags>
Friend Enum FileAttribute
    Normal = 0
    ReadOnly = 1
    Hidden = 2
    System = 4
    Archive = 8
End Enum

' Violation 3: Flags enum with singular name (different context)
<Flags>
Public Enum Style
    None = 0
    Bold = 1
    Italic = 2
    Underline = 4
    Strikethrough = 8
End Enum

' Violation 4: Flags enum with singular name
<Flags>
Friend Enum Option
    Default = 0
    CaseSensitive = 1
    IgnoreWhitespace = 2
    Multiline = 4
    GlobalMatch = 8
End Enum

' Violation 5: Flags enum with singular name (different pattern)
<Flags>
Public Enum AccessRight
    None = 0
    Read = 1
    Write = 2
    Delete = 4
    Modify = 8
    FullControl = Read Or Write Or Delete Or Modify
End Enum

' Violation 6: Flags enum with singular name
<Flags>
Public Enum Feature
    None = 0
    Logging = 1
    Caching = 2
    Compression = 4
    Encryption = 8
    All = Logging Or Caching Or Compression Or Encryption
End Enum

' Violation 7: Flags enum with singular name (Friend)
<Flags>
Friend Enum Setting
    Default = 0
    AutoSave = 1
    ShowToolbar = 2
    EnableSound = 4
    DarkMode = 8
End Enum

' Violation 8: Flags enum with singular name
<Flags>
Public Enum SecurityLevel
    None = 0
    Basic = 1
    Enhanced = 2
    Maximum = 4
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
Public Enum Styles
    None = 0
    Bold = 1
    Italic = 2
    Underline = 4
    Strikethrough = 8
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

' This should NOT be detected - regular enum without Flags attribute (singular is correct)
Public Enum Color
    Red = 1
    Green = 2
    Blue = 3
    Yellow = 4
End Enum

' This should NOT be detected - regular enum without Flags attribute
Friend Enum Status
    Pending = 0
    InProgress = 1
    Completed = 2
    Failed = 3
End Enum

' This should NOT be detected - regular enum without Flags attribute
Public Enum Priority
    Low = 1
    Medium = 2
    High = 3
    Critical = 4
End Enum

' Violation 9: Another Flags enum with singular name
<Flags>
Public Enum NetworkProtocol
    None = 0
    Http = 1
    Https = 2
    Ftp = 4
    Smtp = 8
    All = Http Or Https Or Ftp Or Smtp
End Enum

' Violation 10: Flags enum with singular name in different context
<Flags>
Friend Enum DatabaseFlag
    None = 0
    ReadCommitted = 1
    Serializable = 2
    RepeatableRead = 4
    ReadUncommitted = 8
End Enum

' This should NOT be detected - Flags enum with proper plural name
<Flags>
Public Enum NetworkProtocols
    None = 0
    Http = 1
    Https = 2
    Ftp = 4
    Smtp = 8
    All = Http Or Https Or Ftp Or Smtp
End Enum

' This should NOT be detected - Flags enum with proper plural name
<Flags>
Friend Enum DatabaseFlags
    None = 0
    ReadCommitted = 1
    Serializable = 2
    RepeatableRead = 4
    ReadUncommitted = 8
End Enum

' Violation 11: Flags enum with singular name (compound word)
<Flags>
Public Enum TextFormat
    None = 0
    PlainText = 1
    RichText = 2
    Html = 4
    Markdown = 8
End Enum

' This should NOT be detected - Flags enum with proper plural name (compound word)
<Flags>
Public Enum TextFormats
    None = 0
    PlainText = 1
    RichText = 2
    Html = 4
    Markdown = 8
End Enum
