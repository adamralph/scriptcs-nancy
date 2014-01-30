// to run this sample, execute:
//   scriptcs -install
//   scriptcs host2.csx

Require<NancyPack>().Use(new CustomBootstrapper()).Host();

public class IndexModule : NancyModule
{
    public IndexModule(IGreeter greeter)
    {
        Get["/"] = _ => greeter.Greeting;
    }
}

public interface IGreeter
{
    string Greeting { get; }
}

public class Greeter : IGreeter
{
    private readonly string greeting;

    public Greeter(string greeting)
    {
        this.greeting = greeting;
    }

    public string Greeting
    {
        get { return this.greeting; }
    }
}

public class CustomBootstrapper : DefaultNancyPackBootstrapper
{
    protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, IPipelines pipelines)
    {
        base.ApplicationStartup(container, pipelines);
        container.Register(typeof(IGreeter), (f, o) => new Greeter("Hello World!"));
    }
}
