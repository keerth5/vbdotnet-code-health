' Test file for cq-vbn-0070: Avoid excessive class coupling
' Rule should detect classes with many field dependencies indicating high coupling

Imports System
Imports System.Collections.Generic
Imports System.IO

' Violation 1: Class with multiple field dependencies (high coupling)
Public Class HighlyCoupledClass
    
    ' Multiple Dim statements indicating dependencies on various types
    Dim _fileManager As FileManager
    Dim _databaseConnection As DatabaseConnection
    Dim _logger As Logger
    Dim _validator As Validator
    Dim _processor As DataProcessor
    Dim _formatter As OutputFormatter
    Dim _converter As TypeConverter
    Dim _handler As EventHandler
    Dim _manager As ResourceManager
    
    Public Sub New()
        _fileManager = New FileManager()
        _databaseConnection = New DatabaseConnection()
        _logger = New Logger()
        _validator = New Validator()
        _processor = New DataProcessor()
        _formatter = New OutputFormatter()
        _converter = New TypeConverter()
        _handler = New EventHandler()
        _manager = New ResourceManager()
    End Sub
    
    Public Sub ProcessData()
        _validator.Validate()
        _processor.Process()
        _formatter.Format()
        _logger.Log("Data processed")
    End Sub
    
End Class

' This should NOT be detected - class with few dependencies
Public Class LowCoupledClass
    
    Dim _service As SimpleService
    
    Public Sub New()
        _service = New SimpleService()
    End Sub
    
    Public Sub DoWork()
        _service.Execute()
    End Sub
    
End Class

' Violation 2: Friend class with multiple dependencies
Friend Class AnotherHighlyCoupledClass
    
    Dim _networkManager As NetworkManager
    Dim _securityProvider As SecurityProvider
    Dim _configurationManager As ConfigurationManager
    Dim _cacheManager As CacheManager
    Dim _auditLogger As AuditLogger
    Dim _performanceMonitor As PerformanceMonitor
    
    Public Sub Initialize()
        _networkManager = New NetworkManager()
        _securityProvider = New SecurityProvider()
        _configurationManager = New ConfigurationManager()
        _cacheManager = New CacheManager()
        _auditLogger = New AuditLogger()
        _performanceMonitor = New PerformanceMonitor()
    End Sub
    
    Public Sub ExecuteOperation()
        _securityProvider.Authenticate()
        _networkManager.Connect()
        _performanceMonitor.StartMonitoring()
        _auditLogger.LogOperation("Operation executed")
    End Sub
    
End Class

' Violation 3: Class with different types of dependencies
Public Class ComplexBusinessClass
    
    Dim _orderService As OrderService
    Dim _customerRepository As CustomerRepository
    Dim _inventoryManager As InventoryManager
    Dim _paymentProcessor As PaymentProcessor
    Dim _shippingCalculator As ShippingCalculator
    Dim _taxCalculator As TaxCalculator
    Dim _discountEngine As DiscountEngine
    Dim _notificationService As NotificationService
    
    Public Sub ProcessOrder(orderId As Integer)
        Dim order = _orderService.GetOrder(orderId)
        Dim customer = _customerRepository.GetCustomer(order.CustomerId)
        _inventoryManager.ReserveItems(order.Items)
        Dim total = _taxCalculator.CalculateTax(order.Total)
        _paymentProcessor.ProcessPayment(customer, total)
        _notificationService.SendConfirmation(customer.Email)
    End Sub
    
End Class

' This should NOT be detected - class with only two dependencies
Public Class ModeratelyCoupledClass
    
    Dim _dataAccess As DataAccess
    Dim _businessLogic As BusinessLogic
    
    Public Sub Execute()
        Dim data = _dataAccess.GetData()
        _businessLogic.Process(data)
    End Sub
    
End Class

