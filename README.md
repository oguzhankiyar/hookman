# OK.Hookman
Some architectures have multiple services. Sometime we need communicate between these services. There a few option for communication. One of them is Webhooks. By using webhooks, when an action occurs, we can send a http request to another service to inform about this action.
<br />
<br />
Hookman project aims to manage your webhooks easily by using API, UI or Library from your application.

## Structure
### Concepts
#### Action
Action is using for grouping hooks. All events should have an action. When an action triggered, all events that specified this actions are sent to own receivers. Action has only one field `Name`.
#### Sender
Sender is using for grouping hook senders. An event can have a sender or not. If the sender is specified, when an action triggered, all events with specified sender are sent to own receivers. If the sender is not specified, then all events that specified this actions are sent to own receivers. Sender has two field `Name` and `Token`. `Token` is an auto-generated field, it should be used when creating a hook.
#### Receiver
Receiver is for grouping hook receivers. All events should have a receiver. When an action triggered, all events with this action are sent to this receiver. Receiver has `Name`, `Url`, `Path`, `Headers` and `QueryStrings` fields. When sending a hook, this fields using for specifying Http Request. `Url`, `Path`, `Headers` and `QueryStrings` fields are bindable using hook's Data field.
#### Event
Event is for specifying each hook request. An event has `Sender`, `Receiver`, `Action`, `Path`, `Headers`, `QueryStrings`, `Body` and `RetryCount` fields. When sending a hook, this fields using for specifying Http Request. `Path`, `Headers`, `QueryStrings` and `Body` fields are bindable using hook's Data field.
#### Hook
Hook is for each Http Request. A hook has `Sender`, `Event`, `Action`, `RequestUrl`, `RequestHeaders`, `RequestBody`, `ResponseCode`, `ResponseHeaders`, `ResponseBody`, `Status` and `Message` fields. To create new hook, you should specify sender's token, action's name or event's id properties. When hook is sent using HttpRequest, Other request parameters will be filled by this process. 

### Projects
#### Core
Contains request/response/model domain classes to by use other packages.
#### Persistence.Core
Contains repository and entity domain classes to use by other packages.
<br />
<br />
Used Packages:
- OK.Hookman.Core
#### Persistence.SqlServer
Responsible to access Microsoft Sql Server database to create/read/update/delete operations
<br />
<br />
Used Packages:
- OK.Hookman.Core
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
#### Service
Responsible to manage Actions, Senders, Receivers, Events, Hooks CRUD operations and send HttpRequests in background using HostedServices.
<br />
<br />
Used Packages:
- OK.Hookman.Persistence.Core
- FluentValidation
- AutoMapper
- Newtonsoft.Json
#### API
Responsible to host Dashboard, Actions, Senders, Receivers, Events, Hooks management endpoints.
<br />
<br />
Used Packages:
- OK.Hookman.Service
- Microsoft.AspNetCore.Mvc
#### Client
Responsible to manage Actions, Senders, Receivers, Events, Hooks via api endpoints. This package connects OK.Hookman.API internally.
<br />
<br />
Used Packages:
- OK.Hookman.Core
- Refit
#### UI
Responsible to provide an admin panel using OK.Hookman.API. Using this panel, users can manage Actions, Senders, Receivers, Events, Hooks and show Dashboard to see analytic charts. This is a netstandard package that provide output files in /static/out folder using Static Files package.
<br />
<br />
Used Packages:
- Microsoft.AspNetCore.StaticFiles
- Microsoft.Extensions.FileProviders
- Angular 8
- Bootstrap 4

## Implementation
### Implementing Persistence
- Add the package
```
dotnet add package OK.Hookman.Persistence.SqlServer
```
- Configure in Startup file
```csharp
// Using
using OK.Hookman.Persistence.SqlServer;

// ConfigureServices
services.AddHookmanPersistenceWithSqlServer(cfg =>
{
    cfg.ConnectionString = "MSSQL_CONNECTION_STRING";
});

// Configure
app.UseHookmanPersistenceWithSqlServer();
```

### Implementing Service
To use this package, you should add Persistence package before.
- Add the package
```
dotnet add package OK.Hookman.Service
```
- Configure in Startup file
```csharp
// Using
using OK.Hookman.Service;

// ConfigureServices
services.AddHookmanService();
```

### Implementing API
To use this package, you should add Persistence and Service packages before.
- Add the package
```
dotnet add package OK.Hookman.API
```
- Configure in Startup file
```csharp
// Using
using OK.Hookman.API;

// ConfigureServices
services.AddHookmanAPI(cfg =>
{
    cfg.ApiPath = "PATH_TO_HOST_HOOKMAN_API";
});

// Configure
app.UseHookmanAPI();
```

### Implementing Client
To use this package, you should host the API package on same or different projects before.
- Add the package
```
dotnet add package OK.Hookman.Client
```
- Configure in Startup file
```csharp
// Using
using OK.Hookman.Client;

// ConfigureServices
services.AddHookmanClient(cfg =>
{
    cfg.ApiUrl = "HOOKMAN_API_URL_YOUR_HOSTED";
});
```

