// to run this sample, execute:
// scriptcs -install
// scriptcs host1.csx

Require<NancyPack>().Host();

public class IndexModule : NancyModule
{
    public IndexModule()
    {
        Get["/"] = _ =>	View["index"]; // located in views folder
    }
}

public class HelloModule : NancyModule
{
    public HelloModule()
    {
        Get["/hello"] = _ => "Hello World!";
    }
}
