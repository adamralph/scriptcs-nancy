# ScriptCs.Nancy

## About

A [script pack](https://github.com/scriptcs/scriptcs/wiki/Script-Packs-master-list) for [scriptcs](https://github.com/scriptcs/scriptcs) for self-hosting [Nancy](https://github.com/NancyFx/Nancy).

ScriptCs.Nancy continues Nancy's journey down the "super-duper-happy-path" by hosting your modules with a single line of code: `Require<NancyPack>().Host();`.

Get it on [Nuget](https://nuget.org/packages/ScriptCs.Nancy/).

## Quickstart

* Create a folder for your script, e.g. `C:\hellonancy`.
* Navigate to your folder and run `scriptcs -install ScriptCs.Nancy`.
* Create a new file named 'start.csx' containing the magic line of code and a sample module:

```C#
Require<NancyPack>().Host();

public class SampleModule : Nancy.NancyModule
{
    public SampleModule()
    {
        Get["/"] = _ => "Hello World!";
    }
}
```

* Run `scriptcs start.csx`.

* Using a browser, navigate to `http://localhost:8888/`.

Congratulations! You've created your first self-hosted website using scriptcs and Nancy!

ScriptCs.Nancy will automatically find any modules both in your script and any others which have been loaded using `#load`. The names of all modules found are printed in the console window (at the time of writing, scriptcs adds the prefix `"Submission#0+"`  to scripted module names).

For a sample which renders two views, one HTML and one plain text, check out the [sample folder](https://github.com/adamralph/scriptcs-nancy/tree/master/src/sample).

## Advanced Usage

As demonstrated above, the simplest `NancyPack` method is:
```C#
public void Host()
```
The  hosts your site at `http://localhost:8888/` using the default `ScriptCs.Nancy.Bootstrapper`.

You can override this behaviour by using one of the richer overloads:
```C#
public void Host(params Assembly[] moduleAssemblies);
public void Host(params Type[] moduleTypes);

public void Host(params string[] baseUriStrings);
public void Host(string baseUriString, IEnumerable<Assembly> moduleAssemblies);
public void Host(string baseUriString, IEnumerable<Type> moduleTypes);

public void Host(params Uri[] baseUris)
public void Host(Uri baseUri, IEnumerable<Assembly> moduleAssemblies);
public void Host(Uri baseUri, HostConfiguration configuration);
public void Host(Uri baseUri, HostConfiguration configuration, IEnumerable<Assembly> moduleAssemblies);
public void Host(Uri baseUri, HostConfiguration configuration, IEnumerable<Type> moduleTypes);
public void Host(Uri baseUri, INancyBootstrapper bootstrapper);
public void Host(Uri baseUri, INancyBootstrapper bootstrapper, HostConfiguration configuration);

public void Host(IEnumerable<Assembly> moduleAssemblies, params Uri[] baseUris);
public void Host(IEnumerable<Type> moduleTypes, params Uri[] baseUris);
public void Host(HostConfiguration configuration, params Uri[] baseUris);
public void Host(HostConfiguration configuration, IEnumerable<Assembly> moduleAssemblies, params Uri[] baseUris);
public void Host(HostConfiguration configuration, IEnumerable<Type> moduleTypes, params Uri[] baseUris);
public void Host(INancyBootstrapper bootstrapper, params Uri[] baseUris);
public void Host(INancyBootstrapper bootstrapper, HostConfiguration configuration, params Uri[] baseUris);
```
### Hosting modules in compiled libraries
Simply call one of the overloads which accepts `Assembly` arguments. E.g.:
```C#
Require<NancyPack>().Host(typeof(MyCompiledModule).Assembly);
```
Note that this overrides the automatic finding of modules in your scripts. To preserve this, provide an extra assembly reference like so:
```C#
Require<NancyPack>().Host(typeof(MyCompiledModule).Assembly, typeof(MyScriptedModule).Assembly);
```
All the overloads which accept `Assembly`, `Type` or `INancyBootstrapper` arguments override the automatic finding of modules in your scripts.

### Managing your own host
You can also ignore the methods on `NancyPack` completely and manage your own host:
```C#
var baseUriString = "http://localhost:8888/";
using (var host = new NancyHost(myBootstrapper, new Uri(baseUriString)))
{
    host.Start();
    
    Console.WriteLine("Nancy is hosted at " + baseUriString);
    Console.WriteLine("Press any key to end");
    Console.ReadKey();
    
    host.Stop();
}
```

In this case ScriptCs is still valuable since it bootstraps your package dependencies and default namespace imports. The trick here is to work out how to write your bootstrapper. ScriptCs.Nancy uses a custom bootstrapper derived from `AutofacNancyBootstrapper`. See the [source](https://github.com/adamralph/scriptcs-nancy/blob/master/src/ScriptCs.Nancy/Bootstrapper.cs "Bootstrapper.cs") for more info.

Have fun!

## Updates

Releases will be pushed regularly to [Nuget](https://nuget.org/packages/ScriptCs.Nancy/). For update notifications, follow [@adamralph](https://twitter.com/#!/adamralph).

To build manually, clone or fork this repository and see ['How to build'](https://github.com/adamralph/scriptcs-nancy/blob/master/how_to_build.md).

## Can I help to improve it and/or fix bugs? ##

Absolutely! Please feel free to raise issues, fork the source code, send pull requests, etc.

No pull request is too small. Even whitespace fixes are appreciated. Before you contribute anything make sure you read [CONTRIBUTING.md](https://github.com/adamralph/scriptcs-nancy/blob/master/CONTRIBUTING.md).

## What do the version numbers mean? ##

ScriptCs.Nancy uses [Semantic Versioning](http://semver.org/). The current release is 0.x which means 'initial development'. Version 1.0 will follow a stable release of scriptcs.