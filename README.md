[![GitHub license](https://img.shields.io/badge/License-GPLv3-blue.svg)]() 
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)]()
[![.NET Core Build Actions Status](https://github.com/paragsarin/HALValidator/workflows/.NET%20Core/badge.svg)](https://github.com/paragsarin/HALValidator/actions)
# HAL API Validator  
This Validator API exposes set of API restful endpoints for validating external HAL+JSON API to ensure their complaince with HAL+JSON resource API format.

## Usage
This Validator API exposes set of API restful endpoints for validating external HAL+JSON API to ensure their complaince with HAL+JSON resource API format.

## What us HAL
HAL is a simple format that gives a consistent and easy way to hyperlink between resources in your API.

Adopting HAL will make your API explorable, and its documentation easily discoverable from within the API itself. In short, it will make your API easier to work with and therefore more attractive to client developers.

APIs that adopt HAL can be easily served and consumed using open source libraries available for most major programming languages. It's also simple enough that you can just deal with it as you would any other JSON.

## Contributing
All contributions are welcome

## Development Setup
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.


### API
Contained in the `Validations folder`.

```bash
cd Validations
```

Install the library dependencies.

```bash
dotnet restore
```

Build the API folder.

````bash
dotnet build
````

Start the application/API .

````bash
dotnet run
````

### API will be available on port 5000  (default port)
 - URL- http://localhost:5000/HALValidations
 - Method/Verb: Post
 - Body:{"url": "http://localhost/anyapi/anyendpoint",
			   "headers": {"Authorization":"--YOUR API AUTH KEY--"},
               "method": "get"
              }



## Docker Setup/Support
Docker image for this project can be pulled from dockerhub

```bash
docker pull paragsarin/halvalidator:1.0
````

```bash
docker run -p <host machine port>:80 <imageid>
````
