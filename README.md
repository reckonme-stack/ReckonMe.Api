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
    "id": "592ac92e1c6ads87668asd7a",
    "name": "name",
    "description": "description",
    "owner": "username",
    "members": [
        "member"
      ],
    "expenses": [
      {
        "name": "expense",
        "description": "description",
        "value": 999.99,
        "payer": "payer",
        "date": "2017-06-22 00:00:00",
        "members": [
            "member"
          ]
      }
      ]
  },
  {
    "id": "592f1b5f72dbc60f9c6ec0c8",
    "name": "name",
    "description": "description",
    "owner": "username",
    "members": [
        "member"
      ],
    "expenses": [
      {
        "name": "expense",
        "description": "description",
        "value": 999.99,
        "payer": "payer",
        "date": "2017-06-22 00:00:00",
        "members": [
            "member"
          ]
      }
      ]
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
  "id": "592f1b5f72dbc60f9c6ec0c8",
	"name": "name",
	"description": "description",
	"owner": "username",
	"members": [
			"member"
		],
	"expenses": [
		{
			"name": "expense",
			"description": "description",
			"value": 999.99,
			"payer": "payer",
			"date": "2017-06-22 00:00:00",
			"members": [
					"member"
				]
		}
		]
}
```

#### Add

`POST /api/wallets`

Payload: 
```
{
	"Name": "name",
	"Description": "description",
	"Owner": "username",
	"Members": [
			"member"
		],
	"Expenses": [
		{
			"Name": "expense",
			"Description": "description",
			"Value": 999.99,
			"Payer": "payer",
			"Date": "2017-06-22 00:00:00",
			"Members": [
					"member"
				]
		}
		]
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
	"Name": "name",
	"Description": "description",
	"Owner": "username",
	"Members": [
			"member"
		],
	"Expenses": [
		{
			"Name": "expense",
			"Description": "description",
			"Value": 999.99,
			"Payer": "payer",
			"Date": "2017-06-22 00:00:00",
			"Members": [
					"member"
				]
		}
		]
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
