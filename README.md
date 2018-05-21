## NetCoreStack Localization
### Database Resource Localization for .NET Core with Entity Framework and In Memory Cache
[![NuGet](https://img.shields.io/nuget/v/NetCoreStack.Localization.svg?longCache=true&style=flat-square)](https://www.nuget.org/packages/NetCoreStack.Localization)
[![NuGet](https://img.shields.io/nuget/dt/NetCoreStack.Localization.svg?longCache=true&style=flat-square)](https://www.nuget.org/packages/NetCoreStack.Localization)

[Latest release on Nuget](https://www.nuget.org/packages/NetCoreStack.Localization)

### Requirements:
* .NET Core 2.0 or later
* SQL Server 2008-2016

#### Install for .NET Core
```
PM> Install-Package NetCoreStack.Localization
```

### Features
* .NET Core Resources in MsSql Server
* Injectable .NET Core StringLocalizers
* Serve Resources to JavaScript as JSON
* Directly access and manage Languages/Resources with api and code

### Installation
##### AppSettings Configuration
Configuration settings in `AppSettings.json`:
```json
{
	"DbSettings": {
    	"SqlConnectionString": "Server=.;Database=LocalizationTest;Trusted_Connection=True;MultipleActiveResultSets=true"
  	}
}
```
##### Enable NetCoreStack.Localization in ASP.NET Core
```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddNetCoreStackMvc(options =>
	{
		options.AppName = "NetCoreStack Localization";
	});

	services.AddMvc();
	
	//Required
	services.AddNetCoreStackLocalization(Configuration);
}
```

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	app.UseStaticFiles();
	app.UseNetCoreStackMvc();
	
	//Required
	app.UseNetCoreStackLocalization();

	app.UseMvc(routes =>
	{
    	routes.MapRoute(
			name: "default",
			template: "{controller=Home}/{action=Index}/{id?}");
	});
}
```

------

### Test Project Preview

##### Home Page / Client side localization
<a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_01.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_01.png?raw=true" align="center" width="35%" ></a>


##### Forms / Validations
<a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_02.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_02.png?raw=true" align="center" width="35%" ></a>

##### Api
<a href="https://github.com/NetCoreStack/Localization/blob/master/Sample_03.png?raw=true" target="_blank"><img src="https://github.com/NetCoreStack/Localization/blob/master/Sample_03.png?raw=true" align="center" width="35%" ></a>