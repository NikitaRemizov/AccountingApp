# Simple Accounting

Simple Accounting is ASP.NET Web API that allows the consumer to manage information about income and expenses from different categories in a simple way.
<br />
<br />
This application is a REST API service intended to be used with [AccountingApp.Frontend](https://github.com/NikitaRemizov/AccountingApp.Frontend) application.
<br />
<br />
Main features:
* Create/retrieve/update/delete the types (categories) of income or expenses
* Create/retrieve/update/delete the income or expenses records containing information about the date, amount of money, type of budget change
* The Application is created using `.NET 5`,  `ASP.NET Core 5`,  `Entity Framework Core 5`.

## Describe more specificaly how each endpoint is used


# API description

## Registration

### Request
```
POST /register
```

### Request body example
``` 
{
  "email": "example@example.com",
  "password": "12345678abcdABCD"
}
```

### Response body example
``` 
{
    "accessToken": "tokenValue",
    "email": "example@example.com"
}
```

### Response field description
| Field       | Description     |
| ----------- | --------------- |
| accessToken | This is authentification token. Use it in Authorization header of the request. It is required to access data from other endpoints.|

<br />

## Login

### Request
```
POST /login
```

### Request body example
``` 
{
  "email": "example@example.com",
  "password": "12345678abcdABCD"
}
```

### Response body example
``` 
{
    "accessToken": "tokenValue",
    "email": "example@example.com"
}
```

<br />

## Retrieve budget types list

### Request
```
GET /budget/type
```

### Response body example
``` 
[
    {
        "name": "someType",
        "id": "5e5eb4d6-f098-4d47-10f9-08d9189536e5"
    },
    {
        "name": "anotherType",
        "id": "22dfb942-18ac-45e7-422c-08d919059ac6"
    },
    {
        "name": "andAnotherType",
        "id": "fdf81ed5-f4e4-4a3c-b687-08d925cb719b"
    }
]
```
<br />

## Create new type

### Request
```
POST /budget/type
```

### Request body example
``` 
{
    "name": "someType"
}
```

### Response body example
``` 
{
    "id": "e117d833-4441-49d0-f2e7-08d95fcadd55"
}
```

### Response field description
| Field       | Description     |
| ----------- | --------------- |
| id          | The unique ID of the created type |

<br />

## Change the name of the type 

### Request
```
PUT /budget/type
```

### Request body example
``` 
{
    "name": "newTypeName",
    "id": "5e5eb4d6-f098-4d47-10f9-08d9189536e5"
}
```

### Response field description
| Field       | Description     |
| ----------- | --------------- |
| name        | The new name for the type |
| id          | The ID of existing type  |

<br />

## Delete the type

### Request example

```
DELETE /budget/type/5e5eb4d6-f098-4d47-10f9-08d9189536e5
```

<br />

## Retrieve budget changes for specific date

### Request example
```
GET /budget/change/?date=07-27-2021
```

### Response body example
``` 
[
    {
        "date": "2021-07-27:00:00",
        "amount": 200,
        "budgetTypeId": "33be13bd-70be-4b6f-c621-08d95e4cd1c7",
        "budgetTypeName": "someType",
        "id": "c37920ef-1dd3-4439-bfcd-8328a15b0ef0"
    },
    {
        "date": "2021-07-27:00:00",
        "amount": 100,
        "budgetTypeId": "5e5eb4d6-f098-4d47-10f9-08d9189536e5",
        "budgetTypeName": "anotherType",
        "id": "22dfb942-18ac-45e7-422c-08d919059ac6"
    }
]
```

| Field          | Description     |
| -------------- | --------------- |
| date           | The date the budget change is associated with |
| amount         | The amount in cents budget change is assotiated with |
| budgetTypeId   | The ID of the existing type |
| budgetTypeName | The name of the existing type|

<br />

## Retrieve budget changes between dates

### Request example
```
GET /budget/change/?from=07-27-2021&to=07-28-2021
```

### Response body example
``` 
[
    {
        "date": "2021-07-27:00:00",
        "amount": 1000,
        "budgetTypeId": "33be13bd-70be-4b6f-c621-08d95e4cd1c7",
        "budgetTypeName": "someType",
        "id": "c37920ef-1dd3-4439-bfcd-8328a15b0ef0"
    },
    {
        "date": "2021-07-28:00:00",
        "amount": 2000,
        "budgetTypeId": "5e5eb4d6-f098-4d47-10f9-08d9189536e5",
        "budgetTypeName": "anotherType",
        "id": "22dfb942-18ac-45e7-422c-08d919059ac6"
    }
]
```

<br />

## Create new budget change

### Request
```
POST /budget/change
```

### Request body example
``` 
{
    "date": "2021-08-13",
    "amount": -1300,
    "budgetTypeId": "5e5eb4d6-f098-4d47-10f9-08d9189536e5"
}
```

### Response body example
``` 
{
    "id": "e117d833-4441-49d0-f2e7-08d95fcadd55"
}
```
<br />

## Change the parameters of the budget change 

### Request
```
PUT /budget/change
```
### Request body example
``` 
{
    "date": "2021-01-23T00:00:00", //
    "amount": 300,
    "budgetTypeId": "5e5eb4d6-f098-4d47-10f9-08d9189536e5",
    "id": "e117d833-4441-49d0-f2e7-08d95fcadd55"
}
```

<br />

## Delete the type

### Request example

```
DELETE /budget/type/e117d833-4441-49d0-f2e7-08d95fcadd55
```
