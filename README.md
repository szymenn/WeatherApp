# WeatherApp
Simple weather web app I created in order to learn basics of web development using C#. Any feedback is much welcomed. 
## Features
-  Saving, deleting and getting weather for particular city.
- Getting 7 day forecast for specified city
- Authentication and authorization using OpenIdConnect protocol
## Used Technologies
- C# 7.3
- ASP.NET Core 2.2/2.1
- Entity Framework Core 2.1.2
- IdentityServer4 
- AutoMapper
- PostgreSQL
- xUnit
- Moq
## WeatherApp consists of 3 separate applications 
- IdentityServerService   - IdentityServer4 app responsible for user store, authentication and authorization.
- WeatherApi - ASP.NET Core web API responsible for managing weather data and getting weather data from another web API. 

- WebClient - ASP.NET Core MVC app responsible for consuming WeatherApi and communicating with IdentityServerService app. 


## Other information

WeatherApp is using [APIXU](https://www.apixu.com/) web API to get current weather data.
WebClient app and IdentityServerService app are using [this](https://www.themezy.com/free-website-templates/128-steel-weather-free-responsive-website-template) slightly modified Themezy template. 
IdentityServerService is mostly based on [IdentityServer4 ASP.NET Identity template](https://github.com/IdentityServer/IdentityServer4.Templates/tree/dev/src/IdentityServer4AspNetIdentity), some changes include adding user registration and changing css stylesheet. 
