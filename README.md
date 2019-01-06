# NetCoreStack Localization v1.0.6
## Database Resource Localization for .NET Core with Entity Framework and In Memory Cache
[![NuGet](https://img.shields.io/nuget/v/NetCoreStack.Localization.svg?longCache=true&style=flat-square)](https://www.nuget.org/packages/NetCoreStack.Localization)
[![NuGet](https://img.shields.io/nuget/dt/NetCoreStack.Localization.svg?longCache=true&style=flat-square)](https://www.nuget.org/packages/NetCoreStack.Localization)

## Links
* **Online Demo Page:** [http://netcorestack-localization-test.herokuapp.com/](http://netcorestack-localization-test.herokuapp.com/)

* **Docs:** [https://netcorestack.github.io/Localization/](https://netcorestack.github.io/Localization/)

* **Latest release on Nuget:** [https://www.nuget.org/packages/NetCoreStack.Localization](https://www.nuget.org/packages/NetCoreStack.Localization)

* **Docker Image:** [https://hub.docker.com/r/tahaipek/netcorestack-localization-test-hosting](https://hub.docker.com/r/tahaipek/netcorestack-localization-test-hosting)


## Docker File
```
PM> docker pull tahaipek/netcorestack-localization-test-hosting
PM> docker run -d -p 5003:80 netcorestack-localization-test-hosting
```

## Requirements:
* .NET Core 2.1 or later
* SQLite or MsSQL Server

### Install for .NET Core
```
PM> Install-Package NetCoreStack.Localization
```

## Features
* .NET Core Resources in MsSql Server
* Injectable .NET Core StringLocalizers
* Serve Resources to JavaScript as JSON
* Directly access and manage Languages/Resources with api and code


## Installation
### **AppSettings Configuration**
Configuration settings in `AppSettings.json`:
```json
{
	"DbSettings": {
		"SqlConnectionString": "Server=.;Database=LocalizationTest;Trusted_Connection=True;MultipleActiveResultSets=true"
	},
	"LocalizationSettings": {
		"UseDefaultLanguageWhenValueIsNull": true
  	}
}
```
### **Enable NetCoreStack.Localization in ASP.NET Core**
```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddNetCoreStackMvc(options => { options.AppName = "NetCoreStack Localization"; });
	services.AddNetCoreStackLocalization(Configuration);
}
```

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	app.UseNetCoreStackMvc();
	app.UseNetCoreStackLocalization();
}
```


### **Client-side localization in \*.cshtml file**
```html
<head>
	<!-- Optional: The resources defined javascript. =>  "window.culture.resource"  -->
	<netcorestack-javascriptregistrar></netcorestack-javascriptregistrar>
    
	<!-- 
	Optional: 
	   - If you want cookies to be set by JavaScript, you should use this.   
	   - If you don't want cookies to be set by JavaScript, remove this line. It will automatically redirect to Controller Action.
	-->
	<netcorestack-languageSelector-scripts></netcorestack-languageSelector-scripts>
</head>
<body>
	<!--  
	Required: Language Selector Combobox
	Optional:
	   - If you want cookies to be set by JavaScript, you should set "set-cookie-with-java-script" property.   
	   - If you don't want cookies to be set by JavaScript, the application sets it through Controller Action.
	-->
	<netcorestack-languageSelector name="culture" set-cookie-with-java-script="true"></netcorestack-languageSelector>
	
	@Localizer["Logo_Description"]
</body>
```

### **Back-end Localization in \*.cs file**
```csharp 
public class HomeController : Controller
{
	private readonly IStringLocalizer _stringLocalizer;
	public HomeController(IStringLocalizer stringLocalizer)
	{
		_stringLocalizer = stringLocalizer;
	}

	public IActionResult About()
	{
		ViewData["Message"] = _stringLocalizer["AboutPageDescription"];
		return View();
	}
}
```


### **Test Project Preview**

| How To Use  | Forms & Validations|
| ------------- | ------------- |
| <a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_01.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_01.png?raw=true" align="center" width="90%" ></a>  | <a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_02.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_02.png?raw=true" align="center" width="90%" ></a>  |


| Component Api  | Client-Side Localization|
| ------------- | ------------- |
| <a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_03.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_03.png?raw=true" align="center" width="90%" ></a>  | <a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_06.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_06.png?raw=true" align="center" width="90%" ></a>  |


| Exception Localization  | AjaxException Localization |
| ------------- | ------------- |
| <a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_05.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_05.png?raw=true" align="center" width="90%" ></a>  | <a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_04.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_04.png?raw=true" align="center" width="90%" ></a>  |




---------
#### .Net Core Localization
#### .Net Core Localization with Entity Framework
#### .Net Core Database Localization