### Implementing UI
To use this package, you should host the API package on same or different projects before.
- Add the package
```
dotnet add package OK.Hookman.UI
```
- Configure in Startup file
```csharp
// Using
using OK.Hookman.UI;

// ConfigureServices
services.AddHookmanUI(cfg =>
{
    cfg.ApiUrl = "HOOKMAN_API_URL_YOUR_HOSTED";
    cfg.UIPath = "PATH_TO_HOST_HOOKMAN_UI";
});

// Configure 
app.UseHookmanUI();
```

## Usage
There three ways to use this project. The options are using Service, API and UI.
### Service package
When you use OK.Hookman.Service package, the library is connect to database directly and process your requests.

```csharp
// Declare service using dependency injection
private readonly IHookService _hookService;

public YourApp(IHookService hookService)
{
    _hookService = hookService;
}

// Use in your block
await _hookService.CreateHookAsync(new HookCreateRequest()
{
    SenderToken = "SENDER_TOKEN",
    ActionName = "SOME_ACTION",
    Data = "{ \"id\": 5 }"
});
```
### API package
When you use API package with OK.Hookman.Client package or own REST client, the library is connect to database directly and process your requests.

```csharp
// Declare service using dependency injection
private readonly IHookServiceClient _hookServiceClient;

public YourApp(IHookServiceClientFactory hookServiceFactory)
{
    _hookService = hookServiceFactory.CreateClient();
}

// Use in your block
await _hookService.CreateHookAsync(new HookCreateRequest()
{
    SenderToken = "SENDER_TOKEN",
    ActionName = "SOME_ACTION",
    Data = "{ \"id\": 5 }"
});
```
### UI package
When you use UI package, OK.Hookman.UI sends requests to the OK.Hookman.API. You should implement both OK.Hookman.API and OK.Hookman.UI package in same or different hosts.

You can see the UI project's screenshots below

## Screenshots

<p float="middle">
<img alt="Dashboard" src="https://user-images.githubusercontent.com/4726180/63724299-1c604e80-c860-11e9-8d21-ded2ddcf5616.png" width="32%" />
<img alt="Actions" src="https://user-images.githubusercontent.com/4726180/63724338-300bb500-c860-11e9-8eed-ba258a2770c6.png" width="32%" />
<img alt="Create Action" src="https://user-images.githubusercontent.com/4726180/63724410-4b76c000-c860-11e9-815e-6220f30b3307.png" width="32%" />
<img alt="Senders" src="https://user-images.githubusercontent.com/4726180/63724432-592c4580-c860-11e9-802f-8b43cf5448bf.png" width="32%" />
<img alt="Create Sender" src="https://user-images.githubusercontent.com/4726180/63724461-6e08d900-c860-11e9-9ecd-a23dcd5a7f57.png" width="32%" />
<img alt="Receivers" src="https://user-images.githubusercontent.com/4726180/63724464-7103c980-c860-11e9-90d4-9d42ac3bc1fe.png" width="32%" />
<img alt="Create Receiver" src="https://user-images.githubusercontent.com/4726180/63724484-782ad780-c860-11e9-9cd1-36fa44ae447c.png" width="32%" />
<img alt="Events" src="https://user-images.githubusercontent.com/4726180/63724485-78c36e00-c860-11e9-9ed6-753716eb91fc.png" width="32%" />
<img alt="Create Event 1" src="https://user-images.githubusercontent.com/4726180/63724488-78c36e00-c860-11e9-8e37-7bdde6361140.png" width="32%" />
<img alt="Create Event 2" src="https://user-images.githubusercontent.com/4726180/63724476-77924100-c860-11e9-8294-8c2a4e40ae22.png" width="32%" />
<img alt="Hooks" src="https://user-images.githubusercontent.com/4726180/63724477-77924100-c860-11e9-8703-68ce306f72b2.png" width="32%" />
<img alt="Create Hook 1" src="https://user-images.githubusercontent.com/4726180/63724478-77924100-c860-11e9-8140-4140d081aefe.png" width="32%" />
<img alt="Create Hook 2" src="https://user-images.githubusercontent.com/4726180/63724479-77924100-c860-11e9-9ded-779982b5c096.png" width="32%" />
<img alt="Hook Details 1" src="https://user-images.githubusercontent.com/4726180/63724481-782ad780-c860-11e9-94a3-dd9aa3b6ffb8.png" width="32%" />
<img alt="Hook Details 2" src="https://user-images.githubusercontent.com/4726180/63724482-782ad780-c860-11e9-9ad7-ce05a250f3fc.png" width="32%" />
</p>

## Next Features
The features below are in next versions of this project.

- Unit Tests
- Authentication
- PostgreSql Persistence
- Audit Logs
- More Charts