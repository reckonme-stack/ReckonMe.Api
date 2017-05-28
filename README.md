# ReckonMe.Api

A RESTful service for ReckonMe mobile app.

## Quickstart

In order to launch this project you need to satisfy these requirements:
* Installed .NET Core SDK
* Installed and running MongoDb Server or have remote one
* Modified ./ReckonMe.Api/appsettings.json with proper connection string and credentials

## Endpoints

### Account

Headers:
```
Content-Type: "application/json"
```

#### Login

`POST /api/account/login`

Pyload:
```
{
  "Username": "username",
  "Password": "password"
}
```

Success response:
* Status: `200 OK`
* Body:
```
{
  "access_token": "token",
  "expires_in": 300
}
```

#### Register

`POST /api/account/register`

Pyload:
```
{
  "Username": "username",
  "Password": "password",
  "Email": "username@domain.com"
}
```

Success response 
* Status: `204 NoContent`
* Body: none

### Wallets

Headers:
```
Content-Type: "application/json"
Authorization: "Bearer access_token"
```

#### Get all for user

`GET /api/wallets`

Payload: none

Success response:
* Status: `200 OK`
* Body:
```
[
  {
    "id": "592ac92e1c6ac337885e71b9",
    "name": "wallet1",
    "description": "wallet1desc",
    "owner": "username",
    "members": []
  },
  {
    "id": "592ac92e1c6ads87668asd7a",
    "name": "wallet2",
    "description": "wallet2desc",
    "owner": "username",
    "members": []
  }
]
```

#### Get by id

`Get /api/wallets/{id}`

Payload: none

Success response:
* Status: `200 OK`
* Body:
```
{
  "id": "592ac92e1c6ac337885e71b9",
  "name": "wallet1",
  "description": "wallet1desc",
  "owner": "username",
  "members": []
}
```

#### Add

`POST /api/wallets`

Payload: 
```
{

}
```

Success response:
* Status: `204 NoContent`
* Body: None

#### Edit

`PUT /api/wallets/{id}`

Payload: 
```
{

}
```

Success response:
* Status: `204 NoContent`
* Body: None

#### Delete

`DELETE /api/wallets/{id}`

Payload: None

Success response:
* Status: `204 NoContent`
* Body: None
