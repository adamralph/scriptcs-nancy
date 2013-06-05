// to run this sample, execute:
// scriptcs -install
// scriptcs start1.csx

var n = Require<NancyPack>();
n.Start();

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
