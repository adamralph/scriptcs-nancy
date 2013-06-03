# [![ScriptCs](https://secure.gravatar.com/avatar/5c754f646971d8bc800b9d4057931938?s=200)](http://scriptcs.net/).[![Nancy](https://secure.gravatar.com/avatar/8e00fa6da668702f8b73ac4caebfbee4?s=200)](http://nancyfx.org/)

A [scriptcs](https://github.com/scriptcs/scriptcs) [script pack](https://github.com/scriptcs/scriptcs/wiki/Script-Packs-master-list) for [Nancy](https://github.com/NancyFx/Nancy).

Continue your journey down the super-duper-happy-path by self-hosting Nancy with a single line of code:
```C#
Require<NancyPack>().Host();
```

Get it on [Nuget](https://nuget.org/packages/ScriptCs.Nancy/).

## Quickstart

* Ensure you have [scriptcs installed](https://github.com/scriptcs/scriptcs#getting-scriptcs).
* Open a command prompt *as an administrator*.
* Create a folder, e.g. `mkdir C:\hellonancy`.
* Navigate to your folder and run `scriptcs -install ScriptCs.Nancy`.
* Create a file named `start.csx` containing the magic line of code and a sample module:

```C#
Require<NancyPack>().Host();

public class SampleModule : NancyModule
{
    public SampleModule()
    {
        Get["/"] = _ => "Hello World!";
    }
}
```

* Run `scriptcs start.csx`.

* Browse to `http://localhost:8888/`.

Congratulations! You've created your first self-hosted website using scriptcs and Nancy!

(For a slightly more advanced sample see [this sample](https://github.com/adamralph/scriptcs-nancy/blob/master/src/sample/start1.csx).)

## How it works

ScriptCs.Nancy auto-magically finds all modules in your script (including those loaded with [`#load`](https://github.com/scriptcs/scriptcs/wiki/Writing-a-script#loading-referenced-scripts "Loading referenced scripts")). The names of all modules found are printed in the console window (at the time of writing, scriptcs adds the prefix `"Submission#0+"` to the names of modules located in scripts).

By default, the base URL of your site is `http://localhost:8888/`. You can easily change this to another URL or even multiple URL's (see 'Advanced Usage'). All base URL's being used are printed in the console window. 

The most commonly used Nancy namespaces are also imported into your script:
* `Nancy`
* `Nancy.Bootstrapper`
* `Nancy.Conventions`
* `Nancy.Cryptography`
* `Nancy.ErrorHandling`
* `Nancy.Hosting.Self`
* `Nancy.ModelBinding`
* `Nancy.Security`
* `Nancy.Validation`

You can import more namespaces with `using` statements in your script. If you think another namespace should be imported by default, please [create an issue](https://github.com/adamralph/scriptcs-nancy/issues/new/) or [send a pull request](https://help.github.com/articles/creating-a-pull-request/).

## Advanced Usage

As demonstrated above, the simplest `NancyPack` method is:
```C#
public void Host()
```

You can also use one of the richer methods for customised behaviour:
```C#
// single or multiple URI's using either string or System.Uri arguments
public void Host(params string[] baseUriStrings);
public void Host(params Uri[] baseUris)
// single URI with a custom HostConfiguration and/or INancyBootstrapper
public void Host(Uri baseUri, HostConfiguration configuration);
public void Host(Uri baseUri, INancyBootstrapper bootstrapper);
public void Host(Uri baseUri, INancyBootstrapper bootstrapper, HostConfiguration configuration);
// single or multiple URI's with a custom HostConfiguration and/or INancyBootstrapper
public void Host(HostConfiguration configuration, params Uri[] baseUris);
public void Host(INancyBootstrapper bootstrapper, params Uri[] baseUris);
public void Host(INancyBootstrapper bootstrapper, HostConfiguration configuration, params Uri[] baseUris);
```

### Custom URL's

A single URL:
```C#
Require<NancyPack>().Host("http://localhost:7777/");
```

Multiple URL's:
```C#
Require<NancyPack>().Host("http://localhost:7777/", "http://localhost/hellonancy/");
```

### Hosting modules contained in an assembly

Simply install your assembly (see the [docs](https://github.com/scriptcs/scriptcs/wiki/Writing-a-script#referencing-assemblies "scriptcs documentation")) and `NancyPack` will automatically find any modules it contains.

### Using a custom bootstrapper

You can use a custom bootstrapper by calling a `Host()` method which accepts an `INancyBootstrapper`:
```C#
Require<NancyPack>().Host(new CustomBoostrapper(...));
```

The easiest way to write a custom bootstrapper is to inherit from `DefaultNancyPackBootstrapper` as show in [this example](https://github.com/adamralph/scriptcs-nancy/blob/master/src/sample/start2.csx).

### Registering dependencies

Nancy's built in auto registration does not work in the scriptcs environment. To register dependencies you need to provide a custom bootstrapper (see above) which [manually registers your dependencies](https://github.com/NancyFx/Nancy/wiki/Bootstrapping-nancy#part-2---manually-registering-dependencies "Manually Registering Dependencies") as shown in [this example](https://github.com/adamralph/scriptcs-nancy/blob/master/src/sample/start2.csx).

### Managing the host yourself

You can also manage the lifetime of the host yourself:
```C#
using (var host = new NancyHost(new DefaultNancyPackBootstrapper(), new Uri("http://localhost:88/")))
{
    host.Start();    
    Console.ReadKey();
}
```

Have fun!

## HttpListenerException

Two common causes:

1. You do not have permission to host an HTTP listener at the desired URL. This can be solved by running your command prompt as an administrator or following the steps described [here](https://github.com/NancyFx/Nancy/wiki/Self-Hosting-Nancy#httplistenerexception).

1. Another process is already listening  at the desired URL. Kill the other process and re-try.

## Updates

Releases will be pushed regularly to [NuGet](https://nuget.org/packages/ScriptCs.Nancy/). For update notifications, follow [@adamralph](https://twitter.com/#!/adamralph).

To build manually, clone or fork this repository and see ['How to build'](https://github.com/adamralph/scriptcs-nancy/blob/master/how_to_build.md).

## Can I help to improve it and/or fix bugs? ##

Absolutely! Please feel free to raise issues, fork the source code, send pull requests, etc.

No pull request is too small. Even whitespace fixes are appreciated. Before you contribute anything make sure you read [CONTRIBUTING.md](https://github.com/adamralph/scriptcs-nancy/blob/master/CONTRIBUTING.md).

## What do the version numbers mean? ##

ScriptCs.Nancy uses [Semantic Versioning](http://semver.org/). The current release is 0.x which means 'initial development'. Version 1.0 will follow a stable release of scriptcs.