' Violation 4: Class with system-level dependencies
Public Class SystemIntegrationClass
    
    Dim _fileSystemWatcher As FileSystemWatcher
    Dim _registryManager As RegistryManager
    Dim _serviceController As ServiceController
    Dim _eventLogManager As EventLogManager
    Dim _performanceCounterManager As PerformanceCounterManager
    Dim _windowsServiceManager As WindowsServiceManager
    
    Public Sub MonitorSystem()
        _fileSystemWatcher.StartWatching()
        _serviceController.StartService("MyService")
        _eventLogManager.WriteEntry("System monitoring started")
        _performanceCounterManager.StartCounters()
    End Sub
    
End Class

' This should NOT be detected - simple data class
Public Class SimpleDataClass
    
    Public Property Name As String
    Public Property Value As Integer
    
    Public Sub Display()
        Console.WriteLine($"{Name}: {Value}")
    End Sub
    
End Class

' Supporting classes for testing (these should not be detected individually)
Public Class FileManager
    Public Sub ManageFiles()
    End Sub
End Class

Public Class DatabaseConnection
    Public Sub Connect()
    End Sub
End Class

Public Class Logger
    Public Sub Log(message As String)
    End Sub
End Class

Public Class Validator
    Public Sub Validate()
    End Sub
End Class

Public Class DataProcessor
    Public Sub Process()
    End Sub
End Class

Public Class OutputFormatter
    Public Sub Format()
    End Sub
End Class

Public Class TypeConverter
    Public Sub Convert()
    End Sub
End Class

Public Class EventHandler
    Public Sub Handle()
    End Sub
End Class

Public Class ResourceManager
    Public Sub Manage()
    End Sub
End Class

Public Class SimpleService
    Public Sub Execute()
    End Sub
End Class

Public Class NetworkManager
    Public Sub Connect()
    End Sub
End Class

Public Class SecurityProvider
    Public Sub Authenticate()
    End Sub
End Class

Public Class ConfigurationManager
    Public Sub LoadConfig()
    End Sub
End Class

Public Class CacheManager
    Public Sub Cache()
    End Sub
End Class

Public Class AuditLogger
    Public Sub LogOperation(operation As String)
    End Sub
End Class

Public Class PerformanceMonitor
    Public Sub StartMonitoring()
    End Sub
End Class

Public Class OrderService
    Public Function GetOrder(id As Integer) As Object
        Return Nothing
    End Function
End Class

Public Class CustomerRepository
    Public Function GetCustomer(id As Integer) As Object
        Return Nothing
    End Function
End Class

Public Class InventoryManager
    Public Sub ReserveItems(items As Object)
    End Sub
End Class

Public Class PaymentProcessor
    Public Sub ProcessPayment(customer As Object, amount As Decimal)
    End Sub
End Class

Public Class ShippingCalculator
    Public Function CalculateShipping() As Decimal
        Return 0
    End Function
End Class

Public Class TaxCalculator
    Public Function CalculateTax(amount As Decimal) As Decimal
        Return amount * 0.1D
    End Function
End Class

Public Class DiscountEngine
    Public Function CalculateDiscount() As Decimal
        Return 0
    End Function
End Class

Public Class NotificationService
    Public Sub SendConfirmation(email As String)
    End Sub
End Class

Public Class DataAccess
    Public Function GetData() As Object
        Return Nothing
    End Function
End Class

Public Class BusinessLogic
    Public Sub Process(data As Object)
    End Sub
End Class

Public Class FileSystemWatcher
    Public Sub StartWatching()
    End Sub
End Class

Public Class RegistryManager
    Public Sub ReadRegistry()
    End Sub
End Class

Public Class ServiceController
    Public Sub StartService(serviceName As String)
    End Sub
End Class

Public Class EventLogManager
    Public Sub WriteEntry(message As String)
    End Sub
End Class

Public Class PerformanceCounterManager
    Public Sub StartCounters()
    End Sub
End Class

Public Class WindowsServiceManager
    Public Sub ManageService()
    End Sub
End Class
