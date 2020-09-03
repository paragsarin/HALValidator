
[![GitHub license](https://img.shields.io/badge/License-GPLv3-blue.svg)]() 
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)]()

# HAL API Validator
 
HAL Validator API is exposes set of API's to test HAL+JSON complaint specs API.



## Usage

This project will help in validating HAL specification API.

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

### API will be available on port 5000 

 - URL- http://localhost:5000/HALValidations
 - Method/Verb: Post
 - Body:{
"url": "http://localhost/anyapi/anyendpoint",
"headers": {"Authorization":"eyJ0eV1QiLCJhbGciOiJSgggUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6IjU3YzY3OGIzLWM5YmYtNDk4Yy04NGQxLTYxMTc0ZGU0ZTQ0MSIsImggh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiIwIiwiaXiaHR0cDovL3d3dy5yaXNrbWFzdGVyLmNvbSIsImF1ZCI6InJtQVVYIiwibmJmIjoxNTk5MTU0NTg4fQ.mWCGgUiCIiD90UuEKILA-LEmrKZvh84iEsIngKi8Uh0WCot3QcaXAOqCK98EefJDX2gyHfqhhmmKj8ETt9WbInTsmgflkPpXtGoXudTcpXhRjfuY78WXU2HqBT8hbhZjkPP68-1WQYrWO7A7MgDIYqQoucPBR_RtjEVcb2MyWb_teQZFn0kc6UP9DY8HRx2Wa9x-xMLD_PaSQ4VIMEdOIO_9zFNcbAMD7jVZZ5wf1aMfNQPir-69n1cFx-25G7S95UulPs4vLYcHK-W1T9Eqn7n8kTvldnS7bE46Oa_qWAIgijBhrbTAOq98Gt0H96wf5pzNnNkQva-x-Gzg"},
"method": "get"}



## Docker Setup/Support
Docker image for this project can be pulled from dockerhub

```bash
docker pull paragsarin/halvalidator:1.0
````

```bash
docker run -p 5000:80 <image>
````
