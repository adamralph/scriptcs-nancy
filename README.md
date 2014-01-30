# [![ScriptCs](https://secure.gravatar.com/avatar/5c754f646971d8bc800b9d4057931938?s=200)](http://scriptcs.net/).[![Nancy](https://secure.gravatar.com/avatar/8e00fa6da668702f8b73ac4caebfbee4?s=200)](http://nancyfx.org/)

A [scriptcs](https://github.com/scriptcs/scriptcs) [script pack](https://github.com/scriptcs/scriptcs/wiki/Script-Packs-master-list) for [Nancy](https://github.com/NancyFx/Nancy).

Continue your journey down the super-duper-happy-path by self-hosting Nancy with a single line of code:
```C#
Require<NancyPack>().Go();
```

Get it on [Nuget](https://nuget.org/packages/ScriptCs.Nancy/).

## Quickstart (Interactive)

* Ensure you have [scriptcs installed](https://github.com/scriptcs/scriptcs#getting-scriptcs).
* Open a command prompt *as an administrator*.
* Create a folder, e.g. `mkdir C:\hellonancy`.
* Navigate to your folder and run `scriptcs -install ScriptCs.Nancy`.
* Run `scriptcs`.
* Enter `Require<NancyPack>().Get("/", _ => "Hi!").Go().Browse();`

Congratulations! You've created your first *interactively* self-hosted website using scriptcs and Nancy! :trophy:

## Quickstart (Script)

* Ensure you have [scriptcs installed](https://github.com/scriptcs/scriptcs#getting-scriptcs).
* Open a command prompt *as an administrator*.
* Create a folder, e.g. `mkdir C:\hellonancy`.
* Navigate to your folder and run `scriptcs -install ScriptCs.Nancy`.
* Create a file named `start.csx` containing:

```C#
Require<NancyPack>().Get("/", _ => "Hello world").Host();
```

* Run `scriptcs start.csx`.
* Browse to `http://localhost:8888/`.

Congratulations! You've created your first *scripted* self-hosted website using scriptcs and Nancy! :trophy:

## How it works

As well as any routes registered using the above in-line syntax, ScriptCs.Nancy also auto-magically finds all modules in your script/session  (including those loaded with [`#load`](https://github.com/scriptcs/scriptcs/wiki/Writing-a-script#loading-referenced-scripts "Loading referenced scripts")) and all modules in any [referenced assemblies](https://github.com/scriptcs/scriptcs/wiki/Writing-a-script#referencing-assemblies). The names of all modules found are printed in the console window. If any modules have dependencies, they are also auto-magically wired up! :sunglasses:

Take a look at some samples demonstrating these behaviours in the [sample folder](https://github.com/adamralph/scriptcs-nancy/blob/master/src/sample).


`Go()` and `Stop()` start and stop hosting. `Host()` is a convenience for script writing. It calls `Go()`, waits for the user to press a key and then calls `Stop()`. Use it in place of `Go()` in any examples if you need this behaviour.

By default, the base URL of your site is `http://localhost:8888/`. You can easily change this to another URL or even multiple URL's (see 'Advanced Usage'). All base URL's being used are printed in the console window. 

The most commonly used Nancy namespaces are also imported into your script/session:
* `Nancy`
* `Nancy.Bootstrapper`
* `Nancy.Conventions`
* `Nancy.Cryptography`
* `Nancy.ErrorHandling`
* `Nancy.Hosting.Self`
* `Nancy.ModelBinding`
* `Nancy.Security`
* `Nancy.Validation`

You can import more namespaces with your own `using` statements. If you think another namespace should be imported by default, please [create an issue](https://github.com/adamralph/scriptcs-nancy/issues/new/) or [send a pull request](https://help.github.com/articles/creating-a-pull-request/).

## Advanced Usage

### Interactive Tips

When using scriptcs interactively, assign a variable to `NancyPack` to save your wrists:

```C#
> var n = Require<NancyPack>();
> n.Get("/", _ => "Hello world");
> n.Go();
> n.Browse();
> #load "hhgtg.csx"
> n.At(42, 54, 66).Go().BrowseAll("vogons/poetry");     // also chain method calls whenever possible
```
(See below for an explanation of these methods.)

Note that if you call any methods which alter the configuration of the host whist it is already running, you need to call `Go()` again for the changes to be reflected.

### In-line routes

At the top level, ScriptCs.Nancy provides convenience methods for registering routes without having to write a module class. E.g.

```C#
var n = Require<NancyPack>();
n.Get["/"] = _ => "Hello world";
n.Post["/"] = _ => ... ; // do something else cool
```

All the common overloads for defining DELETE, GET, OPTIONS, PATCH, POST and PUT including `async` variants are provided for you.

Dropping down one level, you can get a reference to the `RouteBuilder` instances themselves: 

```C#
n.Get(g => g["/"] = _ => "Hello world");
```

This is useful if you want to call any methods or extension methods on `RouteBuilder` which are not wrapped by the top level convenience methods.

Dropping down another level, you can get a reference to the default module instance which is used to host in-line routes:

```C#
n.Module(m => m.Get["/"] = _ => "Hello world");
```

This is useful if you want to configure the module in ways that the `RouteBuilder` does not allow for. The action that you pass to the `Module()` method will be invoked during the construction of the `DefaultModule` instance the next time you call `NancyPack.Go()`.

### Custom URL's

```C#
At(777)                                                 // http://localhost:777
At(777, 888)                                            // http://localhost:777 and http://localhost:888
At("http://localhost/abc/")
At("http://localhost/abc/", "http://localhost/def/")
```
### Interactive browsing

`Browse()` opens your default browser using the root URL of the host (or the first URL if multiple URLs are being used).

`BrowseAll()` opens a browser window for each root URL when multiple URLs are being used.

To browse to a specific resource, pass the relative address of the resource as an argument, e.g. `Browse("hello")` or `BrowseAll("hello")`. 

### Manually registering dependencies

Nancy's built in auto registration works perfectly well in the scriptcs environment as demonstrated in [this sample](https://github.com/adamralph/scriptcs-nancy/blob/master/src/sample/host1.csx). If you don't want to take advantage of this feature you can also [manually register your dependencies](https://github.com/NancyFx/Nancy/wiki/Bootstrapping-nancy#part-2---manually-registering-dependencies "Manually Registering Dependencies") as shown in [this sample](https://github.com/adamralph/scriptcs-nancy/blob/master/src/sample/host2.csx).

### Managing the host yourself

If you don't want to take advantage of the built in hosting in ScriptCs.Nancy, you can also manage the lifetime of the host yourself:
```C#
using (var host = new NancyHost(new DefaultNancyPackBootstrapper(), new Uri("http://localhost:8888/")))
{
    host.Start();    
    Console.ReadKey();
}
```
Note that `DefaultNancyBootstrapper` is not suited to scriptcs. `DefaultNancyPackBootstrapper` inherits from `DefaultNancyBootstrapper` and is specifically designed to work with scriptcs.

## HttpListenerException :scream:

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

## Sponsors ##
Our build server is kindly provided by [CodeBetter](http://codebetter.com/) and [JetBrains](http://www.jetbrains.com/).

![YouTrack and TeamCity](http://www.jetbrains.com/img/banners/Codebetter300x250.png)
