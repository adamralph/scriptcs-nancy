# ScriptCs.Nancy

## About

This is a [Script Pack](https://github.com/scriptcs/scriptcs/wiki/Script-Packs) for [scriptcs](https://github.com/scriptcs/scriptcs) that allows you to self-host [Nancy](https://github.com/NancyFx/Nancy).

Get it on [Nuget](https://nuget.org/packages/ScriptCs.Nancy/).

## Quickstart

* Create a folder for your script, e.g. `C:\hellonancy`.
* Navigate to your folder and run `scriptcs -install ScriptCs.Nancy`.
* Create a new file named 'start.csx' containing:

```
Require<NancyPack>().Host(typeof(SampleModule));

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

For a sample which renders an HTML view, check out the [sample folder](https://github.com/adamralph/scriptcs-nancy/tree/master/src/sample).

## Advanced Usage

The simplest `NancyPack` method is:
```C#
public void Host(params Type[] moduleTypes)
```
The  hosts your site at `http://localhost:8888` using the default `Bootstrapper` built into ScriptCs.Nancy.
You can override this behaviour by using one of the richer overloads:
```C#
public void Host(int port, params Type[] moduleTypes)
public void Host(string baseUriString, params Type[] moduleTypes)
public void Host(INancyBootstrapper bootstrapper, string baseUriString, params Type[] moduleTypes)
public void Host(HostConfiguration hostConfiguration, string baseUriString, params Type[] moduleTypes)
public void Host(HostConfiguration hostConfiguration, INancyBootstrapper bootstrapper, string baseUriString, params Type[] moduleTypes)
```
You can also ignore the methods on `NancyPack` and manage your own host:
```C#
var baseUriString = "http://localhost:8888";
using (var host = new NancyHost(myBootstrapper, new Uri(baseUriString)))
{
    host.Start();
    
    Console.WriteLine("Nancy is running at " + baseUriString);
    Console.WriteLine("Press any key to end");
    Console.ReadKey();
    
    host.Stop();
}
```

Have fun!

## Updates

Releases will be pushed regularly to [Nuget](https://nuget.org/packages/ScriptCs.Nancy/). For update notifications, follow [@adamralph](https://twitter.com/#!/adamralph).

To build manually, clone or fork this repository and see ['How to build'](https://github.com/adamralph/scriptcs-nancy/blob/master/how_to_build.md).

## Can I help to improve it and/or fix bugs? ##

Absolutely! Please feel free to raise issues, fork the source code, send pull requests, etc.

No pull request is too small. Even whitespace fixes are appreciated. Before you contribute anything make sure you read [CONTRIBUTING.md](https://github.com/adamralph/scriptcs-nancy/blob/master/CONTRIBUTING.md).

## What do the version numbers mean? ##

ScriptCs.Nancy uses [Semantic Versioning](http://semver.org/). The current release is 0.x which means 'initial development'. Version 1.0 is imminent.