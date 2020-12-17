# Components

This session explains the components of this project in top down layer

## CO.PaymentGateway.API.V1
User interface Layer on DDD

Responsible for reading json, authorizing requests (Annotation - [AuthorizeCO("WriteProcessPayment")]) and calling ICommand or IQuery.

There is one write and one read Endpoint for AggregateEntity

## CO.PaymentGateway.Business.Logic
Application Layer on DDD

Responsible for handling the different UseCases

## CO.PaymentGateway.Core.Logic
Application and Domain Layer on DDD

Provides Domain logic and interfaces to be extended in the other projects

## CO.PaymentGateway.Data
Infrastructure Layer on DDD

Provides structure to store data with EF and SQL Server

Using CQS there will be a read repository and a write repository

## CO.PaymentGateway.HostApp
Infrastructure Layer on BDD

Starts the service and dependency injection container

## CO.PaymentGateway.Test

Responsible for unit test implementation

For the purpose of this demoproject just 3 were added for time management related to the ```ProcessPaymentCommand class```. 

I didn't have vacations on this project days and I have a baby boy of one year. 
So my spare time to do the project was quite low and I wanted to implement almost of everything on the extra mile

___

## Extra Mile

### 1.API Client - (CO.PaymentGateway.BankClient)

Http client for accessing the bank api 

### 2,3.Authentication and Authorization

Provided by CO.AccessControl

### 4.CO.PaymentGateway.Cache

Implemented as a **_inMemoryCache_** and just for the GetById Endpoint

This also puts a new header on the response ```X-Query-Source```that can be cache or origin

It was planned to be extended to accept a header from the ProcessPayment Write command ```-IdempotencyHeader-``` and store its result there as well. This way if a request have the same idempotency it would return cached

### 5.CO.PaymentGateway.Encryption

The symetric encryption client was implemented using ```Microsoft.AspNetCore.DataProtection```

The key shared with the bank service provider is also a environment variable in launchsettings as ```ENCRYPTIONKEY```

Https already provides asymetric encryption, but the credit card data was encrypted again for purposes of demonstration. 

### 6.Logging

Logging was defined on CONSOLE

### 7.Conteinerization

Running database trough docker

Running 2 other created and uploaded images

### 8.Data storage

The payment is being registered in sql server database 
ConnectionString: ```Server=localhost; Database=PaymentProcess;User Id=sa; Password=Dbpass12345678!; MultipleActiveResultSets=true```




