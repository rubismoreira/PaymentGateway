# Dependencies

This session explains the dependencies of this project

Docker-compose will raise:

## 1. BankService;

This is a simulator that will return authorized or not by the bank regarding the deny field on the request body.

The bank service is waiting for requests on localhost:8089

It will be configured on CO.PaymentGateway service as a env variable 

```BANKCLIENTURL```

The requests to this API are made directly using the Component **CO.PaymentGateway.BankClient**

Since it is a simulator, just the docker image will be present for download but no source code

### RequestBody
```
{
	"cardencrypteddata": "encryptedDataOfCardInformation", [string]
	"amount": "the buying value", [decimal]
	"deny": "if the bank should deny or not" [bool]
}
```

### ResponseBody

```
{
	"bankresponseid": "unique id of the payment on bank systems", [guid]
	"status": "wether if it was denied or not" [enum Approved=1, Denied=0]
}

```


## 2. CO-AccessControl;

Access control is responsible for Authentication and Authorization per policy Name

It is a service defined and you will be able to verify the code on folder CO.AccessControl

The JWT key is shared among the services that use it as a environmentvariable defined on launchsettings.json

```JWTSECRET```

A principal should realize login there in 
```http://localhost:7771/accesscontrol/login```

It will generate a valid JWT token for access of the CO.PaymentGateway service.

This service also originates the package

```https://www.nuget.org/packages/CO.AcessControl.AcessClient/1.0.2```

This package provides an ExtendedAuthorization attribute 

```
Ex: [AuthorizeCO("WriteProcessPayment")]

It will authenticate the JWT token and authorize if the user have permissions to WriteProcessPayment

```

### Request body

```
{
    "username": "UserFull",
    "password": "1234"
}
```

### Response body

```
{
    "token": "tokenasdasd",
    "userDetails": {
        "id": 3,
        "username": "UserFull",
        "password": "1234",
        "roles": [
            "ReadProcessPayment",
            "WriteProcessPayment"
        ]
    }
}
```

### Data

This authorization service doesn't accept new users and have its 4 users on memory
they are


|Id |Username|Password|Roles|
| :- | :- | :- | :- | 

| Id = 1| Username = "UserRead"| Password = "1234"| Roles = { "ReadProcessPayment"} |
| Id = 2| Username = "UserWrite"| Password = "1234"| Roles =  { "WriteProcessPayment" } |
| Id = 3| Username = "UserFull"| Password = "1234"| Roles = { "ReadProcessPayment", "WriteProcessPayment" } |
| Id = 4| Username = "UserNone"| Password = "1234"| Roles = {}|





## 3. DB;

It will be created a new SQL Server database to store the processed payments
Everything will be stored in the very same table

The database setting is the following environment variable on launchsettings.json

```SQLSERVER_CONNECTION```
